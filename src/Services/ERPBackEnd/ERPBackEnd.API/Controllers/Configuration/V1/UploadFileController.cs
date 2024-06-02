using Common.BaseController;
using ERPBackEnd.API.Helpers;
using ERPBackEnd.Application.Helpers.UploadHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Maintenance.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UploadFileController : BaseController
    {
       
        public UploadFileController()
        {
           
        }
        [HttpPost("FileUpload")]
        public ActionResult<string> UploadFile()
        {
           
           var res = Upload.SaveFile(Request.Form.Files[0]);
            
            return Ok(res);
        }
        [HttpPost("FileUploadAsBinary")]
        public ActionResult<string> UploadFileAsBinary()
        {

            var res = Upload.SaveFileAsBinary(Request.Form.Files[0]);

            return Ok(res);
        }

        [HttpGet("PathCompany")]
        public string PathCompany()  
        {

            return $"{Request.Scheme}://{Request.Host}{Request.PathBase}" + "/wwwroot/Uploads/Company";
        }
    }
}
