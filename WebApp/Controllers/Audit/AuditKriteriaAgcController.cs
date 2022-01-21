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
    
    public class AuditKriteriaAgcController : Controller
    {
        private string _rule_view = "AuditView";
        private string _rule_add = "AuditAdd";
        private string _rule_edit = "AuditEdit";
        private string _rule_delete = "AuditDelete";
        private string _rule_edit_all = "AuditEditAll";
        private string _rule_delete_all = "AuditDeleteAll";
        private string _path_controler = "/AuditKriteriaAgc/";
        private string _path_view = "/Views/Audit/AuditKriteriaAgc/";
        private readonly string _table_name = "ta_audit_kriteria_agc";
        private readonly string _table_title = "Audit Kriteria Agc";

		private static IHostingEnvironment _hostingEnvironment;
        public AuditKriteriaAgcController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(AuditKriteriaAgcModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = AuditKriteriaAgcModel._pkKey;
                    ViewData["displayItem"] = AuditKriteriaAgcModel.GetDisplayItem();
                    ViewData["column_att"] = AuditKriteriaAgcModel.GetGridColumn();
                    ViewData["param"] = AuditKriteriaAgcModel.GetListParam();
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
        public JsonResult GetListData(AuditKriteriaAgcModel.ActionRequest param)
        {
            GridData data = AuditKriteriaAgcModel.GetListData(param);
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
                    AuditKriteriaAgcModel.ViewModel fieldModel = new AuditKriteriaAgcModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AuditKriteriaAgcModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.audit_id = dr["audit_id"].ToString();
                                fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                                fieldModel.strategy = String.Format("{0:N2}", dr["strategy"]);
                                fieldModel.process = String.Format("{0:N2}", dr["process"]);
                                fieldModel.product = String.Format("{0:N2}", dr["product"]);
                                fieldModel.employee = String.Format("{0:N2}", dr["employee"]);
                                fieldModel.status_akhir_acp_id = dr["status_akhir_acp_id"].ToString();
                                fieldModel.status_akhir_acp_id_text = dr["status_akhir_acp_id_text"].ToString();
                                fieldModel.amdal_id = dr["amdal_id"].ToString();
                                fieldModel.amdal_id_text = dr["amdal_id_text"].ToString();
                                fieldModel.pencemaran_air_id = dr["pencemaran_air_id"].ToString();
                                fieldModel.pencemaran_air_id_text = dr["pencemaran_air_id_text"].ToString();
                                fieldModel.pencemaran_udara_id = dr["pencemaran_udara_id"].ToString();
                                fieldModel.pencemaran_udara_id_text = dr["pencemaran_udara_id_text"].ToString();
                                fieldModel.limbah_lb3_id = dr["limbah_lb3_id"].ToString();
                                fieldModel.limbah_lb3_id_text = dr["limbah_lb3_id_text"].ToString();
                                fieldModel.limbah_nonlb3_id = dr["limbah_nonlb3_id"].ToString();
                                fieldModel.limbah_nonlb3_id_text = dr["limbah_nonlb3_id_text"].ToString();
                                fieldModel.comdev_id = dr["comdev_id"].ToString();
                                fieldModel.comdev_id_text = dr["comdev_id_text"].ToString();
                                fieldModel.status_akhir_cpproper_id = dr["status_akhir_cpproper_id"].ToString();
                                fieldModel.status_akhir_cpproper_id_text = dr["status_akhir_cpproper_id_text"].ToString();
                                fieldModel.sarana_k3_id = dr["sarana_k3_id"].ToString();
                                fieldModel.sarana_k3_id_text = dr["sarana_k3_id_text"].ToString();
                                fieldModel.sarana_ktd_id = dr["sarana_ktd_id"].ToString();
                                fieldModel.sarana_ktd_id_text = dr["sarana_ktd_id_text"].ToString();
                                fieldModel.simulasi_ktd_id = dr["simulasi_ktd_id"].ToString();
                                fieldModel.simulasi_ktd_id_text = dr["simulasi_ktd_id_text"].ToString();
                                fieldModel.fr_tk = String.Format("{0:N2}", dr["fr_tk"]);
                                fieldModel.sr_tf = String.Format("{0:N2}", dr["sr_tf"]);
                                fieldModel.status_akhir_incident_rate_id = dr["status_akhir_incident_rate_id"].ToString();
                                fieldModel.status_akhir_incident_rate_id_text = dr["status_akhir_incident_rate_id_text"].ToString();
                                fieldModel.status_akhir_cpsafety_id = dr["status_akhir_cpsafety_id"].ToString();
                                fieldModel.status_akhir_cpsafety_id_text = dr["status_akhir_cpsafety_id_text"].ToString();
                                fieldModel.status_akhir_legal_comp_id = dr["status_akhir_legal_comp_id"].ToString();
                                fieldModel.status_akhir_legal_comp_id_text = dr["status_akhir_legal_comp_id_text"].ToString();
                                fieldModel.status_akhir_agc_id = dr["status_akhir_agc_id"].ToString();
                                fieldModel.status_akhir_agc_id_text = dr["status_akhir_agc_id_text"].ToString();
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
        public IActionResult Add(AuditKriteriaAgcModel.ViewModel fieldModel)
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
                    //AuditKriteriaAgcModel.ViewModel fieldModel = new AuditKriteriaAgcModel.ViewModel();
                    fieldModel.id = AuditKriteriaAgcModel.GetNewId().ToString();
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
                    DataTable data = AuditKriteriaAgcModel.GetViewData(PkValue);
                    AuditKriteriaAgcModel.ViewModel fieldModel = new AuditKriteriaAgcModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = AuditKriteriaAgcModel.GetNewId().ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.strategy = dr["strategy"].ToString();
                        fieldModel.process = dr["process"].ToString();
                        fieldModel.product = dr["product"].ToString();
                        fieldModel.employee = dr["employee"].ToString();
                        fieldModel.status_akhir_acp_id = dr["status_akhir_acp_id"].ToString();
                        fieldModel.amdal_id = dr["amdal_id"].ToString();
                        fieldModel.pencemaran_air_id = dr["pencemaran_air_id"].ToString();
                        fieldModel.pencemaran_udara_id = dr["pencemaran_udara_id"].ToString();
                        fieldModel.limbah_lb3_id = dr["limbah_lb3_id"].ToString();
                        fieldModel.limbah_nonlb3_id = dr["limbah_nonlb3_id"].ToString();
                        fieldModel.comdev_id = dr["comdev_id"].ToString();
                        fieldModel.status_akhir_cpproper_id = dr["status_akhir_cpproper_id"].ToString();
                        fieldModel.sarana_k3_id = dr["sarana_k3_id"].ToString();
                        fieldModel.sarana_ktd_id = dr["sarana_ktd_id"].ToString();
                        fieldModel.simulasi_ktd_id = dr["simulasi_ktd_id"].ToString();
                        fieldModel.fr_tk = dr["fr_tk"].ToString();
                        fieldModel.sr_tf = dr["sr_tf"].ToString();
                        fieldModel.status_akhir_incident_rate_id = dr["status_akhir_incident_rate_id"].ToString();
                        fieldModel.status_akhir_cpsafety_id = dr["status_akhir_cpsafety_id"].ToString();
                        fieldModel.status_akhir_legal_comp_id = dr["status_akhir_legal_comp_id"].ToString();
                        fieldModel.status_akhir_agc_id = dr["status_akhir_agc_id"].ToString();
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
                    DataTable data = AuditKriteriaAgcModel.GetViewData(PkValue);
                    AuditKriteriaAgcModel.ViewModel fieldModel = new AuditKriteriaAgcModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.strategy = dr["strategy"].ToString();
                        fieldModel.process = dr["process"].ToString();
                        fieldModel.product = dr["product"].ToString();
                        fieldModel.employee = dr["employee"].ToString();
                        fieldModel.status_akhir_acp_id = dr["status_akhir_acp_id"].ToString();
                        fieldModel.status_akhir_acp_id_text = dr["status_akhir_acp_id_text"].ToString();
                        fieldModel.amdal_id = dr["amdal_id"].ToString();
                        fieldModel.amdal_id_text = dr["amdal_id_text"].ToString();
                        fieldModel.pencemaran_air_id = dr["pencemaran_air_id"].ToString();
                        fieldModel.pencemaran_air_id_text = dr["pencemaran_air_id_text"].ToString();
                        fieldModel.pencemaran_udara_id = dr["pencemaran_udara_id"].ToString();
                        fieldModel.pencemaran_udara_id_text = dr["pencemaran_udara_id_text"].ToString();
                        fieldModel.limbah_lb3_id = dr["limbah_lb3_id"].ToString();
                        fieldModel.limbah_lb3_id_text = dr["limbah_lb3_id_text"].ToString();
                        fieldModel.limbah_nonlb3_id = dr["limbah_nonlb3_id"].ToString();
                        fieldModel.limbah_nonlb3_id_text = dr["limbah_nonlb3_id_text"].ToString();
                        fieldModel.comdev_id = dr["comdev_id"].ToString();
                        fieldModel.comdev_id_text = dr["comdev_id_text"].ToString();
                        fieldModel.status_akhir_cpproper_id = dr["status_akhir_cpproper_id"].ToString();
                        fieldModel.status_akhir_cpproper_id_text = dr["status_akhir_cpproper_id_text"].ToString();
                        fieldModel.sarana_k3_id = dr["sarana_k3_id"].ToString();
                        fieldModel.sarana_k3_id_text = dr["sarana_k3_id_text"].ToString();
                        fieldModel.sarana_ktd_id = dr["sarana_ktd_id"].ToString();
                        fieldModel.sarana_ktd_id_text = dr["sarana_ktd_id_text"].ToString();
                        fieldModel.simulasi_ktd_id = dr["simulasi_ktd_id"].ToString();
                        fieldModel.simulasi_ktd_id_text = dr["simulasi_ktd_id_text"].ToString();
                        fieldModel.fr_tk = dr["fr_tk"].ToString();
                        fieldModel.sr_tf = dr["sr_tf"].ToString();
                        fieldModel.status_akhir_incident_rate_id = dr["status_akhir_incident_rate_id"].ToString();
                        fieldModel.status_akhir_incident_rate_id_text = dr["status_akhir_incident_rate_id_text"].ToString();
                        fieldModel.status_akhir_cpsafety_id = dr["status_akhir_cpsafety_id"].ToString();
                        fieldModel.status_akhir_cpsafety_id_text = dr["status_akhir_cpsafety_id_text"].ToString();
                        fieldModel.status_akhir_legal_comp_id = dr["status_akhir_legal_comp_id"].ToString();
                        fieldModel.status_akhir_legal_comp_id_text = dr["status_akhir_legal_comp_id_text"].ToString();
                        fieldModel.status_akhir_agc_id = dr["status_akhir_agc_id"].ToString();
                        fieldModel.status_akhir_agc_id_text = dr["status_akhir_agc_id_text"].ToString();
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
        public JsonResult Insert(AuditKriteriaAgcModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = AuditKriteriaAgcModel.GetNewId().ToString();
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["strategy"] = fieldModel.strategy != null ? fieldModel.strategy.Replace(",",".") : "";
                    data["process"] = fieldModel.process != null ? fieldModel.process.Replace(",",".") : "";
                    data["product"] = fieldModel.product != null ? fieldModel.product.Replace(",",".") : "";
                    data["employee"] = fieldModel.employee != null ? fieldModel.employee.Replace(",",".") : "";
                    data["status_akhir_acp_id"] = fieldModel.status_akhir_acp_id;
                    data["amdal_id"] = fieldModel.amdal_id;
                    data["pencemaran_air_id"] = fieldModel.pencemaran_air_id;
                    data["pencemaran_udara_id"] = fieldModel.pencemaran_udara_id;
                    data["limbah_lb3_id"] = fieldModel.limbah_lb3_id;
                    data["limbah_nonlb3_id"] = fieldModel.limbah_nonlb3_id;
                    data["comdev_id"] = fieldModel.comdev_id;
                    data["status_akhir_cpproper_id"] = fieldModel.status_akhir_cpproper_id;
                    data["sarana_k3_id"] = fieldModel.sarana_k3_id;
                    data["sarana_ktd_id"] = fieldModel.sarana_ktd_id;
                    data["simulasi_ktd_id"] = fieldModel.simulasi_ktd_id;
                    data["fr_tk"] = fieldModel.fr_tk != null ? fieldModel.fr_tk.Replace(",",".") : "";
                    data["sr_tf"] = fieldModel.sr_tf != null ? fieldModel.sr_tf.Replace(",",".") : "";
                    data["status_akhir_incident_rate_id"] = fieldModel.status_akhir_incident_rate_id;
                    data["status_akhir_cpsafety_id"] = fieldModel.status_akhir_cpsafety_id;
                    data["status_akhir_legal_comp_id"] = fieldModel.status_akhir_legal_comp_id;
                    data["status_akhir_agc_id"] = fieldModel.status_akhir_agc_id;
                    data["lampiran_file_path"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_path);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    

                    //---------------------------------------------------------------------------------------------------
                    DoCalculation(ref data);
                    //---------------------------------------------------------------------------------------------------
                    

                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = AuditKriteriaAgcModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(AuditKriteriaAgcModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(AuditKriteriaAgcModel.GetData(PkValue));
                    data_old["strategy"] = data_old["strategy"].ToString() != ""  ? data_old["strategy"].ToString().Replace(",", ".") : "";
                    data_old["process"] = data_old["process"].ToString() != ""  ? data_old["process"].ToString().Replace(",", ".") : "";
                    data_old["product"] = data_old["product"].ToString() != ""  ? data_old["product"].ToString().Replace(",", ".") : "";
                    data_old["employee"] = data_old["employee"].ToString() != ""  ? data_old["employee"].ToString().Replace(",", ".") : "";
                    data_old["fr_tk"] = data_old["fr_tk"].ToString() != ""  ? data_old["fr_tk"].ToString().Replace(",", ".") : "";
                    data_old["sr_tf"] = data_old["sr_tf"].ToString() != ""  ? data_old["sr_tf"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaAgcModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["strategy"] = fieldModel.strategy != null ? fieldModel.strategy.ToString().Replace(",", ".") : "";
                    data["process"] = fieldModel.process != null ? fieldModel.process.ToString().Replace(",", ".") : "";
                    data["product"] = fieldModel.product != null ? fieldModel.product.ToString().Replace(",", ".") : "";
                    data["employee"] = fieldModel.employee != null ? fieldModel.employee.ToString().Replace(",", ".") : "";
                    data["status_akhir_acp_id"] = fieldModel.status_akhir_acp_id;
                    data["amdal_id"] = fieldModel.amdal_id;
                    data["pencemaran_air_id"] = fieldModel.pencemaran_air_id;
                    data["pencemaran_udara_id"] = fieldModel.pencemaran_udara_id;
                    data["limbah_lb3_id"] = fieldModel.limbah_lb3_id;
                    data["limbah_nonlb3_id"] = fieldModel.limbah_nonlb3_id;
                    data["comdev_id"] = fieldModel.comdev_id;
                    data["status_akhir_cpproper_id"] = fieldModel.status_akhir_cpproper_id;
                    data["sarana_k3_id"] = fieldModel.sarana_k3_id;
                    data["sarana_ktd_id"] = fieldModel.sarana_ktd_id;
                    data["simulasi_ktd_id"] = fieldModel.simulasi_ktd_id;
                    data["fr_tk"] = fieldModel.fr_tk != null ? fieldModel.fr_tk.ToString().Replace(",", ".") : "";
                    data["sr_tf"] = fieldModel.sr_tf != null ? fieldModel.sr_tf.ToString().Replace(",", ".") : "";
                    data["status_akhir_incident_rate_id"] = fieldModel.status_akhir_incident_rate_id;
                    data["status_akhir_cpsafety_id"] = fieldModel.status_akhir_cpsafety_id;
                    data["status_akhir_legal_comp_id"] = fieldModel.status_akhir_legal_comp_id;
                    data["status_akhir_agc_id"] = fieldModel.status_akhir_agc_id;
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
                        result = AuditKriteriaAgcModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaAgcModel.GetData(PkValue));
						if (data != null)
						{
							result = AuditKriteriaAgcModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = AuditKriteriaAgcModel.LookupData(param);
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
            data["status_akhir_acp_id"] = AuditKriteriaAgcModel.GetStatusAkhirACP(
                data["strategy"].ToString(),
                data["process"].ToString(),
                data["product"].ToString(),
                data["employee"].ToString()
            );
            data["status_akhir_cpproper_id"] = AuditKriteriaAgcModel.GetStatusAkhirCPProper(
                data["amdal_id"].ToString(),
                data["pencemaran_air_id"].ToString(),
                data["pencemaran_udara_id"].ToString(),
                data["limbah_lb3_id"].ToString(),
                data["limbah_nonlb3_id"].ToString(),
                data["comdev_id"].ToString()
            );
            data["status_akhir_incident_rate_id"] = AuditKriteriaAgcModel.GetStatusAkhirIncidentRate(
                data["audit_id"].ToString(),
                data["fr_tk"].ToString(),
                data["sr_tf"].ToString()
            );
            data["status_akhir_cpsafety_id"] = AuditKriteriaAgcModel.GetStatusAkhirCPSafety(
                data["sarana_k3_id"].ToString(),
                data["sarana_ktd_id"].ToString(),
                data["simulasi_ktd_id"].ToString(),
                data["audit_id"].ToString(),
                data["fr_tk"].ToString(),
                data["sr_tf"].ToString()
            );
            data["status_akhir_agc_id"] = AuditKriteriaAgcModel.GetStatusAkhirAGC(
                data["status_akhir_acp_id"].ToString(),
                data["status_akhir_cpproper_id"].ToString(),
                data["status_akhir_cpsafety_id"].ToString(),
                data["status_akhir_legal_comp_id"].ToString()
            );
        }



    }
}