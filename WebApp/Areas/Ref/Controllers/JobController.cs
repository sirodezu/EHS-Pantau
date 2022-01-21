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
    public class JobController : Controller
    {
        private string _rule_view = "OrganizationView";
        private string _rule_add = "OrganizationAdd";
        private string _rule_edit = "OrganizationEdit";
        private string _rule_delete = "OrganizationDelete";
        private string _path_controler = "/Ref/Job/";
        private string _path_view = "/Areas/Ref/Views/Job/";
        private readonly string _table_name = "ref_job";
        private readonly string _table_title = "Job";

		private static IHostingEnvironment _hostingEnvironment;
        public JobController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(JobModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = JobModel._pkKey;
                    ViewData["displayItem"] = JobModel.GetDisplayItem();
                    ViewData["column_att"] = JobModel.GetGridColumn();
                    ViewData["param"] = JobModel.GetListParam();
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
        public JsonResult GetListData(JobModel.ActionRequest param)
        {
            GridData data = JobModel.GetListData(param);
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
                    JobModel.ViewModel fieldModel = new JobModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = JobModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.kode = dr["kode"].ToString();
                                fieldModel.nama = dr["nama"].ToString();
                                fieldModel.keterangan = dr["keterangan"].ToString();
                                fieldModel.status_id = dr["status_id"].ToString();
                                fieldModel.status_id_text = dr["status_id_text"].ToString();
                                fieldModel.start_date = dr["start_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["start_date"]) : "";
                                fieldModel.end_date = dr["end_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["end_date"]) : "";
                                fieldModel.sap_otype = dr["sap_otype"].ToString();
                                fieldModel.sap_objid = dr["sap_objid"].ToString();
                                fieldModel.sap_otjid = dr["sap_otjid"].ToString();
                                fieldModel.sap_short = dr["sap_short"].ToString();
                                fieldModel.sap_stext = dr["sap_stext"].ToString();
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
        public IActionResult Add(JobModel.ViewModel fieldModel)
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
                    //JobModel.ViewModel fieldModel = new JobModel.ViewModel();
                    fieldModel.id = JobModel.GetNewId().ToString();
                    fieldModel.status_id = "1";
                    fieldModel.end_date = "9999-12-31";
                    fieldModel.dt_end_date = String.Format("{0:dd/MM/yyyy}", DateTime.Parse("9999-12-31"));
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
                    DataTable data = JobModel.GetViewData(PkValue);
                    JobModel.ViewModel fieldModel = new JobModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = JobModel.GetNewId().ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.status_id = dr["status_id"].ToString();
                        fieldModel.start_date = dr["start_date"].ToString();
                        fieldModel.end_date = dr["end_date"].ToString();
                        fieldModel.sap_otype = dr["sap_otype"].ToString();
                        fieldModel.sap_objid = dr["sap_objid"].ToString();
                        fieldModel.sap_otjid = dr["sap_otjid"].ToString();
                        fieldModel.sap_short = dr["sap_short"].ToString();
                        fieldModel.sap_stext = dr["sap_stext"].ToString();
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
                    DataTable data = JobModel.GetViewData(PkValue);
                    JobModel.ViewModel fieldModel = new JobModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.status_id = dr["status_id"].ToString();
                        fieldModel.status_id_text = dr["status_id_text"].ToString();
                        fieldModel.start_date = dr["start_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["start_date"]) : "";
                        fieldModel.dt_start_date = dr["start_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["start_date"]) : "";
                        fieldModel.end_date = dr["end_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["end_date"]) : "";
                        fieldModel.dt_end_date = dr["end_date"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["end_date"]) : "";
                        fieldModel.sap_otype = dr["sap_otype"].ToString();
                        fieldModel.sap_objid = dr["sap_objid"].ToString();
                        fieldModel.sap_otjid = dr["sap_otjid"].ToString();
                        fieldModel.sap_short = dr["sap_short"].ToString();
                        fieldModel.sap_stext = dr["sap_stext"].ToString();
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
        public JsonResult Insert(JobModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = JobModel.GetNewId().ToString();
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["status_id"] = fieldModel.status_id;
                    data["start_date"] = fieldModel.start_date;
                    data["end_date"] = fieldModel.end_date;
                    data["sap_otype"] = AntiXss.HtmlEncode(fieldModel.sap_otype);
                    data["sap_objid"] = fieldModel.sap_objid;
                    data["sap_otjid"] = AntiXss.HtmlEncode(fieldModel.sap_otjid);
                    data["sap_short"] = AntiXss.HtmlEncode(fieldModel.sap_short);
                    data["sap_stext"] = AntiXss.HtmlEncode(fieldModel.sap_stext);
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = JobModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(JobModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(JobModel.GetData(PkValue));
                    data_old["start_date"] = data_old["start_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["start_date"]) : "";
                    data_old["end_date"] = data_old["end_date"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["end_date"]) : "";
                    OrderedDictionary data = DataModel.DtToDictionary(JobModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["status_id"] = fieldModel.status_id;
                    data["start_date"] = fieldModel.start_date;
                    data["end_date"] = fieldModel.end_date;
                    data["sap_otype"] = AntiXss.HtmlEncode(fieldModel.sap_otype);
                    data["sap_objid"] = fieldModel.sap_objid;
                    data["sap_otjid"] = AntiXss.HtmlEncode(fieldModel.sap_otjid);
                    data["sap_short"] = AntiXss.HtmlEncode(fieldModel.sap_short);
                    data["sap_stext"] = AntiXss.HtmlEncode(fieldModel.sap_stext);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = JobModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(JobModel.GetData(PkValue));
						if (data != null)
						{
							result = JobModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = JobModel.LookupData(param);
            return Json(data);
        }

    }
}