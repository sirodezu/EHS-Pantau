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
    public class UsersController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "UserManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "UserManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "UserManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "UserManage";
        private string _path_controler = "/Sys/Users/";
        private string _path_view = "/Areas/Sys/Views/Users/";
        private readonly string _table_name = "sys_users";
        private readonly string _table_title = "User";

        public IActionResult Index()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string curUserid = SecurityHelper.CurrentUserId(HttpContext);
                    ViewData["curUserid"] = curUserid;
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = UsersModel._pkKey;
                    ViewData["displayItem"] = UsersModel.GetDisplayItem();
                    ViewData["column_att"] = UsersModel.GetGridColumn();
                    ViewData["param"] = UsersModel.GetListParam();
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
        public JsonResult GetListData(UsersModel.ActionRequest param)
        {
            GridData data = UsersModel.GetListData(param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string userid)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    UsersModel.ViewModel fieldModel = new UsersModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (userid != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["userid"] = userid;
                        data = UsersModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.userid = dr["userid"].ToString();
                                fieldModel.domain_name = dr["domain_name"].ToString();
                                fieldModel.username = dr["username"].ToString();
                                fieldModel.fullname = dr["fullname"].ToString();
                                fieldModel.password = dr["password"].ToString();
                                fieldModel.old_password = dr["old_password"].ToString();
                                fieldModel.email = dr["email"].ToString();
                                fieldModel.alternative_email = dr["alternative_email"].ToString();
                                fieldModel.allow_concurrent_login = dr["allow_concurrent_login"].ToString();
                                fieldModel.has_change_password = dr["has_change_password"].ToString();
                                fieldModel.enable_change_password = dr["enable_change_password"].ToString();
                                fieldModel.last_change_password = dr["last_change_password"].ToString();
                                fieldModel.enable_password_expired = dr["enable_password_expired"].ToString();
                                fieldModel.password_expired_remainder = dr["password_expired_remainder"].ToString();
                                fieldModel.attemp_count = dr["attemp_count"].ToString();
                                fieldModel.attemp_time = dr["attemp_time"].ToString();
                                fieldModel.user_expired = dr["user_expired"].ToString();
                                fieldModel.must_change_password = dr["must_change_password"].ToString();
                                fieldModel.login_count = dr["login_count"].ToString();
                                fieldModel.last_login = dr["last_login"].ToString();
                                fieldModel.block = "0";// dr["block"].ToString();
                                fieldModel.activation = dr["activation"].ToString();
                                fieldModel.code_activation = dr["code_activation"].ToString();
                                fieldModel.code_forget_password = dr["code_forget_password"].ToString();
                                fieldModel.parameter = dr["parameter"].ToString();
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
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    UsersModel.ViewModel fieldModel = new UsersModel.ViewModel();
                    fieldModel.userid = UsersModel.GetNewId("").ToString();
                    fieldModel.block = "0";
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
        [HttpGet]
        public IActionResult Copy(string userid)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    var PkValue = new OrderedDictionary();
                    PkValue["userid"] = userid;
                    DataTable data = UsersModel.GetViewData(PkValue);
                    UsersModel.ViewModel fieldModel = new UsersModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.userid = UsersModel.GetNewId(dr["username"].ToString()).ToString();
                        fieldModel.userid_old = fieldModel.userid;
                        fieldModel.domain_name = dr["domain_name"].ToString();
                        fieldModel.username = dr["username"].ToString();
                        fieldModel.fullname = dr["fullname"].ToString();
                        fieldModel.password = dr["password"].ToString();
                        fieldModel.old_password = dr["old_password"].ToString();
                        fieldModel.email = dr["email"].ToString();
                        fieldModel.alternative_email = dr["alternative_email"].ToString();
                        fieldModel.allow_concurrent_login = dr["allow_concurrent_login"].ToString();
                        fieldModel.has_change_password = dr["has_change_password"].ToString();
                        fieldModel.enable_change_password = dr["enable_change_password"].ToString();
                        fieldModel.last_change_password = dr["last_change_password"].ToString();
                        fieldModel.enable_password_expired = dr["enable_password_expired"].ToString();
                        fieldModel.password_expired_remainder = dr["password_expired_remainder"].ToString();
                        fieldModel.attemp_count = dr["attemp_count"].ToString();
                        fieldModel.attemp_time = dr["attemp_time"].ToString();
                        fieldModel.user_expired = dr["user_expired"].ToString();
                        fieldModel.must_change_password = dr["must_change_password"].ToString();
                        fieldModel.login_count = dr["login_count"].ToString();
                        fieldModel.last_login = dr["last_login"].ToString();
                        fieldModel.block = dr["block"].ToString();
                        fieldModel.activation = dr["activation"].ToString();
                        fieldModel.code_activation = dr["code_activation"].ToString();
                        fieldModel.code_forget_password = dr["code_forget_password"].ToString();
                        fieldModel.parameter = dr["parameter"].ToString();
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
        [HttpGet]
        public IActionResult Edit(string userid)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["userid"] = userid;
                    DataTable data = UsersModel.GetViewData(PkValue);
                    UsersModel.ViewModel fieldModel = new UsersModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.userid = dr["userid"].ToString();
                        fieldModel.userid_old = dr["userid"].ToString();
                        fieldModel.domain_name = dr["domain_name"].ToString();
                        fieldModel.username = dr["username"].ToString();
                        fieldModel.fullname = dr["fullname"].ToString();
                        fieldModel.password = dr["password"].ToString();
                        fieldModel.old_password = dr["old_password"].ToString();
                        fieldModel.email = dr["email"].ToString();
                        fieldModel.alternative_email = dr["alternative_email"].ToString();
                        fieldModel.allow_concurrent_login = dr["allow_concurrent_login"].ToString();
                        fieldModel.has_change_password = dr["has_change_password"].ToString();
                        fieldModel.enable_change_password = dr["enable_change_password"].ToString();
                        fieldModel.last_change_password = dr["last_change_password"].ToString();
                        fieldModel.enable_password_expired = dr["enable_password_expired"].ToString();
                        fieldModel.password_expired_remainder = dr["password_expired_remainder"].ToString();
                        fieldModel.attemp_count = dr["attemp_count"].ToString();
                        fieldModel.attemp_time = dr["attemp_time"].ToString();
                        fieldModel.user_expired = dr["user_expired"].ToString();
                        fieldModel.must_change_password = dr["must_change_password"].ToString();
                        fieldModel.login_count = dr["login_count"].ToString();
                        fieldModel.last_login = dr["last_login"].ToString();
                        fieldModel.block = dr["block"].ToString();
                        fieldModel.activation = dr["activation"].ToString();
                        fieldModel.code_activation = dr["code_activation"].ToString();
                        fieldModel.code_forget_password = dr["code_forget_password"].ToString();
                        fieldModel.parameter = dr["parameter"].ToString();
                        fieldModel.group_id = UsersModel.GetGroupId(PkValue);

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
        public JsonResult Insert(UsersModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["userid"] = UsersModel.GetNewId(fieldModel.username).ToString();
                    //data["domain_name"] = fieldModel.domain_name;
                    data["username"] = fieldModel.username;
                    data["fullname"] = fieldModel.fullname;
                    data["password"] = fieldModel.password != null && fieldModel.password.ToString() != "" ? SecurityHelper.Encript(fieldModel.password.ToString()) : "";
                    
                    //data["old_password"] = fieldModel.old_password;
                    //data["email"] = fieldModel.email;
                    //data["alternative_email"] = fieldModel.alternative_email;
                    //data["allow_concurrent_login"] = fieldModel.allow_concurrent_login;
                    //data["has_change_password"] = fieldModel.has_change_password;
                    //data["enable_change_password"] = fieldModel.enable_change_password;
                    //data["last_change_password"] = fieldModel.last_change_password;
                    //data["enable_password_expired"] = fieldModel.enable_password_expired;
                    //data["password_expired_remainder"] = fieldModel.password_expired_remainder;
                    //data["attemp_count"] = fieldModel.attemp_count;
                    //data["attemp_time"] = fieldModel.attemp_time;
                    //data["user_expired"] = fieldModel.user_expired;
                    //data["must_change_password"] = fieldModel.must_change_password;
                    //data["login_count"] = fieldModel.login_count;
                    //data["last_login"] = fieldModel.last_login;
                    data["block"] = fieldModel.block;
                    //data["activation"] = fieldModel.activation;
                    //data["code_activation"] = fieldModel.code_activation;
                    //data["code_forget_password"] = fieldModel.code_forget_password;
                    //data["parameter"] = fieldModel.parameter;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    result = UsersModel.Insert(HttpContext, data);
                    string group_id = fieldModel.group_id;

                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    UsersModel.UpdateGroup(HttpContext, data["userid"].ToString(), group_id);
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
        public JsonResult Update(UsersModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string userid_old = fieldModel.userid_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["userid"] = userid_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(UsersModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(UsersModel.GetData(PkValue));
                    data["userid"] = fieldModel.userid;
                    data["domain_name"] = fieldModel.domain_name;
                    data["username"] = fieldModel.username;
                    data["fullname"] = fieldModel.fullname;
                    if (fieldModel.password != null && fieldModel.password.ToString() != "") {
                        data["password"] = SecurityHelper.Encript(fieldModel.password.ToString());
                    }
                    //data["old_password"] = fieldModel.old_password;
                    data["email"] = fieldModel.email;
                    //data["alternative_email"] = fieldModel.alternative_email;
                    //data["allow_concurrent_login"] = fieldModel.allow_concurrent_login;
                    //data["has_change_password"] = fieldModel.has_change_password;
                    //data["enable_change_password"] = fieldModel.enable_change_password;
                    //data["last_change_password"] = fieldModel.last_change_password;
                    //data["enable_password_expired"] = fieldModel.enable_password_expired;
                    //data["password_expired_remainder"] = fieldModel.password_expired_remainder;
                    //data["attemp_count"] = fieldModel.attemp_count;
                    //data["attemp_time"] = fieldModel.attemp_time;
                    //data["user_expired"] = fieldModel.user_expired;
                    //data["must_change_password"] = fieldModel.must_change_password;
                    //data["login_count"] = fieldModel.login_count;
                    //data["last_login"] = fieldModel.last_login;
                    data["block"] = fieldModel.block;
                    //data["activation"] = fieldModel.activation;
                    //data["code_activation"] = fieldModel.code_activation;
                    //data["code_forget_password"] = fieldModel.code_forget_password;
                    //data["parameter"] = fieldModel.parameter;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);

                    OrderedDictionary data_group_old = new OrderedDictionary();
                    OrderedDictionary data_group = new OrderedDictionary();
                    string group_id_old = UsersModel.GetGroupId(PkValue).ToString();
                    string group_id = fieldModel.group_id.ToString();

                    bool is_data_update = DataModel.HasUpdate(data, data_old);
                    bool is_group_update = group_id != group_id_old;
                    if (is_data_update || is_group_update)
                    {
                        if (is_data_update) {
                            data["insert_by"] = data_old["insert_by"];
                            data["insert_at"] = data_old["insert_at"];
                            data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                            data["update_at"] = DateTime.Now;
                            result = UsersModel.Update(HttpContext, data, data_old);
                        }
                        if (is_group_update)
                        {
                            result = UsersModel.UpdateGroup(HttpContext, data["userid"].ToString(), group_id);
                        }
                    }
                    else
                    {
                        result.status = 2;
                        result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                        result.message = ResxHelper.GetValue("Message", "NoRecodeUpdate");
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
        public JsonResult Delete(string userid)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
                    var PkValue = new OrderedDictionary();
                    PkValue["userid"] = userid;
                    OrderedDictionary data = DataModel.DtToDictionary(UsersModel.GetData(PkValue));
                    if (data != null)
                    {
                        result = UsersModel.Delete(HttpContext, data);
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
            DataTable data = UsersModel.LookupData(param);
            return Json(data);
        }

        public IActionResult Takeover()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, "TakeoverUser"))
                {
                    string curUserid = SecurityHelper.CurrentUserId(HttpContext);
                    ViewData["curUserid"] = curUserid;
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = UsersModel._pkKey;
                    ViewData["displayItem"] = UsersModel.GetDisplayItem();
                    ViewData["column_att"] = UsersModel.GetGridColumn();
                    ViewData["param"] = UsersModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlTakeOver"] = baseUrl + _path_controler + "TakeOverGo";
                    ViewData["AllowView"] = HttpContext.Session.GetString(_rule_view) != null ? "1" : "0";
                    ViewData["AllowAdd"] = HttpContext.Session.GetString(_rule_add) != null ? "1" : "0";
                    ViewData["AllowEdit"] = HttpContext.Session.GetString(_rule_edit) != null ? "1" : "0";
                    ViewData["AllowDelete"] = HttpContext.Session.GetString(_rule_delete) != null ? "1" : "0";
                    return View(_path_view + "Takeover.cshtml");
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
        public JsonResult TakeOverGo(string userid)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, "TakeoverUser"))
                {
                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                    result.message = ResxHelper.GetValue("Message", "NoRecodeFound");
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