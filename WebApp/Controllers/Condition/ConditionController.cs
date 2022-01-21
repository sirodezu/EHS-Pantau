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
    
    public class ConditionController : Controller
    {
        private string _rule_view = "ConditionManage";
        private string _rule_add = "ConditionManage";
        private string _rule_edit = "ConditionManage";
        private string _rule_delete = "ConditionManage";
        private string _path_controler = "/Condition/";
        private string _path_view = "/Views/Condition/Condition/";
        private readonly string _table_name = "ta_condition";
        private readonly string _table_title = "Condition";

		private static IHostingEnvironment _hostingEnvironment;
        public ConditionController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(ConditionModel.ViewModel filterColumn)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, "EmergencyView") || SecurityHelper.HasRule(HttpContext, "AuditView") || SecurityHelper.HasRule(HttpContext, "CampaignView"))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = ConditionModel._pkKey;
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";

                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1":"0";
                    ViewData["AllowViewEmergency"] = SecurityHelper.HasRule(HttpContext, "EmergencyView") ? "1" : "0";
                    ViewData["AllowViewAudit"] = SecurityHelper.HasRule(HttpContext, "AuditView") ? "1" : "0";
                    ViewData["AllowViewCampaign"] = SecurityHelper.HasRule(HttpContext, "CampaignView") ? "1" : "0";


                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";

                    if (SecurityHelper.HasRule(HttpContext, "EmergencyViewAll")
                        || SecurityHelper.HasRule(HttpContext, "AuditViewAll")
                        || SecurityHelper.HasRule(HttpContext, "CampaignViewAll")
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
            DataSet data = ConditionModel.GetValidData(date_begin, date_end, ehs_area_id, ba_id, pa_id, psa_id);
            return Json(data);
        }

    }
}