using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DownloadController : Controller
    {
        private static IHostingEnvironment _hostingEnvironment;
        public DownloadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public FileResult Index(string id)
        {
            string par = SecurityHelper.DecryptString(id);
            string[] param = par.Split(';');
            string tbl_name = param[0];
            string PkValue = param[1];
            string col_name = param[2];
            string file_name = param[3];
            string[] filenames = file_name.Split('.');
            string ext = filenames[filenames.Length - 1];
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");

            if (tbl_name == "ta_document_nm")
            {
                var folder_path = SqlHelper.ExecuteScalarString("SELECT folder_path FROM ta_document_nm WHERE id = " + PkValue);
                pathDest = Path.Combine(pathDest, folder_path);
            }
            else
            {
                pathDest = Path.Combine(pathDest, tbl_name);
                string[] pk = PkValue.Split(',');
                foreach (string item in pk)
                {
                    pathDest = Path.Combine(pathDest, item);
                }
                pathDest = Path.Combine(pathDest, col_name);
            }

            var filepath = Path.Combine(pathDest, file_name);
            if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "gif")
            {
                var image = System.IO.File.OpenRead(filepath);
                return File(image, "image/jpeg");
            } else if(ext == "pdf")
            {
                //var stream = new FileStream(filepath, FileMode.Open);
                //return File(stream, "application/pdf", file_name);
                var stream = new FileStream(filepath, FileMode.Open);
                return new FileStreamResult(stream, "application/pdf");
            }
            else {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes, "application/x-msdownload", file_name);
            }
        }
        
    }
}