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
    
    public class DocumentController : Controller
    {
        private string _rule_view = "DocumentView";
        private string _rule_add = "DocumentAdd";
        private string _rule_edit = "DocumentEdit";
        private string _rule_delete = "DocumentDelete";
        private string _rule_edit_all = "DocumentEditAll";
        private string _rule_delete_all = "DocumentEditAll";
        private string _path_controler = "/Document/";
        private string _path_view = "/Views/Document/Document/";
        private readonly string _table_name = "ta_document";
        private readonly string _table_title = "Document";

		private static IHostingEnvironment _hostingEnvironment;
        public DocumentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
        public IActionResult Index(DocumentModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = DocumentModel._pkKey;
                    ViewData["displayItem"] = DocumentModel.GetDisplayItem();
                    ViewData["column_att"] = DocumentModel.GetGridColumn();
                    ViewData["param"] = DocumentModel.GetListParam();
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
        public JsonResult GetListData(DocumentModel.ActionRequest param)
        {
            DocumentModel.InitDocumentData();
            GridData data = DocumentModel.GetListData(HttpContext, param);
            string baseUrl = WebHelper.GetBaseUrl(HttpContext);
            for (int i = 0; i < data.data.Rows.Count; i++)
            {
                var PkValue = new OrderedDictionary();
                //PkValue["id"] = data.data.Rows[i]["id"];
                //data.data.Rows[i]["file_name"] = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_name", data.data.Rows[i]["file_name"].ToString());
                PkValue["id"] = data.data.Rows[i]["tbl_id"].ToString();
                string cur_table_name = data.data.Rows[i]["tbl_name"].ToString();
                string cur_col_name = data.data.Rows[i]["col_name"].ToString();
                data.data.Rows[i]["file_name"] = FileHelper.GetlinkDownload(baseUrl, cur_table_name, PkValue, cur_col_name, data.data.Rows[i]["file_name"].ToString());
            }

            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string id)
        {
            string viewFileName = "View.cshtml";
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_view))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    DocumentModel.ViewModel fieldModel = new DocumentModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = DocumentModel.GetViewData(PkValue);
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
                                fieldModel.group_id = dr["group_id"].ToString();
                                fieldModel.group_name = dr["group_name"].ToString();
                                fieldModel.tbl_name = dr["tbl_name"].ToString();
                                fieldModel.tbl_title = dr["tbl_title"].ToString();
                                fieldModel.tbl_id = dr["tbl_id"].ToString();
                                fieldModel.tbl_parent_id = dr["tbl_parent_id"].ToString();
                                fieldModel.title = dr["title"].ToString();
                                fieldModel.col_name = dr["col_name"].ToString();
                                fieldModel.col_title = dr["col_title"].ToString();
                                //
                                //fieldModel.file_name = FileHelper.GetlinkDownload(baseUrl, _table_name, PkValue, "file_name", dr["file_name"].ToString());
                                var CurPkValue = new OrderedDictionary();
                                CurPkValue["id"] = dr["tbl_id"].ToString();
                                string cur_table_name = dr["tbl_name"].ToString();
                                string cur_col_name = dr["col_name"].ToString();

                                if (cur_table_name == "ta_document_nm")
                                {
                                    viewFileName = "Viewnm.cshtml";                                    
                                }
                                fieldModel.file_name = FileHelper.GetlinkDownload(baseUrl, cur_table_name, CurPkValue, cur_col_name, dr["file_name"].ToString());
                                fieldModel.folder_path = dr["folder_path"].ToString();
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
                    return View(_path_view + viewFileName);//return View(_path_view + "View.cshtml");
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
        public IActionResult Add(DocumentModel.ViewModel fieldModel)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insertnm";  //ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    //DocumentModel.ViewModel fieldModel = new DocumentModel.ViewModel();
                    fieldModel.id = DocumentModel.GetNewId().ToString();
                    fieldModel.file_name_init = "[]";
                    ViewData["fieldModel"] = fieldModel;                    
                    return View(_path_view + "Add.cshtml"); //return View(_path_view + "Edit.cshtml");
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
                    DataTable data = DocumentModel.GetViewData(PkValue);
                    DocumentModel.ViewModel fieldModel = new DocumentModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = DocumentModel.GetNewId().ToString();
                        fieldModel.ehs_area_id = dr["ehs_area_id"].ToString();
                        fieldModel.ba_id = dr["ba_id"].ToString();
                        fieldModel.pa_id = dr["pa_id"].ToString();
                        fieldModel.psa_id = dr["psa_id"].ToString();
                        fieldModel.group_id = dr["group_id"].ToString();
                        fieldModel.group_name = dr["group_name"].ToString();
                        fieldModel.tbl_name = dr["tbl_name"].ToString();
                        fieldModel.tbl_title = dr["tbl_title"].ToString();
                        fieldModel.tbl_id = dr["tbl_id"].ToString();
                        fieldModel.tbl_parent_id = dr["tbl_parent_id"].ToString();
                        fieldModel.title = dr["title"].ToString();
                        fieldModel.col_name = dr["col_name"].ToString();
                        fieldModel.col_title = dr["col_title"].ToString();
                        fieldModel.file_name = dr["file_name"].ToString();
                        fieldModel.folder_path = dr["folder_path"].ToString();
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
            string viewFileName = "Edit.cshtml";
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    ViewData["UrlActionRemoveFolder"] = baseUrl + _path_controler + "RemoveFolder";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = DocumentModel.GetViewData(PkValue);
                    DocumentModel.ViewModel fieldModel = new DocumentModel.ViewModel();
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
                        fieldModel.group_id = dr["group_id"].ToString();
                        fieldModel.group_name = dr["group_name"].ToString();
                        fieldModel.tbl_name = dr["tbl_name"].ToString();
                        fieldModel.tbl_title = dr["tbl_title"].ToString();
                        fieldModel.tbl_id = dr["tbl_id"].ToString();
                        fieldModel.tbl_parent_id = dr["tbl_parent_id"].ToString();
                        fieldModel.title = dr["title"].ToString();
                        fieldModel.col_name = dr["col_name"].ToString();
                        fieldModel.col_title = dr["col_title"].ToString();
                        //
                        //string[] init_file_name = FileHelper.GetinitialFiles(_hostingEnvironment, HttpContext, _table_name, "file_name", PkValue, dr["file_name"].ToString());
                        var CurPkValue = new OrderedDictionary();
                        CurPkValue["id"] = dr["tbl_id"].ToString();
                        string cur_table_name = dr["tbl_name"].ToString();
                        string cur_col_name = dr["col_name"].ToString();

                        string[] init_file_name;
                        if (cur_table_name == "ta_document_nm")
                        {
                            viewFileName = "Editnm.cshtml";
                            ViewData["UrlAction"] = baseUrl + _path_controler + "Updatenm";
                            init_file_name = FileHelper.GetinitialFilesDocument(_hostingEnvironment, HttpContext, cur_table_name, cur_col_name, CurPkValue, dr["file_name"].ToString());
                        }
                        else
                        {
                            init_file_name = FileHelper.GetinitialFilesDocument(_hostingEnvironment, HttpContext, cur_table_name, cur_col_name, CurPkValue, dr["file_name"].ToString());
                        }
                        //
                        fieldModel.file_name = init_file_name[0];
                        fieldModel.file_name_init = init_file_name[1];
                        fieldModel.folder_path = dr["folder_path"].ToString();
                    }
                    ViewData["fieldModel"] = fieldModel;
                    return View(_path_view + viewFileName); //return View(_path_view + "Edit.cshtml");
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
        public JsonResult Insert(DocumentModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = DocumentModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["group_id"] = fieldModel.group_id;
                    data["group_name"] = AntiXss.HtmlEncode(fieldModel.group_name);
                    data["tbl_name"] = AntiXss.HtmlEncode(fieldModel.tbl_name);
                    data["tbl_title"] = AntiXss.HtmlEncode(fieldModel.tbl_title);
                    data["tbl_id"] = fieldModel.tbl_id;
                    data["tbl_parent_id"] = fieldModel.tbl_parent_id;
                    data["title"] = AntiXss.HtmlEncode(fieldModel.title);
                    data["col_name"] = AntiXss.HtmlEncode(fieldModel.col_name);
                    data["col_title"] = AntiXss.HtmlEncode(fieldModel.col_title);
                    data["file_name"] = AntiXss.HtmlEncode(fieldModel.file_name);
                    data["folder_path"] = AntiXss.HtmlEncode(fieldModel.folder_path);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = DocumentModel.Insert(_hostingEnvironment, HttpContext, data);
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

        public JsonResult Insertnm(DocumentModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_add))
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = DocumentModel.GetNewId().ToString();
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["group_id"] = fieldModel.group_id;
                    data["group_name"] = ""; //AntiXss.HtmlEncode(fieldModel.group_name);
                    data["tbl_name"] = ""; //AntiXss.HtmlEncode(fieldModel.tbl_name);
                    data["tbl_title"] = ""; //AntiXss.HtmlEncode(fieldModel.tbl_title);
                    data["tbl_id"] = fieldModel.tbl_id;
                    data["tbl_parent_id"] = fieldModel.tbl_parent_id;
                    data["title"] = ""; //AntiXss.HtmlEncode(fieldModel.title);
                    data["col_name"] = ""; //AntiXss.HtmlEncode(fieldModel.col_name);
                    data["col_title"] = ""; //AntiXss.HtmlEncode(fieldModel.col_title);
                    data["file_name"] = Encoder.HtmlEncode(fieldModel.file_name);
                    data["folder_path"] = Encoder.HtmlEncode(fieldModel.folder_path);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = DocumentModel.Insertnm(_hostingEnvironment, HttpContext, data);
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
        public JsonResult Update(DocumentModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(DocumentModel.GetData(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(DocumentModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["group_id"] = fieldModel.group_id;
                    data["group_name"] = AntiXss.HtmlEncode(fieldModel.group_name);
                    data["tbl_name"] = AntiXss.HtmlEncode(fieldModel.tbl_name);
                    data["tbl_title"] = AntiXss.HtmlEncode(fieldModel.tbl_title);
                    data["tbl_id"] = fieldModel.tbl_id;
                    data["tbl_parent_id"] = fieldModel.tbl_parent_id;
                    data["title"] = AntiXss.HtmlEncode(fieldModel.title);
                    data["col_name"] = AntiXss.HtmlEncode(fieldModel.col_name);
                    data["col_title"] = AntiXss.HtmlEncode(fieldModel.col_title);
                    data["file_name"] = AntiXss.HtmlEncode(fieldModel.file_name);
                    data["folder_path"] = AntiXss.HtmlEncode(fieldModel.folder_path);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {

                        result = DocumentModel.Update(_hostingEnvironment, HttpContext, data, data_old);
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

        public JsonResult Updatenm(DocumentModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_edit))
                {
                    string id_old = fieldModel.tbl_id; //.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(DocumentModel.GetDatanm(PkValue));

                    OrderedDictionary data = DataModel.DtToDictionary(DocumentModel.GetDatanm(PkValue));
                    data["id"] = fieldModel.id;
                    data["ehs_area_id"] = fieldModel.ehs_area_id;
                    data["ba_id"] = fieldModel.ba_id;
                    data["pa_id"] = fieldModel.pa_id;
                    data["psa_id"] = fieldModel.psa_id;
                    data["group_id"] = fieldModel.group_id;
                    data["group_name"] = Encoder.HtmlEncode(fieldModel.group_name);
                    data["tbl_name"] = Encoder.HtmlEncode(fieldModel.tbl_name);
                    data["tbl_title"] = Encoder.HtmlEncode(fieldModel.tbl_title);
                    data["tbl_id"] = fieldModel.tbl_id;
                    data["tbl_parent_id"] = fieldModel.tbl_parent_id;
                    data["title"] = Encoder.HtmlEncode(fieldModel.title);
                    data["col_name"] = Encoder.HtmlEncode(fieldModel.col_name);
                    data["col_title"] = Encoder.HtmlEncode(fieldModel.col_title);
                    data["file_name"] = Encoder.HtmlEncode(fieldModel.file_name);
                    data["folder_path"] = Encoder.HtmlEncode(fieldModel.folder_path);
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);

                    //if (DataModel.HasUpdate(data, data_old))
                    string new_file_name = Encoder.HtmlEncode(fieldModel.file_name).ToString();
                    string old_file_name = data_old["file_name"].ToString();
                    if (old_file_name!=new_file_name) 
                    {

                        result = DocumentModel.Updatenm(_hostingEnvironment, HttpContext, data, data_old);
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
						OrderedDictionary data = DataModel.DtToDictionary(DocumentModel.GetData(PkValue));
						if (data != null)
						{
							result = DocumentModel.Delete(_hostingEnvironment, HttpContext, data);
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

        public JsonResult RemoveFolder(DocumentModel.ViewModel fieldModel)
        {
            string id = fieldModel.tbl_id;
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (SecurityHelper.HasRule(HttpContext, _rule_delete))
                {
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    OrderedDictionary data = DataModel.DtToDictionary(DocumentModel.GetDatanm(PkValue));
                    if (data != null)
                    {
                        result = DocumentModel.Deletenm(_hostingEnvironment, HttpContext, data);
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
            DataTable data = DocumentModel.LookupData(param);
            return Json(data);
        }
        public async Task<IActionResult> save_file_name(IEnumerable<IFormFile> file_name_file, string tbl_name, string col_name, string tbl_id)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid);
            pathData = Path.Combine(pathData, tbl_name); //pathData = Path.Combine(pathData, _table_name); 
            pathData = Path.Combine(pathData, col_name);//pathData = Path.Combine(pathData, "file_name");
            if (file_name_file != null)
            {
                foreach (var file in file_name_file)
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

        public ActionResult remove_file_name(string[] fileNames, string tbl_name, string col_name, string tbl_id)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid);
            pathData = Path.Combine(pathData, tbl_name); //pathData = Path.Combine(pathData, _table_name); 
            pathData = Path.Combine(pathData, col_name);//pathData = Path.Combine(pathData, "file_name");
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    string physicalPath = Path.Combine(pathData, fileName);
                    if (System.IO.File.Exists(physicalPath)) { System.IO.File.Delete(physicalPath); }
                    //
                    //FileHelper.DeleteFromBaseDocument(_hostingEnvironment, HttpContext, tbl_name, col_name, tbl_id, fullName);
                    DocumentModel.InitDocumentData();
                }
            }
            return Content("");
        }


        public async Task<IActionResult> save_file_name_nm(IEnumerable<IFormFile> file_name_file)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid);
            pathData = Path.Combine(pathData, "ta_document_nm"); 
            pathData = Path.Combine(pathData, "file_name");
            if (file_name_file != null)
            {
                foreach (var file in file_name_file)
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

        public ActionResult remove_file_name_nm(string[] fileNames)
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string sessid = HttpContext.Session.Id;
            string pathData = Path.Combine(upload_temp, sessid);
            pathData = Path.Combine(pathData, "ta_document_nm");
            pathData = Path.Combine(pathData, "file_name");
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

        public JsonResult LookupDataPath()
        {
            var data = DocumentModel.LookupDataPath();
            return Json(data);
        }

    }
}