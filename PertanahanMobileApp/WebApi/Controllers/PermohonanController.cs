using System;
using System.Threading.Tasks;
using AppCore.Services;
using AppCore.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AppCore.ModelDTO;
using System.Linq;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Permohonan")]
    public class PermohonanController : Controller
    {
        
        public PermohonanController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }
        IPermohonanService service;

        private UserManager<IdentityUser> UserManager { get; }

        // GET: api/Permohonan
        [HttpGet("{Id}/pemohon",Name = "GetByPemohonId")]
        public IActionResult GetByPemohonId(int Id)
        {
            service = new PermohonanService(new UOWPermohonan());
            var pemohon = service.GetPemohon(Id);
            var result = service.GetPermohonans();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }


        // GET: api/Permohonan/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                service = new PermohonanService(new UOWPermohonan());
               var result= service.GetPermohonan(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
       
        // PUT: api/Permohonan/5
        [HttpPut("{id}",Name ="Put")]
        public async Task<IActionResult> Put(int id,permohonan item)
        {
            try
            {
                service = new PermohonanService(new UOWPermohonan());
                bool result = await service.UpdatePermohonan(item);
                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }

        }

        [HttpPost("UpdatePermohonan")]
        public async Task<IActionResult> UpdatePermohonan([FromBody]permohonan item)
        {
            try
            {
                service = new PermohonanService(new UOWPermohonan());
                bool result = await service.UpdatePermohonan(item);
                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        ///api for admin
        ///
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminWork()
        {   
            try
            {
                var id = UserManager.GetUserId(User);
                if (id == null)
                    throw new SystemException("Anda Belum Login");
                var profile = await User.GetPetugas(id);
                if (profile != null)
                {
                    var service = new PermohonanService(new UOWPermohonan());
                    var adminService = new AdminService(profile, new PermohonanService(new UOWPermohonan()), new BidangUOW(profile));
                    var list = new List<permohonan>();
                    foreach (var item in profile.Bidangs)
                    {
                        adminService.SetBidangTugas(item);
                        var result = adminService.GetPermohonans();
               
                        foreach (var data in result)
                        {
                            service.SetCurrentPermohonan(data);
                            data.CurrentTahapan = service.GetCurrentTahapan();
                            data.NextTahapan = service.GetNextTahapan();
                            if (data.NextTahapan != null && data.NextTahapan.BidangId == item.Id)
                                list.Add(data);
                        }
                    }

                    
                    return Ok(list);
                }
                else
                    throw new SystemException("Anda Tidak Memiliki Akses");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("CompleteStep")]
        public async Task<IActionResult> NextStep(permohonan item)
        {
            try
            {
                var id = UserManager.GetUserId(User);
                if (id == null)
                    throw new SystemException("Anda Belum Login");
                var profile = await User.GetPetugas(id);
                if (profile != null)
                {
                    var service = new PermohonanService(new UOWPermohonan());
                   
                    item.NextTahapan = service.GetNextTahapan();
                    service.SetCurrentPermohonan(item);
                   bool success= service.SetNextStep();

                    return Ok(success);
                }
                else
                    throw new SystemException("Anda Tidak Memiliki Akses");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
            
        
    }
}
