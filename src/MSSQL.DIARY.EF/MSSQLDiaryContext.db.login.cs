using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Models;
using System;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public bool IsLoginSuccessfully(ServerLogin serverLogin)
        {
            using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
            {
                conn.ConnectionString =
                    $"Data Source ={serverLogin.istrServerName}; Initial Catalog ={serverLogin.istrDatabaseName}; User Id = {serverLogin.istrUserName}; Password = {serverLogin.istrPassword}; Trusted_Connection = false";
                try
                {
                    Database.OpenConnection();
                    Database.CloseConnection();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}