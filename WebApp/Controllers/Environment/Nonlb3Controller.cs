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
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using WebApp.Repository;
using ClosedXML.Excel;

namespace WebApp.Controllers
{
    
    public class Nonlb3Controller : Controller
    {
        private string _rule_view = "EnvironmentView";
        private string _rule_add = "EnvironmentAdd";
        private string _rule_edit = "EnvironmentEdit";
        private string _rule_delete = "EnvironmentDelete";
        private string _rule_edit_all = "EnvironmentEditAll";
        private string _rule_delete_all = "EnvironmentDeleteAll";
        private string _path_controler = "/Nonlb3/";
        private string _path_view = "/Views/Environment/Nonlb3/";
        private readonly string _table_name = "ta_nonlb3";
        private readonly string _table_title = "Pengelolaan Limbah Non B3";

		private static IHostingEnvironment _hostingEnvironment;
        public Nonlb3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(Nonlb3Model.ViewModel filterColumn)
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
                    ViewData["pkKey"] = Nonlb3Model._pkKey;
                    ViewData["displayItem"] = Nonlb3Model.GetDisplayItem();
                    ViewData["column_att"] = Nonlb3Model.GetGridColumn();
                    ViewData["param"] = Nonlb3Model.GetListParam();
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
        public JsonResult GetListData(Nonlb3Model.ActionRequest param)
        {
            GridData data = Nonlb3Model.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i=0; i<data.data.Rows.Count; i++) {
                var PkValue = new OrderedDictionary();
                PkValue["id"] = data.data.Rows[i]["id"];
                data.data.Rows[i]["deskripsi_usaha_file_path"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "deskripsi_usaha_file_path", data.data.Rows[i]["deskripsi_usaha_file_path"].ToString());
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
                    Nonlb3Model.ViewModel fieldModel = new Nonlb3Model.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = Nonlb3Model.GetViewData(PkValue);
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
                                fieldModel.jenis_limbah_id = dr["jenis_limbah_id"].ToString();
                                fieldModel.jenis_limbah_id_text = dr["jenis_limbah_id_text"].ToString();
                                fieldModel.timbulan_limbah_cair = dr["timbulan_limbah_cair"].ToString();
                                fieldModel.timbulan_limbah_padat = String.Format("{0:N2}", dr["timbulan_limbah_padat"]);
                                fieldModel.deskripsi_limbah = dr["deskripsi_limbah"].ToString();
                                fieldModel.pengelolaan_oleh_id = dr["pengelolaan_oleh_id"].ToString();
                                fieldModel.pengelolaan_oleh_id_text = dr["pengelolaan_oleh_id_text"].ToString();
                                fieldModel.usaha_kurang_limbah_id = dr["usaha_kurang_limbah_id"].ToString();
                                fieldModel.usaha_kurang_limbah_id_text = dr["usaha_kurang_limbah_id_text"].ToString();
                                fieldModel.deskripsi_usaha = dr["deskripsi_usaha"].ToString();
                                fieldModel.deskripsi_usaha_file_path = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "deskripsi_usaha_file_path", dr["deskripsi_usaha_file_path"].ToString());
                                fieldModel.usaha_kurang_limbah_m3 = dr["usaha_kurang_limbah_m3"].ToString();
                                fieldModel.usaha_kurang_limbah_kg = String.Format("{0:N2}", dr["usaha_kurang_limbah_kg"]);
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
        public IActionResult Add(Nonlb3Model.ViewModel fieldModel)
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
                    //Nonlb3Model.ViewModel fieldModel = new Nonlb3Model.ViewModel();
                    fieldModel.id = Nonlb3Model.GetNewId().ToString();
                    fieldModel.deskripsi_usaha_file_path_init = "[]";
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
                    DataTable data = Nonlb3Model.GetViewData(PkValue);
                    Nonlb3Model.ViewModel fieldModel = new Nonlb3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = Nonlb3Model.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.bulan = dr["bulan"].ToString();
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.jenis_limbah_id = dr["jenis_limbah_id"].ToString();
                        fieldModel.timbulan_limbah_cair = dr["timbulan_limbah_cair"].ToString();
                        fieldModel.timbulan_limbah_padat = dr["timbulan_limbah_padat"].ToString();
                        fieldModel.deskripsi_limbah = dr["deskripsi_limbah"].ToString();
                        fieldModel.pengelolaan_oleh_id = dr["pengelolaan_oleh_id"].ToString();
                        fieldModel.usaha_kurang_limbah_id = dr["usaha_kurang_limbah_id"].ToString();
                        fieldModel.deskripsi_usaha = dr["deskripsi_usaha"].ToString();
                        fieldModel.deskripsi_usaha_file_path = dr["deskripsi_usaha_file_path"].ToString();
                        fieldModel.usaha_kurang_limbah_m3 = dr["usaha_kurang_limbah_m3"].ToString();
                        fieldModel.usaha_kurang_limbah_kg = dr["usaha_kurang_limbah_kg"].ToString();
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
                    DataTable data = Nonlb3Model.GetViewData(PkValue);
                    Nonlb3Model.ViewModel fieldModel = new Nonlb3Model.ViewModel();
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
                        fieldModel.jenis_limbah_id = dr["jenis_limbah_id"].ToString();
                        fieldModel.jenis_limbah_id_text = dr["jenis_limbah_id_text"].ToString();
                        fieldModel.timbulan_limbah_cair = dr["timbulan_limbah_cair"].ToString();
                        fieldModel.timbulan_limbah_padat = dr["timbulan_limbah_padat"].ToString();
                        fieldModel.deskripsi_limbah = dr["deskripsi_limbah"].ToString();
                        fieldModel.pengelolaan_oleh_id = dr["pengelolaan_oleh_id"].ToString();
                        fieldModel.pengelolaan_oleh_id_text = dr["pengelolaan_oleh_id_text"].ToString();
                        fieldModel.usaha_kurang_limbah_id = dr["usaha_kurang_limbah_id"].ToString();
                        fieldModel.usaha_kurang_limbah_id_text = dr["usaha_kurang_limbah_id_text"].ToString();
                        fieldModel.deskripsi_usaha = dr["deskripsi_usaha"].ToString();
                        string[] init_deskripsi_usaha_file_path = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "deskripsi_usaha_file_path", PkValue, dr["deskripsi_usaha_file_path"].ToString());
                        fieldModel.deskripsi_usaha_file_path = init_deskripsi_usaha_file_path[0];
                        fieldModel.deskripsi_usaha_file_path_init = init_deskripsi_usaha_file_path[1];
                        fieldModel.usaha_kurang_limbah_m3 = dr["usaha_kurang_limbah_m3"].ToString();
                        fieldModel.usaha_kurang_limbah_kg = dr["usaha_kurang_limbah_kg"].ToString();
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
        public JsonResult Insert(Nonlb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = Nonlb3Model.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["jenis_limbah_id"] = AntiXss.HtmlEncode(fieldModel.jenis_limbah_id);
                    data["timbulan_limbah_cair"] = fieldModel.timbulan_limbah_cair != null ? fieldModel.timbulan_limbah_cair.Replace(",", ".") : "";
                    data["timbulan_limbah_padat"] = fieldModel.timbulan_limbah_padat != null ? fieldModel.timbulan_limbah_padat.Replace(",",".") : "";
                    data["deskripsi_limbah"] = AntiXss.HtmlEncode(fieldModel.deskripsi_limbah);
                    data["pengelolaan_oleh_id"] = fieldModel.pengelolaan_oleh_id;
                    data["usaha_kurang_limbah_id"] = fieldModel.usaha_kurang_limbah_id;
                    data["deskripsi_usaha"] = AntiXss.HtmlEncode(fieldModel.deskripsi_usaha);
                    data["deskripsi_usaha_file_path"] = AntiXss.HtmlEncode(fieldModel.deskripsi_usaha_file_path);
                    data["usaha_kurang_limbah_m3"] = fieldModel.usaha_kurang_limbah_m3 != null ? fieldModel.usaha_kurang_limbah_m3.Replace(",", ".") : "";
                    data["usaha_kurang_limbah_kg"] = fieldModel.usaha_kurang_limbah_kg != null ? fieldModel.usaha_kurang_limbah_kg.Replace(",",".") : "";
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = Nonlb3Model.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(Nonlb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(Nonlb3Model.GetData(PkValue));
                    data_old["timbulan_limbah_cair"] = data_old["timbulan_limbah_cair"].ToString() != "" ? data_old["timbulan_limbah_cair"].ToString().Replace(",", ".") : "";
                    data_old["timbulan_limbah_padat"] = data_old["timbulan_limbah_padat"].ToString() != ""  ? data_old["timbulan_limbah_padat"].ToString().Replace(",", ".") : "";
                    data_old["usaha_kurang_limbah_kg"] = data_old["usaha_kurang_limbah_kg"].ToString() != ""  ? data_old["usaha_kurang_limbah_kg"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(Nonlb3Model.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["bulan"] = fieldModel.bulan;
                    data["tahun"] = fieldModel.tahun;
                    data["jenis_limbah_id"] = AntiXss.HtmlEncode(fieldModel.jenis_limbah_id);
                    data["timbulan_limbah_cair"] = fieldModel.timbulan_limbah_cair != null ? fieldModel.timbulan_limbah_cair.Replace(",", ".") : "";
                    data["timbulan_limbah_padat"] = fieldModel.timbulan_limbah_padat != null ? fieldModel.timbulan_limbah_padat.ToString().Replace(",", ".") : "";
                    data["deskripsi_limbah"] = AntiXss.HtmlEncode(fieldModel.deskripsi_limbah);
                    data["pengelolaan_oleh_id"] = fieldModel.pengelolaan_oleh_id;
                    data["usaha_kurang_limbah_id"] = fieldModel.usaha_kurang_limbah_id;
                    data["deskripsi_usaha"] = AntiXss.HtmlEncode(fieldModel.deskripsi_usaha);
                    data["deskripsi_usaha_file_path"] = AntiXss.HtmlEncode(fieldModel.deskripsi_usaha_file_path);
                    data["usaha_kurang_limbah_m3"] = fieldModel.usaha_kurang_limbah_m3 != null ? fieldModel.usaha_kurang_limbah_m3.Replace(",", ".") : "";
                    data["usaha_kurang_limbah_kg"] = fieldModel.usaha_kurang_limbah_kg != null ? fieldModel.usaha_kurang_limbah_kg.ToString().Replace(",", ".") : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = Nonlb3Model.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(Nonlb3Model.GetData(PkValue));
						if (data != null)
						{
							result = Nonlb3Model.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = Nonlb3Model.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_deskripsi_usaha_file_path(IEnumerable<IFormFile> deskripsi_usaha_file_path_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "deskripsi_usaha_file_path");
            if (deskripsi_usaha_file_path_file != null)
            {
                foreach (var file in deskripsi_usaha_file_path_file)
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

        public ActionResult remove_deskripsi_usaha_file_path(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid); pathData = Path.Combine(pathData, _table_name); pathData = Path.Combine(pathData, "deskripsi_usaha_file_path");
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
        public async Task<IActionResult> ImportNonLb3()
        {
            IFormFile file = Request.Form.Files[0];
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Import");
            var dir = Directory.GetCurrentDirectory();
            var doc = "\\Import\\";
            //var fullPath = path + images;
            var path = dir + doc;
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            var fullPath = path + fileName;


            using (var fileStream = new FileStream(Path.Combine(path + fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                file.CopyTo(fileStream);
            }
            List<ImportNonLb3Model> data = await getData(fullPath);

            return Created(string.Empty, null);
        }

        private async Task<List<ImportNonLb3Model>> getData(string fileName)
        {
            List<ImportNonLb3Model> data = new List<ImportNonLb3Model>();
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
                            if (row.Cell(i, 1).Value.ToString().Trim().ToLower() != "ehs area id")
                            {
                                throw new Exception("Template Not Match at Cell A1");
                            }
                            if (row.Cell(i, 2).Value.ToString().Trim().ToLower() != "ba id")
                            {
                                throw new Exception("Template Not Match at Cell B1");
                            }
                            if (row.Cell(i, 3).Value.ToString().Trim().ToLower() != "pa id")
                            {
                                throw new Exception("Template Not Match at Cell C1");
                            }
                            if (row.Cell(i, 4).Value.ToString().Trim().ToLower() != "psa id")
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
                            if (row.Cell(i, 7).Value.ToString().Trim().ToLower() != "jenis limbah id")
                            {
                                throw new Exception("Template Not Match at Cell G1");
                            }
                            if (row.Cell(i, 8).Value.ToString().Trim().ToLower() != "timbulan limbah cair")
                            {
                                throw new Exception("Template Not Match at Cell H1");
                            }
                            if (row.Cell(i, 9).Value.ToString().Trim().ToLower() != "timbulan limbah padat")
                            {
                                throw new Exception("Template Not Match at Cell I1");
                            }
                            if (row.Cell(i, 10).Value.ToString().Trim().ToLower() != "deskripsi limbah")
                            {
                                throw new Exception("Template Not Match at Cell J1");
                            }
                            if (row.Cell(i, 11).Value.ToString().Trim().ToLower() != "pengelolaan oleh id")
                            {
                                throw new Exception("Template Not Match at Cell K1");
                            }
                            if (row.Cell(i, 12).Value.ToString().Trim().ToLower() != "usaha kurang limbah id")
                            {
                                throw new Exception("Template Not Match at Cell L1");
                            }
                            if (row.Cell(i, 13).Value.ToString().Trim().ToLower() != "deskripsi usaha")
                            {
                                throw new Exception("Template Not Match at Cell M1");
                            }
                            if (row.Cell(i, 14).Value.ToString().Trim().ToLower() != "deskripsi usaha file path")
                            {
                                throw new Exception("Template Not Match at Cell N1");
                            }
                            if (row.Cell(i, 15).Value.ToString().Trim().ToLower() != "usaha kurang limbah m3")
                            {
                                throw new Exception("Template Not Match at Cell O1");
                            }
                            if (row.Cell(i, 16).Value.ToString().Trim().ToLower() != "usaha kurang limbah kg")
                            {
                                throw new Exception("Template Not Match at Cell P1");
                            }
                        }
                        else
                        {
                            ImportNonLb3Model importNonLb3Model = new ImportNonLb3Model();
                            importNonLb3Model.ehs_area_id = int.Parse(row.Cell(i, 1).Value.ToString());
                            importNonLb3Model.ba_id = int.Parse(row.Cell(i, 2).Value.ToString());
                            importNonLb3Model.pa_id = int.Parse(row.Cell(i, 3).Value.ToString());
                            importNonLb3Model.psa_id = int.Parse(row.Cell(i, 4).Value.ToString());
                            importNonLb3Model.bulan = int.Parse(row.Cell(i, 5).Value.ToString());
                            importNonLb3Model.tahun = int.Parse(row.Cell(i, 6).Value.ToString());
                            importNonLb3Model.jenis_limbah_id = row.Cell(i, 7).Value.ToString();
                            importNonLb3Model.timbulan_limbah_cair = double.Parse(row.Cell(i, 8).Value.ToString());
                            importNonLb3Model.timbulan_limbah_padat = double.Parse(row.Cell(i, 9).Value.ToString());
                            importNonLb3Model.deskripsi_limbah = row.Cell(i, 10).Value.ToString();
                            importNonLb3Model.pengelolaan_oleh_id = int.Parse(row.Cell(i, 11).Value.ToString());
                            importNonLb3Model.usaha_kurang_limbah_id = int.Parse(row.Cell(i, 12).Value.ToString());
                            importNonLb3Model.deskripsi_usaha = row.Cell(i, 13).Value.ToString();
                            importNonLb3Model.deskripsi_usaha_file_path = row.Cell(i, 14).Value.ToString();
                            importNonLb3Model.usaha_kurang_limbah_m3 = double.Parse(row.Cell(i, 15).Value.ToString());
                            importNonLb3Model.usaha_kurang_limbah_kg = double.Parse(row.Cell(i, 16).Value.ToString());
                            ImportRepository lb3 = new ImportRepository();
                            lb3.ImportNonLb3(importNonLb3Model);
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