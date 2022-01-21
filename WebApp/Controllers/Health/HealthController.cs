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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Microsoft.Net.Http.Headers;
using WebApp.Extensions;
using System.Globalization;

using Newtonsoft.Json.Linq;

namespace WebApp.Controllers
{
    
    public class HealthController : Controller
    {
        private string _rule_view = "HealthView";
        private string _rule_add = "HealthAdd";
        private string _rule_edit = "HealthEdit";
        private string _rule_delete = "HealthDelete";
        private string _rule_edit_all = "HealthEditAll";
        private string _rule_delete_all = "HealthDeleteAll";
        private string _path_controler = "/Health/";
        private string _path_view = "/Views/Health/Health/";
        private readonly string _table_name = "ta_health";
        private readonly string _table_title = "Health";

		private static IHostingEnvironment _hostingEnvironment;
        public HealthController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(HealthModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = HealthModel._pkKey;
                    ViewData["displayItem"] = HealthModel.GetDisplayItem();
                    ViewData["column_att"] = HealthModel.GetGridColumn();
                    ViewData["param"] = HealthModel.GetListParam();
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
        public JsonResult GetListData(HealthModel.ActionRequest param)
        {
            GridData data = HealthModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["photo_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "photo_file_path", data.data.Rows[i]["photo_file_path"].ToString());
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
                    HealthModel.ViewModel fieldModel = new HealthModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = HealthModel.GetViewData(PkValue);
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
                                fieldModel.karyawan_id = dr["karyawan_id"].ToString();
                                fieldModel.karyawan_id_text = dr["karyawan_id_text"].ToString();
                                fieldModel.kategori_health_colour_id = dr["kategori_health_colour_id"].ToString();
                                fieldModel.kategori_health_colour_id_text = dr["kategori_health_colour_id_text"].ToString();
                                fieldModel.photo_file_name = dr["photo_file_name"].ToString();
                                fieldModel.photo_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "photo_file_path", dr["photo_file_path"].ToString());
                                fieldModel.deskripsi_diderita = dr["deskripsi_diderita"].ToString();
                                fieldModel.deskripsi_pengobatan = dr["deskripsi_pengobatan"].ToString();
                                fieldModel.pola_hidup_dijaga = dr["pola_hidup_dijaga"].ToString();
                                fieldModel.kunjungan_berikutnya = dr["kunjungan_berikutnya"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["kunjungan_berikutnya"]) : "";
                                fieldModel.dr_rujukan = dr["dr_rujukan"].ToString();
                                fieldModel.rs_rujukan = dr["rs_rujukan"].ToString();
                                fieldModel.tgl_periksa_terakhir = dr["tgl_periksa_terakhir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_periksa_terakhir"]) : "";
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                                fieldModel.pengobatan_sebelumnya = dr["pengobatan_sebelumnya"].ToString();
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["health_id"] = fieldModel.id;
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
        public IActionResult Add(HealthModel.ViewModel fieldModel)
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
                    //HealthModel.ViewModel fieldModel = new HealthModel.ViewModel();
                    fieldModel.id = HealthModel.GetNewId().ToString();
                    fieldModel.photo_file_path_init = "[]";
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
                    DataTable data = HealthModel.GetViewData(PkValue);
                    HealthModel.ViewModel fieldModel = new HealthModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = HealthModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.company_id = dr["company_id"].ToString();
                        fieldModel.karyawan_id = dr["karyawan_id"].ToString();
                        fieldModel.kategori_health_colour_id = dr["kategori_health_colour_id"].ToString();
                        fieldModel.photo_file_name = dr["photo_file_name"].ToString();
                        fieldModel.photo_file_path = dr["photo_file_path"].ToString();
                        fieldModel.deskripsi_diderita = dr["deskripsi_diderita"].ToString();
                        fieldModel.deskripsi_pengobatan = dr["deskripsi_pengobatan"].ToString();
                        fieldModel.pola_hidup_dijaga = dr["pola_hidup_dijaga"].ToString();
                        fieldModel.kunjungan_berikutnya = dr["kunjungan_berikutnya"].ToString();
                        fieldModel.dr_rujukan = dr["dr_rujukan"].ToString();
                        fieldModel.rs_rujukan = dr["rs_rujukan"].ToString();
                        fieldModel.tgl_periksa_terakhir = dr["tgl_periksa_terakhir"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.pengobatan_sebelumnya = dr["pengobatan_sebelumnya"].ToString();
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
                    DataTable data = HealthModel.GetViewData(PkValue);
                    HealthModel.ViewModel fieldModel = new HealthModel.ViewModel();
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
                        fieldModel.karyawan_id = dr["karyawan_id"].ToString();
                        fieldModel.karyawan_id_text = dr["karyawan_id_text"].ToString();
                        fieldModel.kategori_health_colour_id = dr["kategori_health_colour_id"].ToString();
                        fieldModel.kategori_health_colour_id_text = dr["kategori_health_colour_id_text"].ToString();
                        fieldModel.photo_file_name = dr["photo_file_name"].ToString();
                        string[] init_photo_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "photo_file_path", PkValue, dr["photo_file_path"].ToString());
                        fieldModel.photo_file_path = init_photo_file_path[0];
                        fieldModel.photo_file_path_init = init_photo_file_path[1];
                        fieldModel.deskripsi_diderita = dr["deskripsi_diderita"].ToString();
                        fieldModel.deskripsi_pengobatan = dr["deskripsi_pengobatan"].ToString();
                        fieldModel.pola_hidup_dijaga = dr["pola_hidup_dijaga"].ToString();
                        fieldModel.kunjungan_berikutnya = dr["kunjungan_berikutnya"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["kunjungan_berikutnya"]) : "";
                        fieldModel.dt_kunjungan_berikutnya = dr["kunjungan_berikutnya"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["kunjungan_berikutnya"]) : "";
                        fieldModel.dr_rujukan = dr["dr_rujukan"].ToString();
                        fieldModel.rs_rujukan = dr["rs_rujukan"].ToString();
                        fieldModel.tgl_periksa_terakhir = dr["tgl_periksa_terakhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_periksa_terakhir"]) : "";
                        fieldModel.dt_tgl_periksa_terakhir = dr["tgl_periksa_terakhir"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_periksa_terakhir"]) : "";
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.pengobatan_sebelumnya = dr["pengobatan_sebelumnya"].ToString();
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
        public JsonResult Insert(HealthModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = HealthModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["karyawan_id"] = fieldModel.karyawan_id;
                    data["kategori_health_colour_id"] = fieldModel.kategori_health_colour_id;
                    data["photo_file_name"] = AntiXss.HtmlEncode(fieldModel.photo_file_name);
                    data["photo_file_path"] = AntiXss.HtmlEncode(fieldModel.photo_file_path);
                    data["deskripsi_diderita"] = AntiXss.HtmlEncode(fieldModel.deskripsi_diderita);
                    data["deskripsi_pengobatan"] = AntiXss.HtmlEncode(fieldModel.deskripsi_pengobatan);
                    data["pola_hidup_dijaga"] = AntiXss.HtmlEncode(fieldModel.pola_hidup_dijaga);
                    data["kunjungan_berikutnya"] = fieldModel.kunjungan_berikutnya;
                    data["dr_rujukan"] = AntiXss.HtmlEncode(fieldModel.dr_rujukan);
                    data["rs_rujukan"] = AntiXss.HtmlEncode(fieldModel.rs_rujukan);
                    data["tgl_periksa_terakhir"] = fieldModel.tgl_periksa_terakhir;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    data["pengobatan_sebelumnya"] = AntiXss.HtmlEncode(fieldModel.pengobatan_sebelumnya);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = HealthModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(HealthModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(HealthModel.GetData(PkValue));
                    data_old["kunjungan_berikutnya"] = data_old["kunjungan_berikutnya"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["kunjungan_berikutnya"]) : "";
                    data_old["tgl_periksa_terakhir"] = data_old["tgl_periksa_terakhir"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_periksa_terakhir"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(HealthModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["company_id"] = fieldModel.company_id;
                    data["karyawan_id"] = fieldModel.karyawan_id;
                    data["kategori_health_colour_id"] = fieldModel.kategori_health_colour_id;
                    data["photo_file_name"] = AntiXss.HtmlEncode(fieldModel.photo_file_name);
                    data["photo_file_path"] = AntiXss.HtmlEncode(fieldModel.photo_file_path);
                    data["deskripsi_diderita"] = AntiXss.HtmlEncode(fieldModel.deskripsi_diderita);
                    data["deskripsi_pengobatan"] = AntiXss.HtmlEncode(fieldModel.deskripsi_pengobatan);
                    data["pola_hidup_dijaga"] = AntiXss.HtmlEncode(fieldModel.pola_hidup_dijaga);
                    data["kunjungan_berikutnya"] = fieldModel.kunjungan_berikutnya;
                    data["dr_rujukan"] = AntiXss.HtmlEncode(fieldModel.dr_rujukan);
                    data["rs_rujukan"] = AntiXss.HtmlEncode(fieldModel.rs_rujukan);
                    data["tgl_periksa_terakhir"] = fieldModel.tgl_periksa_terakhir;
                    data["pengobatan_sebelumnya"] = AntiXss.HtmlEncode(fieldModel.pengobatan_sebelumnya);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = HealthModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(HealthModel.GetData(PkValue));
						if (data != null)
						{
							result = HealthModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = HealthModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_photo_file_path(IEnumerable<IFormFile> photo_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "photo_file_path");
            if (photo_file_path_file != null)
            {
                foreach (var file in photo_file_path_file)
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

        public ActionResult remove_photo_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "photo_file_path");
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







        public DataTable GetDataGridExport(string DisplayedColumns, string FilteredColumns, string QuickSearch, string SortedStatus)
        {
            HealthModel.ActionRequest param = new HealthModel.ActionRequest();


            List<GridColumn> displayedColumns = new List<GridColumn>();
            if (!string.IsNullOrEmpty(DisplayedColumns))
            {
                JArray dc = JsonConvert.DeserializeObject<JArray>(DisplayedColumns);
                displayedColumns = dc.ToObject<List<GridColumn>>();
            }

            List<ItemFilterColumn> filteredColumns = new List<ItemFilterColumn>();
            if (!string.IsNullOrEmpty(FilteredColumns))
            {
                JArray fc = JsonConvert.DeserializeObject<JArray>(FilteredColumns);
                filteredColumns = fc.ToObject<List<ItemFilterColumn>>();
                param.filter_by_column = filteredColumns;
            }

            GroupFilter quickSearch = new GroupFilter();
            if (!string.IsNullOrEmpty(QuickSearch))
            {
                JObject qs = JObject.Parse(QuickSearch);
                quickSearch = qs.ToObject<GroupFilter>();
                param.filter = quickSearch;
            }

            List<GroupSort> sortedStatus = new List<GroupSort>();
            if (!string.IsNullOrEmpty(SortedStatus))
            {
                JArray sr = JsonConvert.DeserializeObject<JArray>(SortedStatus);
                sortedStatus = sr.ToObject<List<GroupSort>>();
                param.sort = sortedStatus;
            }

            return HealthModel.GetExportData(param, displayedColumns);
        }
        public ActionResult GenerateExportFile(string DisplayedColumns, string FilteredColumns, string QuickSearch, string SortedStatus)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.TimeSeparator = ":";
            System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;

            DataTable data = GetDataGridExport(DisplayedColumns, FilteredColumns, QuickSearch, SortedStatus);

            string sFileName = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("ExportFileName", sFileName);
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            
            string pathDest = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp"; //_hostingEnvironment.WebRootPath;            
            if (!Directory.Exists(pathDest)) { FileHelper.CreateDirectoryRecursively(pathDest); }
            FileInfo file = new FileInfo(Path.Combine(pathDest, sFileName));



            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Health");
            excelSheet.DefaultColumnWidth = 0;

            int row_index = 0;
            IRow row = excelSheet.CreateRow(row_index);


            ICellStyle cellStyleCenter = workbook.CreateCellStyle();
            cellStyleCenter.Alignment = HorizontalAlignment.Center;

            IFont fontBold = workbook.CreateFont();
            fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            //Header
            ICellStyle styleHeader = workbook.CreateCellStyle();
            styleHeader.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            styleHeader.FillPattern = FillPattern.SolidForeground;
            styleHeader.SetFont(fontBold);
            foreach (DataColumn c in data.Columns)
            {
                var colTitle = c.Caption;
                var cell = row.CreateCell(c.Ordinal);
                cell.CellStyle = styleHeader;
                cell.SetCellValue(colTitle);
            }           

            //Content
            using (var fs = new FileStream(Path.Combine(pathDest, sFileName), FileMode.Create, FileAccess.Write))
            {
                foreach (DataRow r in data.Rows)
                {
                    row_index += 1;
                    row = excelSheet.CreateRow(row_index);
                    foreach ( DataColumn c in data.Columns)
                    {
                        string colTitle = c.Caption.ToLower().Replace(" ", "");
                        string colDataType = c.DataType.Name.ToLower();
                        int colIndex = c.Ordinal;
                        var cell = row.CreateCell(colIndex);                           
                        if (colDataType.Contains("int"))
                        {
                            cell.SetCellValue( double.Parse(r[colIndex].ToString()) );
                            cell.SetCellType(CellType.Numeric);
                            cell.CellStyle = cellStyleCenter;
                        }
                        else if (colDataType.Contains("date"))
                        {
                            if (!(r[colIndex] == DBNull.Value))
                            {
                                cell.SetCellValue(String.Format("{0:dd/MM/yyyy}", r[colIndex]));
                                cell.CellStyle = cellStyleCenter;
                            }
                        }
                        else
                        {
                            cell.SetCellValue(r[colIndex].ToString());
                        }
                    }
                }
                foreach (DataColumn c in data.Columns)
                {
                    excelSheet.AutoSizeColumn(c.Ordinal, false);
                }
                workbook.Write(fs);
            }


            var memory = new MemoryStream();
            using (var stream = new FileStream(Path.Combine(pathDest, sFileName), FileMode.Open))
            {
                stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            return Json(new { success = true });
        }

        public ActionResult DownloadExcelReport()
        {
            var sFileName = HttpContext.Session.GetString("ExportFileName");
            string Files = (Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp") + @"/" + sFileName; //"wwwroot /" + sFileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
            System.IO.File.WriteAllBytes(Files, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            try { System.IO.File.Delete(Files); HttpContext.Session.Remove("ExportFileName"); } finally { }
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Health.xlsx");
        }


        




    }
}