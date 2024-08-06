using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSQL.DIARY.UI.Models
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

    public class ExecuteSQLTaskHandler
    {
        public bool IsStoredProcedure { get; set; }
        public string ConnectionName { get; set; }
        public string SqlStatementSource { get; set; }
    }
    public class ScriptTaskHandler
    {

    }
    public class VariableHandler
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
    public class ConnectionHandler
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
    public class ftpHandler
    {
        public string Connection { get; set; }
        public string LocalPath { get; set; }
        public string Operation { get; set; }
    }
    public class emailHandler
    {
        public string CCline { get; set; }
        public string FormLine { get; set; }
        public string Subject { get; set; }
        public string ToLine { get; set; }

    }
    public class ChildPackageHandler
    {
        public string Name { get; set; }
        public string ChildPackageLocation { get; set; }
    }
    public class FileSystemTaskHandler
    {
        public string TaskOperationType { get; set; }
        public string OperationName { get; set; }
        public string TaskOverwriteDestFile { get; set; }
        public string TaskSourcePath { get; set; }
        public string TaskIsSourceVariable { get; set; }
        public string TaskDestinationPath { get; set; }
        public string TaskIsDestinationVariable { get; set; }
        public string TaskFileAttributes { get; set; }
        public string TaskPreservedAttributes { get; set; }

    }
    public class ScripTaskHandler
    {
        public string ReadWriteVariables { get; set; }
        public string ReadOnlyVariables { get; set; }
        public string DefaultActiveItem { get; }
        public string ProjectTemplatePath { get; }
        public string ScriptLanguage { get; set; }
        public string EntryPoint { get; set; }
        public string ScriptProjectName { get; set; }
        public bool ScriptLoaded { get; }
        public bool DebugMode { get; set; }
        public bool SuspendRequired { get; set; }
    }
}
