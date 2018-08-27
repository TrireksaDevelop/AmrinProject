using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
  public interface IAccountServices
    {
        pemohon Pemohon { get; set; }
        Task<bool> Login(LoginDto model);
        Task<bool> Register();
        Task<bool> SaveProfileProfile();
    }
}
