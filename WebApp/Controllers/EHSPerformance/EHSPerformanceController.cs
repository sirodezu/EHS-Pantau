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
    
    public class EHSPerformanceController : Controller
    {
        private string _rule_view = "DashboardEHSPerformance";
        private string _rule_add = "DashboardEHSPerformance";
        private string _rule_edit = "DashboardEHSPerformance";
        private string _rule_delete = "DashboardEHSPerformance";
        private string _rule_edit_all = "DashboardEHSPerformance";
        private string _rule_delete_all = "DashboardEHSPerformance";
        private string _path_controler = "/EHSPerformance/";
        private string _path_view = "/Views/EHSPerformance/EHSPerformance/";
        private readonly string _table_name = "ta_ehs_performance";
        private readonly string _table_title = "EHS Performance";

		private static IHostingEnvironment _hostingEnvironment;
        public EHSPerformanceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(EHSPerformanceModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = EHSPerformanceModel._pkKey;
                    ViewData["displayItem"] = EHSPerformanceModel.GetDisplayItem();
                    ViewData["column_att"] = EHSPerformanceModel.GetGridColumn();
                    ViewData["param"] = EHSPerformanceModel.GetListParam();
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
        public JsonResult GetListData(EHSPerformanceModel.ActionRequest param)
        {
            GridData data = EHSPerformanceModel.GetListData(param);
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
                    EHSPerformanceModel.ViewModel fieldModel = new EHSPerformanceModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = EHSPerformanceModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.kategori_filter = dr["kategori_filter"].ToString();
                                fieldModel.kategori_filter_text = dr["kategori_filter_text"].ToString();
                                fieldModel.periode0 = dr["periode0"].ToString();
                                fieldModel.periode0_text = dr["periode0_text"].ToString();
                                fieldModel.ehs_area_id0 = dr["ehs_area_id0"].ToString();
                                fieldModel.ehs_area_id0_text = dr["ehs_area_id0_text"].ToString();
                                fieldModel.ba_id0 = dr["ba_id0"].ToString();
                                fieldModel.ba_id0_text = dr["ba_id0_text"].ToString();
                                fieldModel.pa_id0 = dr["pa_id0"].ToString();
                                fieldModel.pa_id0_text = dr["pa_id0_text"].ToString();
                                fieldModel.psa_id0 = dr["psa_id0"].ToString();
                                fieldModel.psa_id0_text = dr["psa_id0_text"].ToString();
                                fieldModel.tahun0 = dr["tahun0"].ToString();
                                fieldModel.tahun0_text = dr["tahun0_text"].ToString();
                                fieldModel.bulan0 = dr["bulan0"].ToString();
                                fieldModel.bulan0_text = dr["bulan0_text"].ToString();
                                fieldModel.ehs_area_id1 = dr["ehs_area_id1"].ToString();
                                fieldModel.ehs_area_id1_text = dr["ehs_area_id1_text"].ToString();
                                fieldModel.ba_id1 = dr["ba_id1"].ToString();
                                fieldModel.ba_id1_text = dr["ba_id1_text"].ToString();
                                fieldModel.pa_id1 = dr["pa_id1"].ToString();
                                fieldModel.pa_id1_text = dr["pa_id1_text"].ToString();
                                fieldModel.psa_id1 = dr["psa_id1"].ToString();
                                fieldModel.psa_id1_text = dr["psa_id1_text"].ToString();
                                fieldModel.tahun1 = dr["tahun1"].ToString();
                                fieldModel.tahun1_text = dr["tahun1_text"].ToString();
                                fieldModel.bulan1 = dr["bulan1"].ToString();
                                fieldModel.bulan1_text = dr["bulan1_text"].ToString();
                                fieldModel.ehs_area_id2 = dr["ehs_area_id2"].ToString();
                                fieldModel.ehs_area_id2_text = dr["ehs_area_id2_text"].ToString();
                                fieldModel.ba_id2 = dr["ba_id2"].ToString();
                                fieldModel.ba_id2_text = dr["ba_id2_text"].ToString();
                                fieldModel.pa_id2 = dr["pa_id2"].ToString();
                                fieldModel.pa_id2_text = dr["pa_id2_text"].ToString();
                                fieldModel.psa_id2 = dr["psa_id2"].ToString();
                                fieldModel.psa_id2_text = dr["psa_id2_text"].ToString();
                                fieldModel.tahun2 = dr["tahun2"].ToString();
                                fieldModel.tahun2_text = dr["tahun2_text"].ToString();
                                fieldModel.bulan2 = dr["bulan2"].ToString();
                                fieldModel.bulan2_text = dr["bulan2_text"].ToString();
                                fieldModel.ehs_area_id3 = dr["ehs_area_id3"].ToString();
                                fieldModel.ehs_area_id3_text = dr["ehs_area_id3_text"].ToString();
                                fieldModel.ba_id3 = dr["ba_id3"].ToString();
                                fieldModel.ba_id3_text = dr["ba_id3_text"].ToString();
                                fieldModel.pa_id3 = dr["pa_id3"].ToString();
                                fieldModel.pa_id3_text = dr["pa_id3_text"].ToString();
                                fieldModel.psa_id3 = dr["psa_id3"].ToString();
                                fieldModel.psa_id3_text = dr["psa_id3_text"].ToString();
                                fieldModel.tahun3 = dr["tahun3"].ToString();
                                fieldModel.tahun3_text = dr["tahun3_text"].ToString();
                                fieldModel.bulan3 = dr["bulan3"].ToString();
                                fieldModel.bulan3_text = dr["bulan3_text"].ToString();
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
        public IActionResult Add(EHSPerformanceModel.ViewModel fieldModel)
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
                    //EHSPerformanceModel.ViewModel fieldModel = new EHSPerformanceModel.ViewModel();
                    fieldModel.id = EHSPerformanceModel.GetNewId().ToString();
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
                    DataTable data = EHSPerformanceModel.GetViewData(PkValue);
                    EHSPerformanceModel.ViewModel fieldModel = new EHSPerformanceModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = EHSPerformanceModel.GetNewId().ToString();
                        fieldModel.kategori_filter = dr["kategori_filter"].ToString();
                        fieldModel.periode0 = dr["periode0"].ToString();
                        fieldModel.ehs_area_id0 = dr["ehs_area_id0"].ToString();
                        fieldModel.ba_id0 = dr["ba_id0"].ToString();
                        fieldModel.pa_id0 = dr["pa_id0"].ToString();
                        fieldModel.psa_id0 = dr["psa_id0"].ToString();
                        fieldModel.tahun0 = dr["tahun0"].ToString();
                        fieldModel.bulan0 = dr["bulan0"].ToString();
                        fieldModel.ehs_area_id1 = dr["ehs_area_id1"].ToString();
                        fieldModel.ba_id1 = dr["ba_id1"].ToString();
                        fieldModel.pa_id1 = dr["pa_id1"].ToString();
                        fieldModel.psa_id1 = dr["psa_id1"].ToString();
                        fieldModel.tahun1 = dr["tahun1"].ToString();
                        fieldModel.bulan1 = dr["bulan1"].ToString();
                        fieldModel.ehs_area_id2 = dr["ehs_area_id2"].ToString();
                        fieldModel.ba_id2 = dr["ba_id2"].ToString();
                        fieldModel.pa_id2 = dr["pa_id2"].ToString();
                        fieldModel.psa_id2 = dr["psa_id2"].ToString();
                        fieldModel.tahun2 = dr["tahun2"].ToString();
                        fieldModel.bulan2 = dr["bulan2"].ToString();
                        fieldModel.ehs_area_id3 = dr["ehs_area_id3"].ToString();
                        fieldModel.ba_id3 = dr["ba_id3"].ToString();
                        fieldModel.pa_id3 = dr["pa_id3"].ToString();
                        fieldModel.psa_id3 = dr["psa_id3"].ToString();
                        fieldModel.tahun3 = dr["tahun3"].ToString();
                        fieldModel.bulan3 = dr["bulan3"].ToString();
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
                    DataTable data = EHSPerformanceModel.GetViewData(PkValue);
                    EHSPerformanceModel.ViewModel fieldModel = new EHSPerformanceModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.kategori_filter = dr["kategori_filter"].ToString();
                        fieldModel.kategori_filter_text = dr["kategori_filter_text"].ToString();
                        fieldModel.periode0 = dr["periode0"].ToString();
                        fieldModel.periode0_text = dr["periode0_text"].ToString();
                        fieldModel.ehs_area_id0 = dr["ehs_area_id0"].ToString();
                        fieldModel.ehs_area_id0_text = dr["ehs_area_id0_text"].ToString();
                        fieldModel.ba_id0 = dr["ba_id0"].ToString();
                        fieldModel.ba_id0_text = dr["ba_id0_text"].ToString();
                        fieldModel.pa_id0 = dr["pa_id0"].ToString();
                        fieldModel.pa_id0_text = dr["pa_id0_text"].ToString();
                        fieldModel.psa_id0 = dr["psa_id0"].ToString();
                        fieldModel.psa_id0_text = dr["psa_id0_text"].ToString();
                        fieldModel.tahun0 = dr["tahun0"].ToString();
                        fieldModel.tahun0_text = dr["tahun0_text"].ToString();
                        fieldModel.bulan0 = dr["bulan0"].ToString();
                        fieldModel.bulan0_text = dr["bulan0_text"].ToString();
                        fieldModel.ehs_area_id1 = dr["ehs_area_id1"].ToString();
                        fieldModel.ehs_area_id1_text = dr["ehs_area_id1_text"].ToString();
                        fieldModel.ba_id1 = dr["ba_id1"].ToString();
                        fieldModel.ba_id1_text = dr["ba_id1_text"].ToString();
                        fieldModel.pa_id1 = dr["pa_id1"].ToString();
                        fieldModel.pa_id1_text = dr["pa_id1_text"].ToString();
                        fieldModel.psa_id1 = dr["psa_id1"].ToString();
                        fieldModel.psa_id1_text = dr["psa_id1_text"].ToString();
                        fieldModel.tahun1 = dr["tahun1"].ToString();
                        fieldModel.tahun1_text = dr["tahun1_text"].ToString();
                        fieldModel.bulan1 = dr["bulan1"].ToString();
                        fieldModel.bulan1_text = dr["bulan1_text"].ToString();
                        fieldModel.ehs_area_id2 = dr["ehs_area_id2"].ToString();
                        fieldModel.ehs_area_id2_text = dr["ehs_area_id2_text"].ToString();
                        fieldModel.ba_id2 = dr["ba_id2"].ToString();
                        fieldModel.ba_id2_text = dr["ba_id2_text"].ToString();
                        fieldModel.pa_id2 = dr["pa_id2"].ToString();
                        fieldModel.pa_id2_text = dr["pa_id2_text"].ToString();
                        fieldModel.psa_id2 = dr["psa_id2"].ToString();
                        fieldModel.psa_id2_text = dr["psa_id2_text"].ToString();
                        fieldModel.tahun2 = dr["tahun2"].ToString();
                        fieldModel.tahun2_text = dr["tahun2_text"].ToString();
                        fieldModel.bulan2 = dr["bulan2"].ToString();
                        fieldModel.bulan2_text = dr["bulan2_text"].ToString();
                        fieldModel.ehs_area_id3 = dr["ehs_area_id3"].ToString();
                        fieldModel.ehs_area_id3_text = dr["ehs_area_id3_text"].ToString();
                        fieldModel.ba_id3 = dr["ba_id3"].ToString();
                        fieldModel.ba_id3_text = dr["ba_id3_text"].ToString();
                        fieldModel.pa_id3 = dr["pa_id3"].ToString();
                        fieldModel.pa_id3_text = dr["pa_id3_text"].ToString();
                        fieldModel.psa_id3 = dr["psa_id3"].ToString();
                        fieldModel.psa_id3_text = dr["psa_id3_text"].ToString();
                        fieldModel.tahun3 = dr["tahun3"].ToString();
                        fieldModel.tahun3_text = dr["tahun3_text"].ToString();
                        fieldModel.bulan3 = dr["bulan3"].ToString();
                        fieldModel.bulan3_text = dr["bulan3_text"].ToString();
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
        public JsonResult Insert(EHSPerformanceModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = EHSPerformanceModel.GetNewId().ToString();
                    data["kategori_filter"] = fieldModel.kategori_filter;
                    data["periode0"] = fieldModel.periode0;
                    data["ehs_area_id0"] = fieldModel.ehs_area_id0;
                    data["ba_id0"] = fieldModel.ba_id0;
                    data["pa_id0"] = fieldModel.pa_id0;
                    data["psa_id0"] = fieldModel.psa_id0;
                    data["tahun0"] = fieldModel.tahun0;
                    data["bulan0"] = fieldModel.bulan0;
                    data["ehs_area_id1"] = fieldModel.ehs_area_id1;
                    data["ba_id1"] = fieldModel.ba_id1;
                    data["pa_id1"] = fieldModel.pa_id1;
                    data["psa_id1"] = fieldModel.psa_id1;
                    data["tahun1"] = fieldModel.tahun1;
                    data["bulan1"] = fieldModel.bulan1;
                    data["ehs_area_id2"] = fieldModel.ehs_area_id2;
                    data["ba_id2"] = fieldModel.ba_id2;
                    data["pa_id2"] = fieldModel.pa_id2;
                    data["psa_id2"] = fieldModel.psa_id2;
                    data["tahun2"] = fieldModel.tahun2;
                    data["bulan2"] = fieldModel.bulan2;
                    data["ehs_area_id3"] = fieldModel.ehs_area_id3;
                    data["ba_id3"] = fieldModel.ba_id3;
                    data["pa_id3"] = fieldModel.pa_id3;
                    data["psa_id3"] = fieldModel.psa_id3;
                    data["tahun3"] = fieldModel.tahun3;
                    data["bulan3"] = fieldModel.bulan3;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = EHSPerformanceModel.Insert(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(EHSPerformanceModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(EHSPerformanceModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(EHSPerformanceModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["kategori_filter"] = fieldModel.kategori_filter;
                    data["periode0"] = fieldModel.periode0;
                    data["ehs_area_id0"] = fieldModel.ehs_area_id0;
                    data["ba_id0"] = fieldModel.ba_id0;
                    data["pa_id0"] = fieldModel.pa_id0;
                    data["psa_id0"] = fieldModel.psa_id0;
                    data["tahun0"] = fieldModel.tahun0;
                    data["bulan0"] = fieldModel.bulan0;
                    data["ehs_area_id1"] = fieldModel.ehs_area_id1;
                    data["ba_id1"] = fieldModel.ba_id1;
                    data["pa_id1"] = fieldModel.pa_id1;
                    data["psa_id1"] = fieldModel.psa_id1;
                    data["tahun1"] = fieldModel.tahun1;
                    data["bulan1"] = fieldModel.bulan1;
                    data["ehs_area_id2"] = fieldModel.ehs_area_id2;
                    data["ba_id2"] = fieldModel.ba_id2;
                    data["pa_id2"] = fieldModel.pa_id2;
                    data["psa_id2"] = fieldModel.psa_id2;
                    data["tahun2"] = fieldModel.tahun2;
                    data["bulan2"] = fieldModel.bulan2;
                    data["ehs_area_id3"] = fieldModel.ehs_area_id3;
                    data["ba_id3"] = fieldModel.ba_id3;
                    data["pa_id3"] = fieldModel.pa_id3;
                    data["psa_id3"] = fieldModel.psa_id3;
                    data["tahun3"] = fieldModel.tahun3;
                    data["bulan3"] = fieldModel.bulan3;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {

                        result = EHSPerformanceModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(EHSPerformanceModel.GetData(PkValue));
						if (data != null)
						{
							result = EHSPerformanceModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = EHSPerformanceModel.LookupData(param);
            return Json(data);
        }

        [HttpPost]
        //public JsonResult GetValidData(
        //    string cur_year_1,
        //    string cur_month_1,
        //    string ehs_area_id_1,
        //    string ba_id_1,
        //    string pa_id_1,
        //    string psa_id_1,
        //    string cur_year_2,
        //    string cur_month_2,
        //    string ehs_area_id_2,
        //    string ba_id_2,
        //    string pa_id_2,
        //    string psa_id_2,
        //    string cur_year_3,
        //    string cur_month_3,
        //    string ehs_area_id_3,
        //    string ba_id_3,
        //    string pa_id_3,
        //    string psa_id_3
        //)
        //{
        //    DataSet data =
        //        EHSPerformanceModel.GetValidData(
        //            cur_year_1,
        //            cur_month_1,
        //            ehs_area_id_1,
        //            ba_id_1,
        //            pa_id_1,
        //            psa_id_1,
        //            cur_year_2,
        //            cur_month_2,
        //            ehs_area_id_2,
        //            ba_id_2,
        //            pa_id_2,
        //            psa_id_2,
        //            cur_year_3,
        //            cur_month_3,
        //            ehs_area_id_3,
        //            ba_id_3,
        //            pa_id_3,
        //            psa_id_3
        //        );
        //    return Json(data);
        //}

        public JsonResult GetValidData(string cur_year, string cur_month, string ehs_area_id, string ba_id, string pa_id, string psa_id)
        {
            DataSet data = EHSPerformanceModel.GetValidData(cur_year, cur_month, ehs_area_id, ba_id, pa_id, psa_id);
            return Json(data);
        }

    }
}