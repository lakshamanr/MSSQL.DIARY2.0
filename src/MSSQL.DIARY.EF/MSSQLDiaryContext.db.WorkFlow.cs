using Microsoft.EntityFrameworkCore;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Helper;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.EF
{
    public partial class MssqlDiaryContext
    {
        public List<string> GetWorkList()
        {
            List<string> lstWorkflows = new List<string>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText = SqlQueryConstant.GetWorkList;
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstWorkflows.Add(reader.SafeGetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //igonre
            }

            return lstWorkflows;
        }

        public List<string> GetWorkDetailsbyName(string astrWorkFlowName)
        {
            List<string> lstWorkFlowList = new List<string>();
            try
            {
                using (System.Data.Common.DbConnection conn = Database.GetDbConnection())
                {
                    System.Data.Common.DbCommand commad = conn.CreateCommand();
                    commad.CommandText =
                        SqlQueryConstant.GetWorkFlowDetailsbyName.Replace("@WorkFlowName",
                            "'" + astrWorkFlowName + "'");
                    commad.CommandTimeout = 10 * 60;
                    Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = commad.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lstWorkFlowList.Add(reader.SafeGetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //igonre
            }

            return lstWorkFlowList;
        }
    }
}