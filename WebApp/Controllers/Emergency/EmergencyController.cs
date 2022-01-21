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
    
    public class EmergencyController : Controller
    {
        private string _rule_view = "EmergencyView";
        private string _rule_add = "EmergencyAdd";
        private string _rule_edit = "EmergencyEdit";
        private string _rule_delete = "EmergencyDelete";
        private string _rule_edit_all = "EmergencyEditAll";
        private string _rule_delete_all = "EmergencyDeleteAll";
        private string _path_controler = "/Emergency/";
        private string _path_view = "/Views/Emergency/Emergency/";
        private readonly string _table_name = "ta_emg";
        private readonly string _table_title = "Emergency";

		private static IHostingEnvironment _hostingEnvironment;
        public EmergencyController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(EmergencyModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = EmergencyModel._pkKey;
                    ViewData["displayItem"] = EmergencyModel.GetDisplayItem();
                    ViewData["column_att"] = EmergencyModel.GetGridColumn();
                    ViewData["param"] = EmergencyModel.GetListParam();
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
        public JsonResult GetListData(EmergencyModel.ActionRequest param)
        {
            GridData data = EmergencyModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["laporan_simulasi"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "laporan_simulasi", data.data.Rows[i]["laporan_simulasi"].ToString());
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
                    EmergencyModel.ViewModel fieldModel = new EmergencyModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = EmergencyModel.GetViewData(PkValue);
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
                                fieldModel.company_id = dr["company_id"].ToString();
                                fieldModel.company_id_text = dr["company_id_text"].ToString();
                                fieldModel.alamat = dr["alamat"].ToString();
                                fieldModel.mgr_rep_id = dr["mgr_rep_id"].ToString();
                                fieldModel.mgr_rep_id_text = dr["mgr_rep_id_text"].ToString();
                                fieldModel.mgr_rep_telepon = dr["mgr_rep_telepon"].ToString();
                                fieldModel.customer = dr["customer"].ToString();
                                fieldModel.jenis_kegiatan_id = dr["jenis_kegiatan_id"].ToString();
                                fieldModel.jenis_kegiatan_id_text = dr["jenis_kegiatan_id_text"].ToString();
                                fieldModel.jenis_bangunan_id = dr["jenis_bangunan_id"].ToString();
                                fieldModel.jenis_bangunan_nama = dr["jenis_bangunan_nama"].ToString();
                                fieldModel.luas = String.Format("{0:N2}", dr["luas"]);
                                fieldModel.status_kepemilikan_id = dr["status_kepemilikan_id"].ToString();
                                fieldModel.status_kepemilikan_id_text = dr["status_kepemilikan_id_text"].ToString();
                                fieldModel.pelaksanaan_id = dr["pelaksanaan_id"].ToString();
                                fieldModel.pelaksanaan_id_text = dr["pelaksanaan_id_text"].ToString();
                                fieldModel.kategori_simulasi_darurat_id = dr["kategori_simulasi_darurat_id"].ToString();
                                fieldModel.kategori_simulasi_darurat_id_text = dr["kategori_simulasi_darurat_id_text"].ToString();
                                fieldModel.judul_kegiatan = dr["judul_kegiatan"].ToString();
                                fieldModel.tgl_kegiatan = dr["tgl_kegiatan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kegiatan"]) : "";
                                fieldModel.lokasi = dr["lokasi"].ToString();
                                fieldModel.eksternal_terlibat = dr["eksternal_terlibat"].ToString();
                                fieldModel.evaluasi_simulasi = dr["evaluasi_simulasi"].ToString();
                                fieldModel.laporan_simulasi = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "laporan_simulasi", dr["laporan_simulasi"].ToString());
                                fieldModel.total_peserta_terlibat = dr["total_peserta_terlibat"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["emg_id"] = fieldModel.id;
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
        public IActionResult Add(EmergencyModel.ViewModel fieldModel)
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
                    //EmergencyModel.ViewModel fieldModel = new EmergencyModel.ViewModel();
                    fieldModel.id = EmergencyModel.GetNewId().ToString();
                    fieldModel.laporan_simulasi_init = "[]";
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
                    DataTable data = EmergencyModel.GetViewData(PkValue);
                    EmergencyModel.ViewModel fieldModel = new EmergencyModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = EmergencyModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.alamat = dr["alamat"].ToString();
                        fieldModel.mgr_rep_id = dr["mgr_rep_id"].ToString();
                        fieldModel.mgr_rep_telepon = dr["mgr_rep_telepon"].ToString();
                        fieldModel.customer = dr["customer"].ToString();
                        fieldModel.jenis_kegiatan_id = dr["jenis_kegiatan_id"].ToString();
                        fieldModel.jenis_bangunan_id = dr["jenis_bangunan_id"].ToString();
                        fieldModel.jenis_bangunan_nama = dr["jenis_bangunan_nama"].ToString();
                        fieldModel.luas = dr["luas"].ToString();
                        fieldModel.status_kepemilikan_id = dr["status_kepemilikan_id"].ToString();
                        fieldModel.pelaksanaan_id = dr["pelaksanaan_id"].ToString();
                        fieldModel.kategori_simulasi_darurat_id = dr["kategori_simulasi_darurat_id"].ToString();
                        fieldModel.judul_kegiatan = dr["judul_kegiatan"].ToString();
                        fieldModel.tgl_kegiatan = dr["tgl_kegiatan"].ToString();
                        fieldModel.lokasi = dr["lokasi"].ToString();
                        fieldModel.eksternal_terlibat = dr["eksternal_terlibat"].ToString();
                        fieldModel.evaluasi_simulasi = dr["evaluasi_simulasi"].ToString();
                        fieldModel.laporan_simulasi = dr["laporan_simulasi"].ToString();
                        fieldModel.total_peserta_terlibat = dr["total_peserta_terlibat"].ToString();
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
                    DataTable data = EmergencyModel.GetViewData(PkValue);
                    EmergencyModel.ViewModel fieldModel = new EmergencyModel.ViewModel();
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
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.company_id_text = dr["company_id_text"].ToString();
                        fieldModel.alamat = dr["alamat"].ToString();
                        fieldModel.mgr_rep_id = dr["mgr_rep_id"].ToString();
                        fieldModel.mgr_rep_id_text = dr["mgr_rep_id_text"].ToString();
                        fieldModel.mgr_rep_telepon = dr["mgr_rep_telepon"].ToString();
                        fieldModel.customer = dr["customer"].ToString();
                        fieldModel.jenis_kegiatan_id = dr["jenis_kegiatan_id"].ToString();
                        fieldModel.jenis_kegiatan_id_text = dr["jenis_kegiatan_id_text"].ToString();
                        fieldModel.jenis_bangunan_id = dr["jenis_bangunan_id"].ToString();
                        fieldModel.jenis_bangunan_nama = dr["jenis_bangunan_nama"].ToString();
                        fieldModel.luas = dr["luas"].ToString();
                        fieldModel.status_kepemilikan_id = dr["status_kepemilikan_id"].ToString();
                        fieldModel.status_kepemilikan_id_text = dr["status_kepemilikan_id_text"].ToString();
                        fieldModel.pelaksanaan_id = dr["pelaksanaan_id"].ToString();
                        fieldModel.pelaksanaan_id_text = dr["pelaksanaan_id_text"].ToString();
                        fieldModel.kategori_simulasi_darurat_id = dr["kategori_simulasi_darurat_id"].ToString();
                        fieldModel.kategori_simulasi_darurat_id_text = dr["kategori_simulasi_darurat_id_text"].ToString();
                        fieldModel.judul_kegiatan = dr["judul_kegiatan"].ToString();
                        fieldModel.tgl_kegiatan = dr["tgl_kegiatan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kegiatan"]) : "";
                        fieldModel.dt_tgl_kegiatan = dr["tgl_kegiatan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kegiatan"]) : "";
                        fieldModel.lokasi = dr["lokasi"].ToString();
                        fieldModel.eksternal_terlibat = dr["eksternal_terlibat"].ToString();
                        fieldModel.evaluasi_simulasi = dr["evaluasi_simulasi"].ToString();
                        string[] init_laporan_simulasi = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "laporan_simulasi", PkValue, dr["laporan_simulasi"].ToString());
                        fieldModel.laporan_simulasi = init_laporan_simulasi[0];
                        fieldModel.laporan_simulasi_init = init_laporan_simulasi[1];
                        fieldModel.total_peserta_terlibat = dr["total_peserta_terlibat"].ToString();
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
        public JsonResult Insert(EmergencyModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = EmergencyModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["alamat"] = AntiXss.HtmlEncode(fieldModel.alamat);
                    data["mgr_rep_id"] = fieldModel.mgr_rep_id;
                    data["mgr_rep_telepon"] = AntiXss.HtmlEncode(fieldModel.mgr_rep_telepon);
                    data["customer"] = fieldModel.customer;
                    data["jenis_kegiatan_id"] = fieldModel.jenis_kegiatan_id;
                    data["jenis_bangunan_id"] = fieldModel.jenis_bangunan_id;
                    data["jenis_bangunan_nama"] = AntiXss.HtmlEncode(fieldModel.jenis_bangunan_nama);
                    data["luas"] = fieldModel.luas != null ? fieldModel.luas.Replace(",",".") : "";
                    data["status_kepemilikan_id"] = fieldModel.status_kepemilikan_id;
                    data["pelaksanaan_id"] = fieldModel.pelaksanaan_id;
                    data["kategori_simulasi_darurat_id"] = fieldModel.kategori_simulasi_darurat_id;
                    data["judul_kegiatan"] = AntiXss.HtmlEncode(fieldModel.judul_kegiatan);
                    data["tgl_kegiatan"] = fieldModel.tgl_kegiatan;
                    data["lokasi"] = AntiXss.HtmlEncode(fieldModel.lokasi);
                    data["eksternal_terlibat"] = fieldModel.eksternal_terlibat;
                    data["evaluasi_simulasi"] = AntiXss.HtmlEncode(fieldModel.evaluasi_simulasi);
                    data["laporan_simulasi"] = AntiXss.HtmlEncode(fieldModel.laporan_simulasi);
                    data["total_peserta_terlibat"] = fieldModel.total_peserta_terlibat;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = EmergencyModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(EmergencyModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(EmergencyModel.GetData(PkValue));
                    data_old["luas"] = data_old["luas"].ToString() != ""  ? data_old["luas"].ToString().Replace(",", ".") : "";
                    data_old["tgl_kegiatan"] = data_old["tgl_kegiatan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kegiatan"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(EmergencyModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["alamat"] = AntiXss.HtmlEncode(fieldModel.alamat);
                    data["mgr_rep_id"] = fieldModel.mgr_rep_id;
                    data["mgr_rep_telepon"] = AntiXss.HtmlEncode(fieldModel.mgr_rep_telepon);
                    data["customer"] = fieldModel.customer;
                    data["jenis_kegiatan_id"] = fieldModel.jenis_kegiatan_id;
                    data["jenis_bangunan_id"] = fieldModel.jenis_bangunan_id;
                    data["jenis_bangunan_nama"] = AntiXss.HtmlEncode(fieldModel.jenis_bangunan_nama);
                    data["luas"] = fieldModel.luas != null ? fieldModel.luas.ToString().Replace(",", ".") : "";
                    data["status_kepemilikan_id"] = fieldModel.status_kepemilikan_id;
                    data["pelaksanaan_id"] = fieldModel.pelaksanaan_id;
                    data["kategori_simulasi_darurat_id"] = fieldModel.kategori_simulasi_darurat_id;
                    data["judul_kegiatan"] = AntiXss.HtmlEncode(fieldModel.judul_kegiatan);
                    data["tgl_kegiatan"] = fieldModel.tgl_kegiatan;
                    data["lokasi"] = AntiXss.HtmlEncode(fieldModel.lokasi);
                    data["eksternal_terlibat"] = fieldModel.eksternal_terlibat;
                    data["evaluasi_simulasi"] = AntiXss.HtmlEncode(fieldModel.evaluasi_simulasi);
                    data["laporan_simulasi"] = AntiXss.HtmlEncode(fieldModel.laporan_simulasi);
                    data["total_peserta_terlibat"] = fieldModel.total_peserta_terlibat;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = EmergencyModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(EmergencyModel.GetData(PkValue));
						if (data != null)
						{
							result = EmergencyModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = EmergencyModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_laporan_simulasi(IEnumerable<IFormFile> laporan_simulasi_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "laporan_simulasi");
            if (laporan_simulasi_file != null)
            {
                foreach (var file in laporan_simulasi_file)
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

        public ActionResult remove_laporan_simulasi(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "laporan_simulasi");
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