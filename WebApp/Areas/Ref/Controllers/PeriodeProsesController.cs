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
    public class PeriodeProsesController : Controller
    {
        private string _rule_view = "GlobalConfiguration";
        private string _rule_add = "GlobalConfiguration";
        private string _rule_edit = "GlobalConfiguration";
        private string _rule_delete = "GlobalConfiguration";
        private string _path_controler = "/Ref/PeriodeProses/";
        private string _path_view = "/Areas/Ref/Views/PeriodeProses/";
        private readonly string _table_name = "ref_periode_proses";
        private readonly string _table_title = "Periode";

		private static IHostingEnvironment _hostingEnvironment;
        public PeriodeProsesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(PeriodeProsesModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = PeriodeProsesModel._pkKey;
                    ViewData["displayItem"] = PeriodeProsesModel.GetDisplayItem();
                    ViewData["column_att"] = PeriodeProsesModel.GetGridColumn();
                    ViewData["param"] = PeriodeProsesModel.GetListParam();
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
        public JsonResult GetListData(PeriodeProsesModel.ActionRequest param)
        {
            GridData data = PeriodeProsesModel.GetListData(param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string tahun, string proses_id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    PeriodeProsesModel.ViewModel fieldModel = new PeriodeProsesModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (tahun != null && tahun !=""  && proses_id != null && proses_id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["tahun"] = tahun;
                        PkValue["proses_id"] = proses_id;
                        data = PeriodeProsesModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.tahun = dr["tahun"].ToString();
                                fieldModel.tahun_old = dr["tahun"].ToString();
                                fieldModel.proses_id = dr["proses_id"].ToString();
                                fieldModel.proses_id_old = dr["proses_id"].ToString();
                                fieldModel.proses_id_text = dr["proses_id_text"].ToString();
                                fieldModel.start_date = dr["start_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["start_date"]) : "";
                                fieldModel.end_date = dr["end_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["end_date"]) : "";
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
        public IActionResult Add(PeriodeProsesModel.ViewModel fieldModel)
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
                    //PeriodeProsesModel.ViewModel fieldModel = new PeriodeProsesModel.ViewModel();
                    fieldModel.tahun = fieldModel.tahun;
                    fieldModel.proses_id = PeriodeProsesModel.GetNewId().ToString();
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
        public IActionResult Copy(string tahun, string proses_id)
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
                    PkValue["tahun"] = tahun;
                    PkValue["proses_id"] = proses_id;
                    DataTable data = PeriodeProsesModel.GetViewData(PkValue);
                    PeriodeProsesModel.ViewModel fieldModel = new PeriodeProsesModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.proses_id = PeriodeProsesModel.GetNewId().ToString();
                        fieldModel.start_date = dr["start_date"].ToString();
                        fieldModel.end_date = dr["end_date"].ToString();
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
        public IActionResult Edit(string tahun, string proses_id)
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
                    PkValue["tahun"] = tahun;
                    PkValue["proses_id"] = proses_id;
                    DataTable data = PeriodeProsesModel.GetViewData(PkValue);
                    PeriodeProsesModel.ViewModel fieldModel = new PeriodeProsesModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.tahun = dr["tahun"].ToString();
                        fieldModel.tahun_old = dr["tahun"].ToString();
                        fieldModel.proses_id = dr["proses_id"].ToString();
                        fieldModel.proses_id_old = dr["proses_id"].ToString();
                        fieldModel.proses_id_text = dr["proses_id_text"].ToString();
                        fieldModel.start_date = dr["start_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["start_date"]) : "";
                        fieldModel.dt_start_date = dr["start_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["start_date"]) : "";
                        fieldModel.end_date = dr["end_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["end_date"]) : "";
                        fieldModel.dt_end_date = dr["end_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["end_date"]) : "";
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
        public JsonResult Insert(PeriodeProsesModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["tahun"] = fieldModel.tahun;
                    data["proses_id"] = PeriodeProsesModel.GetNewId().ToString();
                    data["start_date"] = fieldModel.start_date;
                    data["end_date"] = fieldModel.end_date;
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = PeriodeProsesModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(PeriodeProsesModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string tahun_old = fieldModel.tahun_old;
                    string proses_id_old = fieldModel.proses_id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["tahun"] = tahun_old;
                    PkValue["proses_id"] = proses_id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(PeriodeProsesModel.GetData(PkValue));
                    data_old["start_date"] = data_old["start_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["start_date"]) : "";
                    data_old["end_date"] = data_old["end_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["end_date"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(PeriodeProsesModel.GetData(PkValue));
                    data["tahun"] = fieldModel.tahun;
                    data["proses_id"] = fieldModel.proses_id;
                    data["start_date"] = fieldModel.start_date;
                    data["end_date"] = fieldModel.end_date;
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = PeriodeProsesModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
        public JsonResult Delete(string tahun, string proses_id)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_delete))
                {
					int jml = DataModel.GetCountDataUsed(_table_name, proses_id);
                    if (jml == 0) {
						var PkValue = new OrderedDictionary();
						PkValue["tahun"] = tahun;
						PkValue["proses_id"] = proses_id;
						OrderedDictionary data = DataModel.DtToDictionary(PeriodeProsesModel.GetData(PkValue));
						if (data != null)
						{
							result = PeriodeProsesModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = PeriodeProsesModel.LookupData(param);
            return Json(data);
        }

    }
}