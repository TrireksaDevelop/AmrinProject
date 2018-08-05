using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.ModelDTO;
using AppCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Persyaratan")]
    public class PersyaratanController : Controller
    {
        ILayananService service = new LayananService();
        // GET: api/Persyaratan

        [HttpGet("{Id}/layanan")]
        public IActionResult Get(int Id)
        {
            var lay = service.GetLayananById(Id);
            if (lay == null)
                return NotFound();
            else
                return Ok(service.GetPersyaratans(lay));
        }

        // GET: api/Persyaratan/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var result =service.GetPersyartan(id);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
        
        // POST: api/Persyaratan
        [HttpPost]
        public IActionResult Post([FromBody]persyaratan value)
        {
            var result = service.InsertPersyaratan(value);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();

        }
        
        // PUT: api/Persyaratan/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]persyaratan value)
        {
            var result = service.UpdatePersyaratan(value);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = service.DeletePersyaratan(id);
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
