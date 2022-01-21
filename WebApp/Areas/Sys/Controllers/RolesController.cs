using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Areas.Sys.Models;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using Microsoft.Security.Application;

namespace WebApp.Areas.Sys.Controllers
{
    
    [Area("Sys")]
    public class RolesController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RoleManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RoleManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RoleManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RoleManage";
        private string _path_controler = "/Sys/Roles/";
        private string _path_view = "/Areas/Sys/Views/Roles/";
        private readonly string _table_name = "sys_roles";
        private readonly string _table_title = "Peran";
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
                    ViewData["pkKey"] = RolesModel._pkKey;
                    ViewData["displayItem"] = RolesModel.GetDisplayItem();
                    ViewData["column_att"] = RolesModel.GetGridColumn();
                    ParamGrid param = RolesModel.GetListParam();
                    string curUSerId = SecurityHelper.CurrentUserId(HttpContext);
                    if (curUSerId != "-1") {
                        param.btnAdd = "0";
                        param.btnDelete = "0";
                    }
                    ViewData["param"] = param;
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    ViewData["AllowView"] = HttpContext.Session.GetString(_rule_view) != null ? "1" : "0";
                    ViewData["AllowAdd"] = HttpContext.Session.GetString(_rule_add) != null ? "1" : "0";
                    ViewData["AllowEdit"] = HttpContext.Session.GetString(_rule_edit) != null ? "1" : "0";
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
        public JsonResult GetListData(RolesModel.ActionRequest param)
        {
            GridData data = RolesModel.GetListData(HttpContext, param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                string ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();
                string sadsad = HttpContext.Request.QueryString.ToString();

                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    ViewData["Title"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    RolesModel.ViewModel fieldModel = new RolesModel.ViewModel();
                    fieldModel.id = null;
                    fieldModel.nama = "";
                    fieldModel.keterangan = "";
                    fieldModel.level_id = null;
                    fieldModel.level_id_text = "";
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != "")
                    {
                        data = RolesModel.GetViewData(id);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.kode = dr["kode"].ToString();
                                fieldModel.nama = dr["nama"].ToString();
                                fieldModel.keterangan = dr["keterangan"].ToString();
                                fieldModel.level_id = dr["level_id"].ToString();
                                fieldModel.level_id_text = dr["level_id"].ToString();
                                ViewData["fieldModel"] = fieldModel;
                            }
                        }
                    }
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
        [HttpGet]
        public IActionResult Add()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["Title"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    RolesModel.ViewModel fieldModel = new RolesModel.ViewModel();
                    fieldModel.id = RolesModel.GetNewId().ToString();
                    fieldModel.kode = "";
                    fieldModel.nama = "";
                    fieldModel.keterangan = "";
                    fieldModel.level_id = null;
                    fieldModel.level_id_text = "";
                    fieldModel.rule_id = RolesModel.GetRuleId(fieldModel.id);
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "Edit.cshtml");
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
        [System.Obsolete]
        public JsonResult Insert(RolesModel.ViewModel fieldModel)
        {
            //GridData data = RolesModel.GetList(param);
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["level_id"] = fieldModel.level_id;
                    string rule_id = fieldModel.rule_id != null ? fieldModel.rule_id : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = RolesModel.Insert(HttpContext, data, rule_id);
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
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    ViewData["Title"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    DataTable data = RolesModel.GetViewData(id);
                    RolesModel.ViewModel fieldModel = new RolesModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.level_id = dr["level_id"].ToString();
                        fieldModel.level_id_text = "";
                        fieldModel.rule_id = RolesModel.GetRuleId(fieldModel.id);
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["curUSerId"] = SecurityHelper.CurrentUserId(HttpContext);
                    return View(_path_view + "Edit.cshtml");
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
        [System.Obsolete]
        public JsonResult Update(RolesModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string id_old = fieldModel.id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(RolesModel.GetData(id_old));

                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["level_id"] = fieldModel.level_id;

                    OrderedDictionary rule_old = new OrderedDictionary();
                    OrderedDictionary rule_new = new OrderedDictionary();
                    rule_old["rule_id"] = RolesModel.GetRuleId(id_old);
                    string rule_id = fieldModel.rule_id != null ? fieldModel.rule_id : "";
                    rule_new["rule_id"] = rule_id;
                    if (DataModel.HasUpdate(data, data_old) || DataModel.HasUpdate(rule_new, rule_old))
                    {
                        SqlHelper.ConvertEmptyValuesToDBNull(data);
                        result = RolesModel.Update(HttpContext, data, data_old, rule_id);
                    }
                    else
                    {
                        result.status = 2;
                        result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                        result.message = ResxHelper.GetValue("Message", "NoRecodeUpdate", "Tidak Ada Perubahan Data");
                    }
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
        [HttpPost]
        public JsonResult Delete(string id)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
                    OrderedDictionary data = DataModel.DtToDictionary(RolesModel.GetData(id));
                    if (data != null)
                    {
                        result = RolesModel.Delete(HttpContext, data);
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
        public JsonResult getItemRuleTreeCheck(paramTreeRolecheck param)
        {

            IList<lookup_tree_check> data = RolesModel.getItemRuleTreeCheck(param.role_id);
            //string hasil = JsonConvert.SerializeObject(data).Replace("check", "checked");
            return Json(data);

        }
        [HttpPost]
        public JsonResult LookupData(lookup_param param)
        {
            DataTable data = RolesModel.LookupData(param);
            return Json(data);
        }
    }
    public class paramTreeRolecheck
    {
        public string role_id { get; set; }
    }
}