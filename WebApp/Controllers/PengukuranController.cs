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
    
    public class PengukuranController : Controller
    {
        private string _rule_view = "PengukuranView";
        private string _rule_add = "PengukuranAdd";
        private string _rule_edit = "PengukuranEdit";
        private string _rule_delete = "PengukuranDelete";
        private string _rule_edit_all = "PengukuranEditAll";
        private string _rule_delete_all = "PengukuranEditAll";
        private string _path_controler = "/Pengukuran/";
        private string _path_view = "/Views/Pengukuran/";
        private readonly string _table_name = "ta_pengukuran";
        private readonly string _table_title = "Measurement";

		private static IHostingEnvironment _hostingEnvironment;
        public PengukuranController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(PengukuranModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = PengukuranModel._pkKey;
                    ViewData["displayItem"] = PengukuranModel.GetDisplayItem();
                    ViewData["column_att"] = PengukuranModel.GetGridColumn();
                    ViewData["param"] = PengukuranModel.GetListParam();
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
        public JsonResult GetListData(PengukuranModel.ActionRequest param)
        {
            GridData data = PengukuranModel.GetListData(HttpContext,param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["file_path_hasil_uji"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_hasil_uji", data.data.Rows[i]["file_path_hasil_uji"].ToString());
                data.data.Rows[i]["file_path_baku_mutu"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_baku_mutu", data.data.Rows[i]["file_path_baku_mutu"].ToString());
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
                    PengukuranModel.ViewModel fieldModel = new PengukuranModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = PengukuranModel.GetViewData(PkValue);
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
                                fieldModel.jenis_pemeriksaan_pengujian_id = dr["jenis_pemeriksaan_pengujian_id"].ToString();
                                fieldModel.jenis_pemeriksaan_pengujian_id_text = dr["jenis_pemeriksaan_pengujian_id_text"].ToString();
                                fieldModel.keterangan_series_kode = dr["keterangan_series_kode"].ToString();
                                fieldModel.jumlah_titik_penaatan = dr["jumlah_titik_penaatan"].ToString();
                                fieldModel.periode_pemeriksaan_pengujian_id = dr["periode_pemeriksaan_pengujian_id"].ToString();
                                fieldModel.periode_pemeriksaan_pengujian_id_text = dr["periode_pemeriksaan_pengujian_id_text"].ToString();
                                fieldModel.status_penaatan_id = dr["status_penaatan_id"].ToString();
                                fieldModel.status_penaatan_id_text = dr["status_penaatan_id_text"].ToString();
                                fieldModel.hasil_pengukuran = String.Format("{0:N2}", dr["hasil_pengukuran"]);
                                fieldModel.tanggal_pengujian = dr["tanggal_pengujian"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_pengujian"]) : "";
                                fieldModel.file_path_hasil_uji = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_hasil_uji", dr["file_path_hasil_uji"].ToString());
                                fieldModel.file_path_baku_mutu = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_baku_mutu", dr["file_path_baku_mutu"].ToString());
                                fieldModel.keterangan_uji = dr["keterangan_uji"].ToString();
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
        public IActionResult Add(PengukuranModel.ViewModel fieldModel)
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
                    //PengukuranModel.ViewModel fieldModel = new PengukuranModel.ViewModel();
                    fieldModel.id = PengukuranModel.GetNewId().ToString();
                    fieldModel.file_path_hasil_uji_init = "[]";
                    fieldModel.file_path_baku_mutu_init = "[]";
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
                    DataTable data = PengukuranModel.GetViewData(PkValue);
                    PengukuranModel.ViewModel fieldModel = new PengukuranModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = PengukuranModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.jenis_pemeriksaan_pengujian_id = dr["jenis_pemeriksaan_pengujian_id"].ToString();
                        fieldModel.keterangan_series_kode = dr["keterangan_series_kode"].ToString();
                        fieldModel.jumlah_titik_penaatan = dr["jumlah_titik_penaatan"].ToString();
                        fieldModel.periode_pemeriksaan_pengujian_id = dr["periode_pemeriksaan_pengujian_id"].ToString();
                        fieldModel.status_penaatan_id = dr["status_penaatan_id"].ToString();
                        fieldModel.hasil_pengukuran = dr["hasil_pengukuran"].ToString();
                        fieldModel.tanggal_pengujian = dr["tanggal_pengujian"].ToString();
                        fieldModel.file_path_hasil_uji = dr["file_path_hasil_uji"].ToString();
                        fieldModel.file_path_baku_mutu = dr["file_path_baku_mutu"].ToString();
                        fieldModel.keterangan_uji = dr["keterangan_uji"].ToString();
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
                    DataTable data = PengukuranModel.GetViewData(PkValue);
                    PengukuranModel.ViewModel fieldModel = new PengukuranModel.ViewModel();
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
                        fieldModel.jenis_pemeriksaan_pengujian_id = dr["jenis_pemeriksaan_pengujian_id"].ToString();
                        fieldModel.jenis_pemeriksaan_pengujian_id_text = dr["jenis_pemeriksaan_pengujian_id_text"].ToString();
                        fieldModel.keterangan_series_kode = dr["keterangan_series_kode"].ToString();
                        fieldModel.jumlah_titik_penaatan = dr["jumlah_titik_penaatan"].ToString();
                        fieldModel.periode_pemeriksaan_pengujian_id = dr["periode_pemeriksaan_pengujian_id"].ToString();
                        fieldModel.periode_pemeriksaan_pengujian_id_text = dr["periode_pemeriksaan_pengujian_id_text"].ToString();
                        fieldModel.status_penaatan_id = dr["status_penaatan_id"].ToString();
                        fieldModel.status_penaatan_id_text = dr["status_penaatan_id_text"].ToString();
                        fieldModel.hasil_pengukuran = dr["hasil_pengukuran"].ToString();
                        fieldModel.tanggal_pengujian = dr["tanggal_pengujian"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tanggal_pengujian"]) : "";
                        fieldModel.dt_tanggal_pengujian = dr["tanggal_pengujian"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_pengujian"]) : "";
                        string[] init_file_path_hasil_uji = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_hasil_uji", PkValue, dr["file_path_hasil_uji"].ToString());
                        fieldModel.file_path_hasil_uji = init_file_path_hasil_uji[0];
                        fieldModel.file_path_hasil_uji_init = init_file_path_hasil_uji[1];
                        string[] init_file_path_baku_mutu = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_baku_mutu", PkValue, dr["file_path_baku_mutu"].ToString());
                        fieldModel.file_path_baku_mutu = init_file_path_baku_mutu[0];
                        fieldModel.file_path_baku_mutu_init = init_file_path_baku_mutu[1];
                        fieldModel.keterangan_uji = dr["keterangan_uji"].ToString();
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
        public JsonResult Insert(PengukuranModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = PengukuranModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["jenis_pemeriksaan_pengujian_id"] = fieldModel.jenis_pemeriksaan_pengujian_id;
                    data["keterangan_series_kode"] = AntiXss.HtmlEncode(fieldModel.keterangan_series_kode);
                    data["jumlah_titik_penaatan"] = fieldModel.jumlah_titik_penaatan;
                    data["periode_pemeriksaan_pengujian_id"] = fieldModel.periode_pemeriksaan_pengujian_id;
                    data["status_penaatan_id"] = fieldModel.status_penaatan_id;
                    data["hasil_pengukuran"] = fieldModel.hasil_pengukuran != null ? fieldModel.hasil_pengukuran.Replace(",",".") : "";
                    data["tanggal_pengujian"] = fieldModel.tanggal_pengujian;
                    data["file_path_hasil_uji"] = AntiXss.HtmlEncode(fieldModel.file_path_hasil_uji);
                    data["file_path_baku_mutu"] = AntiXss.HtmlEncode(fieldModel.file_path_baku_mutu);
                    data["keterangan_uji"] = AntiXss.HtmlEncode(fieldModel.keterangan_uji);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = PengukuranModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(PengukuranModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(PengukuranModel.GetData(PkValue));
                    data_old["hasil_pengukuran"] = data_old["hasil_pengukuran"].ToString() != ""  ? data_old["hasil_pengukuran"].ToString().Replace(",", ".") : "";
                    data_old["tanggal_pengujian"] = data_old["tanggal_pengujian"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tanggal_pengujian"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(PengukuranModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["jenis_pemeriksaan_pengujian_id"] = fieldModel.jenis_pemeriksaan_pengujian_id;
                    data["keterangan_series_kode"] = AntiXss.HtmlEncode(fieldModel.keterangan_series_kode);
                    data["jumlah_titik_penaatan"] = fieldModel.jumlah_titik_penaatan;
                    data["periode_pemeriksaan_pengujian_id"] = fieldModel.periode_pemeriksaan_pengujian_id;
                    data["status_penaatan_id"] = fieldModel.status_penaatan_id;
                    data["hasil_pengukuran"] = fieldModel.hasil_pengukuran != null ? fieldModel.hasil_pengukuran.ToString().Replace(",", ".") : "";
                    data["tanggal_pengujian"] = fieldModel.tanggal_pengujian;
                    data["file_path_hasil_uji"] = AntiXss.HtmlEncode(fieldModel.file_path_hasil_uji);
                    data["file_path_baku_mutu"] = AntiXss.HtmlEncode(fieldModel.file_path_baku_mutu);
                    data["keterangan_uji"] = AntiXss.HtmlEncode(fieldModel.keterangan_uji);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = PengukuranModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(PengukuranModel.GetData(PkValue));
						if (data != null)
						{
							result = PengukuranModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = PengukuranModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_file_path_hasil_uji(IEnumerable<IFormFile> file_path_hasil_uji_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_hasil_uji");
            if (file_path_hasil_uji_file != null)
            {
                foreach (var file in file_path_hasil_uji_file)
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

        public ActionResult remove_file_path_hasil_uji(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_hasil_uji");
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

        public async Task<IActionResult> save_file_path_baku_mutu(IEnumerable<IFormFile> file_path_baku_mutu_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_baku_mutu");
            if (file_path_baku_mutu_file != null)
            {
                foreach (var file in file_path_baku_mutu_file)
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

        public ActionResult remove_file_path_baku_mutu(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_baku_mutu");
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