using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;

namespace MSSQL.DIARY.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseSchemaController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;

        public DatabaseSchemaController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            srvDatabaseSchema = new SrvDatabaseSchema();
        }

        private SrvDatabaseSchema srvDatabaseSchema { get; }

        [HttpGet("[action]")]
        public List<PropertyInfo> GetListOfAllSchemaAndMsDescription(string istrdbName)
        {
            return srvDatabaseSchema.GetListOfAllSchemaAndMsDescription(istrdbName);
        }

        [HttpGet("[action]")]
        public List<PropertyInfo> CreateOrUpdateSchemaMsDescription(string istrdbName)
        {
            return srvDatabaseSchema.GetListOfAllSchemaAndMsDescription(istrdbName);
        }

        [HttpGet("[action]")]
        public List<SchemaReferanceInfo> GetSchemaReferanceInfo(string istrdbName, string astrSchema_Name)
        {
            return srvDatabaseSchema.GetSchemaReferanceInfo(istrdbName, astrSchema_Name);
        }

        [HttpGet("[action]")]
        public Ms_Description GetSchemaMsDescription(string istrdbName, string astrSchema_Name)
        {
            return srvDatabaseSchema.GetSchemaMsDescription(istrdbName, astrSchema_Name);
        }
    }
}