using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.ModelDTO;
using AppCore.Services;
using AppCore.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientPermohonanController : ControllerBase
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
