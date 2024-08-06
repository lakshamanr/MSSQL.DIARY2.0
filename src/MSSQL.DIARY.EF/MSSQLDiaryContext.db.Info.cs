using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Models;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext : IMssqlDiaryContext
    {
        protected MssqlDiaryContext()
        {
        }

        public MssqlDiaryContext(DbContextOptions options) : base(options)
        {
        }

        public List<PropertyInfo> GetdbProperties()
        {
            List<PropertyInfo> dbProperties = new List<PropertyInfo>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText =
                        SqlQueryConstant.GetdbProperties.Replace("@DatabaseName", $"'{conn.Database}'");
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    dbProperties.Add(new PropertyInfo
                                    {
                                        istrName = reader.GetName(i),
                                        istrValue = reader.GetString(i)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return dbProperties;
        }

        public List<PropertyInfo> GetdbOptions()
        {
            List<PropertyInfo> dbOptions = new List<PropertyInfo>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetdbOptions.Replace("@DatabaseName", $"'{conn.Database}'");
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < 14; i++)
                                {
                                    dbOptions.Add(new PropertyInfo
                                    {
                                        istrName = reader.GetName(i),
                                        istrValue = reader.GetString(i)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return dbOptions;
        }

        public List<FileInfomration> GetdbFiles()
        {
            List<FileInfomration> dbFile = new List<FileInfomration>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetdbFiles.Replace("@DatabaseName", $"'{conn.Database}'");
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dbFile.Add(new FileInfomration
                                {
                                    Name = reader.GetString(0),
                                    FileType = reader.GetString(1),
                                    FileLocation = reader.GetString(2),
                                    FileSize = reader.GetInt32(3).ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return dbFile;
        }

        public List<TableFKDependency> GetAllTableRefernce(string istrSchemaName = null)
        {
            List<TableFKDependency> tableFkDependencies = new List<TableFKDependency>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    if (istrSchemaName.IsNullOrEmpty())
                    {
                        commad.CommandText = SqlQueryConstant.AllDatabaseReferances;
                    }
                    else
                    {
                        commad.CommandText =
                            SqlQueryConstant.AllDatabaseReferancesBySchemaName.Replace("@SchemaName",
                                $"'{istrSchemaName}'");
                        ;
                    }

                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableFkDependencies.Add(new TableFKDependency
                                {
                                    Fk_name = reader.GetString(0),
                                    fk_refe_table_name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tableFkDependencies;
        }
    }
}