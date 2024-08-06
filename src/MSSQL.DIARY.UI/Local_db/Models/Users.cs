using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSSQL.DIARY.UI.Local_db.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } 
        public string SERVER_NAME { get; set; }
        public bool IsAdmin { get; set; }
        public string CONNECTION_STRING { get; set; }  
    }
    public class DatabaseModule
    {
        [Key]
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string DbModuleName { get; set; }
        public string tables { get; set; } 
    }
}
