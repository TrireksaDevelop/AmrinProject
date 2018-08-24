using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using AppCore.ModelDTO;
using AppCore.Services;
using Microsoft.AspNetCore.Identity;

namespace WebApi
{
    public static class UserProfileExtention
    {
        public static async Task<petugas> GetPetugas(this IPrincipal user, string uid)
        {
            try
            {

                IMasterService domain = new MasterService();
                var result = await domain.GetPetugasByUserId(uid);
                return result;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

       


    }
}