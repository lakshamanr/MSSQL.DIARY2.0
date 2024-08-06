using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.UI.Local_db.Models;

namespace MSSQL.DIARY.UI.Local_db
{
    public class ApplicationDbContext : DbContext
    { 
        public DbSet<Users> users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        
        
    }
}
