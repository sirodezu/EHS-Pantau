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
    
    public class EmergencySaranaController : Controller
    {
        private string _rule_view = "EmergencyView";
        private string _rule_add = "EmergencyAdd";
        private string _rule_edit = "EmergencyEdit";
        private string _rule_delete = "EmergencyDelete";
        private string _rule_edit_all = "EmergencyEditAll";
        private string _rule_delete_all = "EmergencyDeleteAll";
        private string _path_controler = "/EmergencySarana/";
        private string _path_view = "/Views/Emergency/EmergencySarana/";
        private readonly string _table_name = "ta_emg_sarana";
        private readonly string _table_title = "Emergency Sarana";

		private static IHostingEnvironment _hostingEnvironment;
        public EmergencySaranaController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(EmergencySaranaModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = EmergencySaranaModel._pkKey;
                    ViewData["displayItem"] = EmergencySaranaModel.GetDisplayItem();
                    ViewData["column_att"] = EmergencySaranaModel.GetGridColumn();
                    ViewData["param"] = EmergencySaranaModel.GetListParam();
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
        public JsonResult GetListData(EmergencySaranaModel.ActionRequest param)
        {
            GridData data = EmergencySaranaModel.GetListData(param);
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
                    EmergencySaranaModel.ViewModel fieldModel = new EmergencySaranaModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = EmergencySaranaModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.emg_id = dr["emg_id"].ToString();
                                fieldModel.jenis_sarana_ktd_id = dr["jenis_sarana_ktd_id"].ToString();
                                fieldModel.jenis_sarana_ktd_id_text = dr["jenis_sarana_ktd_id_text"].ToString();
                                fieldModel.tipe_hydrant_id = dr["tipe_hydrant_id"].ToString();
                                fieldModel.tipe_hydrant_id_text = dr["tipe_hydrant_id_text"].ToString();
                                fieldModel.jumlah_pompa = dr["jumlah_pompa"].ToString();
                                fieldModel.sistem_kerja_pompa_permanen_hydrant_id = dr["sistem_kerja_pompa_permanen_hydrant_id"].ToString();
                                fieldModel.sistem_kerja_pompa_permanen_hydrant_id_text = dr["sistem_kerja_pompa_permanen_hydrant_id_text"].ToString();
                                fieldModel.ukuran_hose_nozzle_hydrant_id = dr["ukuran_hose_nozzle_hydrant_id"].ToString();
                                fieldModel.ukuran_hose_nozzle_hydrant_id_text = dr["ukuran_hose_nozzle_hydrant_id_text"].ToString();
                                fieldModel.jenis_apar_id = dr["jenis_apar_id"].ToString();
                                fieldModel.jenis_apar_id_text = dr["jenis_apar_id_text"].ToString();
                                fieldModel.ukuran_apar_id = dr["ukuran_apar_id"].ToString();
                                fieldModel.ukuran_apar_id_text = dr["ukuran_apar_id_text"].ToString();
                                fieldModel.ukuran_apa_id = dr["ukuran_apa_id"].ToString();
                                fieldModel.ukuran_apa_id_text = dr["ukuran_apa_id_text"].ToString();
                                fieldModel.jumlah_sarana = dr["jumlah_sarana"].ToString();
                                fieldModel.kondisi_sarana_id = dr["kondisi_sarana_id"].ToString();
                                fieldModel.kondisi_sarana_id_text = dr["kondisi_sarana_id_text"].ToString();
                                fieldModel.lampiran_file_name = dr["lampiran_file_name"].ToString();
                                fieldModel.lampiran_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "lampiran_file_path", dr["lampiran_file_path"].ToString());
                                fieldModel.catatan_perbaikan_id = dr["catatan_perbaikan_id"].ToString();
                                fieldModel.catatan_perbaikan_id_text = dr["catatan_perbaikan_id_text"].ToString();
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
        public IActionResult Add(EmergencySaranaModel.ViewModel fieldModel)
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
                    //EmergencySaranaModel.ViewModel fieldModel = new EmergencySaranaModel.ViewModel();
                    fieldModel.id = EmergencySaranaModel.GetNewId().ToString();
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
                    DataTable data = EmergencySaranaModel.GetViewData(PkValue);
                    EmergencySaranaModel.ViewModel fieldModel = new EmergencySaranaModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = EmergencySaranaModel.GetNewId().ToString();
                        fieldModel.emg_id = dr["emg_id"].ToString();
                        fieldModel.jenis_sarana_ktd_id = dr["jenis_sarana_ktd_id"].ToString();
                        fieldModel.tipe_hydrant_id = dr["tipe_hydrant_id"].ToString();
                        fieldModel.jumlah_pompa = dr["jumlah_pompa"].ToString();
                        fieldModel.sistem_kerja_pompa_permanen_hydrant_id = dr["sistem_kerja_pompa_permanen_hydrant_id"].ToString();
                        fieldModel.ukuran_hose_nozzle_hydrant_id = dr["ukuran_hose_nozzle_hydrant_id"].ToString();
                        fieldModel.jenis_apar_id = dr["jenis_apar_id"].ToString();
                        fieldModel.ukuran_apar_id = dr["ukuran_apar_id"].ToString();
                        fieldModel.ukuran_apa_id = dr["ukuran_apa_id"].ToString();
                        fieldModel.jumlah_sarana = dr["jumlah_sarana"].ToString();
                        fieldModel.kondisi_sarana_id = dr["kondisi_sarana_id"].ToString();
                        fieldModel.lampiran_file_name = dr["lampiran_file_name"].ToString();
                        fieldModel.lampiran_file_path = dr["lampiran_file_path"].ToString();
                        fieldModel.catatan_perbaikan_id = dr["catatan_perbaikan_id"].ToString();
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
                    DataTable data = EmergencySaranaModel.GetViewData(PkValue);
                    EmergencySaranaModel.ViewModel fieldModel = new EmergencySaranaModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.emg_id = dr["emg_id"].ToString();
                        fieldModel.jenis_sarana_ktd_id = dr["jenis_sarana_ktd_id"].ToString();
                        fieldModel.jenis_sarana_ktd_id_text = dr["jenis_sarana_ktd_id_text"].ToString();
                        fieldModel.tipe_hydrant_id = dr["tipe_hydrant_id"].ToString();
                        fieldModel.tipe_hydrant_id_text = dr["tipe_hydrant_id_text"].ToString();
                        fieldModel.jumlah_pompa = dr["jumlah_pompa"].ToString();
                        fieldModel.sistem_kerja_pompa_permanen_hydrant_id = dr["sistem_kerja_pompa_permanen_hydrant_id"].ToString();
                        fieldModel.sistem_kerja_pompa_permanen_hydrant_id_text = dr["sistem_kerja_pompa_permanen_hydrant_id_text"].ToString();
                        fieldModel.ukuran_hose_nozzle_hydrant_id = dr["ukuran_hose_nozzle_hydrant_id"].ToString();
                        fieldModel.ukuran_hose_nozzle_hydrant_id_text = dr["ukuran_hose_nozzle_hydrant_id_text"].ToString();
                        fieldModel.jenis_apar_id = dr["jenis_apar_id"].ToString();
                        fieldModel.jenis_apar_id_text = dr["jenis_apar_id_text"].ToString();
                        fieldModel.ukuran_apar_id = dr["ukuran_apar_id"].ToString();
                        fieldModel.ukuran_apar_id_text = dr["ukuran_apar_id_text"].ToString();
                        fieldModel.ukuran_apa_id = dr["ukuran_apa_id"].ToString();
                        fieldModel.ukuran_apa_id_text = dr["ukuran_apa_id_text"].ToString();
                        fieldModel.jumlah_sarana = dr["jumlah_sarana"].ToString();
                        fieldModel.kondisi_sarana_id = dr["kondisi_sarana_id"].ToString();
                        fieldModel.kondisi_sarana_id_text = dr["kondisi_sarana_id_text"].ToString();
                        fieldModel.lampiran_file_name = dr["lampiran_file_name"].ToString();
                        string[] init_lampiran_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "lampiran_file_path", PkValue, dr["lampiran_file_path"].ToString());
                        fieldModel.lampiran_file_path = init_lampiran_file_path[0];
                        fieldModel.lampiran_file_path_init = init_lampiran_file_path[1];
                        fieldModel.catatan_perbaikan_id = dr["catatan_perbaikan_id"].ToString();
                        fieldModel.catatan_perbaikan_id_text = dr["catatan_perbaikan_id_text"].ToString();
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
        public JsonResult Insert(EmergencySaranaModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = EmergencySaranaModel.GetNewId().ToString();
                    data["emg_id"] = fieldModel.emg_id;
                    data["jenis_sarana_ktd_id"] = fieldModel.jenis_sarana_ktd_id;
                    data["tipe_hydrant_id"] = fieldModel.tipe_hydrant_id;
                    data["jumlah_pompa"] = fieldModel.jumlah_pompa;
                    data["sistem_kerja_pompa_permanen_hydrant_id"] = fieldModel.sistem_kerja_pompa_permanen_hydrant_id;
                    data["ukuran_hose_nozzle_hydrant_id"] = fieldModel.ukuran_hose_nozzle_hydrant_id;
                    data["jenis_apar_id"] = fieldModel.jenis_apar_id;
                    data["ukuran_apar_id"] = fieldModel.ukuran_apar_id;
                    data["ukuran_apa_id"] = fieldModel.ukuran_apa_id;
                    data["jumlah_sarana"] = fieldModel.jumlah_sarana;
                    data["kondisi_sarana_id"] = fieldModel.kondisi_sarana_id;
                    data["lampiran_file_name"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_name);
                    data["lampiran_file_path"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_path);
                    data["catatan_perbaikan_id"] = fieldModel.catatan_perbaikan_id;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = EmergencySaranaModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(EmergencySaranaModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(EmergencySaranaModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(EmergencySaranaModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["emg_id"] = fieldModel.emg_id;
                    data["jenis_sarana_ktd_id"] = fieldModel.jenis_sarana_ktd_id;
                    data["tipe_hydrant_id"] = fieldModel.tipe_hydrant_id;
                    data["jumlah_pompa"] = fieldModel.jumlah_pompa;
                    data["sistem_kerja_pompa_permanen_hydrant_id"] = fieldModel.sistem_kerja_pompa_permanen_hydrant_id;
                    data["ukuran_hose_nozzle_hydrant_id"] = fieldModel.ukuran_hose_nozzle_hydrant_id;
                    data["jenis_apar_id"] = fieldModel.jenis_apar_id;
                    data["ukuran_apar_id"] = fieldModel.ukuran_apar_id;
                    data["ukuran_apa_id"] = fieldModel.ukuran_apa_id;
                    data["jumlah_sarana"] = fieldModel.jumlah_sarana;
                    data["kondisi_sarana_id"] = fieldModel.kondisi_sarana_id;
                    data["lampiran_file_name"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_name);
                    data["lampiran_file_path"] = AntiXss.HtmlEncode(fieldModel.lampiran_file_path);
                    data["catatan_perbaikan_id"] = fieldModel.catatan_perbaikan_id;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {

                        result = EmergencySaranaModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(EmergencySaranaModel.GetData(PkValue));
						if (data != null)
						{
							result = EmergencySaranaModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = EmergencySaranaModel.LookupData(param);
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

    }
}