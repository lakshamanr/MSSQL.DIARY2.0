using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV
{
    public class srvDatabaseTrigger
    {
        public List<PropertyInfo> GetAllDatabaseTrigger(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllDatabaseTrigger();
            }
        }

        public List<TriggerInfo> GetTriggerInfosByName(string istrdbName, string istrTriggerName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetTriggerInfosByName(istrTriggerName);
            }
        }

        public void CreateOrUpdateTriggerDescription(string istrdbName, string astrDescription_Value,
            string astrTrigger_Name)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateTriggerDescription(astrDescription_Value, astrTrigger_Name);
            }
        }
    }
}