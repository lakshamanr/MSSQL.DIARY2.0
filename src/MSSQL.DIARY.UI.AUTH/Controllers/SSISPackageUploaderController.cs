using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSSQL.DIARY.SRV;
using MSSQL.DIARY.UI.Models;

namespace MSSQL.DIARY.UI.AUTH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSISPackageUploaderController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;

        public SSISPackageUploaderController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            SrvServerInfo = new SrvDatabaseServerInfo();
        }

        private SrvDatabaseServerInfo SrvServerInfo { get; }

        [HttpPost]
        public IActionResult Post([FromBody] FileToUpload theFile)
        {
            var webRootPath = _hostingEnv.WebRootPath;
            var newPath = Path.Combine(webRootPath, SrvServerInfo.GetServerName().FirstOrDefault() + "\\Packages");
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);
            if (!Directory.Exists(Path.Combine(webRootPath, SrvServerInfo.GetServerName().FirstOrDefault() + "\\json")))
                Directory.CreateDirectory(Path.Combine(webRootPath,
                    SrvServerInfo.GetServerName().FirstOrDefault() + "\\json"));
            var filePathName =
                Path.Combine(
                    _hostingEnv.WebRootPath, SrvServerInfo.GetServerName().FirstOrDefault() + "\\Packages\\")
                + Path.GetFileNameWithoutExtension(theFile.FileName)
                + Path.GetExtension(theFile.FileName);
            if (System.IO.File.Exists(filePathName)) System.IO.File.Delete(filePathName);
            using (var fs = new FileStream(filePathName, FileMode.CreateNew, FileAccess.Write))
            {
                if (theFile.FileAsBase64.Contains(","))
                    theFile.FileAsBase64 = theFile.FileAsBase64.Substring(theFile.FileAsBase64.IndexOf(",") + 1);
                theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);
                fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
            }

            return Ok();
        }
    }
}