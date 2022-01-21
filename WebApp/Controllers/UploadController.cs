using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class UploadController : Controller
    {
        [DataContract]
        public class ChunkMetaData
        {
            [DataMember(Name = "uploadUid")]
            public string UploadUid { get; set; }
            [DataMember(Name = "fileName")]
            public string FileName { get; set; }
            [DataMember(Name = "contentType")]
            public string ContentType { get; set; }
            [DataMember(Name = "chunkIndex")]
            public long ChunkIndex { get; set; }
            [DataMember(Name = "totalChunks")]
            public long TotalChunks { get; set; }
            [DataMember(Name = "totalFileSize")]
            public long TotalFileSize { get; set; }
        }

        public class FileResult
        {
            public bool uploaded { get; set; }
            public string fileUid { get; set; }
        }
        public class FileTemp
        {
            public string file_name { get; set; }
            public string file_path { get; set; }
        }

        private static IHostingEnvironment _hostingEnvironment;
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Submit(IEnumerable<IFormFile> files)
        {
            if (files != null)
            {
                TempData["UploadedFiles"] = GetFileInfo(files);
            }

            return RedirectToRoute("Demo", new { section = "upload", example = "result" });
        }

        public void clear_temp() {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            System.IO.DirectoryInfo di = new DirectoryInfo(upload_temp);

            foreach (FileInfo file in di.GetFiles())
            {
                System.TimeSpan diff = DateTime.Now.Subtract(file.CreationTime);
                if (diff.Days > 10)
                {
                    file.Delete(); 
                }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                System.TimeSpan diff = DateTime.Now.Subtract(dir.CreationTime);
                if (diff.Days > 10) {
                    dir.Delete(true);
                }
            }
        }
        public async Task<IActionResult> SaveImg(IEnumerable<IFormFile> file_img)
        {
            clear_temp();
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null? Settings.GetAppSetting("path_upload_temp"): "C:\\Temp" ;
            string sessid = HttpContext.Session.Id;
            IList<FileTemp> result_temp = new List<FileTemp>();
            if (file_img != null)
            {
                foreach (var file in file_img)
                {
                    if (!Directory.Exists(upload_temp)){Directory.CreateDirectory(upload_temp);}
                    string pathData = Path.Combine(upload_temp, sessid);
                    if (!Directory.Exists(pathData)) { Directory.CreateDirectory(pathData); }
                    // Some browsers send file names with full path. This needs to be stripped.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(pathData, fileName);
                    FileTemp item_temp = new FileTemp();
                    // The files are not actually saved in this demo
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        item_temp.file_name = fileName;
                        item_temp.file_path = physicalPath;
                        result_temp.Add(item_temp);
                    }
                }
            }
            // Return an empty string to signify success
            return Content(JsonConvert.SerializeObject(result_temp));
        }

        public ActionResult RemoveImg(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(upload_temp, sessid);
                    physicalPath = Path.Combine(physicalPath, fileName);
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }
            clear_temp();
            // Return an empty string to signify success
            return Content("");
        }

        public void AppendToFile(string fullPath, Stream content)
        {
            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (content)
                    {
                        content.CopyTo(stream);
                    }
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        //public ActionResult ChunkSave(IEnumerable<IFormFile> files, string metaData)
        //{
        //    if (metaData == null)
        //    {
        //        return Save(files);
        //    }

        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(metaData));
        //    var serializer = new DataContractJsonSerializer(typeof(ChunkMetaData));
        //    ChunkMetaData somemetaData = serializer.ReadObject(ms) as ChunkMetaData;
        //    string path = String.Empty;
        //    // The Name of the Upload component is "files"
        //    if (files != null)
        //    {
        //        foreach (var file in files)
        //        {
        //            //path = Path.Combine(Server.MapPath("~/App_Data"), somemetaData.FileName);

        //            //AppendToFile(path, file.InputStream);
        //        }
        //    }

        //    FileResult fileBlob = new FileResult();
        //    fileBlob.uploaded = somemetaData.TotalChunks - 1 <= somemetaData.ChunkIndex;
        //    fileBlob.fileUid = somemetaData.UploadUid;

        //    return Json(fileBlob);
        //}

        private IEnumerable<string> GetFileInfo(IEnumerable<IFormFile> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.Length);
        }
    }
}