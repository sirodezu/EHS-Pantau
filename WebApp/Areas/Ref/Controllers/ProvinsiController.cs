using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Collections.Specialized;
using WebApp.Models;
using WebApp.Areas.Ref.Models;
using Microsoft.Security.Application;

namespace WebApp.Areas.Ref.Controllers
{
    
    [Area("Ref")]
    public class ProvinsiController : Controller
    {
        private string _rule_view = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationView";
        private string _rule_add = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationAdd";
        private string _rule_edit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationEdit";
        private string _rule_delete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationDelete";
        private string _path_controler = "/Ref/Provinsi/";
        private string _path_view = "/Areas/Ref/Views/Provinsi/";
        private readonly string _table_name = "ref_provinsi";
        private readonly string _table_title = "Provinsi";

		[HttpGet]
        public IActionResult Index(ProvinsiModel.ViewModel filterColumn)
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
                    ViewData["pkKey"] = ProvinsiModel._pkKey;
                    ViewData["displayItem"] = ProvinsiModel.GetDisplayItem();
                    ViewData["column_att"] = ProvinsiModel.GetGridColumn();
                    ViewData["param"] = ProvinsiModel.GetListParam();
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
        public JsonResult GetListData(ProvinsiModel.ActionRequest param)
        {
            GridData data = ProvinsiModel.GetListData(param);
            return Json(data);
        }
        [HttpGet]
        public IActionResult ViewItem(string id)
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_view) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "ViewTitle", "Infromasi Detil " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ProvinsiModel.ViewModel fieldModel = new ProvinsiModel.ViewModel();
                    ViewData["fieldModel"] = fieldModel;
                    DataTable data = new DataTable();
                    if (id != null && id !="" )
                    {
                        var PkValue = new OrderedDictionary();
                        PkValue["id"] = id;
                        data = ProvinsiModel.GetViewData(PkValue);
                        if (data != null)
                        {
                            foreach (DataRow dr in data.Rows)
                            {
                                fieldModel.id = dr["id"].ToString();
                                fieldModel.id_old = dr["id"].ToString();
                                fieldModel.kode = dr["kode"].ToString();
                                fieldModel.nama = dr["nama"].ToString();
                                fieldModel.keterangan = dr["keterangan"].ToString();
                                fieldModel.tanggal = dr["tanggal"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal"]) : "";
                                fieldModel.waktu = dr["waktu"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["waktu"]) : "";
                                fieldModel.nilai_decimal = String.Format("{0:N2}", dr["nilai_decimal"]);
                                fieldModel.point_x = String.Format("{0:N10}", dr["point_x"]);
                                fieldModel.point_y = String.Format("{0:N10}", dr["point_y"]);
                                fieldModel.aktif_id = dr["aktif_id"].ToString();
                                fieldModel.aktif_id_text = dr["aktif_id_text"].ToString();
                                fieldModel.lock_by = dr["lock_by"].ToString();
                                fieldModel.lock_at = dr["lock_at"].ToString() != "" ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", dr["lock_at"]) : "";
                                fieldModel.lock_address = dr["lock_address"].ToString();
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
        public IActionResult Add()
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
                    ProvinsiModel.ViewModel fieldModel = new ProvinsiModel.ViewModel();
                    fieldModel.id = ProvinsiModel.GetNewId().ToString();
                    fieldModel.kode = "";
                    fieldModel.nama = "";
                    fieldModel.keterangan = "";
                    fieldModel.tanggal = "";
                    fieldModel.dt_tanggal = "";
                    fieldModel.waktu = "";
                    fieldModel.nilai_decimal = "";
                    fieldModel.point_x = "";
                    fieldModel.point_y = "";
                    fieldModel.aktif_id = "1";
                    fieldModel.lock_by = "";
                    fieldModel.lock_at = "";
                    fieldModel.lock_address = "";
                    fieldModel.insert_by = "";
                    fieldModel.insert_at = "";
                    fieldModel.update_by = "";
                    fieldModel.update_at = "";
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
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "AddTitle", "Tambah " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Insert";
                    ViewData["form_type"] = "Add";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = ProvinsiModel.GetViewData(PkValue);
                    ProvinsiModel.ViewModel fieldModel = new ProvinsiModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = ProvinsiModel.GetNewId().ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.tanggal = dr["tanggal"].ToString();
                        fieldModel.waktu = dr["waktu"].ToString();
                        fieldModel.nilai_decimal = dr["nilai_decimal"].ToString();
                        fieldModel.point_x = dr["point_x"].ToString();
                        fieldModel.point_y = dr["point_y"].ToString();
                        fieldModel.aktif_id = dr["aktif_id"].ToString();
                        fieldModel.lock_by = dr["lock_by"].ToString();
                        fieldModel.lock_at = dr["lock_at"].ToString();
                        fieldModel.lock_address = dr["lock_address"].ToString();
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
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue(_table_name, "EditTitle", "Edit " + _table_title);
                    ViewData["Title"] = ViewData["TitleHeader"];
                    ViewData["UrlAction"] = baseUrl + _path_controler + "Update";
                    ViewData["form_type"] = "Edit";
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id;
                    DataTable data = ProvinsiModel.GetViewData(PkValue);
                    ProvinsiModel.ViewModel fieldModel = new ProvinsiModel.ViewModel();
                    foreach (DataRow dr in data.Rows)
                    {
                        fieldModel.id = dr["id"].ToString();
                        fieldModel.id_old = dr["id"].ToString();
                        fieldModel.kode = dr["kode"].ToString();
                        fieldModel.nama = dr["nama"].ToString();
                        fieldModel.keterangan = dr["keterangan"].ToString();
                        fieldModel.tanggal = dr["tanggal"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", dr["tanggal"]) : "";
                        fieldModel.dt_tanggal = dr["tanggal"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["tanggal"]) : "";
                        fieldModel.waktu = dr["waktu"].ToString();
                        fieldModel.nilai_decimal = dr["nilai_decimal"].ToString();
                        fieldModel.point_x = dr["point_x"].ToString();
                        fieldModel.point_y = dr["point_y"].ToString();
                        fieldModel.aktif_id = dr["aktif_id"].ToString();
                        fieldModel.aktif_id_text = dr["aktif_id_text"].ToString();
                        fieldModel.lock_by = dr["lock_by"].ToString();
                        fieldModel.lock_at = dr["lock_at"].ToString();
                        fieldModel.lock_address = dr["lock_address"].ToString();
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
        public JsonResult Insert(ProvinsiModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_add) != null)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["id"] = ProvinsiModel.GetNewId().ToString();
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["tanggal"] = fieldModel.tanggal;
                    data["waktu"] = fieldModel.waktu;
                    data["nilai_decimal"] = fieldModel.nilai_decimal != null ? fieldModel.nilai_decimal.Replace(",",".") : "";
                    data["point_x"] = fieldModel.point_x != null ? fieldModel.point_x.Replace(",",".") : "";
                    data["point_y"] = fieldModel.point_y != null ? fieldModel.point_y.Replace(",",".") : "";
                    data["aktif_id"] = fieldModel.aktif_id;
                    data["lock_by"] = fieldModel.lock_by;
                    data["lock_at"] = fieldModel.lock_at;
                    data["lock_address"] = fieldModel.lock_address;
                    data["insert_by"] = SecurityHelper.CurrentUserId(HttpContext);
                    data["insert_at"] = DateTime.Now;
                    data["update_by"] = data["insert_by"];
                    data["update_at"] = data["insert_at"];
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    result = ProvinsiModel.Insert(HttpContext, data);
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
        public JsonResult Update(ProvinsiModel.ViewModel fieldModel)
        {
            ProsesResult result = new ProsesResult();
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_edit) != null)
                {
                    string id_old = fieldModel.id_old;
                    var PkValue = new OrderedDictionary();
                    PkValue["id"] = id_old;
                    OrderedDictionary data_old = DataModel.DtToDictionary(ProvinsiModel.GetData(PkValue));
                    data_old["tanggal"] = data_old["tanggal"].ToString() != "" ? String.Format("{0:yyyy-MM-dd}", data_old["tanggal"]) : "";
                    data_old["nilai_decimal"] = data_old["nilai_decimal"].ToString() != ""  ? data_old["nilai_decimal"].ToString().Replace(",", ".") : "";
                    data_old["point_x"] = data_old["point_x"].ToString() != ""  ? data_old["point_x"].ToString().Replace(",", ".") : "";
                    data_old["point_y"] = data_old["point_y"].ToString() != ""  ? data_old["point_y"].ToString().Replace(",", ".") : "";
                    OrderedDictionary data = DataModel.DtToDictionary(ProvinsiModel.GetData(PkValue));
                    data["id"] = fieldModel.id;
                    data["kode"] = AntiXss.HtmlEncode(fieldModel.kode);
                    data["nama"] = AntiXss.HtmlEncode(fieldModel.nama);
                    data["keterangan"] = AntiXss.HtmlEncode(fieldModel.keterangan);
                    data["tanggal"] = fieldModel.tanggal;
                    data["waktu"] = fieldModel.waktu;
                    data["nilai_decimal"] = fieldModel.nilai_decimal != null ? fieldModel.nilai_decimal.ToString().Replace(",", ".") : "";
                    data["point_x"] = fieldModel.point_x != null ? fieldModel.point_x.ToString().Replace(",", ".") : "";
                    data["point_y"] = fieldModel.point_y != null ? fieldModel.point_y.ToString().Replace(",", ".") : "";
                    data["aktif_id"] = fieldModel.aktif_id;
                    data["lock_by"] = fieldModel.lock_by;
                    data["lock_at"] = fieldModel.lock_at;
                    data["lock_address"] = fieldModel.lock_address;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    SqlHelper.ConvertEmptyValuesToDBNull(data_old);
                    if (DataModel.HasUpdate(data, data_old))
                    {
                        data["update_by"] = SecurityHelper.CurrentUserId(HttpContext);
                        data["update_at"] = DateTime.Now;
                        result = ProvinsiModel.Update(HttpContext, data, data_old);
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
                if (HttpContext.Session.GetString(_rule_delete) != null)
                {
					int jml = DataModel.GetCountDataUsed(_table_name, id);
                    if (jml == 0) {
						var PkValue = new OrderedDictionary();
						PkValue["id"] = id;
						OrderedDictionary data = DataModel.DtToDictionary(ProvinsiModel.GetData(PkValue));
						if (data != null)
						{
							result = ProvinsiModel.Delete(HttpContext, data);
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
            DataTable data = ProvinsiModel.LookupData(param);
            return Json(data);
        }

    }
}