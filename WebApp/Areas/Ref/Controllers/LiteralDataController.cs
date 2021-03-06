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
using WebApp.Areas.Ref.Models;

namespace WebApp.Areas.Ref.Controllers
{

    [Area("Ref")]
    public class LiteralDataController : Controller
    {
        private string _rule_view = "LiteralDataView";
        private string _rule_add = "LiteralDataAdd";
        private string _rule_edit = "LiteralDataEdit";
        private string _rule_delete = "LiteralDataDelete";
        private string _path_controler = "/Ref/LiteralData/";
        private string _path_view = "/Areas/Ref/Views/LiteralData/";
        private readonly string _table_name = "ref_literal_data";
        private readonly string _table_title = "Literal Data";

        private static IHostingEnvironment _hostingEnvironment;
        public LiteralDataController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(LiteralDataModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = LiteralDataModel._pkKey;
                    ViewData["displayItem"] = LiteralDataModel.GetDisplayItem();
                    ViewData["column_att"] = LiteralDataModel.GetGridColumn();
                    ViewData["param"] = LiteralDataModel.GetListParam();
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
                    ViewData["filterColumn"] = filterColumn;
                    ViewData["curUserId"] = SecurityHelper.CurrentUserId(HttpContext);
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
        public JsonResult GetListData(LiteralDataModel.ActionRequest param)
        {
            GridData data = LiteralDataModel.GetListData(param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string cat_id, string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    LiteralDataModel.ViewModel fieldModel = new LiteralDataModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (cat_id != null && cat_id != "" && id != null && id != "")
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["cat_id"] = cat_id;
                        PkValue["id"] = id;
                        data = LiteralDataModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.cat_id = dr["cat_id"].ToString();
                                fieldModel.cat_id_old = dr["cat_id"].ToString();
                                fieldModel.cat_id_text = dr["cat_id_text"].ToString();
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.kode = dr["kode"].ToString();
                                fieldModel.nama = dr["nama"].ToString();
                                fieldModel.keterangan = dr["keterangan"].ToString();
                                fieldModel.ordinal = dr["ordinal"].ToString();
                                fieldModel.nilai_min = String.Format("{0:N2}", dr["nilai_min"]);
                                fieldModel.nilai_mak = String.Format("{0:N2}", dr["nilai_mak"]);
                                fieldModel.bg_color = dr["bg_color"].ToString();
                                fieldModel.font_color = dr["font_color"].ToString();
                                fieldModel.satuan = dr["satuan"].ToString();
                                fieldModel.status_id = dr["status_id"].ToString();
                                fieldModel.cat_kode = dr["cat_kode"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    LiteralKategoriModel.ViewModel KategoriModel = new LiteralKategoriModel.ViewModel();
                    if (fieldModel.cat_id != null && fieldModel.cat_id != "")
                    {
                        OrderedDictionary pkKategori = new OrderedDictionary();
                        pkKategori["id"] = fieldModel.cat_id;
                        DataTable dataCat = LiteralKategoriModel.GetViewData(pkKategori);
                        if (dataCat != null)
                        {
                            foreach (DataRow dr in dataCat.Rows)
                            {
                                KategoriModel.id = dr["id"].ToString();
                                KategoriModel.id_old = dr["id"].ToString();
                                KategoriModel.kode = dr["kode"].ToString();
                                KategoriModel.nama = dr["nama"].ToString();
                                KategoriModel.keterangan = dr["keterangan"].ToString();
                                KategoriModel.use_min_max = dr["use_min_max"].ToString();
                                KategoriModel.user_color_id = dr["user_color_id"].ToString();
                                KategoriModel.use_satuan = dr["use_satuan"].ToString();
                                KategoriModel.ordinal = dr["ordinal"].ToString();
                                KategoriModel.insert_by = dr["insert_by"].ToString();
                                KategoriModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                KategoriModel.update_by = dr["update_by"].ToString();
                                KategoriModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["KategoriModel"] = KategoriModel;
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["curUserId"] = SecurityHelper.CurrentUserId(HttpContext);
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
        public IActionResult Add(LiteralDataModel.ViewModel fieldModel)
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
                    //LiteralDataModel.ViewModel fieldModel = new LiteralDataModel.ViewModel();
                    fieldModel.cat_id = fieldModel.cat_id;
                    fieldModel.id = LiteralDataModel.GetNewId(fieldModel.cat_id).ToString();
                    fieldModel.ordinal = LiteralDataModel.GetNewOrdinal(fieldModel.cat_id).ToString();
                    fieldModel.status_id = "1";
                    ViewData["fieldModel"] = fieldModel;

                    LiteralKategoriModel.ViewModel KategoriModel = new LiteralKategoriModel.ViewModel();
                    if (fieldModel.cat_id != null && fieldModel.cat_id != "")
                    {
                        OrderedDictionary pkKategori = new OrderedDictionary();
                        pkKategori["id"] = fieldModel.cat_id;
                        DataTable dataCat = LiteralKategoriModel.GetViewData(pkKategori);
                        if (dataCat != null)
                        {
                            foreach (DataRow dr in dataCat.Rows)
                            {
                                KategoriModel.id = dr["id"].ToString();
                                KategoriModel.id_old = dr["id"].ToString();
                                KategoriModel.kode = dr["kode"].ToString();
                                KategoriModel.nama = dr["nama"].ToString();
                                KategoriModel.keterangan = dr["keterangan"].ToString();
                                KategoriModel.use_min_max = dr["use_min_max"].ToString();
                                KategoriModel.user_color_id = dr["user_color_id"].ToString();
                                KategoriModel.use_satuan = dr["use_satuan"].ToString();
                                KategoriModel.ordinal = dr["ordinal"].ToString();
                                KategoriModel.insert_by = dr["insert_by"].ToString();
                                KategoriModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                KategoriModel.update_by = dr["update_by"].ToString();
                                KategoriModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["KategoriModel"] = KategoriModel;
                    ViewData["curUserId"] = SecurityHelper.CurrentUserId(HttpContext);
                    return View(_path_view + "Add.cshtml");
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
        public IActionResult Copy(string cat_id, string id)
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
                    PkValue["cat_id"] = cat_id;
                    PkValue["id"] = id;
                    DataTable data = LiteralDataModel.GetViewData(PkValue);
                    LiteralDataModel.ViewModel fieldModel = new LiteralDataModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.cat_id = dr["cat_id"].ToString();
                        fieldModel.id = dr["id"].ToString(); //LiteralDataModel.GetNewId(dr["cat_id"].ToString()).ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.ordinal = dr["ordinal"].ToString();
                        fieldModel.nilai_min = dr["nilai_min"].ToString();
                        fieldModel.nilai_mak = dr["nilai_mak"].ToString();
                        fieldModel.bg_color = dr["bg_color"].ToString();
                        fieldModel.font_color = dr["font_color"].ToString();
                        fieldModel.satuan = dr["satuan"].ToString();
                        fieldModel.status_id = dr["status_id"].ToString();
                        fieldModel.cat_kode = dr["cat_kode"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.satuan = dr["satuan"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;

                    LiteralKategoriModel.ViewModel KategoriModel = new LiteralKategoriModel.ViewModel();
                    OrderedDictionary pkKategori = new OrderedDictionary();
                    if (fieldModel.cat_id != null && fieldModel.cat_id != "")
                    {
                        pkKategori["id"] = fieldModel.cat_id;
                        DataTable dataCat = LiteralKategoriModel.GetViewData(pkKategori);
                        if (dataCat != null)
                        {
                            foreach (DataRow dr in dataCat.Rows)
                            {
                                KategoriModel.id = dr["id"].ToString();
                                KategoriModel.id_old = dr["id"].ToString();
                                KategoriModel.kode = dr["kode"].ToString();
                                KategoriModel.nama = dr["nama"].ToString();
                                KategoriModel.keterangan = dr["keterangan"].ToString();
                                KategoriModel.use_min_max = dr["use_min_max"].ToString();
                                KategoriModel.user_color_id = dr["user_color_id"].ToString();
                                KategoriModel.use_satuan = dr["use_satuan"].ToString();
                                KategoriModel.ordinal = dr["ordinal"].ToString();
                                KategoriModel.insert_by = dr["insert_by"].ToString();
                                KategoriModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                KategoriModel.update_by = dr["update_by"].ToString();
                                KategoriModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["KategoriModel"] = KategoriModel;
                    ViewData["curUserId"] = SecurityHelper.CurrentUserId(HttpContext);
                    return View(_path_view + "Add.cshtml");
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
        public IActionResult Edit(string cat_id, string id)
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
                    PkValue["cat_id"] = cat_id;
                    PkValue["id"] = id;
                    DataTable data = LiteralDataModel.GetViewData(PkValue);
                    LiteralDataModel.ViewModel fieldModel = new LiteralDataModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.cat_id = dr["cat_id"].ToString();
                        fieldModel.cat_id_old = dr["cat_id"].ToString();
                        fieldModel.cat_id_text = dr["cat_id_text"].ToString();
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.ordinal = dr["ordinal"].ToString();
                        fieldModel.nilai_min = dr["nilai_min"].ToString();
                        fieldModel.nilai_mak = dr["nilai_mak"].ToString();
                        fieldModel.bg_color = dr["bg_color"].ToString();
                        fieldModel.font_color = dr["font_color"].ToString();
                        fieldModel.satuan = dr["satuan"].ToString();
                        fieldModel.status_id = dr["status_id"].ToString();
                        fieldModel.cat_kode = dr["cat_kode"].ToString();
                        fieldModel.insert_by = dr["insert_by"].ToString();
                        fieldModel.insert_at = dr["insert_at"].ToString();
                        fieldModel.update_by = dr["update_by"].ToString();
                        fieldModel.update_at = dr["update_at"].ToString();
                        fieldModel.satuan = dr["satuan"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;

                    LiteralKategoriModel.ViewModel KategoriModel = new LiteralKategoriModel.ViewModel();
                    if (fieldModel.cat_id != null && fieldModel.cat_id != "")
                    {
                        OrderedDictionary pkKategori = new OrderedDictionary();
                        pkKategori["id"] = fieldModel.cat_id;
                        DataTable dataCat = LiteralKategoriModel.GetViewData(pkKategori);
                        if (dataCat != null)
                        {
                            foreach (DataRow dr in dataCat.Rows)
                            {
                                KategoriModel.id = dr["id"].ToString();
                                KategoriModel.id_old = dr["id"].ToString();
                                KategoriModel.kode = dr["kode"].ToString();
                                KategoriModel.nama = dr["nama"].ToString();
                                KategoriModel.keterangan = dr["keterangan"].ToString();
                                KategoriModel.use_min_max = dr["use_min_max"].ToString();
                                KategoriModel.user_color_id = dr["user_color_id"].ToString();
                                KategoriModel.use_satuan = dr["use_satuan"].ToString();
                                KategoriModel.ordinal = dr["ordinal"].ToString();
                                KategoriModel.insert_by = dr["insert_by"].ToString();
                                KategoriModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                KategoriModel.update_by = dr["update_by"].ToString();
                                KategoriModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["KategoriModel"] = KategoriModel;
                    ViewData["curUserId"] = SecurityHelper.CurrentUserId(HttpContext);
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
        public JsonResult Insert(LiteralDataModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["cat_id"] = fieldModel.cat_id;
                    data["id"] = LiteralDataModel.GetNewId(fieldModel.cat_id).ToString(); //fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["ordinal"] = fieldModel.ordinal;
                    data["nilai_min"] = fieldModel.nilai_min != null ? fieldModel.nilai_min.Replace(",", ".") : "";
                    data["nilai_mak"] = fieldModel.nilai_mak != null ? fieldModel.nilai_mak.Replace(",", ".") : "";
                    data["bg_color"] = AntiXss.HtmlEncode(fieldModel.bg_color);
                    data["font_color"] = AntiXss.HtmlEncode(fieldModel.font_color);
                    data["satuan"] = AntiXss.HtmlEncode(fieldModel.satuan);
                    data["status_id"] = fieldModel.status_id;
                    data["cat_kode"] = AntiXss.HtmlEncode(fieldModel.cat_kode);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = LiteralDataModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(LiteralDataModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string cat_id_old = fieldModel.cat_id_old;
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["cat_id"] = cat_id_old;
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(LiteralDataModel.GetData(PkValue));
                    data_old["nilai_min"] = data_old["nilai_min"].ToString() != "" ? data_old["nilai_min"].ToString().Replace(",", ".") : "";
                    data_old["nilai_mak"] = data_old["nilai_mak"].ToString() != "" ? data_old["nilai_mak"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(LiteralDataModel.GetData(PkValue));
                    data["cat_id"] = fieldModel.cat_id;
                    data["id"] = fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["ordinal"] = fieldModel.ordinal;
                    data["nilai_min"] = fieldModel.nilai_min != null ? fieldModel.nilai_min.ToString().Replace(",", ".") : "";
                    data["nilai_mak"] = fieldModel.nilai_mak != null ? fieldModel.nilai_mak.ToString().Replace(",", ".") : "";
                    data["bg_color"] = AntiXss.HtmlEncode(fieldModel.bg_color);
                    data["font_color"] = AntiXss.HtmlEncode(fieldModel.font_color);
                    data["satuan"] = AntiXss.HtmlEncode(fieldModel.satuan);
                    data["status_id"] = fieldModel.status_id;
                    data["cat_kode"] = AntiXss.HtmlEncode(fieldModel.cat_kode);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = LiteralDataModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
        public JsonResult Delete(string cat_id, string id)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_delete))
                {
                    int jml = LiteralDataModel.GetCountDataUsed(_table_name, cat_id, id);
                    if (jml == 0)
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["cat_id"] = cat_id;
                        PkValue["id"] = id;
                        OrderedDictionary data = DataModel.DtToDictionary(LiteralDataModel.GetData(PkValue));
                        if (data != null)
                        {
                            result = LiteralDataModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = LiteralDataModel.LookupData(param);
            return Json(data);
        }
        [HttpPost]
        public JsonResult GetNewIdOrdinal(string cat_id)
        {
            //DataTable data = LiteralDataModel.LookupData(param);
            OrderedDictionary data = new OrderedDictionary();
            data["id"] = LiteralDataModel.GetNewId(cat_id);
            data["ordinal"] = LiteralDataModel.GetNewOrdinal(cat_id);
            return Json(data);
        }

        [HttpPost]
        public JsonResult LookupDataAuditKriteria(lookup_param param)
        {
            DataTable data = LiteralDataModel.LookupDataAuditKriteria(param);
            return Json(data);
        }

        public JsonResult LookupDataEHSPerformanceYear()
        {
            DataTable data = LiteralDataModel.LookupDataEHSPerformanceYear(HttpContext);
            return Json(data);
        }
        public JsonResult LookupDataEHSPerformanceMonth(string year)
        {
            DataTable data = LiteralDataModel.LookupDataEHSPerformanceMonth(HttpContext, year);
            return Json(data);
        }
    }
}