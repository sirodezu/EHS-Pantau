using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers.Pwa
{
    public class PwaAccidentController : Controller
    {
        private string _rule_view = "AccidentView";
        private string _rule_add = "AccidentAdd";
        private string _rule_edit = "AccidentEdit";
        private string _rule_delete = "AccidentDelete";
        private string _rule_edit_all = "AccidentEditAll";
        private string _rule_delete_all = "AccidentEditAll";
        private string _path_controler = "/PwaAccident/";
        private string _path_view = "/Views/Pwa/Accident/";
        private readonly string _table_name = "ta_acc";
        private readonly string _table_title = "Accident";
        private static IHostingEnvironment _hostingEnvironment;
        public PwaAccidentController(IHostingEnvironment hostingEnvironment)
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
                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1" : "0";
                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";
                    ViewData["AllowEditAll"] = SecurityHelper.HasRule(HttpContext, _rule_edit_all) ? "1" : "0";
                    ViewData["AllowDeleteAll"] = SecurityHelper.HasRule(HttpContext, _rule_delete_all) ? "1" : "0";

                    DataTable dt = WebApp.Areas.Ref.Models.PersonalSubAreaModel.GetListPSAByUser(HttpContext, "Accident");
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        filterColumn.ehs_area_id = dt.Rows[0]["ehs_area_id"].ToString();
                        filterColumn.ehs_area_id_text = dt.Rows[0]["ehs_area_kode"].ToString() + " - " + dt.Rows[0]["ehs_area_nama"].ToString();
                        filterColumn.ba_id = dt.Rows[0]["ba_id"].ToString();
                        filterColumn.ba_id_text = dt.Rows[0]["ba_kode"].ToString() + " - " + dt.Rows[0]["ba_nama"].ToString();
                        filterColumn.pa_id = dt.Rows[0]["pa_id"].ToString();
                        filterColumn.pa_id_text = dt.Rows[0]["pa_kode"].ToString() + " - " + dt.Rows[0]["pa_nama"].ToString();
                        filterColumn.psa_id = dt.Rows[0]["psa_id"].ToString();
                        filterColumn.psa_id_text = dt.Rows[0]["psa_kode"].ToString() + " - " + dt.Rows[0]["psa_nama"].ToString();
                    }
                    ViewData["filterColumn"] = filterColumn;
                    if (SecurityHelper.HasRule(HttpContext, "AccidentViewAll") || SecurityHelper.HasRule(HttpContext, "AccidentEditAll"))
                    {
                        return View(_path_view + "IndexAll.cshtml");
                    }
                    else
                    {
                        return View(_path_view + "Index.cshtml");
                    }
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpPost]
        public JsonResult GetListData(AccidentModel.ActionRequest param)
        {
            GridData data = AccidentModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i = 0; i < data.data.Rows.Count; i++)
            {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["lampiran"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "lampiran", data.data.Rows[i]["lampiran"].ToString());
            }
            return Json(data);
        }
        [HttpPost]
        public JsonResult GetListDataPelaku(AccidentPelakuModel.ActionRequest param)
        {
            GridData data = AccidentPelakuModel.GetListData(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult GetListDataSaksi(AccidentSaksiModel.ActionRequest param)
        {
            GridData data = AccidentSaksiModel.GetListData(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult GetListDataKorban(AccidentKorbanModel.ActionRequest param)
        {
            GridData data = AccidentKorbanModel.GetListData(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult GetListDataFollowup(AccidentFollowupModel.ActionRequest param)
        {
            GridData data = AccidentFollowupModel.GetListData(param);
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
                    if (id != null && id != "")
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
                                fieldModel.ehs_head_id = dr["ehs_head_id"].ToString();
                                fieldModel.ehs_head_nrp = dr["ehs_head_nrp"].ToString();
                                fieldModel.ehs_head_nama = dr["ehs_head_nama"].ToString();
                                fieldModel.officer_id = dr["officer_id"].ToString();
                                fieldModel.officer_nrp = dr["officer_nrp"].ToString();
                                fieldModel.officer_nama = dr["officer_nama"].ToString();

                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["acc_id"] = fieldModel.id;
                    PersonData personData = SecurityHelper.GetPersonData(HttpContext);
                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1" : "0";
                    if (
                        (SecurityHelper.HasRule(HttpContext, _rule_add) && (fieldModel.officer_id == personData.id || fieldModel.ehs_head_id == personData.id))
                        || (SecurityHelper.HasRule(HttpContext, _rule_edit_all))
                    )
                    {
                        ViewData["AllowAdd"] = "1";
                    }
                    else {
                        ViewData["AllowAdd"] = "0";
                    }
                    if (
                        (SecurityHelper.HasRule(HttpContext, _rule_edit) && (fieldModel.officer_id == personData.id || fieldModel.ehs_head_id == personData.id))
                        || (SecurityHelper.HasRule(HttpContext, _rule_edit_all))
                    )
                    {
                        ViewData["AllowEdit"] = "1";
                    }
                    else
                    {
                        ViewData["AllowEdit"] = "0";
                    }
                    if (
                        (SecurityHelper.HasRule(HttpContext, _rule_delete) && (fieldModel.officer_id == personData.id || fieldModel.ehs_head_id == personData.id))
                        || (SecurityHelper.HasRule(HttpContext, _rule_delete_all))
                    )
                    {
                        ViewData["AllowDelete"] = "1";
                    }
                    else
                    {
                        ViewData["AllowDelete"] = "0";
                    }
                    return View(_path_view + "View.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
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
                    ViewData["UrlAction"] = baseUrl + "/Accident/Insert";
                    ViewData["form_type"] = "Add";
                    //AccidentModel.ViewModel fieldModel = new AccidentModel.ViewModel();
                    fieldModel.id = AccidentModel.GetNewId().ToString();
                    fieldModel.lampiran_init = "[]";
                    //
                    //string generatedNo = AccidentModel.GetNoAccident(HttpContext, fieldModel.jenis_kejadian_id);
                    //fieldModel.nomor_kejadian = generatedNo;
                    //
                    DataTable dt = WebApp.Areas.Ref.Models.PersonalSubAreaModel.GetListPSAByUser(HttpContext, "Accident");
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        fieldModel.ehs_area_id = dt.Rows[0]["ehs_area_id"].ToString();
                        fieldModel.ehs_area_id_text = dt.Rows[0]["ehs_area_kode"].ToString() + " - " + dt.Rows[0]["ehs_area_nama"].ToString();
                        fieldModel.ba_id = dt.Rows[0]["ba_id"].ToString();
                        fieldModel.ba_id_text = dt.Rows[0]["ba_kode"].ToString() + " - " + dt.Rows[0]["ba_nama"].ToString();
                        fieldModel.pa_id = dt.Rows[0]["pa_id"].ToString();
                        fieldModel.pa_id_text = dt.Rows[0]["pa_kode"].ToString() + " - " + dt.Rows[0]["pa_nama"].ToString();
                        fieldModel.psa_id = dt.Rows[0]["psa_id"].ToString();
                        fieldModel.psa_id_text = dt.Rows[0]["psa_kode"].ToString() + " - " + dt.Rows[0]["psa_nama"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "Edit.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
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
                    ViewData["UrlAction"] = baseUrl + "/Accident/Update";
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
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpGet]
        public IActionResult ViewPelaku(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_pelaku", "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    AccidentPelakuModel.ViewModel fieldModel = new AccidentPelakuModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AccidentPelakuModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.acc_id = dr["acc_id"].ToString();
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
                                fieldModel.person_jabatan = dr["person_jabatan"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "ViewPelaku.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpGet]
        public IActionResult AddPelaku(AccidentPelakuModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_pelaku", "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentPelaku/Insert";
                    ViewData["form_type"] = "Add";
                    //AccidentPelakuModel.ViewModel fieldModel = new AccidentPelakuModel.ViewModel();
                    fieldModel.id = AccidentPelakuModel.GetNewId().ToString();
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditPelaku.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
        [HttpGet]
        public IActionResult EditPelaku(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_pelaku", "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentPelaku/Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = AccidentPelakuModel.GetViewData(PkValue);
                    AccidentPelakuModel.ViewModel fieldModel = new AccidentPelakuModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.acc_id = dr["acc_id"].ToString();
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
                        fieldModel.person_jabatan = dr["person_jabatan"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditPelaku.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpGet]
        public IActionResult ViewSaksi(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_saksi", "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    AccidentSaksiModel.ViewModel fieldModel = new AccidentSaksiModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AccidentSaksiModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.acc_id = dr["acc_id"].ToString();
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
                                fieldModel.person_jabatan = dr["person_jabatan"].ToString();
                                fieldModel.deskripsi = dr["deskripsi"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "ViewSaksi.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpGet]
        public IActionResult AddSaksi(AccidentSaksiModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_saksi", "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentSaksi/Insert";
                    ViewData["form_type"] = "Add";
                    fieldModel.id = AccidentSaksiModel.GetNewId().ToString();
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditSaksi.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
        [HttpGet]
        public IActionResult EditSaksi(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_saksi", "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentSaksi/Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = AccidentSaksiModel.GetViewData(PkValue);
                    AccidentSaksiModel.ViewModel fieldModel = new AccidentSaksiModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.acc_id = dr["acc_id"].ToString();
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
                        fieldModel.person_jabatan = dr["person_jabatan"].ToString();
                        fieldModel.deskripsi = dr["deskripsi"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditSaksi.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpGet]
        public IActionResult ViewKorban(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_korban", "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    AccidentKorbanModel.ViewModel fieldModel = new AccidentKorbanModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AccidentKorbanModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.acc_id = dr["acc_id"].ToString();
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
                                fieldModel.person_jabatan = dr["person_jabatan"].ToString();
                                fieldModel.dampak_kerja_id = dr["dampak_kerja_id"].ToString();
                                fieldModel.dampak_kerja_id_text = dr["dampak_kerja_id_text"].ToString();
                                fieldModel.dampak_tubuh_id = dr["dampak_tubuh_id"].ToString();
                                fieldModel.dampak_tubuh_nama = dr["dampak_tubuh_nama"].ToString();
                                fieldModel.dampak_kategori_id = dr["dampak_kategori_id"].ToString();
                                fieldModel.dampak_kategori_nama = dr["dampak_kategori_nama"].ToString();
                                fieldModel.total_biaya_obat = String.Format("{0:N2}", dr["total_biaya_obat"]);
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "ViewKorban.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
        [HttpGet]
        public IActionResult AddKorban(AccidentKorbanModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_korban", "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentKorban/Insert";
                    ViewData["form_type"] = "Add";
                    //AccidentKorbanModel.ViewModel fieldModel = new AccidentKorbanModel.ViewModel();
                    fieldModel.id = AccidentKorbanModel.GetNewId().ToString();
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditKorban.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
        [HttpGet]
        public IActionResult EditKorban(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_korban", "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentKorban/Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = AccidentKorbanModel.GetViewData(PkValue);
                    AccidentKorbanModel.ViewModel fieldModel = new AccidentKorbanModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.acc_id = dr["acc_id"].ToString();
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
                        fieldModel.person_jabatan = dr["person_jabatan"].ToString();
                        fieldModel.dampak_kerja_id = dr["dampak_kerja_id"].ToString();
                        fieldModel.dampak_kerja_id_text = dr["dampak_kerja_id_text"].ToString();
                        fieldModel.dampak_tubuh_id = dr["dampak_tubuh_id"].ToString();
                        fieldModel.dampak_tubuh_nama = dr["dampak_tubuh_nama"].ToString();
                        fieldModel.dampak_kategori_id = dr["dampak_kategori_id"].ToString();
                        fieldModel.dampak_kategori_nama = dr["dampak_kategori_nama"].ToString();
                        fieldModel.total_biaya_obat = dr["total_biaya_obat"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditKorban.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }

        [HttpGet]
        public IActionResult ViewFollowup(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_followup", "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    AccidentFollowupModel.ViewModel fieldModel = new AccidentFollowupModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AccidentFollowupModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.acc_id = dr["acc_id"].ToString();
                                fieldModel.followup_accident_id = dr["followup_accident_id"].ToString();
                                fieldModel.followup_accident_id_text = dr["followup_accident_id_text"].ToString();
                                fieldModel.deskripsi_tindak_lanjut = dr["deskripsi_tindak_lanjut"].ToString();
                                fieldModel.due_date = dr["due_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["due_date"]) : "";
                                fieldModel.status_followup_id = dr["status_followup_id"].ToString();
                                fieldModel.status_followup_id_text = dr["status_followup_id_text"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "ViewFollowup.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
        [HttpGet]
        public IActionResult AddFollowup(AccidentFollowupModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_followup", "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentFollowup/Insert";
                    ViewData["form_type"] = "Add";
                    fieldModel.id = AccidentFollowupModel.GetNewId().ToString();
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditFollowup.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
        [HttpGet]
        public IActionResult EditFollowup(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("ta_acc_followup", "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + "/AccidentFollowup/Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = AccidentFollowupModel.GetViewData(PkValue);
                    AccidentFollowupModel.ViewModel fieldModel = new AccidentFollowupModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.acc_id = dr["acc_id"].ToString();
                        fieldModel.followup_accident_id = dr["followup_accident_id"].ToString();
                        fieldModel.followup_accident_id_text = dr["followup_accident_id_text"].ToString();
                        fieldModel.deskripsi_tindak_lanjut = dr["deskripsi_tindak_lanjut"].ToString();
                        fieldModel.due_date = dr["due_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["due_date"]) : "";
                        fieldModel.dt_due_date = dr["due_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["due_date"]) : "";
                        fieldModel.status_followup_id = dr["status_followup_id"].ToString();
                        fieldModel.status_followup_id_text = dr["status_followup_id_text"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + "EditFollowup.cshtml");
                }
                else
                {
                    return View("Views/Pwa/AccessDenied.cshtml");
                }
            }
            else
            {
                return RedirectToAction("SessionExpired", "Pwa");
            }
        }
    }
}