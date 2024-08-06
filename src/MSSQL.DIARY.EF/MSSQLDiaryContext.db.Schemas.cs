using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Models;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public List<PropertyInfo> GetListOfAllSchemaAndMsDescription()
        {
            List<PropertyInfo> lstPropInfo = new List<PropertyInfo>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText = SqlQueryConstant.GetListOfAllSchemaAndMsDescription;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstPropInfo.Add(new PropertyInfo
                                {
                                    istrName = reader.GetString(0),
                                    istrValue = reader.GetString(1)
                                }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return lstPropInfo;
        }

        public void CreateOrUpdateSchemaMsDescription(string astrDescriptionValue, string astrSchemaName)
        {
            try
            {
                UpdateFunctionDescription(astrDescriptionValue, astrSchemaName);
            }
            catch (Exception)
            {
                CreateFunctionDescription(astrDescriptionValue, astrSchemaName);
            }
        }

        private void CreateFunctionDescription(string astrDescriptionValue, string astrSchemaName)
        {
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                commad.CommandText = SqlQueryConstant
                    .CreateSchemaColumnExtendedProperty
                    .Replace("@Schema_info", "'" + astrDescriptionValue + "'")
                    .Replace("@SchemaName", "'" + astrSchemaName + "'");

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                commad.ExecuteNonQuery();
            }
        }

        private void UpdateFunctionDescription(string astrDescriptionValue, string astrSchemaName)
        {
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                commad.CommandText = SqlQueryConstant
                    .UpdateSchemaColumnExtendedProperty
                    .Replace("@Schema_info", "'" + astrDescriptionValue + "'")
                    .Replace("@SchemaName", "'" + astrSchemaName + "'");

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                commad.ExecuteNonQuery();
            }
        }

        public List<SchemaReferanceInfo> GetSchemaReferanceInfo(string astrSchema_Name)
        {
            List<SchemaReferanceInfo> lstOfSchemaReferance = new List<SchemaReferanceInfo>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetAllSchemaReferancedObject.Replace("@schema_id",
                            "'" + astrSchema_Name + "'");
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstOfSchemaReferance.Add(new SchemaReferanceInfo
                                {
                                    istrName = reader.GetString(0)
                                }
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return lstOfSchemaReferance;
        }

        public Ms_Description GetSchemaMsDescription(string astrSchemaName)
        {
            Ms_Description msDesc = new Ms_Description();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetSchemaMsDescription.Replace("@schemaName", "'" + astrSchemaName + "'");
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                msDesc.desciption = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return msDesc;
        }

        //public SchemaCreateScript GetSchemaCreateSript()
        //{
        //    var sch_cs = new SchemaCreateScript();
        //    try
        //    {
        //        using (var commad = Database.GetDbConnection().CreateCommand())
        //        {
        //            commad.CommandText = SqlQueryConstant.GetDatabaseServerName;
        //            Database.OpenConnection();
        //            using (var reader = commad.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                    while (reader.Read())
        //                    {
        //                        sch_cs.istrCreateScript = reader.GetString(0);
        //                    }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return sch_cs;
        //}
    }
}