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
    
    public class LegalController : Controller
    {
        private string _rule_view = "LegalView";
        private string _rule_add = "LegalAdd";
        private string _rule_edit = "LegalEdit";
        private string _rule_delete = "LegalDelete";
        private string _rule_edit_all = "LegalEditAll";
        private string _rule_delete_all = "LegalEditAll";
        private string _path_controler = "/Legal/";
        private string _path_view = "/Views/Legal/Legal/";
        private readonly string _table_name = "ta_legal";
        private readonly string _table_title = "Legal";

		private static IHostingEnvironment _hostingEnvironment;
        public LegalController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(LegalModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = LegalModel._pkKey;
                    ViewData["displayItem"] = LegalModel.GetDisplayItem();
                    ViewData["column_att"] = LegalModel.GetGridColumn();
                    ViewData["param"] = LegalModel.GetListParam();
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
        public JsonResult GetListData(LegalModel.ActionRequest param)
        {
            GridData data = LegalModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["file_path_bukti_lapor"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_bukti_lapor", data.data.Rows[i]["file_path_bukti_lapor"].ToString());
                data.data.Rows[i]["file_path_status_izin"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_status_izin", data.data.Rows[i]["file_path_status_izin"].ToString());
                data.data.Rows[i]["file_path_dokumen_izin"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_dokumen_izin", data.data.Rows[i]["file_path_dokumen_izin"].ToString());
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
                    LegalModel.ViewModel fieldModel = new LegalModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = LegalModel.GetViewData(PkValue);
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
                                fieldModel.kategori_perizinan_id = dr["kategori_perizinan_id"].ToString();
                                fieldModel.kategori_perizinan_id_text = dr["kategori_perizinan_id_text"].ToString();
                                fieldModel.jenis_perizinan_id = dr["jenis_perizinan_id"].ToString();
                                fieldModel.jenis_perizinan_id_text = dr["jenis_perizinan_id_text"].ToString();
                                fieldModel.jenis_limbah_tersimpan_id = dr["jenis_limbah_tersimpan_id"].ToString();
                                fieldModel.jenis_limbah_tersimpan_id_text = dr["jenis_limbah_tersimpan_id_text"].ToString();
                                fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                                fieldModel.periode_telah_dilaporkan_id = dr["periode_telah_dilaporkan_id"].ToString();
                                fieldModel.periode_telah_dilaporkan_id_text = dr["periode_telah_dilaporkan_id_text"].ToString();
                                fieldModel.tanggal_pelaporan = dr["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_pelaporan"]) : "";
                                fieldModel.file_path_bukti_lapor = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_bukti_lapor", dr["file_path_bukti_lapor"].ToString());
                                fieldModel.no_izin = dr["no_izin"].ToString();
                                fieldModel.institusi_pengesahan_izin_id = dr["institusi_pengesahan_izin_id"].ToString();
                                fieldModel.institusi_pengesahan_izin_id_text = dr["institusi_pengesahan_izin_id_text"].ToString();
                                fieldModel.tanggal_izin_terbit = dr["tanggal_izin_terbit"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_izin_terbit"]) : "";
                                fieldModel.tgl_masa_berlaku = dr["tgl_masa_berlaku"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_masa_berlaku"]) : "";
                                fieldModel.tanggal_habis_izin = dr["tanggal_habis_izin"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_habis_izin"]) : "";
                                fieldModel.status_izin_id = dr["status_izin_id"].ToString();
                                fieldModel.status_izin_id_text = dr["status_izin_id_text"].ToString();
                                fieldModel.file_path_status_izin = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_status_izin", dr["file_path_status_izin"].ToString());
                                fieldModel.file_path_dokumen_izin = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_dokumen_izin", dr["file_path_dokumen_izin"].ToString());
                                fieldModel.keterangan_izin = dr["keterangan_izin"].ToString();
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
        public IActionResult Add(LegalModel.ViewModel fieldModel)
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
                    //LegalModel.ViewModel fieldModel = new LegalModel.ViewModel();
                    fieldModel.id = LegalModel.GetNewId().ToString();
                    fieldModel.file_path_bukti_lapor_init = "[]";
                    fieldModel.file_path_status_izin_init = "[]";
                    fieldModel.file_path_dokumen_izin_init = "[]";
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
                    DataTable data = LegalModel.GetViewData(PkValue);
                    LegalModel.ViewModel fieldModel = new LegalModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = LegalModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.kategori_perizinan_id = dr["kategori_perizinan_id"].ToString();
                        fieldModel.jenis_perizinan_id = dr["jenis_perizinan_id"].ToString();
                        fieldModel.jenis_limbah_tersimpan_id = dr["jenis_limbah_tersimpan_id"].ToString();
                        fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                        fieldModel.periode_telah_dilaporkan_id = dr["periode_telah_dilaporkan_id"].ToString();
                        fieldModel.tanggal_pelaporan = dr["tanggal_pelaporan"].ToString();
                        fieldModel.file_path_bukti_lapor = dr["file_path_bukti_lapor"].ToString();
                        fieldModel.no_izin = dr["no_izin"].ToString();
                        fieldModel.institusi_pengesahan_izin_id = dr["institusi_pengesahan_izin_id"].ToString();
                        fieldModel.tanggal_izin_terbit = dr["tanggal_izin_terbit"].ToString();
                        fieldModel.tgl_masa_berlaku = dr["tgl_masa_berlaku"].ToString();
                        fieldModel.tanggal_habis_izin = dr["tanggal_habis_izin"].ToString();
                        fieldModel.status_izin_id = dr["status_izin_id"].ToString();
                        fieldModel.file_path_status_izin = dr["file_path_status_izin"].ToString();
                        fieldModel.file_path_dokumen_izin = dr["file_path_dokumen_izin"].ToString();
                        fieldModel.keterangan_izin = dr["keterangan_izin"].ToString();
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
                    DataTable data = LegalModel.GetViewData(PkValue);
                    LegalModel.ViewModel fieldModel = new LegalModel.ViewModel();
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
                        fieldModel.kategori_perizinan_id = dr["kategori_perizinan_id"].ToString();
                        fieldModel.kategori_perizinan_id_text = dr["kategori_perizinan_id_text"].ToString();
                        fieldModel.jenis_perizinan_id = dr["jenis_perizinan_id"].ToString();
                        fieldModel.jenis_perizinan_id_text = dr["jenis_perizinan_id_text"].ToString();
                        fieldModel.jenis_limbah_tersimpan_id = dr["jenis_limbah_tersimpan_id"].ToString();
                        fieldModel.jenis_limbah_tersimpan_id_text = dr["jenis_limbah_tersimpan_id_text"].ToString();
                        fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                        fieldModel.periode_telah_dilaporkan_id = dr["periode_telah_dilaporkan_id"].ToString();
                        fieldModel.periode_telah_dilaporkan_id_text = dr["periode_telah_dilaporkan_id_text"].ToString();
                        fieldModel.tanggal_pelaporan = dr["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tanggal_pelaporan"]) : "";
                        fieldModel.dt_tanggal_pelaporan = dr["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_pelaporan"]) : "";
                        string[] init_file_path_bukti_lapor = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_bukti_lapor", PkValue, dr["file_path_bukti_lapor"].ToString());
                        fieldModel.file_path_bukti_lapor = init_file_path_bukti_lapor[0];
                        fieldModel.file_path_bukti_lapor_init = init_file_path_bukti_lapor[1];
                        fieldModel.no_izin = dr["no_izin"].ToString();
                        fieldModel.institusi_pengesahan_izin_id = dr["institusi_pengesahan_izin_id"].ToString();
                        fieldModel.institusi_pengesahan_izin_id_text = dr["institusi_pengesahan_izin_id_text"].ToString();
                        fieldModel.tanggal_izin_terbit = dr["tanggal_izin_terbit"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tanggal_izin_terbit"]) : "";
                        fieldModel.dt_tanggal_izin_terbit = dr["tanggal_izin_terbit"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_izin_terbit"]) : "";
                        fieldModel.tgl_masa_berlaku = dr["tgl_masa_berlaku"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_masa_berlaku"]) : "";
                        fieldModel.dt_tgl_masa_berlaku = dr["tgl_masa_berlaku"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_masa_berlaku"]) : "";
                        fieldModel.tanggal_habis_izin = dr["tanggal_habis_izin"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tanggal_habis_izin"]) : "";
                        fieldModel.dt_tanggal_habis_izin = dr["tanggal_habis_izin"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal_habis_izin"]) : "";
                        fieldModel.status_izin_id = dr["status_izin_id"].ToString();
                        fieldModel.status_izin_id_text = dr["status_izin_id_text"].ToString();
                        string[] init_file_path_status_izin = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_status_izin", PkValue, dr["file_path_status_izin"].ToString());
                        fieldModel.file_path_status_izin = init_file_path_status_izin[0];
                        fieldModel.file_path_status_izin_init = init_file_path_status_izin[1];
                        string[] init_file_path_dokumen_izin = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_dokumen_izin", PkValue, dr["file_path_dokumen_izin"].ToString());
                        fieldModel.file_path_dokumen_izin = init_file_path_dokumen_izin[0];
                        fieldModel.file_path_dokumen_izin_init = init_file_path_dokumen_izin[1];
                        fieldModel.keterangan_izin = dr["keterangan_izin"].ToString();
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
        public JsonResult Insert(LegalModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = LegalModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["kategori_perizinan_id"] = fieldModel.kategori_perizinan_id;
                    data["jenis_perizinan_id"] = fieldModel.jenis_perizinan_id;
                    data["jenis_limbah_tersimpan_id"] = fieldModel.jenis_limbah_tersimpan_id;
                    data["kode_limbah"] = AntiXss.HtmlEncode(fieldModel.kode_limbah);
                    data["periode_telah_dilaporkan_id"] = fieldModel.periode_telah_dilaporkan_id;
                    data["tanggal_pelaporan"] = fieldModel.tanggal_pelaporan;
                    data["file_path_bukti_lapor"] = AntiXss.HtmlEncode(fieldModel.file_path_bukti_lapor);
                    data["no_izin"] = AntiXss.HtmlEncode(fieldModel.no_izin);
                    data["institusi_pengesahan_izin_id"] = fieldModel.institusi_pengesahan_izin_id;
                    data["tanggal_izin_terbit"] = fieldModel.tanggal_izin_terbit;
                    data["tgl_masa_berlaku"] = fieldModel.tgl_masa_berlaku;
                    data["tanggal_habis_izin"] = fieldModel.tanggal_habis_izin;
                    data["status_izin_id"] = fieldModel.status_izin_id;
                    data["file_path_status_izin"] = AntiXss.HtmlEncode(fieldModel.file_path_status_izin);
                    data["file_path_dokumen_izin"] = AntiXss.HtmlEncode(fieldModel.file_path_dokumen_izin);
                    data["keterangan_izin"] = AntiXss.HtmlEncode(fieldModel.keterangan_izin);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = LegalModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(LegalModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(LegalModel.GetData(PkValue));
                    data_old["tanggal_pelaporan"] = data_old["tanggal_pelaporan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tanggal_pelaporan"]) : "";
                    data_old["tanggal_izin_terbit"] = data_old["tanggal_izin_terbit"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tanggal_izin_terbit"]) : "";
                    data_old["tanggal_habis_izin"] = data_old["tanggal_habis_izin"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tanggal_habis_izin"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(LegalModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["kategori_perizinan_id"] = fieldModel.kategori_perizinan_id;
                    data["jenis_perizinan_id"] = fieldModel.jenis_perizinan_id;
                    data["jenis_limbah_tersimpan_id"] = fieldModel.jenis_limbah_tersimpan_id;
                    data["kode_limbah"] = AntiXss.HtmlEncode(fieldModel.kode_limbah);
                    data["periode_telah_dilaporkan_id"] = fieldModel.periode_telah_dilaporkan_id;
                    data["tanggal_pelaporan"] = fieldModel.tanggal_pelaporan;
                    data["file_path_bukti_lapor"] = AntiXss.HtmlEncode(fieldModel.file_path_bukti_lapor);
                    data["no_izin"] = AntiXss.HtmlEncode(fieldModel.no_izin);
                    data["institusi_pengesahan_izin_id"] = fieldModel.institusi_pengesahan_izin_id;
                    data["tanggal_izin_terbit"] = fieldModel.tanggal_izin_terbit;
                    data["tgl_masa_berlaku"] = fieldModel.tgl_masa_berlaku;
                    data["tanggal_habis_izin"] = fieldModel.tanggal_habis_izin;
                    data["status_izin_id"] = fieldModel.status_izin_id;
                    data["file_path_status_izin"] = AntiXss.HtmlEncode(fieldModel.file_path_status_izin);
                    data["file_path_dokumen_izin"] = AntiXss.HtmlEncode(fieldModel.file_path_dokumen_izin);
                    data["keterangan_izin"] = AntiXss.HtmlEncode(fieldModel.keterangan_izin);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = LegalModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(LegalModel.GetData(PkValue));
						if (data != null)
						{
							result = LegalModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = LegalModel.LookupData(param);
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

        public async Task<IActionResult> save_file_path_status_izin(IEnumerable<IFormFile> file_path_status_izin_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_status_izin");
            if (file_path_status_izin_file != null)
            {
                foreach (var file in file_path_status_izin_file)
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

        public ActionResult remove_file_path_status_izin(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_status_izin");
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

        public async Task<IActionResult> save_file_path_dokumen_izin(IEnumerable<IFormFile> file_path_dokumen_izin_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_dokumen_izin");
            if (file_path_dokumen_izin_file != null)
            {
                foreach (var file in file_path_dokumen_izin_file)
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

        public ActionResult remove_file_path_dokumen_izin(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_dokumen_izin");
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