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
    
    public class AuditKriteriaController : Controller
    {
        private string _rule_view = "AuditView";
        private string _rule_add = "AuditAdd";
        private string _rule_edit = "AuditEdit";
        private string _rule_delete = "AuditDelete";
        private string _rule_edit_all = "AuditEditAll";
        private string _rule_delete_all = "AuditDeleteAll";
        private string _path_controler = "/AuditKriteria/";
        private string _path_view = "/Views/Audit/AuditKriteria/";
        private readonly string _table_name = "ta_audit_kriteria";
        private readonly string _table_title = "Kriteria Audit ";

		private static IHostingEnvironment _hostingEnvironment;
        public AuditKriteriaController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(AuditKriteriaModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = AuditKriteriaModel._pkKey;
                    ViewData["displayItem"] = AuditKriteriaModel.GetDisplayItem();
                    ViewData["column_att"] = AuditKriteriaModel.GetGridColumn();
                    ViewData["param"] = AuditKriteriaModel.GetListParam();
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
        public JsonResult GetListData(AuditKriteriaModel.ActionRequest param)
        {
            GridData data = AuditKriteriaModel.GetListData(param);
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
                    AuditKriteriaModel.ViewModel fieldModel = new AuditKriteriaModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = AuditKriteriaModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.audit_id = dr["audit_id"].ToString();
                                fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                                fieldModel.kriteria_audit_id_text = dr["kriteria_audit_id_text"].ToString();
                                fieldModel.jumlah_iso_14001_2015 = dr["jumlah_iso_14001_2015"].ToString();
                                fieldModel.jumlah_iso_45001_2018 = dr["jumlah_iso_45001_2018"].ToString();
                                fieldModel.insert_by = dr["insert_by"].ToString();
                                fieldModel.insert_at = dr["insert_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["insert_at"]) : "";
                                fieldModel.update_by = dr["update_by"].ToString();
                                fieldModel.update_at = dr["update_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["update_at"]) : "";
                            }
                        }
                    }
                    ViewData["fieldModel"] = fieldModel;
                    ViewData["audit_id"] = fieldModel.audit_id;
                    ViewData["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
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
        public IActionResult Add(AuditKriteriaModel.ViewModel fieldModel)
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
                    //AuditKriteriaModel.ViewModel fieldModel = new AuditKriteriaModel.ViewModel();
                    fieldModel.id = AuditKriteriaModel.GetNewId().ToString();
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
                    DataTable data = AuditKriteriaModel.GetViewData(PkValue);
                    AuditKriteriaModel.ViewModel fieldModel = new AuditKriteriaModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = AuditKriteriaModel.GetNewId().ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.jumlah_iso_14001_2015 = dr["jumlah_iso_14001_2015"].ToString();
                        fieldModel.jumlah_iso_45001_2018 = dr["jumlah_iso_45001_2018"].ToString();
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
                    DataTable data = AuditKriteriaModel.GetViewData(PkValue);
                    AuditKriteriaModel.ViewModel fieldModel = new AuditKriteriaModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.audit_id = dr["audit_id"].ToString();
                        fieldModel.kriteria_audit_id = dr["kriteria_audit_id"].ToString();
                        fieldModel.kriteria_audit_id_text = dr["kriteria_audit_id_text"].ToString();
                        fieldModel.jumlah_iso_14001_2015 = dr["jumlah_iso_14001_2015"].ToString();
                        fieldModel.jumlah_iso_45001_2018 = dr["jumlah_iso_45001_2018"].ToString();
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
        public JsonResult Insert(AuditKriteriaModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = AuditKriteriaModel.GetNewId().ToString();
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["jumlah_iso_14001_2015"] = fieldModel.jumlah_iso_14001_2015;
                    data["jumlah_iso_45001_2018"] = fieldModel.jumlah_iso_45001_2018;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = AuditKriteriaModel.Insert(_hostingEnvironment, HttpContext, data);



                    //-------------------------------------------------------------------------
                    if (fieldModel.kriteria_audit_id == "1") //IAMS
                    {
                        OrderedDictionary dataAGC = new OrderedDictionary();
                        dataAGC["id"] = AuditKriteriaAgcModel.GetNewId().ToString();
                        dataAGC["audit_id"] = fieldModel.audit_id;
                        dataAGC["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataAGC["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataAGC["insert_at"] = DateTime.Now;
                        dataAGC["update_by"] = data["insert_by"];
                        dataAGC["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataAGC);
                        AuditKriteriaAgcModel.Insert(_hostingEnvironment, HttpContext, dataAGC);

                        OrderedDictionary dataSMK3 = new OrderedDictionary();
                        dataSMK3["id"] = AuditKriteriaSmk3Model.GetNewId().ToString();
                        dataSMK3["audit_id"] = fieldModel.audit_id;
                        dataSMK3["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataSMK3["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataSMK3["insert_at"] = DateTime.Now;
                        dataSMK3["update_by"] = data["insert_by"];
                        dataSMK3["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataSMK3);
                        AuditKriteriaSmk3Model.Insert(_hostingEnvironment, HttpContext, dataSMK3);

                        OrderedDictionary dataSMKP = new OrderedDictionary();
                        dataSMKP["id"] = AuditKriteriaSmkpModel.GetNewId().ToString();
                        dataSMKP["audit_id"] = fieldModel.audit_id;
                        dataSMKP["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataSMKP["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataSMKP["insert_at"] = DateTime.Now;
                        dataSMKP["update_by"] = data["insert_by"];
                        dataSMKP["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataSMKP);
                        AuditKriteriaSmkpModel.Insert(_hostingEnvironment, HttpContext, dataSMKP);
                    }


                    //Kantor, SOHE, Gudang, Konstruksi
                    if (fieldModel.kriteria_audit_id == "2" || fieldModel.kriteria_audit_id == "3" || fieldModel.kriteria_audit_id == "6" || fieldModel.kriteria_audit_id == "7")
                    {
                        OrderedDictionary dataAGC = new OrderedDictionary();
                        dataAGC["id"] = AuditKriteriaAgcModel.GetNewId().ToString();
                        dataAGC["audit_id"] = fieldModel.audit_id;
                        dataAGC["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataAGC["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataAGC["insert_at"] = DateTime.Now;
                        dataAGC["update_by"] = data["insert_by"];
                        dataAGC["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataAGC);
                        AuditKriteriaAgcModel.Insert(_hostingEnvironment, HttpContext, dataAGC);
                    }


                    //Tambang, Manufaktur
                    if (fieldModel.kriteria_audit_id == "4" || fieldModel.kriteria_audit_id == "5")
                    {
                        OrderedDictionary dataAGC_TM = new OrderedDictionary();
                        dataAGC_TM["id"] = AuditKriteriaAgcModel.GetNewId().ToString();
                        dataAGC_TM["audit_id"] = fieldModel.audit_id;
                        dataAGC_TM["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataAGC_TM["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataAGC_TM["insert_at"] = DateTime.Now;
                        dataAGC_TM["update_by"] = data["insert_by"];
                        dataAGC_TM["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataAGC_TM);
                        AuditKriteriaAgcTmModel.Insert(_hostingEnvironment, HttpContext, dataAGC_TM);
                    }



                    //SMK3
                    if (fieldModel.kriteria_audit_id == "8")
                    {
                        OrderedDictionary dataSMK3 = new OrderedDictionary();
                        dataSMK3["id"] = AuditKriteriaSmk3Model.GetNewId().ToString();
                        dataSMK3["audit_id"] = fieldModel.audit_id;
                        dataSMK3["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataSMK3["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataSMK3["insert_at"] = DateTime.Now;
                        dataSMK3["update_by"] = data["insert_by"];
                        dataSMK3["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataSMK3);
                        AuditKriteriaSmk3Model.Insert(_hostingEnvironment, HttpContext, dataSMK3);
                    }


                    //SMKP
                    if (fieldModel.kriteria_audit_id == "9")
                    {
                        OrderedDictionary dataSMKP = new OrderedDictionary();
                        dataSMKP["id"] = AuditKriteriaSmkpModel.GetNewId().ToString();
                        dataSMKP["audit_id"] = fieldModel.audit_id;
                        dataSMKP["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                        dataSMKP["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataSMKP["insert_at"] = DateTime.Now;
                        dataSMKP["update_by"] = data["insert_by"];
                        dataSMKP["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataSMKP);
                        AuditKriteriaSmkpModel.Insert(_hostingEnvironment, HttpContext, dataSMKP);
                    }


                    //CSMS
                    if (fieldModel.kriteria_audit_id == "10")
                    {
                        OrderedDictionary dataCSMS = new OrderedDictionary();
                        dataCSMS["id"] = AuditKriteriaCsmsModel.GetNewId().ToString();
                        dataCSMS["audit_id"] = fieldModel.audit_id;
                        dataCSMS["kriteria_audit_id"] = fieldModel.kriteria_audit_id;

                        dataCSMS["green_strategy_bobot"] = 15;
                        dataCSMS["green_process_bobot"] = 25;
                        dataCSMS["green_product_bobot"] = 20;
                        dataCSMS["green_employee_bobot"] = 20;
                        dataCSMS["legal_bobot"] = 20;

                        dataCSMS["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        dataCSMS["insert_at"] = DateTime.Now;
                        dataCSMS["update_by"] = data["insert_by"];
                        dataCSMS["update_at"] = data["insert_at"];
                        SqlHelper.ConvertEmptyValuesToDBNull(dataCSMS);
                        AuditKriteriaCsmsModel.Insert(_hostingEnvironment, HttpContext, dataCSMS);
                    }

                    //-------------------------------------------------------------------------
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
        public JsonResult Update(AuditKriteriaModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(AuditKriteriaModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["audit_id"] = fieldModel.audit_id;
                    data["kriteria_audit_id"] = fieldModel.kriteria_audit_id;
                    data["jumlah_iso_14001_2015"] = fieldModel.jumlah_iso_14001_2015;
                    data["jumlah_iso_45001_2018"] = fieldModel.jumlah_iso_45001_2018;

                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = AuditKriteriaModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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
                        OrderedDictionary data = DataModel.DtToDictionary(AuditKriteriaModel.GetData(PkValue));
                        if (data != null)
						{

                            //-------------------------------------------------------------------------
                            DataTable dtKriteriaAudit = AuditKriteriaModel.GetData(PkValue);
                            var audit_id = dtKriteriaAudit.Rows[0]["audit_id"].ToString();
                            var kriteria_audit_id = dtKriteriaAudit.Rows[0]["kriteria_audit_id"].ToString();

                            if (kriteria_audit_id == "1") //IAMS
                            {
                                var dataAGC = new OrderedDictionary();
                                dataAGC["audit_id"] = audit_id;
                                dataAGC["kriteria_audit_id"] = kriteria_audit_id;                                
                                DataTable dtAGC = AuditKriteriaAgcModel.GetViewDataByColumn(dataAGC);
                                foreach (DataRow row in dtAGC.Rows)
                                {
                                    var dataAGCKey = new OrderedDictionary();
                                    dataAGCKey["id"] = row["id"].ToString();
                                    AuditKriteriaAgcModel.Delete(_hostingEnvironment, HttpContext, dataAGCKey);
                                }



                                OrderedDictionary dataSMK3 = new OrderedDictionary();
                                dataSMK3["audit_id"] = audit_id;
                                dataSMK3["kriteria_audit_id"] = kriteria_audit_id;                                
                                DataTable dtSMK3 = AuditKriteriaSmk3Model.GetViewDataByColumn(dataSMK3);
                                foreach (DataRow row in dtSMK3.Rows)
                                {
                                    var dataSMK3Key = new OrderedDictionary();
                                    dataSMK3Key["id"] = row["id"].ToString();
                                    AuditKriteriaSmk3Model.Delete(_hostingEnvironment, HttpContext, dataSMK3Key);
                                }



                                OrderedDictionary dataSMKP = new OrderedDictionary();
                                dataSMKP["audit_id"] = audit_id;
                                dataSMKP["kriteria_audit_id"] = kriteria_audit_id;
                                DataTable dtSMKP = AuditKriteriaSmkpModel.GetViewDataByColumn(dataSMKP);
                                foreach (DataRow row in dtSMKP.Rows)
                                {
                                    var dataSMKPKey = new OrderedDictionary();
                                    dataSMKPKey["id"] = row["id"].ToString();
                                    AuditKriteriaSmkpModel.Delete(_hostingEnvironment, HttpContext, dataSMKPKey);
                                }
                            }


                            //Kantor, SOHE, Gudang, Konstruksi
                            if (kriteria_audit_id == "2" || kriteria_audit_id == "3" || kriteria_audit_id == "6" || kriteria_audit_id == "7")
                            {
                                OrderedDictionary dataAGC = new OrderedDictionary();
                                dataAGC["audit_id"] = audit_id;
                                dataAGC["kriteria_audit_id"] = kriteria_audit_id;
                                DataTable dtAGC = AuditKriteriaAgcModel.GetViewDataByColumn(dataAGC);
                                foreach (DataRow row in dtAGC.Rows)
                                {
                                    var dataAGCKey = new OrderedDictionary();
                                    dataAGCKey["id"] = row["id"].ToString();
                                    AuditKriteriaAgcModel.Delete(_hostingEnvironment, HttpContext, dataAGCKey);
                                }
                            }


                            //Tambang, Manufaktur
                            if (kriteria_audit_id == "4" || kriteria_audit_id == "5")
                            {
                                OrderedDictionary dataAGC_TM = new OrderedDictionary();
                                dataAGC_TM["audit_id"] = audit_id;
                                dataAGC_TM["kriteria_audit_id"] = kriteria_audit_id;
                                DataTable dtAGC_TM = AuditKriteriaAgcTmModel.GetViewDataByColumn(dataAGC_TM);
                                foreach (DataRow row in dtAGC_TM.Rows)
                                {
                                    var dataAGC_TMKey = new OrderedDictionary();
                                    dataAGC_TMKey["id"] = row["id"].ToString();
                                    AuditKriteriaAgcTmModel.Delete(_hostingEnvironment, HttpContext, dataAGC_TMKey);
                                }
                            }



                            //SMK3
                            if (kriteria_audit_id == "8")
                            {
                                OrderedDictionary dataSMK3 = new OrderedDictionary();
                                dataSMK3["audit_id"] = audit_id;
                                dataSMK3["kriteria_audit_id"] = kriteria_audit_id;
                                DataTable dtSMK3 = AuditKriteriaSmk3Model.GetViewDataByColumn(dataSMK3);
                                foreach (DataRow row in dtSMK3.Rows)
                                {
                                    var dataSMK3Key = new OrderedDictionary();
                                    dataSMK3Key["id"] = row["id"].ToString();
                                    AuditKriteriaSmk3Model.Delete(_hostingEnvironment, HttpContext, dataSMK3Key);
                                }
                            }


                            //SMKP
                            if (kriteria_audit_id == "9")
                            {
                                OrderedDictionary dataSMKP = new OrderedDictionary();
                                dataSMKP["audit_id"] = audit_id;
                                dataSMKP["kriteria_audit_id"] = kriteria_audit_id;
                                DataTable dtSMKP = AuditKriteriaSmkpModel.GetViewDataByColumn(dataSMKP);
                                foreach (DataRow row in dtSMKP.Rows)
                                {
                                    var dataSMKPKey = new OrderedDictionary();
                                    dataSMKPKey["id"] = row["id"].ToString();
                                    AuditKriteriaSmkpModel.Delete(_hostingEnvironment, HttpContext, dataSMKPKey);
                                }
                            }


                            //CSMS
                            if (kriteria_audit_id == "10")
                            {
                                OrderedDictionary dataCSMS = new OrderedDictionary();
                                dataCSMS["audit_id"] = audit_id;
                                dataCSMS["kriteria_audit_id"] = kriteria_audit_id;
                                DataTable dtCSMS = AuditKriteriaCsmsModel.GetViewDataByColumn(dataCSMS);
                                foreach (DataRow row in dtCSMS.Rows)
                                {
                                    var dataCSMSKey = new OrderedDictionary();
                                    dataCSMSKey["id"] = row["id"].ToString();
                                    AuditKriteriaCsmsModel.Delete(_hostingEnvironment, HttpContext, dataCSMSKey);
                                }
                            }

                            //-------------------------------------------------------------------------


                            result = AuditKriteriaModel.Delete(_hostingEnvironment, HttpContext, data);
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
            DataTable data = AuditKriteriaModel.LookupData(param);
            return Json(data);
        }

    }
}