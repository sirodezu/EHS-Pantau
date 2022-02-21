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
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using WebApp.Repository;
using ClosedXML.Excel;

namespace WebApp.Controllers
{
    
    public class Lb3Controller : Controller
    {
        private string _rule_view = "EnvironmentView";
        private string _rule_add = "EnvironmentAdd";
        private string _rule_edit = "EnvironmentEdit";
        private string _rule_delete = "EnvironmentDelete";
        private string _rule_edit_all = "EnvironmentEditAll";
        private string _rule_delete_all = "EnvironmentDeleteAll";
        private string _path_controler = "/Lb3/";
        private string _path_view = "/Views/Environment/Lb3/";
        private readonly string _table_name = "ta_lb3";
        private readonly string _table_title = "Pengelolaan Limbah B3";

		private static IHostingEnvironment _hostingEnvironment;
        public Lb3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(Lb3Model.ViewModel filterColumn)
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
                    ViewData["pkKey"] = Lb3Model._pkKey;
                    ViewData["displayItem"] = Lb3Model.GetDisplayItem();
                    ViewData["column_att"] = Lb3Model.GetGridColumn();
                    ViewData["param"] = Lb3Model.GetListParam();
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
        public JsonResult GetListData(Lb3Model.ActionRequest param)
        {
            GridData data = Lb3Model.GetListData(HttpContext, param);
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
                    Lb3Model.ViewModel fieldModel = new Lb3Model.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = Lb3Model.GetViewData(PkValue);
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
                                fieldModel.jenis_limbah_dihasilkan_id = dr["jenis_limbah_dihasilkan_id"].ToString();
                                fieldModel.jenis_limbah_dihasilkan_id_text = dr["jenis_limbah_dihasilkan_id_text"].ToString();
                                fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                                fieldModel.sumber_limbah_id = dr["sumber_limbah_id"].ToString();
                                fieldModel.sumber_limbah_id_text = dr["sumber_limbah_id_text"].ToString();
                                fieldModel.kegiatan_id = dr["kegiatan_id"].ToString();
                                fieldModel.kegiatan_id_text = dr["kegiatan_id_text"].ToString();
                                fieldModel.tgl_dihasilkan = dr["tgl_dihasilkan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_dihasilkan"]) : "";
                                fieldModel.jenis_limbah_dihasilkan = String.Format("{0:N2}", dr["jenis_limbah_dihasilkan"]);
                                fieldModel.pengelolaan_oleh_id = dr["pengelolaan_oleh_id"].ToString();
                                fieldModel.pengelolaan_oleh_id_text = dr["pengelolaan_oleh_id_text"].ToString();
                                fieldModel.masa_simpan_limbah_id = dr["masa_simpan_limbah_id"].ToString();
                                fieldModel.masa_simpan_limbah_id_text = dr["masa_simpan_limbah_id_text"].ToString();
                                fieldModel.tgl_dikeluarkan = dr["tgl_dikeluarkan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_dikeluarkan"]) : "";
                                fieldModel.jumlah_limbah_dikelola = String.Format("{0:N2}", dr["jumlah_limbah_dikelola"]);
                                fieldModel.kode_manifest = dr["kode_manifest"].ToString();
                                fieldModel.perusahaan_angkut_id = dr["perusahaan_angkut_id"].ToString();
                                fieldModel.perusahaan_angkut_id_text = dr["perusahaan_angkut_id_text"].ToString();
                                fieldModel.diserahkan_ke_id = dr["diserahkan_ke_id"].ToString();
                                fieldModel.diserahkan_ke_id_text = dr["diserahkan_ke_id_text"].ToString();
                                fieldModel.perusahaan_serah_id = dr["perusahaan_serah_id"].ToString();
                                fieldModel.perusahaan_serah_id_text = dr["perusahaan_serah_id_text"].ToString();
                                fieldModel.sisa_di_tps = String.Format("{0:N2}", dr["sisa_di_tps"]);
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                                // -----------------------------------------------------------
                                fieldModel.satuan_limbah = dr["satuan_limbah"].ToString();
                                // -----------------------------------------------------------
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
        public IActionResult Add(Lb3Model.ViewModel fieldModel)
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
                    //Lb3Model.ViewModel fieldModel = new Lb3Model.ViewModel();
                    fieldModel.id = Lb3Model.GetNewId().ToString();
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
                    DataTable data = Lb3Model.GetViewData(PkValue);
                    Lb3Model.ViewModel fieldModel = new Lb3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = Lb3Model.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.jenis_limbah_dihasilkan_id = dr["jenis_limbah_dihasilkan_id"].ToString();
                        fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                        fieldModel.sumber_limbah_id = dr["sumber_limbah_id"].ToString();
                        fieldModel.kegiatan_id = dr["kegiatan_id"].ToString();
                        fieldModel.tgl_dihasilkan = dr["tgl_dihasilkan"].ToString();
                        fieldModel.jenis_limbah_dihasilkan = dr["jenis_limbah_dihasilkan"].ToString();
                        fieldModel.pengelolaan_oleh_id = dr["pengelolaan_oleh_id"].ToString();
                        fieldModel.masa_simpan_limbah_id = dr["masa_simpan_limbah_id"].ToString();
                        fieldModel.tgl_dikeluarkan = dr["tgl_dikeluarkan"].ToString();
                        fieldModel.jumlah_limbah_dikelola = dr["jumlah_limbah_dikelola"].ToString();
                        fieldModel.kode_manifest = dr["kode_manifest"].ToString();
                        fieldModel.perusahaan_angkut_id = dr["perusahaan_angkut_id"].ToString();
                        fieldModel.diserahkan_ke_id = dr["diserahkan_ke_id"].ToString();
                        fieldModel.perusahaan_serah_id = dr["perusahaan_serah_id"].ToString();
                        fieldModel.sisa_di_tps = dr["sisa_di_tps"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        // -----------------------------------------------------------
                        fieldModel.satuan_limbah = dr["satuan_limbah"].ToString();
                        // -----------------------------------------------------------
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
                    DataTable data = Lb3Model.GetViewData(PkValue);
                    Lb3Model.ViewModel fieldModel = new Lb3Model.ViewModel();
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
                        fieldModel.jenis_limbah_dihasilkan_id = dr["jenis_limbah_dihasilkan_id"].ToString();
                        fieldModel.jenis_limbah_dihasilkan_id_text = dr["jenis_limbah_dihasilkan_id_text"].ToString();
                        fieldModel.kode_limbah = dr["kode_limbah"].ToString();
                        fieldModel.sumber_limbah_id = dr["sumber_limbah_id"].ToString();
                        fieldModel.sumber_limbah_id_text = dr["sumber_limbah_id_text"].ToString();
                        fieldModel.kegiatan_id = dr["kegiatan_id"].ToString();
                        fieldModel.kegiatan_id_text = dr["kegiatan_id_text"].ToString();
                        fieldModel.tgl_dihasilkan = dr["tgl_dihasilkan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_dihasilkan"]) : "";
                        fieldModel.dt_tgl_dihasilkan = dr["tgl_dihasilkan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_dihasilkan"]) : "";
                        fieldModel.jenis_limbah_dihasilkan = dr["jenis_limbah_dihasilkan"].ToString();
                        fieldModel.pengelolaan_oleh_id = dr["pengelolaan_oleh_id"].ToString();
                        fieldModel.pengelolaan_oleh_id_text = dr["pengelolaan_oleh_id_text"].ToString();
                        fieldModel.masa_simpan_limbah_id = dr["masa_simpan_limbah_id"].ToString();
                        fieldModel.masa_simpan_limbah_id_text = dr["masa_simpan_limbah_id_text"].ToString();
                        fieldModel.tgl_dikeluarkan = dr["tgl_dikeluarkan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_dikeluarkan"]) : "";
                        fieldModel.dt_tgl_dikeluarkan = dr["tgl_dikeluarkan"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_dikeluarkan"]) : "";
                        fieldModel.jumlah_limbah_dikelola = dr["jumlah_limbah_dikelola"].ToString();
                        fieldModel.kode_manifest = dr["kode_manifest"].ToString();
                        fieldModel.perusahaan_angkut_id = dr["perusahaan_angkut_id"].ToString();
                        fieldModel.perusahaan_angkut_id_text = dr["perusahaan_angkut_id_text"].ToString();
                        fieldModel.diserahkan_ke_id = dr["diserahkan_ke_id"].ToString();
                        fieldModel.diserahkan_ke_id_text = dr["diserahkan_ke_id_text"].ToString();
                        fieldModel.perusahaan_serah_id = dr["perusahaan_serah_id"].ToString();
                        fieldModel.perusahaan_serah_id_text = dr["perusahaan_serah_id_text"].ToString();
                        fieldModel.sisa_di_tps = dr["sisa_di_tps"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        // -----------------------------------------------------------
                        fieldModel.satuan_limbah = dr["satuan_limbah"].ToString();
                        // -----------------------------------------------------------
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
        public JsonResult Insert(Lb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = Lb3Model.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["jenis_limbah_dihasilkan_id"] = AntiXss.HtmlEncode(fieldModel.jenis_limbah_dihasilkan_id);
                    data["kode_limbah"] = AntiXss.HtmlEncode(fieldModel.kode_limbah);
                    data["sumber_limbah_id"] = fieldModel.sumber_limbah_id;
                    data["kegiatan_id"] = fieldModel.kegiatan_id;
                    data["tgl_dihasilkan"] = fieldModel.tgl_dihasilkan;
                    data["jenis_limbah_dihasilkan"] = fieldModel.jenis_limbah_dihasilkan != null ? fieldModel.jenis_limbah_dihasilkan.Replace(",",".") : "";
                    data["pengelolaan_oleh_id"] = fieldModel.pengelolaan_oleh_id;
                    data["masa_simpan_limbah_id"] = fieldModel.masa_simpan_limbah_id;
                    data["tgl_dikeluarkan"] = fieldModel.tgl_dikeluarkan;
                    data["jumlah_limbah_dikelola"] = fieldModel.jumlah_limbah_dikelola != null ? fieldModel.jumlah_limbah_dikelola.Replace(",",".") : "";
                    data["kode_manifest"] = AntiXss.HtmlEncode(fieldModel.kode_manifest);
                    data["perusahaan_angkut_id"] = fieldModel.perusahaan_angkut_id;
                    data["diserahkan_ke_id"] = fieldModel.diserahkan_ke_id;
                    data["perusahaan_serah_id"] = fieldModel.perusahaan_serah_id;
                    data["sisa_di_tps"] = fieldModel.sisa_di_tps != null ? fieldModel.sisa_di_tps.Replace(",",".") : "";
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = Lb3Model.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(Lb3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(Lb3Model.GetData(PkValue));
                    data_old["tgl_dihasilkan"] = data_old["tgl_dihasilkan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_dihasilkan"]) : "";
                    data_old["jenis_limbah_dihasilkan"] = data_old["jenis_limbah_dihasilkan"].ToString() != ""  ? data_old["jenis_limbah_dihasilkan"].ToString().Replace(",", ".") : "";
                    data_old["tgl_dikeluarkan"] = data_old["tgl_dikeluarkan"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_dikeluarkan"]) : "";
                    data_old["jumlah_limbah_dikelola"] = data_old["jumlah_limbah_dikelola"].ToString() != ""  ? data_old["jumlah_limbah_dikelola"].ToString().Replace(",", ".") : "";
                    data_old["sisa_di_tps"] = data_old["sisa_di_tps"].ToString() != ""  ? data_old["sisa_di_tps"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(Lb3Model.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["jenis_limbah_dihasilkan_id"] = AntiXss.HtmlEncode(fieldModel.jenis_limbah_dihasilkan_id);
                    data["kode_limbah"] = AntiXss.HtmlEncode(fieldModel.kode_limbah);
                    data["sumber_limbah_id"] = fieldModel.sumber_limbah_id;
                    data["kegiatan_id"] = fieldModel.kegiatan_id;
                    data["tgl_dihasilkan"] = fieldModel.tgl_dihasilkan;
                    data["jenis_limbah_dihasilkan"] = fieldModel.jenis_limbah_dihasilkan != null ? fieldModel.jenis_limbah_dihasilkan.ToString().Replace(",", ".") : "";
                    data["pengelolaan_oleh_id"] = fieldModel.pengelolaan_oleh_id;
                    data["masa_simpan_limbah_id"] = fieldModel.masa_simpan_limbah_id;
                    data["tgl_dikeluarkan"] = fieldModel.tgl_dikeluarkan;
                    data["jumlah_limbah_dikelola"] = fieldModel.jumlah_limbah_dikelola != null ? fieldModel.jumlah_limbah_dikelola.ToString().Replace(",", ".") : "";
                    data["kode_manifest"] = AntiXss.HtmlEncode(fieldModel.kode_manifest);
                    data["perusahaan_angkut_id"] = fieldModel.perusahaan_angkut_id;
                    data["diserahkan_ke_id"] = fieldModel.diserahkan_ke_id;
                    data["perusahaan_serah_id"] = fieldModel.perusahaan_serah_id;
                    data["sisa_di_tps"] = fieldModel.sisa_di_tps != null ? fieldModel.sisa_di_tps.ToString().Replace(",", ".") : "";
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = Lb3Model.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(Lb3Model.GetData(PkValue));
						if (data != null)
						{
							result = Lb3Model.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = Lb3Model.LookupData(param);
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ImportLb3()
        {
            //var httpContextFile = HttpContext.Request.Form.Files[0];
            //string[] namePart = httpContextFile.FileName.Split('.');
            //string extension = namePart[namePart.Length - 1];
            //var extension = "." + files.FileName.Split('.')[files.FileName.Split('.').Length - 1];
            //string fileName = Guid.NewGuid().ToString() + '.' + extension;
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
            //string dir = await saveFile(files, files.FileName);
            //https://karthiktechblog.com/aspnetcore/how-to-upload-a-file-with-net-core-web-api-3-1-using-iformfile
            //List<CriticalAnalysisDto> data = getData(dir, fileName);
            List<ImportModel> data = await getData(fullPath);
            //await _mediator.Send(new CriticalAnalysisImportCmd(company_id, data));

            return Created(string.Empty, null);
        }

        private  async Task<List<ImportModel>> getData(string fileName)
        {
            List<ImportModel> data = new List<ImportModel>();
            try
            {
                using (XLWorkbook workBook = new XLWorkbook(fileName))
                {
                    IXLWorksheet test = workBook.Worksheet("Sheet1");
                    IXLRange range = test.RangeUsed();
                    //int a = range.RowCount();
                    for (int i = 1; i < range.RowCount() + 1; i++)
                    {
                        if(i == 1)
                        {
                            //var testt = test.Cell(i, 1).ToString();
                            if (test.Cell(i, 1).Value.ToString().Trim().ToLower() != "ehs area id")
                            {
                                throw new Exception("Template Not Match at Cell A1");
                            }
                            if (test.Cell(i, 2).Value.ToString().Trim().ToLower() != "ba id")
                            {
                                throw new Exception("Template Not Match at Cell B1");
                            }
                            if (test.Cell(i, 3).Value.ToString().Trim().ToLower() != "pa id")
                            {
                                throw new Exception("Template Not Match at Cell C1");
                            }
                            if (test.Cell(i, 4).Value.ToString().Trim().ToLower() != "jenis limbah dihasilkan id")
                            {
                                throw new Exception("Template Not Match at Cell D1");
                            }
                            if (test.Cell(i, 5).Value.ToString().Trim().ToLower() != "kode limbah")
                            {
                                throw new Exception("Template Not Match at Cell E1");
                            }
                            if (test.Cell(i, 6).Value.ToString().Trim().ToLower() != "sumber limbah id")
                            {
                                throw new Exception("Template Not Match at Cell F1");
                            }
                            if (test.Cell(i, 7).Value.ToString().Trim().ToLower() != "kegiatan id")
                            {
                                throw new Exception("Template Not Match at Cell G1");
                            }
                            if (test.Cell(i, 8).Value.ToString().Trim().ToLower() != "tgl dihasilkan")
                            {
                                throw new Exception("Template Not Match at Cell H1");
                            }
                            if (test.Cell(i, 9).Value.ToString().Trim().ToLower() != "jenis limbah dihasilkan")
                            {
                                throw new Exception("Template Not Match at Cell I1");
                            }
                            if (test.Cell(i, 10).Value.ToString().Trim().ToLower() != "pengelolaan oleh id")
                            {
                                throw new Exception("Template Not Match at Cell J1");
                            }
                            if (test.Cell(i, 11).Value.ToString().Trim().ToLower() != "masa simpan limbah id")
                            {
                                throw new Exception("Template Not Match at Cell K1");
                            }
                            if (test.Cell( i, 12).Value.ToString().Trim().ToLower() != "tgl dikeluarkan")
                            {
                                throw new Exception("Template Not Match at Cell L1");
                            }
                            if (test.Cell( i, 13).Value.ToString().Trim().ToLower() != "jumlah limbah dikelola")
                            {
                                throw new Exception("Template Not Match at Cell M1");
                            }
                            if (test.Cell( i, 14).Value.ToString().Trim().ToLower() != "kode manifest")
                            {
                                throw new Exception("Template Not Match at Cell N1");
                            }
                            if (test.Cell( i, 15).Value.ToString().Trim().ToLower() != "perusahaan angkut id")
                            {
                                throw new Exception("Template Not Match at Cell O1");
                            }
                            if (test.Cell( i, 16).Value.ToString().Trim().ToLower() != "diserahkan ke id")
                            {
                                throw new Exception("Template Not Match at Cell P1");
                            }
                            if (test.Cell( i, 17).Value.ToString().Trim().ToLower() != "perusahaan serah id")
                            {
                                throw new Exception("Template Not Match at Cell Q1");
                            }
                            if (test.Cell( i, 18).Value.ToString().Trim().ToLower() != "sisa di tps")
                            {
                                throw new Exception("Template Not Match at Cell R1");
                            }
                            if (test.Cell( i, 19).Value.ToString().Trim().ToLower() != "psa id")
                            {
                                throw new Exception("Template Not Match at Cell S1");
                            }
                        }
                        else
                        {
                            ImportModel importModels = new ImportModel();
                            importModels.ehs_area_id = int.Parse(test.Cell(i, 1).Value.ToString());
                            importModels.ba_id = int.Parse(test.Cell(i, 2).Value.ToString());
                            importModels.pa_id = int.Parse(test.Cell(i, 3).Value.ToString());
                            importModels.jenis_limbah_dihasilkan_id = test.Cell(i, 4).Value.ToString();
                            importModels.kode_limbah = test.Cell(i, 5).Value.ToString();
                            importModels.sumber_limbah_id = int.Parse(test.Cell(i, 6).Value.ToString());
                            importModels.kegiatan_id = int.Parse(test.Cell(i, 7).Value.ToString());
                            importModels.tgl_dihasilkan = DateTime.Parse(test.Cell(i, 8).Value.ToString());
                            importModels.jenis_limbah_dihasilkan = double.Parse(test.Cell(i, 9).Value.ToString());
                            importModels.pengelolaan_oleh_id = int.Parse(test.Cell(i, 10).Value.ToString());
                            importModels.masa_simpan_limbah_id = int.Parse(test.Cell(i, 11).Value.ToString());
                            importModels.tgl_dikeluarkan = DateTime.Parse(test.Cell(i, 12).Value.ToString());
                            importModels.jumlah_limbah_dikelola = double.Parse(test.Cell(i, 13).Value.ToString());
                            importModels.kode_manifest = test.Cell(i, 14).Value.ToString();
                            importModels.perusahaan_angkut_id = int.Parse(test.Cell(i, 15).Value.ToString());
                            importModels.diserahkan_ke_id = int.Parse(test.Cell(i, 16).Value.ToString());
                            importModels.perusahaan_serah_id = int.Parse(test.Cell(i, 17).Value.ToString());
                            importModels.sisa_di_tps = double.Parse(test.Cell(i, 18).Value.ToString());
                            importModels.psa_id = int.Parse(test.Cell(i, 19).Value.ToString());

                            ImportRepository lb3 = new ImportRepository();
                            lb3.Import(importModels);
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
    }
}