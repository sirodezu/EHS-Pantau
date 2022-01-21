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
    
    public class PesertaTrainingController : Controller
    {
        private string _rule_view = "TrainingView";
        private string _rule_add = "TrainingAdd";
        private string _rule_edit = "TrainingEdit";
        private string _rule_delete = "TrainingDelete";
        private string _rule_edit_all = "TrainingEditAll";
        private string _rule_delete_all = "TrainingDeleteAll";
        private string _path_controler = "/PesertaTraining/";
        private string _path_view = "/Views/Training/PesertaTraining/";
        private readonly string _table_name = "ta_pelatihan_peserta";
        private readonly string _table_title = "Peserta Training";

		private static IHostingEnvironment _hostingEnvironment;
        public PesertaTrainingController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(PesertaTrainingModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = PesertaTrainingModel._pkKey;
                    ViewData["displayItem"] = PesertaTrainingModel.GetDisplayItem();
                    ViewData["column_att"] = PesertaTrainingModel.GetGridColumn();
                    ViewData["param"] = PesertaTrainingModel.GetListParam();
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
        public JsonResult GetListData(PesertaTrainingModel.ActionRequest param)
        {
            GridData data = PesertaTrainingModel.GetListData(param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["sertifikat_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "sertifikat_file_path", data.data.Rows[i]["sertifikat_file_path"].ToString());
            }

            return Json(data);
        }
        [HttpPost]
        public JsonResult GetListDataByPeserta(PesertaTrainingModel.ActionRequest param)
        {
            GridData data = PesertaTrainingModel.GetListDataByPeserta(param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i = 0; i < data.data.Rows.Count; i++)
            {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["sertifikat_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "sertifikat_file_path", data.data.Rows[i]["sertifikat_file_path"].ToString());
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
                    PesertaTrainingModel.ViewModel fieldModel = new PesertaTrainingModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = PesertaTrainingModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.pelatihan_id = dr["pelatihan_id"].ToString();
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
                                fieldModel.peserta_id = dr["peserta_id"].ToString();
                                fieldModel.peserta_id_text = dr["peserta_id_text"].ToString();
                                fieldModel.peserta_nrp = dr["peserta_nrp"].ToString();
                                fieldModel.peserta_nama = dr["peserta_nama"].ToString();
                                fieldModel.peserta_status_karyawan_id = dr["peserta_status_karyawan_id"].ToString();
                                fieldModel.peserta_status_user_id = dr["peserta_status_user_id"].ToString();
                                fieldModel.status_kelulusan_id = dr["status_kelulusan_id"].ToString();
                                fieldModel.status_kelulusan_id_text = dr["status_kelulusan_id_text"].ToString();
                                fieldModel.sertifikat_no = dr["sertifikat_no"].ToString();
                                fieldModel.sertifikat_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "sertifikat_file_path", dr["sertifikat_file_path"].ToString());
                                fieldModel.masa_berlaku = dr["masa_berlaku"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["masa_berlaku"]) : "";
                                fieldModel.pengesahan_sertifikat_oleh_id = dr["pengesahan_sertifikat_oleh_id"].ToString();
                                fieldModel.pengesahan_sertifikat_oleh_id_text = dr["pengesahan_sertifikat_oleh_id_text"].ToString();
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
                    TrainingModel.ViewModel fieldTraining = new TrainingModel.ViewModel();
                    if (fieldModel.pelatihan_id != null) {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = fieldModel.pelatihan_id;
                        data = TrainingModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldTraining.id = dr["id"].ToString();
                                fieldTraining.id_old = dr["id"].ToString();
                                fieldTraining.ehs_area_id = dr["ehs_area_id"].ToString();
                                fieldTraining.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                                fieldTraining.ba_id = dr["ba_id"].ToString();
                                fieldTraining.ba_id_text = dr["ba_id_text"].ToString();
                                fieldTraining.pa_id = dr["pa_id"].ToString();
                                fieldTraining.pa_id_text = dr["pa_id_text"].ToString();
                                fieldTraining.psa_id = dr["psa_id"].ToString();
                                fieldTraining.psa_id_text = dr["psa_id_text"].ToString();
                                fieldTraining.nama_pelatihan_id = dr["nama_pelatihan_id"].ToString();
                                fieldTraining.nama_pelatihan_id_text = dr["nama_pelatihan_id_text"].ToString();
                                fieldTraining.kode_pelatihan = dr["kode_pelatihan"].ToString();
                                fieldTraining.kategori_pelatihan_id = dr["kategori_pelatihan_id"].ToString();
                                fieldTraining.kategori_pelatihan_id_text = dr["kategori_pelatihan_id_text"].ToString();
                                fieldTraining.nama_pengajar_penyelenggara = dr["nama_pengajar_penyelenggara"].ToString();
                                fieldTraining.tgl_pelatihan_start = dr["tgl_pelatihan_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_start"]) : "";
                                fieldTraining.tgl_pelatihan_until = dr["tgl_pelatihan_until"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_until"]) : "";
                                fieldTraining.lama_pelatihan_jam = dr["lama_pelatihan_jam"].ToString();
                                fieldTraining.insert_by = dr["insert_by"].ToString();
                                fieldTraining.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldTraining.update_by = dr["update_by"].ToString();
                                fieldTraining.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldTraining"] = fieldTraining;
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
        public IActionResult Add(PesertaTrainingModel.ViewModel fieldModel)
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
                    //PesertaTrainingModel.ViewModel fieldModel = new PesertaTrainingModel.ViewModel();
                    fieldModel.id = PesertaTrainingModel.GetNewId().ToString();
                    fieldModel.sertifikat_file_path_init = "[]";

                    //TR/Kode Nama Pelatihan/Area/ddmmyyy(tgl pelatihan dilaksanakan)
                    //DataTable dttm = TrainingModel.GetDataByPelatihanId(int.Parse(fieldModel.pelatihan_id));
                    //var kategori = dttm.Rows[0]["kategori_pelatihan_id"].ToString();
                    //if (kategori == "1")
                    //{
                    //    var area = dttm.Rows[0]["area"];
                    //    var tgl_latihan = dttm.Rows[0]["tgl_pelatihan_start"];
                    //    var kode = dttm.Rows[0]["kode_pelatihan"];
                    //    fieldModel.sertifikat_no = "TR/" + kode + "/" + area + "/" + tgl_latihan;
                    //}
                    fieldModel.sertifikat_no = TrainingModel.GetNoSertifikat(fieldModel.pelatihan_id.ToString());


                    ViewData["fieldModel"] = fieldModel;
                    TrainingModel.ViewModel fieldTraining = new TrainingModel.ViewModel();
                    if (fieldModel.pelatihan_id != null)
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = fieldModel.pelatihan_id;
                        DataTable data = TrainingModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldTraining.id = dr["id"].ToString();
                                fieldTraining.id_old = dr["id"].ToString();
                                fieldTraining.ehs_area_id = dr["ehs_area_id"].ToString();
                                fieldTraining.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                                fieldTraining.ba_id = dr["ba_id"].ToString();
                                fieldTraining.ba_id_text = dr["ba_id_text"].ToString();
                                fieldTraining.pa_id = dr["pa_id"].ToString();
                                fieldTraining.pa_id_text = dr["pa_id_text"].ToString();
                                fieldTraining.psa_id = dr["psa_id"].ToString();
                                fieldTraining.psa_id_text = dr["psa_id_text"].ToString();
                                fieldTraining.nama_pelatihan_id = dr["nama_pelatihan_id"].ToString();
                                fieldTraining.nama_pelatihan_id_text = dr["nama_pelatihan_id_text"].ToString();
                                fieldTraining.kode_pelatihan = dr["kode_pelatihan"].ToString();
                                fieldTraining.kategori_pelatihan_id = dr["kategori_pelatihan_id"].ToString();
                                fieldTraining.kategori_pelatihan_id_text = dr["kategori_pelatihan_id_text"].ToString();
                                fieldTraining.nama_pengajar_penyelenggara = dr["nama_pengajar_penyelenggara"].ToString();
                                fieldTraining.tgl_pelatihan_start = dr["tgl_pelatihan_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_start"]) : "";
                                fieldTraining.tgl_pelatihan_until = dr["tgl_pelatihan_until"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_until"]) : "";
                                fieldTraining.lama_pelatihan_jam = dr["lama_pelatihan_jam"].ToString();
                                fieldTraining.sertifikat_no = fieldModel.sertifikat_no;
                            }
                        }
                    }
                    ViewData["fieldTraining"] = fieldTraining;
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
                    DataTable data = PesertaTrainingModel.GetViewData(PkValue);
                    PesertaTrainingModel.ViewModel fieldModel = new PesertaTrainingModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = PesertaTrainingModel.GetNewId().ToString();
                        fieldModel.pelatihan_id = dr["pelatihan_id"].ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.peserta_id = dr["peserta_id"].ToString();
                        fieldModel.peserta_nrp = dr["peserta_nrp"].ToString();
                        fieldModel.peserta_nama = dr["peserta_nama"].ToString();
                        fieldModel.peserta_status_karyawan_id = dr["peserta_status_karyawan_id"].ToString();
                        fieldModel.peserta_status_user_id = dr["peserta_status_user_id"].ToString();
                        fieldModel.status_kelulusan_id = dr["status_kelulusan_id"].ToString();
                        fieldModel.sertifikat_no = dr["sertifikat_no"].ToString();
                        fieldModel.sertifikat_file_path = dr["sertifikat_file_path"].ToString();
                        fieldModel.masa_berlaku = dr["masa_berlaku"].ToString();
                        fieldModel.pengesahan_sertifikat_oleh_id = dr["pengesahan_sertifikat_oleh_id"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    TrainingModel.ViewModel fieldTraining = new TrainingModel.ViewModel();
                    if (fieldModel.pelatihan_id != null)
                    {
                        var PkValuePelatihan = new OrderedDictionary();
                        PkValue["id"] = fieldModel.pelatihan_id;
                        data = TrainingModel.GetViewData(PkValuePelatihan);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldTraining.id = dr["id"].ToString();
                                fieldTraining.id_old = dr["id"].ToString();
                                fieldTraining.ehs_area_id = dr["ehs_area_id"].ToString();
                                fieldTraining.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                                fieldTraining.ba_id = dr["ba_id"].ToString();
                                fieldTraining.ba_id_text = dr["ba_id_text"].ToString();
                                fieldTraining.pa_id = dr["pa_id"].ToString();
                                fieldTraining.pa_id_text = dr["pa_id_text"].ToString();
                                fieldTraining.psa_id = dr["psa_id"].ToString();
                                fieldTraining.psa_id_text = dr["psa_id_text"].ToString();
                                fieldTraining.nama_pelatihan_id = dr["nama_pelatihan_id"].ToString();
                                fieldTraining.nama_pelatihan_id_text = dr["nama_pelatihan_id_text"].ToString();
                                fieldTraining.kode_pelatihan = dr["kode_pelatihan"].ToString();
                                fieldTraining.kategori_pelatihan_id = dr["kategori_pelatihan_id"].ToString();
                                fieldTraining.kategori_pelatihan_id_text = dr["kategori_pelatihan_id_text"].ToString();
                                fieldTraining.nama_pengajar_penyelenggara = dr["nama_pengajar_penyelenggara"].ToString();
                                fieldTraining.tgl_pelatihan_start = dr["tgl_pelatihan_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_start"]) : "";
                                fieldTraining.tgl_pelatihan_until = dr["tgl_pelatihan_until"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_until"]) : "";
                                fieldTraining.lama_pelatihan_jam = dr["lama_pelatihan_jam"].ToString();
                                fieldTraining.sertifikat_no = TrainingModel.GetNoSertifikat(fieldModel.pelatihan_id.ToString());
                            }
                        }
                    }
                    ViewData["fieldTraining"] = fieldTraining;
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
                    DataTable data = PesertaTrainingModel.GetViewData(PkValue);
                    PesertaTrainingModel.ViewModel fieldModel = new PesertaTrainingModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.pelatihan_id = dr["pelatihan_id"].ToString();
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
                        fieldModel.peserta_id = dr["peserta_id"].ToString();
                        fieldModel.peserta_id_text = dr["peserta_id_text"].ToString();
                        fieldModel.peserta_nrp = dr["peserta_nrp"].ToString();
                        fieldModel.peserta_nama = dr["peserta_nama"].ToString();
                        fieldModel.peserta_status_karyawan_id = dr["peserta_status_karyawan_id"].ToString();
                        fieldModel.peserta_status_user_id = dr["peserta_status_user_id"].ToString();
                        fieldModel.status_kelulusan_id = dr["status_kelulusan_id"].ToString();
                        fieldModel.status_kelulusan_id_text = dr["status_kelulusan_id_text"].ToString();
                        fieldModel.sertifikat_no = dr["sertifikat_no"].ToString();
                        string[] init_sertifikat_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "sertifikat_file_path", PkValue, dr["sertifikat_file_path"].ToString());
                        fieldModel.sertifikat_file_path = init_sertifikat_file_path[0];
                        fieldModel.sertifikat_file_path_init = init_sertifikat_file_path[1];
                        fieldModel.masa_berlaku = dr["masa_berlaku"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["masa_berlaku"]) : "";
                        fieldModel.dt_masa_berlaku = dr["masa_berlaku"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["masa_berlaku"]) : "";
                        fieldModel.pengesahan_sertifikat_oleh_id = dr["pengesahan_sertifikat_oleh_id"].ToString();
                        fieldModel.pengesahan_sertifikat_oleh_id_text = dr["pengesahan_sertifikat_oleh_id_text"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    TrainingModel.ViewModel fieldTraining = new TrainingModel.ViewModel();
                    if (fieldModel.pelatihan_id != null)
                    {
                        var PkValuePelatihan = new OrderedDictionary();
                        PkValuePelatihan["id"] = fieldModel.pelatihan_id;
                        data = TrainingModel.GetViewData(PkValuePelatihan);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldTraining.id = dr["id"].ToString();
                                fieldTraining.id_old = dr["id"].ToString();
                                fieldTraining.ehs_area_id = dr["ehs_area_id"].ToString();
                                fieldTraining.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                                fieldTraining.ba_id = dr["ba_id"].ToString();
                                fieldTraining.ba_id_text = dr["ba_id_text"].ToString();
                                fieldTraining.pa_id = dr["pa_id"].ToString();
                                fieldTraining.pa_id_text = dr["pa_id_text"].ToString();
                                fieldTraining.psa_id = dr["psa_id"].ToString();
                                fieldTraining.psa_id_text = dr["psa_id_text"].ToString();
                                fieldTraining.nama_pelatihan_id = dr["nama_pelatihan_id"].ToString();
                                fieldTraining.nama_pelatihan_id_text = dr["nama_pelatihan_id_text"].ToString();
                                fieldTraining.kode_pelatihan = dr["kode_pelatihan"].ToString();
                                fieldTraining.kategori_pelatihan_id = dr["kategori_pelatihan_id"].ToString();
                                fieldTraining.kategori_pelatihan_id_text = dr["kategori_pelatihan_id_text"].ToString();
                                fieldTraining.nama_pengajar_penyelenggara = dr["nama_pengajar_penyelenggara"].ToString();
                                fieldTraining.tgl_pelatihan_start = dr["tgl_pelatihan_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_start"]) : "";
                                fieldTraining.tgl_pelatihan_until = dr["tgl_pelatihan_until"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_pelatihan_until"]) : "";
                                fieldTraining.lama_pelatihan_jam = dr["lama_pelatihan_jam"].ToString();
                                fieldTraining.sertifikat_no = TrainingModel.GetNoSertifikat(fieldModel.pelatihan_id.ToString());
                            }
                        }
                    }
                    ViewData["fieldTraining"] = fieldTraining;
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
        public JsonResult Insert(PesertaTrainingModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = PesertaTrainingModel.GetNewId().ToString();
                    data["pelatihan_id"] = fieldModel.pelatihan_id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["peserta_id"] = fieldModel.peserta_id;
                    data["peserta_nrp"] = AntiXss.HtmlEncode(fieldModel.peserta_nrp);
                    data["peserta_nama"] = AntiXss.HtmlEncode(fieldModel.peserta_nama);
                    data["peserta_status_karyawan_id"] = fieldModel.peserta_status_karyawan_id;
                    data["peserta_status_user_id"] = fieldModel.peserta_status_user_id;
                    data["status_kelulusan_id"] = fieldModel.status_kelulusan_id;
                    //
                    //data["sertifikat_no"] = AntiXss.HtmlEncode(fieldModel.sertifikat_no);
                    string generatedNo = TrainingModel.GetNoSertifikat(fieldModel.pelatihan_id);
                    fieldModel.sertifikat_no = generatedNo;
                    data["sertifikat_no"] = AntiXss.HtmlEncode(fieldModel.sertifikat_no);
                    //
                    data["sertifikat_file_path"] = AntiXss.HtmlEncode(fieldModel.sertifikat_file_path);
                    data["masa_berlaku"] = fieldModel.masa_berlaku;
                    data["pengesahan_sertifikat_oleh_id"] = fieldModel.pengesahan_sertifikat_oleh_id;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = PesertaTrainingModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(PesertaTrainingModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(PesertaTrainingModel.GetData(PkValue));
                    data_old["masa_berlaku"] = data_old["masa_berlaku"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["masa_berlaku"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(PesertaTrainingModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["pelatihan_id"] = fieldModel.pelatihan_id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["peserta_id"] = fieldModel.peserta_id;
                    data["peserta_nrp"] = AntiXss.HtmlEncode(fieldModel.peserta_nrp);
                    data["peserta_nama"] = AntiXss.HtmlEncode(fieldModel.peserta_nama);
                    data["peserta_status_karyawan_id"] = fieldModel.peserta_status_karyawan_id;
                    data["peserta_status_user_id"] = fieldModel.peserta_status_user_id;
                    data["status_kelulusan_id"] = fieldModel.status_kelulusan_id;
                    data["sertifikat_no"] = AntiXss.HtmlEncode(fieldModel.sertifikat_no);
                    data["sertifikat_file_path"] = AntiXss.HtmlEncode(fieldModel.sertifikat_file_path);
                    data["masa_berlaku"] = fieldModel.masa_berlaku;
                    data["pengesahan_sertifikat_oleh_id"] = fieldModel.pengesahan_sertifikat_oleh_id;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = PesertaTrainingModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(PesertaTrainingModel.GetData(PkValue));
						if (data != null)
						{
							result = PesertaTrainingModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = PesertaTrainingModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_sertifikat_file_path(IEnumerable<IFormFile> sertifikat_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "sertifikat_file_path");
            if (sertifikat_file_path_file != null)
            {
                foreach (var file in sertifikat_file_path_file)
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

        public ActionResult remove_sertifikat_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "sertifikat_file_path");
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