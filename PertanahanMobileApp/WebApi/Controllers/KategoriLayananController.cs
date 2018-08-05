using Microsoft.AspNetCore.Mvc;
using AppCore.ModelDTO;
using AppCore;
using Microsoft.AspNetCore.Authorization;
using AppCore.Services;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/KategoriLayanan")]
    [Authorize]
    public class KategoriLayananController : Controller
    {
        private ILayananService service;

      
        public KategoriLayananController()
        {
            this.service = new LayananService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetKategories());
        }

        // GET: api/KategoriLayanan/5
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            return Ok(service.GetKategory(id));
        }

        // POST: api/KategoriLayanan
        [HttpPost]
        public IActionResult Post([FromBody]kategorilayanan value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var result = service.InsertKategory(value);
                        if (result != null)
                            return Ok(value);
                        else
                            return NotFound();
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex);
                }
              
              
            }
        }

        // PUT: api/KategoriLayanan/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]kategorilayanan value)
        {
            using (var db = new OcphDbContext())
            {
                if(ModelState.IsValid)
                {
                    var result = service.UpdateKategory(value);
                    if(result!=null)
                    {
                        return Ok(value);
                    }
                    else
                        return  BadRequest();
                }else
                    return BadRequest(ModelState);

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                if (service.DeleteKategory(id))
                {
                    return Ok();
                }
                else
                    return BadRequest();
            }
        }
    }
}
