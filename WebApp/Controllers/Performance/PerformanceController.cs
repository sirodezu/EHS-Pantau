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
    
    public class PerformanceController : Controller
    {
        private string _rule_view = "PerformanceView";
        private string _rule_add = "PerformanceAdd";
        private string _rule_edit = "PerformanceEdit";
        private string _rule_delete = "PerformanceDelete";
        private string _rule_edit_all = "PerformanceEditAll";
        private string _rule_delete_all = "PerformanceDeleteAll";
        private string _path_controler = "/Performance/";
        private string _path_view = "/Views/Performance/Performance/";
        private readonly string _table_name = "ta_performance";
        private readonly string _table_title = "Performance";

		private static IHostingEnvironment _hostingEnvironment;
        public PerformanceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(PerformanceModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = PerformanceModel._pkKey;
                    ViewData["displayItem"] = PerformanceModel.GetDisplayItem();
                    ViewData["column_att"] = PerformanceModel.GetGridColumn();
                    ViewData["param"] = PerformanceModel.GetListParam();
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
        public JsonResult GetListData(PerformanceModel.ActionRequest param)
        {
            GridData data = PerformanceModel.GetListData(HttpContext, param);
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
                    PerformanceModel.ViewModel fieldModel = new PerformanceModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = PerformanceModel.GetViewData(PkValue);
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
                                fieldModel.person_id = dr["person_id"].ToString();
                                fieldModel.person_id_text = dr["person_id_text"].ToString();
                                fieldModel.kategori_kejadian_id = dr["kategori_kejadian_id"].ToString();
                                fieldModel.kategori_kejadian_id_text = dr["kategori_kejadian_id_text"].ToString();
                                fieldModel.bidang_penghargaan_id = dr["bidang_penghargaan_id"].ToString();
                                fieldModel.bidang_penghargaan_id_text = dr["bidang_penghargaan_id_text"].ToString();
                                fieldModel.bidang_penghargaan_deskripsi = dr["bidang_penghargaan_deskripsi"].ToString();
                                fieldModel.tgl_kasih_penghargaan = dr["tgl_kasih_penghargaan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kasih_penghargaan"]) : "";
                                fieldModel.pemberi_penghargaan = dr["pemberi_penghargaan"].ToString();
                                fieldModel.pemberi_penghargaan_text = dr["pemberi_penghargaan_text"].ToString();
                                fieldModel.sakit_jml_hari = dr["sakit_jml_hari"].ToString();
                                fieldModel.sakit_deskripsi = dr["sakit_deskripsi"].ToString();
                                fieldModel.judul_kejadian = dr["judul_kejadian"].ToString();
                                fieldModel.no_kejadian = dr["no_kejadian"].ToString();
                                fieldModel.tgl_kejadian = dr["tgl_kejadian"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kejadian"]) : "";
                                fieldModel.waktu_kejadian_id = dr["waktu_kejadian_id"].ToString();
                                fieldModel.waktu_kejadian_id_text = dr["waktu_kejadian_id_text"].ToString();
                                fieldModel.lokasi_kejadian_id = dr["lokasi_kejadian_id"].ToString();
                                fieldModel.lokasi_kejadian_id_text = dr["lokasi_kejadian_id_text"].ToString();
                                fieldModel.jenis_pelanggaran_id = dr["jenis_pelanggaran_id"].ToString();
                                fieldModel.jenis_pelanggaran_id_text = dr["jenis_pelanggaran_id_text"].ToString();
                                fieldModel.resiko_kejadian_id = dr["resiko_kejadian_id"].ToString();
                                fieldModel.resiko_kejadian_id_text = dr["resiko_kejadian_id_text"].ToString();
                                fieldModel.deskripsi_sanksi = dr["deskripsi_sanksi"].ToString();
                                fieldModel.masa_berlaku_sanksi = dr["masa_berlaku_sanksi"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["masa_berlaku_sanksi"]) : "";
                                fieldModel.masa_berakhir_sanksi = dr["masa_berakhir_sanksi"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["masa_berakhir_sanksi"]) : "";
                                fieldModel.deskripsi_kejadian = dr["deskripsi_kejadian"].ToString();
                                fieldModel.pemberi_sanksi_id = dr["pemberi_sanksi_id"].ToString();
                                fieldModel.pemberi_sanksi_id_text = dr["pemberi_sanksi_id_text"].ToString();
                                fieldModel.nama_pemberi_sanksi = dr["nama_pemberi_sanksi"].ToString();
                                fieldModel.company_code_pemberi_sanksi = dr["company_code_pemberi_sanksi"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                                fieldModel.sakit_tgl_mulai = dr["sakit_tgl_mulai"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["sakit_tgl_mulai"]) : "";
                                fieldModel.sakit_tgl_akhir = dr["sakit_tgl_akhir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["sakit_tgl_akhir"]) : "";
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
        public IActionResult Add(PerformanceModel.ViewModel fieldModel)
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
                    //PerformanceModel.ViewModel fieldModel = new PerformanceModel.ViewModel();
                    fieldModel.id = PerformanceModel.GetNewId().ToString();
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
                    DataTable data = PerformanceModel.GetViewData(PkValue);
                    PerformanceModel.ViewModel fieldModel = new PerformanceModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = PerformanceModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.person_id = dr["person_id"].ToString();
                        fieldModel.kategori_kejadian_id = dr["kategori_kejadian_id"].ToString();
                        fieldModel.bidang_penghargaan_id = dr["bidang_penghargaan_id"].ToString();
                        fieldModel.bidang_penghargaan_deskripsi = dr["bidang_penghargaan_deskripsi"].ToString();
                        fieldModel.tgl_kasih_penghargaan = dr["tgl_kasih_penghargaan"].ToString();
                        fieldModel.pemberi_penghargaan = dr["pemberi_penghargaan"].ToString();
                        fieldModel.sakit_jml_hari = dr["sakit_jml_hari"].ToString();
                        fieldModel.sakit_deskripsi = dr["sakit_deskripsi"].ToString();
                        fieldModel.judul_kejadian = dr["judul_kejadian"].ToString();
                        fieldModel.no_kejadian = dr["no_kejadian"].ToString();
                        fieldModel.tgl_kejadian = dr["tgl_kejadian"].ToString();
                        fieldModel.waktu_kejadian_id = dr["waktu_kejadian_id"].ToString();
                        fieldModel.lokasi_kejadian_id = dr["lokasi_kejadian_id"].ToString();
                        fieldModel.jenis_pelanggaran_id = dr["jenis_pelanggaran_id"].ToString();
                        fieldModel.resiko_kejadian_id = dr["resiko_kejadian_id"].ToString();
                        fieldModel.deskripsi_sanksi = dr["deskripsi_sanksi"].ToString();
                        fieldModel.masa_berlaku_sanksi = dr["masa_berlaku_sanksi"].ToString();
                        fieldModel.masa_berakhir_sanksi = dr["masa_berakhir_sanksi"].ToString();
                        fieldModel.deskripsi_kejadian = dr["deskripsi_kejadian"].ToString();
                        fieldModel.pemberi_sanksi_id = dr["pemberi_sanksi_id"].ToString();
                        fieldModel.nama_pemberi_sanksi = dr["nama_pemberi_sanksi"].ToString();
                        fieldModel.company_code_pemberi_sanksi = dr["company_code_pemberi_sanksi"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.sakit_tgl_mulai = dr["sakit_tgl_mulai"].ToString();
                        fieldModel.sakit_tgl_akhir = dr["sakit_tgl_akhir"].ToString();
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
                    DataTable data = PerformanceModel.GetViewData(PkValue);
                    PerformanceModel.ViewModel fieldModel = new PerformanceModel.ViewModel();
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
                        fieldModel.person_id = dr["person_id"].ToString();
                        fieldModel.person_id_text = dr["person_id_text"].ToString();
                        fieldModel.kategori_kejadian_id = dr["kategori_kejadian_id"].ToString();
                        fieldModel.kategori_kejadian_id_text = dr["kategori_kejadian_id_text"].ToString();
                        fieldModel.bidang_penghargaan_id = dr["bidang_penghargaan_id"].ToString();
                        fieldModel.bidang_penghargaan_id_text = dr["bidang_penghargaan_id_text"].ToString();
                        fieldModel.bidang_penghargaan_deskripsi = dr["bidang_penghargaan_deskripsi"].ToString();
                        fieldModel.tgl_kasih_penghargaan = dr["tgl_kasih_penghargaan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kasih_penghargaan"]) : "";
                        fieldModel.dt_tgl_kasih_penghargaan = dr["tgl_kasih_penghargaan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kasih_penghargaan"]) : "";
                        fieldModel.pemberi_penghargaan = dr["pemberi_penghargaan"].ToString();
                        fieldModel.pemberi_penghargaan_text = dr["pemberi_penghargaan_text"].ToString();
                        fieldModel.sakit_jml_hari = dr["sakit_jml_hari"].ToString();
                        fieldModel.sakit_deskripsi = dr["sakit_deskripsi"].ToString();
                        fieldModel.judul_kejadian = dr["judul_kejadian"].ToString();
                        fieldModel.no_kejadian = dr["no_kejadian"].ToString();
                        fieldModel.tgl_kejadian = dr["tgl_kejadian"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kejadian"]) : "";
                        fieldModel.dt_tgl_kejadian = dr["tgl_kejadian"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kejadian"]) : "";
                        fieldModel.waktu_kejadian_id = dr["waktu_kejadian_id"].ToString();
                        fieldModel.waktu_kejadian_id_text = dr["waktu_kejadian_id_text"].ToString();
                        fieldModel.lokasi_kejadian_id = dr["lokasi_kejadian_id"].ToString();
                        fieldModel.lokasi_kejadian_id_text = dr["lokasi_kejadian_id_text"].ToString();
                        fieldModel.jenis_pelanggaran_id = dr["jenis_pelanggaran_id"].ToString();
                        fieldModel.jenis_pelanggaran_id_text = dr["jenis_pelanggaran_id_text"].ToString();
                        fieldModel.resiko_kejadian_id = dr["resiko_kejadian_id"].ToString();
                        fieldModel.resiko_kejadian_id_text = dr["resiko_kejadian_id_text"].ToString();
                        fieldModel.deskripsi_sanksi = dr["deskripsi_sanksi"].ToString();
                        fieldModel.masa_berlaku_sanksi = dr["masa_berlaku_sanksi"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["masa_berlaku_sanksi"]) : "";
                        fieldModel.dt_masa_berlaku_sanksi = dr["masa_berlaku_sanksi"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["masa_berlaku_sanksi"]) : "";
                        fieldModel.masa_berakhir_sanksi = dr["masa_berakhir_sanksi"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["masa_berakhir_sanksi"]) : "";
                        fieldModel.dt_masa_berakhir_sanksi = dr["masa_berakhir_sanksi"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["masa_berakhir_sanksi"]) : "";
                        fieldModel.deskripsi_kejadian = dr["deskripsi_kejadian"].ToString();
                        fieldModel.pemberi_sanksi_id = dr["pemberi_sanksi_id"].ToString();
                        fieldModel.pemberi_sanksi_id_text = dr["pemberi_sanksi_id_text"].ToString();
                        fieldModel.nama_pemberi_sanksi = dr["nama_pemberi_sanksi"].ToString();
                        fieldModel.company_code_pemberi_sanksi = dr["company_code_pemberi_sanksi"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.sakit_tgl_mulai = dr["sakit_tgl_mulai"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["sakit_tgl_mulai"]) : "";
                        fieldModel.dt_sakit_tgl_mulai = dr["sakit_tgl_mulai"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["sakit_tgl_mulai"]) : "";
                        fieldModel.sakit_tgl_akhir = dr["sakit_tgl_akhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["sakit_tgl_akhir"]) : "";
                        fieldModel.dt_sakit_tgl_akhir = dr["sakit_tgl_akhir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["sakit_tgl_akhir"]) : "";
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
        public JsonResult Insert(PerformanceModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = PerformanceModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["person_id"] = fieldModel.person_id;
                    data["kategori_kejadian_id"] = fieldModel.kategori_kejadian_id;
                    data["bidang_penghargaan_id"] = fieldModel.bidang_penghargaan_id;
                    data["bidang_penghargaan_deskripsi"] = AntiXss.HtmlEncode(fieldModel.bidang_penghargaan_deskripsi);
                    data["tgl_kasih_penghargaan"] = fieldModel.tgl_kasih_penghargaan;
                    data["pemberi_penghargaan"] = fieldModel.pemberi_penghargaan;
                    data["sakit_jml_hari"] = fieldModel.sakit_jml_hari;
                    data["sakit_deskripsi"] = AntiXss.HtmlEncode(fieldModel.sakit_deskripsi);
                    data["judul_kejadian"] = AntiXss.HtmlEncode(fieldModel.judul_kejadian);
                    //
                    //data["no_kejadian"] = AntiXss.HtmlEncode(fieldModel.no_kejadian);
                    if (data["kategori_kejadian_id"].ToString() == "2")
                    {
                        string generatedNo = PerformanceModel.GetNoPerformance(HttpContext, fieldModel.ehs_area_id, fieldModel.jenis_pelanggaran_id, fieldModel.tgl_kejadian);
                        fieldModel.no_kejadian = generatedNo;
                        data["no_kejadian"] = AntiXss.HtmlEncode(fieldModel.no_kejadian);
                    } else
                    {
                        data["no_kejadian"] = "";
                    }
                    //
                    data["tgl_kejadian"] = fieldModel.tgl_kejadian;
                    data["waktu_kejadian_id"] = fieldModel.waktu_kejadian_id;
                    data["lokasi_kejadian_id"] = fieldModel.lokasi_kejadian_id;
                    data["jenis_pelanggaran_id"] = fieldModel.jenis_pelanggaran_id;
                    data["resiko_kejadian_id"] = fieldModel.resiko_kejadian_id;
                    data["deskripsi_sanksi"] = AntiXss.HtmlEncode(fieldModel.deskripsi_sanksi);
                    data["masa_berlaku_sanksi"] = fieldModel.masa_berlaku_sanksi;
                    data["masa_berakhir_sanksi"] = fieldModel.masa_berakhir_sanksi;
                    data["deskripsi_kejadian"] = AntiXss.HtmlEncode(fieldModel.deskripsi_kejadian);
                    data["pemberi_sanksi_id"] = fieldModel.pemberi_sanksi_id;
                    data["nama_pemberi_sanksi"] = AntiXss.HtmlEncode(fieldModel.nama_pemberi_sanksi);
                    data["company_code_pemberi_sanksi"] = AntiXss.HtmlEncode(fieldModel.company_code_pemberi_sanksi);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    data["sakit_tgl_mulai"] = fieldModel.sakit_tgl_mulai;
                    data["sakit_tgl_akhir"] = fieldModel.sakit_tgl_akhir;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = PerformanceModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(PerformanceModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(PerformanceModel.GetData(PkValue));
                    data_old["tgl_kasih_penghargaan"] = data_old["tgl_kasih_penghargaan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kasih_penghargaan"]) : "";
                    data_old["tgl_kejadian"] = data_old["tgl_kejadian"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kejadian"]) : "";
                    data_old["masa_berlaku_sanksi"] = data_old["masa_berlaku_sanksi"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["masa_berlaku_sanksi"]) : "";
                    data_old["masa_berakhir_sanksi"] = data_old["masa_berakhir_sanksi"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["masa_berakhir_sanksi"]) : "";
                    data_old["sakit_tgl_mulai"] = data_old["sakit_tgl_mulai"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["sakit_tgl_mulai"]) : "";
                    data_old["sakit_tgl_akhir"] = data_old["sakit_tgl_akhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["sakit_tgl_akhir"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(PerformanceModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["person_id"] = fieldModel.person_id;
                    data["kategori_kejadian_id"] = fieldModel.kategori_kejadian_id;
                    data["bidang_penghargaan_id"] = fieldModel.bidang_penghargaan_id;
                    data["bidang_penghargaan_deskripsi"] = AntiXss.HtmlEncode(fieldModel.bidang_penghargaan_deskripsi);
                    data["tgl_kasih_penghargaan"] = fieldModel.tgl_kasih_penghargaan;
                    data["pemberi_penghargaan"] = fieldModel.pemberi_penghargaan;
                    data["sakit_jml_hari"] = fieldModel.sakit_jml_hari;
                    data["sakit_deskripsi"] = AntiXss.HtmlEncode(fieldModel.sakit_deskripsi);
                    data["judul_kejadian"] = AntiXss.HtmlEncode(fieldModel.judul_kejadian);
                    data["no_kejadian"] = AntiXss.HtmlEncode(fieldModel.no_kejadian);
                    data["tgl_kejadian"] = fieldModel.tgl_kejadian;
                    data["waktu_kejadian_id"] = fieldModel.waktu_kejadian_id;
                    data["lokasi_kejadian_id"] = fieldModel.lokasi_kejadian_id;
                    data["jenis_pelanggaran_id"] = fieldModel.jenis_pelanggaran_id;
                    data["resiko_kejadian_id"] = fieldModel.resiko_kejadian_id;
                    data["deskripsi_sanksi"] = AntiXss.HtmlEncode(fieldModel.deskripsi_sanksi);
                    data["masa_berlaku_sanksi"] = fieldModel.masa_berlaku_sanksi;
                    data["masa_berakhir_sanksi"] = fieldModel.masa_berakhir_sanksi;
                    data["deskripsi_kejadian"] = AntiXss.HtmlEncode(fieldModel.deskripsi_kejadian);
                    data["pemberi_sanksi_id"] = fieldModel.pemberi_sanksi_id;
                    data["nama_pemberi_sanksi"] = AntiXss.HtmlEncode(fieldModel.nama_pemberi_sanksi);
                    data["company_code_pemberi_sanksi"] = AntiXss.HtmlEncode(fieldModel.company_code_pemberi_sanksi);
                    data["sakit_tgl_mulai"] = fieldModel.sakit_tgl_mulai;
                    data["sakit_tgl_akhir"] = fieldModel.sakit_tgl_akhir;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = PerformanceModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(PerformanceModel.GetData(PkValue));
						if (data != null)
						{
							result = PerformanceModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = PerformanceModel.LookupData(param);
            return Json(data);
        }

    }
}