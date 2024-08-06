using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSSSQL.DIARY.SERVICE.Model;

namespace MSSSQL.DIARY.SERVICE
{
    public class PackageJsonHandler
    {
        public PackageJsonHandler()
        {
            ExecuteSQLTask = new List<ExecuteSQLTaskHandler>();
            Variable = new List<VariableHandler>();
            ftpTasks = new List<ftpHandler>();
            Connections = new List<ConnectionHandler>();
            EmailsTasks = new List<emailHandler>();
            FileSystemTask = new List<FileSystemTaskHandler>();
            ChildPackages = new List<ChildPackageHandler>();
            ScripTasks = new List<ScripTaskHandler>();
        }
        public string PackageLocation { get; set; }
        public List<ExecuteSQLTaskHandler> ExecuteSQLTask { get; set; }
        public List<VariableHandler> Variable { get; set; }
        public List<ftpHandler> ftpTasks { get; set; }
        public List<ConnectionHandler> Connections { get; set; }
        public List<emailHandler> EmailsTasks { get; set; }
        public List<FileSystemTaskHandler> FileSystemTask { get; set; }
        public List<ChildPackageHandler> ChildPackages { get; set; }
        public List<ScripTaskHandler> ScripTasks { get; }
    }
}
