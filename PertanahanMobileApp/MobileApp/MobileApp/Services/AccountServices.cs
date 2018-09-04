using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;


[assembly: Xamarin.Forms.Dependency(typeof(MobileApp.Services.AccountServices))]
namespace MobileApp.Services
{
    public class AccountServices : PropertyChange, IAccountServices
    {
        private pemohon _pemohon;

        public pemohon Pemohon { get => _pemohon; set => SetProperty(ref _pemohon,value); }

        public Task<bool> ChangePassword(string email)
        {
            try
            {
                using (var res = new RestServices())
                {
                    var result = res.Post<bool>("", email);
                } 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<pemohon> GetProfile()
        {
            try
            {
                using (var res = new RestServices())
                {
                    try
                    {
                        var result = await res.Get<pemohon>("/account/ClientProfile");
                        if (result != null)
                        {
                          return  result;   
                        }
                        else
                        {
                            return new pemohon();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new SystemException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> Login(LoginDto model)
        {
            using (var res= new RestServices())
            {
                try
                {
                    var result = await res.Post<AuthenticationToken>("/account/login", model);
                    if(result!=null)
                    {
                        


                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public async Task<bool> Register(UserRegister model)
        {
            using (var res = new RestServices())
            {
                try
                {
                    var result = await res.Post<AuthenticationToken>("/account/UserRegister", model);
                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public async Task<bool> SaveProfileProfile()
        {
            using (var res = new RestServices())
            {
                try
                {
                    var result = await res.Put<pemohon>("api/ClientPermohonan/"+Pemohon.Id, Pemohon);
                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

      
    }
}
