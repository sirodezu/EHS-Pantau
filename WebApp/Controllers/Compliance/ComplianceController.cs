using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using WebApp.Models;
using Newtonsoft.Json;
using Microsoft.Security.Application;


namespace WebApp.Controllers
{
    
    public class ComplianceController : Controller
    {
        private string _rule_view = "ComplianceManage";
        private string _rule_add = "ComplianceManage";
        private string _rule_edit = "ComplianceManage";
        private string _rule_delete = "ComplianceManage";
        private string _path_controler = "/Compliance/";
        private string _path_view = "/Views/Compliance/Compliance/";
        private readonly string _table_name = "ta_compliance";
        private readonly string _table_title = "Compliance";

		private static IHostingEnvironment _hostingEnvironment;
        public ComplianceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(ComplianceModel.ViewModel filterColumn)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, "FasilitasView") || SecurityHelper.HasRule(HttpContext, "LegalView") || SecurityHelper.HasRule(HttpContext, "EnvironmentView"))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = ComplianceModel._pkKey;
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";

                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1":"0";
                    ViewData["AllowViewLegal"] = SecurityHelper.HasRule(HttpContext, "LegalView") ? "1" : "0";
                    ViewData["AllowViewFasilitas"] = SecurityHelper.HasRule(HttpContext, "FasilitasView") ? "1" : "0";
                    ViewData["AllowViewEnvironment"] = SecurityHelper.HasRule(HttpContext, "EnvironmentView") ? "1" : "0";


                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";

                    if (SecurityHelper.HasRule(HttpContext, "LegalViewAll")
                        || SecurityHelper.HasRule(HttpContext, "FasilitasViewAll")
                        || SecurityHelper.HasRule(HttpContext, "EnvironmentViewAll")
                        )
                    {
                        ViewData["AllowViewAll"] = "1";
                    }
                    else
                    {
                        ViewData["AllowViewAll"] = "0";
                    }
                    InfoUser infoUser = SecurityHelper.GetInfoUser(HttpContext);
                    ViewData["IsHeadArea"] = SecurityHelper.IsHeadArea(infoUser.person.id) ? "1" : "0";
                    ViewData["IsHeadPSA"] = SecurityHelper.IsHeadPSA(infoUser.person.id) ? "1" : "0";
                    if (ViewData["AllowViewAll"].ToString() == "0")
                    {
                        SecurityHelper.AreaData area = SecurityHelper.GetAreaOwner(infoUser.person.id);
                        filterColumn.ehs_area_id = area.ehs_area_id;
                        filterColumn.ba_id = area.ba_id;
                        filterColumn.pa_id = area.pa_id;
                        filterColumn.psa_id = area.psa_id;
                    }
                    ViewData["filterColumn"] = filterColumn;
                    return View(_path_view + "Index.cshtml");
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Account");
            }
        }

        [HttpPost]
        public JsonResult GetValidData(string date_begin, string date_end, string ehs_area_id, string ba_id, string pa_id, string psa_id)
        {
            DataSet data = ComplianceModel.GetValidData(date_begin, date_end, ehs_area_id, ba_id, pa_id, psa_id);
            return Json(data);
        }

    }
}