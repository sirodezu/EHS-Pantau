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
    
    public class AuditKriteriaCsmsController : Controller
    {
        private string _rule_view = "AuditView";
        private string _rule_add = "AuditAdd";
        private string _rule_edit = "AuditEdit";
        private string _rule_delete = "AuditDelete";
        private string _rule_edit_all = "AuditEditAll";
        private string _rule_delete_all = "AuditDeleteAll";
        private string _path_controler = "/AuditKriteriaCsms/";
        private string _path_view = "/Views/Audit/AuditKriteriaCsms/";
        private readonly string _table_name = "ta_audit_kriteria_csms";
        private readonly string _table_title = "Audit Kriteria Csms";

		private static IHostingEnvironment _hostingEnvironment;
        public AuditKriteriaCsmsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(AuditKriteriaCsmsModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = AuditKriteriaCsmsModel._pkKey;
                    ViewData["displayItem"] = AuditKriteriaCsmsModel.GetDisplayItem();
                    ViewData["column_att"] = AuditKriteriaCsmsModel.GetGridColumn();
                    ViewData["param"] = AuditKriteriaCsmsModel.GetListParam();
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
        public JsonResult GetListData(AuditKriteriaCsmsModel.ActionRequest param)
        {
            GridData data = AuditKriteriaCsmsModel.GetListData(param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["lampiran_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "lampiran_file_path", data.data.Rows[i]["lampiran_file_path"].ToString());
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
                    AuditKriteriaCsmsModel.ViewModel fieldModel = new AuditKriteriaCsmsModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AuditKriteriaCsmsModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.audit_id = dr["audit_id"].ToString();
                                fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                                fieldModel.csms_kategori_id = dr["csms_kategori_id"].ToString();
                                fieldModel.csms_kategori_id_text = dr["csms_kategori_id_text"].ToString();
                                fieldModel.csms_tahapan_id = dr["csms_tahapan_id"].ToString();
                                fieldModel.csms_tahapan_id_text = dr["csms_tahapan_id_text"].ToString();
                                fieldModel.nama_vendor = dr["nama_vendor"].ToString();
                                fieldModel.csms_resiko_id = dr["csms_resiko_id"].ToString();
                                fieldModel.csms_resiko_id_text = dr["csms_resiko_id_text"].ToString();
                                fieldModel.green_strategy_bobot = String.Format("{0:N2}", dr["green_strategy_bobot"]);
                                fieldModel.green_process_bobot = String.Format("{0:N2}", dr["green_process_bobot"]);
                                fieldModel.green_product_bobot = String.Format("{0:N2}", dr["green_product_bobot"]);
                                fieldModel.green_employee_bobot = String.Format("{0:N2}", dr["green_employee_bobot"]);
                                fieldModel.legal_bobot = String.Format("{0:N2}", dr["legal_bobot"]);
                                fieldModel.green_strategy_actual = String.Format("{0:N2}", dr["green_strategy_actual"]);
                                fieldModel.green_process_actual = String.Format("{0:N2}", dr["green_process_actual"]);
                                fieldModel.green_product_actual = String.Format("{0:N2}", dr["green_product_actual"]);
                                fieldModel.green_employee_actual = String.Format("{0:N2}", dr["green_employee_actual"]);
                                fieldModel.legal_actual = String.Format("{0:N2}", dr["legal_actual"]);
                                fieldModel.green_strategy_penuh = String.Format("{0:N2}", dr["green_strategy_penuh"]);
                                fieldModel.green_strategy_penuh_text = dr["green_strategy_penuh_text"].ToString();
                                fieldModel.green_process_penuh = String.Format("{0:N2}", dr["green_process_penuh"]);
                                fieldModel.green_process_penuh_text = dr["green_process_penuh_text"].ToString();
                                fieldModel.green_product_penuh = String.Format("{0:N2}", dr["green_product_penuh"]);
                                fieldModel.green_product_penuh_text = dr["green_product_penuh_text"].ToString();
                                fieldModel.green_employee_penuh = String.Format("{0:N2}", dr["green_employee_penuh"]);
                                fieldModel.green_employee_penuh_text = dr["green_employee_penuh_text"].ToString();
                                fieldModel.legal_penuh = String.Format("{0:N2}", dr["legal_penuh"]);
                                fieldModel.legal_penuh_text = dr["legal_penuh_text"].ToString();
                                fieldModel.presentasi_pencapaian = String.Format("{0:N2}", dr["presentasi_pencapaian"]);
                                fieldModel.nilai_bendera_id = dr["nilai_bendera_id"].ToString();
                                fieldModel.nilai_bendera_id_text = dr["nilai_bendera_id_text"].ToString();
                                fieldModel.lampiran_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "lampiran_file_path", dr["lampiran_file_path"].ToString());
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
        public IActionResult Add(AuditKriteriaCsmsModel.ViewModel fieldModel)
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
                    //AuditKriteriaCsmsModel.ViewModel fieldModel = new AuditKriteriaCsmsModel.ViewModel();
                    fieldModel.id = AuditKriteriaCsmsModel.GetNewId().ToString();
                    fieldModel.lampiran_file_path_init = "[]";
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
                    DataTable data = AuditKriteriaCsmsModel.GetViewData(PkValue);
                    AuditKriteriaCsmsModel.ViewModel fieldModel = new AuditKriteriaCsmsModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = AuditKriteriaCsmsModel.GetNewId().ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.csms_kategori_id = dr["csms_kategori_id"].ToString();
                        fieldModel.csms_tahapan_id = dr["csms_tahapan_id"].ToString();
                        fieldModel.nama_vendor = dr["nama_vendor"].ToString();
                        fieldModel.csms_resiko_id = dr["csms_resiko_id"].ToString();
                        fieldModel.green_strategy_bobot = dr["green_strategy_bobot"].ToString();
                        fieldModel.green_process_bobot = dr["green_process_bobot"].ToString();
                        fieldModel.green_product_bobot = dr["green_product_bobot"].ToString();
                        fieldModel.green_employee_bobot = dr["green_employee_bobot"].ToString();
                        fieldModel.legal_bobot = dr["legal_bobot"].ToString();
                        fieldModel.green_strategy_actual = dr["green_strategy_actual"].ToString();
                        fieldModel.green_process_actual = dr["green_process_actual"].ToString();
                        fieldModel.green_product_actual = dr["green_product_actual"].ToString();
                        fieldModel.green_employee_actual = dr["green_employee_actual"].ToString();
                        fieldModel.legal_actual = dr["legal_actual"].ToString();
                        fieldModel.green_strategy_penuh = dr["green_strategy_penuh"].ToString();
                        fieldModel.green_process_penuh = dr["green_process_penuh"].ToString();
                        fieldModel.green_product_penuh = dr["green_product_penuh"].ToString();
                        fieldModel.green_employee_penuh = dr["green_employee_penuh"].ToString();
                        fieldModel.legal_penuh = dr["legal_penuh"].ToString();
                        fieldModel.presentasi_pencapaian = dr["presentasi_pencapaian"].ToString();
                        fieldModel.nilai_bendera_id = dr["nilai_bendera_id"].ToString();
                        fieldModel.lampiran_file_path = dr["lampiran_file_path"].ToString();
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
                    DataTable data = AuditKriteriaCsmsModel.GetViewData(PkValue);
                    AuditKriteriaCsmsModel.ViewModel fieldModel = new AuditKriteriaCsmsModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.csms_kategori_id = dr["csms_kategori_id"].ToString();
                        fieldModel.csms_kategori_id_text = dr["csms_kategori_id_text"].ToString();
                        fieldModel.csms_tahapan_id = dr["csms_tahapan_id"].ToString();
                        fieldModel.csms_tahapan_id_text = dr["csms_tahapan_id_text"].ToString();
                        fieldModel.nama_vendor = dr["nama_vendor"].ToString();
                        fieldModel.csms_resiko_id = dr["csms_resiko_id"].ToString();
                        fieldModel.csms_resiko_id_text = dr["csms_resiko_id_text"].ToString();
                        fieldModel.green_strategy_bobot = dr["green_strategy_bobot"].ToString();
                        fieldModel.green_process_bobot = dr["green_process_bobot"].ToString();
                        fieldModel.green_product_bobot = dr["green_product_bobot"].ToString();
                        fieldModel.green_employee_bobot = dr["green_employee_bobot"].ToString();
                        fieldModel.legal_bobot = dr["legal_bobot"].ToString();
                        fieldModel.green_strategy_actual = dr["green_strategy_actual"].ToString();
                        fieldModel.green_process_actual = dr["green_process_actual"].ToString();
                        fieldModel.green_product_actual = dr["green_product_actual"].ToString();
                        fieldModel.green_employee_actual = dr["green_employee_actual"].ToString();
                        fieldModel.legal_actual = dr["legal_actual"].ToString();
                        fieldModel.green_strategy_penuh = dr["green_strategy_penuh"].ToString();
                        fieldModel.green_strategy_penuh_text = dr["green_strategy_penuh_text"].ToString();
                        fieldModel.green_process_penuh = dr["green_process_penuh"].ToString();
                        fieldModel.green_process_penuh_text = dr["green_process_penuh_text"].ToString();
                        fieldModel.green_product_penuh = dr["green_product_penuh"].ToString();
                        fieldModel.green_product_penuh_text = dr["green_product_penuh_text"].ToString();
                        fieldModel.green_employee_penuh = dr["green_employee_penuh"].ToString();
                        fieldModel.green_employee_penuh_text = dr["green_employee_penuh_text"].ToString();
                        fieldModel.legal_penuh = dr["legal_penuh"].ToString();
                        fieldModel.legal_penuh_text = dr["legal_penuh_text"].ToString();
                        fieldModel.presentasi_pencapaian = dr["presentasi_pencapaian"].ToString();
                        fieldModel.nilai_bendera_id = dr["nilai_bendera_id"].ToString();
                        fieldModel.nilai_bendera_id_text = dr["nilai_bendera_id_text"].ToString();
                        string[] init_lampiran_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "lampiran_file_path", PkValue, dr["lampiran_file_path"].ToString());
                        fieldModel.lampiran_file_path = init_lampiran_file_path[0];
                        fieldModel.lampiran_file_path_init = init_lampiran_file_path[1];
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
        public JsonResult Insert(AuditKriteriaCsmsModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = AuditKriteriaCsmsModel.GetNewId().ToString();
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["csms_kategori_id"] = fieldModel.csms_kategori_id;
                    data["csms_tahapan_id"] = fieldModel.csms_tahapan_id;
                    data["nama_vendor"] = AntiXss.HtmlEncode(fieldModel.nama_vendor);
                    data["csms_resiko_id"] = fieldModel.csms_resiko_id;
                    data["green_strategy_bobot"] = fieldModel.green_strategy_bobot != null ? fieldModel.green_strategy_bobot.Replace(",",".") : "";
                    data["green_process_bobot"] = fieldModel.green_process_bobot != null ? fieldModel.green_process_bobot.Replace(",",".") : "";
                    data["green_product_bobot"] = fieldModel.green_product_bobot != null ? fieldModel.green_product_bobot.Replace(",",".") : "";
                    data["green_employee_bobot"] = fieldModel.green_employee_bobot != null ? fieldModel.green_employee_bobot.Replace(",",".") : "";
                    data["legal_bobot"] = fieldModel.legal_bobot != null ? fieldModel.legal_bobot.Replace(",",".") : "";
                    data["green_strategy_actual"] = fieldModel.green_strategy_actual != null ? fieldModel.green_strategy_actual.Replace(",",".") : "";
                    data["green_process_actual"] = fieldModel.green_process_actual != null ? fieldModel.green_process_actual.Replace(",",".") : "";
                    data["green_product_actual"] = fieldModel.green_product_actual != null ? fieldModel.green_product_actual.Replace(",",".") : "";
                    data["green_employee_actual"] = fieldModel.green_employee_actual != null ? fieldModel.green_employee_actual.Replace(",",".") : "";
                    data["legal_actual"] = fieldModel.legal_actual != null ? fieldModel.legal_actual.Replace(",",".") : "";
                    data["green_strategy_penuh"] = fieldModel.green_strategy_penuh != null ? fieldModel.green_strategy_penuh.Replace(",",".") : "";
                    data["green_process_penuh"] = fieldModel.green_process_penuh != null ? fieldModel.green_process_penuh.Replace(",",".") : "";
                    data["green_product_penuh"] = fieldModel.green_product_penuh != null ? fieldModel.green_product_penuh.Replace(",",".") : "";
                    data["green_employee_penuh"] = fieldModel.green_employee_penuh != null ? fieldModel.green_employee_penuh.Replace(",",".") : "";
                    data["legal_penuh"] = fieldModel.legal_penuh != null ? fieldModel.legal_penuh.Replace(",",".") : "";
                    data["presentasi_pencapaian"] = fieldModel.presentasi_pencapaian != null ? fieldModel.presentasi_pencapaian.Replace(",",".") : "";
                    data["nilai_bendera_id"] = fieldModel.nilai_bendera_id;
                    data["lampiran_file_path"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_path);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];

                    //---------------------------------------------------------------------------------------------------
                    DoCalculation(ref data);
                    //---------------------------------------------------------------------------------------------------	

                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = AuditKriteriaCsmsModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(AuditKriteriaCsmsModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(AuditKriteriaCsmsModel.GetData(PkValue));
                    data_old["green_strategy_bobot"] = data_old["green_strategy_bobot"].ToString() != ""  ? data_old["green_strategy_bobot"].ToString().Replace(",", ".") : "";
                    data_old["green_process_bobot"] = data_old["green_process_bobot"].ToString() != ""  ? data_old["green_process_bobot"].ToString().Replace(",", ".") : "";
                    data_old["green_product_bobot"] = data_old["green_product_bobot"].ToString() != ""  ? data_old["green_product_bobot"].ToString().Replace(",", ".") : "";
                    data_old["green_employee_bobot"] = data_old["green_employee_bobot"].ToString() != ""  ? data_old["green_employee_bobot"].ToString().Replace(",", ".") : "";
                    data_old["legal_bobot"] = data_old["legal_bobot"].ToString() != ""  ? data_old["legal_bobot"].ToString().Replace(",", ".") : "";
                    data_old["green_strategy_actual"] = data_old["green_strategy_actual"].ToString() != ""  ? data_old["green_strategy_actual"].ToString().Replace(",", ".") : "";
                    data_old["green_process_actual"] = data_old["green_process_actual"].ToString() != ""  ? data_old["green_process_actual"].ToString().Replace(",", ".") : "";
                    data_old["green_product_actual"] = data_old["green_product_actual"].ToString() != ""  ? data_old["green_product_actual"].ToString().Replace(",", ".") : "";
                    data_old["green_employee_actual"] = data_old["green_employee_actual"].ToString() != ""  ? data_old["green_employee_actual"].ToString().Replace(",", ".") : "";
                    data_old["legal_actual"] = data_old["legal_actual"].ToString() != ""  ? data_old["legal_actual"].ToString().Replace(",", ".") : "";
                    data_old["green_strategy_penuh"] = data_old["green_strategy_penuh"].ToString() != ""  ? data_old["green_strategy_penuh"].ToString().Replace(",", ".") : "";
                    data_old["green_process_penuh"] = data_old["green_process_penuh"].ToString() != ""  ? data_old["green_process_penuh"].ToString().Replace(",", ".") : "";
                    data_old["green_product_penuh"] = data_old["green_product_penuh"].ToString() != ""  ? data_old["green_product_penuh"].ToString().Replace(",", ".") : "";
                    data_old["green_employee_penuh"] = data_old["green_employee_penuh"].ToString() != ""  ? data_old["green_employee_penuh"].ToString().Replace(",", ".") : "";
                    data_old["legal_penuh"] = data_old["legal_penuh"].ToString() != ""  ? data_old["legal_penuh"].ToString().Replace(",", ".") : "";
                    data_old["presentasi_pencapaian"] = data_old["presentasi_pencapaian"].ToString() != ""  ? data_old["presentasi_pencapaian"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaCsmsModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["csms_kategori_id"] = fieldModel.csms_kategori_id;
                    data["csms_tahapan_id"] = fieldModel.csms_tahapan_id;
                    data["nama_vendor"] = AntiXss.HtmlEncode(fieldModel.nama_vendor);
                    data["csms_resiko_id"] = fieldModel.csms_resiko_id;
                    data["green_strategy_bobot"] = fieldModel.green_strategy_bobot != null ? fieldModel.green_strategy_bobot.ToString().Replace(",", ".") : "";
                    data["green_process_bobot"] = fieldModel.green_process_bobot != null ? fieldModel.green_process_bobot.ToString().Replace(",", ".") : "";
                    data["green_product_bobot"] = fieldModel.green_product_bobot != null ? fieldModel.green_product_bobot.ToString().Replace(",", ".") : "";
                    data["green_employee_bobot"] = fieldModel.green_employee_bobot != null ? fieldModel.green_employee_bobot.ToString().Replace(",", ".") : "";
                    data["legal_bobot"] = fieldModel.legal_bobot != null ? fieldModel.legal_bobot.ToString().Replace(",", ".") : "";
                    data["green_strategy_actual"] = fieldModel.green_strategy_actual != null ? fieldModel.green_strategy_actual.ToString().Replace(",", ".") : "";
                    data["green_process_actual"] = fieldModel.green_process_actual != null ? fieldModel.green_process_actual.ToString().Replace(",", ".") : "";
                    data["green_product_actual"] = fieldModel.green_product_actual != null ? fieldModel.green_product_actual.ToString().Replace(",", ".") : "";
                    data["green_employee_actual"] = fieldModel.green_employee_actual != null ? fieldModel.green_employee_actual.ToString().Replace(",", ".") : "";
                    data["legal_actual"] = fieldModel.legal_actual != null ? fieldModel.legal_actual.ToString().Replace(",", ".") : "";
                    data["green_strategy_penuh"] = fieldModel.green_strategy_penuh != null ? fieldModel.green_strategy_penuh.ToString().Replace(",", ".") : "";
                    data["green_process_penuh"] = fieldModel.green_process_penuh != null ? fieldModel.green_process_penuh.ToString().Replace(",", ".") : "";
                    data["green_product_penuh"] = fieldModel.green_product_penuh != null ? fieldModel.green_product_penuh.ToString().Replace(",", ".") : "";
                    data["green_employee_penuh"] = fieldModel.green_employee_penuh != null ? fieldModel.green_employee_penuh.ToString().Replace(",", ".") : "";
                    data["legal_penuh"] = fieldModel.legal_penuh != null ? fieldModel.legal_penuh.ToString().Replace(",", ".") : "";
                    data["presentasi_pencapaian"] = fieldModel.presentasi_pencapaian != null ? fieldModel.presentasi_pencapaian.ToString().Replace(",", ".") : "";
                    data["nilai_bendera_id"] = fieldModel.nilai_bendera_id;
                    data["lampiran_file_path"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_path);

                    //---------------------------------------------------------------------------------------------------
                    DoCalculation(ref data);
                    //---------------------------------------------------------------------------------------------------	


                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = AuditKriteriaCsmsModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaCsmsModel.GetData(PkValue));
						if (data != null)
						{
							result = AuditKriteriaCsmsModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = AuditKriteriaCsmsModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_lampiran_file_path(IEnumerable<IFormFile> lampiran_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "lampiran_file_path");
            if (lampiran_file_path_file != null)
            {
                foreach (var file in lampiran_file_path_file)
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

        public ActionResult remove_lampiran_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "lampiran_file_path");
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




        public void DoCalculation(ref OrderedDictionary data)
        {
            double green_strategy_bobot = double.Parse(data["green_strategy_bobot"].ToString()) / double.Parse("100");
            double green_process_bobot = double.Parse(data["green_process_bobot"].ToString()) / double.Parse("100");
            double green_product_bobot = double.Parse(data["green_product_bobot"].ToString()) / double.Parse("100");
            double green_employee_bobot = double.Parse(data["green_employee_bobot"].ToString()) / double.Parse("100");
            double legal_bobot = double.Parse(data["legal_bobot"].ToString()) / double.Parse("100");

            string str_green_strategy_actual = data["green_strategy_actual"].ToString();
            string str_green_process_actual = data["green_process_actual"].ToString();
            string str_green_product_actual = data["green_product_actual"].ToString();
            string str_green_employee_actual = data["green_employee_actual"].ToString();
            string str_legal_actual = data["legal_actual"].ToString();

            double green_strategy_actual = (str_green_strategy_actual == "" ? 0 : double.Parse(str_green_strategy_actual));
            double green_process_actual = (str_green_process_actual == "" ? 0 : double.Parse(str_green_process_actual));
            double green_product_actual = (str_green_product_actual == "" ? 0 : double.Parse(str_green_product_actual));
            double green_employee_actual = (str_green_employee_actual == "" ? 0 : double.Parse(str_green_employee_actual));
            double legal_actual = (str_legal_actual == "" ? 0 : double.Parse(str_legal_actual));





            double green_strategy_penuh = 0;
            double green_process_penuh = 0;
            double green_product_penuh = 0;
            double green_employee_penuh = 0;
            double legal_penuh = 0;


            green_strategy_penuh = green_strategy_actual * green_strategy_bobot;
            green_process_penuh = green_process_actual * green_process_bobot;
            green_product_penuh = green_product_actual * green_product_bobot;
            green_employee_penuh = green_employee_actual * green_employee_bobot;
            legal_penuh = legal_actual * legal_bobot;

            data["green_strategy_penuh"] = green_strategy_penuh.ToString().Replace(",", ".");
            data["green_process_penuh"] = green_process_penuh.ToString().Replace(",", ".");
            data["green_product_penuh"] = green_product_penuh.ToString().Replace(",", ".");
            data["green_employee_penuh"] = green_employee_penuh.ToString().Replace(",", ".");
            data["legal_penuh"] = legal_penuh.ToString().Replace(",", ".");


            double presentasi_pencapaian =
                green_strategy_penuh +
                green_process_penuh +
                green_product_penuh +
                green_employee_penuh +
                legal_penuh;
            data["presentasi_pencapaian"] = presentasi_pencapaian;


            int bendera_id = 0;
            if (75 <= presentasi_pencapaian)
            {
                bendera_id = 1;
            }
            else if (60 <= presentasi_pencapaian & presentasi_pencapaian < 75)
            {
                bendera_id = 2;
            }
            else if (0 < presentasi_pencapaian & presentasi_pencapaian < 60)
            {
                bendera_id = 3;
            }
            else if (presentasi_pencapaian == 0)
            {
                bendera_id = 4;
            }
            data["nilai_bendera_id"] = bendera_id;
        }

    }
}