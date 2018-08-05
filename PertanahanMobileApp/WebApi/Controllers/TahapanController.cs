using System;
using AppCore.ModelDTO;
using AppCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Tahapan")]
    public class TahapanController : Controller
    {
        IMasterService service = new MasterService();
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = service.GetTahapan();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var results = service.GetTahapanById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]tahapan value)
        {
            try
            {
                tahapan results = service.SaveChange(value);
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Petugas/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]tahapan value)
        {

            try
            {
                tahapan results = service.SaveChange(value);
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteTahapan(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
