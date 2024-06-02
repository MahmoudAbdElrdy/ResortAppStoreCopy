﻿using Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.BulkOperations.Internal.Data.SqlClient;

namespace ERPBackEnd.Application.Helpers.UploadHelper
{
    public static class Upload
    {
        private static string GetRandomName()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<char> characters = new List<char>()
                {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
                'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

            string name = "";
            Random rand = new Random();
            // run the loop till I get a string of 10 characters  
            for (int i = 0; i < 25; i++)
            {
                // Get random numbers, to get either a character or a number...  
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number  
                    random = rand.Next(0, numbers.Count);
                    name += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    name += characters[random].ToString();
                }
            }
            return name;
        }
        public static string SaveFile(Microsoft.AspNetCore.Http.IFormFile formFile,long complaintId)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            List<string> _extentions = new List<string>() {  ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".OGG", //etc
                                                             ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".RMA", //etc
                                                             ".AVI", ".MP4", ".DIVX", ".WMV" };
            try
            {
                var file = formFile;
                var folderName = Path.Combine("wwwroot/Uploads/Complanits",complaintId.ToString());
                // Check if directory exist or not
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if (file.Length > 0)
                {


                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var _imgname = DateTime.Now.Ticks;
                    // check valid extension
                    #region  check size

                    string extension = Path.GetExtension(file.FileName);

                    //extension= extension.Substring(1, extension.Length-1);
                    var size = file.Length;
                    if (!_extentions.Contains(extension.ToUpper()))
                    {
                        temp.Add("dbPath", "");
                        temp.Add("_ext", "");
                        temp.Add("stat", "File extension is not valid.");
                        //return new {path="",extention="",state= "File extension is not valid." };
                        return "!!امتداد الملف غير صحيح";
                    }

                    if (size > (1000 * 1024 * 1024))
                        return "حجم الملف اكبر من 5 ميجا بايت";
                    #endregion

                    // Updated To GetFileName By Elgendy
                    var _ext = Path.GetFileNameWithoutExtension(file.FileName);

                    //  var fullPath = Path.Combine(pathToSave, _imgname +_ext);
                    var filepath = Path.Combine(folderName, _imgname + extension);

                    using (var stream = new System.IO.FileStream(filepath, FileMode.Create))

                    {
                        file.CopyTo(stream);
                    }
                    string dbPath = _imgname + extension;
                    temp.Add("dbPath", dbPath);
                    temp.Add("_ext", _ext);
                    temp.Add("stat", "done");
                    //return new { path = dbPath, extention = _ext, state = "done." };

                    return dbPath;

                }
                else
                {
                    temp.Add("dbPath", "");
                    temp.Add("_ext", "");
                    temp.Add("stat", "BadRequest");
                    //return new { path = "", extention = "", state = "BadRequest" };

                    return "BadRequest";
                }
            }
            catch (Exception ex)
            {
                temp.Add("dbPath", "");
                temp.Add("_ext", "");
                temp.Add("stat", "Internal server error" + ex.ToString());
                //return new { path = "", extention = "", state = "Internal server error" + ex.ToString() };

                return "خطا في السيرفر";
            }
        }

        public static string SaveFile(Microsoft.AspNetCore.Http.IFormFile formFile)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            List<string> _extentions = new List<string>() {  ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".OGG", //etc
                                                             ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".RMA", //etc
                                                             ".AVI", ".MP4", ".DIVX", ".WMV" };
            try
            {
                var file = formFile;
                var folderName = Path.Combine("wwwroot/Uploads/Company");
                // Check if directory exist or not
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if (file.Length > 0)
                {


                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var _imgname =  DateTime.Now.Ticks;
                    // check valid extension
                    #region  check size

                    string extension = Path.GetExtension(file.FileName);

                    //extension= extension.Substring(1, extension.Length-1);
                    var size = file.Length;
                    if (!_extentions.Contains(extension.ToUpper()))
                    {
                        temp.Add("dbPath", "");
                        temp.Add("_ext", "");
                        temp.Add("stat", "File extension is not valid.");
                        //return new {path="",extention="",state= "File extension is not valid." };
                        return "!!امتداد الملف غير صحيح";
                    }

                    if (size > (1000 * 1024 * 1024))
                        return "حجم الملف اكبر من 5 ميجا بايت";
                    #endregion

                    // Updated To GetFileName By Elgendy
                    var _ext = Path.GetFileNameWithoutExtension(file.FileName);

                    //  var fullPath = Path.Combine(pathToSave, _imgname +_ext);
                    var filepath = Path.Combine(folderName, _imgname + extension);

                    using (var stream = new System.IO.FileStream(filepath, FileMode.Create))

                    {
                        file.CopyTo(stream);
                    }
                    string dbPath = _imgname + extension;
                    temp.Add("dbPath", dbPath);
                    temp.Add("_ext", _ext);
                    temp.Add("stat", "done");
                    //return new { path = dbPath, extention = _ext, state = "done." };

                    return dbPath;

                }
                else
                {
                    temp.Add("dbPath", "");
                    temp.Add("_ext", "");
                    temp.Add("stat", "BadRequest");
                    //return new { path = "", extention = "", state = "BadRequest" };

                    throw new UserFriendlyException("errorInServer");
                }
            }
            catch (Exception ex)
            {
                temp.Add("dbPath", "");
                temp.Add("_ext", "");
                temp.Add("stat", "Internal server error" + ex.ToString());
                //return new { path = "", extention = "", state = "Internal server error" + ex.ToString() };

                throw new UserFriendlyException("errorInServer");
            }
        }
        public static string SaveFile(Microsoft.AspNetCore.Http.IFormFile formFile,string folderPath)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            List<string> _extentions = new List<string>() {  ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".OGG", //etc
                                                             ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".RMA", //etc
                                                             ".AVI", ".MP4", ".DIVX", ".WMV" };
            try
            {
                var file = formFile;
                var folderName = Path.Combine(folderPath);
                // Check if directory exist or not
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if (file.Length > 0)
                {


                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var _imgname = DateTime.Now.Ticks;
                    // check valid extension
                    #region  check size

                    string extension = Path.GetExtension(file.FileName);

                    //extension= extension.Substring(1, extension.Length-1);
                    var size = file.Length;
                    if (!_extentions.Contains(extension.ToUpper()))
                    {
                        temp.Add("dbPath", "");
                        temp.Add("_ext", "");
                        temp.Add("stat", "File extension is not valid.");
                        //return new {path="",extention="",state= "File extension is not valid." };
                        return "!!امتداد الملف غير صحيح";
                    }

                    if (size > (1000 * 1024 * 1024))
                        return "حجم الملف اكبر من 5 ميجا بايت";
                    #endregion

                    // Updated To GetFileName By Elgendy
                    var _ext = Path.GetFileNameWithoutExtension(file.FileName);

                    //  var fullPath = Path.Combine(pathToSave, _imgname +_ext);
                    var filepath = Path.Combine(folderName, _imgname + extension);

                    using (var stream = new System.IO.FileStream(filepath, FileMode.Create))

                    {
                        file.CopyTo(stream);
                    }
                    string dbPath = _imgname + extension;
                    temp.Add("dbPath", dbPath);
                    temp.Add("_ext", _ext);
                    temp.Add("stat", "done");
                    //return new { path = dbPath, extention = _ext, state = "done." };

                    return dbPath;

                }
                else
                {
                    temp.Add("dbPath", "");
                    temp.Add("_ext", "");
                    temp.Add("stat", "BadRequest");
                    //return new { path = "", extention = "", state = "BadRequest" };

                    throw new UserFriendlyException("errorInServer");
                }
            }
            catch (Exception ex)
            {
                temp.Add("dbPath", "");
                temp.Add("_ext", "");
                temp.Add("stat", "Internal server error" + ex.ToString());
                //return new { path = "", extention = "", state = "Internal server error" + ex.ToString() };

                throw new UserFriendlyException("errorInServer");
            }
        }


        public static byte[] SaveFileAsBinary(Microsoft.AspNetCore.Http.IFormFile formFile)
        {
            try
            {
                var file = formFile;
                byte[] imageData = null;

                if (file.Length > 0)
                {
                    // Check file extension
                    List<string> _extensions = new List<string>() { ".PNG", ".JPG", ".JPEG", ".GIF" };
                    string extension = Path.GetExtension(file.FileName).ToUpper();
                    if (!_extensions.Contains(extension))
                    {
                        throw new Exception("File extension is not valid.");
                    }

                    // Check file size (max size is 2 MB)
                    long maxSize = 2 * 1024 * 1024; // 2 MB
                    if (file.Length > maxSize)
                    {
                        throw new Exception("File size exceeds the maximum allowed size (2 MB).");
                    }

                    // Read file data into byte array
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();



                        return imageData;

                    }

                }
                else
                {
                    throw new Exception("No file content.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving file as binary: " + ex.Message);
            }
        }



        public static async Task<string> UploadFiles(IFormFile file, IHostingEnvironment hosting, string pathName, string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string path = Path.Combine(hosting.WebRootPath, "Uploads", property, pathName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filesNames = Directory.GetFiles(path);
                var isNotFinished = true;
                var newFileName = "";
                while (isNotFinished)
                {
                    newFileName = GetRandomName() + "." + file.FileName.Split(".").Last();
                    if (!filesNames.Contains(newFileName))
                    {
                        isNotFinished = false;
                    }
                }
                using (FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return ("wwwroot/" + "Uploads/" + property + "/" + pathName + "/" + newFileName);
            }
            else
            {
                return await UploadFiles(file, hosting, pathName);
            }
        }
        public static async Task<string> UploadFiles(IFormFile file, IHostingEnvironment hosting, string pathName)
        {
            string path = Path.Combine(hosting.WebRootPath, "Uploads", pathName.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filesNames = Directory.GetFiles(path);
            var isNotFinished = true;
            var newFileName = "";
            while (isNotFinished)
            {
                newFileName = GetRandomName() + "." + file.FileName.Split(".").Last();
                if (!filesNames.Contains(newFileName))
                {
                    isNotFinished = false;
                }
            }
            using (FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return ("wwwroot/" + "Uploads/" + pathName + "/" + newFileName);
        }
        public static async Task<string> UploadPostFiles(IFormFile file, IHostingEnvironment hosting, string pathName, string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string path = Path.Combine(hosting.WebRootPath, "Uploads", property);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filesNames = Directory.GetFiles(path);
                var isNotFinished = true;
                var newFileName = "";
                while (isNotFinished)
                {
                    newFileName = GetRandomName() + "." + file.FileName.Split(".").Last();
                    if (!filesNames.Contains(newFileName))
                    {
                        isNotFinished = false;
                    }
                }
                using (FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return ("wwwroot/" + "Uploads/" + property + "/" + newFileName);
            }
            else
            {
                return await UploadFiles(file, hosting, pathName);
            }
        }

        public static async Task<string> UploadPostFilesExternal(IFormFile file, IHostingEnvironment hosting, string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                string path = Path.Combine(hosting.WebRootPath, "Uploads", property);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filesNames = Directory.GetFiles(path);
                var isNotFinished = true;
                var newFileName = "";
                while (isNotFinished)
                {
                    newFileName = GetRandomName() + "." + file.FileName.Split(".").Last();
                    if (!filesNames.Contains(newFileName))
                    {
                        isNotFinished = false;
                    }
                }
                using (FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return ("wwwroot/" + "Uploads/" + property + "/" + newFileName);
            }
            else
            {
                return await UploadFiles(file, hosting, "ExternalRequests");
            }
        }



        public static async Task<string> UploadQRCode(IFormFile file, string pathName)
        {
            
                if (!Directory.Exists(pathName))
                {
                    Directory.CreateDirectory(pathName);
                }

                var newFileName = file.Name+ "." + file.FileName.Split(".").Last();
                 
                using (FileStream stream = new FileStream(Path.Combine(pathName, newFileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return (pathName + "/" + newFileName);
        }
        public static Image BinaryToImage(byte[] binaryData)
        {
            using (MemoryStream ms = new MemoryStream(binaryData))
            {
                // Load the binary data into an image
                Image image = Image.FromStream(ms);
                return image;
            }
        }

    }
}
