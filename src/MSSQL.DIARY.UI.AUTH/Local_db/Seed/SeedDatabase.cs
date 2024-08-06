using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSSQL.DIARY.UI.Local_db.Models;

namespace MSSQL.DIARY.UI.Local_db.Seed 
{
    public static class SeedDatabase
    {
        public static string S_SERVER_Name;
        public static string S_CONNECTION_STRING;

        public static void SetServerInformations(this IConfiguration _configuration)
        {
            S_SERVER_Name = _configuration.GetSection("DefaultEnvirmanetDetails").GetSection("ServerName").Value;
            S_CONNECTION_STRING = _configuration.GetSection("DefaultEnvirmanetDetails").GetSection("ConnectionString").Value;
        }
            public static void SeedData(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated(); 
            if (!context.users.Any())
            {
                
                var identityUser = new Users
                {
                    UserName = "admin",
                    Password = "admin",
                    IsAdmin = true, 
                    SERVER_NAME= S_SERVER_Name, //@"DESKTOP-NFUD15G\SQLEXPRESS",
                    CONNECTION_STRING = S_CONNECTION_STRING //@"Data Source =DESKTOP-NFUD15G\SQLEXPRESS; Initial Catalog =AdventureWorks2016; User Id = mssql; Password = mssql; Trusted_Connection = false",

                };
                context.users.Add(identityUser);
                context.SaveChanges();
        }
    }
    }
}