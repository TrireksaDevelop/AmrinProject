using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.ModelDTO;
using AppCore.Services;
using AppCore.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ClientPermohonan")]
    //[Authorize]
    public class ClientPermohonanController : Controller
    {
        public UserManager<IdentityUser> UserManagers { get; }

        // GET: api/ClientPermohonan
        public ClientPermohonanController(UserManager<IdentityUser> userManager)
        {
            UserManagers = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var id = UserManagers.GetUserId(User);
                var user =await User.GetPemohon(id);
                var service = new PermohonanService(user, new UOWPermohonan());
                    var result = service.GetPermohonans();
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/ClientPermohonan/5
        [HttpGet("last", Name = "GetLast")]
        public async Task<IActionResult> GetLast()
        {
            try
            {
                var id = UserManagers.GetUserId(User);
                var user = await User.GetPemohon(id);
                var service = new PermohonanService(user, new UOWPermohonan());
                var result = service.Permohonan;
                result.Tahapans = service.ItemsTahapan();
                if (result != null)
                {
                    result.CurrentTahapan = service.GetCurrentTahapan();
                    result.NextTahapan = service.GetNextTahapan();
                    


                    return Ok(result);
                }
          
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetClientPermohonan")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var Userid = UserManagers.GetUserId(User);
                var user = await User.GetPemohon(Userid);
                var service = new PermohonanService(new UOWPermohonan());
                var result = service.GetPermohonan(id);
                service.Permohonan = result;
                result.Tahapans = service.ItemsTahapan();
                if (result != null)
                {
                    result.CurrentTahapan = service.GetCurrentTahapan();
                    result.NextTahapan = service.GetNextTahapan();
                    return Ok(result);
                }

                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/ClientPermohonan
        [HttpPost]
        public IActionResult Post([FromBody] permohonan value)
        {
            try
            {
                var id = UserManagers.GetUserId(User);
                var client = new ClientService(id);
                if (client.Pemohon == null)
                {
                    throw new SystemException("Maaf Anda Belum Terdaftar");
                }
                var service = new PermohonanService(client.Pemohon, new UOWPermohonan());
                var result = service.CreatePermohonan(new layanan { Id = value.IdLayanan });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ClientPermohonan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] pemohon value)
        {
            try
            {
                var service = new ClientService();
                pemohon result = await service.UpdateProfile(value);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
