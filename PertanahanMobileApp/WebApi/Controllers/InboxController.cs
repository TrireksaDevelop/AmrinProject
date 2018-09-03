using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : ControllerBase
    {
        // GET: api/Inbox
       
        // GET: api/Inbox/5
        [HttpGet("{id}", Name = "Get")]
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
        public IActionResult Post([FromBody] inbox value)
        {

            try
            {
                if (value.Id <= 0)
                    throw new SystemException("Permohonan Tidak Ditemukan");


                var service = new AppCore.Services.InboxServices(value.Id);
                var result = service.AddNewMessage(value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
