using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseStoreProc
    {
        public static NaiveCache<List<SP_PropertyInfo>> StoreprocedureDescription =
            new NaiveCache<List<SP_PropertyInfo>>();

        public static NaiveCache<List<ExecutionPlanInfo>>
            CacheExecutionPlan = new NaiveCache<List<ExecutionPlanInfo>>();

        public static NaiveCache<string> cacheThatDependsOn = new NaiveCache<string>();

        public List<SP_PropertyInfo> GetAllStoreprocedureDescription(string istrdbName)
        {
            return StoreprocedureDescription.GetOrCreate(istrdbName + "AllStoreprocedureDescription",
                () => CacheStoreProcInfo(istrdbName));
        }

        private List<SP_PropertyInfo> CacheStoreProcInfo(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                List<SP_PropertyInfo> db_sp_info = new List<SP_PropertyInfo>();
                foreach (IGrouping<string, PropertyInfo> grp in dbSqldocContext.GetAllStoreprocedureDescription().GroupBy(x => x.istrName))
                {
                    SP_PropertyInfo Property = new SP_PropertyInfo { istrName = grp.Key };
                    foreach (PropertyInfo value in grp)
                    {
                        if (value.istrValue.IsNotNullOrEmpty())
                        {
                            Property.istrValue += value.istrValue;
                        }
                    }

                    if (Property.istrValue.IsNullOrEmpty())
                    {
                        Property.istrValue += "MS Desciption of " + Property.istrName + " is Empty ";
                    }

                    db_sp_info.Add(Property);
                }

                if (!db_sp_info.Any())
                {
                    db_sp_info.Add(new SP_PropertyInfo { istrName = "", istrValue = "" });
                }

                db_sp_info.ForEach(x =>
                {
                    x.lstrCreateScript = new List<string>
                    {
                        GetCreateScriptOfStoreProc(istrdbName, x.istrName).desciption
                    };
                });

                return db_sp_info;
            }
        }

        public Ms_Description GetCreateScriptOfStoreProc(string istrdbName, string StoreprocName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetCreateScriptOfStoreProc(StoreprocName);
            }
        }

        public List<SP_Depencancy> GetStoreProcDependancy(string istrdbName, string StoreprocName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetStoreProcDependancy(StoreprocName);
            }
        }

        public string CreatorOrGetDependancyTree(string istrdbName, string storeprocName)
        {
            return cacheThatDependsOn.GetOrCreate
            (
                istrdbName + "StroreProce" + storeprocName,
                () =>
                    CreateOrGetcacheTableThatDependsOn(istrdbName, storeprocName)
            );
        }

        public string CreateOrGetcacheTableThatDependsOn(string istrdbName, string ObjectName)
        {
            SrvDatabaseObjectDependncy srvDatabaseObjectDependncy = new SrvDatabaseObjectDependncy();
            string ThatDependsOn = srvDatabaseObjectDependncy.GetObjectThatDependsOn(istrdbName, ObjectName);
            string OnWhichDepends = srvDatabaseObjectDependncy.GetObjectOnWhichDepends(istrdbName, ObjectName);
            return srvDatabaseObjectDependncy.JsonResutl(OnWhichDepends, ThatDependsOn, ObjectName);
        }

        public List<SP_Parameters> GetStoreProcParameters(string istrdbName, string StoreprocName)
        {
            List<SP_Parameters> SP_Parameters = new List<SP_Parameters>();
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                foreach (IGrouping<string, SP_Parameters> key in dbSqldocContext.GetStoreProcParameters(StoreprocName)
                    .GroupBy(x => x.Parameter_name))
                {
                    SP_Parameters sp_parameter = new SP_Parameters { Parameter_name = key.Key };
                    foreach (SP_Parameters value in key)
                    {
                        sp_parameter.Parameter_name = value.Parameter_name;
                        sp_parameter.Type = value.Type;
                        sp_parameter.Length = value.Length;
                        sp_parameter.Prec = value.Prec;
                        sp_parameter.Scale = value.Scale;
                        sp_parameter.Param_order = value.Param_order;
                        sp_parameter.Extended_property += value.Extended_property;
                    }

                    SP_Parameters.Add(sp_parameter);
                }
            }

            return SP_Parameters;
        }

        public List<ExecutionPlanInfo> GetCachedExecutionPlan(string istrdbName, string StoreprocName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                List<ExecutionPlanInfo> ExecutionPlanResult = dbSqldocContext.GetCachedExecutionPlan(StoreprocName);

                if (ExecutionPlanResult.Any())
                {
                    return CacheExecutionPlan.GetOrCreate(istrdbName + StoreprocName, () => ExecutionPlanResult);
                }

                if (CacheExecutionPlan.Cache.TryGetValue(istrdbName + StoreprocName, out List<ExecutionPlanInfo> result))
                {
                    return result;
                }

                return ExecutionPlanResult;
            }
        }

        public void CreateOrUpdateStoreProcDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name, string StoreprocName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateStoreProcDescription(astrDescription_Value, astrSchema_Name,
                    StoreprocName);
            }
        }

        public void CreateOrUpdateStoreProcParameterDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name, string StoreprocName, string ParameterName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateStoreProcDescription(astrDescription_Value, astrSchema_Name,
                    StoreprocName, ParameterName);
            }
        }

        public string GetStoreProcMsDescription(string istrdbName, string StoreprocName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetStoreProcMsDescription(StoreprocName);
            }
        }
    }
}