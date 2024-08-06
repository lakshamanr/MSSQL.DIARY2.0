using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseTable
    {
        public static NaiveCache<string> CacheThatDependsOn = new NaiveCache<string>();

        public static NaiveCache<List<TablePropertyInfo>> CacheAllTableDetails =
            new NaiveCache<List<TablePropertyInfo>>();

        public static NaiveCache<List<TableFragmentationDetails>> CacheAllTableFragmentationDetails =
            new NaiveCache<List<TableFragmentationDetails>>();

        public List<TablePropertyInfo> GetAllDatabaseTablesDescription(string istrdbName = null)
        {
            return CacheAllTableDetails.GetOrCreate(istrdbName, () => CreateCacheIfNot(istrdbName));
        }

        private static List<TablePropertyInfo> CreateCacheIfNot(string istrdbName)
        {
            List<TablePropertyInfo> lst = new List<TablePropertyInfo>();
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.GetAllTableDescription().GroupBy(x => x.istrName).ToList().ForEach(x =>
                {
                    TablePropertyInfo tablePropertyInfo = new TablePropertyInfo();
                    foreach (TablePropertyInfo tableinfo in x)
                    {
                        tablePropertyInfo.istrFullName = tableinfo.istrFullName;
                        tablePropertyInfo.istrName = tableinfo.istrName;
                        tablePropertyInfo.istrSchemaName = tableinfo.istrSchemaName;
                        if (tableinfo.istrValue.Length > 0)
                        {
                            tablePropertyInfo.istrValue += tableinfo.istrValue;
                        }
                        else
                        {
                            if (tablePropertyInfo.istrValue != null && tablePropertyInfo.istrValue.Length > 0)
                            {
                                tablePropertyInfo.istrValue += ";";
                            }
                            else
                            {
                                tablePropertyInfo.istrValue +=
                                    "MS Desciption of " + tableinfo.istrFullName + " is Empty ";
                            }
                        }

                        tablePropertyInfo.tableColumns = tableinfo.tableColumns;
                    }

                    if (!(tablePropertyInfo.istrName.Contains("$") || tablePropertyInfo.istrFullName.Contains("\\") ||
                          tablePropertyInfo.istrFullName.Contains("-")))
                    {
                        lst.Add(tablePropertyInfo);
                    }
                });
            }

            return lst;
        }

        public Ms_Description GetTableDescription(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetTableDescription(istrtableName);
            }
        }

        public List<TableIndexInfo> GetTableIndex(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetTableIndex(istrtableName);
            }
        }

        public TableCreateScript GetTableCreateScript(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetTableCreateScript(istrtableName);
            }
        }

        public List<Tabledependencies> GetAllTabledependencies(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllTableDependencies(istrtableName);
            }
        }

        public List<TableColumns> GetAllTablesColumn(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                List<TableColumns> lsttblcolumn = new List<TableColumns>();
                foreach (IGrouping<string, TableColumns> keyValue in dbSqldocContext.GetAllTablesColumn(istrtableName).GroupBy(x => x.columnname))
                {
                    TableColumns tblcolumn = new TableColumns { columnname = keyValue.Key };
                    foreach (TableColumns values in keyValue)
                    {
                        tblcolumn.tablename = values.tablename;
                        tblcolumn.key = values.key;
                        tblcolumn.identity = values.identity;
                        tblcolumn.max_length = values.max_length;
                        tblcolumn.allow_null = values.allow_null;
                        tblcolumn.defaultValue = values.defaultValue;
                        tblcolumn.data_type = values.data_type;
                        tblcolumn.description += values.description;
                    }

                    lsttblcolumn.Add(tblcolumn);
                }

                return lsttblcolumn;
            }
        }

        public List<TableFKDependency> GetAllTableForeignKeys(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllTableForeignKeys(istrtableName);
            }
        }

        public List<TableKeyConstraint> GetAlllKeyConstraints(string istrtableName, string istrdbName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAlllKeyConstraints(istrtableName);
            }
        }

        public bool CreateOrUpdateColumnDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name, string astrTableName, string astrColumnValue)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateColumnDescription(astrDescription_Value, astrSchema_Name, astrTableName,
                    astrColumnValue);
                return true;
            }
        }

        public void CreateOrUpdateTableDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name, string astrTableName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                dbSqldocContext.CreateOrUpdateTableDescription(astrDescription_Value, astrSchema_Name, astrTableName);
            }
        }

        public string CreatorOrGetDependancyTree(string istrdbName, string istrtableName)
        {
            return CacheThatDependsOn.GetOrCreate
            (
                istrdbName + "Table" + istrtableName,
                () =>
                    CreateOrGetcacheTableThatDependsOn(istrdbName, istrtableName)
            );
        }

        public string CreateOrGetcacheTableThatDependsOn(string istrdbName, string ObjectName)
        {
            SrvDatabaseObjectDependncy srvDatabaseObjectDependncy = new SrvDatabaseObjectDependncy();
            return srvDatabaseObjectDependncy.JsonResutl(
                srvDatabaseObjectDependncy.GetObjectThatDependsOn(istrdbName, ObjectName),
                srvDatabaseObjectDependncy.GetObjectOnWhichDepends(istrdbName, ObjectName),
                ObjectName);
        }

        public List<TableFragmentationDetails> CacheTblFramentationDetails(string istrdbName)
        {
            return CacheAllTableFragmentationDetails.GetOrCreate(istrdbName,
                () => GetAllTableFragmentationDetails(istrdbName));
        }

        private List<TableFragmentationDetails> GetAllTableFragmentationDetails(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetAllTableFragmentations();
            }
        }

        public List<TableFragmentationDetails> TableFragmentationDetails(string istrdbName, string istrtableName)
        {
            return CacheTblFramentationDetails(istrdbName).Where(x => x.TableName.Equals(istrtableName)).ToList();
        }
    }
}