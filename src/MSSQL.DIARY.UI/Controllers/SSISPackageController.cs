using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

//Microsoft.SqlServer.DTSRuntimeWrap
namespace MSSQL.DIARY.UI.Controllers
{
    [Produces("applications/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SSISPackageController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly string folderName = "Upload";

        public SSISPackageController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        //[HttpPost]
        public JsonResult UploadFile()
        {
            try
            {
                foreach (var file in Request.Form.Files)
                {
                    var webRootPath = _hostingEnv.WebRootPath;
                    var newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(newPath, fileName);
                        var fi = new FileInfo(fileName);
                        if (fi.Extension != ".dtsx")
                            continue;
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

                return new JsonResult("Upload Successful.");
            }
            catch (Exception ex)
            {
                new JsonResult("Upload Failed: " + ex.Message);
            }

            return new JsonResult("Upload Successful.");
        }
    }
}