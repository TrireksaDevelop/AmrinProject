using Microsoft.AspNetCore.Mvc;
using AppCore.ModelDTO;
using AppCore;
using Microsoft.AspNetCore.Authorization;
using AppCore.Services;
using System;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/inbox")]
    [Authorize]
    public class InboxController : Controller
    {
        public InboxController(UserManager<IdentityUser> userManager)
        {
            UserManagers = userManager;
        }

        public UserManager<IdentityUser> UserManagers { get; }



        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                    throw new SystemException("Permohonan Tidak Ditemukan");
                var service = new AppCore.Services.InboxServices(id);
                var result = service.GetPesans();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Inbox
        [HttpPost]
        public IActionResult Post([FromBody]inbox value)
        {
            try
            {
                var id = UserManagers.GetUserId(User);
                value.Tanggal = DateTime.Now;
                value.UserId = id;
                if (value.PermohonanId <= 0)
                    throw new SystemException("Permohonan Tidak Ditemukan");
                var service = new AppCore.Services.InboxServices(value.Id);

                var result = service.AddNewMessage(value);
                if (result)
                    return Ok(value);
                else
                    throw new SystemException("Data Tidak Tersimpan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
