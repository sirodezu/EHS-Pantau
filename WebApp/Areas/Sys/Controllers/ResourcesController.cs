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
    public class ResourcesController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "ResourcesManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "ResourcesManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "ResourcesManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "ResourcesManage";
        private string _path_controler = "/Sys/Resources/";
        private string _path_view = "/Areas/Sys/Views/Resources/";
        private readonly string _table_name = "sys_resources";
        private readonly string _table_title = "Bahasa";
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
                    ViewData["pkKey"] = ResourcesModel._pkKey;
                    ViewData["displayItem"] = ResourcesModel.GetDisplayItem();
                    ViewData["column_att"] = ResourcesModel.GetGridColumn();
                    var param = ResourcesModel.GetListParam();
                    param.btnDelete = SecurityHelper.CurrentUserId(HttpContext) == "-1" ? "1":"0";
                    ViewData["param"] = param;
                    ViewData["UrlGetList"] = baseUrl+_path_controler+"GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    ViewData["UrlClearCache"] = baseUrl + _path_controler + "ClearCache";
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
        public JsonResult GetListData(ResourcesModel.ActionRequest param)
        {
            GridData data = ResourcesModel.GetListData(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult LookupClassName()
        {
            DataTable data = ResourcesModel.LookupClassName();
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(ResourcesModel.Pk Pk)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ConfigHelper.GetValue("ApllicationCode") + ":: " + ViewData["TitleHeader"];
                    ResourcesModel.ViewModel fieldModel = new ResourcesModel.ViewModel();
                    fieldModel.lang_code = "";
                    fieldModel.class_name = "";
                    fieldModel.key_name = "";
                    fieldModel.key_value = "";
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    data = ResourcesModel.GetViewData(Pk);
                    if (data != null)
                    {
                        foreach (DataRow dr in data.Rows)
                        {
                            fieldModel.lang_code = dr["lang_code"].ToString();
                            fieldModel.lang_code_old = dr["lang_code"].ToString();
                            fieldModel.class_name = dr["class_name"].ToString();
                            fieldModel.class_name_old = dr["class_name"].ToString();
                            fieldModel.key_name = dr["key_name"].ToString();
                            fieldModel.key_name_old = dr["key_name"].ToString();
                            fieldModel.key_value = dr["key_value"].ToString();
                            ViewData["fieldModel"] = fieldModel;
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
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ConfigHelper.GetValue("ApllicationCode") + ":: " + ViewData["TitleHeader"];
                    ViewData["form_type"] = "Add";
                    ViewData["UrlAction"] = baseUrl+_path_controler+"Insert";
                    ResourcesModel.ViewModel fieldModel = new ResourcesModel.ViewModel();
                    fieldModel.lang_code = "";
                    fieldModel.class_name = "";
                    fieldModel.key_name = "";
                    fieldModel.key_value = "";
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
        public JsonResult Insert(ResourcesModel.ViewModel fieldModel)
        {
            //GridData data = ResourcesModel.GetList(param);
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["lang_code"] = fieldModel.lang_code;
                    data["class_name"] = fieldModel.class_name;
                    data["key_name"] = fieldModel.key_name;
                    data["key_value"] = AntiXss.HtmlEncode(fieldModel.key_value);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = ResourcesModel.Insert(HttpContext, data);
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
        public IActionResult Edit(ResourcesModel.Pk Pk)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ConfigHelper.GetValue("ApllicationCode") + ":: " + ViewData["TitleHeader"];
                    ViewData["form_type"] = "Edit";
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    DataTable data = ResourcesModel.GetViewData(Pk);
                    ResourcesModel.ViewModel fieldModel = new ResourcesModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.lang_code = dr["lang_code"].ToString();
                        fieldModel.lang_code_old = dr["lang_code"].ToString();
                        fieldModel.class_name = dr["class_name"].ToString();
                        fieldModel.class_name_old = dr["class_name"].ToString();
                        fieldModel.key_name = dr["key_name"].ToString();
                        fieldModel.key_name_old = dr["key_name"].ToString();
                        fieldModel.key_value = dr["key_value"].ToString();
                    }
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
        public JsonResult Update(ResourcesModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    ResourcesModel.Pk Pk_old = new ResourcesModel.Pk();
                    Pk_old.lang_code = fieldModel.lang_code_old;
                    Pk_old.class_name = fieldModel.class_name_old;
                    Pk_old.key_name = fieldModel.key_name_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(ResourcesModel.GetData(Pk_old));

                    OrderedDictionary data = new OrderedDictionary();
                    data["lang_code"] = fieldModel.lang_code;
                    data["class_name"] = fieldModel.class_name;
                    data["key_name"] = fieldModel.key_name;
                    data["key_value"] = AntiXss.HtmlEncode(fieldModel.key_value);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        SqlHelper.ConvertEmptyValuesToDBNull(data);
                        result = ResourcesModel.Update(HttpContext, data, data_old);
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
        public JsonResult Delete(ResourcesModel.Pk Pk)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
                    OrderedDictionary data = DataModel.DtToDictionary(ResourcesModel.GetData(Pk));
                    if (data != null)
                    {
                        result = ResourcesModel.Delete(HttpContext, data);
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

        //UrlClearCache
        [HttpPost]
        public JsonResult ClearCache()
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
                    ResxHelper.ClearCahce();
                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "ResultInfo", "Informasi Hasil");
                    result.message = ResxHelper.GetValue("Message", "CahceHasBenDelete", "Data cahce telah berhasil dihapus");
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