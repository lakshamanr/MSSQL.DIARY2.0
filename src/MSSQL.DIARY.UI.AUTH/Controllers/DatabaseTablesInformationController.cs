using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;
using MSSQL.DIARY.UI.Models;
using Newtonsoft.Json;

namespace MSSQL.DIARY.UI.AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTablesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;

        public DatabaseTablesController(IHostingEnvironment env)
        {
            _hostingEnv = env;
            SrvDatabaseTable = new SrvDatabaseTable();
            srvServerInfo = new SrvDatabaseServerInfo();
        }

        private SrvDatabaseTable SrvDatabaseTable { get; }
        private SrvDatabaseServerInfo srvServerInfo { get; }

        [HttpGet("[action]")]
        public List<TablePropertyInfo> GetAllDatabaseTables(string istrdbName, bool iblnSearchInSSISPackages)
        {
            var tblMS_Description = SrvDatabaseTable.GetAllDatabaseTablesDescription(istrdbName);
            var serverName = srvServerInfo.GetServerName().FirstOrDefault();
            var SSRS_package = new List<PackageJsonHandler>();
            SSISPackageInfoHandlerController.GetAllSSISPackages(_hostingEnv.WebRootPath);

            SSISPackageInfoHandlerController.SSISPkgeCache.Cache.TryGetValue(serverName, out SSRS_package);
            if (SSRS_package != null)
                tblMS_Description.ForEach(x =>
                {
                    SSRS_package.ForEach(x1 =>
                    {
                        x1.ExecuteSQLTask.ForEach(x3 =>
                        {
                            if (x3.SqlStatementSource.Contains(x.istrFullName))
                            {
                                if (x.lstSSISpackageReferance == null) x.lstSSISpackageReferance = new List<string>();
                                x.lstSSISpackageReferance.Add(x1.PackageLocation);
                            }
                        });
                    });
                    if (x.lstSSISpackageReferance != null)
                        x.lstSSISpackageReferance = x.lstSSISpackageReferance.DistinctBy(x1 => x1).ToList();
                });
            if (iblnSearchInSSISPackages)
                tblMS_Description = tblMS_Description.Where(x => x.lstSSISpackageReferance.IsNotNull()).ToList();
            return tblMS_Description;
        }

        [HttpGet("[action]")]
        public List<TableIndexInfo> GetTableIndex(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetTableIndex(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public TableCreateScript GetTableCreateScript(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetTableCreateScript(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public List<Tabledependencies> GetAllTabledependencies(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetAllTabledependencies(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public List<TableColumns> GetAllTablesColumn(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetAllTablesColumn(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public Ms_Description GetTableDescription(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetTableDescription(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public List<TableFKDependency> GetAllTableForeignKeys(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetAllTableForeignKeys(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public List<TableKeyConstraint> GetAlllKeyConstraints(string istrtableName, string istrdbName = null)
        {
            return SrvDatabaseTable.GetAlllKeyConstraints(istrtableName, istrdbName);
        }

        [HttpGet("[action]")]
        public bool CreateOrUpdateColumnDescription(string istrdbName, string astrDescription_Value,
            string astrTableName, string astrColumnName)
        {
            return SrvDatabaseTable.CreateOrUpdateColumnDescription(istrdbName, astrDescription_Value,
                astrTableName.Split(".")[0], astrTableName, astrColumnName);
        }

        [HttpGet("[action]")]
        public bool CreateOrUpdateTableDescription(string istrdbName, string astrDescription_Value,
            string astrSchema_Name, string astrTableName)
        {
            SrvDatabaseTable.CreateOrUpdateTableDescription(istrdbName, astrDescription_Value,
                astrTableName.Split(".")[0], astrTableName);
            return true;
        }

        [HttpGet("[action]")]
        public object GetDependancyTree(string istrdbName, string istrtableName)
        {
            return JsonConvert.DeserializeObject(
                SrvDatabaseTable.CreatorOrGetDependancyTree(istrdbName, istrtableName));
        }

        [HttpGet("[action]")]
        public List<TableFragmentationDetails> TableFragmentationDetails(string istrtableName, string istrdbName)
        {
            return SrvDatabaseTable.TableFragmentationDetails(istrdbName, istrtableName);
        }
    }
}