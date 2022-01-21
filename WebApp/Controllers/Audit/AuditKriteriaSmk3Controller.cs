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
    
    public class AuditKriteriaSmk3Controller : Controller
    {
        private string _rule_view = "AuditView";
        private string _rule_add = "AuditAdd";
        private string _rule_edit = "AuditEdit";
        private string _rule_delete = "AuditDelete";
        private string _rule_edit_all = "AuditEditAll";
        private string _rule_delete_all = "AuditDeleteAll";
        private string _path_controler = "/AuditKriteriaSmk3/";
        private string _path_view = "/Views/Audit/AuditKriteriaSmk3/";
        private readonly string _table_name = "ta_audit_kriteria_smk3";
        private readonly string _table_title = "Audit Kriteria Smk3";

		private static IHostingEnvironment _hostingEnvironment;
        public AuditKriteriaSmk3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(AuditKriteriaSmk3Model.ViewModel filterColumn)
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
                    ViewData["pkKey"] = AuditKriteriaSmk3Model._pkKey;
                    ViewData["displayItem"] = AuditKriteriaSmk3Model.GetDisplayItem();
                    ViewData["column_att"] = AuditKriteriaSmk3Model.GetGridColumn();
                    ViewData["param"] = AuditKriteriaSmk3Model.GetListParam();
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
        public JsonResult GetListData(AuditKriteriaSmk3Model.ActionRequest param)
        {
            GridData data = AuditKriteriaSmk3Model.GetListData(param);
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
                    AuditKriteriaSmk3Model.ViewModel fieldModel = new AuditKriteriaSmk3Model.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AuditKriteriaSmk3Model.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.audit_id = dr["audit_id"].ToString();
                                fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                                fieldModel.pembangunan_komitmen = String.Format("{0:N2}", dr["pembangunan_komitmen"]);
                                fieldModel.pembuatan_rencana_kerja = String.Format("{0:N2}", dr["pembuatan_rencana_kerja"]);
                                fieldModel.pengendalian_kontrak = String.Format("{0:N2}", dr["pengendalian_kontrak"]);
                                fieldModel.pengendalian_dokumen = String.Format("{0:N2}", dr["pengendalian_dokumen"]);
                                fieldModel.pengendalian_produk = String.Format("{0:N2}", dr["pengendalian_produk"]);
                                fieldModel.keamanan = String.Format("{0:N2}", dr["keamanan"]);
                                fieldModel.pemantauan = String.Format("{0:N2}", dr["pemantauan"]);
                                fieldModel.pelaporan_kekurangan = String.Format("{0:N2}", dr["pelaporan_kekurangan"]);
                                fieldModel.pengelolaan_material = String.Format("{0:N2}", dr["pengelolaan_material"]);
                                fieldModel.pengumpulan_data = String.Format("{0:N2}", dr["pengumpulan_data"]);
                                fieldModel.pemeriksaan = String.Format("{0:N2}", dr["pemeriksaan"]);
                                fieldModel.pengembangan_keterampilan = String.Format("{0:N2}", dr["pengembangan_keterampilan"]);
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
        public IActionResult Add(AuditKriteriaSmk3Model.ViewModel fieldModel)
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
                    //AuditKriteriaSmk3Model.ViewModel fieldModel = new AuditKriteriaSmk3Model.ViewModel();
                    fieldModel.id = AuditKriteriaSmk3Model.GetNewId().ToString();
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
                    DataTable data = AuditKriteriaSmk3Model.GetViewData(PkValue);
                    AuditKriteriaSmk3Model.ViewModel fieldModel = new AuditKriteriaSmk3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = AuditKriteriaSmk3Model.GetNewId().ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.pembangunan_komitmen = dr["pembangunan_komitmen"].ToString();
                        fieldModel.pembuatan_rencana_kerja = dr["pembuatan_rencana_kerja"].ToString();
                        fieldModel.pengendalian_kontrak = dr["pengendalian_kontrak"].ToString();
                        fieldModel.pengendalian_dokumen = dr["pengendalian_dokumen"].ToString();
                        fieldModel.pengendalian_produk = dr["pengendalian_produk"].ToString();
                        fieldModel.keamanan = dr["keamanan"].ToString();
                        fieldModel.pemantauan = dr["pemantauan"].ToString();
                        fieldModel.pelaporan_kekurangan = dr["pelaporan_kekurangan"].ToString();
                        fieldModel.pengelolaan_material = dr["pengelolaan_material"].ToString();
                        fieldModel.pengumpulan_data = dr["pengumpulan_data"].ToString();
                        fieldModel.pemeriksaan = dr["pemeriksaan"].ToString();
                        fieldModel.pengembangan_keterampilan = dr["pengembangan_keterampilan"].ToString();
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
                    DataTable data = AuditKriteriaSmk3Model.GetViewData(PkValue);
                    AuditKriteriaSmk3Model.ViewModel fieldModel = new AuditKriteriaSmk3Model.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.pembangunan_komitmen = dr["pembangunan_komitmen"].ToString();
                        fieldModel.pembuatan_rencana_kerja = dr["pembuatan_rencana_kerja"].ToString();
                        fieldModel.pengendalian_kontrak = dr["pengendalian_kontrak"].ToString();
                        fieldModel.pengendalian_dokumen = dr["pengendalian_dokumen"].ToString();
                        fieldModel.pengendalian_produk = dr["pengendalian_produk"].ToString();
                        fieldModel.keamanan = dr["keamanan"].ToString();
                        fieldModel.pemantauan = dr["pemantauan"].ToString();
                        fieldModel.pelaporan_kekurangan = dr["pelaporan_kekurangan"].ToString();
                        fieldModel.pengelolaan_material = dr["pengelolaan_material"].ToString();
                        fieldModel.pengumpulan_data = dr["pengumpulan_data"].ToString();
                        fieldModel.pemeriksaan = dr["pemeriksaan"].ToString();
                        fieldModel.pengembangan_keterampilan = dr["pengembangan_keterampilan"].ToString();
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
        public JsonResult Insert(AuditKriteriaSmk3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = AuditKriteriaSmk3Model.GetNewId().ToString();
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["pembangunan_komitmen"] = fieldModel.pembangunan_komitmen != null ? fieldModel.pembangunan_komitmen.Replace(",",".") : "";
                    data["pembuatan_rencana_kerja"] = fieldModel.pembuatan_rencana_kerja != null ? fieldModel.pembuatan_rencana_kerja.Replace(",",".") : "";
                    data["pengendalian_kontrak"] = fieldModel.pengendalian_kontrak != null ? fieldModel.pengendalian_kontrak.Replace(",",".") : "";
                    data["pengendalian_dokumen"] = fieldModel.pengendalian_dokumen != null ? fieldModel.pengendalian_dokumen.Replace(",",".") : "";
                    data["pengendalian_produk"] = fieldModel.pengendalian_produk != null ? fieldModel.pengendalian_produk.Replace(",",".") : "";
                    data["keamanan"] = fieldModel.keamanan != null ? fieldModel.keamanan.Replace(",",".") : "";
                    data["pemantauan"] = fieldModel.pemantauan != null ? fieldModel.pemantauan.Replace(",",".") : "";
                    data["pelaporan_kekurangan"] = fieldModel.pelaporan_kekurangan != null ? fieldModel.pelaporan_kekurangan.Replace(",",".") : "";
                    data["pengelolaan_material"] = fieldModel.pengelolaan_material != null ? fieldModel.pengelolaan_material.Replace(",",".") : "";
                    data["pengumpulan_data"] = fieldModel.pengumpulan_data != null ? fieldModel.pengumpulan_data.Replace(",",".") : "";
                    data["pemeriksaan"] = fieldModel.pemeriksaan != null ? fieldModel.pemeriksaan.Replace(",",".") : "";
                    data["pengembangan_keterampilan"] = fieldModel.pengembangan_keterampilan != null ? fieldModel.pengembangan_keterampilan.Replace(",",".") : "";
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
                    result = AuditKriteriaSmk3Model.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(AuditKriteriaSmk3Model.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(AuditKriteriaSmk3Model.GetData(PkValue));
                    data_old["pembangunan_komitmen"] = data_old["pembangunan_komitmen"].ToString() != ""  ? data_old["pembangunan_komitmen"].ToString().Replace(",", ".") : "";
                    data_old["pembuatan_rencana_kerja"] = data_old["pembuatan_rencana_kerja"].ToString() != ""  ? data_old["pembuatan_rencana_kerja"].ToString().Replace(",", ".") : "";
                    data_old["pengendalian_kontrak"] = data_old["pengendalian_kontrak"].ToString() != ""  ? data_old["pengendalian_kontrak"].ToString().Replace(",", ".") : "";
                    data_old["pengendalian_dokumen"] = data_old["pengendalian_dokumen"].ToString() != ""  ? data_old["pengendalian_dokumen"].ToString().Replace(",", ".") : "";
                    data_old["pengendalian_produk"] = data_old["pengendalian_produk"].ToString() != ""  ? data_old["pengendalian_produk"].ToString().Replace(",", ".") : "";
                    data_old["keamanan"] = data_old["keamanan"].ToString() != ""  ? data_old["keamanan"].ToString().Replace(",", ".") : "";
                    data_old["pemantauan"] = data_old["pemantauan"].ToString() != ""  ? data_old["pemantauan"].ToString().Replace(",", ".") : "";
                    data_old["pelaporan_kekurangan"] = data_old["pelaporan_kekurangan"].ToString() != ""  ? data_old["pelaporan_kekurangan"].ToString().Replace(",", ".") : "";
                    data_old["pengelolaan_material"] = data_old["pengelolaan_material"].ToString() != ""  ? data_old["pengelolaan_material"].ToString().Replace(",", ".") : "";
                    data_old["pengumpulan_data"] = data_old["pengumpulan_data"].ToString() != ""  ? data_old["pengumpulan_data"].ToString().Replace(",", ".") : "";
                    data_old["pemeriksaan"] = data_old["pemeriksaan"].ToString() != ""  ? data_old["pemeriksaan"].ToString().Replace(",", ".") : "";
                    data_old["pengembangan_keterampilan"] = data_old["pengembangan_keterampilan"].ToString() != ""  ? data_old["pengembangan_keterampilan"].ToString().Replace(",", ".") : "";
                    data_old["total_internal"] = data_old["total_internal"].ToString() != ""  ? data_old["total_internal"].ToString().Replace(",", ".") : "";
                    data_old["total_standar"] = data_old["total_standar"].ToString() != ""  ? data_old["total_standar"].ToString().Replace(",", ".") : "";
                    data_old["presentasi_pencapaian"] = data_old["presentasi_pencapaian"].ToString() != ""  ? data_old["presentasi_pencapaian"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaSmk3Model.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["pembangunan_komitmen"] = fieldModel.pembangunan_komitmen != null ? fieldModel.pembangunan_komitmen.ToString().Replace(",", ".") : "";
                    data["pembuatan_rencana_kerja"] = fieldModel.pembuatan_rencana_kerja != null ? fieldModel.pembuatan_rencana_kerja.ToString().Replace(",", ".") : "";
                    data["pengendalian_kontrak"] = fieldModel.pengendalian_kontrak != null ? fieldModel.pengendalian_kontrak.ToString().Replace(",", ".") : "";
                    data["pengendalian_dokumen"] = fieldModel.pengendalian_dokumen != null ? fieldModel.pengendalian_dokumen.ToString().Replace(",", ".") : "";
                    data["pengendalian_produk"] = fieldModel.pengendalian_produk != null ? fieldModel.pengendalian_produk.ToString().Replace(",", ".") : "";
                    data["keamanan"] = fieldModel.keamanan != null ? fieldModel.keamanan.ToString().Replace(",", ".") : "";
                    data["pemantauan"] = fieldModel.pemantauan != null ? fieldModel.pemantauan.ToString().Replace(",", ".") : "";
                    data["pelaporan_kekurangan"] = fieldModel.pelaporan_kekurangan != null ? fieldModel.pelaporan_kekurangan.ToString().Replace(",", ".") : "";
                    data["pengelolaan_material"] = fieldModel.pengelolaan_material != null ? fieldModel.pengelolaan_material.ToString().Replace(",", ".") : "";
                    data["pengumpulan_data"] = fieldModel.pengumpulan_data != null ? fieldModel.pengumpulan_data.ToString().Replace(",", ".") : "";
                    data["pemeriksaan"] = fieldModel.pemeriksaan != null ? fieldModel.pemeriksaan.ToString().Replace(",", ".") : "";
                    data["pengembangan_keterampilan"] = fieldModel.pengembangan_keterampilan != null ? fieldModel.pengembangan_keterampilan.ToString().Replace(",", ".") : "";
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
                        result = AuditKriteriaSmk3Model.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaSmk3Model.GetData(PkValue));
						if (data != null)
						{
							result = AuditKriteriaSmk3Model.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = AuditKriteriaSmk3Model.LookupData(param);
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
            string str_pembangunan_komitmen = data["pembangunan_komitmen"].ToString();
            string str_pembuatan_rencana_kerja = data["pembuatan_rencana_kerja"].ToString();
            string str_pengendalian_kontrak = data["pengendalian_kontrak"].ToString();
            string str_pengendalian_dokumen = data["pengendalian_dokumen"].ToString();
            string str_pengendalian_produk = data["pengendalian_produk"].ToString();
            string str_keamanan = data["keamanan"].ToString();
            string str_pemantauan = data["pemantauan"].ToString();
            string str_pelaporan_kekurangan = data["pelaporan_kekurangan"].ToString();
            string str_pengelolaan_material = data["pengelolaan_material"].ToString();
            string str_pengumpulan_data = data["pengumpulan_data"].ToString();
            string str_pemeriksaan = data["pemeriksaan"].ToString();
            string str_pengembangan_keterampilan = data["pengembangan_keterampilan"].ToString();


            double pembangunan_komitmen = (str_pembangunan_komitmen == "" ? 0 : double.Parse(str_pembangunan_komitmen));
            double pembuatan_rencana_kerja = (str_pembuatan_rencana_kerja == "" ? 0 : double.Parse(str_pembuatan_rencana_kerja));
            double pengendalian_kontrak = (str_pengendalian_kontrak == "" ? 0 : double.Parse(str_pengendalian_kontrak));
            double pengendalian_dokumen = (str_pengendalian_dokumen == "" ? 0 : double.Parse(str_pengendalian_dokumen));
            double pengendalian_produk = (str_pengendalian_produk == "" ? 0 : double.Parse(str_pengendalian_produk));
            double keamanan = (str_keamanan == "" ? 0 : double.Parse(str_keamanan));
            double pemantauan = (str_pemantauan == "" ? 0 : double.Parse(str_pemantauan));
            double pelaporan_kekurangan = (str_pelaporan_kekurangan == "" ? 0 : double.Parse(str_pelaporan_kekurangan));
            double pengelolaan_material = (str_pengelolaan_material == "" ? 0 : double.Parse(str_pengelolaan_material));
            double pengumpulan_data = (str_pengumpulan_data == "" ? 0 : double.Parse(str_pengumpulan_data));
            double pemeriksaan = (str_pemeriksaan == "" ? 0 : double.Parse(str_pemeriksaan));
            double pengembangan_keterampilan = (str_pengembangan_keterampilan == "" ? 0 : double.Parse(str_pengembangan_keterampilan));


            double pembangunan_komitmen_std = 26;
            double pembuatan_rencana_kerja_std = 14;
            double pengendalian_kontrak_std = 8;
            double pengendalian_dokumen_std = 7;
            double pengendalian_produk_std = 9;
            double keamanan_std = 41;
            double pemantauan_std = 17;
            double pelaporan_kekurangan_std = 9;
            double pengelolaan_material_std = 12;
            double pengumpulan_data_std = 6;
            double pemeriksaan_std = 3;
            double pengembangan_keterampilan_std = 14;


            double pembangunan_komitmen_total = 0;
            double pembuatan_rencana_kerja_total = 0;
            double pengendalian_kontrak_total = 0;
            double pengendalian_dokumen_total = 0;
            double pengendalian_produk_total = 0;
            double keamanan_total = 0;
            double pemantauan_total = 0;
            double pelaporan_kekurangan_total = 0;
            double pengelolaan_material_total = 0;
            double pengumpulan_data_total = 0;
            double pemeriksaan_total = 0;
            double pengembangan_keterampilan_total = 0;


            pembangunan_komitmen_total = pembangunan_komitmen;// * pembangunan_komitmen_std;
            pembuatan_rencana_kerja_total =  pembuatan_rencana_kerja;// * pembuatan_rencana_kerja_std;
            pengendalian_kontrak_total =  pengendalian_kontrak;// * pengendalian_kontrak_std;
            pengendalian_dokumen_total =  pengendalian_dokumen;// * pengendalian_dokumen_std;
            pengendalian_produk_total =  pengendalian_produk;// * pengendalian_produk_std;
            keamanan_total =  keamanan;// * keamanan_std;
            pemantauan_total =  pemantauan;// * pemantauan_std;
            pelaporan_kekurangan_total =  pelaporan_kekurangan;// * pelaporan_kekurangan_std;
            pengelolaan_material_total =  pengelolaan_material;// * pengelolaan_material_std;
            pengumpulan_data_total =  pengumpulan_data;// * pengumpulan_data_std;
            pemeriksaan_total =  pemeriksaan;// * pemeriksaan_std;
            pengembangan_keterampilan_total = pengembangan_keterampilan;// * pengembangan_keterampilan_std;


            double total_internal =
                pembangunan_komitmen_total +
                pembuatan_rencana_kerja_total +
                pengendalian_kontrak_total +
                pengendalian_dokumen_total +
                pengendalian_produk_total +
                keamanan_total +
                pemantauan_total +
                pelaporan_kekurangan_total +
                pengelolaan_material_total +
                pengumpulan_data_total +
                pemeriksaan_total +
                pengembangan_keterampilan_total;
            data["total_internal"] = total_internal;



            double total_standar =
                pembangunan_komitmen_std +
                pembuatan_rencana_kerja_std +
                pengendalian_kontrak_std +
                pengendalian_dokumen_std +
                pengendalian_produk_std +
                keamanan_std +
                pemantauan_std +
                pelaporan_kekurangan_std +
                pengelolaan_material_std +
                pengumpulan_data_std +
                pemeriksaan_std +
                pengembangan_keterampilan_std;
            data["total_standar"] = total_standar;


            double presentasi_pencapaian = (total_internal / total_standar) * 100;
            data["presentasi_pencapaian"] = presentasi_pencapaian;


            int bendera_id = 0;
            if (85 <= presentasi_pencapaian & presentasi_pencapaian <= 100)
            {
                bendera_id = 1;
            }
            else if (60 <= presentasi_pencapaian & presentasi_pencapaian <= 84)
            {
                bendera_id = 2;
            }
            else if (presentasi_pencapaian <= 59)
            {
                bendera_id = 3;
            }
            data["nilai_bendera_id"] = bendera_id;
        }



    }
}