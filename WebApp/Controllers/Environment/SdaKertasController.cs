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
using WebApp.Repository;
using ClosedXML.Excel;

namespace WebApp.Controllers
{
    
    public class SdaKertasController : Controller
    {
        private string _rule_view = "EnvironmentView";
        private string _rule_add = "EnvironmentAdd";
        private string _rule_edit = "EnvironmentEdit";
        private string _rule_delete = "EnvironmentDelete";
        private string _rule_edit_all = "EnvironmentEditAll";
        private string _rule_delete_all = "EnvironmentDeleteAll";
        private string _path_controler = "/SdaKertas/";
        private string _path_view = "/Views/Environment/SdaKertas/";
        private readonly string _table_name = "ta_sda_kertas";
        private readonly string _table_title = "Sumber Daya Alam - Kertas";

		private static IHostingEnvironment _hostingEnvironment;
        public SdaKertasController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(SdaKertasModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = SdaKertasModel._pkKey;
                    ViewData["displayItem"] = SdaKertasModel.GetDisplayItem();
                    ViewData["column_att"] = SdaKertasModel.GetGridColumn();
                    ViewData["param"] = SdaKertasModel.GetListParam();
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
        public JsonResult GetListData(SdaKertasModel.ActionRequest param)
        {
            GridData data = SdaKertasModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["usaha_pengurangan_kertas_desc_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "usaha_pengurangan_kertas_desc_file_path", data.data.Rows[i]["usaha_pengurangan_kertas_desc_file_path"].ToString());
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
                    SdaKertasModel.ViewModel fieldModel = new SdaKertasModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = SdaKertasModel.GetViewData(PkValue);
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
                                fieldModel.bulan = dr["bulan"].ToString();
                                fieldModel.bulan_text = dr["bulan_text"].ToString();
                                fieldModel.tahun = dr["tahun"].ToString();
                                fieldModel.tahun_text = dr["tahun_text"].ToString();
                                fieldModel.konsumsi_kertas = String.Format("{0:N2}", dr["konsumsi_kertas"]);
                                fieldModel.usaha_pengurangan_kertas_id = dr["usaha_pengurangan_kertas_id"].ToString();
                                fieldModel.usaha_pengurangan_kertas_id_text = dr["usaha_pengurangan_kertas_id_text"].ToString();
                                fieldModel.usaha_pengurangan_kertas_desc = dr["usaha_pengurangan_kertas_desc"].ToString();
                                fieldModel.usaha_pengurangan_kertas_desc_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "usaha_pengurangan_kertas_desc_file_path", dr["usaha_pengurangan_kertas_desc_file_path"].ToString());
                                fieldModel.usaha_pengurangan_kertas_jumlah = String.Format("{0:N2}", dr["usaha_pengurangan_kertas_jumlah"]);
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
        public IActionResult Add(SdaKertasModel.ViewModel fieldModel)
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
                    //SdaKertasModel.ViewModel fieldModel = new SdaKertasModel.ViewModel();
                    fieldModel.id = SdaKertasModel.GetNewId().ToString();
                    fieldModel.usaha_pengurangan_kertas_desc_file_path_init = "[]";
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
                    DataTable data = SdaKertasModel.GetViewData(PkValue);
                    SdaKertasModel.ViewModel fieldModel = new SdaKertasModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = SdaKertasModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.bulan = dr["bulan"].ToString();
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.konsumsi_kertas = dr["konsumsi_kertas"].ToString();
                        fieldModel.usaha_pengurangan_kertas_id = dr["usaha_pengurangan_kertas_id"].ToString();
                        fieldModel.usaha_pengurangan_kertas_desc = dr["usaha_pengurangan_kertas_desc"].ToString();
                        fieldModel.usaha_pengurangan_kertas_desc_file_path = dr["usaha_pengurangan_kertas_desc_file_path"].ToString();
                        fieldModel.usaha_pengurangan_kertas_jumlah = dr["usaha_pengurangan_kertas_jumlah"].ToString();
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
                    DataTable data = SdaKertasModel.GetViewData(PkValue);
                    SdaKertasModel.ViewModel fieldModel = new SdaKertasModel.ViewModel();
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
                        fieldModel.bulan = dr["bulan"].ToString();
                        fieldModel.bulan_text = dr["bulan_text"].ToString();
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.tahun_text = dr["tahun_text"].ToString();
                        fieldModel.konsumsi_kertas = dr["konsumsi_kertas"].ToString();
                        fieldModel.usaha_pengurangan_kertas_id = dr["usaha_pengurangan_kertas_id"].ToString();
                        fieldModel.usaha_pengurangan_kertas_id_text = dr["usaha_pengurangan_kertas_id_text"].ToString();
                        fieldModel.usaha_pengurangan_kertas_desc = dr["usaha_pengurangan_kertas_desc"].ToString();
                        string[] init_usaha_pengurangan_kertas_desc_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "usaha_pengurangan_kertas_desc_file_path", PkValue, dr["usaha_pengurangan_kertas_desc_file_path"].ToString());
                        fieldModel.usaha_pengurangan_kertas_desc_file_path = init_usaha_pengurangan_kertas_desc_file_path[0];
                        fieldModel.usaha_pengurangan_kertas_desc_file_path_init = init_usaha_pengurangan_kertas_desc_file_path[1];
                        fieldModel.usaha_pengurangan_kertas_jumlah = dr["usaha_pengurangan_kertas_jumlah"].ToString();
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
        public JsonResult Insert(SdaKertasModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = SdaKertasModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["konsumsi_kertas"] = fieldModel.konsumsi_kertas != null ? fieldModel.konsumsi_kertas.Replace(",",".") : "";
                    data["usaha_pengurangan_kertas_id"] = fieldModel.usaha_pengurangan_kertas_id;
                    data["usaha_pengurangan_kertas_desc"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_kertas_desc);
                    data["usaha_pengurangan_kertas_desc_file_path"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_kertas_desc_file_path);
                    data["usaha_pengurangan_kertas_jumlah"] = fieldModel.usaha_pengurangan_kertas_jumlah != null ? fieldModel.usaha_pengurangan_kertas_jumlah.Replace(",",".") : "";
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = SdaKertasModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(SdaKertasModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(SdaKertasModel.GetData(PkValue));
                    data_old["konsumsi_kertas"] = data_old["konsumsi_kertas"].ToString() != ""  ? data_old["konsumsi_kertas"].ToString().Replace(",", ".") : "";
                    data_old["usaha_pengurangan_kertas_jumlah"] = data_old["usaha_pengurangan_kertas_jumlah"].ToString() != ""  ? data_old["usaha_pengurangan_kertas_jumlah"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(SdaKertasModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["konsumsi_kertas"] = fieldModel.konsumsi_kertas != null ? fieldModel.konsumsi_kertas.ToString().Replace(",", ".") : "";
                    data["usaha_pengurangan_kertas_id"] = fieldModel.usaha_pengurangan_kertas_id;
                    data["usaha_pengurangan_kertas_desc"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_kertas_desc);
                    data["usaha_pengurangan_kertas_desc_file_path"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_kertas_desc_file_path);
                    data["usaha_pengurangan_kertas_jumlah"] = fieldModel.usaha_pengurangan_kertas_jumlah != null ? fieldModel.usaha_pengurangan_kertas_jumlah.ToString().Replace(",", ".") : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = SdaKertasModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(SdaKertasModel.GetData(PkValue));
						if (data != null)
						{
							result = SdaKertasModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = SdaKertasModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_usaha_pengurangan_kertas_desc_file_path(IEnumerable<IFormFile> usaha_pengurangan_kertas_desc_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "usaha_pengurangan_kertas_desc_file_path");
            if (usaha_pengurangan_kertas_desc_file_path_file != null)
            {
                foreach (var file in usaha_pengurangan_kertas_desc_file_path_file)
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

        public ActionResult remove_usaha_pengurangan_kertas_desc_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "usaha_pengurangan_kertas_desc_file_path");
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
        [HttpPost]
        public async Task<IActionResult> ImportSdaKertas()
        {
            IFormFile file = Request.Form.Files[0];
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Import");
            var dir = Directory.GetCurrentDirectory();
            var doc = "\\Import\\";
            var path = dir + doc;
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            var fullPath = path + fileName;

            using (var fileStream = new FileStream(Path.Combine(path + fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                file.CopyTo(fileStream);
            }
            List<ImportSdaKertasModel> data = await GetData(fullPath);

            return Created(string.Empty, null);
        }

        private async Task<List<ImportSdaKertasModel>> GetData(string fileName)
        {
            List<ImportSdaKertasModel> data = new List<ImportSdaKertasModel>();
            try
            {
                using (XLWorkbook workBook = new XLWorkbook(fileName))
                {
                    IXLWorksheet row = workBook.Worksheet("Sheet1");
                    IXLRange range = row.RangeUsed();
                    //int a = range.RowCount();
                    for (int i = 1; i < range.RowCount() + 1; i++)
                    {
                        if(i == 1)
                        {
                            if (row.Cell(i, 1).Value.ToString().Trim().ToLower() != "area")
                            {
                                throw new Exception("Template Not Match at Cell A1");
                            }
                            if (row.Cell(i, 2).Value.ToString().Trim().ToLower() != "business area")
                            {
                                throw new Exception("Template Not Match at Cell B1");
                            }
                            if (row.Cell(i, 3).Value.ToString().Trim().ToLower() != "personal area")
                            {
                                throw new Exception("Template Not Match at Cell C1");
                            }
                            if (row.Cell(i, 4).Value.ToString().Trim().ToLower() != "personal sub area")
                            {
                                throw new Exception("Template Not Match at Cell D1");
                            }
                            if (row.Cell(i, 5).Value.ToString().Trim().ToLower() != "bulan")
                            {
                                throw new Exception("Template Not Match at Cell E1");
                            }
                            if (row.Cell(i, 6).Value.ToString().Trim().ToLower() != "tahun")
                            {
                                throw new Exception("Template Not Match at Cell F1");
                            }
                            if (row.Cell(i, 7).Value.ToString().Trim().ToLower() != "konsumsi kertas baru (kg)")
                            {
                                throw new Exception("Template Not Match at Cell G1");
                            }
                            if (row.Cell(i, 8).Value.ToString().Trim().ToLower() != "usaha pengurangan")
                            {
                                throw new Exception("Template Not Match at Cell H1");
                            }
                            if (row.Cell(i, 9).Value.ToString().Trim().ToLower() != "deskripsi usaha")
                            {
                                throw new Exception("Template Not Match at Cell I1");
                            }
                            if (row.Cell(i, 10).Value.ToString().Trim().ToLower() != "dokumen deskripsi usaha")
                            {
                                throw new Exception("Template Not Match at Cell J1");
                            }
                            if (row.Cell(i, 11).Value.ToString().Trim().ToLower() != "jumlah usaha pengurangan (kg)")
                            {
                                throw new Exception("Template Not Match at Cell K1");
                            }
                        }
                        else
                        {
                            ImportSdaKertasModel sda = new ImportSdaKertasModel();
                            sda.ehs_area_id = row.Cell(i, 1).Value.ToString();
                            sda.ba_id = row.Cell(i, 2).Value.ToString();
                            sda.pa_id = row.Cell(i, 3).Value.ToString();
                            sda.psa_id = row.Cell(i, 4).Value.ToString();
                            sda.bulan = row.Cell(i, 5).Value.ToString();
                            sda.tahun = int.Parse(row.Cell(i, 6).Value.ToString());
                            sda.konsumsi_kertas = double.Parse(row.Cell(i, 7).Value.ToString());
                            sda.usaha_pengurangan_kertas_id = row.Cell(i, 8).Value.ToString();
                            sda.usaha_pengurangan_kertas_desc = row.Cell(i, 9).Value.ToString();
                            sda.usaha_pengurangan_kertas_desc_file_path = row.Cell(i, 10).Value.ToString();
                            sda.usaha_pengurangan_kertas_jumlah = double.Parse(row.Cell(i, 11).Value.ToString());
                            ImportRepository sdaAir = new ImportRepository();
                            sdaAir.ImportSdaKertas(sda);
                        }
                    }
                    return data;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}