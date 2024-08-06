using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.DIARY.SSIS
{
    public class PackageHandler
    {
        public PackageHandler()
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
            LoadListofScriptTask = new List<object>();
            LoadListofFtphandler = new List<FTPHandler>();
            LoadListofStroreProc = new List<ExecuteSQLTask>();
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
        public List<object> LoadListofScriptTask { get; set; }
        public List<ExecuteSQLTask> LoadListofStroreProc { get; set; }
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
            var Connections = new Dictionary<string, string>();
            SsisPackage.Connections.OfType<ConnectionManager>().ToList().ForEach(x =>
            {
                
                if (!Connections.ContainsKey(x.Name))
                {
                    Connections.Add(x.Name, x.ConnectionString);

                }
            }); 
            return Connections;
        }
        public Dictionary<string, object> LoadVariablesData()
        { 
            var Variables = new Dictionary<string, object>();
            SsisPackage.Connections.OfType<Variable>().ToList().ForEach(x =>
            {

                if (!Variables.ContainsKey(x.Name))
                {
                    Variables.Add(x.Name, x.Value);

                }
            }); 
           
            return Variables;
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
                        case "Sequence":
                            {
                                
                            }
                            break;
                        case "RuntimeType":
                            {
                                
                            }
                            break;
                    }
                }
                catch (Exception )
                {
       
                }
            }
        }
        private void TaksHostObjTypeDtls(object Subtask)
        {
            try
            {
                var PackageTask = ((dynamic)Subtask).InnerObject;

                switch (PackageTask.GetType().Name)
                {
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
                    case "ExecuteSQLTask":
                        {
                            //   LoadListofScriptTask.Add(PackageTask);
                        }
                        break;

                    //

                    default:
                        break;
                }
            }
            catch (Exception )
            {
                  
            }

        }
    }
}
