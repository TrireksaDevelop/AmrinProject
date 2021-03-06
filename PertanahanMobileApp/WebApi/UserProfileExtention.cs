﻿using System;
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

        public static  Task<pemohon> GetPemohon(this IPrincipal user, string uid)
        {
            try
            {
                ClientService client = new ClientService();
                var result = client.GetPemohonBy(uid);
                if (result == null)
                    throw new SystemException("Anda Tidak Memiliki Akses");
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


    }
}