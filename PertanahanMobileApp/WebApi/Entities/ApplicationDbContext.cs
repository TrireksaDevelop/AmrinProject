using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(GetConnection());
        }

        private string GetConnection()
        {
            /*const string databaseName = "dbpertanahan";
            const string databaseUser = "root";
            const string databasePass = "";*/

            const string databaseName = "dbpertanahan";
            const string databaseUser = "dbpertanahan";
            const string databasePass = "Ty0286!NTH_6";

            var con= $"Server=den1.mysql6.gear.host;" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling=true;";
            return con;
        }
    }
}
