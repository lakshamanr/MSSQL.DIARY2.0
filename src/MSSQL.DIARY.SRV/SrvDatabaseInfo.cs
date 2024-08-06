using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseInfo
    {
        //private static NaiveCache<List<string>> _avatarCache = new NaiveCache<List<string>>();
        //private static NaiveCache<List<PropertyInfo>> _avatarCache2 = new NaiveCache<List<PropertyInfo>>();
        //private static NaiveCache<List<FileInfomration>> _avatarCache3 = new NaiveCache<List<FileInfomration>>();

        public string GetDatabaseUserDefinedText()
        {
            return "";
        }

        public List<string> GetDatabaseObjectTypes()
        {
            return new List<string>
            {
                "Tables",
                "Views",
                "Stored Procedures",
                "Table-valued Functions",
                "Scalar-valued Functions",
                "Database Triggers",
                "User-Defined Data Types",
                "XML Schema Collections",
                "Full Text Catalogs",
                "Users",
                "Database Roles",
                "Schemas",
                "WorkFlow",
                "ERdiagram",
                "Modules"
            };
        }

        public List<PropertyInfo> GetdbPropertValues(string istrdbName)
        {
            return GetdbProperties(istrdbName);
        }

        private static List<PropertyInfo> GetdbProperties(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetdbProperties();
            }
        }

        public List<PropertyInfo> GetdbOptionValues(string istrdbName)
        {
            return GetdbOptions(istrdbName);
        }

        private static List<PropertyInfo> GetdbOptions(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetdbOptions();
            }
        }

        public List<FileInfomration> GetdbFilesDetails(string istrdbName)
        {
            return GetdbFilesDetail(istrdbName);
        }

        public string GetERDiagram(string istrPath, string istrdbName, string istrServerName, string istrSchemaName)
        {
            string result = File.ReadAllText(GenGraphHtmlString(istrPath + "\\" + istrdbName + ".svg", istrdbName, istrSchemaName))
                .Replace("<svg", "<svg id='svgDatabaseDiagram' \t");
            //.Replace("</svg>", "<image xlink:href='https://svgshare.com/i/9Eo.svg' width='1280px' height='560px' ></image></svg>");
            //result = result.Replace("width=", "width=1280px").Replace("height=", " height=600px");
            return result;
        }

        public string GetERDiagram(string istrPath, string istrdbName, string istrServerName, string istrSchemaName, List<string> alstOfSelectedTables)
        {
            string result = File.ReadAllText(GenGraphHtmlString(istrPath + "\\" + istrdbName + ".svg", istrdbName, istrSchemaName, alstOfSelectedTables))
                .Replace("<svg", "<svg id='svgDatabaseDiagram' \t");
            return result;
        }

        //
        private string GenGraphHtmlString(string istrPathToStoreSVG, string istrdbName, string istrSchemaName)
        {
            try
            {
                istrdbName = istrdbName.Split('/')[0];
            }
            catch (System.Exception)
            {
            }
            File.WriteAllBytes(istrPathToStoreSVG,
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "svg", istrSchemaName).ToArray());
            File.WriteAllBytes(istrPathToStoreSVG.Replace(".svg", ".pdf"),
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "pdf", istrSchemaName).ToArray());
            File.WriteAllBytes(istrPathToStoreSVG.Replace(".svg", ".png"),
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "png", istrSchemaName).ToArray());
            File.WriteAllBytes(istrPathToStoreSVG.Replace(".svg", ".jpg"),
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "jpg", istrSchemaName).ToArray());

            return istrPathToStoreSVG;
        }

        private string GenGraphHtmlString(string istrPathToStoreSVG, string istrdbName, string istrSchemaName, List<string> alstOfSelectedTables)
        {
            try
            {
                istrdbName = istrdbName.Split('/')[0];
            }
            catch (System.Exception)
            {
            }
            File.WriteAllBytes(istrPathToStoreSVG,
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "svg", istrSchemaName, alstOfSelectedTables).ToArray());
            File.WriteAllBytes(istrPathToStoreSVG.Replace(".svg", ".pdf"),
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "pdf", istrSchemaName, alstOfSelectedTables).ToArray());
            File.WriteAllBytes(istrPathToStoreSVG.Replace(".svg", ".png"),
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "png", istrSchemaName, alstOfSelectedTables).ToArray());
            File.WriteAllBytes(istrPathToStoreSVG.Replace(".svg", ".jpg"),
                new srvDatabaseERDiagram().GetGraphHtmlString(istrdbName, "jpg", istrSchemaName, alstOfSelectedTables).ToArray());

            return istrPathToStoreSVG;
        }

        private static List<FileInfomration> GetdbFilesDetail(string istrdbName)
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext(istrdbName))
            {
                return dbSqldocContext.GetdbFiles();
            }
        }
    }
}