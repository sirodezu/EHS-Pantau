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
    
    public class AccidentController : Controller
    {
        private string _rule_view = "AccidentView";
        private string _rule_add = "AccidentAdd";
        private string _rule_edit = "AccidentEdit";
        private string _rule_delete = "AccidentDelete";
        private string _rule_edit_all = "AccidentEditAll";
        private string _rule_delete_all = "AccidentDeleteAll";
        private string _path_controler = "/Accident/";
        private string _path_view = "/Views/Accident/Accident/";
        private readonly string _table_name = "ta_acc";
        private readonly string _table_title = "Accident";

		private static IHostingEnvironment _hostingEnvironment;
        public AccidentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(AccidentModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = AccidentModel._pkKey;
                    ViewData["displayItem"] = AccidentModel.GetDisplayItem();
                    ViewData["column_att"] = AccidentModel.GetGridColumn();
                    ViewData["param"] = AccidentModel.GetListParam();
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
        public JsonResult GetListData(AccidentModel.ActionRequest param)
        {
            GridData data = AccidentModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["lampiran"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "lampiran", data.data.Rows[i]["lampiran"].ToString());
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
                    AccidentModel.ViewModel fieldModel = new AccidentModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AccidentModel.GetViewData(PkValue);
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
                                fieldModel.jenis_kejadian_id = dr["jenis_kejadian_id"].ToString();
                                fieldModel.jenis_kejadian_id_text = dr["jenis_kejadian_id_text"].ToString();
                                fieldModel.judul_kejadian = dr["judul_kejadian"].ToString();
                                fieldModel.nomor_kejadian = dr["nomor_kejadian"].ToString();
                                fieldModel.lokasi_kejadian_id = dr["lokasi_kejadian_id"].ToString();
                                fieldModel.lokasi_kejadian_id_text = dr["lokasi_kejadian_id_text"].ToString();
                                fieldModel.tgl_kejadian = dr["tgl_kejadian"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kejadian"]) : "";
                                fieldModel.waktu_kejadian_id = dr["waktu_kejadian_id"].ToString();
                                fieldModel.waktu_kejadian_id_text = dr["waktu_kejadian_id_text"].ToString();
                                fieldModel.deskripsi_kejadian = dr["deskripsi_kejadian"].ToString();
                                fieldModel.kategori_kejadian_id = dr["kategori_kejadian_id"].ToString();
                                fieldModel.kategori_kejadian_nama = dr["kategori_kejadian_nama"].ToString();
                                fieldModel.jumlah_korban = dr["jumlah_korban"].ToString();
                                fieldModel.jumlah_kerugian = String.Format("{0:N2}", dr["jumlah_kerugian"]);
                                fieldModel.lampiran = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "lampiran", dr["lampiran"].ToString());
                                fieldModel.status_jam_kerja = dr["status_jam_kerja"].ToString();
                                fieldModel.status_jam_kerja_text = dr["status_jam_kerja_text"].ToString();
                                fieldModel.jml_hari_hilang = dr["jml_hari_hilang"].ToString();
                                fieldModel.dampak_fasilitas_id = dr["dampak_fasilitas_id"].ToString();
                                fieldModel.dampak_fasilitas_nama = dr["dampak_fasilitas_nama"].ToString();
                                fieldModel.dampak_lingkungan_id = dr["dampak_lingkungan_id"].ToString();
                                fieldModel.dampak_lingkungan_nama = dr["dampak_lingkungan_nama"].ToString();
                                fieldModel.tipe_celaka_id = dr["tipe_celaka_id"].ToString();
                                fieldModel.tipe_celaka_nama = dr["tipe_celaka_nama"].ToString();
                                fieldModel.faktor_pribadi_id = dr["faktor_pribadi_id"].ToString();
                                fieldModel.faktor_pribadi_nama = dr["faktor_pribadi_nama"].ToString();
                                fieldModel.faktor_kerja_id = dr["faktor_kerja_id"].ToString();
                                fieldModel.faktor_kerja_nama = dr["faktor_kerja_nama"].ToString();
                                fieldModel.tindak_bahaya_id = dr["tindak_bahaya_id"].ToString();
                                fieldModel.tindak_bahaya_nama = dr["tindak_bahaya_nama"].ToString();
                                fieldModel.kondisi_bahaya_id = dr["kondisi_bahaya_id"].ToString();
                                fieldModel.kondisi_bahaya_nama = dr["kondisi_bahaya_nama"].ToString();
                                fieldModel.kompen_rugi_id = dr["kompen_rugi_id"].ToString();
                                fieldModel.kompen_rugi_nama = dr["kompen_rugi_nama"].ToString();
                                fieldModel.deskripsi_kerugian = dr["deskripsi_kerugian"].ToString();
                                fieldModel.biaya_kerugian = String.Format("{0:N2}", dr["biaya_kerugian"]);
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["acc_id"] = fieldModel.id;
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
        public IActionResult Add(AccidentModel.ViewModel fieldModel)
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
                    //AccidentModel.ViewModel fieldModel = new AccidentModel.ViewModel();
                    fieldModel.id = AccidentModel.GetNewId().ToString();
                    fieldModel.lampiran_init = "[]";
                    //
                    //string generatedNo = AccidentModel.GetNoAccident(HttpContext, fieldModel.jenis_kejadian_id);
                    //fieldModel.nomor_kejadian = generatedNo;
                    //
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
                    DataTable data = AccidentModel.GetViewData(PkValue);
                    AccidentModel.ViewModel fieldModel = new AccidentModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = AccidentModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.jenis_kejadian_id = dr["jenis_kejadian_id"].ToString();
                        fieldModel.judul_kejadian = dr["judul_kejadian"].ToString();
                        fieldModel.nomor_kejadian = dr["nomor_kejadian"].ToString();
                        fieldModel.lokasi_kejadian_id = dr["lokasi_kejadian_id"].ToString();
                        fieldModel.tgl_kejadian = dr["tgl_kejadian"].ToString();
                        fieldModel.waktu_kejadian_id = dr["waktu_kejadian_id"].ToString();
                        fieldModel.deskripsi_kejadian = dr["deskripsi_kejadian"].ToString();
                        fieldModel.kategori_kejadian_id = dr["kategori_kejadian_id"].ToString();
                        fieldModel.kategori_kejadian_nama = dr["kategori_kejadian_nama"].ToString();
                        fieldModel.jumlah_korban = dr["jumlah_korban"].ToString();
                        fieldModel.jumlah_kerugian = dr["jumlah_kerugian"].ToString();
                        fieldModel.lampiran = dr["lampiran"].ToString();
                        fieldModel.status_jam_kerja = dr["status_jam_kerja"].ToString();
                        fieldModel.jml_hari_hilang = dr["jml_hari_hilang"].ToString();
                        fieldModel.dampak_fasilitas_id = dr["dampak_fasilitas_id"].ToString();
                        fieldModel.dampak_fasilitas_nama = dr["dampak_fasilitas_nama"].ToString();
                        fieldModel.dampak_lingkungan_id = dr["dampak_lingkungan_id"].ToString();
                        fieldModel.dampak_lingkungan_nama = dr["dampak_lingkungan_nama"].ToString();
                        fieldModel.tipe_celaka_id = dr["tipe_celaka_id"].ToString();
                        fieldModel.tipe_celaka_nama = dr["tipe_celaka_nama"].ToString();
                        fieldModel.faktor_pribadi_id = dr["faktor_pribadi_id"].ToString();
                        fieldModel.faktor_pribadi_nama = dr["faktor_pribadi_nama"].ToString();
                        fieldModel.faktor_kerja_id = dr["faktor_kerja_id"].ToString();
                        fieldModel.faktor_kerja_nama = dr["faktor_kerja_nama"].ToString();
                        fieldModel.tindak_bahaya_id = dr["tindak_bahaya_id"].ToString();
                        fieldModel.tindak_bahaya_nama = dr["tindak_bahaya_nama"].ToString();
                        fieldModel.kondisi_bahaya_id = dr["kondisi_bahaya_id"].ToString();
                        fieldModel.kondisi_bahaya_nama = dr["kondisi_bahaya_nama"].ToString();
                        fieldModel.kompen_rugi_id = dr["kompen_rugi_id"].ToString();
                        fieldModel.kompen_rugi_nama = dr["kompen_rugi_nama"].ToString();
                        fieldModel.deskripsi_kerugian = dr["deskripsi_kerugian"].ToString();
                        fieldModel.biaya_kerugian = dr["biaya_kerugian"].ToString();
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
                    DataTable data = AccidentModel.GetViewData(PkValue);
                    AccidentModel.ViewModel fieldModel = new AccidentModel.ViewModel();
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
                        fieldModel.jenis_kejadian_id = dr["jenis_kejadian_id"].ToString();
                        fieldModel.jenis_kejadian_id_text = dr["jenis_kejadian_id_text"].ToString();
                        fieldModel.judul_kejadian = dr["judul_kejadian"].ToString();
                        fieldModel.nomor_kejadian = dr["nomor_kejadian"].ToString();
                        fieldModel.lokasi_kejadian_id = dr["lokasi_kejadian_id"].ToString();
                        fieldModel.lokasi_kejadian_id_text = dr["lokasi_kejadian_id_text"].ToString();
                        fieldModel.tgl_kejadian = dr["tgl_kejadian"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kejadian"]) : "";
                        fieldModel.dt_tgl_kejadian = dr["tgl_kejadian"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kejadian"]) : "";
                        fieldModel.waktu_kejadian_id = dr["waktu_kejadian_id"].ToString();
                        fieldModel.waktu_kejadian_id_text = dr["waktu_kejadian_id_text"].ToString();
                        fieldModel.deskripsi_kejadian = dr["deskripsi_kejadian"].ToString();
                        fieldModel.kategori_kejadian_id = dr["kategori_kejadian_id"].ToString();
                        fieldModel.kategori_kejadian_nama = dr["kategori_kejadian_nama"].ToString();
                        fieldModel.jumlah_korban = dr["jumlah_korban"].ToString();
                        fieldModel.jumlah_kerugian = dr["jumlah_kerugian"].ToString();
                        string[] init_lampiran = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "lampiran", PkValue, dr["lampiran"].ToString());
                        fieldModel.lampiran = init_lampiran[0];
                        fieldModel.lampiran_init = init_lampiran[1];
                        fieldModel.status_jam_kerja = dr["status_jam_kerja"].ToString();
                        fieldModel.status_jam_kerja_text = dr["status_jam_kerja_text"].ToString();
                        fieldModel.jml_hari_hilang = dr["jml_hari_hilang"].ToString();
                        fieldModel.dampak_fasilitas_id = dr["dampak_fasilitas_id"].ToString();
                        fieldModel.dampak_fasilitas_nama = dr["dampak_fasilitas_nama"].ToString();
                        fieldModel.dampak_lingkungan_id = dr["dampak_lingkungan_id"].ToString();
                        fieldModel.dampak_lingkungan_nama = dr["dampak_lingkungan_nama"].ToString();
                        fieldModel.tipe_celaka_id = dr["tipe_celaka_id"].ToString();
                        fieldModel.tipe_celaka_nama = dr["tipe_celaka_nama"].ToString();
                        fieldModel.faktor_pribadi_id = dr["faktor_pribadi_id"].ToString();
                        fieldModel.faktor_pribadi_nama = dr["faktor_pribadi_nama"].ToString();
                        fieldModel.faktor_kerja_id = dr["faktor_kerja_id"].ToString();
                        fieldModel.faktor_kerja_nama = dr["faktor_kerja_nama"].ToString();
                        fieldModel.tindak_bahaya_id = dr["tindak_bahaya_id"].ToString();
                        fieldModel.tindak_bahaya_nama = dr["tindak_bahaya_nama"].ToString();
                        fieldModel.kondisi_bahaya_id = dr["kondisi_bahaya_id"].ToString();
                        fieldModel.kondisi_bahaya_nama = dr["kondisi_bahaya_nama"].ToString();
                        fieldModel.kompen_rugi_id = dr["kompen_rugi_id"].ToString();
                        fieldModel.kompen_rugi_nama = dr["kompen_rugi_nama"].ToString();
                        fieldModel.deskripsi_kerugian = dr["deskripsi_kerugian"].ToString();
                        fieldModel.biaya_kerugian = dr["biaya_kerugian"].ToString();
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
        public JsonResult Insert(AccidentModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = AccidentModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["jenis_kejadian_id"] = fieldModel.jenis_kejadian_id;
                    data["judul_kejadian"] = AntiXss.HtmlEncode(fieldModel.judul_kejadian);
                    //
                    string generatedNo = AccidentModel.GetNoAccident(HttpContext, fieldModel.jenis_kejadian_id, fieldModel.ehs_area_id, fieldModel.company_id);
                    fieldModel.nomor_kejadian = generatedNo;
                    data["nomor_kejadian"] = AntiXss.HtmlEncode(fieldModel.nomor_kejadian);
                    //
                    data["lokasi_kejadian_id"] = fieldModel.lokasi_kejadian_id;
                    data["tgl_kejadian"] = fieldModel.tgl_kejadian;
                    data["waktu_kejadian_id"] = fieldModel.waktu_kejadian_id;
                    data["deskripsi_kejadian"] = AntiXss.HtmlEncode(fieldModel.deskripsi_kejadian);
                    data["kategori_kejadian_id"] = AntiXss.HtmlEncode(fieldModel.kategori_kejadian_id);
                    data["kategori_kejadian_nama"] = AntiXss.HtmlEncode(fieldModel.kategori_kejadian_nama);
                    data["jumlah_korban"] = fieldModel.jumlah_korban;
                    data["jumlah_kerugian"] = fieldModel.jumlah_kerugian != null ? fieldModel.jumlah_kerugian.Replace(",",".") : "";
                    data["lampiran"] = AntiXss.HtmlEncode(fieldModel.lampiran);
                    data["status_jam_kerja"] = fieldModel.status_jam_kerja;
                    data["jml_hari_hilang"] = fieldModel.jml_hari_hilang;
                    data["dampak_fasilitas_id"] = AntiXss.HtmlEncode(fieldModel.dampak_fasilitas_id);
                    data["dampak_fasilitas_nama"] = AntiXss.HtmlEncode(fieldModel.dampak_fasilitas_nama);
                    data["dampak_lingkungan_id"] = AntiXss.HtmlEncode(fieldModel.dampak_lingkungan_id);
                    data["dampak_lingkungan_nama"] = AntiXss.HtmlEncode(fieldModel.dampak_lingkungan_nama);
                    data["tipe_celaka_id"] = AntiXss.HtmlEncode(fieldModel.tipe_celaka_id);
                    data["tipe_celaka_nama"] = AntiXss.HtmlEncode(fieldModel.tipe_celaka_nama);
                    data["faktor_pribadi_id"] = AntiXss.HtmlEncode(fieldModel.faktor_pribadi_id);
                    data["faktor_pribadi_nama"] = AntiXss.HtmlEncode(fieldModel.faktor_pribadi_nama);
                    data["faktor_kerja_id"] = AntiXss.HtmlEncode(fieldModel.faktor_kerja_id);
                    data["faktor_kerja_nama"] = AntiXss.HtmlEncode(fieldModel.faktor_kerja_nama);
                    data["tindak_bahaya_id"] = AntiXss.HtmlEncode(fieldModel.tindak_bahaya_id);
                    data["tindak_bahaya_nama"] = AntiXss.HtmlEncode(fieldModel.tindak_bahaya_nama);
                    data["kondisi_bahaya_id"] = AntiXss.HtmlEncode(fieldModel.kondisi_bahaya_id);
                    data["kondisi_bahaya_nama"] = AntiXss.HtmlEncode(fieldModel.kondisi_bahaya_nama);
                    data["kompen_rugi_id"] = AntiXss.HtmlEncode(fieldModel.kompen_rugi_id);
                    data["kompen_rugi_nama"] = AntiXss.HtmlEncode(fieldModel.kompen_rugi_nama);
                    data["deskripsi_kerugian"] = AntiXss.HtmlEncode(fieldModel.deskripsi_kerugian);
                    data["biaya_kerugian"] = fieldModel.biaya_kerugian != null ? fieldModel.biaya_kerugian.Replace(",",".") : "";
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = AccidentModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(AccidentModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(AccidentModel.GetData(PkValue));
                    data_old["tgl_kejadian"] = data_old["tgl_kejadian"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kejadian"]) : "";
                    data_old["jumlah_kerugian"] = data_old["jumlah_kerugian"].ToString() != ""  ? data_old["jumlah_kerugian"].ToString().Replace(",", ".") : "";
                    data_old["biaya_kerugian"] = data_old["biaya_kerugian"].ToString() != ""  ? data_old["biaya_kerugian"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(AccidentModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["jenis_kejadian_id"] = fieldModel.jenis_kejadian_id;
                    data["judul_kejadian"] = AntiXss.HtmlEncode(fieldModel.judul_kejadian);
                    data["nomor_kejadian"] = AntiXss.HtmlEncode(fieldModel.nomor_kejadian);
                    data["lokasi_kejadian_id"] = fieldModel.lokasi_kejadian_id;
                    data["tgl_kejadian"] = fieldModel.tgl_kejadian;
                    data["waktu_kejadian_id"] = fieldModel.waktu_kejadian_id;
                    data["deskripsi_kejadian"] = AntiXss.HtmlEncode(fieldModel.deskripsi_kejadian);
                    data["kategori_kejadian_id"] = AntiXss.HtmlEncode(fieldModel.kategori_kejadian_id);
                    data["kategori_kejadian_nama"] = AntiXss.HtmlEncode(fieldModel.kategori_kejadian_nama);
                    data["jumlah_korban"] = fieldModel.jumlah_korban;
                    data["jumlah_kerugian"] = fieldModel.jumlah_kerugian != null ? fieldModel.jumlah_kerugian.ToString().Replace(",", ".") : "";
                    data["lampiran"] = AntiXss.HtmlEncode(fieldModel.lampiran);
                    data["status_jam_kerja"] = fieldModel.status_jam_kerja;
                    data["jml_hari_hilang"] = fieldModel.jml_hari_hilang;
                    data["dampak_fasilitas_id"] = AntiXss.HtmlEncode(fieldModel.dampak_fasilitas_id);
                    data["dampak_fasilitas_nama"] = AntiXss.HtmlEncode(fieldModel.dampak_fasilitas_nama);
                    data["dampak_lingkungan_id"] = AntiXss.HtmlEncode(fieldModel.dampak_lingkungan_id);
                    data["dampak_lingkungan_nama"] = AntiXss.HtmlEncode(fieldModel.dampak_lingkungan_nama);
                    data["tipe_celaka_id"] = AntiXss.HtmlEncode(fieldModel.tipe_celaka_id);
                    data["tipe_celaka_nama"] = AntiXss.HtmlEncode(fieldModel.tipe_celaka_nama);
                    data["faktor_pribadi_id"] = AntiXss.HtmlEncode(fieldModel.faktor_pribadi_id);
                    data["faktor_pribadi_nama"] = AntiXss.HtmlEncode(fieldModel.faktor_pribadi_nama);
                    data["faktor_kerja_id"] = AntiXss.HtmlEncode(fieldModel.faktor_kerja_id);
                    data["faktor_kerja_nama"] = AntiXss.HtmlEncode(fieldModel.faktor_kerja_nama);
                    data["tindak_bahaya_id"] = AntiXss.HtmlEncode(fieldModel.tindak_bahaya_id);
                    data["tindak_bahaya_nama"] = AntiXss.HtmlEncode(fieldModel.tindak_bahaya_nama);
                    data["kondisi_bahaya_id"] = AntiXss.HtmlEncode(fieldModel.kondisi_bahaya_id);
                    data["kondisi_bahaya_nama"] = AntiXss.HtmlEncode(fieldModel.kondisi_bahaya_nama);
                    data["kompen_rugi_id"] = AntiXss.HtmlEncode(fieldModel.kompen_rugi_id);
                    data["kompen_rugi_nama"] = AntiXss.HtmlEncode(fieldModel.kompen_rugi_nama);
                    data["deskripsi_kerugian"] = AntiXss.HtmlEncode(fieldModel.deskripsi_kerugian);
                    data["biaya_kerugian"] = fieldModel.biaya_kerugian != null ? fieldModel.biaya_kerugian.ToString().Replace(",", ".") : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = AccidentModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(AccidentModel.GetData(PkValue));
						if (data != null)
						{
							result = AccidentModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = AccidentModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_lampiran(IEnumerable<IFormFile> lampiran_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "lampiran");
            if (lampiran_file != null)
            {
                foreach (var file in lampiran_file)
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

        public ActionResult remove_lampiran(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "lampiran");
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