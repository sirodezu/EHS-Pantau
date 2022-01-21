using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Sys.Models;
using System.Data;

namespace WebApp.Areas.Sys.Controllers
{
    [Area("Sys")]
    public class LangController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LookupData()
        {
            DataTable data = LangModel.LookupData();
            return Json(data);
        }
    }
}