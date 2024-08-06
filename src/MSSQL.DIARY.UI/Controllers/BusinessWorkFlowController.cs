using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.SRV;
using Newtonsoft.Json;

namespace MSSQL.DIARY.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessWorkFlowController : ControllerBase
    {
        public BusinessWorkFlowController()
        {
            SrvDatabaseWorkFlow = new SrvDatabaseWorkFlow();
        }

        public SrvDatabaseWorkFlow SrvDatabaseWorkFlow { get; set; }

        [HttpGet("[action]")]
        public List<string> GetWorkList(string istrdbName)
        {
            return SrvDatabaseWorkFlow.GetWorkList(istrdbName);
        }

        [HttpGet("[action]")]
        public object GetWorkDetailsbyName(string istrdbName, string istrWorkFlowName)
        {
            return JsonConvert.DeserializeObject(
                SrvDatabaseWorkFlow.GetWorkDetailsbyName(istrdbName, istrWorkFlowName));
        }
    }
}