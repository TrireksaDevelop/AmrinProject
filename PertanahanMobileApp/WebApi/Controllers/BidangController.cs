using AppCore.ModelDTO;
using AppCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Bidang")]
    public class BidangController : Controller
    {
        IMasterService service = new MasterService();
        // GET: api/Petugas
        [HttpGet]
        public IActionResult Get()
        {
           
            try
            {
                var results = service.GetBidang();
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET: api/Petugas/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           
            try
            {
                var results = service.GetBidangById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST: api/Petugas
        [HttpPost]
        public IActionResult Post([FromBody]bidang value)
        {
         
            try
            {
                bidang results = service.SaveChange(value);
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Petugas/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]bidang value)
        {
          
            try
            {
                bidang results = service.SaveChange(value);
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
                return Ok(service.DeleteBidang(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
