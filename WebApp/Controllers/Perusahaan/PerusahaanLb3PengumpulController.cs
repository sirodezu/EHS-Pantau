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
    
    public class PerusahaanLb3PengumpulController : Controller
    {
        private string _rule_view = "GeneralView";
        private string _rule_add = "GeneralAdd";
        private string _rule_edit = "GeneralEdit";
        private string _rule_delete = "GeneralDelete";
        private string _rule_edit_all = "GeneralEditAll";
        private string _rule_delete_all = "GeneralDeleteAll";
        private string _path_controler = "/PerusahaanLb3Pengumpul/";
        private string _path_view = "/Views/Perusahaan/PerusahaanLb3Pengumpul/";
        private readonly string _table_name = "ref_lb3_pengumpul";
        private readonly string _table_title = "Perusahaan Pengumpul Limbah B3";

		private static IHostingEnvironment _hostingEnvironment;
        public PerusahaanLb3PengumpulController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(PerusahaanLb3PengumpulModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = PerusahaanLb3PengumpulModel._pkKey;
                    ViewData["displayItem"] = PerusahaanLb3PengumpulModel.GetDisplayItem();
                    ViewData["column_att"] = PerusahaanLb3PengumpulModel.GetGridColumn();
                    ViewData["param"] = PerusahaanLb3PengumpulModel.GetListParam();
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
        public JsonResult GetListData(PerusahaanLb3PengumpulModel.ActionRequest param)
        {
            GridData data = PerusahaanLb3PengumpulModel.GetListData(param);
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
                    PerusahaanLb3PengumpulModel.ViewModel fieldModel = new PerusahaanLb3PengumpulModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = PerusahaanLb3PengumpulModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.perusahaan_id = dr["perusahaan_id"].ToString();
                                fieldModel.no_kontrak_penghasil = dr["no_kontrak_penghasil"].ToString();
                                fieldModel.tgl_kontrak_penghasil = dr["tgl_kontrak_penghasil"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kontrak_penghasil"]) : "";
                                fieldModel.masa_berlaku_kontrak_penghasil = dr["masa_berlaku_kontrak_penghasil"].ToString();
                                fieldModel.no_kontrak_pengangkut = dr["no_kontrak_pengangkut"].ToString();
                                fieldModel.tgl_kontrak_pengangkut = dr["tgl_kontrak_pengangkut"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kontrak_pengangkut"]) : "";
                                fieldModel.masa_berlaku_kontrak_pengangkut = dr["masa_berlaku_kontrak_pengangkut"].ToString();
                                fieldModel.no_izin_pengumpul_klh = dr["no_izin_pengumpul_klh"].ToString();
                                fieldModel.tgl_berlaku_izin_klh = dr["tgl_berlaku_izin_klh"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_berlaku_izin_klh"]) : "";
                                fieldModel.masa_berlaku_izin_klh = dr["masa_berlaku_izin_klh"].ToString();
                                fieldModel.nama_pihak_pengolah = dr["nama_pihak_pengolah"].ToString();
                                fieldModel.no_kontrak_pengolah = dr["no_kontrak_pengolah"].ToString();
                                fieldModel.tgl_kontrak_pengolah = dr["tgl_kontrak_pengolah"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kontrak_pengolah"]) : "";
                                fieldModel.masa_berlaku_kontrak_pengolah = dr["masa_berlaku_kontrak_pengolah"].ToString();
                                fieldModel.no_izin_pengolah_klh = dr["no_izin_pengolah_klh"].ToString();
                                fieldModel.tgl_berlaku_izin_pengolah_klh = dr["tgl_berlaku_izin_pengolah_klh"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_berlaku_izin_pengolah_klh"]) : "";
                                fieldModel.masa_berlaku_izin_pengolah_klh = dr["masa_berlaku_izin_pengolah_klh"].ToString();
                                fieldModel.surat_tdk_bermasalah = dr["surat_tdk_bermasalah"].ToString();
                                fieldModel.surat_tdk_bermasalah_text = dr["surat_tdk_bermasalah_text"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["pengumpul_id"] = fieldModel.id;
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
        public IActionResult Add(PerusahaanLb3PengumpulModel.ViewModel fieldModel)
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
                    //PerusahaanLb3PengumpulModel.ViewModel fieldModel = new PerusahaanLb3PengumpulModel.ViewModel();
                    fieldModel.id = PerusahaanLb3PengumpulModel.GetNewId().ToString();
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
                    DataTable data = PerusahaanLb3PengumpulModel.GetViewData(PkValue);
                    PerusahaanLb3PengumpulModel.ViewModel fieldModel = new PerusahaanLb3PengumpulModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = PerusahaanLb3PengumpulModel.GetNewId().ToString();
                        fieldModel.perusahaan_id = dr["perusahaan_id"].ToString();
                        fieldModel.no_kontrak_penghasil = dr["no_kontrak_penghasil"].ToString();
                        fieldModel.tgl_kontrak_penghasil = dr["tgl_kontrak_penghasil"].ToString();
                        fieldModel.masa_berlaku_kontrak_penghasil = dr["masa_berlaku_kontrak_penghasil"].ToString();
                        fieldModel.no_kontrak_pengangkut = dr["no_kontrak_pengangkut"].ToString();
                        fieldModel.tgl_kontrak_pengangkut = dr["tgl_kontrak_pengangkut"].ToString();
                        fieldModel.masa_berlaku_kontrak_pengangkut = dr["masa_berlaku_kontrak_pengangkut"].ToString();
                        fieldModel.no_izin_pengumpul_klh = dr["no_izin_pengumpul_klh"].ToString();
                        fieldModel.tgl_berlaku_izin_klh = dr["tgl_berlaku_izin_klh"].ToString();
                        fieldModel.masa_berlaku_izin_klh = dr["masa_berlaku_izin_klh"].ToString();
                        fieldModel.nama_pihak_pengolah = dr["nama_pihak_pengolah"].ToString();
                        fieldModel.no_kontrak_pengolah = dr["no_kontrak_pengolah"].ToString();
                        fieldModel.tgl_kontrak_pengolah = dr["tgl_kontrak_pengolah"].ToString();
                        fieldModel.masa_berlaku_kontrak_pengolah = dr["masa_berlaku_kontrak_pengolah"].ToString();
                        fieldModel.no_izin_pengolah_klh = dr["no_izin_pengolah_klh"].ToString();
                        fieldModel.tgl_berlaku_izin_pengolah_klh = dr["tgl_berlaku_izin_pengolah_klh"].ToString();
                        fieldModel.masa_berlaku_izin_pengolah_klh = dr["masa_berlaku_izin_pengolah_klh"].ToString();
                        fieldModel.surat_tdk_bermasalah = dr["surat_tdk_bermasalah"].ToString();
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
                    DataTable data = PerusahaanLb3PengumpulModel.GetViewData(PkValue);
                    PerusahaanLb3PengumpulModel.ViewModel fieldModel = new PerusahaanLb3PengumpulModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.perusahaan_id = dr["perusahaan_id"].ToString();
                        fieldModel.no_kontrak_penghasil = dr["no_kontrak_penghasil"].ToString();
                        fieldModel.tgl_kontrak_penghasil = dr["tgl_kontrak_penghasil"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kontrak_penghasil"]) : "";
                        fieldModel.dt_tgl_kontrak_penghasil = dr["tgl_kontrak_penghasil"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kontrak_penghasil"]) : "";
                        fieldModel.masa_berlaku_kontrak_penghasil = dr["masa_berlaku_kontrak_penghasil"].ToString();
                        fieldModel.no_kontrak_pengangkut = dr["no_kontrak_pengangkut"].ToString();
                        fieldModel.tgl_kontrak_pengangkut = dr["tgl_kontrak_pengangkut"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kontrak_pengangkut"]) : "";
                        fieldModel.dt_tgl_kontrak_pengangkut = dr["tgl_kontrak_pengangkut"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kontrak_pengangkut"]) : "";
                        fieldModel.masa_berlaku_kontrak_pengangkut = dr["masa_berlaku_kontrak_pengangkut"].ToString();
                        fieldModel.no_izin_pengumpul_klh = dr["no_izin_pengumpul_klh"].ToString();
                        fieldModel.tgl_berlaku_izin_klh = dr["tgl_berlaku_izin_klh"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_berlaku_izin_klh"]) : "";
                        fieldModel.dt_tgl_berlaku_izin_klh = dr["tgl_berlaku_izin_klh"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_berlaku_izin_klh"]) : "";
                        fieldModel.masa_berlaku_izin_klh = dr["masa_berlaku_izin_klh"].ToString();
                        fieldModel.nama_pihak_pengolah = dr["nama_pihak_pengolah"].ToString();
                        fieldModel.no_kontrak_pengolah = dr["no_kontrak_pengolah"].ToString();
                        fieldModel.tgl_kontrak_pengolah = dr["tgl_kontrak_pengolah"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_kontrak_pengolah"]) : "";
                        fieldModel.dt_tgl_kontrak_pengolah = dr["tgl_kontrak_pengolah"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_kontrak_pengolah"]) : "";
                        fieldModel.masa_berlaku_kontrak_pengolah = dr["masa_berlaku_kontrak_pengolah"].ToString();
                        fieldModel.no_izin_pengolah_klh = dr["no_izin_pengolah_klh"].ToString();
                        fieldModel.tgl_berlaku_izin_pengolah_klh = dr["tgl_berlaku_izin_pengolah_klh"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tgl_berlaku_izin_pengolah_klh"]) : "";
                        fieldModel.dt_tgl_berlaku_izin_pengolah_klh = dr["tgl_berlaku_izin_pengolah_klh"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tgl_berlaku_izin_pengolah_klh"]) : "";
                        fieldModel.masa_berlaku_izin_pengolah_klh = dr["masa_berlaku_izin_pengolah_klh"].ToString();
                        fieldModel.surat_tdk_bermasalah = dr["surat_tdk_bermasalah"].ToString();
                        fieldModel.surat_tdk_bermasalah_text = dr["surat_tdk_bermasalah_text"].ToString();
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
        public JsonResult Insert(PerusahaanLb3PengumpulModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = PerusahaanLb3PengumpulModel.GetNewId().ToString();
                    data["perusahaan_id"] = fieldModel.perusahaan_id;
                    data["no_kontrak_penghasil"] = AntiXss.HtmlEncode(fieldModel.no_kontrak_penghasil);
                    data["tgl_kontrak_penghasil"] = fieldModel.tgl_kontrak_penghasil;
                    data["masa_berlaku_kontrak_penghasil"] = fieldModel.masa_berlaku_kontrak_penghasil;
                    data["no_kontrak_pengangkut"] = AntiXss.HtmlEncode(fieldModel.no_kontrak_pengangkut);
                    data["tgl_kontrak_pengangkut"] = fieldModel.tgl_kontrak_pengangkut;
                    data["masa_berlaku_kontrak_pengangkut"] = fieldModel.masa_berlaku_kontrak_pengangkut;
                    data["no_izin_pengumpul_klh"] = AntiXss.HtmlEncode(fieldModel.no_izin_pengumpul_klh);
                    data["tgl_berlaku_izin_klh"] = fieldModel.tgl_berlaku_izin_klh;
                    data["masa_berlaku_izin_klh"] = fieldModel.masa_berlaku_izin_klh;
                    data["nama_pihak_pengolah"] = AntiXss.HtmlEncode(fieldModel.nama_pihak_pengolah);
                    data["no_kontrak_pengolah"] = AntiXss.HtmlEncode(fieldModel.no_kontrak_pengolah);
                    data["tgl_kontrak_pengolah"] = fieldModel.tgl_kontrak_pengolah;
                    data["masa_berlaku_kontrak_pengolah"] = fieldModel.masa_berlaku_kontrak_pengolah;
                    data["no_izin_pengolah_klh"] = AntiXss.HtmlEncode(fieldModel.no_izin_pengolah_klh);
                    data["tgl_berlaku_izin_pengolah_klh"] = fieldModel.tgl_berlaku_izin_pengolah_klh;
                    data["masa_berlaku_izin_pengolah_klh"] = fieldModel.masa_berlaku_izin_pengolah_klh;
                    data["surat_tdk_bermasalah"] = fieldModel.surat_tdk_bermasalah;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = PerusahaanLb3PengumpulModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(PerusahaanLb3PengumpulModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(PerusahaanLb3PengumpulModel.GetData(PkValue));
                    data_old["tgl_kontrak_penghasil"] = data_old["tgl_kontrak_penghasil"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kontrak_penghasil"]) : "";
                    data_old["tgl_kontrak_pengangkut"] = data_old["tgl_kontrak_pengangkut"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kontrak_pengangkut"]) : "";
                    data_old["tgl_berlaku_izin_klh"] = data_old["tgl_berlaku_izin_klh"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_berlaku_izin_klh"]) : "";
                    data_old["tgl_kontrak_pengolah"] = data_old["tgl_kontrak_pengolah"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_kontrak_pengolah"]) : "";
                    data_old["tgl_berlaku_izin_pengolah_klh"] = data_old["tgl_berlaku_izin_pengolah_klh"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tgl_berlaku_izin_pengolah_klh"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(PerusahaanLb3PengumpulModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["perusahaan_id"] = fieldModel.perusahaan_id;
                    data["no_kontrak_penghasil"] = AntiXss.HtmlEncode(fieldModel.no_kontrak_penghasil);
                    data["tgl_kontrak_penghasil"] = fieldModel.tgl_kontrak_penghasil;
                    data["masa_berlaku_kontrak_penghasil"] = fieldModel.masa_berlaku_kontrak_penghasil;
                    data["no_kontrak_pengangkut"] = AntiXss.HtmlEncode(fieldModel.no_kontrak_pengangkut);
                    data["tgl_kontrak_pengangkut"] = fieldModel.tgl_kontrak_pengangkut;
                    data["masa_berlaku_kontrak_pengangkut"] = fieldModel.masa_berlaku_kontrak_pengangkut;
                    data["no_izin_pengumpul_klh"] = AntiXss.HtmlEncode(fieldModel.no_izin_pengumpul_klh);
                    data["tgl_berlaku_izin_klh"] = fieldModel.tgl_berlaku_izin_klh;
                    data["masa_berlaku_izin_klh"] = fieldModel.masa_berlaku_izin_klh;
                    data["nama_pihak_pengolah"] = AntiXss.HtmlEncode(fieldModel.nama_pihak_pengolah);
                    data["no_kontrak_pengolah"] = AntiXss.HtmlEncode(fieldModel.no_kontrak_pengolah);
                    data["tgl_kontrak_pengolah"] = fieldModel.tgl_kontrak_pengolah;
                    data["masa_berlaku_kontrak_pengolah"] = fieldModel.masa_berlaku_kontrak_pengolah;
                    data["no_izin_pengolah_klh"] = AntiXss.HtmlEncode(fieldModel.no_izin_pengolah_klh);
                    data["tgl_berlaku_izin_pengolah_klh"] = fieldModel.tgl_berlaku_izin_pengolah_klh;
                    data["masa_berlaku_izin_pengolah_klh"] = fieldModel.masa_berlaku_izin_pengolah_klh;
                    data["surat_tdk_bermasalah"] = fieldModel.surat_tdk_bermasalah;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = PerusahaanLb3PengumpulModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(PerusahaanLb3PengumpulModel.GetData(PkValue));
						if (data != null)
						{
							result = PerusahaanLb3PengumpulModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = PerusahaanLb3PengumpulModel.LookupData(param);
            return Json(data);
        }

    }
}