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
    
    public class PersonController : Controller
    {
        private string _rule_view = "PersonView";
        private string _rule_add = "PersonAdd";
        private string _rule_edit = "PersonEdit";
        private string _rule_delete = "PersonDelete";
        private string _rule_edit_all = "PersonEditAll";
        private string _rule_delete_all = "PersonDeleteAll";
        private string _path_controler = "/Person/";
        private string _path_view = "/Views/Person/";
        private readonly string _table_name = "ta_person";
        private readonly string _table_title = "Person";

		private static IHostingEnvironment _hostingEnvironment;
        public PersonController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(PersonModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = PersonModel._pkKey;
                    ViewData["displayItem"] = PersonModel.GetDisplayItem();
                    ViewData["column_att"] = PersonModel.GetGridColumn();
                    ViewData["param"] = PersonModel.GetListParam();
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
        public JsonResult GetListData(PersonModel.ActionRequest param)
        {
            GridData data = PersonModel.GetListData(param);
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
                    PersonModel.ViewModel fieldModel = new PersonModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = PersonModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.person_type_id = dr["person_type_id"].ToString();
                                fieldModel.person_type_id_text = dr["person_type_id_text"].ToString();
                                fieldModel.company_id = dr["company_id"].ToString();
                                fieldModel.company_id_text = dr["company_id_text"].ToString();
                                fieldModel.nrp = dr["nrp"].ToString();
                                fieldModel.nama = dr["nama"].ToString();
                                fieldModel.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                                fieldModel.jenis_kelamin_id_text = dr["jenis_kelamin_id_text"].ToString();
                                fieldModel.tempat_lahir = dr["tempat_lahir"].ToString();
                                fieldModel.tgl_lahir = dr["tgl_lahir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_lahir"]) : "";
                                fieldModel.ba_id = dr["ba_id"].ToString();
                                fieldModel.ba_id_text = dr["ba_id_text"].ToString();
                                fieldModel.pa_id = dr["pa_id"].ToString();
                                fieldModel.pa_id_text = dr["pa_id_text"].ToString();
                                fieldModel.psa_id = dr["psa_id"].ToString();
                                fieldModel.psa_id_text = dr["psa_id_text"].ToString();
                                fieldModel.jabatan_id = dr["jabatan_id"].ToString();
                                fieldModel.jabatan_id_text = dr["jabatan_id_text"].ToString();
                                fieldModel.jabatan = dr["jabatan"].ToString();
                                fieldModel.grade = dr["grade"].ToString();
                                fieldModel.job = dr["job"].ToString();
                                fieldModel.job_start = dr["job_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["job_start"]) : "";
                                fieldModel.position = dr["position"].ToString();
                                fieldModel.position_start = dr["position_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["position_start"]) : "";
                                fieldModel.photo = dr["photo"].ToString();
                                fieldModel.status_id = dr["status_id"].ToString();
                                fieldModel.status_id_text = dr["status_id_text"].ToString();
                                fieldModel.mulai_bekerja = dr["mulai_bekerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["mulai_bekerja"]) : "";
                                fieldModel.akhir_bekerja = dr["akhir_bekerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["akhir_bekerja"]) : "";
                                fieldModel.superior_id = dr["superior_id"].ToString();
                                fieldModel.superior_id_text = dr["superior_id_text"].ToString();
                                fieldModel.alamat = dr["alamat"].ToString();
                                fieldModel.tlp = dr["tlp"].ToString();
                                fieldModel.pendidikan_id = dr["pendidikan_id"].ToString();
                                fieldModel.pendidikan_id_text = dr["pendidikan_id_text"].ToString();
                                fieldModel.jurusan = dr["jurusan"].ToString();
                                fieldModel.sap_ba_id = dr["sap_ba_id"].ToString();
                                fieldModel.sap_ba_id_text = dr["sap_ba_id_text"].ToString();
                                fieldModel.sap_pa_id = dr["sap_pa_id"].ToString();
                                fieldModel.sap_pa_id_text = dr["sap_pa_id_text"].ToString();
                                fieldModel.sap_psa_id = dr["sap_psa_id"].ToString();
                                fieldModel.sap_psa_id_text = dr["sap_psa_id_text"].ToString();
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
        public IActionResult Add(PersonModel.ViewModel fieldModel)
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
                    //PersonModel.ViewModel fieldModel = new PersonModel.ViewModel();
                    fieldModel.id = PersonModel.GetNewId().ToString();
                    fieldModel.status_id = "1";
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
                    DataTable data = PersonModel.GetViewData(PkValue);
                    PersonModel.ViewModel fieldModel = new PersonModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = PersonModel.GetNewId().ToString();
                        fieldModel.person_type_id = dr["person_type_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.nrp = dr["nrp"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                        fieldModel.tempat_lahir = dr["tempat_lahir"].ToString();
                        fieldModel.tgl_lahir = dr["tgl_lahir"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.jabatan_id = dr["jabatan_id"].ToString();
                        fieldModel.jabatan = dr["jabatan"].ToString();
                        fieldModel.grade = dr["grade"].ToString();
                        fieldModel.job = dr["job"].ToString();
                        fieldModel.job_start = dr["job_start"].ToString();
                        fieldModel.position = dr["position"].ToString();
                        fieldModel.position_start = dr["position_start"].ToString();
                        fieldModel.photo = dr["photo"].ToString();
                        fieldModel.status_id = dr["status_id"].ToString();
                        fieldModel.mulai_bekerja = dr["mulai_bekerja"].ToString();
                        fieldModel.akhir_bekerja = dr["akhir_bekerja"].ToString();
                        fieldModel.superior_id = dr["superior_id"].ToString();
                        fieldModel.alamat = dr["alamat"].ToString();
                        fieldModel.tlp = dr["tlp"].ToString();
                        fieldModel.pendidikan_id = dr["pendidikan_id"].ToString();
                        fieldModel.jurusan = dr["jurusan"].ToString();
                        fieldModel.sap_ba_id = dr["sap_ba_id"].ToString();
                        fieldModel.sap_pa_id = dr["sap_pa_id"].ToString();
                        fieldModel.sap_psa_id = dr["sap_psa_id"].ToString();
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
                    DataTable data = PersonModel.GetViewData(PkValue);
                    PersonModel.ViewModel fieldModel = new PersonModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.person_type_id = dr["person_type_id"].ToString();
                        fieldModel.person_type_id_text = dr["person_type_id_text"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.company_id_text = dr["company_id_text"].ToString();
                        fieldModel.nrp = dr["nrp"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                        fieldModel.jenis_kelamin_id_text = dr["jenis_kelamin_id_text"].ToString();
                        fieldModel.tempat_lahir = dr["tempat_lahir"].ToString();
                        fieldModel.tgl_lahir = dr["tgl_lahir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_lahir"]) : "";
                        fieldModel.dt_tgl_lahir = dr["tgl_lahir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_lahir"]) : "";
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.ba_id_text = dr["ba_id_text"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.pa_id_text = dr["pa_id_text"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.psa_id_text = dr["psa_id_text"].ToString();
                        fieldModel.jabatan_id = dr["jabatan_id"].ToString();
                        fieldModel.jabatan_id_text = dr["jabatan_id_text"].ToString();
                        fieldModel.jabatan = dr["jabatan"].ToString();
                        fieldModel.grade = dr["grade"].ToString();
                        fieldModel.job = dr["job"].ToString();
                        fieldModel.job_start = dr["job_start"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["job_start"]) : "";
                        fieldModel.dt_job_start = dr["job_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["job_start"]) : "";
                        fieldModel.position = dr["position"].ToString();
                        fieldModel.position_start = dr["position_start"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["position_start"]) : "";
                        fieldModel.dt_position_start = dr["position_start"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["position_start"]) : "";
                        fieldModel.photo = dr["photo"].ToString();
                        fieldModel.status_id = dr["status_id"].ToString();
                        fieldModel.status_id_text = dr["status_id_text"].ToString();
                        fieldModel.mulai_bekerja = dr["mulai_bekerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["mulai_bekerja"]) : "";
                        fieldModel.dt_mulai_bekerja = dr["mulai_bekerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["mulai_bekerja"]) : "";
                        fieldModel.akhir_bekerja = dr["akhir_bekerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["akhir_bekerja"]) : "";
                        fieldModel.dt_akhir_bekerja = dr["akhir_bekerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["akhir_bekerja"]) : "";
                        fieldModel.superior_id = dr["superior_id"].ToString();
                        fieldModel.superior_id_text = dr["superior_id_text"].ToString();
                        fieldModel.alamat = dr["alamat"].ToString();
                        fieldModel.tlp = dr["tlp"].ToString();
                        fieldModel.pendidikan_id = dr["pendidikan_id"].ToString();
                        fieldModel.pendidikan_id_text = dr["pendidikan_id_text"].ToString();
                        fieldModel.jurusan = dr["jurusan"].ToString();
                        fieldModel.sap_ba_id = dr["sap_ba_id"].ToString();
                        fieldModel.sap_ba_id_text = dr["sap_ba_id_text"].ToString();
                        fieldModel.sap_pa_id = dr["sap_pa_id"].ToString();
                        fieldModel.sap_pa_id_text = dr["sap_pa_id_text"].ToString();
                        fieldModel.sap_psa_id = dr["sap_psa_id"].ToString();
                        fieldModel.sap_psa_id_text = dr["sap_psa_id_text"].ToString();
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
        public JsonResult Insert(PersonModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = PersonModel.GetNewId().ToString();
                    data["person_type_id"] = fieldModel.person_type_id;
                    data["company_id"] = fieldModel.company_id;
                    data["nrp"] = fieldModel.nrp;
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["jenis_kelamin_id"] = fieldModel.jenis_kelamin_id;
                    data["tempat_lahir"] = AntiXss.HtmlEncode(fieldModel.tempat_lahir);
                    data["tgl_lahir"] = fieldModel.tgl_lahir;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["jabatan_id"] = fieldModel.jabatan_id;
                    data["jabatan"] = AntiXss.HtmlEncode(fieldModel.jabatan);
                    data["grade"] = AntiXss.HtmlEncode(fieldModel.grade);
                    data["job"] = AntiXss.HtmlEncode(fieldModel.job);
                    data["job_start"] = fieldModel.job_start;
                    data["position"] = AntiXss.HtmlEncode(fieldModel.position);
                    data["position_start"] = fieldModel.position_start;
                    data["photo"] = AntiXss.HtmlEncode(fieldModel.photo);
                    data["status_id"] = fieldModel.status_id;
                    data["mulai_bekerja"] = fieldModel.mulai_bekerja;
                    data["akhir_bekerja"] = fieldModel.akhir_bekerja;
                    data["superior_id"] = fieldModel.superior_id;
                    data["alamat"] = AntiXss.HtmlEncode(fieldModel.alamat);
                    data["tlp"] = AntiXss.HtmlEncode(fieldModel.tlp);
                    data["pendidikan_id"] = fieldModel.pendidikan_id;
                    data["jurusan"] = AntiXss.HtmlEncode(fieldModel.jurusan);
                    data["sap_ba_id"] = fieldModel.sap_ba_id;
                    data["sap_pa_id"] = fieldModel.sap_pa_id;
                    data["sap_psa_id"] = fieldModel.sap_psa_id;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = PersonModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(PersonModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(PersonModel.GetData(PkValue));
                    data_old["tgl_lahir"] = data_old["tgl_lahir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_lahir"]) : "";
                    data_old["job_start"] = data_old["job_start"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["job_start"]) : "";
                    data_old["position_start"] = data_old["position_start"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["position_start"]) : "";
                    data_old["mulai_bekerja"] = data_old["mulai_bekerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["mulai_bekerja"]) : "";
                    data_old["akhir_bekerja"] = data_old["akhir_bekerja"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["akhir_bekerja"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(PersonModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["person_type_id"] = fieldModel.person_type_id;
                    data["company_id"] = fieldModel.company_id;
                    data["nrp"] = fieldModel.nrp;
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["jenis_kelamin_id"] = fieldModel.jenis_kelamin_id;
                    data["tempat_lahir"] = AntiXss.HtmlEncode(fieldModel.tempat_lahir);
                    data["tgl_lahir"] = fieldModel.tgl_lahir;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["jabatan_id"] = fieldModel.jabatan_id;
                    data["jabatan"] = AntiXss.HtmlEncode(fieldModel.jabatan);
                    data["grade"] = AntiXss.HtmlEncode(fieldModel.grade);
                    data["job"] = AntiXss.HtmlEncode(fieldModel.job);
                    data["job_start"] = fieldModel.job_start;
                    data["position"] = AntiXss.HtmlEncode(fieldModel.position);
                    data["position_start"] = fieldModel.position_start;
                    data["photo"] = AntiXss.HtmlEncode(fieldModel.photo);
                    data["status_id"] = fieldModel.status_id;
                    data["mulai_bekerja"] = fieldModel.mulai_bekerja;
                    data["akhir_bekerja"] = fieldModel.akhir_bekerja;
                    data["superior_id"] = fieldModel.superior_id;
                    data["alamat"] = AntiXss.HtmlEncode(fieldModel.alamat);
                    data["tlp"] = AntiXss.HtmlEncode(fieldModel.tlp);
                    data["pendidikan_id"] = fieldModel.pendidikan_id;
                    data["jurusan"] = AntiXss.HtmlEncode(fieldModel.jurusan);
                    data["sap_ba_id"] = fieldModel.sap_ba_id;
                    data["sap_pa_id"] = fieldModel.sap_pa_id;
                    data["sap_psa_id"] = fieldModel.sap_psa_id;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = PersonModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(PersonModel.GetData(PkValue));
						if (data != null)
						{
							result = PersonModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = PersonModel.LookupData(param);
            return Json(data);
        }

        [HttpPost]
        public IActionResult GetViewData(string id)
        {
            var PkValue = new OrderedDictionary();
            DataTable data = new DataTable();
            if (id != null && id != "") {
                PkValue["id"] = id;
                data = PersonModel.GetViewData(PkValue);
            }
            return Json(data);

        }
    }
}