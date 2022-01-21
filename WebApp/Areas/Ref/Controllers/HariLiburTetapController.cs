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
using WebApp.Areas.Ref.Models;
using Microsoft.Security.Application;

namespace WebApp.Areas.Ref.Controllers
{
    
    [Area("Ref")]
    public class HariLiburTetapController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "HariLiburManage";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "HariLiburManage";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "HariLiburManage";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "HariLiburManage";
        private string _path_controler = "/Ref/HariLiburTetap/";
        private string _path_view = "/Areas/Ref/Views/HariLiburTetap/";
        private readonly string _table_name = "ref_hari_libur_tetap";
        private readonly string _table_title = "Hari Libur Tetap";

		private static IHostingEnvironment _hostingEnvironment;
        public HariLiburTetapController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(HariLiburTetapModel.ViewModel filterColumn)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ListTitle", "Daftar " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["_table_name"] = _table_name;
                    ViewData["_table_title"] = _table_title;
                    ViewData["pkKey"] = HariLiburTetapModel._pkKey;
                    ViewData["displayItem"] = HariLiburTetapModel.GetDisplayItem();
                    ViewData["column_att"] = HariLiburTetapModel.GetGridColumn();
                    ViewData["param"] = HariLiburTetapModel.GetListParam();
                    ViewData["UrlGetList"] = baseUrl + _path_controler + "GetListData";
                    ViewData["UrlView"] = baseUrl + _path_controler + "ViewItem";
                    ViewData["UrlAdd"] = baseUrl + _path_controler + "Add";
                    ViewData["UrlEdit"] = baseUrl + _path_controler + "Edit";
                    ViewData["UrlCopy"] = baseUrl + _path_controler + "Copy";
                    ViewData["UrlDelete"] = baseUrl + _path_controler + "Delete";
                    ViewData["AllowView"] = HttpContext.Session.GetString(_rule_view)!=null ? "1":"0";
                    ViewData["AllowAdd"] = HttpContext.Session.GetString(_rule_add) != null ? "1" : "0";
                    ViewData["AllowEdit"] = HttpContext.Session.GetString(_rule_edit) != null ? "1" : "0";
                    ViewData["AllowDelete"] = HttpContext.Session.GetString(_rule_delete) != null ? "1" : "0";
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
        public JsonResult GetListData(HariLiburTetapModel.ActionRequest param)
        {
            GridData data = HariLiburTetapModel.GetListData(param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string bulan_tanggal)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    HariLiburTetapModel.ViewModel fieldModel = new HariLiburTetapModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (bulan_tanggal != null && bulan_tanggal !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["bulan_tanggal"] = bulan_tanggal;
                        data = HariLiburTetapModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.bulan_tanggal = dr["bulan_tanggal"].ToString();
                                fieldModel.bulan_tanggal_old = dr["bulan_tanggal"].ToString();
                                fieldModel.bulan_id = dr["bulan_id"].ToString();
                                fieldModel.bulan_id_text = dr["bulan_id_text"].ToString();
                                fieldModel.tanggal = dr["tanggal"].ToString();
                                fieldModel.keterangan = dr["keterangan"].ToString();
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
        public IActionResult Add(HariLiburTetapModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    //HariLiburTetapModel.ViewModel fieldModel = new HariLiburTetapModel.ViewModel();
                    //fieldModel.bulan_tanggal = HariLiburTetapModel.GetNewId().ToString();
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
        public IActionResult Copy(string bulan_tanggal)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    var PkValue = new OrderedDictionary();
                    PkValue["bulan_tanggal"] = bulan_tanggal;
                    DataTable data = HariLiburTetapModel.GetViewData(PkValue);
                    HariLiburTetapModel.ViewModel fieldModel = new HariLiburTetapModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        //fieldModel.bulan_tanggal = HariLiburTetapModel.GetNewId().ToString();
                        fieldModel.bulan_id = dr["bulan_id"].ToString();
                        fieldModel.tanggal = dr["tanggal"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
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
        public IActionResult Edit(string bulan_tanggal)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["bulan_tanggal"] = bulan_tanggal;
                    DataTable data = HariLiburTetapModel.GetViewData(PkValue);
                    HariLiburTetapModel.ViewModel fieldModel = new HariLiburTetapModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.bulan_tanggal = dr["bulan_tanggal"].ToString();
                        fieldModel.bulan_tanggal_old = dr["bulan_tanggal"].ToString();
                        fieldModel.bulan_id = dr["bulan_id"].ToString();
                        fieldModel.bulan_id_text = dr["bulan_id_text"].ToString();
                        fieldModel.tanggal = dr["tanggal"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
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
        public JsonResult Insert(HariLiburTetapModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["bulan_tanggal"] = fieldModel.bulan_id.Length == 1 ? "0"+fieldModel.bulan_id : fieldModel.bulan_id;
                    data["bulan_tanggal"] += "-";
                    data["bulan_tanggal"] += fieldModel.tanggal.Length == 1 ? "0" + fieldModel.tanggal : fieldModel.tanggal;
                    data["bulan_id"] = fieldModel.bulan_id;
                    data["tanggal"] = fieldModel.tanggal;
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = HariLiburTetapModel.Insert(_hostingEnvironment, HttpContext, data);
                    HariLiburModel.InitHariLibur(HttpContext);
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
        public JsonResult Update(HariLiburTetapModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string bulan_tanggal_old = fieldModel.bulan_tanggal_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["bulan_tanggal"] = bulan_tanggal_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(HariLiburTetapModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(HariLiburTetapModel.GetData(PkValue));
                    data["bulan_tanggal"] = fieldModel.bulan_id.Length == 1 ? "0" + fieldModel.bulan_id : fieldModel.bulan_id;
                    data["bulan_tanggal"] += "-";
                    data["bulan_tanggal"] += fieldModel.tanggal.Length == 1 ? "0" + fieldModel.tanggal : fieldModel.tanggal;
                    data["bulan_id"] = fieldModel.bulan_id;
                    data["tanggal"] = fieldModel.tanggal;
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = HariLiburTetapModel.Update(_hostingEnvironment, HttpContext, data, data_old);
                        HariLiburModel.InitHariLibur(HttpContext);
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
        public JsonResult Delete(string bulan_tanggal)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
					int jml = DataModel.GetCountDataUsed(_table_name, bulan_tanggal);
                    if (jml == 0) {
						var PkValue = new OrderedDictionary();
						PkValue["bulan_tanggal"] = bulan_tanggal;
						OrderedDictionary data = DataModel.DtToDictionary(HariLiburTetapModel.GetData(PkValue));
						if (data != null)
						{
							result = HariLiburTetapModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = HariLiburTetapModel.LookupData(param);
            return Json(data);
        }

    }
}