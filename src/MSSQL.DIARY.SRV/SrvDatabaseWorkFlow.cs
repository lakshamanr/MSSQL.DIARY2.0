using MSSQL.DIARY.EF;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseWorkFlow
    {
        public List<string> GetWorkList(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetWorkList();
            }
        }

        public List<string> BuildBusinessWorkFlowTree(string istrdbName, string istrWorkFlowName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetWorkDetailsbyName(istrWorkFlowName);
            }
        }

        public string GetWorkDetailsbyName(string istrdbName, string istrWorkFlowName)
        {
            SrvDatabaseObjectDependncy srvDatabaseObjectDependncy = new SrvDatabaseObjectDependncy();
            return srvDatabaseObjectDependncy.WorkFlowJsonResutl(
                srvDatabaseObjectDependncy
                    .GetBusinessWorkFlowJson(
                        BuildBusinessWorkFlowTree(
                            istrdbName, istrWorkFlowName)), istrWorkFlowName);
        }
    }
}