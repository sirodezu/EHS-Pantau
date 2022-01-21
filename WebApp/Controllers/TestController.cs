using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ViewData["SessionId"] = HttpContext.Session.Id;
            //TextReader reader = new StreamReader("import.txt");
            //var csvReader = new CsvReader(reader);
            //SAPHelper.GetDataRelation("D_Q");
            //SAPHelper.GetDataRelation("D_P");
            //SAPHelper.GetDataObject("QK");
            //SAPHelper.GetDataObject("Q");
            //SAPHelper.GetDataRelation("QK_Q");
            return View();
        }
        [HttpGet]
        public JsonResult GetDataObject(string obj)
        {
            SAPHelper.GetDataObject(obj);
            //return View();
            return Json(obj);
        }
        [HttpGet]
        public JsonResult GetDataRelation(string obj)
        {
            SAPHelper.GetDataRelation(obj);
            return Json(obj);
        }
        [HttpPost]
        public JsonResult GetListData(ActionRequest req)
        {
            IList<attRequest> param = req.param;
            return Json(param);
        }
        public class ActionRequest
        {
            public IList<attRequest>param { get; set; }
        }
        public class attRequest
        {
            public string name { get; set; }
            public string opr { get; set; }
            public string value { get; set; }
        }
    }
}