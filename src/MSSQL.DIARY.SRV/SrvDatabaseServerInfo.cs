using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using MSSQL.DIARY.SRV.Interfaces;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseServerInfo : ISrvServerInfo
    {
        public List<string> GetServerName()
        {
            return GetServerNameList();
        }

        public List<string> GetDatabaseNames()
        {
            return GetDatabaseNameList();
        }

        public List<PropertyInfo> GetServerProperties()
        {
            return GetServerPropertiesList();
        }

        //GetAdvancedServerSettingsInfo

        public List<PropertyInfo> GetAdvancedServerSettingsInfo()
        {
            return GetAdvancedServerSettingsInfoList();
        }

        public static List<string> GetServerNameList()
        {
            List<string> lst = new List<string>();
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                lst.Add(dbSqldocContext.GetDatabaseServerName());
            }

            return lst;
        }

        private static List<string> GetDatabaseNameList()
        {
            List<string> lst = new List<string>();
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                return dbSqldocContext.GetDatabaseNames();
            }
        }

        private static List<PropertyInfo> GetServerPropertiesList()
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                return dbSqldocContext.GetServerProperties();
            }
        }

        private static List<PropertyInfo> GetAdvancedServerSettingsInfoList()
        {
            List<PropertyInfo> lst = new List<PropertyInfo>();
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                return dbSqldocContext.GetAdvancedServerSettingsInfo();
            }
        }
    }
}