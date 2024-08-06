using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;
using System.Linq;
namespace MSSQL.DIARY.UI.AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseInformationController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;

        public DatabaseInformationController(IHostingEnvironment env)
        {
            _hostingEnv = env;
            SrvDatabaseInfo = new SrvDatabaseInfo();
        }

        private SrvDatabaseInfo SrvDatabaseInfo { get; }

        [HttpGet("[action]")]
        public string GetDatabaseUserDefinedText()
        {
            return "";
        }

        [HttpGet("[action]")]
        public List<string> GetDatabaseObjectTypes(string istrdbName)
        {
            return SrvDatabaseInfo.GetDatabaseObjectTypes();
        }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetdbPropertValues(string istrdbName)
        {
            return SrvDatabaseInfo.GetdbPropertValues(istrdbName);
        }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetdbOptionValues(string istrdbName)
        {
            return SrvDatabaseInfo.GetdbOptionValues(istrdbName);
        }

        [HttpGet("[action]")]
        public List<FileInfomration> GetdbFilesDetails(string istrdbName)
        {
            var result = SrvDatabaseInfo.GetdbFilesDetails(istrdbName);
            return result;
        }

        [HttpGet("[action]")]
        public Ms_Description GetERDiagram(string istrdbName, string istrServerName, string istrSchemaName)
        {
            var result = new Ms_Description();
            if (istrSchemaName.Equals("All"))
                result.desciption = !istrServerName.IsNullOrEmpty()
                    ? SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName, istrServerName, null)
                    : SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName.Split('/')[0],
                        istrdbName.Split('/')[1], null);
            else
                result.desciption = !istrServerName.IsNullOrEmpty()
                    ? SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName, istrServerName, istrSchemaName)
                    : SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName.Split('/')[0],
                        istrdbName.Split('/')[1], istrSchemaName);


            return result;
        }
        [HttpGet("[action]")] 
        public Ms_Description GetERDiagramWithSelectedTables(string istrdbName, string istrServerName, string istrSchemaName,  string SelectedTables)
        {
            var alstOfSelectedTables = SelectedTables.Split(';').Where(x=>x.IsNotNullOrEmpty()).ToList();
            var newSelectedTables = new List<string>();
            alstOfSelectedTables.ForEach(x => {
                newSelectedTables.Add(x.Split('.')[1]);
            });
            var result = new Ms_Description();
            if (istrSchemaName.Equals("All"))
                result.desciption = !istrServerName.IsNullOrEmpty()
                    ? SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName, istrServerName, null, newSelectedTables)
                    : SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName.Split('/')[0],
                        istrdbName.Split('/')[1], null, newSelectedTables);
            else
                result.desciption = !istrServerName.IsNullOrEmpty()
                    ? SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName, istrServerName, istrSchemaName, newSelectedTables)
                    : SrvDatabaseInfo.GetERDiagram(_hostingEnv.WebRootPath, istrdbName.Split('/')[0],
                        istrdbName.Split('/')[1], istrSchemaName, newSelectedTables);


            return result;

        }
    }
}