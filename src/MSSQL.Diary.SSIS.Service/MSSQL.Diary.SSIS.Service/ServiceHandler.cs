using MSSQL.Diary.SSIS.Service.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.Diary.SSIS.Service
{
  public class ServiceHandler
    {
        public void Start()
        {
            Settings settings = new Settings(); 
            //foreach (string line in File.ReadAllLines(settings.SSISPackageSourceLocation, Encoding.UTF8))
            //{
                LoadSSISPackageFiles(settings.SSISPackageSourceLocation).ForEach(x =>
                {
                    PackageInforHandler packageHandler = new PackageInforHandler();
                    packageHandler.LoadPackageDetailsByName(x.FullName);
                    packageHandler.PackageExecutables();
                    GenerateJsonFile(x, packageHandler, settings.SSISPackageSourceLocation+"\\json");
                });
           // }
          
        }
        private static void GenerateJsonFile(FileInfo x, PackageInforHandler packageHandler,string JsonPath)
        {
            PackageJsonHandler packageJson = new PackageJsonHandler
            {
                PackageLocation = x.Name
            };
            packageHandler.LoadConnectionString().ToList()
            .ForEach(x1 =>
            {
                packageJson.Connections.Add(new ConnectionHandler() { Name = x1.Key, ConnectionString = x1.Value });
            });
            packageHandler.LoadVariablesData().ToList().ForEach(x2 =>
            {
                packageJson.Variable.Add(new VariableHandler()
                {
                    Name = x2.Key,
                    Value = x2.Value
                });
            });
            packageHandler.lstExecuteSQLTask.ForEach(x2 =>
            {
                packageJson.ExecuteSQLTask.Add(new ExecuteSQLTaskHandler()
                { 
                    ConnectionName = x2.Connection,
                    IsStoredProcedure = x2.IsStoredProcedure,
                    SqlStatementSource = x2.SqlStatementSource 
                });
            });
            packageHandler.LoadChildPackages().ToList().ForEach(x1 =>
            {
                packageJson.ChildPackages.Add(new ChildPackageHandler()
                {
                    Name = x1.Key,
                    ChildPackageLocation = x1.Value
                }
                    );
            });
            packageHandler.LoadListofScriptTask.ForEach(x1 =>
            {
                packageJson.ScripTasks.Add(
                    new ScripTaskHandler
                    {
                        DebugMode = x1.DebugMode,
                        EntryPoint = x1.EntryPoint,
                        ReadOnlyVariables = x1.ReadOnlyVariables,
                        ReadWriteVariables = x1.ReadWriteVariables,
                        ScriptLanguage = x1.ScriptLanguage,
                        ScriptProjectName = x1.ScriptProjectName,
                        SuspendRequired = x1.SuspendRequired

                    }
                    );
            }
            );
            string output = JsonConvert.SerializeObject(packageJson);
            File.WriteAllText(JsonPath + x + ".json", output);
        }

        public static List<FileInfo> LoadSSISPackageFiles(string istrFolderPath = null)
        {
            string[] lstPackgeExtension = { "*.dtsx" };
            List<FileInfo> PackageFileDetails = new List<FileInfo>();
            DirectoryInfo CurrentFileDirectory = new DirectoryInfo(istrFolderPath);
            foreach (string ext in lstPackgeExtension)
            {
                FileInfo[] folder = CurrentFileDirectory.GetFiles(ext, SearchOption.AllDirectories);
                foreach (FileInfo file in folder)
                {
                    if ((file.Attributes & FileAttributes.Directory) != 0)
                    {
                        continue;
                    }

                    PackageFileDetails.Add(file);
                }
            }
            return PackageFileDetails;
        }
    }
}
