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
    
    public class LegalJenisIzinEhsLb3Controller : Controller
    {
        private string _rule_view = "LegalView";
        private string _rule_add = "LegalAdd";
        private string _rule_edit = "LegalEdit";
        private string _rule_delete = "LegalDelete";
        private string _rule_edit_all = "LegalEditAll";
        private string _rule_delete_all = "LegalDeleteAll";
        private string _path_controler = "/LegalJenisIzinEhsLb3/";
        private string _path_view = "/Views/Legal/LegalJenisIzinEhsLb3/";
        private readonly string _table_name = "ta_legal_jenis_izin_ehs_lb3";
        private readonly string _table_title = "Legal Jenis Izin Ehs Lb3";

		private static IHostingEnvironment _hostingEnvironment;
        public LegalJenisIzinEhsLb3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(LegalJenisIzinEhsLb3Model.ViewModel filterColumn)
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
                    ViewData["pkKey"] = LegalJenisIzinEhsLb3Model._pkKey;
                    ViewData["displayItem"] = LegalJenisIzinEhsLb3Model.GetDisplayItem();
                    ViewData["column_att"] = LegalJenisIzinEhsLb3Model.GetGridColumn();
                    ViewData["param"] = LegalJenisIzinEhsLb3Model.GetListParam();
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
        public JsonResult GetListData(LegalJenisIzinEhsLb3Model.ActionRequest param)
        {
            GridData data = LegalJenisIzinEhsLb3Model.GetListData(param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["file_path_bukti_lapor"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_bukti_lapor", data.data.Rows[i]["file_path_bukti_lapor"].ToString());
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
                    LegalJenisIzinEhsLb3Model.ViewModel fieldModel = new LegalJenisIzinEhsLb3Model.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = LegalJenisIzinEhsLb3Model.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.legal_id = dr["legal_id"].ToString();
                                fieldModel.legal_id_text = dr["legal_id_text"].ToString();
                                fieldModel.jenis_limbah_tersimpan_id = dr["jenis_limbah_tersimpan_id"].ToString();
                                fieldModel.jenis_limbah_tersimpan_id_text = dr["jenis_limbah_tersimpan_id_text"].ToString();
                                fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                                fieldModel.periode_telah_dilaporkan_id = dr["periode_telah_dilaporkan_id"].ToString();
                                fieldModel.periode_telah_dilaporkan_id_text = dr["periode_telah_dilaporkan_id_text"].ToString();
                                fieldModel.tanggal_pelaporan = dr["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_pelaporan"]) : "";
                                fieldModel.file_name_bukti_lapor = dr["file_name_bukti_lapor"].ToString();
                                fieldModel.file_path_bukti_lapor = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_bukti_lapor", dr["file_path_bukti_lapor"].ToString());
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
        public IActionResult Add(LegalJenisIzinEhsLb3Model.ViewModel fieldModel)
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
                    //LegalJenisIzinEhsLb3Model.ViewModel fieldModel = new LegalJenisIzinEhsLb3Model.ViewModel();
                    fieldModel.id = LegalJenisIzinEhsLb3Model.GetNewId().ToString();
                    fieldModel.file_path_bukti_lapor_init = "[]";
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
                    DataTable data = LegalJenisIzinEhsLb3Model.GetViewData(PkValue);
                    LegalJenisIzinEhsLb3Model.ViewModel fieldModel = new LegalJenisIzinEhsLb3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = LegalJenisIzinEhsLb3Model.GetNewId().ToString();
                        fieldModel.legal_id = dr["legal_id"].ToString();
                        fieldModel.jenis_limbah_tersimpan_id = dr["jenis_limbah_tersimpan_id"].ToString();
                        fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                        fieldModel.periode_telah_dilaporkan_id = dr["periode_telah_dilaporkan_id"].ToString();
                        fieldModel.tanggal_pelaporan = dr["tanggal_pelaporan"].ToString();
                        fieldModel.file_name_bukti_lapor = dr["file_name_bukti_lapor"].ToString();
                        fieldModel.file_path_bukti_lapor = dr["file_path_bukti_lapor"].ToString();
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
                    DataTable data = LegalJenisIzinEhsLb3Model.GetViewData(PkValue);
                    LegalJenisIzinEhsLb3Model.ViewModel fieldModel = new LegalJenisIzinEhsLb3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.legal_id = dr["legal_id"].ToString();
                        fieldModel.legal_id_text = dr["legal_id_text"].ToString();
                        fieldModel.jenis_limbah_tersimpan_id = dr["jenis_limbah_tersimpan_id"].ToString();
                        fieldModel.jenis_limbah_tersimpan_id_text = dr["jenis_limbah_tersimpan_id_text"].ToString();
                        fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                        fieldModel.periode_telah_dilaporkan_id = dr["periode_telah_dilaporkan_id"].ToString();
                        fieldModel.periode_telah_dilaporkan_id_text = dr["periode_telah_dilaporkan_id_text"].ToString();
                        fieldModel.tanggal_pelaporan = dr["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tanggal_pelaporan"]) : "";
                        fieldModel.dt_tanggal_pelaporan = dr["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_pelaporan"]) : "";
                        fieldModel.file_name_bukti_lapor = dr["file_name_bukti_lapor"].ToString();
                        string[] init_file_path_bukti_lapor = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_bukti_lapor", PkValue, dr["file_path_bukti_lapor"].ToString());
                        fieldModel.file_path_bukti_lapor = init_file_path_bukti_lapor[0];
                        fieldModel.file_path_bukti_lapor_init = init_file_path_bukti_lapor[1];
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
        public JsonResult Insert(LegalJenisIzinEhsLb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = LegalJenisIzinEhsLb3Model.GetNewId().ToString();
                    data["legal_id"] = fieldModel.legal_id;
                    data["jenis_limbah_tersimpan_id"] = fieldModel.jenis_limbah_tersimpan_id;
                    data["kode_limbah"] = AntiXss.HtmlEncode(fieldModel.kode_limbah);
                    data["periode_telah_dilaporkan_id"] = fieldModel.periode_telah_dilaporkan_id;
                    data["tanggal_pelaporan"] = fieldModel.tanggal_pelaporan;
                    data["file_name_bukti_lapor"] = AntiXss.HtmlEncode(fieldModel.file_name_bukti_lapor);
                    data["file_path_bukti_lapor"] = AntiXss.HtmlEncode(fieldModel.file_path_bukti_lapor);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = LegalJenisIzinEhsLb3Model.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(LegalJenisIzinEhsLb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(LegalJenisIzinEhsLb3Model.GetData(PkValue));
                    data_old["tanggal_pelaporan"] = data_old["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tanggal_pelaporan"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(LegalJenisIzinEhsLb3Model.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["legal_id"] = fieldModel.legal_id;
                    data["jenis_limbah_tersimpan_id"] = fieldModel.jenis_limbah_tersimpan_id;
                    data["kode_limbah"] = AntiXss.HtmlEncode(fieldModel.kode_limbah);
                    data["periode_telah_dilaporkan_id"] = fieldModel.periode_telah_dilaporkan_id;
                    data["tanggal_pelaporan"] = fieldModel.tanggal_pelaporan;
                    data["file_name_bukti_lapor"] = AntiXss.HtmlEncode(fieldModel.file_name_bukti_lapor);
                    data["file_path_bukti_lapor"] = AntiXss.HtmlEncode(fieldModel.file_path_bukti_lapor);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = LegalJenisIzinEhsLb3Model.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(LegalJenisIzinEhsLb3Model.GetData(PkValue));
						if (data != null)
						{
							result = LegalJenisIzinEhsLb3Model.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = LegalJenisIzinEhsLb3Model.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_file_path_bukti_lapor(IEnumerable<IFormFile> file_path_bukti_lapor_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_bukti_lapor");
            if (file_path_bukti_lapor_file != null)
            {
                foreach (var file in file_path_bukti_lapor_file)
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

        public ActionResult remove_file_path_bukti_lapor(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_bukti_lapor");
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