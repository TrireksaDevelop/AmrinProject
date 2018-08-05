using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.ModelDTO;
using AppCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/Layanan")]
    
    public class LayananController : Controller
    {
        ILayananService service = new LayananService();

        // GET: api/Layanan
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetLayanan());
        }

        // GET: api/Layanan/5
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetLayananById(id));
        }
        
        // POST: api/Layanan
        [HttpPost]
        public IActionResult Post([FromBody]layanan value)
        {
            try
            {
                var result = service.InsertLayanan(value);
                if (result != null)
                    return Ok(result);
                else
                    throw new SystemException("Tidak Dapat Tambah Data");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            
            
        }
        
        // PUT: api/Layanan/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]layanan value)
        {
            return Ok(service.UpdateLayanan(value));
        }


        [HttpPut("tahapans/{id}",Name ="EditTahapans")]
        public IActionResult PutTahapans(int id, [FromBody]List<tahapan> value)
        {
            try
            {
                List<tahapan>results= service.UpdateTahapans(id, value.Where(O=>O.Urutan>0).ToList());
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
            return Ok(service.DeleteLayanan(id));
        }
    }
}
