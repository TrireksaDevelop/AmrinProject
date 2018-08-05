using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AppCore.ModelDTO;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Monitoring")]
    public class MonitoringController : Controller
    {
        // GET: api/Monitoring
        [HttpGet]
        public IActionResult GetPerMohonans()
        {
            try
            {
                var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(uid))
                {
                    var service = new AppCore.Services.ClientService(uid);
                    List<permohonan> result = service.GetPermohonans();
                    return Ok(result);
                }
                else
                    return Unauthorized();
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Monitoring/5
        [HttpGet("{id}", Name = "GetLastPermohonan")]
        public IActionResult GetLastPermohonan(int id)
        {
            try
            {
                var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(uid))
                {
                    var service = new AppCore.Services.ClientService(uid);
                    permohonan result = service.GetLastPermohonan();
                    return Ok(result);
                }
                else
                    return Unauthorized();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // POST: api/Monitoring
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Monitoring/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
