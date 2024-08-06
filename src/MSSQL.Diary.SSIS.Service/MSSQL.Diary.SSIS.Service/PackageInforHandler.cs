using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;
using Microsoft.SqlServer.Dts.Tasks.ScriptTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.Diary.SSIS.Service
{
    public class PackageInforHandler
    {

        public PackageInforHandler()
        {

            SSISPackage = new Application();
            SsisPackage = new Package();
            IdtListOfChildPackage = new Dictionary<string, string>();
            IdtListOfConnections = new Dictionary<string, string>();
            IdtListOfExecuteTasks = new Dictionary<string, string>();
            IdtListOfForloop = new Dictionary<string, string>();
            IdtListOfVariables = new Dictionary<string, object>();
            LoadListofEmailhandler = new List<EmailHandler>();
            LoadListofFileTask = new List<FileSystemTask>();
            LoadListofScriptTask = new List<ScriptTask>();
            LoadListofFtphandler = new List<FTPHandler>();
            lstExecuteSQLTask = new List<ExecuteSQLTask>();
            LoadExecuteTaskDetails = new Dictionary<string, string>();



        }
        public Dictionary<string, string> IdtListOfChildPackage { get; set; }
        public Dictionary<string, string> IdtListOfConnections { get; set; }
        public Dictionary<string, string> IdtListOfExecuteTasks { get; set; }
        public Dictionary<string, string> IdtListOfForloop { get; set; }
        public Dictionary<string, object> IdtListOfVariables { get; set; }
        public string IstPackagename { get; set; }
        public List<EmailHandler> LoadListofEmailhandler { get; set; }
        public List<FileSystemTask> LoadListofFileTask { get; set; }
        public List<FTPHandler> LoadListofFtphandler { get; set; }
        public List<ScriptTask> LoadListofScriptTask { get; set; }
        public List<ExecuteSQLTask> lstExecuteSQLTask { get; set; }
        public Dictionary<string, string> LoadExecuteTaskDetails { get; set; }
        public Package SsisPackage { get; set; }
        public Application SSISPackage { get; set; }
        public Package LoadPackageDetailsByName(string istrPackageName)
        {
            SsisPackage = SSISPackage.LoadPackage(istrPackageName, null);
            return SsisPackage;
        }
        public Dictionary<string, string> LoadConnectionString()
        {
            var result = SsisPackage.Connections.OfType<ConnectionManager>().Where(x => !x.Name.Contains(".dtsx"))
                 .ToDictionary(
                 x => x.Name,
                 x => x.ConnectionString,
                 StringComparer.CurrentCultureIgnoreCase);
            return result;
        }
        public Dictionary<string, object> LoadVariablesData()
        {
            var result = SsisPackage.Variables.OfType<Variable>().ToList()
               .ToDictionary(
               x => x.Name,
               x => x.Value,
               StringComparer.CurrentCultureIgnoreCase);
            return result;

        }
        public Dictionary<string, string> LoadChildPackages()
        {
            var result = SsisPackage.Connections.OfType<ConnectionManager>()
                .Where(x => x.Name.Contains(".dtsx")) // To load only file which are contain dtsx files
                 .ToDictionary(
                 x => x.Name,
                 x => x.ConnectionString,
                 StringComparer.CurrentCultureIgnoreCase);
            return result;

        }

        public void PackageExecutables()
        {
            foreach (object ssisTask in SsisPackage.Executables)
            {
                try
                {
                    switch (ssisTask.GetType().Name)
                    {
                        case "TaskHost":
                            {

                                TaksHostObjTypeDtls(ssisTask);
                            }
                            break;
                        case "ForEachLoop":
                            {
                                foreach (object Subtask in ((ForEachLoop)(ssisTask)).Executables)
                                {
                                    TaksHostObjTypeDtls(Subtask);
                                }
                            }
                            break;
                    }
                }
                catch (Exception )
                {
                    //  Console.WriteLine("Exception : " + ex.Message);
                }
            }
        }
        private void TaksHostObjTypeDtls(object Subtask)
        {
            var PackageTask = ((dynamic)Subtask).InnerObject;
            Console.WriteLine(PackageTask.GetType().Name);
            switch (PackageTask.GetType().Name)
            {
                case "ExecuteSQLTask":
                    {
                        lstExecuteSQLTask.Add(PackageTask);
                    }
                    break;
                case "ScriptTask":
                    {
                        LoadListofScriptTask.Add(PackageTask);
                    }
                    break;
                case "ExecuteProcess":
                    {
                        LoadExecuteTaskDetails.Add(((dynamic)Subtask).InnerObject.Name, PackageTask.Arguments);
                    }
                    break;
                case "FtpTask":
                    {
                        var FtpHadler = new FTPHandler
                        {
                            Connection = (dynamic)PackageTask.Connection,
                            LocalPath = (dynamic)PackageTask.LocalPath,
                            Operation = (dynamic)PackageTask.Operation.ToString(),
                            RemotePath = (dynamic)PackageTask.RemotePath
                        };
                        LoadListofFtphandler.Add(FtpHadler);
                    }
                    break;
                case "SendMailTask":
                    {
                    }
                    break;
                case "FileSystemTask":
                    {
                        LoadListofFileTask.Add(PackageTask);
                    }
                    break;
                case "__ComObject":
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
    public class FTPHandler
    {
        public string Connection { get; set; }
        public string LocalPath { get; set; }
        public string Operation { get; set; }
        public string RemotePath { get; set; }
    }
    public class EmailHandler
    {
        public string CCline { get; set; }
        public string FormLine { get; set; }
        public string Subject { get; set; }
        public string ToLine { get; set; }

    }
}
