﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Services;
using AppCore.UnitOfWorks;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Permohonan")]
    public class PermohonanController : Controller
    {
        IPermohonanService service;
       
        // GET: api/Permohonan
        [HttpGet("{Id}/pemohon",Name = "GetByPemohonId")]
        public IActionResult GetByPemohonId(int Id)
        {
            service = new PermohonanService(new UOWPermohonan());
            var pemohon = service.GetPemohon(Id);
            var result = service.GetPermohonans();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        // GET: api/Permohonan/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Permohonan
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }
        
        // PUT: api/Permohonan/5
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