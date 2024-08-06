using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.COMN.Constant;
using MSSQL.DIARY.COMN.Enums;
using MSSQL.DIARY.COMN.Helper;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System.Collections.Generic;
using System.Linq;

namespace MSSQL.DIARY.SRV
{
    public class SrvTreeViewJsonGen
    {
        public static NaiveCache<string> cacheLeftMenu = new NaiveCache<string>();

        public SrvTreeViewJsonGen(string istrRootPath)
        {
            IstrRootPath = istrRootPath;
            Service = new SrvLeftNavigationTreeView(istrRootPath);
        }

        private string IstrRootPath { get; }
        private SrvLeftNavigationTreeView Service { get; }
        private string IstrProjectName { get; set; }
        private string IstrServerName { get; set; }
        private string IstrDatabaseName { get; set; }

        private TreeViewJson SearchInDb()
        {
            return new TreeViewJson
            {
                text = "Search",
                icon = "fa fa-search",
                mdaIcon = "Search",
                link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Tables",
                selected = true,
                badge = 12,
                expand = true,
                //SchemaEnums = SchemaEnums.AllTable,
                children = SearchInDbObjects()
            };
        }

        private List<TreeViewJson> SearchInDbObjects()
        {
            List<TreeViewJson> SearchInDbObject = new List<TreeViewJson>
            {
                new TreeViewJson
                {
                    text = "Search In column",
                    icon = "fa fa-search",
                    mdaIcon = "Search In column",
                    expand = false,
                    link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Search/Column",
                    selected = true,
                    badge = 12
                    //SchemaEnums = SchemaEnums.TableCoumns,
                },
                new TreeViewJson
                {
                    text = "Search In column",
                    icon = "fa fa-search",
                    mdaIcon = "Search In column",
                    expand = false,
                    link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Search/Column",
                    selected = true,
                    badge = 12
                    //SchemaEnums = SchemaEnums.TableCoumns,
                }
            };
            return SearchInDbObject;
        }

        private TreeViewJson GetDatabase(string astrdatabaseName = null)
        {
            return new TreeViewJson
            {
                text = astrdatabaseName,
                icon = "fa fa-database fa-fw",
                mdaIcon = astrdatabaseName,
                link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}",
                selected = true,
                badge = 12,
                expand = true,
                SchemaEnums = SchemaEnums.AllDatabase,
                children = new List<TreeViewJson>
                {
                    // SearchInDb(),
                    GetTables(),
                    GetViews(),
                    GetProgrammability()
                    //GetStorage(),
                    //GetSecurity()
                }
            };
        }

        private TreeViewJson GetUserDatabaseNames(string astrUserDataBaseName = null, string astrdatabaseName = null)
        {
            return new TreeViewJson
            {
                text = astrUserDataBaseName,
                //icon = "fa fa-home fa-fw",
                mdaIcon = astrUserDataBaseName,
                link = "/home/dashboard",
                selected = true,
                badge = 12,
                children = new List<TreeViewJson>
                {
                    GetDatabase(astrdatabaseName)
                }
            };
        }

        private TreeViewJson GetProjectName(string astrProjectName = null, string astrServerName = null,
            string astrdatabaseName = null, List<string> astrdbNamelist = null)
        {
            IstrServerName = astrServerName;
            IstrDatabaseName = astrdatabaseName;
            IstrProjectName = astrProjectName;
            return AddDbInformations(astrProjectName, astrServerName, astrdbNamelist);
        }

        private TreeViewJson AddDbInformations(string astrProjectName, string astrServerName,
            List<string> astrdbNamelist = null)
        {
            TreeViewJson leftTreeJoson = new TreeViewJson
            {
                text = astrProjectName,
                icon = "fa fa-home fa-fw",
                mdaIcon = astrProjectName,
                link = $"/{IstrProjectName}/{IstrProjectName}",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.ProjectInfo,
                children = new List<TreeViewJson> { GetServerName(astrServerName, astrdbNamelist) }
            };

            //var children = new List<TreeViewJson>();
            //astrdbNamelist.ForEach(dbInstance =>
            //{
            //    istrDatabaseName = dbInstance;
            //    children.Add(GetServerName(astrServerName, dbInstance));
            //});
            return leftTreeJoson;
        }

        private TreeViewJson GetServerName(string istrServerName = null, List<string> astrdbNamelist = null)
        {
            TreeViewJson result = new TreeViewJson
            {
                text = istrServerName,
                icon = "fa  fa-desktop fa-fw",
                mdaIcon = istrServerName,
                link = $"/{IstrProjectName}/{istrServerName}",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.DatabaseServer,
                children = new List<TreeViewJson> { DatabaseInform(astrdbNamelist) }
            };

            return result;
        }

        private TreeViewJson DatabaseInform(List<string> astrdbNamelist = null)
        {
            TreeViewJson rest = new TreeViewJson
            {
                text = "User Database",
                icon = "fa fa-folder",
                mdaIcon = "User Database",
                link = $"/{IstrProjectName}/{IstrServerName}/User Database",
                selected = true,
                expand = true,
                badge = 12,
                children = new List<TreeViewJson>()
            };
            astrdbNamelist.ForEach(dbInstance =>
            {
                IstrDatabaseName = dbInstance;
                rest.children.Add(GetDatabase(dbInstance));
            });
            return rest;
        }

        public List<TreeViewJson> GetLeftMenuJson()
        {
            List<TreeViewJson> data = new List<TreeViewJson>
            {
                GetProjectName("Project", Service.GetServerName().FirstOrDefault(),
                    Service.GetDatabaseName().FirstOrDefault(), Service.GetDatabaseName())
            };
            return data;
        }

        #region Tables

        private TreeViewJson GetTables()
        {
            return new TreeViewJson
            {
                text = "Tables",
                icon = "fa fa-folder",
                mdaIcon = "Tables",
                link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Tables",
                selected = true,
                badge = 12,
                expand = true,
                SchemaEnums = SchemaEnums.AllTable,
                children = GetTablesChildren()
            };
        }

        private List<TreeViewJson> GetTablesChildren()
        {
            List<TreeViewJson> tablesList = new List<TreeViewJson>();
            Service.GetTables(IstrDatabaseName).ForEach(tables =>
            {
                tablesList.Add(
                    new TreeViewJson
                    {
                        text = tables,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = tables,
                        expand = false,
                        link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Tables/{tables}",
                        selected = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.Table
                        //  children = GetTableColumns(tables)
                    }
                );
            });
            return tablesList;
        }

        private IList<TreeViewJson> GetTableColumns(string tables)
        {
            List<TreeViewJson> tablesColumns = new List<TreeViewJson>();
            Service.GetTablesColumns(tables.Replace(tables.Substring(0, tables.IndexOf(".")) + ".", ""),
                IstrDatabaseName).ForEach(Column =>
            {
                tablesColumns.Add(
                    new TreeViewJson
                    {
                        text = Column,
                        icon = "fa fa fa-columns",
                        mdaIcon = Column,
                        expand = false,
                        link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Tables/{Column}",
                        selected = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.TableCoumns
                    }
                );
            });
            return tablesColumns;
        }

        #endregion Tables

        #region Views

        private TreeViewJson GetViews()
        {
            return new TreeViewJson
            {
                text = "Views",
                icon = "fa fa-folder",
                mdaIcon = "Views",
                link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Views",
                selected = true,
                badge = 12,
                expand = true,
                SchemaEnums = SchemaEnums.AllViews,
                children = GetViewsChildrens()
            };
        }

        private List<TreeViewJson> GetViewsChildrens()
        {
            List<TreeViewJson> viewsChildens = new List<TreeViewJson>();

            Service.GetViews(IstrDatabaseName).ForEach(view =>
            {
                viewsChildens.Add(
                    new TreeViewJson
                    {
                        text = view,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = view,
                        expand = true,
                        link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Views/{view}",
                        selected = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.Views
                    }
                );
            });
            return viewsChildens;
        }

        #endregion Views

        #region Programmability

        private TreeViewJson GetProgrammability()
        {
            return new TreeViewJson
            {
                text = "Programmability",
                icon = "fa fa-folder",
                mdaIcon = "Programmability",
                link = $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability",
                selected = true,
                badge = 12,
                expand = true,
                SchemaEnums = SchemaEnums.AllProgrammability,
                children = new List<TreeViewJson>
                {
                    GetStoredProcedures(),
                    GetFunction(),
                    GetDatabaseTrigger(),
                    GetDataBaseType()
                }
            };
        }

        private TreeViewJson GetStoredProcedures()
        {
            return new TreeViewJson
            {
                text = "StoredProcedures",
                icon = "fa fa-folder",
                mdaIcon = "StoredProcedures",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/StoredProcedures",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllStoreprocedure,
                children = GetStoredProceduresChildren()
            };
        }

        private List<TreeViewJson> GetStoredProceduresChildren()
        {
            List<TreeViewJson> storeProcedureList = new List<TreeViewJson>();
            Service.GetStoreProcedures(IstrDatabaseName).ForEach(storeProcedure =>
            {
                storeProcedureList.Add(new TreeViewJson
                {
                    text = storeProcedure,
                    icon = "fa fa-table fa-fw",
                    mdaIcon = storeProcedure,
                    link =
                        $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/StoredProcedures/{storeProcedure}",
                    selected = true,
                    badge = 12,
                    SchemaEnums = SchemaEnums.Storeprocedure
                });
            });

            return storeProcedureList;
        }

        private TreeViewJson GetFunction()
        {
            return new TreeViewJson
            {
                text = "Functions",
                icon = "fa fa-folder",
                mdaIcon = "Functions",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions",
                selected = true,
                badge = 12,
                expand = true,
                SchemaEnums = SchemaEnums.AllFunctions,
                children = new List<TreeViewJson>
                {
                    GetTableValuedFunctions(),
                    GetScalarValuedFunctions(),
                    GetAggregateFunctions()
                }
            };
        }

        private TreeViewJson GetTableValuedFunctions()
        {
            return new TreeViewJson
            {
                text = "Table-valued Functions",
                icon = "fa fa-folder",
                mdaIcon = "Table-valued Functions",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions/TableValuedFunctions",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllTableValueFunction,
                children = GetTableValuedFunctionsChildren()
            };
        }

        private List<TreeViewJson> GetTableValuedFunctionsChildren()
        {
            List<TreeViewJson> TableValuedFunctions = new List<TreeViewJson>();

            Service.GetTableValueFunctions(IstrDatabaseName).ForEach(Function =>
            {
                TableValuedFunctions.Add(
                    new TreeViewJson
                    {
                        text = Function,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = Function,
                        link =
                            $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions/TableValuedFunctions/{Function}",
                        selected = true,
                        expand = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.TableValueFunction
                    }
                );
            });
            return TableValuedFunctions;
        }

        private TreeViewJson GetScalarValuedFunctions()
        {
            return new TreeViewJson
            {
                text = "Scalar-valued Functions",
                icon = "fa fa-folder",
                mdaIcon = "Scalar-valued Functions",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions/ScalarValuedFunctions",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllScalarValueFunctions,
                children = GetScalarValuedFunctionsChildren()
            };
        }

        private List<TreeViewJson> GetScalarValuedFunctionsChildren()
        {
            List<TreeViewJson> ScalarValuedFunctions = new List<TreeViewJson>();

            Service.GetScalarFunctions(IstrDatabaseName).ForEach(Function =>
            {
                ScalarValuedFunctions.Add(
                    new TreeViewJson
                    {
                        text = Function,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = Function,
                        link =
                            $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions/ScalarValuedFunctions/{Function}",
                        selected = true,
                        expand = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.ScalarValueFunctions
                    }
                );
            });
            return ScalarValuedFunctions;
        }

        private TreeViewJson GetAggregateFunctions()
        {
            return new TreeViewJson
            {
                text = "Aggregate Functions",
                icon = "fa fa-folder",
                mdaIcon = "Aggregate Functions",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions/AggregateFunctions",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllAggregateFunciton,
                children = GetAggregateFunctionsChildren()
            };
        }

        private List<TreeViewJson> GetAggregateFunctionsChildren()
        {
            List<TreeViewJson> AggregateFunctions = new List<TreeViewJson>();

            Service.GetAggregateFunctions(IstrDatabaseName).ForEach(Function =>
            {
                AggregateFunctions.Add(
                    new TreeViewJson
                    {
                        text = Function,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = Function,
                        link =
                            $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/Functions/AggregateFunctions/{Function}",
                        selected = true,
                        expand = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.AggregateFunciton
                    }
                );
            });
            return AggregateFunctions;
        }

        private TreeViewJson GetDatabaseTrigger()
        {
            return new TreeViewJson
            {
                text = "DatabaseTrigger",
                icon = "fa fa-folder",
                mdaIcon = "DatabaseTrigger",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DatabaseTrigger",
                selected = true,
                expand = true,
                SchemaEnums = SchemaEnums.AllTriggers,
                badge = 12,
                children = GetDatabaseTriggerChildren()
            };
        }

        private List<TreeViewJson> GetDatabaseTriggerChildren()
        {
            List<TreeViewJson> databaseTrigger = new List<TreeViewJson>();
            Service.GetTriggers(IstrDatabaseName).ForEach(trigger =>
            {
                databaseTrigger.Add(
                    new TreeViewJson
                    {
                        text = trigger,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = trigger,
                        link =
                            $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DatabaseTrigger/{trigger}",
                        selected = true,
                        expand = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.Triggers
                    }
                );
            });
            return databaseTrigger;
        }

        private TreeViewJson GetDataBaseType()
        {
            return new TreeViewJson
            {
                text = "Type",
                icon = "fa fa-folder",
                mdaIcon = "Type",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DataBaseType",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllDatabaseDataTypes,
                children = new List<TreeViewJson>
                {
                    GetUserDefinedDataType(),
                    GetXmlSchemaCollection()
                }
            };
        }

        private TreeViewJson GetUserDefinedDataType()
        {
            return new TreeViewJson
            {
                text = "User-Defined Data Types",
                icon = "fa fa-folder",
                mdaIcon = "User Defined Data Types",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DataBaseType/UserDefinedDataType",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllUserDefinedDataType,
                children = GetUserDefinedDataTypeChildren()
            };
        }

        private List<TreeViewJson> GetUserDefinedDataTypeChildren()
        {
            List<TreeViewJson> userDefinedType = new List<TreeViewJson>();
            Service.GetUserDefinedType(IstrDatabaseName).ForEach(userdefinedfunction =>
            {
                userDefinedType.Add(
                    new TreeViewJson
                    {
                        text = userdefinedfunction,
                        icon = "fa fa-table fa-fw",
                        mdaIcon = userdefinedfunction,
                        link =
                            $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DataBaseType/UserDefinedDataType/{userdefinedfunction}",
                        selected = true,
                        expand = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.UserDefinedDataType
                    }
                );
            });
            return userDefinedType;
        }

        private TreeViewJson GetXmlSchemaCollection()
        {
            return new TreeViewJson
            {
                text = "XML Schema Collections",
                icon = "fa fa-folder",
                mdaIcon = "XML Schema Collections",
                link =
                    $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DataBaseType/XmlSchemaCollection",
                selected = true,
                expand = true,
                badge = 12,
                SchemaEnums = SchemaEnums.AllXMLSchemaCollection,
                children = GetXmlSchemaCollectionChildren()
            };
        }

        private List<TreeViewJson> GetXmlSchemaCollectionChildren()
        {
            List<TreeViewJson> definedType = new List<TreeViewJson>();
            Service.GetTriggers(IstrDatabaseName).ForEach(XmlSchema =>
            {
                definedType.Add(
                    new TreeViewJson
                    {
                        text = XmlSchema,
                        //icon = "fa fa-home fa-fw",
                        mdaIcon = XmlSchema,
                        link =
                            $"/{IstrProjectName}/{IstrServerName}/User Database/{IstrDatabaseName}/Programmability/DataBaseType/XmlSchemaCollection/{XmlSchema}",
                        selected = true,
                        expand = true,
                        badge = 12,
                        SchemaEnums = SchemaEnums.XMLSchemaCollection
                    }
                );
            });
            return definedType;
        }

        #endregion Programmability

        #region Storage

        private TreeViewJson GetStorage()
        {
            return new TreeViewJson();
        }

        private TreeViewJson GetFullTextCatalogs()
        {
            return new TreeViewJson();
        }

        #endregion Storage

        #region Security

        private TreeViewJson GetSecurity()
        {
            return new TreeViewJson();
        }

        private TreeViewJson GetSecurityUsers()
        {
            return new TreeViewJson();
        }

        private TreeViewJson GetSecurityRoles()
        {
            return new TreeViewJson();
        }

        private TreeViewJson GetSecuritySchema()
        {
            return new TreeViewJson();
        }

        #endregion Security
    }

    public partial class SrvLeftNavigationTreeView
    {
        public SrvDatabaseObjectDependncy srvDatabaseObjectDependncy = new SrvDatabaseObjectDependncy();

        public SrvLeftNavigationTreeView()
        {
            CreateDirtStructForJsonFiles();
            srvDatabaseObjectDependncy = new SrvDatabaseObjectDependncy();
        }

        public SrvLeftNavigationTreeView(string astrRootPath)
        {
            istrRootPath = astrRootPath;
            CreateDirtStructForJsonFiles(istrRootPath);
        }

        public SrvLeftNavigationTreeView(bool iblnFlushTheCache)
        {
            if (iblnFlushTheCache)
            {
            }
        }

        public string istrRootPath { get; set; }

        public List<string> GetDatabaseName()
        {
            return GetDatabaseNameList();
        }

        public static List<string> GetDatabaseNameList()
        {
            List<string> lst = new List<string>();
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                lst = dbSqldocContext.GetDatabaseNames();
            }

            return lst;
        }

        public List<string> GetServerName()
        {
            return GetServerNameList();
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

        public List<string> GetTables(string dbInstanceName = null)
        {
            return GetTableList(dbInstanceName);
        }

        public static List<string> GetTableList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetTables().Where(x => x != null).ToList();
            }
        }

        public List<string> GetTablesColumns(string astrTableName, string dbInstanceName = null)
        {
            return GetTablesColumnsList(astrTableName, dbInstanceName);
        }

        public static List<string> GetTablesColumnsList(string astrTableName, string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetTableColumns(astrTableName);
            }
        }

        //GetTableColumns
        public static List<string> GetTableColumns(string astrColumnName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                return dbSqldocContext.GetTableColumns(astrColumnName);
            }
        }

        public List<string> GetViews(string dbInstanceName = null)
        {
            return GetViewsList(dbInstanceName);
        }

        public static List<string> GetViewsList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetViews().Where(x => x != null).ToList();
            }
        }

        public List<string> GetStoreProcedures(string dbInstanceName = null)
        {
            return GetStoreProceduresList(dbInstanceName);
        }

        public static List<string> GetStoreProceduresList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetStoreProcedures().Where(x => x != null).ToList();
            }
        }

        public List<string> GetScalarFunctions(string dbInstanceName = null)
        {
            return GetScalarFunctionsList(dbInstanceName);
        }

        public static List<string> GetScalarFunctionsList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetScalarFunction().Where(x => x != null).ToList();
                ;
            }
        }

        public List<string> GetTableValueFunctions(string dbInstanceName = null)
        {
            return GetTableValueFunctionsList(dbInstanceName);
        }

        public static List<string> GetTableValueFunctionsList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetTableValueFunction().Where(x => x != null).ToList();
            }
        }

        public List<string> GetAggregateFunctions(string dbInstanceName = null)
        {
            return GetAggregateFunctionsList(dbInstanceName);
        }

        public static List<string> GetAggregateFunctionsList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetAggregateFunctions().Where(x => x != null).ToList();
            }
        }

        public List<string> GetTriggers(string dbInstanceName = null)
        {
            return GetTriggersList(dbInstanceName);
        }

        public static List<string> GetTriggersList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetTigger().Where(x => x != null).ToList();
            }
        }

        public List<string> GetUserDefinedTypes(string dbInstanceName = null)
        {
            return GetUserDefinedTypesList(dbInstanceName);
        }

        public static List<string> GetUserDefinedTypesList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(dbInstanceName))
            {
                return dbSqldocContext.GetUserDefinedDataType().Where(x => x != null).ToList();
            }
        }

        public List<string> GetXmlSchemaCollection(string dbInstanceName = null)
        {
            return GetXmlSchemaCollectionList(dbInstanceName);
        }

        public static List<string> GetXmlSchemaCollectionList(string dbInstanceName = null)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                return dbSqldocContext.GetXmlSchemaCollection().Where(x => x != null).ToList();
            }
        }

        public List<string> GetFulllTextLogs()
        {
            return new List<string>();
        }

        public List<string> GetDatabaseRoles()
        {
            return new List<string>();
        }

        public List<string> GetSchemas()
        {
            return new List<string>();
        }

        #region Directory stucture handler

        public void CreateDirtStructForJsonFiles(string astrRootPath = null)
        {
            CreateTreeViewJsonFolder(istrRootPath + "\\data\\componentdata\\");
            CreateServerNameFolders(istrRootPath + "\\" + GetServerName().FirstOrDefault());
        }

        public void CreateTreeViewJsonFolder(string istrlocation)
        {
            istrlocation.IfDirectoryNotExistThenCreate();
        }

        public void CreateServerNameFolders(string istrlocation)
        {
            CreateUserDatabasaeFolder(istrlocation.IfDirectoryNotExistThenCreate());
        }

        public void CreateUserDatabasaeFolder(string istrlocation)
        {
            CreateDatabasaeFolder((istrlocation + "\\" + busConstants.UserDatabase).IfDirectoryNotExistThenCreate());
        }

        public void CreateDatabasaeFolder(string istrlocation)
        {
            GetDatabaseName().ForEach(dbInstance =>
            {
                string istrUserDatabaseFolder = (istrlocation + "\\" + dbInstance).IfDirectoryNotExistThenCreate();
                CreateDatabasaeTablesFolder(istrUserDatabaseFolder, dbInstance);
                CreateDatabasaeViewsFolder(istrUserDatabaseFolder, dbInstance);
                CreateDatabasaeProgrammabilityFolder(istrUserDatabaseFolder, dbInstance);
            });
        }

        public void CreateDatabasaeProgrammabilityFolder(string istrUserDatabaseFolder, string dbinstance = null)
        {
            string istrUserDatabaseProgrammabilityFolder = (istrUserDatabaseFolder + "\\" + busConstants.Programmability)
                .IfDirectoryNotExistThenCreate();
            CreateDatabaseStoredProceduresFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
            CreateDatabaseFunctionsFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
            CreateDatabaseDatabaseTriggersFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
            CreateDatabaseTypeFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
        }

        public void CreateDatabaseTypeFolder(string istrUserDatabaseProgrammabilityFolder, string dbinstance = null)
        {
            istrUserDatabaseProgrammabilityFolder = (istrUserDatabaseProgrammabilityFolder + "\\" + busConstants.Type)
                .IfDirectoryNotExistThenCreate();
            CreateDatabaseUserDefinedDataTypesFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
            CreateDatabaseXmlSchemaCollectionsFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
        }

        public void CreateDatabaseXmlSchemaCollectionsFolder(string istrUserDatabaseProgrammabilityFolder,
            string dbinstance = null)
        {
            istrUserDatabaseProgrammabilityFolder =
                (istrUserDatabaseProgrammabilityFolder + "\\" + busConstants.XmlSchemaCollection)
                .IfDirectoryNotExistThenCreate();
            CreateDatabaseXmlSchemaCollectionFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
        }

        public void CreateDatabaseXmlSchemaCollectionFolder(string istrUserDatabaseProgrammabilityFolder,
            string dbinstance = null)
        {
            GetXmlSchemaCollection(dbinstance).ForEach(x =>
            {
                (istrUserDatabaseProgrammabilityFolder + "\\" + x).IfDirectoryNotExistThenCreate();
            });
        }

        public void CreateDatabaseUserDefinedDataTypesFolder(string istrUserDatabaseProgrammabilityFolder,
            string dbinstance = null)
        {
            istrUserDatabaseProgrammabilityFolder =
                (istrUserDatabaseProgrammabilityFolder + "\\" + busConstants.DatabaseUserDefinedDataType)
                .IfDirectoryNotExistThenCreate();
            CreateDatabaseUserDefinedDataTypeFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
        }

        public void CreateDatabaseUserDefinedDataTypeFolder(string istrUserDatabaseProgrammabilityFolder,
            string dbinstance = null)
        {
            GetUserDefinedTypes(dbinstance).ForEach(x =>
            {
                (istrUserDatabaseProgrammabilityFolder + "\\" + x).IfDirectoryNotExistThenCreate();
            });
        }

        public void CreateDatabaseDatabaseTriggersFolder(string istrUserDatabaseProgrammabilityTriggersFolder,
            string dbinstance = null)
        {
            (istrUserDatabaseProgrammabilityTriggersFolder += "\\" + busConstants.Trigger)
                .IfDirectoryNotExistThenCreate();
            CreateDatabaseDatabaseTriggerFolder(istrUserDatabaseProgrammabilityTriggersFolder, dbinstance);
        }

        public void CreateDatabaseDatabaseTriggerFolder(string istrUserDatabaseProgrammabilityTriggerFolder,
            string dbinstance = null)
        {
            GetTriggers(dbinstance).ForEach(x =>
            {
                (istrUserDatabaseProgrammabilityTriggerFolder + "\\" + x).IfDirectoryNotExistThenCreate();
            });
        }

        public void CreateDatabaseFunctionsFolder(string istrUserDatabaseProgrammabilityFolder,
            string dbinstance = null)
        {
            CreateDatabaseTableValueFunctionsFolder(
                istrUserDatabaseProgrammabilityFolder + "\\" + busConstants.TableValueFunctions, dbinstance);
            CreateDatabaseScalarValueFunctionsFolder(
                istrUserDatabaseProgrammabilityFolder + "\\" + busConstants.ScalarValueFunctions, dbinstance);
            CreateDatabaseaggregatedFunctionsFolder(
                istrUserDatabaseProgrammabilityFolder + "\\" + busConstants.AggregatedFunctions, dbinstance);
        }

        public void CreateDatabaseaggregatedFunctionsFolder(
            string istrUserDatabaseProgrammabilityTableValueFunctionFolder, string dbinstance = null)
        {
            CreateDatabaseaggregatedFunctionfolder(
                istrUserDatabaseProgrammabilityTableValueFunctionFolder.IfDirectoryNotExistThenCreate(), dbinstance);
        }

        public void CreateDatabaseaggregatedFunctionfolder(
            string istrUserDatabaseProgrammabilityTableValueFunctionFolder, string dbinstance = null)
        {
            GetAggregateFunctions(dbinstance).ForEach(x =>
            {
                (istrUserDatabaseProgrammabilityTableValueFunctionFolder + "\\" + x)
                    .IfDirectoryNotExistThenCreate();
            });
        }

        public void CreateDatabaseScalarValueFunctionsFolder(string istrUserDatabaseProgrammabilityScalarFunctionFolder,
            string dbinstance = null)
        {
            CreateDatabaseScalarValueFunctionFolders(
                istrUserDatabaseProgrammabilityScalarFunctionFolder.IfDirectoryNotExistThenCreate(), dbinstance);
        }

        public void CreateDatabaseScalarValueFunctionFolders(string istrUserDatabaseProgrammabilityScalarFunctionFolder,
            string dbinstance = null)
        {
            GetScalarFunctions(dbinstance).ForEach(x =>
            {
                DependancyTreeCreator(
                    (istrUserDatabaseProgrammabilityScalarFunctionFolder + "\\" + x)
                    .IfDirectoryNotExistThenCreate(), dbinstance, x);
            });
        }

        public void CreateDatabaseTableValueFunctionsFolder(
            string istrUserDatabaseProgrammabilityTableValueFunctionFolder, string dbinstance = null)
        {
            CreateDatabaseTableValueFunctionFolder(
                istrUserDatabaseProgrammabilityTableValueFunctionFolder.IfDirectoryNotExistThenCreate(), dbinstance);
        }

        public void CreateDatabaseTableValueFunctionFolder(
            string istrUserDatabaseProgrammabilityTableValueFunctionFolder, string dbinstance = null)
        {
            GetTableValueFunctions(dbinstance).ForEach(x =>
            {
                DependancyTreeCreator(
                    (istrUserDatabaseProgrammabilityTableValueFunctionFolder + "\\" + x)
                    .IfDirectoryNotExistThenCreate(), dbinstance, x);
            });
        }

        public void CreateDatabaseStoredProceduresFolder(string istrUserDatabaseProgrammabilityFolder,
            string dbinstance = null)
        {
            (istrUserDatabaseProgrammabilityFolder += "\\" + busConstants.StoredProcedures)
                .IfDirectoryNotExistThenCreate();
            CreateDatabaseStoredProcedureFolder(istrUserDatabaseProgrammabilityFolder, dbinstance);
        }

        public void CreateDatabaseStoredProcedureFolder(string istrUserDatabaseStoredProceduresFolder,
            string dbinstance = null)
        {
            GetStoreProcedures(dbinstance).ForEach(x =>
            {
                DependancyTreeCreator(istrUserDatabaseStoredProceduresFolder, dbinstance, x);
            });
        }

        public void CreateDatabasaeViewsFolder(string istrUserDatabaseFolder, string dbinstance = null)
        {
            (istrUserDatabaseFolder += "\\" + busConstants.Views).IfDirectoryNotExistThenCreate();
            GetViews(dbinstance).ForEach(x =>
            {
                DependancyTreeCreator((istrUserDatabaseFolder + "\\" + x).IfDirectoryNotExistThenCreate(),
                    dbinstance, x);
            });
        }

        public void CreateDatabasaeTablesFolder(string istrlocation, string dbinstance = null)
        {
            CreateDatabasaeTableFolder((istrlocation + "\\" + busConstants.Tables).IfDirectoryNotExistThenCreate(),
                dbinstance);
        }

        public void CreateDatabasaeTableFolder(string istrlocation, string dbinstance = null)
        {
            string istrUserDatabaseTableFolder = istrlocation + "\\Tables";
            GetTables(dbinstance).ForEach(x => { DependancyTreeCreator(istrlocation, dbinstance, x); });
        }

        private void DependancyTreeCreator(string istrlocation, string dbinstance, string x)
        {
            //var PathName = (istrlocation + "\\" + x).IfDirectoryNotExistThenCreate();
            //var FileNameThatDependsOn = PathName.Split('\\').LastOrDefault() + "ThatDependsOn" + ".json";
            //var fullFilenameThatDependsOn = PathName + "\\" + FileNameThatDependsOn;

            //var FileNameOnWhichDepends = PathName.Split('\\').LastOrDefault() + "OnWhichDepends" + ".json";
            //var fullFilenameOnWhichDepends = PathName + "\\" + FileNameOnWhichDepends;

            //var JsonDataThatDependsOn = srvDatabaseObjectDependncy.GetObjectThatDependsOn(dbinstance, x);
            //var JsonDataOnWhichDepends = srvDatabaseObjectDependncy.GetObjectOnWhichDepends(dbinstance, x);
            //if (JsonDataThatDependsOn.Length>=265 || JsonDataOnWhichDepends.Length>=256)
            //{
            //    return;
            //}

            //try
            //{
            //    System.IO.File.WriteAllText
            //                       (
            //                          fullFilenameThatDependsOn,
            //                          JsonDataThatDependsOn
            //                       );

            //    System.IO.File.WriteAllText
            //       (
            //          fullFilenameOnWhichDepends,
            //          JsonDataOnWhichDepends
            //       );

            //}
            //catch (Exception)
            //{
            //}
        }

        internal List<string> GetUserDefinedType(string istrDatabaseName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrDatabaseName))
            {
                return dbSqldocContext.GetUserDefinedDataType().Where(x => x != null).ToList();
            }
        }

        #endregion Directory stucture handler
    }
}