using MoreLinq;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseFunctions
    {
        public SrvDatabaseFunctions(string function_type)
        {
            this.function_type = function_type;
        }

        public string function_type { get; set; }

        public List<FunctionDependencies> GetFunctionDependencies(string istrdbName, string astrFunctionName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetFunctionDependencies(astrFunctionName, function_type).DistinctBy(x => x.name)
                    .ToList();
            }
        }

        public List<FunctionProperties> GetFunctionProperties(string istrdbName, string astrFunctionName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetFunctionProperties(astrFunctionName, function_type);
            }
        }

        public List<FunctionParameters> GetFunctionParameters(string istrdbName, string astrFunctionName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetFunctionParameters(astrFunctionName, function_type);
            }
        }

        public FunctionCreateScript GetFunctionCreateScript(string istrdbName, string astrFunctionName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetFunctionCreateScript(astrFunctionName, function_type);
            }
        }

        public List<PropertyInfo> GetAllFunctionWithMsDescriptions(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllFunctionWithMsDescriptions(function_type);
            }
        }

        public PropertyInfo GetFunctionMsDescriptions(string istrdbName, string astrFunctionName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllFunctionWithMsDescriptions(function_type)
                    .FirstOrDefault(x => x.istrName.Contains(astrFunctionName)) ?? new PropertyInfo
                    { istrName = astrFunctionName, istrValue = "" };
            }
        }

        public void CreateOrUpdateFunctionDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name, string astrFunctionName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateFunctionDescription(astrDescription_Value, astrSchema_Name,
                    astrFunctionName);
            }
        }
    }
}