using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApp;

namespace WebApp.Areas.Ref.Controllers
{
    [Area("Ref")]
    public class HomeController : Controller
    {
        private string _rule_AreaView = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationView";
        private string _rule_AreaAdd = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationAdd";
        private string _rule_AreaEdit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationEdit";
        private string _rule_AreaDelete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "OrganizationDelete";

        private string _rule_TrainingView = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RefTrainingView";
        private string _rule_TrainingAdd = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RefTrainingAdd";
        private string _rule_TrainingEdit = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RefTrainingEdit";
        private string _rule_TrainingDelete = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + "RefTrainingDelete";
        private string _path_view = "/Areas/Ref/Views/Home/";
        public IActionResult Index()
        {
            if (SecurityHelper.onPageInit(HttpContext))
            {
                if (HttpContext.Session.GetString(_rule_AreaView) != null
                    || HttpContext.Session.GetString(_rule_AreaAdd) != null
                    || HttpContext.Session.GetString(_rule_AreaEdit) != null
                    || HttpContext.Session.GetString(_rule_AreaDelete) != null
                    || HttpContext.Session.GetString(_rule_TrainingView) != null
                    || HttpContext.Session.GetString(_rule_TrainingAdd) != null
                    || HttpContext.Session.GetString(_rule_TrainingEdit) != null
                    || HttpContext.Session.GetString(_rule_TrainingDelete) != null
                    )
                {
                    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                    ViewData["baseUrl"] = baseUrl;
                    ViewData["TitleHeader"] = ResxHelper.GetValue("Message", "ReferensiListTitle", "Daftar Referensi");
                    ViewData["Title"] = ViewData["TitleHeader"];
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
    }
}