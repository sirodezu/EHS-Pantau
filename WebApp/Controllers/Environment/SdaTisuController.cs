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
    
    public class SdaTisuController : Controller
    {
        private string _rule_view = "EnvironmentView";
        private string _rule_add = "EnvironmentAdd";
        private string _rule_edit = "EnvironmentEdit";
        private string _rule_delete = "EnvironmentDelete";
        private string _rule_edit_all = "EnvironmentEditAll";
        private string _rule_delete_all = "EnvironmentDeleteAll";
        private string _path_controler = "/SdaTisu/";
        private string _path_view = "/Views/Environment/SdaTisu/";
        private readonly string _table_name = "ta_sda_tisu";
        private readonly string _table_title = "Sumber Daya Alam - Tisu";

		private static IHostingEnvironment _hostingEnvironment;
        public SdaTisuController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(SdaTisuModel.ViewModel filterColumn)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = SdaTisuModel._pkKey;
                    ViewData["displayItem"] = SdaTisuModel.GetDisplayItem();
                    ViewData["column_att"] = SdaTisuModel.GetGridColumn();
                    ViewData["param"] = SdaTisuModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1":"0";
                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";
                    ViewData["AllowEditAll"] = SecurityHelper.HasRule(HttpContext, _rule_edit_all) ? "1" : "0";
                    ViewData["AllowDeleteAll"] = SecurityHelper.HasRule(HttpContext, _rule_delete_all) ? "1" : "0";
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
        public JsonResult GetListData(SdaTisuModel.ActionRequest param)
        {
            GridData data = SdaTisuModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["usaha_pengurangan_tisu_desc_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "usaha_pengurangan_tisu_desc_file_path", data.data.Rows[i]["usaha_pengurangan_tisu_desc_file_path"].ToString());
            }

            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    SdaTisuModel.ViewModel fieldModel = new SdaTisuModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = SdaTisuModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                                fieldModel.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                                fieldModel.ba_id = dr["ba_id"].ToString();
                                fieldModel.ba_id_text = dr["ba_id_text"].ToString();
                                fieldModel.pa_id = dr["pa_id"].ToString();
                                fieldModel.pa_id_text = dr["pa_id_text"].ToString();
                                fieldModel.psa_id = dr["psa_id"].ToString();
                                fieldModel.psa_id_text = dr["psa_id_text"].ToString();
                                fieldModel.bulan = dr["bulan"].ToString();
                                fieldModel.bulan_text = dr["bulan_text"].ToString();
                                fieldModel.tahun = dr["tahun"].ToString();
                                fieldModel.tahun_text = dr["tahun_text"].ToString();
                                fieldModel.konsumsi_tisu = String.Format("{0:N2}", dr["konsumsi_tisu"]);
                                fieldModel.usaha_pengurangan_tisu_id = dr["usaha_pengurangan_tisu_id"].ToString();
                                fieldModel.usaha_pengurangan_tisu_id_text = dr["usaha_pengurangan_tisu_id_text"].ToString();
                                fieldModel.usaha_pengurangan_tisu_desc = dr["usaha_pengurangan_tisu_desc"].ToString();
                                fieldModel.usaha_pengurangan_tisu_desc_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "usaha_pengurangan_tisu_desc_file_path", dr["usaha_pengurangan_tisu_desc_file_path"].ToString());
                                fieldModel.usaha_pengurangan_tisu_jumlah = String.Format("{0:N2}", dr["usaha_pengurangan_tisu_jumlah"]);
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1" : "0";
                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";
                    ViewData["AllowEditAll"] = SecurityHelper.HasRule(HttpContext, _rule_edit_all) ? "1" : "0";
                    ViewData["AllowDeleteAll"] = SecurityHelper.HasRule(HttpContext, _rule_delete_all) ? "1" : "0";
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
        public IActionResult Add(SdaTisuModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    //SdaTisuModel.ViewModel fieldModel = new SdaTisuModel.ViewModel();
                    fieldModel.id = SdaTisuModel.GetNewId().ToString();
                    fieldModel.usaha_pengurangan_tisu_desc_file_path_init = "[]";
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
        public IActionResult Copy(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = SdaTisuModel.GetViewData(PkValue);
                    SdaTisuModel.ViewModel fieldModel = new SdaTisuModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = SdaTisuModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.bulan = dr["bulan"].ToString();
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.konsumsi_tisu = dr["konsumsi_tisu"].ToString();
                        fieldModel.usaha_pengurangan_tisu_id = dr["usaha_pengurangan_tisu_id"].ToString();
                        fieldModel.usaha_pengurangan_tisu_desc = dr["usaha_pengurangan_tisu_desc"].ToString();
                        fieldModel.usaha_pengurangan_tisu_desc_file_path = dr["usaha_pengurangan_tisu_desc_file_path"].ToString();
                        fieldModel.usaha_pengurangan_tisu_jumlah = dr["usaha_pengurangan_tisu_jumlah"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
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
        public IActionResult Edit(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = SdaTisuModel.GetViewData(PkValue);
                    SdaTisuModel.ViewModel fieldModel = new SdaTisuModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.ba_id_text = dr["ba_id_text"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.pa_id_text = dr["pa_id_text"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.psa_id_text = dr["psa_id_text"].ToString();
                        fieldModel.bulan = dr["bulan"].ToString();
                        fieldModel.bulan_text = dr["bulan_text"].ToString();
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.tahun_text = dr["tahun_text"].ToString();
                        fieldModel.konsumsi_tisu = dr["konsumsi_tisu"].ToString();
                        fieldModel.usaha_pengurangan_tisu_id = dr["usaha_pengurangan_tisu_id"].ToString();
                        fieldModel.usaha_pengurangan_tisu_id_text = dr["usaha_pengurangan_tisu_id_text"].ToString();
                        fieldModel.usaha_pengurangan_tisu_desc = dr["usaha_pengurangan_tisu_desc"].ToString();
                        string[] init_usaha_pengurangan_tisu_desc_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "usaha_pengurangan_tisu_desc_file_path", PkValue, dr["usaha_pengurangan_tisu_desc_file_path"].ToString());
                        fieldModel.usaha_pengurangan_tisu_desc_file_path = init_usaha_pengurangan_tisu_desc_file_path[0];
                        fieldModel.usaha_pengurangan_tisu_desc_file_path_init = init_usaha_pengurangan_tisu_desc_file_path[1];
                        fieldModel.usaha_pengurangan_tisu_jumlah = dr["usaha_pengurangan_tisu_jumlah"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
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
		[Obsolete]
        public JsonResult Insert(SdaTisuModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = SdaTisuModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["konsumsi_tisu"] = fieldModel.konsumsi_tisu != null ? fieldModel.konsumsi_tisu.Replace(",",".") : "";
                    data["usaha_pengurangan_tisu_id"] = fieldModel.usaha_pengurangan_tisu_id;
                    data["usaha_pengurangan_tisu_desc"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_tisu_desc);
                    data["usaha_pengurangan_tisu_desc_file_path"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_tisu_desc_file_path);
                    data["usaha_pengurangan_tisu_jumlah"] = fieldModel.usaha_pengurangan_tisu_jumlah != null ? fieldModel.usaha_pengurangan_tisu_jumlah.Replace(",",".") : "";
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = SdaTisuModel.Insert(_hostingEnvironment, HttpContext, data);
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
		[Obsolete]
        public JsonResult Update(SdaTisuModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(SdaTisuModel.GetData(PkValue));
                    data_old["konsumsi_tisu"] = data_old["konsumsi_tisu"].ToString() != ""  ? data_old["konsumsi_tisu"].ToString().Replace(",", ".") : "";
                    data_old["usaha_pengurangan_tisu_jumlah"] = data_old["usaha_pengurangan_tisu_jumlah"].ToString() != ""  ? data_old["usaha_pengurangan_tisu_jumlah"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(SdaTisuModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["konsumsi_tisu"] = fieldModel.konsumsi_tisu != null ? fieldModel.konsumsi_tisu.ToString().Replace(",", ".") : "";
                    data["usaha_pengurangan_tisu_id"] = fieldModel.usaha_pengurangan_tisu_id;
                    data["usaha_pengurangan_tisu_desc"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_tisu_desc);
                    data["usaha_pengurangan_tisu_desc_file_path"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_tisu_desc_file_path);
                    data["usaha_pengurangan_tisu_jumlah"] = fieldModel.usaha_pengurangan_tisu_jumlah != null ? fieldModel.usaha_pengurangan_tisu_jumlah.ToString().Replace(",", ".") : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = SdaTisuModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
        public JsonResult Delete(string id)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_delete))
                {
					int jml = DataModel.GetCountDataUsed(_table_name, id);
                    if (jml == 0) {
						var PkValue = new OrderedDictionary();
						PkValue["id"] = id;
						OrderedDictionary data = DataModel.DtToDictionary(SdaTisuModel.GetData(PkValue));
						if (data != null)
						{
							result = SdaTisuModel.Delete(_hostingEnvironment, HttpContext, data);
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
                        result.status = 2;
                        result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                        result.message = ResxHelper.GetValue("Message", "DataHasBeenUsed");
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
            DataTable data = SdaTisuModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_usaha_pengurangan_tisu_desc_file_path(IEnumerable<IFormFile> usaha_pengurangan_tisu_desc_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "usaha_pengurangan_tisu_desc_file_path");
            if (usaha_pengurangan_tisu_desc_file_path_file != null)
            {
                foreach (var file in usaha_pengurangan_tisu_desc_file_path_file)
                {
                    if (!Directory.Exists(pathData)) { FileHelper.CreateDirectoryRecursively(pathData); }
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(pathData, fileName);
                    if (System.IO.File.Exists(physicalPath)) { System.IO.File.Delete(physicalPath); }
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create)) { await file.CopyToAsync(fileStream); }
                }
            }
            return Content("");
        }

        public ActionResult remove_usaha_pengurangan_tisu_desc_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "usaha_pengurangan_tisu_desc_file_path");
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    string physicalPath = Path.Combine(pathData, fileName);
                    if (System.IO.File.Exists(physicalPath)) { System.IO.File.Delete(physicalPath); }
                }
            }
            return Content("");
        }

    }
}