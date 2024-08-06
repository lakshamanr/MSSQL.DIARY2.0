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
        public List<ReferencesModel> GetObjectThatDependsOn(string astrObjectName)
        {
            List<ReferencesModel> listOfObjectDependncy = new List<ReferencesModel>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    try
                    {
                        System.Data.Common.DbCommand commad = conn.CreateCommand();
                        string newObjectName = astrObjectName.Replace(
                            astrObjectName.Substring(0, astrObjectName.IndexOf(".", StringComparison.Ordinal)) + ".",
                            "");
                        commad.CommandText =
                            SqlQueryConstant.ObjectThatDependsOn.Replace("@ObjectName", "'" + newObjectName + "'");
                        commad.CommandTimeout = 10 * 60;
                        Database.OpenConnection();
                        using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    listOfObjectDependncy.Add(new ReferencesModel
                                    {
                                        ThePath = reader.SafeGetString(0),
                                        TheFullEntityName = reader.SafeGetString(1),
                                        TheType = reader.SafeGetString(2),
                                        iteration = reader.GetInt32(3)
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return listOfObjectDependncy;
        }

        public List<ReferencesModel> GetObjectOnWhichDepends(string astrObjectName)
        {
            List<ReferencesModel> listOfObjectDependncy = new List<ReferencesModel>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    try
                    {
                        System.Data.Common.DbCommand commad = conn.CreateCommand();
                        string newObjectName = astrObjectName.Replace(
                            astrObjectName.Substring(0, astrObjectName.IndexOf(".", StringComparison.Ordinal)) + ".",
                            "");
                        commad.CommandText =
                            SqlQueryConstant.ObjectOnWhichDepends.Replace("@ObjectName", "'" + newObjectName + "'");
                        commad.CommandTimeout = 10 * 60;
                        Database.OpenConnection();
                        using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    listOfObjectDependncy.Add(new ReferencesModel
                                    {
                                        ThePath = reader.SafeGetString(0),
                                        TheFullEntityName = reader.SafeGetString(1),
                                        TheType = reader.SafeGetString(2),
                                        iteration = reader.GetInt32(3)
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return listOfObjectDependncy;
        }
    }
}