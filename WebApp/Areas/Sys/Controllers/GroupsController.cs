using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Areas.Sys.Models;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System;

namespace WebApp.Areas.Sys.Controllers
{
    
    [Area("Sys")]
    public class GroupsController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "GroupManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "GroupManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "GroupManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "GroupManage";
        private string _path_controler = "/Sys/Groups/";
        private string _path_view = "/Areas/Sys/Views/Groups/";
        private readonly string _table_name = "sys_groups";
        private readonly string _table_title = "Grup User";
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
                    ViewData["pkKey"] = GroupsModel._pkKey;
                    ViewData["displayItem"] = GroupsModel.GetDisplayItem();
                    ViewData["column_att"] = GroupsModel.GetGridColumn();
                    ViewData["param"] = GroupsModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
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
        public JsonResult GetListData(GroupsModel.ActionRequest param)
        {
            GridData data = GroupsModel.GetListData(param);
            return Json(data);
        }
        public IActionResult TreeList()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    IList<GridColumn> column_att = GroupsModel.GetGridColumn();
                    ViewData["column_att"] = column_att;
                    ViewBag.column_att = column_att;
                    ParamGrid param = GroupsModel.GetListParam();
                    ViewData["param"] = param;
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetTreeData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    return View(_path_view + "Tree.cshtml");

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
        public JsonResult GetTreeData(GroupsModel.ActionRequest param)
        {
            DataTable data = GroupsModel.GetTreeData(param);
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
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    GroupsModel.ViewModel fieldModel = new GroupsModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    data = GroupsModel.GetViewData(PkValue);
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
                            fieldModel.role_id = dr["role_id"].ToString();
                            fieldModel.role_id_text = dr["role_id_text"].ToString();
                            fieldModel.ba_id = dr["ba_id"].ToString();
                            fieldModel.ba_id_text = dr["ba_id_text"].ToString();
                            fieldModel.pa_id = dr["pa_id"].ToString();
                            fieldModel.pa_id_text = dr["pa_id_text"].ToString();
                            fieldModel.psa_id = dr["psa_id"].ToString();
                            fieldModel.psa_id_text = dr["psa_id_text"].ToString();
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
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["form_type"] = "Add";
                    ViewData["UrlAction"] = baseUrl+_path_controler + "Insert";
                    GroupsModel.ViewModel fieldModel = new GroupsModel.ViewModel();
                    fieldModel.id = GroupsModel.GetNewId().ToString();
                    fieldModel.parent_id = parent_id;
                    fieldModel.parent_id_text = "";
                    fieldModel.parent_id_old = "";
                    fieldModel.kode = "";
                    fieldModel.nama = "";
                    fieldModel.keterangan = "";
                    fieldModel.aktif_id = "1";
                    fieldModel.aktif_id_text = "";
                    fieldModel.aktif_id0 = "";
                    fieldModel.aktif_id1 = "checked";
                    fieldModel.ordinal = GroupsModel.GetLastOrdinalByParent(parent_id).ToString();
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
        public JsonResult Insert(GroupsModel.ViewModel fieldModel)
        {
            //GridData data = GroupsModel.GetList(param);
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = GroupsModel.GetNewId().ToString();
                    data["parent_id"] = fieldModel.parent_id;
                    data["kode"] = fieldModel.kode;
                    data["nama"] = fieldModel.nama;
                    data["role_id"] = fieldModel.role_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["keterangan"] = fieldModel.keterangan;
                    data["aktif_id"] = fieldModel.aktif_id;
                    data["ordinal"] = GroupsModel.GetLastOrdinalByParent(fieldModel.parent_id);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = GroupsModel.Insert(HttpContext, data);
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
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ConfigHelper.GetValue("ApllicationCode") + ":: " + ViewData["TitleHeader"];
                    ViewData["form_type"] = "Edit";
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = GroupsModel.GetViewData(PkValue);
                    GroupsModel.ViewModel fieldModel = new GroupsModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.parent_id = dr["parent_id"].ToString();
                        fieldModel.parent_id_text = dr["parent_id_text"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.role_id = dr["role_id"].ToString();
                        fieldModel.role_id_text = dr["role_id_text"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.ba_id_text = dr["ba_id_text"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.pa_id_text = dr["pa_id_text"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.psa_id_text = dr["psa_id_text"].ToString();
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
        public JsonResult Update(GroupsModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = fieldModel.id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(GroupsModel.GetData(PkValue));
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = fieldModel.id;
                    data["parent_id"] = fieldModel.parent_id;
                    data["kode"] = fieldModel.kode;
                    data["nama"] = fieldModel.nama;
                    data["role_id"] = fieldModel.role_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["keterangan"] = fieldModel.keterangan;
                    data["aktif_id"] = fieldModel.aktif_id;
                    data["ordinal"] = fieldModel.ordinal;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["insert_by"] = data_old["insert_by"];
                        data["insert_at"] = data_old["insert_at"];
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = GroupsModel.Update(HttpContext, data, data_old);
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
                    OrderedDictionary data = DataModel.DtToDictionary(GroupsModel.GetData(PkValue));
                    if (data != null)
                    {
                        SqlHelper.ConvertEmptyValuesToDBNull(data);
                        result = GroupsModel.Delete(HttpContext, data);
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
        public JsonResult LookupData(lookup_param param)
        {
            DataTable data = GroupsModel.LookupData(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult LookupTreeData()
        {
            var data = GroupsModel.LookupTreeData();
            return Json(data);
        }
        //GetListOrderByParent
        [HttpPost]
        public JsonResult LookupOrderByParent(lookup_param param)
        {
            var data = GroupsModel.LookupOrderByParent(param);
            return Json(data);
        }
    }
}