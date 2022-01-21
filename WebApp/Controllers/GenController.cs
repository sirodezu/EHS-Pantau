using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class GenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GeneratePSAMT()
        {
            SAPHelper.GeneratePSAMT();
            return Json("GeneratePSAMT");
        }
    }
}