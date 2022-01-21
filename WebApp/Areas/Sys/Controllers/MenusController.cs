using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Areas.Sys.Models;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Security.Application;

namespace WebApp.Areas.Sys.Controllers
{
    
    [Area("Sys")]
    public class MenusController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "MenuManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "MenuManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "MenuManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "MenuManage";
        private string _path_controler = "/Sys/Menus/";
        private string _path_view = "/Areas/Sys/Views/Menus/";
        private readonly string _table_name = "sys_mesus";
        private readonly string _table_title = "Menu";

        private readonly IHostingEnvironment _hostingEnvironment;
        public MenusController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

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
                    ViewData["pkKey"] = MenusModel._pkKey;
                    ViewData["displayItem"] = MenusModel.GetDisplayItem();
                    ViewData["column_att"] = MenusModel.GetGridColumn();
                    ViewData["param"] = MenusModel.GetListParam();
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
        public JsonResult GetListData(MenusModel.ActionRequest param)
        {
            GridData data = MenusModel.GetListData(param);
            return Json(data);
        }
        public IActionResult Index(MenusModel.ViewModel filterModel)
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
                    ViewBag.column_att = MenusModel.GetTreeColumn();
                    ViewData["param"] = MenusModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetTreeData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    if (filterModel.menu_type_id == null || filterModel.menu_type_id == "") {
                        filterModel.menu_type_id = "1";
                    }
                    ViewData["filterModel"] = filterModel;
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
        public JsonResult GetTreeData(MenusModel.ActionRequest param)
        {
            DataTable data = MenusModel.GetTreeData(param);
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
                    ViewData["Title"] = ResxHelper.GetValue(MenusModel._table_name, "ViewTitle", "Infromasi Detil Menus");
                    ViewData["TitleHeader"] = ResxHelper.GetValue(MenusModel._table_name, "ViewTitle", "Infromasi Detil Menus");
                    MenusModel.ViewModel fieldModel = new MenusModel.ViewModel();
                    fieldModel.id = "";
                    fieldModel.parent_id = "";
                    fieldModel.parent_id_text = "";
                    fieldModel.parent_id_old = "";
                    fieldModel.name = "";
                    fieldModel.label_id = "";
                    fieldModel.label_en = "";
                    fieldModel.link = "";
                    fieldModel.icon = "";
                    fieldModel.active_id = "";
                    fieldModel.active_id_text = "";
                    fieldModel.ordinal = "";
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    data = MenusModel.GetViewData(PkValue);
                    if (data != null)
                    {
                        foreach (DataRow dr in data.Rows)
                        {
                            fieldModel.id = dr["id"].ToString();
                            fieldModel.id_old = dr["id"].ToString();
                            fieldModel.parent_id = dr["parent_id"].ToString();
                            fieldModel.parent_id_text = dr["parent_id_text"].ToString();
                            fieldModel.name = dr["name"].ToString();
                            fieldModel.label_id = dr["label"].ToString();
                            fieldModel.label_en = dr["label_an"].ToString();
                            fieldModel.link = dr["link"].ToString();
                            fieldModel.icon = dr["icon"].ToString();
                            fieldModel.imgurl = dr["imgurl"].ToString();
                            fieldModel.active_id = dr["active_id"].ToString();
                            fieldModel.active_id_text = dr["active_id_text"].ToString();
                            fieldModel.ordinal = dr["ordinal"].ToString();
                            fieldModel.menu_type_id = dr["menu_type_id"].ToString();
                            fieldModel.menu_type_id_text = dr["menu_type_id_text"].ToString();
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
        public IActionResult Add(MenusModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["form_type"] = "Add";
                    ViewData["Title"] = ResxHelper.GetValue(MenusModel._table_name, "AddTitle", "Tambah Menus");
                    ViewData["TitleHeader"] = ResxHelper.GetValue(MenusModel._table_name, "AddTitle", "Tambah Menus");
                    ViewData["UrlAction"] = baseUrl+_path_controler+"Insert";
                    //MenusModel.ViewModel fieldModel = new MenusModel.ViewModel();
                    fieldModel.id = MenusModel.GetNewId().ToString();
                    //fieldModel.parent_id = parent_id;
                    fieldModel.parent_id_text = "";
                    fieldModel.parent_id_old = "";
                    fieldModel.name = "";
                    fieldModel.label_id = "";
                    fieldModel.label_en = "";
                    fieldModel.link = "";
                    fieldModel.icon = "";
                    fieldModel.imgurl = "";
                    fieldModel.active_id = "1";
                    fieldModel.active_id_text = "";
                    fieldModel.active_id0 = "";
                    fieldModel.active_id1 = "checked";
                    fieldModel.ordinal = MenusModel.GetLastOrdinalByParent(fieldModel.menu_type_id,fieldModel.parent_id).ToString();
                    fieldModel.rule_id = MenusModel.GetRuleId(fieldModel.id);
                    ViewData["fieldModel"] = fieldModel;
                    IList<FileHelper.initialFiles> initialFiles_imgurl = new List<FileHelper.initialFiles>();
                    ViewData["initialFiles_imgurl"] = JsonConvert.SerializeObject(initialFiles_imgurl);
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
        public JsonResult Insert(MenusModel.ViewModel fieldModel)
        {
            //GridData data = MenusModel.GetList(param);
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = MenusModel.GetNewId().ToString();
                    data["menu_type_id"] = fieldModel.menu_type_id;
                    data["parent_id"] = fieldModel.parent_id;
                    data["name"] = AntiXss.HtmlEncode(fieldModel.name);
                    data["label_id"] = AntiXss.HtmlEncode(fieldModel.label_id);
                    data["label_en"] = AntiXss.HtmlEncode(fieldModel.label_en);
                    data["link"] = fieldModel.link;
                    data["icon"] = fieldModel.icon;
                    data["imgurl"] = fieldModel.imgurl;
                    data["active_id"] = fieldModel.active_id;
                    data["ordinal"] = MenusModel.GetLastOrdinalByParent(fieldModel.menu_type_id,fieldModel.parent_id);
                    string rule_id = fieldModel.rule_id != null ? fieldModel.rule_id : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = MenusModel.Insert(HttpContext, data, rule_id);
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
                    ViewData["form_type"] = "Edit";
                    ViewData["Title"] = ResxHelper.GetValue(MenusModel._table_name, "AddTitle", "Tambah Menus");
                    ViewData["TitleHeader"] = ResxHelper.GetValue(MenusModel._table_name, "AddTitle", "Tambah Menus");
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = MenusModel.GetViewData(PkValue);
                    MenusModel.ViewModel fieldModel = new MenusModel.ViewModel();
                    IList<FileHelper.initialFiles> initialFiles_imgurl = new List<FileHelper.initialFiles>();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.parent_id = dr["parent_id"].ToString();
                        fieldModel.parent_id_text = dr["parent_id_text"].ToString();
                        fieldModel.name = dr["name"].ToString();
                        fieldModel.label_id = dr["label_id"].ToString();
                        fieldModel.label_en = dr["label_en"].ToString();
                        fieldModel.link = dr["link"].ToString();
                        fieldModel.icon = dr["icon"].ToString();
                        fieldModel.imgurl = dr["imgurl"].ToString();
                        fieldModel.active_id = dr["active_id"].ToString();
                        fieldModel.active_id_text = dr["active_id_text"].ToString();
                        fieldModel.active_id0 = dr["active_id"].ToString() == "0" ? "checked" : "";
                        fieldModel.active_id1 = dr["active_id"].ToString() == "1" ? "checked" : "";
                        fieldModel.ordinal = dr["ordinal"].ToString();
                        fieldModel.menu_type_id = dr["menu_type_id"].ToString();
                        fieldModel.menu_type_id_text = dr["menu_type_id_text"].ToString();
                        fieldModel.rule_id = MenusModel.GetRuleId(fieldModel.id);
                        
                        if (fieldModel.imgurl != "") {

                            string pathSource = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                            pathSource = Path.Combine(pathSource, "menu");
                            string file_source = Path.Combine(pathSource, fieldModel.imgurl);

                            if (System.IO.File.Exists(file_source)) {
                                string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
                                string pathSession = Path.Combine(upload_temp, HttpContext.Session.Id);
                                FileHelper.CreateDirectoryRecursively(pathSession);
                                string file_Dest = Path.Combine(pathSession, fieldModel.imgurl);
                                if (System.IO.File.Exists(file_Dest)) { System.IO.File.Delete(file_Dest); }
                                System.IO.File.Copy(file_source, file_Dest);

                                FileInfo fi = new FileInfo(file_Dest);
                                FileHelper.initialFiles initialFiles = new FileHelper.initialFiles();
                                initialFiles.name = fi.Name;
                                initialFiles.extension = fi.Extension;
                                initialFiles.size = fi.Length;
                                initialFiles_imgurl.Add(initialFiles);
                            }
                        }
                    }
                    ViewData["initialFiles_imgurl"] = JsonConvert.SerializeObject(initialFiles_imgurl);
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
        public JsonResult Update(MenusModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = fieldModel.id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(MenusModel.GetData(PkValue));
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = fieldModel.id;
                    data["menu_type_id"] = fieldModel.menu_type_id;
                    data["parent_id"] = fieldModel.parent_id;
                    data["name"] = AntiXss.HtmlEncode(fieldModel.name);
                    data["label_id"] = AntiXss.HtmlEncode(fieldModel.label_id);
                    data["label_en"] = AntiXss.HtmlEncode(fieldModel.label_en);
                    data["link"] = fieldModel.link;
                    data["icon"] = fieldModel.icon;
                    data["imgurl"] = fieldModel.imgurl;
                    data["active_id"] = fieldModel.active_id;
                    data["ordinal"] = fieldModel.ordinal;

                    OrderedDictionary rule_old = new OrderedDictionary();
                    OrderedDictionary rule_new = new OrderedDictionary();
                    rule_old["rule_id"] = MenusModel.GetRuleId(fieldModel.id_old);
                    string rule_id = fieldModel.rule_id != null ? fieldModel.rule_id : "";
                    rule_new["rule_id"] = rule_id;
                    if (DataModel.HasUpdate(data, data_old) || DataModel.HasUpdate(rule_new, rule_old))
                    {
                        SqlHelper.ConvertEmptyValuesToDBNull(data);
                        SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                        string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
                        string pathSession = Path.Combine(upload_temp, HttpContext.Session.Id);
                        //string pathSource = Path.Combine(pathSession, fieldModel.imgurl);
                        string pathDest = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                        pathDest = Path.Combine(pathDest, "menu");
                        if (fieldModel.imgurl != null && fieldModel.imgurl != "")
                        {
                            string pathSource = Path.Combine(pathSession, fieldModel.imgurl);
                            if (System.IO.File.Exists(pathSource)) {
                                string[] file_names = fieldModel.imgurl.Split('.');
                                string ext = file_names[file_names.Length - 1];
                                string new_file_name = "menu_" + data["id"].ToString() + "." + ext;
                                string file_Dest = Path.Combine(pathDest, new_file_name);
                                data["imgurl"] = new_file_name;
                                if (System.IO.File.Exists(file_Dest))
                                {
                                    System.IO.File.Delete(file_Dest);
                                }
                                System.IO.File.Copy(pathSource, file_Dest);
                                Directory.Delete(pathSession, true);
                            }
                        }
                        else {
                            if (data_old["imgurl"] != null && data_old["imgurl"].ToString() != "")
                            {
                                string file_old = Path.Combine(pathDest, data_old["imgurl"].ToString());
                                if (System.IO.File.Exists(file_old))
                                {
                                    System.IO.File.Delete(file_old);
                                }
                            }
                            data["imgurl"] = "";
                        }
                        result = MenusModel.Update(HttpContext, data, data_old, rule_id);
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
                    OrderedDictionary data = DataModel.DtToDictionary(MenusModel.GetData(PkValue));
                    if (data != null)
                    {
                        result = MenusModel.hasChild(id);
                        if (result.status != 1)
                        {
                            return Json(result);
                        }
                        else
                        {
                            SqlHelper.ConvertEmptyValuesToDBNull(data);
                            result = MenusModel.Delete(HttpContext, data);
                            return Json(result);
                        }
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
        public JsonResult LookupTreeData(lookup_param param)
        {
            var data = MenusModel.LookupTreeData(param);
            return Json(data);
        }
        //GetListOrderByParent
        [HttpPost]
        public JsonResult LookupOrderByParent(lookup_param param)
        {
            var data = MenusModel.LookupOrderByParent(param);
            return Json(data);
        }
        //
        [HttpPost]
        public JsonResult LookupMenuType(lookup_param param)
        {
            var data = MenusModel.LookupMenuType(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult getItemRuleTreeCheck(paramTreeMenucheck param)
        {

            IList<lookup_tree_check> data = MenusModel.getItemRuleTreeCheck(param.menuid);
            //string hasil = JsonConvert.SerializeObject(data).Replace("check", "checked");
            return Json(data);

        }

        public async Task<IActionResult> save_imgurl(IEnumerable<IFormFile> imgurl)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            if (imgurl != null)
            {
                foreach (var file in imgurl)
                {
                    string pathData = Path.Combine(webRootPath, "img");
                    if (!Directory.Exists(pathData)){Directory.CreateDirectory(pathData);}
                    pathData = Path.Combine(pathData, "menu");
                    if (!Directory.Exists(pathData)) { Directory.CreateDirectory(pathData); }
                    // Some browsers send file names with full path. This needs to be stripped.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(pathData, fileName);
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }
    }
    public class paramTreeMenucheck
    {
        public string menuid { get; set; }
    }
}