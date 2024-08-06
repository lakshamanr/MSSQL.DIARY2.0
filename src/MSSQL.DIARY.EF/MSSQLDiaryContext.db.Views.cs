using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Helper;
using MSSQL.DIARY.COMN.Models;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext : DbContext, IMssqlDiaryContext
    {
        public List<PropertyInfo> GetAllViewsDetailsWithms_description()
        {
            List<PropertyInfo> dbProperties = new List<PropertyInfo>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetAllViewsDetailsWithMsDesc;
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dbProperties.Add(new PropertyInfo
                                {
                                    istrName = reader.SafeGetString(0),
                                    istrValue = reader.SafeGetString(1)
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

            return dbProperties;
        }

        public List<ViewDependancy> GetViewDependancies(string astrViewName)
        {
            List<ViewDependancy> lstViewdependancy = new List<ViewDependancy>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    //var newViewName = astrViewName.Replace(astrViewName.Substring(0, astrViewName.IndexOf(".")) + ".", "");
                    commad.CommandText =
                        SqlQueryConstant.GetViewsdependancies.Replace("@viewname", "'" + astrViewName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstViewdependancy.Add(new ViewDependancy
                                {
                                    name = reader.SafeGetString(0)
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

            return lstViewdependancy;
        }

        public List<View_Properties> GetViewProperties(string astrViewName)
        {
            List<View_Properties> lstViewProperties = new List<View_Properties>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText =
                        SqlQueryConstant.GetViewProperties.Replace("@viewname", "'" + astrViewName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstViewProperties.Add(new View_Properties
                                {
                                    uses_ansi_nulls = reader.SafeGetString(0),
                                    uses_quoted_identifier = reader.SafeGetString(1),
                                    create_date = reader.SafeGetString(2),
                                    modify_date = reader.SafeGetString(3)
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

            return lstViewProperties;
        }

        public List<ViewColumns> GetViewColumns(string astrViewName)
        {
            List<ViewColumns> lstGetViewColumns = new List<ViewColumns>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetViewColumns.Replace("@viewname", "'" + astrViewName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstGetViewColumns.Add(new ViewColumns
                                {
                                    name = reader.SafeGetString(0),
                                    type = reader.SafeGetString(1),
                                    updated = reader.SafeGetString(2),
                                    selected = reader.SafeGetString(3),
                                    column_name = reader.SafeGetString(4)
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

            return lstGetViewColumns;
        }

        public ViewCreateScript GetViewCreateScript(string astrViewName)
        {
            ViewCreateScript createScript = new ViewCreateScript();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    //var newViewName = astrViewName.Replace(astrViewName.Substring(0, astrViewName.IndexOf(".")) + ".", "");

                    commad.CommandText =
                        SqlQueryConstant.GetViewCreateScript.Replace("@viewname", "'" + astrViewName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();

                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                createScript.createViewScript = reader.SafeGetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return createScript;
        }
    }
}