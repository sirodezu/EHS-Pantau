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
    
    public class FasSpekTpsLb3Controller : Controller
    {
        private string _rule_view = "FasilitasView";
        private string _rule_add = "FasilitasAdd";
        private string _rule_edit = "FasilitasEdit";
        private string _rule_delete = "FasilitasDelete";
        private string _rule_edit_all = "FasilitasEditAll";
        private string _rule_delete_all = "FasilitasDeleteAll";
        private string _path_controler = "/FasSpekTpsLb3/";
        private string _path_view = "/Views/Fasilitas/FasSpekTpsLb3/";
        private readonly string _table_name = "ta_fas_spek_tps_lb3";
        private readonly string _table_title = "Fas Spek Tps Lb3";

		private static IHostingEnvironment _hostingEnvironment;
        public FasSpekTpsLb3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(FasSpekTpsLb3Model.ViewModel filterColumn)
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
                    ViewData["pkKey"] = FasSpekTpsLb3Model._pkKey;
                    ViewData["displayItem"] = FasSpekTpsLb3Model.GetDisplayItem();
                    ViewData["column_att"] = FasSpekTpsLb3Model.GetGridColumn();
                    ViewData["param"] = FasSpekTpsLb3Model.GetListParam();
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
        public JsonResult GetListData(FasSpekTpsLb3Model.ActionRequest param)
        {
            GridData data = FasSpekTpsLb3Model.GetListData(param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["layout_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "layout_file_path", data.data.Rows[i]["layout_file_path"].ToString());
                data.data.Rows[i]["photo"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "photo", data.data.Rows[i]["photo"].ToString());
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
                    FasSpekTpsLb3Model.ViewModel fieldModel = new FasSpekTpsLb3Model.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = FasSpekTpsLb3Model.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.fasilitas_id = dr["fasilitas_id"].ToString();
                                fieldModel.nomor_aset = dr["nomor_aset"].ToString();
                                fieldModel.luas_bangunan = String.Format("{0:N2}", dr["luas_bangunan"]);
                                fieldModel.latitude = String.Format("{0:N2}", dr["latitude"]);
                                fieldModel.longitude = String.Format("{0:N2}", dr["longitude"]);
                                fieldModel.layout_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "layout_file_path", dr["layout_file_path"].ToString());
                                fieldModel.photo = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "photo", dr["photo"].ToString());
                                fieldModel.tgl_periksa_akhir = dr["tgl_periksa_akhir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_periksa_akhir"]) : "";
                                fieldModel.hasil_periksa_akhir = dr["hasil_periksa_akhir"].ToString();
                                fieldModel.hasil_periksa_akhir_text = dr["hasil_periksa_akhir_text"].ToString();
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
        public IActionResult Add(FasSpekTpsLb3Model.ViewModel fieldModel)
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
                    //FasSpekTpsLb3Model.ViewModel fieldModel = new FasSpekTpsLb3Model.ViewModel();
                    fieldModel.id = FasSpekTpsLb3Model.GetNewId().ToString();
                    fieldModel.layout_file_path_init = "[]";
                    fieldModel.photo_init = "[]";
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
                    DataTable data = FasSpekTpsLb3Model.GetViewData(PkValue);
                    FasSpekTpsLb3Model.ViewModel fieldModel = new FasSpekTpsLb3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = FasSpekTpsLb3Model.GetNewId().ToString();
                        fieldModel.fasilitas_id = dr["fasilitas_id"].ToString();
                        fieldModel.nomor_aset = dr["nomor_aset"].ToString();
                        fieldModel.luas_bangunan = dr["luas_bangunan"].ToString();
                        fieldModel.latitude = dr["latitude"].ToString();
                        fieldModel.longitude = dr["longitude"].ToString();
                        fieldModel.layout_file_path = dr["layout_file_path"].ToString();
                        fieldModel.photo = dr["photo"].ToString();
                        fieldModel.tgl_periksa_akhir = dr["tgl_periksa_akhir"].ToString();
                        fieldModel.hasil_periksa_akhir = dr["hasil_periksa_akhir"].ToString();
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
                    DataTable data = FasSpekTpsLb3Model.GetViewData(PkValue);
                    FasSpekTpsLb3Model.ViewModel fieldModel = new FasSpekTpsLb3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.fasilitas_id = dr["fasilitas_id"].ToString();
                        fieldModel.nomor_aset = dr["nomor_aset"].ToString();
                        fieldModel.luas_bangunan = dr["luas_bangunan"].ToString();
                        fieldModel.latitude = dr["latitude"].ToString();
                        fieldModel.longitude = dr["longitude"].ToString();
                        string[] init_layout_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "layout_file_path", PkValue, dr["layout_file_path"].ToString());
                        fieldModel.layout_file_path = init_layout_file_path[0];
                        fieldModel.layout_file_path_init = init_layout_file_path[1];
                        string[] init_photo = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "photo", PkValue, dr["photo"].ToString());
                        fieldModel.photo = init_photo[0];
                        fieldModel.photo_init = init_photo[1];
                        fieldModel.tgl_periksa_akhir = dr["tgl_periksa_akhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_periksa_akhir"]) : "";
                        fieldModel.dt_tgl_periksa_akhir = dr["tgl_periksa_akhir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_periksa_akhir"]) : "";
                        fieldModel.hasil_periksa_akhir = dr["hasil_periksa_akhir"].ToString();
                        fieldModel.hasil_periksa_akhir_text = dr["hasil_periksa_akhir_text"].ToString();
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
        public JsonResult Insert(FasSpekTpsLb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = FasSpekTpsLb3Model.GetNewId().ToString();
                    data["fasilitas_id"] = fieldModel.fasilitas_id;
                    data["nomor_aset"] = AntiXss.HtmlEncode(fieldModel.nomor_aset);
                    data["luas_bangunan"] = fieldModel.luas_bangunan != null ? fieldModel.luas_bangunan.Replace(",",".") : "";
                    data["latitude"] = fieldModel.latitude != null ? fieldModel.latitude.Replace(",",".") : "";
                    data["longitude"] = fieldModel.longitude != null ? fieldModel.longitude.Replace(",",".") : "";
                    data["layout_file_path"] = AntiXss.HtmlEncode(fieldModel.layout_file_path);
                    data["photo"] = AntiXss.HtmlEncode(fieldModel.photo);
                    data["tgl_periksa_akhir"] = fieldModel.tgl_periksa_akhir;
                    data["hasil_periksa_akhir"] = fieldModel.hasil_periksa_akhir;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = FasSpekTpsLb3Model.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(FasSpekTpsLb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(FasSpekTpsLb3Model.GetData(PkValue));
                    data_old["luas_bangunan"] = data_old["luas_bangunan"].ToString() != ""  ? data_old["luas_bangunan"].ToString().Replace(",", ".") : "";
                    data_old["latitude"] = data_old["latitude"].ToString() != ""  ? data_old["latitude"].ToString().Replace(",", ".") : "";
                    data_old["longitude"] = data_old["longitude"].ToString() != ""  ? data_old["longitude"].ToString().Replace(",", ".") : "";
                    data_old["tgl_periksa_akhir"] = data_old["tgl_periksa_akhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_periksa_akhir"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(FasSpekTpsLb3Model.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["fasilitas_id"] = fieldModel.fasilitas_id;
                    data["nomor_aset"] = AntiXss.HtmlEncode(fieldModel.nomor_aset);
                    data["luas_bangunan"] = fieldModel.luas_bangunan != null ? fieldModel.luas_bangunan.ToString().Replace(",", ".") : "";
                    data["latitude"] = fieldModel.latitude != null ? fieldModel.latitude.ToString().Replace(",", ".") : "";
                    data["longitude"] = fieldModel.longitude != null ? fieldModel.longitude.ToString().Replace(",", ".") : "";
                    data["layout_file_path"] = AntiXss.HtmlEncode(fieldModel.layout_file_path);
                    data["photo"] = AntiXss.HtmlEncode(fieldModel.photo);
                    data["tgl_periksa_akhir"] = fieldModel.tgl_periksa_akhir;
                    data["hasil_periksa_akhir"] = fieldModel.hasil_periksa_akhir;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = FasSpekTpsLb3Model.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(FasSpekTpsLb3Model.GetData(PkValue));
						if (data != null)
						{
							result = FasSpekTpsLb3Model.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = FasSpekTpsLb3Model.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_layout_file_path(IEnumerable<IFormFile> layout_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "layout_file_path");
            if (layout_file_path_file != null)
            {
                foreach (var file in layout_file_path_file)
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

        public ActionResult remove_layout_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "layout_file_path");
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

        public async Task<IActionResult> save_photo(IEnumerable<IFormFile> photo_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "photo");
            if (photo_file != null)
            {
                foreach (var file in photo_file)
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

        public ActionResult remove_photo(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "photo");
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