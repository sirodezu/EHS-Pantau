using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Areas.Sys.Models;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;

namespace WebApp.Areas.Sys.Controllers
{
    
    [Area("Sys")]
    public class RulesController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RuleManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RuleManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RuleManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RuleManage";
        private string _path_controler = "/Sys/Rules/";
        private string _path_view = "/Areas/Sys/Views/Rules/";
        private readonly string _table_name = "sys_rules";
        private readonly string _table_title = "Kewenangan";
        public IActionResult Tabular()
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
                    ViewData["pkKey"] = RulesModel._pkKey;
                    ViewData["displayItem"] = RulesModel.GetDisplayItem();
                    ViewData["column_att"] = RulesModel.GetTreeColumn();
                    ViewData["param"] = RulesModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    ViewData["AllowView"] = HttpContext.Session.GetString(_rule_view) != null ? "1" : "0";
                    ViewData["AllowAdd"] = HttpContext.Session.GetString(_rule_add) != null ? "1" : "0";
                    ViewData["AllowEdit"] = HttpContext.Session.GetString(_rule_edit) != null ? "1" : "0";
                    ViewData["AllowDelete"] = HttpContext.Session.GetString(_rule_delete) != null ? "1" : "0";
                    return View(_path_view + "Tabular.cshtml");
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
        public JsonResult GetListData(RulesModel.ActionRequest param)
        {
            GridData data = RulesModel.GetListData(param);
            return Json(data);
        }
        public IActionResult Index()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    IList<GridColumn> column_att = RulesModel.GetTreeColumn();
                    ViewData["column_att"] = column_att;
                    ViewBag.column_att = column_att;
                    ParamGrid param = RulesModel.GetListParam();
                    ViewData["param"] = param;
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetTreeData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
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
        public JsonResult GetTreeData(RulesModel.ActionRequest param)
        {
            DataTable data = RulesModel.GetTreeData(param);
            return Json(data);
        }

        [HttpGet]
        public IActionResult ViewItem(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["Title"] = ResxHelper.GetValue(RulesModel._table_name, "ViewTitle", "Infromasi Detil Rules");
                    ViewData["TitleHeader"] = ResxHelper.GetValue(RulesModel._table_name, "ViewTitle", "Infromasi Detil Rules");
                    RulesModel.ViewModel fieldModel = new RulesModel.ViewModel();
                    fieldModel.id = "";
                    fieldModel.parent_id = "";
                    fieldModel.parent_id_text = "";
                    fieldModel.parent_id_old = "";
                    fieldModel.nama = "";
                    fieldModel.keterangan = "";
                    fieldModel.aktif_id = "";
                    fieldModel.aktif_id_text = "";
                    fieldModel.ordinal = "";
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    data = RulesModel.GetViewData(PkValue);
                    if (data != null)
                    {
                        foreach (DataRow dr in data.Rows)
                        {
                            fieldModel.id = dr["id"].ToString();
                            fieldModel.id_old = dr["id"].ToString();
                            fieldModel.parent_id = dr["parent_id"].ToString();
                            fieldModel.parent_id_text = dr["parent_id_text"].ToString();
                            fieldModel.kode = dr["kode"].ToString();
                            fieldModel.nama = dr["nama"].ToString();
                            fieldModel.keterangan = dr["keterangan"].ToString();
                            fieldModel.aktif_id = dr["aktif_id"].ToString();
                            fieldModel.aktif_id_text = dr["aktif_id_text"].ToString();
                            fieldModel.ordinal = dr["ordinal"].ToString();
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
        public IActionResult Add(string parent_id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["form_type"] = "Add";
                    ViewData["Title"] = ResxHelper.GetValue(RulesModel._table_name, "AddTitle", "Tambah Rules");
                    ViewData["TitleHeader"] = ResxHelper.GetValue(RulesModel._table_name, "AddTitle", "Tambah Rules");
                    //ViewData["UrlAction"] = Request.Scheme+ "//"+Request.Host+""+Request.PathBase+ "/Rules/Insert";
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    RulesModel.ViewModel fieldModel = new RulesModel.ViewModel();
                    fieldModel.id = RulesModel.GetNewId().ToString();
                    fieldModel.parent_id = parent_id;
                    fieldModel.parent_id_text = "";
                    fieldModel.parent_id_old = "";
                    fieldModel.nama = "";
                    fieldModel.keterangan = "";
                    fieldModel.aktif_id = "1";
                    fieldModel.aktif_id_text = "";
                    fieldModel.aktif_id0 = "";
                    fieldModel.aktif_id1 = "checked";
                    fieldModel.ordinal = RulesModel.GetLastOrdinalByParent(parent_id).ToString();
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
        public JsonResult Insert(RulesModel.ViewModel fieldModel)
        {
            //GridData data = RulesModel.GetList(param);
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = RulesModel.GetNewId().ToString();
                    data["parent_id"] = fieldModel.parent_id;
                    data["kode"] = fieldModel.kode;
                    data["nama"] = fieldModel.nama;
                    data["keterangan"] = fieldModel.keterangan;
                    data["aktif_id"] = fieldModel.aktif_id;
                    data["ordinal"] = RulesModel.GetLastOrdinalByParent(fieldModel.parent_id);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = RulesModel.Insert(HttpContext, data);
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
                    ViewData["form_type"] = "Edit";
                    ViewData["Title"] = ResxHelper.GetValue(RulesModel._table_name, "AddTitle", "Tambah Rules");
                    ViewData["TitleHeader"] = ResxHelper.GetValue(RulesModel._table_name, "AddTitle", "Tambah Rules");
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";

                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = RulesModel.GetViewData(PkValue);
                    RulesModel.ViewModel fieldModel = new RulesModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.parent_id = dr["parent_id"].ToString();
                        fieldModel.parent_id_text = dr["parent_id_text"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.aktif_id = dr["aktif_id"].ToString();
                        fieldModel.aktif_id_text = dr["aktif_id_text"].ToString();
                        fieldModel.aktif_id0 = dr["aktif_id"].ToString() == "0" ? "checked" : "";
                        fieldModel.aktif_id1 = dr["aktif_id"].ToString() == "1" ? "checked" : "";
                        fieldModel.ordinal = dr["ordinal"].ToString();
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
        public JsonResult Update(RulesModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = fieldModel.id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(RulesModel.GetData(PkValue));
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = fieldModel.id;
                    data["parent_id"] = fieldModel.parent_id;
                    data["kode"] = fieldModel.kode;
                    data["nama"] = fieldModel.nama;
                    data["keterangan"] = fieldModel.keterangan;
                    data["aktif_id"] = fieldModel.aktif_id;
                    data["ordinal"] = fieldModel.ordinal;
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        SqlHelper.ConvertEmptyValuesToDBNull(data);
                        SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                        result = RulesModel.Update(HttpContext, data, data_old);
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
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    OrderedDictionary data = DataModel.DtToDictionary(RulesModel.GetData(PkValue));
                    if (data != null)
                    {
                        SqlHelper.ConvertEmptyValuesToDBNull(data);
                        result = RulesModel.Delete(HttpContext, data);
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
        public JsonResult LookupTreeData()
        {
            var data = RulesModel.LookupTreeData();
            return Json(data);
        }
        //GetListOrderByParent
        [HttpPost]
        public JsonResult LookupOrderByParent(lookup_param param)
        {
            var data = RulesModel.LookupOrderByParent(param);
            return Json(data);
        }
    }
}