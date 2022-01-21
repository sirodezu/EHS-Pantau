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
    
    public class AuditKriteriaSmkpController : Controller
    {
        private string _rule_view = "AuditView";
        private string _rule_add = "AuditAdd";
        private string _rule_edit = "AuditEdit";
        private string _rule_delete = "AuditDelete";
        private string _rule_edit_all = "AuditEditAll";
        private string _rule_delete_all = "AuditDeleteAll";
        private string _path_controler = "/AuditKriteriaSmkp/";
        private string _path_view = "/Views/Audit/AuditKriteriaSmkp/";
        private readonly string _table_name = "ta_audit_kriteria_smkp";
        private readonly string _table_title = "Audit Kriteria Smkp";

		private static IHostingEnvironment _hostingEnvironment;
        public AuditKriteriaSmkpController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(AuditKriteriaSmkpModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = AuditKriteriaSmkpModel._pkKey;
                    ViewData["displayItem"] = AuditKriteriaSmkpModel.GetDisplayItem();
                    ViewData["column_att"] = AuditKriteriaSmkpModel.GetGridColumn();
                    ViewData["param"] = AuditKriteriaSmkpModel.GetListParam();
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
        public JsonResult GetListData(AuditKriteriaSmkpModel.ActionRequest param)
        {
            GridData data = AuditKriteriaSmkpModel.GetListData(param);
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
                    AuditKriteriaSmkpModel.ViewModel fieldModel = new AuditKriteriaSmkpModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AuditKriteriaSmkpModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.audit_id = dr["audit_id"].ToString();
                                fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                                fieldModel.kebijakan = String.Format("{0:N2}", dr["kebijakan"]);
                                fieldModel.perencanaan = String.Format("{0:N2}", dr["perencanaan"]);
                                fieldModel.organisasi = String.Format("{0:N2}", dr["organisasi"]);
                                fieldModel.implementasi = String.Format("{0:N2}", dr["implementasi"]);
                                fieldModel.evaluasi = String.Format("{0:N2}", dr["evaluasi"]);
                                fieldModel.dokumentasi = String.Format("{0:N2}", dr["dokumentasi"]);
                                fieldModel.manajemen = String.Format("{0:N2}", dr["manajemen"]);
                                fieldModel.total_internal = String.Format("{0:N2}", dr["total_internal"]);
                                fieldModel.total_standar = String.Format("{0:N2}", dr["total_standar"]);
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
        public IActionResult Add(AuditKriteriaSmkpModel.ViewModel fieldModel)
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
                    //AuditKriteriaSmkpModel.ViewModel fieldModel = new AuditKriteriaSmkpModel.ViewModel();
                    fieldModel.id = AuditKriteriaSmkpModel.GetNewId().ToString();
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
                    DataTable data = AuditKriteriaSmkpModel.GetViewData(PkValue);
                    AuditKriteriaSmkpModel.ViewModel fieldModel = new AuditKriteriaSmkpModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = AuditKriteriaSmkpModel.GetNewId().ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.kebijakan = dr["kebijakan"].ToString();
                        fieldModel.perencanaan = dr["perencanaan"].ToString();
                        fieldModel.organisasi = dr["organisasi"].ToString();
                        fieldModel.implementasi = dr["implementasi"].ToString();
                        fieldModel.evaluasi = dr["evaluasi"].ToString();
                        fieldModel.dokumentasi = dr["dokumentasi"].ToString();
                        fieldModel.manajemen = dr["manajemen"].ToString();
                        fieldModel.total_internal = dr["total_internal"].ToString();
                        fieldModel.total_standar = dr["total_standar"].ToString();
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
                    DataTable data = AuditKriteriaSmkpModel.GetViewData(PkValue);
                    AuditKriteriaSmkpModel.ViewModel fieldModel = new AuditKriteriaSmkpModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.kebijakan = dr["kebijakan"].ToString();
                        fieldModel.perencanaan = dr["perencanaan"].ToString();
                        fieldModel.organisasi = dr["organisasi"].ToString();
                        fieldModel.implementasi = dr["implementasi"].ToString();
                        fieldModel.evaluasi = dr["evaluasi"].ToString();
                        fieldModel.dokumentasi = dr["dokumentasi"].ToString();
                        fieldModel.manajemen = dr["manajemen"].ToString();
                        fieldModel.total_internal = dr["total_internal"].ToString();
                        fieldModel.total_standar = dr["total_standar"].ToString();
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
        public JsonResult Insert(AuditKriteriaSmkpModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = AuditKriteriaSmkpModel.GetNewId().ToString();
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["kebijakan"] = fieldModel.kebijakan != null ? fieldModel.kebijakan.Replace(",",".") : "";
                    data["perencanaan"] = fieldModel.perencanaan != null ? fieldModel.perencanaan.Replace(",",".") : "";
                    data["organisasi"] = fieldModel.organisasi != null ? fieldModel.organisasi.Replace(",",".") : "";
                    data["implementasi"] = fieldModel.implementasi != null ? fieldModel.implementasi.Replace(",",".") : "";
                    data["evaluasi"] = fieldModel.evaluasi != null ? fieldModel.evaluasi.Replace(",",".") : "";
                    data["dokumentasi"] = fieldModel.dokumentasi != null ? fieldModel.dokumentasi.Replace(",",".") : "";
                    data["manajemen"] = fieldModel.manajemen != null ? fieldModel.manajemen.Replace(",",".") : "";
                    data["total_internal"] = fieldModel.total_internal != null ? fieldModel.total_internal.Replace(",",".") : "";
                    data["total_standar"] = fieldModel.total_standar != null ? fieldModel.total_standar.Replace(",",".") : "";
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
                    result = AuditKriteriaSmkpModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(AuditKriteriaSmkpModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(AuditKriteriaSmkpModel.GetData(PkValue));
                    data_old["kebijakan"] = data_old["kebijakan"].ToString() != ""  ? data_old["kebijakan"].ToString().Replace(",", ".") : "";
                    data_old["perencanaan"] = data_old["perencanaan"].ToString() != ""  ? data_old["perencanaan"].ToString().Replace(",", ".") : "";
                    data_old["organisasi"] = data_old["organisasi"].ToString() != ""  ? data_old["organisasi"].ToString().Replace(",", ".") : "";
                    data_old["implementasi"] = data_old["implementasi"].ToString() != ""  ? data_old["implementasi"].ToString().Replace(",", ".") : "";
                    data_old["evaluasi"] = data_old["evaluasi"].ToString() != ""  ? data_old["evaluasi"].ToString().Replace(",", ".") : "";
                    data_old["dokumentasi"] = data_old["dokumentasi"].ToString() != ""  ? data_old["dokumentasi"].ToString().Replace(",", ".") : "";
                    data_old["manajemen"] = data_old["manajemen"].ToString() != ""  ? data_old["manajemen"].ToString().Replace(",", ".") : "";
                    data_old["total_internal"] = data_old["total_internal"].ToString() != ""  ? data_old["total_internal"].ToString().Replace(",", ".") : "";
                    data_old["total_standar"] = data_old["total_standar"].ToString() != ""  ? data_old["total_standar"].ToString().Replace(",", ".") : "";
                    data_old["presentasi_pencapaian"] = data_old["presentasi_pencapaian"].ToString() != ""  ? data_old["presentasi_pencapaian"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaSmkpModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["kebijakan"] = fieldModel.kebijakan != null ? fieldModel.kebijakan.ToString().Replace(",", ".") : "";
                    data["perencanaan"] = fieldModel.perencanaan != null ? fieldModel.perencanaan.ToString().Replace(",", ".") : "";
                    data["organisasi"] = fieldModel.organisasi != null ? fieldModel.organisasi.ToString().Replace(",", ".") : "";
                    data["implementasi"] = fieldModel.implementasi != null ? fieldModel.implementasi.ToString().Replace(",", ".") : "";
                    data["evaluasi"] = fieldModel.evaluasi != null ? fieldModel.evaluasi.ToString().Replace(",", ".") : "";
                    data["dokumentasi"] = fieldModel.dokumentasi != null ? fieldModel.dokumentasi.ToString().Replace(",", ".") : "";
                    data["manajemen"] = fieldModel.manajemen != null ? fieldModel.manajemen.ToString().Replace(",", ".") : "";
                    data["total_internal"] = fieldModel.total_internal != null ? fieldModel.total_internal.ToString().Replace(",", ".") : "";
                    data["total_standar"] = fieldModel.total_standar != null ? fieldModel.total_standar.ToString().Replace(",", ".") : "";
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
                        result = AuditKriteriaSmkpModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaSmkpModel.GetData(PkValue));
						if (data != null)
						{
							result = AuditKriteriaSmkpModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = AuditKriteriaSmkpModel.LookupData(param);
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
            string str_kebijakan = data["kebijakan"].ToString();
            string str_perencanaan = data["perencanaan"].ToString();
            string str_organisasi = data["organisasi"].ToString();
            string str_implementasi = data["implementasi"].ToString();
            string str_evaluasi = data["evaluasi"].ToString();
            string str_dokumentasi = data["dokumentasi"].ToString();
            string str_manajemen = data["manajemen"].ToString();


            double kebijakan = (str_kebijakan == "" ? 0 : double.Parse(str_kebijakan));
            double perencanaan = (str_perencanaan == "" ? 0 : double.Parse(str_perencanaan));
            double organisasi = (str_organisasi == "" ? 0 : double.Parse(str_organisasi));
            double implementasi = (str_implementasi == "" ? 0 : double.Parse(str_implementasi));
            double evaluasi = (str_evaluasi == "" ? 0 : double.Parse(str_evaluasi));
            double dokumentasi = (str_dokumentasi == "" ? 0 : double.Parse(str_dokumentasi));
            double manajemen = (str_manajemen == "" ? 0 : double.Parse(str_manajemen));


            double kebijakan_max = 200;
            double perencanaan_max = 200;
            double organisasi_max = 150;
            double implementasi_max = 200;
            double evaluasi_max = 150;
            double dokumentasi_max = 50;
            double manajemen_max = 50;


            double kebijakan_total = 0;
            double perencanaan_total = 0;
            double organisasi_total = 0;
            double implementasi_total = 0;
            double evaluasi_total = 0;
            double dokumentasi_total = 0;
            double manajemen_total = 0;


            kebijakan_total = kebijakan;// * kebijakan_max;
            perencanaan_total = perencanaan;// * perencanaan_max;
            organisasi_total = organisasi;// * organisasi_max;
            implementasi_total = implementasi;// * implementasi_max;
            evaluasi_total = evaluasi;// * evaluasi_max;
            dokumentasi_total = dokumentasi;// * dokumentasi_max;
            manajemen_total = manajemen;// * manajemen_max;



            double total_internal =
                kebijakan_total +
                perencanaan_total +
                organisasi_total +
                implementasi_total +
                evaluasi_total +
                dokumentasi_total +
                manajemen_total;
            data["total_internal"] = total_internal;



            double total_standar =
                kebijakan_max +
                perencanaan_max +
                organisasi_max +
                implementasi_max +
                evaluasi_max +
                dokumentasi_max +
                manajemen_max;
            data["total_standar"] = total_standar;


            double presentasi_pencapaian = (total_internal / total_standar) * 100;
            data["presentasi_pencapaian"] = presentasi_pencapaian;


            int bendera_id = 0;
            if (90 <= presentasi_pencapaian)
            {
                bendera_id = 1;
            }
            else if (80 <= presentasi_pencapaian & presentasi_pencapaian < 90)
            {
                bendera_id = 2;
            }
            else if (70 <= presentasi_pencapaian & presentasi_pencapaian < 80)
            {
                bendera_id = 3;
            }
            else if (presentasi_pencapaian < 70)
            {
                bendera_id = 4;
            }
            data["nilai_bendera_id"] = bendera_id;
        }
    }
}