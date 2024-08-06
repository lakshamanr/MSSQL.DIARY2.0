using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Models;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public static bool IsAlreadyLogin;

        public MssqlDiaryContext(string astrDbName = null)
        {
            IstrDbInstance = astrDbName;
        }

        public MssqlDiaryContext(DbContextOptions<MssqlDiaryContext> options) : base(options)
        {
        }

        public static ServerLogin LoginDetails { get; set; }

        public string IstrDbInstance { get; set; }

        public string GetDatabaseName
        {
            get
            {
                using (System.Data.Common.DbConnection con = Database.GetDbConnection())
                {
                    return con.Database;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    if (LoginDetails.IsNotNull())
                    {
                        optionsBuilder.UseSqlServer(
                                        string.IsNullOrEmpty(IstrDbInstance)
                                            ? $"Server={LoginDetails?.istrServerName};Database={LoginDetails?.istrDatabaseName}; User Id={LoginDetails?.istrUserName}; Password={LoginDetails?.istrPassword};Trusted_Connection=false;"
                                            : $"Server={LoginDetails?.istrServerName};Database={IstrDbInstance}; User Id={LoginDetails?.istrUserName}; Password={LoginDetails?.istrPassword};Trusted_Connection=false;");
                        // optionsBuilder.UseSqlServer($"Server=DESKTOP-NFUD15G;Database={istrDbInstance}; User Id=msssqldoc; Password=msssqldoc;Trusted_Connection=false;");
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }
    }
}