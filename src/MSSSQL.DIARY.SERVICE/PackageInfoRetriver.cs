using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;
using Microsoft.SqlServer.Dts.Tasks.ScriptTask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MSSSQL.DIARY.SERVICE.Model;
using Newtonsoft.Json;

namespace MSSSQL.DIARY.SERVICE
{
    public class PackageInfoRetriver
	{

		public PackageInfoRetriver()
		{ 
			SSISApplication = new Application();
			SSSISPackage = new Package();
			ChildPackages = new Dictionary<string, string>();
			Connections = new Dictionary<string, string>();  
			Variables = new Dictionary<string, object>();
			EmailTasks = new List<EmailHandler>();
			FileSystemTaks = new List<FileSystemTask>();
			ScriptTasks = new List<ScriptTask>();
			FTPTasks = new List<FTPHandler>();
			ExecuteSQLTasks = new List<ExecuteSQLTask>();
			ExecuteTasks = new Dictionary<string, string>(); 
		}

		public static List<FileInfo> LoadPackageFiles(string istrFolderPath)
		{
			string[] lstPackgeExtension = { "*.dtsx" };
			List<FileInfo> PackageFileDetails = new List<FileInfo>();
			DirectoryInfo FileDirectory = new DirectoryInfo(istrFolderPath);
			foreach (string ext in lstPackgeExtension)
			{
				FileInfo[] folder = FileDirectory.GetFiles(ext, SearchOption.AllDirectories);
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
		public void deleteTheFile(FileInfo x)
		{
			x.Delete();
		}



		public void GenerateJsonFile(FileInfo x, PackageInfoRetriver packageHandler)
		{

			PackageJsonHandler packageJson = new PackageJsonHandler
			{
				PackageLocation = x.Name
			};

			packageHandler.LoadConnections().ToList()
			.ForEach(x1 =>
			{
				packageJson.Connections.Add(new ConnectionHandler() { Name = x1.Key, ConnectionString = x1.Value });
			});

			packageHandler.LoadVariables().ToList()
			.ForEach(x2 =>
			{
				packageJson.Variable.Add(new VariableHandler()
				{
					Name = x2.Key,
					Value = x2.Value
				});
			});

			packageHandler.ExecuteSQLTasks
			.ForEach(ConnectionInfo =>
			{
				packageJson.ExecuteSQLTask.Add(new ExecuteSQLTaskHandler()
				{
					ConnectionName = ConnectionInfo.Connection,
					IsStoredProcedure = ConnectionInfo.IsStoredProcedure,
					SqlStatementSource = ConnectionInfo.SqlStatementSource
				});
			});

			packageHandler.LoadChildPackages().ToList()
			.ForEach(x1 =>
			{
				packageJson.ChildPackages.Add(new ChildPackageHandler()
				{
					Name = x1.Key,
					ChildPackageLocation = x1.Value
				}
					);
			});


			packageHandler.ScriptTasks.ForEach(scriptTaskInfo =>
			{
				packageJson.ScripTasks.Add(
					new ScripTaskHandler
					{
						DebugMode = scriptTaskInfo.DebugMode,
						EntryPoint = scriptTaskInfo.EntryPoint,
						ReadOnlyVariables = scriptTaskInfo.ReadOnlyVariables,
						ReadWriteVariables = scriptTaskInfo.ReadWriteVariables,
						ScriptLanguage = scriptTaskInfo.ScriptLanguage,
						ScriptProjectName = scriptTaskInfo.ScriptProjectName,
						SuspendRequired = scriptTaskInfo.SuspendRequired

					}
					);
			}
			);

			var JsonPath = x.Directory.FullName.Replace("Packages", "JSON");
			if (!Directory.Exists(JsonPath))
			{
				Directory.CreateDirectory(JsonPath);
			}
			File.WriteAllText(JsonPath + "\\" + x + ".json", JsonConvert.SerializeObject(packageJson));
		}

		public Dictionary<string, string> ChildPackages { get; set; }
		public Dictionary<string, string> Connections { get; set; } 
		public Dictionary<string, string> ForloopTasks { get; set; }
		public Dictionary<string, object> Variables { get; set; }
		public string PackageName { get; set; }
		public List<EmailHandler> EmailTasks { get; set; }
		public List<FileSystemTask> FileSystemTaks { get; set; }
		public List<FTPHandler> FTPTasks { get; set; }
		public List<ScriptTask> ScriptTasks { get; set; }
		public List<ExecuteSQLTask> ExecuteSQLTasks { get; set; }
		public Dictionary<string, string> ExecuteTasks { get; set; }
		public Package SSSISPackage { get; set; }
		public Application SSISApplication { get; set; }
		public Package LoadPackage(string istrPackageName)
		{
			MyEventListener eventListener = new MyEventListener();
			return SSISApplication.LoadPackage(istrPackageName, eventListener); ;
		}
		public Dictionary<string, string> LoadConnections()
		{
			return SSSISPackage.Connections.OfType<ConnectionManager>().Where(x => !x.Name.Contains(".dtsx"))
				 .ToDictionary(
				 x => x.Name,
				 x => x.ConnectionString,
				 StringComparer.CurrentCultureIgnoreCase);
			  
		}
		public Dictionary<string, object> LoadVariables()
		{
			return SSSISPackage.Variables.OfType<Variable>().ToList()
			   .ToDictionary(
			   x => x.Name,
			   x => x.Value,
			   StringComparer.CurrentCultureIgnoreCase); 

		}
		public Dictionary<string, string> LoadChildPackages()
		{
			return SSSISPackage.Connections.OfType<ConnectionManager>().Where(x => x.Name.Contains(".dtsx"))  
				 .ToDictionary(
				 x => x.Name,
				 x => x.ConnectionString,
				 StringComparer.CurrentCultureIgnoreCase);
			 

		} 
		public void LoadPackageDetails()
		{
			foreach (object SSISTasks in SSSISPackage.Executables)
			{
				try
				{
					switch (SSISTasks.GetType().Name)
					{
						case "TaskHost":
							{

								TaskHostObjTypeDtls(SSISTasks);
							}
							break;
						case "ForEachLoop":
							{
								foreach (object Subtask in ((ForEachLoop)(SSISTasks)).Executables)
								{
									TaskHostObjTypeDtls(Subtask);
								}
							}
							break;
					}
				}
				catch (Exception)
				{
					
				}
			}
		}
		private void TaskHostObjTypeDtls(object Subtask)
		{
			var PackageTask = ((dynamic)Subtask).InnerObject; 
			switch (PackageTask.GetType().Name)
			{
				case "ExecuteSQLTask":
					{
						ExecuteSQLTasks.Add(PackageTask);
					}
					break;
				case "ScriptTask":
					{
						ScriptTasks.Add(PackageTask);
					}
					break;
				case "ExecuteProcess":
					{
						ExecuteTasks.Add(((dynamic)Subtask).InnerObject.Name, PackageTask.Arguments);
					}
					break;
				case "FtpTask":
					{ 
						FTPTasks.Add(new FTPHandler
						{
							Connection = (dynamic)PackageTask.Connection,
							LocalPath = (dynamic)PackageTask.LocalPath,
							Operation = (dynamic)PackageTask.Operation.ToString(),
							RemotePath = (dynamic)PackageTask.RemotePath
						});
					}
					break;
				case "SendMailTask":
					{
					}
					break;
				case "FileSystemTask":
					{
						FileSystemTaks.Add(PackageTask);
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
}
