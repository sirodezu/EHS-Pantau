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
    
    public class FasSpekMobilCraneController : Controller
    {
        private string _rule_view = "FasilitasView";
        private string _rule_add = "FasilitasAdd";
        private string _rule_edit = "FasilitasEdit";
        private string _rule_delete = "FasilitasDelete";
        private string _rule_edit_all = "FasilitasEditAll";
        private string _rule_delete_all = "FasilitasDeleteAll";
        private string _path_controler = "/FasSpekMobilCrane/";
        private string _path_view = "/Views/Fasilitas/FasSpekMobilCrane/";
        private readonly string _table_name = "ta_fas_spek_mobil_crane";
        private readonly string _table_title = "Fas Spek Mobil Crane";

		private static IHostingEnvironment _hostingEnvironment;
        public FasSpekMobilCraneController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(FasSpekMobilCraneModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = FasSpekMobilCraneModel._pkKey;
                    ViewData["displayItem"] = FasSpekMobilCraneModel.GetDisplayItem();
                    ViewData["column_att"] = FasSpekMobilCraneModel.GetGridColumn();
                    ViewData["param"] = FasSpekMobilCraneModel.GetListParam();
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
        public JsonResult GetListData(FasSpekMobilCraneModel.ActionRequest param)
        {
            GridData data = FasSpekMobilCraneModel.GetListData(param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
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
                    FasSpekMobilCraneModel.ViewModel fieldModel = new FasSpekMobilCraneModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = FasSpekMobilCraneModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.fasilitas_id = dr["fasilitas_id"].ToString();
                                fieldModel.nomor_aset = dr["nomor_aset"].ToString();
                                fieldModel.merk = dr["merk"].ToString();
                                fieldModel.serial_number = dr["serial_number"].ToString();
                                fieldModel.nama_pabrik_pembuatan = dr["nama_pabrik_pembuatan"].ToString();
                                fieldModel.tempat_pembuatan = dr["tempat_pembuatan"].ToString();
                                fieldModel.tahun_pembuatan = dr["tahun_pembuatan"].ToString();
                                fieldModel.untuk_mengangkut = dr["untuk_mengangkut"].ToString();
                                fieldModel.kapasitas_angkut = String.Format("{0:N2}", dr["kapasitas_angkut"]);
                                fieldModel.tinggi_angkatan = String.Format("{0:N2}", dr["tinggi_angkatan"]);
                                fieldModel.jenis_penggerak = dr["jenis_penggerak"].ToString();
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
        public IActionResult Add(FasSpekMobilCraneModel.ViewModel fieldModel)
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
                    //FasSpekMobilCraneModel.ViewModel fieldModel = new FasSpekMobilCraneModel.ViewModel();
                    fieldModel.id = FasSpekMobilCraneModel.GetNewId().ToString();
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
                    DataTable data = FasSpekMobilCraneModel.GetViewData(PkValue);
                    FasSpekMobilCraneModel.ViewModel fieldModel = new FasSpekMobilCraneModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = FasSpekMobilCraneModel.GetNewId().ToString();
                        fieldModel.fasilitas_id = dr["fasilitas_id"].ToString();
                        fieldModel.nomor_aset = dr["nomor_aset"].ToString();
                        fieldModel.merk = dr["merk"].ToString();
                        fieldModel.serial_number = dr["serial_number"].ToString();
                        fieldModel.nama_pabrik_pembuatan = dr["nama_pabrik_pembuatan"].ToString();
                        fieldModel.tempat_pembuatan = dr["tempat_pembuatan"].ToString();
                        fieldModel.tahun_pembuatan = dr["tahun_pembuatan"].ToString();
                        fieldModel.untuk_mengangkut = dr["untuk_mengangkut"].ToString();
                        fieldModel.kapasitas_angkut = dr["kapasitas_angkut"].ToString();
                        fieldModel.tinggi_angkatan = dr["tinggi_angkatan"].ToString();
                        fieldModel.jenis_penggerak = dr["jenis_penggerak"].ToString();
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
                    DataTable data = FasSpekMobilCraneModel.GetViewData(PkValue);
                    FasSpekMobilCraneModel.ViewModel fieldModel = new FasSpekMobilCraneModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.fasilitas_id = dr["fasilitas_id"].ToString();
                        fieldModel.nomor_aset = dr["nomor_aset"].ToString();
                        fieldModel.merk = dr["merk"].ToString();
                        fieldModel.serial_number = dr["serial_number"].ToString();
                        fieldModel.nama_pabrik_pembuatan = dr["nama_pabrik_pembuatan"].ToString();
                        fieldModel.tempat_pembuatan = dr["tempat_pembuatan"].ToString();
                        fieldModel.tahun_pembuatan = dr["tahun_pembuatan"].ToString();
                        fieldModel.untuk_mengangkut = dr["untuk_mengangkut"].ToString();
                        fieldModel.kapasitas_angkut = dr["kapasitas_angkut"].ToString();
                        fieldModel.tinggi_angkatan = dr["tinggi_angkatan"].ToString();
                        fieldModel.jenis_penggerak = dr["jenis_penggerak"].ToString();
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
        public JsonResult Insert(FasSpekMobilCraneModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = FasSpekMobilCraneModel.GetNewId().ToString();
                    data["fasilitas_id"] = fieldModel.fasilitas_id;
                    data["nomor_aset"] = AntiXss.HtmlEncode(fieldModel.nomor_aset);
                    data["merk"] = AntiXss.HtmlEncode(fieldModel.merk);
                    data["serial_number"] = AntiXss.HtmlEncode(fieldModel.serial_number);
                    data["nama_pabrik_pembuatan"] = AntiXss.HtmlEncode(fieldModel.nama_pabrik_pembuatan);
                    data["tempat_pembuatan"] = AntiXss.HtmlEncode(fieldModel.tempat_pembuatan);
                    data["tahun_pembuatan"] = fieldModel.tahun_pembuatan;
                    data["untuk_mengangkut"] = AntiXss.HtmlEncode(fieldModel.untuk_mengangkut);
                    data["kapasitas_angkut"] = fieldModel.kapasitas_angkut != null ? fieldModel.kapasitas_angkut.Replace(",",".") : "";
                    data["tinggi_angkatan"] = fieldModel.tinggi_angkatan != null ? fieldModel.tinggi_angkatan.Replace(",",".") : "";
                    data["jenis_penggerak"] = AntiXss.HtmlEncode(fieldModel.jenis_penggerak);
                    data["photo"] = AntiXss.HtmlEncode(fieldModel.photo);
                    data["tgl_periksa_akhir"] = fieldModel.tgl_periksa_akhir;
                    data["hasil_periksa_akhir"] = fieldModel.hasil_periksa_akhir;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = FasSpekMobilCraneModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(FasSpekMobilCraneModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(FasSpekMobilCraneModel.GetData(PkValue));
                    data_old["kapasitas_angkut"] = data_old["kapasitas_angkut"].ToString() != ""  ? data_old["kapasitas_angkut"].ToString().Replace(",", ".") : "";
                    data_old["tinggi_angkatan"] = data_old["tinggi_angkatan"].ToString() != ""  ? data_old["tinggi_angkatan"].ToString().Replace(",", ".") : "";
                    data_old["tgl_periksa_akhir"] = data_old["tgl_periksa_akhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_periksa_akhir"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(FasSpekMobilCraneModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["fasilitas_id"] = fieldModel.fasilitas_id;
                    data["nomor_aset"] = AntiXss.HtmlEncode(fieldModel.nomor_aset);
                    data["merk"] = AntiXss.HtmlEncode(fieldModel.merk);
                    data["serial_number"] = AntiXss.HtmlEncode(fieldModel.serial_number);
                    data["nama_pabrik_pembuatan"] = AntiXss.HtmlEncode(fieldModel.nama_pabrik_pembuatan);
                    data["tempat_pembuatan"] = AntiXss.HtmlEncode(fieldModel.tempat_pembuatan);
                    data["tahun_pembuatan"] = fieldModel.tahun_pembuatan;
                    data["untuk_mengangkut"] = AntiXss.HtmlEncode(fieldModel.untuk_mengangkut);
                    data["kapasitas_angkut"] = fieldModel.kapasitas_angkut != null ? fieldModel.kapasitas_angkut.ToString().Replace(",", ".") : "";
                    data["tinggi_angkatan"] = fieldModel.tinggi_angkatan != null ? fieldModel.tinggi_angkatan.ToString().Replace(",", ".") : "";
                    data["jenis_penggerak"] = AntiXss.HtmlEncode(fieldModel.jenis_penggerak);
                    data["photo"] = AntiXss.HtmlEncode(fieldModel.photo);
                    data["tgl_periksa_akhir"] = fieldModel.tgl_periksa_akhir;
                    data["hasil_periksa_akhir"] = fieldModel.hasil_periksa_akhir;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = FasSpekMobilCraneModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(FasSpekMobilCraneModel.GetData(PkValue));
						if (data != null)
						{
							result = FasSpekMobilCraneModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = FasSpekMobilCraneModel.LookupData(param);
            return Json(data);
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