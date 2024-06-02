using Common.Constants;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Dto;
using System.Net.Http.Headers;
using System.Text;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Archiving.V1
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UploadFilesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UploadFilesController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        
        [HttpPost("upload"), DisableRequestSizeLimit]        
        public IActionResult UploadFiles([FromQuery]string formName, [FromQuery]string dbName, [FromQuery]Guid directoryGuid, [FromQuery]int type)
        {
            try
            {

                var folderPath = _configuration.GetSection("ArchiveFilePath").Value;
                if (string.IsNullOrWhiteSpace(folderPath))
                {
                    return BadRequest(new { Success = false, Message = "achiveFolderMissingInAppSettings", Status = -100 });
                }
                             
                if (string.IsNullOrWhiteSpace(formName))
                {
                    return BadRequest(new { Success = false, Message = "formNameMissing", Status = -200 });
                }

                //Guid archiveGuid = Guid.NewGuid();
                string savePath = "";
                if (type == 0)
                {
                    savePath = Path.Combine(folderPath, dbName, formName, directoryGuid.ToString());

                }
                else
                {
                    savePath = Path.Combine(folderPath, dbName, formName, type.ToString(), directoryGuid.ToString());
                }
                Directory.CreateDirectory(savePath);

                var files = Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        string fileWithExtension = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');



                        string fullPathFile = Path.Combine(savePath, fileWithExtension);

                        using (var stream = new FileStream(fullPathFile, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }


                    }

                }
                return Ok(new { Success = true, Message = "success", Status = 0 });

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errMsg += "\n" + ex.InnerException.Message;
                }
                return BadRequest(new { Success = false, Message = errMsg, Status = -1 });
            }

        }

        [HttpGet("getFiles")]
        public ResponseResult<List<ArchiveFiles>> GetFiles( string formName,  string dbName,  Guid directoryGuid,  int type)
        {
            try 
            {
                var folderPath = _configuration.GetSection("ArchiveFilePath").Value;

                if (string.IsNullOrWhiteSpace(folderPath))
                {
                    return new ResponseResult<List<ArchiveFiles>>() { Success = false, Message = "achiveFolderMissingInAppSettings", Status = -100 };
                }

                if (string.IsNullOrWhiteSpace(formName))
                {
                    return new ResponseResult<List<ArchiveFiles>>() { Success = false, Message = "formNameMissing", Status = -200 };
                }

                //Guid archiveGuid = Guid.NewGuid();
                string directorFullPath = "";
                if (type == 0)
                {
                    directorFullPath = Path.Combine(folderPath, dbName, formName, directoryGuid.ToString());

                }
                else
                {
                    directorFullPath = Path.Combine(folderPath, dbName, formName, type.ToString(), directoryGuid.ToString());
                }
               // var filesFullPathes = Directory.GetFiles(directorFullPath);
                FileInfo[] filesInfos = new DirectoryInfo(directorFullPath)
                            .GetFiles()
                            .OrderByDescending(f => f.LastWriteTime)
                            .ToArray();

                List<ArchiveFiles> files = new List<ArchiveFiles>();
                int index = 1;
                foreach (var file in filesInfos)
                {
                    //var fileInfo = new FileInfo(file);
                    files.Add(new ArchiveFiles() {
                        //Caption =  Path.GetFileName(file.FullName),
                        Caption = Path.GetFileName(file.Name),
                        Size = file.Length,
                        Key = index,
                        FileType = file.Extension
                    });
                    index++;
                }
                return new ResponseResult<List<ArchiveFiles>>()
                {
                    Success = true,
                    Data = files.ToList(),
                    Message = "success",
                    Status = 0

                };
            }
            catch(Exception ex) 
            { 
                string errMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errMsg = ex.InnerException.Message;
                }
                return new ResponseResult<List<ArchiveFiles>>() 
                {
                    Success = false,
                    Data = null,
                    Status = -300,
                    Message = errMsg
                };
            }

            
        }

        [HttpGet("download")]
        public IActionResult Download(string filePath, string fileName)
        {
            var folderPath = _configuration.GetSection("ArchiveFilePath").Value;
            var file = Path.Combine(folderPath, filePath);

            if (System.IO.File.Exists(file))
            {
                var fileBytes = System.IO.File.ReadAllBytes(file);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("delete")]
        public IActionResult delete(string filePath, string fileName)
        {
            var folderPath = _configuration.GetSection("ArchiveFilePath").Value;
            var file = Path.Combine(folderPath, filePath);

            if (System.IO.File.Exists(file))
            {
                //var fileBytes = System.IO.File.ReadAllBytes(file);
                //return File(fileBytes, "application/octet-stream", fileName);
                System.IO.File.Delete(file);
                return Ok(new { Message="susccess", Success = true});
            }
            else
            {
                return NotFound();
            }
        }

        //private string FileToBase64String(string filePath)
        //{
        //    try
        //    {
        //        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        //        return Convert.ToBase64String(fileData);
        //        //return Encoding.UTF8.GetString(fileData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error converting file to Base64: {ex.Message}");
        //        return "";
        //    }
        //}

    }
}
