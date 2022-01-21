using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApp.Models;
using System.Data;
using System.Collections.Specialized;
using WebApp.Areas.Sys.Models;
using Newtonsoft.Json;

namespace WebApp.Areas.Sys.Controllers
{
    
    [Area("Sys")]
    public class AudittrailController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "AuditTrailView";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "AuditTrailDelete";
        private string _path_controler = "/Sys/Audittrail/";
        private string _path_view = "/Areas/Sys/Views/Audittrail/";
        private readonly string _table_name = "sys_audittrail";
        private readonly string _table_title = "Audittrail";

        public IActionResult Index()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = AudittrailModel._pkKey;
                    ViewData["displayItem"] = AudittrailModel.GetDisplayItem();
                    ViewData["column_att"] = AudittrailModel.GetGridColumn();
                    ViewData["param"] = AudittrailModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    ViewData["AllowView"] = HttpContext.Session.GetString(_rule_view) != null ? "1" : "0";
                    ViewData["AllowDelete"] = HttpContext.Session.GetString(_rule_delete) != null ? "1" : "0";
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
        public JsonResult GetListData(AudittrailModel.ActionRequest param)
        {
            GridData data = AudittrailModel.GetListData(param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    ViewData["Title"] = ResxHelper.GetValue("sys_audittrail", "ViewTitle", "Infromasi Detil Audittrail");
                    ViewData["TitleHeader"] = ResxHelper.GetValue("sys_audittrail", "ViewTitle", "Infromasi Detil Audittrail");
                    AudittrailModel.ViewModel fieldModel = new AudittrailModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AudittrailModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.action_time = dr["action_time"].ToString();
                                fieldModel.ipaddress = dr["ipaddress"].ToString();
                                fieldModel.username = dr["username"].ToString();
                                fieldModel.obj_data = dr["obj_data"].ToString();
                                fieldModel.obj_key = dr["obj_key"].ToString();
                                fieldModel.url = dr["url"].ToString();
                                fieldModel.user_agent = dr["user_agent"].ToString();
                                fieldModel.action_type = dr["action_type"].ToString();
                                fieldModel.action_title = dr["action_title"].ToString();
                                fieldModel.action_data = JsonConvert.DeserializeObject<List<AudittrailModel.ActionDataModel>>(dr["action_data"].ToString());
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "View.cshtml");
                }
                else
                {
                    return View("Views/Account/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Account");
            }
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
                    if (id != null)
                    {
                        result = AudittrailModel.Delete(id);
                        return Json(result);
                    }
                    else
                    {
                        result.status = 2;
                        result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                        result.message = ResxHelper.GetValue("Message", "NoRecodeFound");
                        return Json(result);
                    }
                }
                else
                {
                    result.status = 3;
                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                    result.message = ResxHelper.GetValue("Message", "NotAuthorization");
                    return Json(result);
                }
            }
            else
            {
                result.status = 3;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "SessionHasExpired");
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult LookupObjData(string obj_data)
        {

            DataTable data = AudittrailModel.LookupObjData(obj_data);
            return Json(data);

        }
        [HttpPost]
        public JsonResult LookupUsername(string username)
        {

            DataTable data = AudittrailModel.LookupUsername(username);
            return Json(data);

        }

        [HttpPost]
        public JsonResult Reset()
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
                    result = AudittrailModel.Reset();
                    return Json(result);
                }
                else
                {
                    result.status = 3;
                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                    result.message = ResxHelper.GetValue("Message", "NotAuthorization");
                    return Json(result);
                }
            }
            else
            {
                result.status = 3;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "SessionHasExpired");
                return Json(result);
            }
        }

    }
}