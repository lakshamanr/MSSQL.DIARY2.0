using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Helper;
using MSSQL.DIARY.COMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public List<PropertyInfo> GetAllStoreprocedureDescription()
        {
            List<PropertyInfo> getAllTableDesc = new List<PropertyInfo>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText = SqlQueryConstant.GetAllStoreProcWithMsDesc;
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                getAllTableDesc.Add(new PropertyInfo
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

            return getAllTableDesc;
        }

        public Ms_Description GetCreateScriptOfStoreProc(string StoreprocName)
        {
            List<PropertyInfo> getStoreProcInfos = new List<PropertyInfo>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetCreateScriptOfStoreProc.Replace("@StoreprocName",
                            "'" + StoreprocName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                getStoreProcInfos.Add(new PropertyInfo
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

            return new Ms_Description { desciption = getStoreProcInfos.FirstOrDefault()?.istrValue };
        }

        public List<SP_Depencancy> GetStoreProcDependancy(string storeprocName)
        {
            List<SP_Depencancy> getSpDependancies = new List<SP_Depencancy>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetStoreProcDependencies.Replace("@StoreprocName", "'" + storeprocName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                getSpDependancies.Add(new SP_Depencancy
                                {
                                    referencing_object_name = reader.SafeGetString(0),
                                    referencing_object_type = reader.SafeGetString(1),
                                    referenced_object_name = reader.SafeGetString(2)
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

            return getSpDependancies;
        }

        public List<SP_Parameters> GetStoreProcParameters(string storeprocName)
        {
            List<SP_Parameters> getSpParameters = new List<SP_Parameters>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetAllStoreProcParamWithMsDesc.Replace("@StoreprocName",
                            "'" + storeprocName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                getSpParameters.Add(new SP_Parameters
                                {
                                    Parameter_name = reader.SafeGetString(0),
                                    Type = reader.SafeGetString(1),
                                    Length = reader.SafeGetString(2),
                                    Prec = reader.SafeGetString(3),
                                    Scale = reader.SafeGetString(4),
                                    Param_order = reader.SafeGetString(5),
                                    Extended_property = reader.SafeGetString(7)
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

            return getSpParameters;
        }

        public List<ExecutionPlanInfo> GetCachedExecutionPlan(string storeprocName)
        {
            List<ExecutionPlanInfo> exeutionPlan = new List<ExecutionPlanInfo>();
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    //tables.Replace(tables.Substring(0, tables.IndexOf("."))+".", ""
                    string newStoreprocName =
                        storeprocName.Replace(storeprocName.Substring(0, storeprocName.IndexOf(".")) + ".", "");
                    commad.CommandText =
                        SqlQueryConstant.GetExecutionPlanOfStoreProc.Replace("@StoreprocName",
                            "'" + newStoreprocName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                exeutionPlan.Add(new ExecutionPlanInfo
                                {
                                    QueryPlanXML = reader.SafeGetString(0),
                                    UseAccounts = reader.SafeGetString(1),
                                    CacheObjectType = reader.SafeGetString(2),
                                    Size_In_Byte = reader.SafeGetString(3),
                                    SqlText = reader.SafeGetString(4)
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

            return exeutionPlan;
        }

        public void CreateOrUpdateStoreProcDescription(string astrDescriptionValue, string astrSchemaName, string storeprocName, string parameterName = null)
        {
            try
            {
                UpdateStoreProcDescription(astrDescriptionValue, astrSchemaName, storeprocName, parameterName);
            }
            catch (Exception)
            {
                CreateStoreprocDescription(astrDescriptionValue, astrSchemaName, storeprocName, parameterName);
            }
        }

        public string GetStoreProcMsDescription(string StoreprocName)
        {
            string strSpDescription = "";
            try
            {
                using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
                {
                    commad.CommandText =
                        SqlQueryConstant.GetStoreProcMsDescription.Replace("@StoreprocName", "'" + StoreprocName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                strSpDescription = reader.SafeGetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return strSpDescription;
        }

        private void UpdateStoreProcDescription(string astrDescriptionValue, string astrSchemaName, string storeprocName, string parameterName = null)
        {
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                string spName =
                    storeprocName.Replace(
                        storeprocName.Substring(0, storeprocName.IndexOf(".", StringComparison.Ordinal)) + ".", "");
                if (parameterName == null)
                {
                    commad.CommandText = SqlQueryConstant
                        .UpdateStoreProcExtendedProperty
                        .Replace("@sp_value", "'" + astrDescriptionValue + "'")
                        .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                        .Replace("@sp_Name", "'" + spName + "'");
                }
                else
                {
                    commad.CommandText = SqlQueryConstant
                            .UpdateStoreProcParameterExtendedProperty
                            .Replace("@sp_value", "'" + astrDescriptionValue + "'")
                            .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                            .Replace("@sp_Name", "'" + spName + "'")
                            .Replace("@parmeterName", "'" + parameterName + "'")
                        ;
                }

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                commad.ExecuteNonQuery();
            }
        }

        private void CreateStoreprocDescription(string astrDescriptionValue, string astrSchemaName, string storeprocName, string parameterName = null)
        {
            string spName =
                storeprocName.Replace(
                    storeprocName.Substring(0, storeprocName.IndexOf(".", StringComparison.Ordinal)) + ".", "");
            using (System.Data.Common.DbCommand commad = Database.GetDbConnection().CreateCommand())
            {
                if (parameterName == null)
                {
                    commad.CommandText = SqlQueryConstant
                        .InsertStoreProcExtendedProperty
                        .Replace("@sp_value", "'" + astrDescriptionValue + "'")
                        .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                        .Replace("@sp_Name", "'" + spName + "'");
                }
                else
                {
                    commad.CommandText = SqlQueryConstant
                            .InsertStoreProcParameterExtendedProperty
                            .Replace("@sp_value", "'" + astrDescriptionValue + "'")
                            .Replace("@Schema_Name", "'" + astrSchemaName + "'")
                            .Replace("@sp_Name", "'" + spName + "'")
                            .Replace("@parmeterName", "'" + parameterName + "'")
                        ;
                }

                commad.CommandTimeout = 10 * 60;
                Database.OpenConnection();
                try
                {
                    commad.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}