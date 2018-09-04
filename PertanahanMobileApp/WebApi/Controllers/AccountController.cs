using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AppCore;
using AppCore.Services;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var roles = await _userManager.GetRolesAsync(appUser);
                var token = await GenerateJwtToken(model.Email, appUser);
                var data = new { roles, token };
                return data;
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }


        [HttpGet]
        [Authorize]
        public async Task<object> PetugasProfile()
        {
            try
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                    throw new SystemException("Anda Tidak Memiliki Akses");
                else
                {
                    var profile = await User.GetPetugas(user.Id);
                    return Ok(profile);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<object> ClientProfile()
        {
            try
            {

                //       var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var id = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(id))
                    throw new SystemException("Anda Tidak Memiliki Akses");
                else
                {

                    var profile = await User.GetPemohon(id);
                    return Ok(profile);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            try
            {
                Console.WriteLine(model.Email);
                Console.WriteLine(model.UserName);
                var role = "admin";
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, role);
                    return await GenerateJwtToken(model.Email, user);
                }

                throw new ApplicationException("UNKNOWN_ERROR");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<object> UserRegister([FromBody] UserRegisterDto model)
        {
            try
            {


                var role = "pemohon";
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, role);
                    ClientService service = new ClientService();
                    service.CreatePemohon(new AppCore.ModelDTO.pemohon { NIK = model.NIK, UserId = user.Id, Nama = model.Nama });

                    return await GenerateJwtToken(model.Email, user);
                }

                throw new ApplicationException("UNKNOWN_ERROR");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ResetPassword(ChangePasswordModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(model.Email).Result;

                if (user == null || !(_userManager.IsEmailConfirmedAsync(user).Result))
                {
                    throw new SystemException("Gagal Reset Password, Ulangi");
                }

                var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                return Ok(new ChangePasswordModel { Token=token });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var user = _userManager.FindByNameAsync(model.Email).Result;

                IdentityResult result = _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword).Result;
                if (result.Succeeded)
                {
                  return Ok(model);
                }
                else
                {
                    throw new SystemException("Error while resetting the password!");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }





        private Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var tk = new JwtSecurityTokenHandler();
            return Task.FromResult(tk.WriteToken(token) as object);
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }


        


        public class ChangePasswordModel
        {
            public string Email { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string Token { get; set; }
        }


        public class UserRegisterDto : RegisterDto
        {
            public string Nama { get; set; }
            public Gender Gender { get; set; }
            public string NIK { get; set; }
        }



        [HttpGet]
        public IActionResult SignInWithGoogle()
        {
            var provider = "Google";
            var returnUrl = "";
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }


        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }




        [HttpGet]
        public async Task<object> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                //Login Success
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == email);
                return await GenerateJwtToken(email, appUser);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (!string.IsNullOrEmpty(email)) //Cek Social Account True
                {
                     var existingUser = await _userManager.FindByEmailAsync(email);
                    if (existingUser!= null)//Cek User Registered
                    {
                        var userName = !string.IsNullOrEmpty(email) ? email : info.ProviderKey;
                        var newUser = new IdentityUser { UserName = userName, Email = email };
               //         var createResult = await _userManager.CreateAsync(newUser);
                      var  createResult = await _userManager.AddLoginAsync(newUser, info);
                        if (createResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(newUser, isPersistent: false);
                            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == newUser.Email);
                            return await GenerateJwtToken(newUser.Email, appUser);
                        }
                        foreach (var error in createResult.Errors)
                        {
                            ModelState.AddModelError("User", error.Description);
                        }
                    }else
                    {
                        //User Not Yet Register
                        return Unauthorized();
                    }
                }

                return Unauthorized();
            }
        }


     
        private  AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null)
        {
            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {
                RedirectUri = redirectUrl
            };
            authenticationProperties.Items["LoginProvider"] = provider;
            return authenticationProperties;
        }
    }

    
}