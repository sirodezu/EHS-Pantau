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
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApp.Controllers
{
    
    public class SdaAirController : Controller
    {
        private string ConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["MainConnection"];
        private string _rule_view = "EnvironmentView";
        private string _rule_add = "EnvironmentAdd";
        private string _rule_edit = "EnvironmentEdit";
        private string _rule_delete = "EnvironmentDelete";
        private string _rule_edit_all = "EnvironmentEditAll";
        private string _rule_delete_all = "EnvironmentDeleteAll";
        private string _path_controler = "/SdaAir/";
        private string _path_view = "/Views/Environment/SdaAir/";
        private readonly string _table_name = "ta_sda_air";
        private readonly string _table_title = "Sumber Daya Alam - Air";

		private static IHostingEnvironment _hostingEnvironment;
        public SdaAirController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(SdaAirModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = SdaAirModel._pkKey;
                    ViewData["displayItem"] = SdaAirModel.GetDisplayItem();
                    ViewData["column_att"] = SdaAirModel.GetGridColumn();
                    ViewData["param"] = SdaAirModel.GetListParam();
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
        public JsonResult GetListData(SdaAirModel.ActionRequest param)
        {
            GridData data = SdaAirModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["tagihan_air"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "tagihan_air", data.data.Rows[i]["tagihan_air"].ToString());
                data.data.Rows[i]["usaha_pengurangan_air_desc_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "usaha_pengurangan_air_desc_file_path", data.data.Rows[i]["usaha_pengurangan_air_desc_file_path"].ToString());
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
                    SdaAirModel.ViewModel fieldModel = new SdaAirModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = SdaAirModel.GetViewData(PkValue);
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
                                fieldModel.sumber_air_id = dr["sumber_air_id"].ToString();
                                fieldModel.sumber_air_id_text = dr["sumber_air_id_text"].ToString();
                                fieldModel.no_rek_air = dr["no_rek_air"].ToString();
                                fieldModel.konsumsi_air = String.Format("{0:N2}", dr["konsumsi_air"]);
                                fieldModel.tagihan_air = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "tagihan_air", dr["tagihan_air"].ToString());
                                fieldModel.usaha_pengurangan_air_id = dr["usaha_pengurangan_air_id"].ToString();
                                fieldModel.usaha_pengurangan_air_id_text = dr["usaha_pengurangan_air_id_text"].ToString();
                                fieldModel.usaha_pengurangan_air_desc = dr["usaha_pengurangan_air_desc"].ToString();
                                fieldModel.usaha_pengurangan_air_desc_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "usaha_pengurangan_air_desc_file_path", dr["usaha_pengurangan_air_desc_file_path"].ToString());
                                fieldModel.usaha_pengurangan_air_jumlah = String.Format("{0:N2}", dr["usaha_pengurangan_air_jumlah"]);
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
        public IActionResult Add(SdaAirModel.ViewModel fieldModel)
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
                    //SdaAirModel.ViewModel fieldModel = new SdaAirModel.ViewModel();
                    fieldModel.id = SdaAirModel.GetNewId().ToString();
                    fieldModel.tagihan_air_init = "[]";
                    fieldModel.usaha_pengurangan_air_desc_file_path_init = "[]";
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
                    DataTable data = SdaAirModel.GetViewData(PkValue);
                    SdaAirModel.ViewModel fieldModel = new SdaAirModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = SdaAirModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.bulan = dr["bulan"].ToString();
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.sumber_air_id = dr["sumber_air_id"].ToString();
                        fieldModel.no_rek_air = dr["no_rek_air"].ToString();
                        fieldModel.konsumsi_air = dr["konsumsi_air"].ToString();
                        fieldModel.tagihan_air = dr["tagihan_air"].ToString();
                        fieldModel.usaha_pengurangan_air_id = dr["usaha_pengurangan_air_id"].ToString();
                        fieldModel.usaha_pengurangan_air_desc = dr["usaha_pengurangan_air_desc"].ToString();
                        fieldModel.usaha_pengurangan_air_desc_file_path = dr["usaha_pengurangan_air_desc_file_path"].ToString();
                        fieldModel.usaha_pengurangan_air_jumlah = dr["usaha_pengurangan_air_jumlah"].ToString();
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
                    DataTable data = SdaAirModel.GetViewData(PkValue);
                    SdaAirModel.ViewModel fieldModel = new SdaAirModel.ViewModel();
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
                        fieldModel.sumber_air_id = dr["sumber_air_id"].ToString();
                        fieldModel.sumber_air_id_text = dr["sumber_air_id_text"].ToString();
                        fieldModel.no_rek_air = dr["no_rek_air"].ToString();
                        fieldModel.konsumsi_air = dr["konsumsi_air"].ToString();
                        string[] init_tagihan_air = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "tagihan_air", PkValue, dr["tagihan_air"].ToString());
                        fieldModel.tagihan_air = init_tagihan_air[0];
                        fieldModel.tagihan_air_init = init_tagihan_air[1];
                        fieldModel.usaha_pengurangan_air_id = dr["usaha_pengurangan_air_id"].ToString();
                        fieldModel.usaha_pengurangan_air_id_text = dr["usaha_pengurangan_air_id_text"].ToString();
                        fieldModel.usaha_pengurangan_air_desc = dr["usaha_pengurangan_air_desc"].ToString();
                        string[] init_usaha_pengurangan_air_desc_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "usaha_pengurangan_air_desc_file_path", PkValue, dr["usaha_pengurangan_air_desc_file_path"].ToString());
                        fieldModel.usaha_pengurangan_air_desc_file_path = init_usaha_pengurangan_air_desc_file_path[0];
                        fieldModel.usaha_pengurangan_air_desc_file_path_init = init_usaha_pengurangan_air_desc_file_path[1];
                        fieldModel.usaha_pengurangan_air_jumlah = dr["usaha_pengurangan_air_jumlah"].ToString();
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
        public JsonResult Insert(SdaAirModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = SdaAirModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["sumber_air_id"] = fieldModel.sumber_air_id;
                    data["no_rek_air"] = AntiXss.HtmlEncode(fieldModel.no_rek_air);
                    data["konsumsi_air"] = fieldModel.konsumsi_air != null ? fieldModel.konsumsi_air.Replace(",",".") : "";
                    data["tagihan_air"] = AntiXss.HtmlEncode(fieldModel.tagihan_air);
                    data["usaha_pengurangan_air_id"] = fieldModel.usaha_pengurangan_air_id;
                    data["usaha_pengurangan_air_desc"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_air_desc);
                    data["usaha_pengurangan_air_desc_file_path"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_air_desc_file_path);
                    data["usaha_pengurangan_air_jumlah"] = fieldModel.usaha_pengurangan_air_jumlah != null ? fieldModel.usaha_pengurangan_air_jumlah.Replace(",",".") : "";
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = SdaAirModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(SdaAirModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(SdaAirModel.GetData(PkValue));
                    data_old["konsumsi_air"] = data_old["konsumsi_air"].ToString() != ""  ? data_old["konsumsi_air"].ToString().Replace(",", ".") : "";
                    data_old["usaha_pengurangan_air_jumlah"] = data_old["usaha_pengurangan_air_jumlah"].ToString() != ""  ? data_old["usaha_pengurangan_air_jumlah"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(SdaAirModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["sumber_air_id"] = fieldModel.sumber_air_id;
                    data["no_rek_air"] = AntiXss.HtmlEncode(fieldModel.no_rek_air);
                    data["konsumsi_air"] = fieldModel.konsumsi_air != null ? fieldModel.konsumsi_air.ToString().Replace(",", ".") : "";
                    data["tagihan_air"] = AntiXss.HtmlEncode(fieldModel.tagihan_air);
                    data["usaha_pengurangan_air_id"] = fieldModel.usaha_pengurangan_air_id;
                    data["usaha_pengurangan_air_desc"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_air_desc);
                    data["usaha_pengurangan_air_desc_file_path"] = AntiXss.HtmlEncode(fieldModel.usaha_pengurangan_air_desc_file_path);
                    data["usaha_pengurangan_air_jumlah"] = fieldModel.usaha_pengurangan_air_jumlah != null ? fieldModel.usaha_pengurangan_air_jumlah.ToString().Replace(",", ".") : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = SdaAirModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(SdaAirModel.GetData(PkValue));
						if (data != null)
						{
							result = SdaAirModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = SdaAirModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_tagihan_air(IEnumerable<IFormFile> tagihan_air_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "tagihan_air");
            if (tagihan_air_file != null)
            {
                foreach (var file in tagihan_air_file)
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

        public ActionResult remove_tagihan_air(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "tagihan_air");
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

        public async Task<IActionResult> save_usaha_pengurangan_air_desc_file_path(IEnumerable<IFormFile> usaha_pengurangan_air_desc_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "usaha_pengurangan_air_desc_file_path");
            if (usaha_pengurangan_air_desc_file_path_file != null)
            {
                foreach (var file in usaha_pengurangan_air_desc_file_path_file)
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

        public ActionResult remove_usaha_pengurangan_air_desc_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "usaha_pengurangan_air_desc_file_path");
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
        public async Task<IActionResult> ImportSdaAir()
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
            List<ImportSdaAirModel> data = await GetData(fullPath);

            return Created(string.Empty, null);
        }

        private async Task<List<ImportSdaAirModel>> GetData(string fileName)
        {
            List<ImportSdaAirModel> data = new List<ImportSdaAirModel>();
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
                            if (row.Cell(i, 7).Value.ToString().Trim().ToLower() != "sumber air")
                            {
                                throw new Exception("Template Not Match at Cell G1");
                            }
                            if (row.Cell(i, 8).Value.ToString().Trim().ToLower() != "no rek air")
                            {
                                throw new Exception("Template Not Match at Cell H1");
                            }
                            if (row.Cell(i, 9).Value.ToString().Trim().ToLower() != "konsumsi air")
                            {
                                throw new Exception("Template Not Match at Cell I1");
                            }
                            if (row.Cell(i, 10).Value.ToString().Trim().ToLower() != "tagihan air")
                            {
                                throw new Exception("Template Not Match at Cell J1");
                            }
                            if (row.Cell(i, 11).Value.ToString().Trim().ToLower() != "usaha pengurangan air")
                            {
                                throw new Exception("Template Not Match at Cell K1");
                            }
                            if (row.Cell(i, 12).Value.ToString().Trim().ToLower() != "usaha pengurangan air desc")
                            {
                                throw new Exception("Template Not Match at Cell L1");
                            }
                            if (row.Cell(i, 13).Value.ToString().Trim().ToLower() != "usaha pengurangan air desc file path")
                            {
                                throw new Exception("Template Not Match at Cell M1");
                            }
                            if (row.Cell(i, 14).Value.ToString().Trim().ToLower() != "usaha pengurangan air jumlah")
                            {
                                throw new Exception("Template Not Match at Cell N1");
                            }
                        }
                        else
                        {
                            ImportSdaAirModel sda = new ImportSdaAirModel();
                            sda.ehs_area_id = row.Cell(i, 1).Value.ToString();
                            sda.ba_id = row.Cell(i, 2).Value.ToString();
                            sda.pa_id = row.Cell(i, 3).Value.ToString();
                            sda.psa_id = row.Cell(i, 4).Value.ToString();
                            sda.bulan = row.Cell(i, 5).Value.ToString();
                            sda.tahun = int.Parse(row.Cell(i, 6).Value.ToString());
                            sda.sumber_air_id = row.Cell(i, 7).Value.ToString();
                            sda.no_rek_air = row.Cell(i, 8).Value.ToString();
                            sda.konsumsi_air = double.Parse(row.Cell(i, 9).Value.ToString());
                            sda.tagihan_air = row.Cell(i, 10).Value.ToString();
                            sda.usaha_pengurangan_air_id = row.Cell(i, 11).Value.ToString();
                            sda.usaha_pengurangan_air_desc = row.Cell(i, 12).Value.ToString();
                            sda.usaha_pengurangan_air_desc_file_path = row.Cell(i, 13).Value.ToString();
                            sda.usaha_pengurangan_air_jumlah = double.Parse(row.Cell(i, 14).Value.ToString());
                            ImportRepository sdaAir = new ImportRepository();
                            sdaAir.ImportSdaAir(sda);
                        }
                    }
                    return data;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult TemplateNonLb3()
        {
            try
            {
                string fileName = "Lb3Template.xlsx";
                string path = Path.Combine(ConfigurationManager.AppSettings["filePath"].Replace(@"\\", "/").ToString() + fileName);

                using (var wb = new XLWorkbook())
                {
                    var sda = wb.Worksheets.Add("Sheet1");
                    var currentRow = 1;
                    sda.Cell(currentRow, 1).Value = "Area";
                    sda.Cell(currentRow, 2).Value = "Business Area";
                    sda.Cell(currentRow, 3).Value = "Personal Area";
                    sda.Cell(currentRow, 4).Value = "Personal Sub Area";
                    sda.Cell(currentRow, 5).Value = "Bulan";
                    sda.Cell(currentRow, 6).Value = "Tahun";
                    sda.Cell(currentRow, 7).Value = "Sumber Air";
                    sda.Cell(currentRow, 8).Value = "No Rek Air";
                    sda.Cell(currentRow, 9).Value = "Konsumsi Air";
                    sda.Cell(currentRow, 10).Value = "Tagihan Air";
                    sda.Cell(currentRow, 11).Value = "Usaha Pengurangan Air";
                    sda.Cell(currentRow, 12).Value = "Usaha Pengurangan Air Desc";
                    sda.Cell(currentRow, 13).Value = "Usaha Pengurangan Air Desc File Path";
                    sda.Cell(currentRow, 14).Value = "Usaha Pengurangan Air Jumlah";

                    sda.Columns().AdjustToContents();
                    var lookup = wb.Worksheets.Add("lookup").Hide();
                    var dataArea = GetArea();
                    int a = 1;

                    foreach (string data in dataArea)
                    {
                        lookup.Cell(a, 1).Value = data;
                        a++;
                    }

                    //add lookup
                    sda.Column("A").SetDataValidation().List(lookup.Range("A1:A" + a), true);
                    //lb3.Column("B").SetDataValidation().List("=INDIRECT(\"named_field\"&SUBSTITUTE(A1,\" \",\"_\"))", true);
                    sda.Columns().AdjustToContents();

                    wb.SaveAs(path);
                    FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private List<string> GetArea()
        {
            var result = new List<string>();
            var query = @"select distinct nama as area from ref_ehs_area";
            using (var con = new SqlConnection(ConnStr))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    var data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        result.Add(data["area"].ToString());
                    }
                    con.Close();
                }
            }
            return result;
        }
    }
}