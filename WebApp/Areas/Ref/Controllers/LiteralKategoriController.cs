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
    public class LiteralKategoriController : Controller
    {
        private string _rule_view = "LiteralDataView";
        private string _rule_add = "LiteralDataAdd";
        private string _rule_edit = "LiteralDataEdit";
        private string _rule_delete = "LiteralDataDelete";
        private string _path_controler = "/Ref/LiteralKategori/";
        private string _path_view = "/Areas/Ref/Views/LiteralKategori/";
        private readonly string _table_name = "ref_literal_kategori";
        private readonly string _table_title = "Kategori Referensi";

		private static IHostingEnvironment _hostingEnvironment;
        public LiteralKategoriController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(LiteralKategoriModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = LiteralKategoriModel._pkKey;
                    ViewData["displayItem"] = LiteralKategoriModel.GetDisplayItem();
                    ViewData["column_att"] = LiteralKategoriModel.GetGridColumn();
                    ViewData["param"] = LiteralKategoriModel.GetListParam();
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
        public JsonResult GetListData(LiteralKategoriModel.ActionRequest param)
        {
            GridData data = LiteralKategoriModel.GetListData(param);
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
                    LiteralKategoriModel.ViewModel fieldModel = new LiteralKategoriModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = LiteralKategoriModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.kode = dr["kode"].ToString();
                                fieldModel.nama = dr["nama"].ToString();
                                fieldModel.keterangan = dr["keterangan"].ToString();
                                fieldModel.use_min_max = dr["use_min_max"].ToString();
                                fieldModel.use_min_max_text = dr["use_min_max"].ToString() == "1" ? "Ya" : "Tidak";
                                fieldModel.user_color_id = dr["user_color_id"].ToString();
                                fieldModel.user_color_id_text = dr["user_color_id"].ToString() == "1" ? "Ya" : "Tidak";
                                fieldModel.use_satuan = dr["use_satuan"].ToString();
                                fieldModel.use_satuan_text = dr["use_satuan"].ToString() == "1" ? "Ya" : "Tidak";
                                fieldModel.allow_add = dr["allow_add"].ToString();
                                fieldModel.allow_add_text = dr["allow_add"].ToString()=="1"?"Ya":"Tidak";
                                fieldModel.ordinal = dr["ordinal"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
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
        public IActionResult Add(LiteralKategoriModel.ViewModel fieldModel)
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
                    //LiteralKategoriModel.ViewModel fieldModel = new LiteralKategoriModel.ViewModel();
                    fieldModel.id = LiteralKategoriModel.GetNewId().ToString();
                    fieldModel.use_min_max = "0";
                    fieldModel.user_color_id = "0";
                    fieldModel.allow_add = "0";
                    fieldModel.ordinal = LiteralKategoriModel.GetNewOrdinal().ToString();
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
                    DataTable data = LiteralKategoriModel.GetViewData(PkValue);
                    LiteralKategoriModel.ViewModel fieldModel = new LiteralKategoriModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = LiteralKategoriModel.GetNewId().ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.use_min_max = dr["use_min_max"].ToString();
                        fieldModel.use_min_max_text = dr["use_min_max"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.user_color_id = dr["user_color_id"].ToString();
                        fieldModel.user_color_id_text = dr["user_color_id"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.use_satuan = dr["use_satuan"].ToString();
                        fieldModel.use_satuan_text = dr["use_satuan"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.allow_add = dr["allow_add"].ToString();
                        fieldModel.allow_add_text = dr["allow_add"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.ordinal = dr["ordinal"].ToString();
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
                    DataTable data = LiteralKategoriModel.GetViewData(PkValue);
                    LiteralKategoriModel.ViewModel fieldModel = new LiteralKategoriModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.use_min_max = dr["use_min_max"].ToString();
                        fieldModel.use_min_max_text = dr["use_min_max"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.user_color_id = dr["user_color_id"].ToString();
                        fieldModel.user_color_id_text = dr["user_color_id"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.use_satuan = dr["use_satuan"].ToString();
                        fieldModel.use_satuan_text = dr["use_satuan"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.allow_add = dr["allow_add"].ToString();
                        fieldModel.allow_add_text = dr["allow_add"].ToString() == "1" ? "Ya" : "Tidak";
                        fieldModel.ordinal = dr["ordinal"].ToString();
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
        public JsonResult Insert(LiteralKategoriModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = fieldModel.id;// LiteralKategoriModel.GetNewId().ToString();
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["use_min_max"] = fieldModel.use_min_max;
                    data["user_color_id"] = fieldModel.user_color_id;
                    data["use_satuan"] = fieldModel.use_satuan;
                    data["allow_add"] = fieldModel.allow_add;
                    data["ordinal"] = fieldModel.ordinal;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = LiteralKategoriModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(LiteralKategoriModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(LiteralKategoriModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(LiteralKategoriModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["use_min_max"] = fieldModel.use_min_max;
                    data["user_color_id"] = fieldModel.user_color_id;
                    data["use_satuan"] = fieldModel.use_satuan;
                    data["allow_add"] = fieldModel.allow_add;
                    data["ordinal"] = fieldModel.ordinal;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = LiteralKategoriModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(LiteralKategoriModel.GetData(PkValue));
						if (data != null)
						{
							result = LiteralKategoriModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = LiteralKategoriModel.LookupData(param);
            return Json(data);
        }

    }
}