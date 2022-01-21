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
    
    public class ManPowerController : Controller
    {
        private string _rule_view = "ManPowerView";
        private string _rule_add = "ManPowerAdd";
        private string _rule_edit = "ManPowerEdit";
        private string _rule_delete = "ManPowerDelete";
        private string _rule_view_all = "ManPowerViewAll";
        private string _rule_edit_all = "ManPowerEditAll";
        private string _path_controler = "/ManPower/";
        private string _path_view = "/Views/ManPower/ManPower/";
        private readonly string _table_name = "ta_mp";
        private readonly string _table_title = "Man Power Profile";

		private static IHostingEnvironment _hostingEnvironment;
        public ManPowerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(ManPowerModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = ManPowerModel._pkKey;
                    ViewData["displayItem"] = ManPowerModel.GetDisplayItem();
                    ViewData["column_att"] = ManPowerModel.GetGridColumn();
                    ViewData["param"] = ManPowerModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";

                    ViewData["AllowViewAll"] = SecurityHelper.HasRule(HttpContext, _rule_view_all) ? "1" : "0";
                    ViewData["AllowEditAll"] = SecurityHelper.HasRule(HttpContext, _rule_edit_all) ? "1" : "0";
                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1":"0";
                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";
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
        public JsonResult GetListData(ManPowerModel.ActionRequest param)
        {
            //GridData data = ManPowerModel.GetListData(param);
            GridData data = ManPowerModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["file_path_photo"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_photo", data.data.Rows[i]["file_path_photo"].ToString());
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
                    ManPowerModel.ViewModel fieldModel = new ManPowerModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = ManPowerModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.karyawan_nonkaryawan_id = dr["karyawan_nonkaryawan_id"].ToString();
                                fieldModel.karyawan_nonkaryawan_id_text = dr["karyawan_nonkaryawan_id_text"].ToString();
                                fieldModel.user_nonuser_id = dr["user_nonuser_id"].ToString();
                                fieldModel.user_nonuser_id_text = dr["user_nonuser_id_text"].ToString();
                                fieldModel.person_id = dr["person_id"].ToString();
                                fieldModel.person_id_text = dr["person_id_text"].ToString();
                                fieldModel.file_path_photo = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_path_photo", dr["file_path_photo"].ToString());

                                fieldModel.photo = FileHelper.GetViewImg(baseUrl, _table_name, PkValue, "file_path_photo", dr["file_path_photo"].ToString());
                                fieldModel.photo_link = FileHelper.GetlinkImg(baseUrl, _table_name, PkValue, "file_path_photo", dr["file_path_photo"].ToString());

                                fieldModel.nama_lengkap = dr["nama_lengkap"].ToString();
                                fieldModel.nrp = dr["nrp"].ToString();
                                fieldModel.tempat_lahir = dr["tempat_lahir"].ToString();
                                fieldModel.tgl_lahir = dr["tgl_lahir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_lahir"]) : "";
                                fieldModel.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                                fieldModel.jenis_kelamin_id_text = dr["jenis_kelamin_id_text"].ToString();
                                fieldModel.company_id = dr["company_id"].ToString();
                                fieldModel.company_id_text = dr["company_id_text"].ToString();
                                fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                                fieldModel.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                                fieldModel.business_area_id = dr["business_area_id"].ToString();
                                fieldModel.business_area_id_text = dr["business_area_id_text"].ToString();
                                fieldModel.personal_area_id = dr["personal_area_id"].ToString();
                                fieldModel.personal_area_id_text = dr["personal_area_id_text"].ToString();
                                fieldModel.personal_sub_area_id = dr["personal_sub_area_id"].ToString();
                                fieldModel.personal_sub_area_id_text = dr["personal_sub_area_id_text"].ToString();
                                //fieldModel.departemen_id = dr["departemen_id"].ToString();
                                //fieldModel.departemen_id_text = dr["departemen_id_text"].ToString();
                                //fieldModel.level_jabatan_id = dr["level_jabatan_id"].ToString();
                                //fieldModel.level_jabatan_id_text = dr["level_jabatan_id_text"].ToString();
                                //fieldModel.level_jabatan_nama = dr["level_jabatan_nama"].ToString();
                                fieldModel.tgl_masuk_kerja = dr["tgl_masuk_kerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_masuk_kerja"]) : "";
                                fieldModel.tgl_akhir_kerja = dr["tgl_akhir_kerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_akhir_kerja"]) : "";
                                fieldModel.job_id = dr["job_id"].ToString();
                                fieldModel.job = dr["job"].ToString();
                                fieldModel.job_start = dr["job_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["job_start"]) : "";
                                fieldModel.position = dr["position"].ToString();
                                fieldModel.position_start = dr["position_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["position_start"]) : "";
                                fieldModel.behaviour_screen_nilai = dr["behaviour_screen_nilai"].ToString();
                                fieldModel.behaviour_screen_objective = dr["behaviour_screen_objective"].ToString();
                                fieldModel.toc_nilai = dr["toc_nilai"].ToString();
                                fieldModel.toc_objective = dr["toc_objective"].ToString();
                                fieldModel.bmc_nilai = dr["bmc_nilai"].ToString();
                                fieldModel.bmc_objective = dr["bmc_objective"].ToString();
                                fieldModel.english_nilai = dr["english_nilai"].ToString();
                                fieldModel.english_objective = dr["english_objective"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                                fieldModel.screening_time = dr["screening_time"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["screening_time"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["mp_id"] = fieldModel.id;
                    ViewData["AllowView"] = SecurityHelper.HasRule(HttpContext, _rule_view) ? "1" : "0";
                    ViewData["AllowAdd"] = SecurityHelper.HasRule(HttpContext, _rule_add) ? "1" : "0";
                    ViewData["AllowEdit"] = SecurityHelper.HasRule(HttpContext, _rule_edit) ? "1" : "0";
                    ViewData["AllowDelete"] = SecurityHelper.HasRule(HttpContext, _rule_delete) ? "1" : "0";
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
        public IActionResult Add(ManPowerModel.ViewModel fieldModel)
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
                    //ManPowerModel.ViewModel fieldModel = new ManPowerModel.ViewModel();
                    fieldModel.id = ManPowerModel.GetNewId().ToString();
                    fieldModel.file_path_photo_init = "[]";
                    fieldModel.karyawan_nonkaryawan_id = "2";
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
                    DataTable data = ManPowerModel.GetViewData(PkValue);
                    ManPowerModel.ViewModel fieldModel = new ManPowerModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = ManPowerModel.GetNewId().ToString();
                        fieldModel.karyawan_nonkaryawan_id = dr["karyawan_nonkaryawan_id"].ToString();
                        fieldModel.user_nonuser_id = dr["user_nonuser_id"].ToString();
                        fieldModel.person_id = dr["person_id"].ToString();
                        fieldModel.file_path_photo = dr["file_path_photo"].ToString();
                        fieldModel.nama_lengkap = dr["nama_lengkap"].ToString();
                        fieldModel.nrp = dr["nrp"].ToString();
                        fieldModel.tempat_lahir = dr["tempat_lahir"].ToString();
                        fieldModel.tgl_lahir = dr["tgl_lahir"].ToString();
                        fieldModel.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.business_area_id = dr["business_area_id"].ToString();
                        fieldModel.personal_area_id = dr["personal_area_id"].ToString();
                        fieldModel.personal_sub_area_id = dr["personal_sub_area_id"].ToString();
                        //fieldModel.departemen_id = dr["departemen_id"].ToString();
                        //fieldModel.level_jabatan_id = dr["level_jabatan_id"].ToString();
                        //fieldModel.level_jabatan_nama = dr["level_jabatan_nama"].ToString();
                        fieldModel.tgl_masuk_kerja = dr["tgl_masuk_kerja"].ToString();
                        fieldModel.tgl_akhir_kerja = dr["tgl_akhir_kerja"].ToString();
                        fieldModel.job_id = dr["job_id"].ToString();
                        fieldModel.job = dr["job"].ToString();
                        fieldModel.job_start = dr["job_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["job_start"]) : "";
                        fieldModel.position = dr["position"].ToString();
                        fieldModel.position_start = dr["position_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["position_start"]) : "";
                        fieldModel.behaviour_screen_nilai = dr["behaviour_screen_nilai"].ToString();
                        fieldModel.behaviour_screen_objective = dr["behaviour_screen_objective"].ToString();
                        fieldModel.toc_nilai = dr["toc_nilai"].ToString();
                        fieldModel.toc_objective = dr["toc_objective"].ToString();
                        fieldModel.bmc_nilai = dr["bmc_nilai"].ToString();
                        fieldModel.bmc_objective = dr["bmc_objective"].ToString();
                        fieldModel.english_nilai = dr["english_nilai"].ToString();
                        fieldModel.english_objective = dr["english_objective"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.screening_time = dr["screening_time"].ToString();
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
                    DataTable data = ManPowerModel.GetViewData(PkValue);
                    ManPowerModel.ViewModel fieldModel = new ManPowerModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.karyawan_nonkaryawan_id = dr["karyawan_nonkaryawan_id"].ToString();
                        fieldModel.karyawan_nonkaryawan_id_text = dr["karyawan_nonkaryawan_id_text"].ToString();
                        fieldModel.user_nonuser_id = dr["user_nonuser_id"].ToString();
                        fieldModel.user_nonuser_id_text = dr["user_nonuser_id_text"].ToString();
                        fieldModel.person_id = dr["person_id"].ToString();
                        fieldModel.person_id_text = dr["person_id_text"].ToString();
                        string[] init_file_path_photo = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_path_photo", PkValue, dr["file_path_photo"].ToString());
                        fieldModel.file_path_photo = init_file_path_photo[0];
                        fieldModel.file_path_photo_init = init_file_path_photo[1];
                        fieldModel.nama_lengkap = dr["nama_lengkap"].ToString();
                        fieldModel.nrp = dr["nrp"].ToString();
                        fieldModel.tempat_lahir = dr["tempat_lahir"].ToString();
                        fieldModel.tgl_lahir = dr["tgl_lahir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_lahir"]) : "";
                        fieldModel.dt_tgl_lahir = dr["tgl_lahir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_lahir"]) : "";
                        fieldModel.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                        fieldModel.jenis_kelamin_id_text = dr["jenis_kelamin_id_text"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.company_id_text = dr["company_id_text"].ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ehs_area_id_text = dr["ehs_area_id_text"].ToString();
                        fieldModel.business_area_id = dr["business_area_id"].ToString();
                        fieldModel.business_area_id_text = dr["business_area_id_text"].ToString();
                        fieldModel.personal_area_id = dr["personal_area_id"].ToString();
                        fieldModel.personal_area_id_text = dr["personal_area_id_text"].ToString();
                        fieldModel.personal_sub_area_id = dr["personal_sub_area_id"].ToString();
                        fieldModel.personal_sub_area_id_text = dr["personal_sub_area_id_text"].ToString();
                        //fieldModel.departemen_id = dr["departemen_id"].ToString();
                        //fieldModel.departemen_id_text = dr["departemen_id_text"].ToString();
                        //fieldModel.level_jabatan_id = dr["level_jabatan_id"].ToString();
                        //fieldModel.level_jabatan_id_text = dr["level_jabatan_id_text"].ToString();
                        //fieldModel.level_jabatan_nama = dr["level_jabatan_nama"].ToString();
                        fieldModel.job_id = dr["job_id"].ToString();
                        fieldModel.job = dr["job"].ToString();
                        fieldModel.job_start = dr["job_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["job_start"]) : "";
                        fieldModel.position = dr["position"].ToString();
                        fieldModel.position_start = dr["position_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["position_start"]) : "";
                        fieldModel.tgl_masuk_kerja = dr["tgl_masuk_kerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_masuk_kerja"]) : "";
                        fieldModel.dt_tgl_masuk_kerja = dr["tgl_masuk_kerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_masuk_kerja"]) : "";
                        fieldModel.tgl_akhir_kerja = dr["tgl_akhir_kerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_akhir_kerja"]) : "";
                        fieldModel.dt_tgl_akhir_kerja = dr["tgl_akhir_kerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_akhir_kerja"]) : "";
                        fieldModel.job = dr["job"].ToString();
                        fieldModel.job_start = dr["job_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["job_start"]) : "";
                        fieldModel.position = dr["position"].ToString();
                        fieldModel.position_start = dr["position_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["position_start"]) : "";
                        fieldModel.behaviour_screen_nilai = dr["behaviour_screen_nilai"].ToString();
                        fieldModel.behaviour_screen_objective = dr["behaviour_screen_objective"].ToString();
                        fieldModel.toc_nilai = dr["toc_nilai"].ToString();
                        fieldModel.toc_objective = dr["toc_objective"].ToString();
                        fieldModel.bmc_nilai = dr["bmc_nilai"].ToString();
                        fieldModel.bmc_objective = dr["bmc_objective"].ToString();
                        fieldModel.english_nilai = dr["english_nilai"].ToString();
                        fieldModel.english_objective = dr["english_objective"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.screening_time = dr["screening_time"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["screening_time"]) : "";
                        fieldModel.dt_screening_time = dr["screening_time"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["screening_time"]) : "";
                    }
                    ViewData["fieldModel"] = fieldModel;
                    if (fieldModel.karyawan_nonkaryawan_id == "1")
                    {
                        return View(_path_view + "EditKaryawan.cshtml");
                    }
                    else {
                        return View(_path_view + "Edit.cshtml");
                    }
                    
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
        public JsonResult Insert(ManPowerModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = ManPowerModel.GetNewId().ToString();
                    data["karyawan_nonkaryawan_id"] = fieldModel.karyawan_nonkaryawan_id;
                    data["user_nonuser_id"] = fieldModel.user_nonuser_id;
                    data["person_id"] = fieldModel.person_id;
                    data["file_path_photo"] = AntiXss.HtmlEncode(fieldModel.file_path_photo);
                    data["nama_lengkap"] = AntiXss.HtmlEncode(fieldModel.nama_lengkap);
                    data["nrp"] = AntiXss.HtmlEncode(fieldModel.nrp);
                    data["tempat_lahir"] = AntiXss.HtmlEncode(fieldModel.tempat_lahir);
                    data["tgl_lahir"] = fieldModel.tgl_lahir;
                    data["jenis_kelamin_id"] = fieldModel.jenis_kelamin_id;
                    data["company_id"] = fieldModel.company_id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["business_area_id"] = fieldModel.business_area_id;
                    data["personal_area_id"] = fieldModel.personal_area_id;
                    data["personal_sub_area_id"] = fieldModel.personal_sub_area_id;
                    //data["departemen_id"] = fieldModel.departemen_id;
                    //data["level_jabatan_id"] = fieldModel.level_jabatan_id;
                    //data["level_jabatan_nama"] = AntiXss.HtmlEncode(fieldModel.level_jabatan_nama);
                    data["job_id"] = fieldModel.job_id;
                    data["job"] = AntiXss.HtmlEncode(fieldModel.job);
                    data["position"] = AntiXss.HtmlEncode(fieldModel.position);
                    data["tgl_masuk_kerja"] = fieldModel.tgl_masuk_kerja;
                    data["tgl_akhir_kerja"] = fieldModel.tgl_akhir_kerja;
                    data["behaviour_screen_nilai"] = fieldModel.behaviour_screen_nilai;
                    data["behaviour_screen_objective"] = AntiXss.HtmlEncode(fieldModel.behaviour_screen_objective);
                    data["toc_nilai"] = fieldModel.toc_nilai;
                    data["toc_objective"] = AntiXss.HtmlEncode(fieldModel.toc_objective);
                    data["bmc_nilai"] = fieldModel.bmc_nilai;
                    data["bmc_objective"] = AntiXss.HtmlEncode(fieldModel.bmc_objective);
                    data["english_nilai"] = fieldModel.english_nilai;
                    data["english_objective"] = AntiXss.HtmlEncode(fieldModel.english_objective);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    data["screening_time"] = fieldModel.screening_time;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = ManPowerModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(ManPowerModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(ManPowerModel.GetData(PkValue));
                    data_old["tgl_lahir"] = data_old["tgl_lahir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_lahir"]) : "";
                    data_old["tgl_masuk_kerja"] = data_old["tgl_masuk_kerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_masuk_kerja"]) : "";
                    data_old["tgl_akhir_kerja"] = data_old["tgl_akhir_kerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_akhir_kerja"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(ManPowerModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["karyawan_nonkaryawan_id"] = fieldModel.karyawan_nonkaryawan_id;
                    data["user_nonuser_id"] = fieldModel.user_nonuser_id;
                    data["person_id"] = fieldModel.person_id;
                    data["file_path_photo"] = AntiXss.HtmlEncode(fieldModel.file_path_photo);
                    data["nama_lengkap"] = AntiXss.HtmlEncode(fieldModel.nama_lengkap);
                    data["nrp"] = AntiXss.HtmlEncode(fieldModel.nrp);
                    data["tempat_lahir"] = AntiXss.HtmlEncode(fieldModel.tempat_lahir);
                    data["tgl_lahir"] = fieldModel.tgl_lahir;
                    data["jenis_kelamin_id"] = fieldModel.jenis_kelamin_id;
                    data["company_id"] = fieldModel.company_id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["business_area_id"] = fieldModel.business_area_id;
                    data["personal_area_id"] = fieldModel.personal_area_id;
                    data["personal_sub_area_id"] = fieldModel.personal_sub_area_id;
                    //data["departemen_id"] = fieldModel.departemen_id;
                    //data["level_jabatan_id"] = fieldModel.level_jabatan_id;
                    //data["level_jabatan_nama"] = AntiXss.HtmlEncode(fieldModel.level_jabatan_nama);
                    //data["job_id"] = fieldModel.job_id;
                    data["job"] = AntiXss.HtmlEncode(fieldModel.job);
                    data["position"] = AntiXss.HtmlEncode(fieldModel.position);
                    data["tgl_masuk_kerja"] = fieldModel.tgl_masuk_kerja;
                    data["tgl_akhir_kerja"] = fieldModel.tgl_akhir_kerja;
                    data["behaviour_screen_nilai"] = fieldModel.behaviour_screen_nilai;
                    data["behaviour_screen_objective"] = AntiXss.HtmlEncode(fieldModel.behaviour_screen_objective);
                    data["toc_nilai"] = fieldModel.toc_nilai;
                    data["toc_objective"] = AntiXss.HtmlEncode(fieldModel.toc_objective);
                    data["bmc_nilai"] = fieldModel.bmc_nilai;
                    data["bmc_objective"] = AntiXss.HtmlEncode(fieldModel.bmc_objective);
                    data["english_nilai"] = fieldModel.english_nilai;
                    data["english_objective"] = AntiXss.HtmlEncode(fieldModel.english_objective);
                    data["screening_time"] = fieldModel.screening_time;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = ManPowerModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(ManPowerModel.GetData(PkValue));
						if (data != null)
						{
							result = ManPowerModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = ManPowerModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_file_path_photo(IEnumerable<IFormFile> file_path_photo_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_photo");
            if (file_path_photo_file != null)
            {
                foreach (var file in file_path_photo_file)
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

        public ActionResult remove_file_path_photo(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "file_path_photo");
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

        [HttpGet]
        public IActionResult LookupList(ManPowerModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = ManPowerModel._pkKey;
                    ViewData["displayItem"] = ManPowerModel.GetDisplayItem();
                    ViewData["column_att"] = ManPowerModel.GetGridColumn();
                    ViewData["param"] = ManPowerModel.GetListParam();
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
                    ViewData["filterColumn"] = filterColumn;
                    return View(_path_view + "LookupList.cshtml");
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
        public JsonResult GetData(string id)
        {
            var PkValue = new OrderedDictionary();
            PkValue["id"] = id;
            DataTable data = ManPowerModel.GetViewData(PkValue);
            return Json(data);
        }
    }
}