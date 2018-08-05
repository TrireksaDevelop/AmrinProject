using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.ModelDTO;
using AppCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Petugas")]
    public class PetugasController : Controller
    {
        IMasterService service = new MasterService();
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public PetugasController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: api/Petugas
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                
                var results = service.GetPetugas();
                return Ok(results);
            }
            catch (Exception ex)
            {

             return   BadRequest(ex.Message);
            }
        }

        // GET: api/Petugas/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                petugas results = service.GetPetugasById(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // POST: api/Petugas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]petugas value)
        {
            try
            {
                if(value!=null)
                {
                    var result= await Register(value, "Petugas");
                    return Ok(result);
                }
                throw new SystemException("Data Tidak Valid");
          
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private async Task<petugas> Register(petugas item, string role)
        {
            var user = new IdentityUser
            {
                Email = item.Email,
                UserName = item.Email
            };
            try
            {
              

                var result = await _userManager.CreateAsync(user, "Petugas@123");
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        var roleCreated = await _roleManager.CreateAsync(new IdentityRole { Name = role });
                        if (!roleCreated.Succeeded)
                            throw new SystemException("Petugas Tidak Berhasil Ditambahkan");
                    }

                    var addToRole = await _userManager.AddToRoleAsync(user, role);
                    if (!addToRole.Succeeded)
                    {
                        throw new SystemException("Petugas Tidak Berhasil Ditambahkan");
                    }

                    item.UserId = user.Id;
                    var petugasResult=  service.SaveChange(item);
                    if(petugasResult==null)
                    {
                        throw new SystemException();
                    }
                
                    return petugasResult;
                }
                throw new SystemException();


                
            }
            catch (Exception)
            {

                await _userManager.DeleteAsync(user);
                throw new SystemException("Petugas Tidak Berhasil Ditambahkan");
            }
        }

        // PUT: api/Petugas/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]petugas value)
        {
            petugas results = service.SaveChange(value);
            return Ok(results);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeletePetugas(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        
    }
}
