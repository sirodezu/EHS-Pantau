using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using WebApp.Models;
using WebApp.Areas.Sys.Models;
using Newtonsoft.Json;

namespace WebApp
{
    public class SecurityHelper
    {
        public static string cookiesClienKey = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "ClientId" : "KKClientId";
        public static string passPhrase = "12345678901234567890123456789012";
        public static string iv = "1234567890123456";
        public static string PasswordHash = "P@@Sw0rd";
        public static string SaltKey = "S@LT&KEY";
        public static string VIKey = "@1B2c3D4e5F6g7H8";
        public static string keyString = "E546C8DF278CD5931069B522E695D4F2";
        public static string ConnString = Settings.GetConnectionString("MainConnection");
        public static string COOKIES_KEY_SESSIONID = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_SESSIONID" : "KK_SESSIONID";
        public static string SESSION_KEY_PERSON_DATA = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_PersonData" : "KK_PersonData";
        public static string SESSION_KEY_PERSONID = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_PersonId" : "KK_PersonId";
        public static string SESSION_KEY_PERSONNRP = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_PersonNRP" : "KK_PersonNRP";
        public static string SESSION_KEY_PERSONNama = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_PersonNama" : "KK_PersonNama";
        public static string SESSION_KEY_USERID = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_UserId" : "KK_UserId";
        public static string SESSION_KEY_REMEMBER_ME = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_RememberMe" : "KK_RememberMe";
        public static string SESSION_KEY_DOMAINNAME = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_DomainName" : "KK_DomainName";
        public static string SESSION_KEY_USERNAME = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_Username" : "KK_Username";
        public static string SESSION_KEY_FULLNAME = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_Fullname" : "KK_Fullname";
        public static string SESSION_KEY_PASSWORD = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_Password" : "KK_Password";
        public static string SESSION_DISPALAY_NAME = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_FullName" : "KK_FullName";
        public static string SESSION_KEY_GROUPID_LIST = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_GroupId" : "KK_GroupId";
        public static string SESSION_KEY_GROUPNAME_LIST = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_GroupName" : "KK_GroupName";
        public static string SESSION_KEY_RULE_LIST = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_Rules" : "KK_Rules";
        public static string SESSION_KEY_ROLE_LIST = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_Roles" : "KK_Roles";
        public static string SESSION_KEY_ROLE_CODE_LIST = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_RoleCode" : "KK_RoleCode";
        public static string SESSION_KEY_ROLE_NAME_LIST = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_RoleName" : "KK_RoleName";
        public static string SESSION_KEY_AUTHENTICATED_USER = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode").ToString() + "_authenticatedUser" : "KK_authenticatedUser";
        public static string SESSION_KEY_HAS_CHANGE_PASSWORD = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_HasChangePassword" : "KK_HasChangePassword";
        public static string SESSION_KEY_MUST_CHANGE_PASSWORD = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_MustChangePassword" : "KK_MustChangePassword";
        public static string SESSION_KEY_ENABLE_CHANGE_PASSWORD = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_EnableChangePassword" : "KK_EnableChangePassword";
        public static string SESSION_KEY_OVERRIDE_LOGIN = Settings.GetAppSetting("AppCode") != null ? Settings.GetAppSetting("AppCode") + "_Rules_OverrideLogin" : "KK_Rules_OverrideLogin";
        private static readonly byte[] EMERGENCY_USER_LOGIN_NAME_HASH = { 0x54, 0xB5, 0x30, 0x72, 0x54, 0xE, 0xEE, 0xB8, 0xF8, 0xE9, 0x34, 0x3E, 0x71, 0xF2, 0x81, 0x76 };
        private static readonly byte[] EMERGENCY_USER_PASSWORD_HASH = { 0xF2, 0xB, 0xDF, 0x62, 0xAD, 0x59, 0x76, 0x8, 0xA7, 0xF1, 0xC4, 0x4B, 0x82, 0x6A, 0xE8, 0x30 };
        public class LoginResult
        {
            public bool Succeeded { get; set; } //
            public bool trackFailedAttempts { get; set; } //trackFailedAttempts
            public bool IsLockedOut { get; set; } // Check User Block
            public bool IncorrectPassword { get; set; } // Check Password
            public bool UserHasExpired { get; set; } // Check User expired
            public string Message { get; set; } // Check User expired
            public string ReturnUrl { get; set; }
        }
        public class Application
        {
            public Application()
            {
            }
            public int ApplicationPK { get; set; }
            public string ApplicationID { get; set; }
            public string ApplicationName { get; set; }
            public string Description { get; set; }
        }
        public class TransactionAuthorization
        {
            public TransactionAuthorization()
            {
            }
            public TransactionAuthorization(long? TransactionAuthorizationPK, string TransactionAuthorizationID)
            {
            }
            public TransactionAuthorization(long? TransactionAuthorizationPK, string TransactionAuthorizationID, string TransactionDescription, Application Application)
            {
            }
            public long? TransactionAuthorizationPK { get; set; }
            public string TransactionAuthorizationID { get; set; }
            public string TransactionDescription { get; set; }
            public int? ApplicationFK { get; set; }
            public Application Application { get; set; }
        }
        public class AreaData
        {
            public string ehs_area_id { get; set; }
            public string ehs_area_kode { get; set; }
            public string ehs_area_nama { get; set; }
            public string ba_id { get; set; }
            public string ba_kode { get; set; }
            public string ba_nama { get; set; }
            public string pa_id { get; set; }
            public string pa_kode { get; set; }
            public string pa_nama { get; set; }
            public string psa_id { get; set; }
            public string psa_kode { get; set; }
            public string psa_nama { get; set; }
        }
        public class UserRole
        {
            public UserRole() { }
            public UserRole(int UserRolePK, string UserRoleID, string UserRoleName) { }
            public int UserRolePK { get; set; }
            public string UserRoleID { get; set; }
            public string UserRoleName { get; set; }
            public string TCArea { get; set; }
            public string PSArea { get; set; }
            public int ApplicationFK { get; set; }
            public List<TransactionAuthorization> TransactionAuthorizations { get; set; }
        }
        public class UserData
        {
            public int UserPK { get; set; }
            public string UserID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
            public string ApplicationID { get; set; }
            public List<UserRole> UserRoles { get; set; }
            public UserData() { }
            public UserData(string userID)
            {
                //UserID = userID;
            }
            public UserData(string userID, string password, string applicationID)
            {
                //UserID = userID;
                //Password = password;
                //ApplicationID = applicationID;
            }
            public UserData(int UserPK, string userID, string username, string password, bool isActive)
            {
                //UserID = userID;
                //Username = username;
                //Password = password;
                //IsActive = isActive;
            }
        }
        public class UserParam
        {
            public string RoleCode { get; set; }
            public string RoleName { get; set; }
            public List<RuleParam> Rules { get; set; }
        }
        public class RuleParam
        {
            public string kode { get; set; }
        }
        public static UserData LoginApi(string username, string password)
        {
            UserData user = new UserData();
            string applicationID = Settings.GetAppSetting("ApplicationID");
            string urlAPI = Settings.GetAppSetting("UrlLoginAPI") != null ? Settings.GetAppSetting("UrlLoginAPI") : "http://itis_dev/user";
            HttpClient UserManagementSystem_WebAPI = new HttpClient();
            UserManagementSystem_WebAPI.BaseAddress = new Uri(urlAPI);
            UserManagementSystem_WebAPI.DefaultRequestHeaders.Accept.Clear();
            UserManagementSystem_WebAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = UserManagementSystem_WebAPI.PostAsJsonAsync(urlAPI + "/api/user/login/", new { UserID = username, Password = password, ApplicationID = applicationID }).Result;
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    List<String> roles = new List<String>();
                    user = response.Content.ReadAsAsync<UserData>().Result;
                }
                catch (Exception e)
                {
                    var g = e;
                }
            }
            return user;
        }
        public static InfoUser GetInfoUser(HttpContext contex) {

            string SessionID = contex.Session.Id;
            string userid = CurrentUserId(contex);
            string username = CurrentUsername(contex);
            string CSessionId = "";
            if (contex.Request.Cookies[COOKIES_KEY_SESSIONID] != null && contex.Request.Cookies[COOKIES_KEY_SESSIONID].ToString() != "")
            {
                CSessionId = contex.Request.Cookies[COOKIES_KEY_SESSIONID].ToString();
            }
            if (CSessionId != "" && SessionID!= CSessionId)
            {
                OrderedDictionary param = new OrderedDictionary();
                param["session_id"] = CSessionId;
                string sqlceck = "select a.*" +
                    " ,case when c.nama_lengkap is not null then c.nama_lengkap else b.fullname end as fullname " +
                    " ,stuff ((select ',' + cast(d.group_id as varchar(20)) from sys_user_group as d left outer join sys_groups as e on d.group_id=e.id where d.userid=a.userid for XML PATH('') ), 1,1,'') AS group_id " +
                    " ,stuff ((select ', '+ e.nama from sys_user_group as d left outer join sys_groups as e on d.group_id=e.id where d.userid=a.userid for XML PATH('') ), 1,1,'') AS group_name " +
                    " from [sys_user_online] as a " +
                    " left outer join sys_users as b on a.userid=b.userid " +
                    " left outer join ta_mp as c on a.userid=c.nrp " +
                    " where a.[session_id]=@session_id ";
                DataTable dt = SqlHelper.GetDataTable(sqlceck, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SessionID = contex.Session.Id;
                    userid = dt.Rows[0]["userid"].ToString();
                    username = dt.Rows[0]["username"].ToString();
                    string fullname = dt.Rows[0]["fullname"].ToString();
                    string group_id = dt.Rows[0]["group_id"].ToString();
                    string group_name = dt.Rows[0]["group_name"].ToString();
                    contex.Session.SetString(SESSION_KEY_USERID, userid);
                    contex.Session.SetString(SESSION_KEY_USERNAME, username);
                    contex.Session.SetString(SESSION_KEY_FULLNAME, fullname);
                    contex.Session.SetString(SESSION_KEY_GROUPID_LIST, group_id);
                    contex.Session.SetString(SESSION_KEY_GROUPNAME_LIST, group_name);
                    string data_param = dt.Rows[0]["data"].ToString();
                    UserParam userParam = JsonConvert.DeserializeObject<UserParam>(data_param);
                    //userParam.RoleCode;
                    contex.Session.SetString(SESSION_KEY_ROLE_CODE_LIST, userParam.RoleCode);
                    contex.Session.SetString(SESSION_KEY_ROLE_NAME_LIST, userParam.RoleName);
                    foreach (RuleParam item in userParam.Rules) {
                        contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + item.kode, item.kode);
                    }
                    param = new OrderedDictionary();
                    param["session_id_old"] = CSessionId;
                    param["session_id_new"] = contex.Session.Id;
                    string sqlupdate = "UPDATE [sys_user_online] set [session_id]=@session_id_new where [session_id]=@session_id_old ";
                    SqlHelper.ExecuteNonQuery(sqlupdate, param);
                    contex.Response.Cookies.Delete(COOKIES_KEY_SESSIONID);
                    contex.Response.Cookies.Append(COOKIES_KEY_SESSIONID, contex.Session.Id);
                    //=================
                    int SessionTimeOut = int.Parse(ConfigHelper.GetValue("SessionTimeOut"));
                    OrderedDictionary parameter = new OrderedDictionary();
                    parameter["CurrentDateTime"] = DateTime.Now;
                    parameter["SessionTimeOut"] = SessionTimeOut;
                    string sql = "Delete from sys_user_online where remember_me=0 and DATEDIFF(minute, last_visit,getdate() )> @SessionTimeOut";
                    SqlHelper.ExecuteNonQuery(sql, parameter);

                    OrderedDictionary param2 = new OrderedDictionary();
                    param2["session_id"] = SessionID;
                    string sql2 = "SELECT count(*) as jml from sys_user_online where session_id = @session_id";
                    int jumlah = SqlHelper.ExecuteScalarInt(sql2, param2);
                    if (jumlah == 1)
                    {
                        OrderedDictionary param4 = new OrderedDictionary();
                        param4["session_id"] = SessionID;
                        param4["userid"] = userid;
                        param4["username"] = username;
                        param4["host_address"] = contex.Request.Host.ToString();
                        param4["user_agent"] = contex.Request.Headers["User-Agent"].ToString();
                        param4["Uri"] = contex.Request.Path.ToString();
                        param4["current_page"] = contex.Request.Path.ToString();
                        param4["last_visit"] = DateTime.Now;
                        //param4["Data"] = "";
                        string sqlUpdate4 = " UPDATE sys_user_online SET userid=@userid,username=@username,host_address=@host_address,"
                                            + "	 uri=@uri,current_page=@current_page,last_visit=@last_visit where session_id=@session_id";
                        SqlHelper.ExecuteNonQuery(sqlUpdate4, param4);

                    }
                }
            }
            InfoUser data = new InfoUser();
            data.userid = CurrentUserId(contex);
            data.username = CurrentUsername(contex);
            data.fullname = CurrentFullname(contex);
            data.groupid = CurrentGroupId(contex);
            data.groupname = CurrentGroupName(contex);
            data.rolename = CurrentRoleName(contex);
            data.person = GetPersonData(contex);
            return data;
        }
        public static string Encript(string text)
        {
            byte[] byteArray = Encoding.Default.GetBytes(text);

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] hash = provider.ComputeHash(byteArray);

            long hashValue = BitConverter.ToInt64(hash, 0);
            return (Convert.ToDecimal(hashValue)).ToString();
        }

        public static ProsesResult SignIn(string username_frm, string password_frm, bool RememberMe,HttpContext contex) {
            string AuthenticateType = Settings.GetAppSetting("AuthenticateType") != null ? Settings.GetAppSetting("AuthenticateType"): "internal";
            string remember_me = RememberMe == true ? "1" : "0";
            ProsesResult result = new ProsesResult();
            result.status = 1;
            result.title = "";
            result.message = "";
            string pwdHash = Encript(password_frm);
            if (AuthenticateType == "internal")
            {
                result = CheckLoginAttack(contex);
                if (result.status != 1)
                {
                    return result;
                }
                OrderedDictionary param = new OrderedDictionary();
                param["username"] = username_frm;
                string sql = "select * from sys_users where username=@username ";
                DataTable dt = SqlHelper.GetDataTable(sql, param);
                //Incorrect Username
                if (dt.Rows.Count == 0)
                {
                    UpdateLoginAttack(contex);
                    result.status = 2;
                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                    result.message = ResxHelper.GetValue("Login", "IncorrectUsernameOrPassword", "Username atau Password tidak sesuai");
                    return result;
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    //check login attempt
                    string trackFailedAttempts = ConfigHelper.GetValue("TrackFailedAttempts", contex);
                    if (trackFailedAttempts == "1")
                    {
                        int maximumFailedAttempts = ConfigHelper.GetValue("MaximumFailedAttempts", contex) != "" ? int.Parse(ConfigHelper.GetValue("MaximumFailedAttempts", contex)) : 3;
                        int failedAttempsTime = ConfigHelper.GetValue("MaximumFailedAttempts", contex) != "" ? int.Parse(ConfigHelper.GetValue("MaximumFailedAttempts", contex)) : 0;
                        int attempCount = dr["attemp_count"].ToString() != "" ? int.Parse(dr["attemp_count"].ToString()) : 0;
                        DateTime attempTime = dr["attemp_time"].ToString() != "" ? (DateTime)dr["attemp_time"] : DateTime.MinValue;
                        TimeSpan ts = DateTime.Now - attempTime;
                        if (attempCount >= maximumFailedAttempts && ts.Minutes < failedAttempsTime)
                        {
                            result.status = 2;
                            result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                            result.message = ResxHelper.GetValue("Login", "CanNotLogin", "Tidak dapat Login ") + failedAttempsTime.ToString() + " Minutes";
                            return result;
                        }

                    }
                    if (result.status == 1)
                    {
                        // Check Password
                        if (dr["password"].ToString() != pwdHash)
                        {
                            UpdateLoginAttack(contex);
                            param = new OrderedDictionary();
                            param["username"] = username_frm;
                            string updateSql = "update sys_users set attemp_count=(select isnull(max(attemp_count),0) + 1 from sys_users  where username=@username ),attemp_time=getdate()  where username=@username ";
                            SqlHelper.ExecuteNonQuery(updateSql, param);
                            result.status = 2;
                            result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                            result.message = ResxHelper.GetValue("Login", "IncorrectUsernameOrPassword", "Username atau Password tidak sesuai");
                            return result;
                        }
                        else
                        {
                            param = new OrderedDictionary();
                            param["username"] = username_frm;
                            string updateSql = "update sys_users set attemp_count=0,attemp_time=null where username=@username ";
                            SqlHelper.ExecuteNonQuery(updateSql, param);
                            //============================
                            // Check User expired
                            if (dr["user_expired"].ToString() != "")
                            {
                                DateTime userExpired = (DateTime)dr["user_expired"];
                                int selisih = DateTime.Compare(DateTime.Now, userExpired);
                                if (selisih > 0)
                                {
                                    result.status = 2;
                                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                                    result.message = ResxHelper.GetValue("Login", "UsernameHasExpired") + " at " + userExpired.ToString(ResxHelper.GetValue("Format", "DateFormat2"));
                                    return result;
                                }
                            }
                            // Check User Block
                            if (result.status == 1 && dr["block"].ToString() == "1")
                            {
                                result.status = 2;
                                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                                result.message = ResxHelper.GetValue("Login", "UsernameHasBeenBlock");
                                return result;
                            }

                            if (result.status == 1)
                            {
                                // Check AllowConcurentLogin
                                string countuser = dr["allow_concurrent_login"].ToString();
                                if (countuser == "-1" || countuser == "")
                                {
                                    countuser = ConfigHelper.GetValue("AllowConcurentLogin", contex);
                                }
                                int icountuser = int.Parse(countuser);
                                //Check Current User Online
                                DeleteOnlineUserHasExpired(contex);
                                param = new OrderedDictionary();
                                param["userid"] = dr["userid"].ToString();
                                param["remember_me"] = remember_me;

                                string sqlCheck = "Select * from sys_user_online where userid=@userid and remember_me=@remember_me ";
                                DataTable dtU = SqlHelper.GetDataTable(sqlCheck, param);
                                int icountdb = dtU.Rows.Count;
                                if (icountdb >= icountuser)
                                {
                                    //string overrideSessionLogin = ConfigHelper.GetValue("OverrideSessionLogin", contex);
                                    //if (dtU.Rows[0]["host_address"].ToString() != contex.Request.Host.ToString())
                                    //{
                                    //    if (overrideSessionLogin == "0")
                                    //    {
                                    //        result.Succeeded = false;
                                    //        result.Message = ResxHelper.GetValue("Login", "AlreadyLogin") + " from <b>" + dtU.Rows[0]["host_address"].ToString() + "</b>";
                                    //    }
                                    //    else if (overrideSessionLogin == "1")
                                    //    {
                                    //        //SetAttributeUser(dr["userid"].ToString(), contex);
                                    //        result.ReturnUrl = "~/Account/ConfirmOverideUser?userid=" + dr["userid"].ToString();
                                    //    }
                                    //    else
                                    //    {
                                    //        string sqlUpdate = " Delete from sys_user_online where userid=" + dr["userid"].ToString();
                                    //        SqlHelper.ExecuteNonQuery(sqlUpdate);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    string sqlUpdate = " Delete from sys_user_online where userid=" + dr["userid"].ToString();
                                    //    SqlHelper.ExecuteNonQuery(sqlUpdate);
                                    //}
                                    param = new OrderedDictionary();
                                    param["userid"] = dr["userid"].ToString();
                                    param["remember_me"] = remember_me;
                                    string sqlUpdate = " Delete from sys_user_online where userid=@userid and remember_me=@remember_me ";
                                    SqlHelper.ExecuteNonQuery(sqlUpdate, param);
                                }

                            }
                            if (result.status == 1)
                            {
                                contex.Session.SetString(SESSION_KEY_USERID, dr["userid"].ToString());
                                //contex.Session.SetString(SESSION_KEY_DOMAINNAME, namaDomain);
                                contex.Session.SetString(SESSION_KEY_USERNAME, username_frm);
                                contex.Session.SetString(SESSION_KEY_PASSWORD, pwdHash);
                                contex.Session.SetString(SESSION_KEY_FULLNAME, dr["fullname"].ToString());
                                contex.Session.SetString(SESSION_KEY_REMEMBER_ME, RememberMe == true ? "1" : "0");
                                contex.Session.SetString(SESSION_KEY_HAS_CHANGE_PASSWORD, dr["has_change_password"].ToString());
                                contex.Session.SetString(SESSION_KEY_MUST_CHANGE_PASSWORD, dr["must_change_password"].ToString());
                                string enableChangePassword = dr["enable_change_password"].ToString() != "-1" ? dr["enable_change_password"].ToString() : ConfigHelper.GetValue("EnableChangePassword", contex);
                                contex.Session.SetString(SESSION_KEY_ENABLE_CHANGE_PASSWORD, enableChangePassword);
                                UpdateSession(contex);
                                //SetRuleByUser(contex, dr["userid"].ToString());
                                //SetRoleByUser(contex, dr["userid"].ToString());
                                SetRoleRuleByUser(contex, dr["userid"].ToString());
                            }
                        }
                    }
                }
            }
            else if (AuthenticateType == "eksternal")
            {
                //======================
                result = CheckLoginAttack(contex);
                if (result.status != 1)
                {
                    return result;
                }
                //====================
                UserData user = LoginApi(username_frm, password_frm);
                // TEST DATA ----------
                //UserData user = new UserData();
                //user.UserID = "80100041";
                //user.Username = "TRI EDI SULISTYO";
                //user.IsActive = true;
                //List<UserRole> userRoles = new List<UserRole>();
                //UserRole ur = new UserRole();
                //ur.UserRolePK = 5;
                //ur.UserRoleID = "TCH";
                //ur.UserRoleName = "TC Head";
                //userRoles.Add(ur);
                //user.UserRoles = userRoles;
                //----------------------------                 

                if (user != null && user.Username != null && user.UserRoles != null && user.UserRoles.Count > 0)
                {
                    string userId = user.UserID;
                    string userName = user.UserID;
                    string userNameExt = user.Username;
                    bool IsActive = user.IsActive;
                    OrderedDictionary param = new OrderedDictionary();
                    param["username"] = userName;
                    string sql = "select * from sys_users where username=@username ";
                    DataTable dtUser = SqlHelper.GetDataTable(sql, param);
                    string userid = "";
                    if (dtUser.Rows.Count == 0)
                    {
                        userid = UsersModel.GetNewId(userName).ToString();
                        param = new OrderedDictionary();
                        param["userid"] = userid;
                        param["username"] = userName;
                        param["username_ext"] = userNameExt;
                        sql = "insert into sys_users (userid,username,username_ext) values (@userid,@username,@username_ext) ";
                        SqlHelper.ExecuteNonQuery(sql, param);
                    }
                    else
                    {
                        userid = dtUser.Rows[0]["userid"].ToString();
                        if (dtUser.Rows[0]["username_ext"].ToString() != userNameExt)
                        {
                            param = new OrderedDictionary();
                            param["userid"] = userid;
                            param["username_ext"] = userNameExt;
                            sql = "update sys_users set username_ext=@username_ext where userid=@userid ";
                            SqlHelper.ExecuteNonQuery(sql, param);
                        }
                    }
                    param = new OrderedDictionary();
                    param["nrp"] = userName;
                    sql = "select * from ta_mp where nrp=@nrp  ";
                    DataTable dtPerson = SqlHelper.GetDataTable(sql, param);
                    string fullname = userName;
                    if (dtPerson.Rows.Count > 0)
                    {
                        DataRow drPerson = dtPerson.Rows[0];
                        fullname = drPerson["nama_lengkap"].ToString();
                    }
                    List<String> roles = new List<String>();
                    string roleCode = "";
                    foreach (UserRole item in user.UserRoles)
                    {
                        roleCode += roleCode != "" ? "," : "";
                        roleCode += item.UserRoleID;
                        roles.Add(item.UserRoleID);
                    }

                    contex.Session.SetString(SESSION_KEY_USERID, userid);
                    contex.Session.SetString(SESSION_KEY_USERNAME, userName);
                    contex.Session.SetString(SESSION_KEY_FULLNAME, fullname);
                    contex.Session.SetString(SESSION_KEY_REMEMBER_ME, RememberMe == true ? "1" : "0");
                    UpdateSession(contex);
                    //SetRuleByRoleCode(contex, roleCode);
                    //SetRoleCode(contex, roleCode);
                    SetRoleRuleByCode(contex, roleCode);
                }
                else
                {
                    UpdateLoginAttack(contex);
                    result.status = 2;
                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                    result.message = ResxHelper.GetValue("Login", "IncorrectUsernameOrPassword", "Username atau Password tidak sesuai");
                    return result;
                }
            }

            return result;
        }
        public static ProsesResult CheckLoginAttack(HttpContext contex) {
            ProsesResult result = new ProsesResult();
            result.status = 1;
            result.title = "";
            result.message = "";
            //check login attempt
            string SessionID = "";
            if (!contex.Request.Cookies.ContainsKey(cookiesClienKey))
            {
                SessionID = contex.Session.Id;
                contex.Response.Cookies.Append(cookiesClienKey, SessionID);
            }
            else
            {
                SessionID = contex.Request.Cookies[cookiesClienKey].ToString();
            }
            OrderedDictionary param_session_id = new OrderedDictionary();
            param_session_id["session_id"] = SessionID;
            string sql = "select * from sys_user_attack where session_id=@session_id ";
            DataTable dtUser = SqlHelper.GetDataTable(sql, param_session_id);
            if (dtUser.Rows.Count > 0)
            {
                DataRow dr = dtUser.Rows[0];
                string trackFailedAttempts = ConfigHelper.GetValue("TrackFailedAttempts", contex);
                if (trackFailedAttempts == "1")
                {
                    int maximumFailedAttempts = ConfigHelper.GetValue("MaximumFailedAttempts", contex) != "" ? int.Parse(ConfigHelper.GetValue("MaximumFailedAttempts", contex)) : 3;
                    int failedAttempsTime = ConfigHelper.GetValue("FailedAttempsTime", contex) != "" ? int.Parse(ConfigHelper.GetValue("FailedAttempsTime", contex)) : 0;
                    int attempCount = dr["attemp_count"].ToString() != "" ? int.Parse(dr["attemp_count"].ToString()) : 0;
                    DateTime attempTime = dr["attemp_time"].ToString() != "" ? (DateTime)dr["attemp_time"] : DateTime.MinValue;
                    TimeSpan ts = DateTime.Now - attempTime;
                    if (attempCount >= maximumFailedAttempts && ts.Minutes < failedAttempsTime)
                    {
                        result.status = 2;
                        result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                        result.message = ResxHelper.GetValue("Login", "CanNotBeLogin", "Tidak dapat login selama ") + failedAttempsTime.ToString() + " "+ ResxHelper.GetValue("Message", "Minutes", "menit");
                        return result;
                    }
                }
            }
            return result;
        }
        public static void UpdateLoginAttack(HttpContext contex)
        {
            string SessionID = "";
            if (!contex.Request.Cookies.ContainsKey(cookiesClienKey))
            {
                SessionID = contex.Session.Id;
                contex.Response.Cookies.Append(cookiesClienKey, SessionID);
            }
            else
            {
                SessionID = contex.Request.Cookies[cookiesClienKey].ToString();
            }
            OrderedDictionary param_session_id = new OrderedDictionary();
            param_session_id["session_id"] = SessionID;
            string sql = "select * from sys_user_attack where session_id=@session_id ";
            DataTable dtUser = SqlHelper.GetDataTable(sql, param_session_id);
            if (dtUser.Rows.Count > 0)
            {
                DataRow dr = dtUser.Rows[0];
                param_session_id = new OrderedDictionary();
                param_session_id["session_id"] = SessionID;
                param_session_id["attemp_count"] = int.Parse(dr["attemp_count"].ToString()) + 1;
                sql = "update sys_user_attack set attemp_count=@attemp_count, attemp_time=getdate() where session_id=@session_id ";
                SqlHelper.ExecuteNonQuery(sql, param_session_id);
            }
            else {
                param_session_id = new OrderedDictionary();
                param_session_id["session_id"] = SessionID;
                param_session_id["attemp_count"] = 1;
                sql = "insert into sys_user_attack (session_id,attemp_count,attemp_time) values (@session_id,@attemp_count,getdate())";
                SqlHelper.ExecuteNonQuery(sql, param_session_id);
            }
        }
        public static void SetRoleRuleByUser(HttpContext contex, string userId)
        {
            if (userId != "")
            {
                OrderedDictionary param = new OrderedDictionary();
                param["userid"] = userId;
                string sql = "SELECT DISTINCT sys_rules.kode "
                + " FROM sys_user_group "
                + " LEFT OUTER JOIN sys_groups on sys_user_group.group_id = sys_groups.id "
                + " LEFT OUTER JOIN sys_role_rule ON sys_groups.role_id = sys_role_rule.role_id "
                + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                + " WHERE sys_user_group.userid=@userid ";
                DataTable dtRule = SqlHelper.GetDataTable(sql, param);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + drRule["kode"].ToString(), drRule["kode"].ToString());
                }

                // SET ROLE
                param = new OrderedDictionary();
                param["userId"] = userId;
                sql = "select distinct r.kode,r.nama from sys_user_group as ug inner join sys_groups as g on ug.group_id=g.id " +
                " inner join sys_roles as r on g.role_id = r.id " +
                " where ug.userid = @userId ";
                DataTable dtRole = SqlHelper.GetDataTable(sql, param);
                string sc_roleCode = "";
                string sc_roleNama = "";
                foreach (DataRow dr in dtRole.Rows)
                {
                    sc_roleCode += sc_roleCode != "" ? ", " : "";
                    sc_roleCode += dr["kode"].ToString();
                    sc_roleNama += sc_roleNama != "" ? ", " : "";
                    sc_roleNama += dr["nama"].ToString();
                    contex.Session.SetString(SESSION_KEY_ROLE_LIST + "_" + dr["kode"].ToString(), dr["kode"].ToString());
                }
                contex.Session.SetString(SESSION_KEY_ROLE_CODE_LIST, sc_roleCode);
                contex.Session.SetString(SESSION_KEY_ROLE_NAME_LIST, sc_roleNama);

                string rule_data = JsonConvert.SerializeObject(dtRule);
                string data_param = "{\"RoleCode\" : \""+ sc_roleCode + "\", \"RoleName\" : \"" + sc_roleNama + "\", \"Rules\" : " + rule_data + "}";
                param = new OrderedDictionary();
                param["session_id"] = contex.Session.Id;
                param["data_param"] = data_param;
                sql = "update sys_user_online set data=@data_param where [session_id]=@session_id";
                SqlHelper.ExecuteNonQuery(sql, param);
            }
            
        }
        public static void SetRuleByUser(HttpContext contex,string userId) {
            if (userId != "") {
                OrderedDictionary param = new OrderedDictionary();
                param["userid"] = userId;
                string sql = "SELECT DISTINCT sys_rules.kode "
                + " FROM sys_user_group "
                + " LEFT OUTER JOIN sys_groups on sys_user_group.group_id = sys_groups.id "
                + " LEFT OUTER JOIN sys_role_rule ON sys_groups.role_id = sys_role_rule.role_id "
                + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                + " WHERE sys_user_group.userid=@userid ";
                DataTable dtRule = SqlHelper.GetDataTable(sql, param);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + drRule["kode"].ToString(), drRule["kode"].ToString());
                }
            }
        }
        public static void SetRuleByGroup(HttpContext contex, string groupId)
        {
            if (groupId != "")
            {
                OrderedDictionary param = new OrderedDictionary();
                param["groupId"] = groupId;
                string sql = "SELECT DISTINCT sys_rules.kode  "
                + " FROM sys_groups "
                + " LEFT OUTER JOIN sys_role_rule ON sys_groups.role_id = sys_role_rule.role_id "
                + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                + " WHERE sys_groups.id in (@groupId) ";
                DataTable dtRule = SqlHelper.GetDataTable(sql, param);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + drRule["kode"].ToString(), drRule["kode"].ToString());
                }
            }
        }
        public static void SetRuleByRole(HttpContext contex, string roleId)
        {
            if (roleId != "")
            {
                OrderedDictionary param = new OrderedDictionary();
                param["roleId"] = roleId;
                string sql = "SELECT DISTINCT sys_rules.kode "
                + " FROM sys_role_rule "
                + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                + " where sys_role_rule.role_id in  (@roleId) ";
                DataTable dtRule = SqlHelper.GetDataTable(sql, param);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + drRule["kode"].ToString(), drRule["kode"].ToString());
                }
            }
        }
        public static void SetRuleByRoleCode(HttpContext contex, string roleCode)
        {
            if (roleCode != "")
            {
                string[] roleCodes = roleCode.Split(',');
                string sc_roleCode = "";
                foreach (string item in roleCodes)
                {
                    sc_roleCode += sc_roleCode != "" ? ", " : "";
                    sc_roleCode += "'" + item.Trim() + "'";
                }                

                string sql = "SELECT DISTINCT sys_rules.kode "
                + " FROM sys_role_rule "
                + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                + " LEFT JOIN sys_roles ON sys_role_rule.role_id = sys_roles.id "
                + " where sys_roles.kode in  (" + sc_roleCode + ") ";
                DataTable dtRule = SqlHelper.GetDataTable(sql);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + drRule["kode"].ToString(), drRule["kode"].ToString());
                }
            }
        }
        public static void SetRoleRuleByCode(HttpContext contex, string roleCode)
        {
            if (roleCode != "")
            {
                string[] roleCodes = roleCode.Split(',');
                string sc_roleCode = "";
                foreach (string item in roleCodes)
                {
                    sc_roleCode += sc_roleCode != "" ? ", " : "";
                    sc_roleCode += "'" + item.Trim() + "'";
                }

                string sql = "SELECT DISTINCT sys_rules.kode "
                + " FROM sys_role_rule "
                + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                + " LEFT JOIN sys_roles ON sys_role_rule.role_id = sys_roles.id "
                + " where sys_roles.kode in  (" + sc_roleCode + ") ";
                DataTable dtRule = SqlHelper.GetDataTable(sql);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + drRule["kode"].ToString(), drRule["kode"].ToString());
                }
                //===== ROLE
                sql = "select kode, nama from sys_roles where kode in (" + sc_roleCode + ")";
                DataTable dtRole = SqlHelper.GetDataTable(sql);
                string var_roleCode = "";
                string var_roleNama = "";
                foreach (DataRow dr in dtRole.Rows)
                {
                    var_roleCode += var_roleCode != "" ? ", " : "";
                    var_roleCode += dr["kode"].ToString();
                    var_roleNama += var_roleNama != "" ? ", " : "";
                    var_roleNama += dr["nama"].ToString();
                    contex.Session.SetString(SESSION_KEY_ROLE_LIST + "_" + dr["kode"].ToString(), dr["kode"].ToString());
                }
                contex.Session.SetString(SESSION_KEY_ROLE_CODE_LIST, var_roleCode);
                contex.Session.SetString(SESSION_KEY_ROLE_NAME_LIST, var_roleNama);

                var rule_data = JsonConvert.SerializeObject(dtRule);
                string data_param = "{\"RoleCode\" : \"" + var_roleCode + "\", \"RoleName\" : \"" + var_roleNama + "\", \"Rules\" : " + rule_data + "}";
                OrderedDictionary param = new OrderedDictionary();
                param["session_id"] = contex.Session.Id;
                param["data_param"] = data_param;
                sql = "update sys_user_online set data=@data_param where [session_id]=@session_id";
                SqlHelper.ExecuteNonQuery(sql, param);
            }
        }

        public static void SetRoleByUser(HttpContext contex, string userId)
        {
            if (userId != "")
            {
                OrderedDictionary param = new OrderedDictionary();
                param["userId"] = userId;
                string sql = "select distinct r.kode,r.nama from sys_user_group as ug inner join sys_groups as g on ug.group_id=g.id " +
                " inner join sys_roles as r on g.role_id = r.id " +
                " where ug.userid = @userId ";
                DataTable dtRule = SqlHelper.GetDataTable(sql, param);
                string sc_roleCode = "";
                string sc_roleNama = "";
                foreach (DataRow dr in dtRule.Rows)
                {
                    sc_roleCode += sc_roleCode != "" ? ", " : "";
                    sc_roleCode += dr["kode"].ToString();
                    sc_roleNama += sc_roleNama != "" ? ", " : "";
                    sc_roleNama += dr["nama"].ToString();
                    contex.Session.SetString(SESSION_KEY_ROLE_LIST + "_" + dr["kode"].ToString(), dr["kode"].ToString());
                }
                contex.Session.SetString(SESSION_KEY_ROLE_CODE_LIST, sc_roleCode);
                contex.Session.SetString(SESSION_KEY_ROLE_NAME_LIST, sc_roleNama);
            }
        }
        public static void SetRoleCode(HttpContext contex, string roleCode)
        {
            if (roleCode != "")
            {
                string[] roleCodes = roleCode.Split(',');
                string sc_roleCode = "";
                foreach (string item in roleCodes)
                {
                    sc_roleCode += sc_roleCode != "" ? ", " : "";
                    sc_roleCode += "'" + item.Trim() + "'";
                }
                string sql = "select kode, nama from sys_roles where kode in ("+ sc_roleCode + ")";
                DataTable dtRule = SqlHelper.GetDataTable(sql);
                string var_roleCode = "";
                string var_roleNama = "";
                foreach (DataRow dr in dtRule.Rows)
                {
                    var_roleCode += var_roleCode != "" ? ", " : "";
                    var_roleCode += dr["kode"].ToString();
                    var_roleNama += var_roleNama != "" ? ", " : "";
                    var_roleNama += dr["nama"].ToString();
                    contex.Session.SetString(SESSION_KEY_ROLE_LIST + "_" + dr["kode"].ToString(), dr["kode"].ToString());
                }
                contex.Session.SetString(SESSION_KEY_ROLE_CODE_LIST, var_roleCode);
                contex.Session.SetString(SESSION_KEY_ROLE_NAME_LIST, var_roleNama);
            }
        }
        public static void UpdateSession(HttpContext contex)
        {
            string currentUserId = contex.Session.GetString(SESSION_KEY_USERID) != null ? contex.Session.GetString(SecurityHelper.SESSION_KEY_USERID).ToString() : "";
            string currentUsername = contex.Session.GetString(SESSION_KEY_USERNAME) != null ? contex.Session.GetString(SecurityHelper.SESSION_KEY_USERNAME).ToString() : "";
            string currentRememberMe = contex.Session.GetString(SESSION_KEY_REMEMBER_ME) != null ? contex.Session.GetString(SecurityHelper.SESSION_KEY_REMEMBER_ME).ToString() : "";
            if (currentUsername != "")
            {
                //int SessionTimeOut = contex.Session.Timeout;
                int SessionTimeOut = ConfigHelper.GetValue("SessionTimeOut") != null ? int.Parse(ConfigHelper.GetValue("SessionTimeOut")) : 20;
                string sqlUpdate1 = " Delete from sys_user_online where remember_me=0 and DATEDIFF(minute, last_visit,@CurrentDateTime )> @SessionTimeOut ;";

                OrderedDictionary param1 = new OrderedDictionary();
                param1["CurrentDateTime"] = DateTime.Now;
                param1["SessionTimeOut"] = SessionTimeOut;
                SqlHelper.ExecuteNonQuery(sqlUpdate1, param1);

                OrderedDictionary param2 = new OrderedDictionary();
                param2["session_id"] = contex.Session.Id;
                int jumlah = SqlHelper.ExecuteScalarInt("SELECT count(*) as jml from sys_user_online where session_id = @session_id", param2);
                if (jumlah == 0)
                {
                    OrderedDictionary param3 = new OrderedDictionary();
                    param3["session_id"] = contex.Session.Id;
                    param3["userid"] = currentUserId;
                    param3["username"] = currentUsername;
                    param3["remember_me"] = currentRememberMe;
                    param3["host_address"] = contex.Request.Host.ToString();
                    param3["login_time"] = DateTime.Now;
                    param3["user_agent"] = contex.Request.Headers["User-Agent"].ToString();
                    param3["uri"] = contex.Request.Path.ToString();
                    param3["current_page"] = contex.Request.Path.ToString();
                    param3["last_visit"] = DateTime.Now;
                    //param3["data"] = "";
                    string sqlUpdate3 = " insert into sys_user_online (session_id,userid,username,remember_me,host_address,login_time,user_agent,uri,current_page,last_visit) values "
                    + " (@session_id,@userid,@username,@remember_me,@host_address,@login_time,@user_agent,@uri,@current_page,@last_visit) ";
                    SqlHelper.ExecuteNonQuery(sqlUpdate3, param3);
                }
                else
                {
                    OrderedDictionary param4 = new OrderedDictionary();
                    param4["session_id"] =contex.Session.Id;
                    param4["userid"] = currentUserId;
                    param4["host_address"] = contex.Request.Host.ToString();
                    param4["user_agent"] = contex.Request.Headers["User-Agent"].ToString();
                    param4["uri"] = contex.Request.Path.ToString();
                    param4["current_page"] = contex.Request.Path.ToString();
                    param4["last_visit"] = DateTime.Now;
                    //param4["data"] = "";
                    string sqlUpdate4 = " UPDATE sys_user_online SET userid=@userid,host_address=@host_address,user_agent=@user_agent,"
                    + "	 uri=@uri,current_page=@current_page,last_visit=@last_visit where session_id=@session_id";
                    SqlHelper.ExecuteNonQuery(sqlUpdate4, param4);
                }
            }
            if (currentRememberMe == "1")
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddYears(1);
                contex.Response.Cookies.Append(COOKIES_KEY_SESSIONID, contex.Session.Id, option);
            }
            else {
                if (contex.Request.Cookies[COOKIES_KEY_SESSIONID] != null && contex.Request.Cookies[COOKIES_KEY_SESSIONID].ToString() != "") {
                    string sesId = contex.Request.Cookies[COOKIES_KEY_SESSIONID].ToString();
                    OrderedDictionary parameter = new OrderedDictionary();
                    parameter["session_id"] = sesId;
                    string sqlUpdate = "delete from sys_user_online where session_id = @session_id ;";
                    SqlHelper.ExecuteNonQuery(sqlUpdate, parameter);
                    contex.Response.Cookies.Delete(COOKIES_KEY_SESSIONID);
                }                
            }
        }

        public static void DeleteOnlineUserHasExpired(HttpContext contex)
        {
            int SessionTimeOut = ConfigHelper.GetValue("SessionTimeOut") != null ? int.Parse(ConfigHelper.GetValue("SessionTimeOut")) : 20;
            string sqlUpdate = "delete from sys_user_online where remember_me=0 and DATEDIFF(minute, last_visit,@CurrentDateTime )> @SessionTimeOut ;";
            OrderedDictionary parameter = new OrderedDictionary();
            parameter["CurrentDateTime"] = DateTime.Now;
            parameter["SessionTimeOut"] = SessionTimeOut;
            SqlHelper.ExecuteNonQuery(sqlUpdate, parameter);
        }
        public static void Logout(HttpContext contex)
        {
            contex.Response.Cookies.Delete(COOKIES_KEY_SESSIONID);
            contex.Session.Clear();
            string sqlUpdate = "delete from sys_user_online where session_id = @session_id ;";
            OrderedDictionary parameter = new OrderedDictionary();
            parameter["session_id"] = contex.Session.Id;
            SqlHelper.ExecuteNonQuery(sqlUpdate, parameter);
            DeleteOnlineUserHasExpired(contex);
        }
        public static string CurrentUserId(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_USERID) != null)
                return contex.Session.GetString(SESSION_KEY_USERID).ToString();
            else
                return "";
        }
        public static string CurrentUsername(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_USERNAME) != null)
                return contex.Session.GetString(SESSION_KEY_USERNAME).ToString();
            else
                return "";
        }
        public static string CurrentFullname(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_FULLNAME) != null)
                return contex.Session.GetString(SESSION_KEY_FULLNAME).ToString();
            else
                return "";
        }
        //CurrentGroupName
        public static string CurrentGroupId(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_GROUPID_LIST) != null)
                return contex.Session.GetString(SESSION_KEY_GROUPID_LIST).ToString();
            else
                return "";
        }
        public static string CurrentGroupName(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_GROUPNAME_LIST) != null)
                return contex.Session.GetString(SESSION_KEY_GROUPNAME_LIST).ToString();
            else
                return "";
        }
        public static string CurrentRoleName(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_ROLE_NAME_LIST) != null)
                return contex.Session.GetString(SESSION_KEY_ROLE_NAME_LIST).ToString();
            else
                return "";
        }
        public static string CurrentRoleCode(HttpContext contex)
        {
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_ROLE_CODE_LIST) != null)
                return contex.Session.GetString(SESSION_KEY_ROLE_CODE_LIST).ToString();
            else
                return "";
        }
        
        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }
        public static PersonData GetPersonData(HttpContext contex)
        {
            PersonData pdata = new PersonData();
            if (contex.Session != null && contex.Session.GetString(SESSION_KEY_PERSON_DATA) != null) {
                string persondata = contex.Session.GetString(SESSION_KEY_PERSON_DATA);
                pdata = JsonConvert.DeserializeObject<PersonData>(persondata);
                return pdata;
            }
            else {
                string username = CurrentUsername(contex);
                if (IsNumeric(username))
                {
                    OrderedDictionary param = new OrderedDictionary();
                    param["username"] = username;
                    string sql = "SELECT a.[id] " +
                    " ,a.karyawan_nonkaryawan_id as person_type_id " +
                    " ,pt.nama as person_type_nama " +
                    " ,a.company_id " +
                    " ,c.kode as company_kode " +
                    " ,c.nama as company_nama " +
                    " ,a.nrp " +
                    " ,a.nama_lengkap as nama " +
                    " ,a.jenis_kelamin_id " +
                    " ,jk.nama as jenis_kelamin_nama " +
                    " ,a.tempat_lahir " +
                    " ,a.tgl_lahir " +
                    " ,ea.id as area_id " +
                    " ,ea.kode as area_kode " +
                    " ,ea.nama as area_nama " +
                    " ,a.business_area_id as ba_id " +
                    " ,ba.kode as ba_kode " +
                    " ,ba.nama as ba_nama " +
                    " ,a.personal_area_id as pa_id " +
                    " ,pa.kode as pa_kode " +
                    " ,pa.nama as pa_nama " +
                    " ,a.personal_sub_area_id as psa_id " +
                    " ,psa.kode as psa_kode " +
                    " ,psa.nama as psa_nama " +
                    " ,a.tgl_masuk_kerja as mulai_bekerja " +
                    " ,a.tgl_akhir_kerja as akhir_bekerja" +
                    " FROM[dbo].[ta_mp] as a " +
                    " left outer join ref_literal_data as pt on a.karyawan_nonkaryawan_id = pt.id and pt.cat_id=147 " +
                    " left outer join ref_literal_data as jk on a.jenis_kelamin_id = jk.id and jk.cat_id = 5 " +
                    " left outer join ref_company as c on a.company_id=c.id " +
                    " left outer join ref_business_area as ba on a.business_area_id=ba.id " +
                    " left outer join ref_personal_area as pa on a.personal_area_id=pa.id " +
                    " left outer join ref_personal_sub_area as psa on a.personal_sub_area_id=psa.id " +
                    " left outer join ref_ehs_area ea on psa.ehs_area_id=ea.id  " +
                    " where a.nrp= @username ";
                    DataTable dt = SqlHelper.GetDataTable(sql,param);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        pdata.id = dr["id"].ToString();
                        pdata.person_type_id = dr["person_type_id"].ToString();
                        pdata.person_type_nama = dr["person_type_nama"].ToString();
                        pdata.company_id = dr["company_id"].ToString();
                        pdata.company_kode = dr["company_kode"].ToString();
                        pdata.company_nama = dr["company_nama"].ToString();
                        pdata.nrp = dr["nrp"].ToString();
                        pdata.nama = dr["nama"].ToString();
                        pdata.jenis_kelamin_id = dr["jenis_kelamin_id"].ToString();
                        pdata.jenis_kelamin_nama = dr["jenis_kelamin_nama"].ToString();
                        pdata.tempat_lahir = dr["tempat_lahir"].ToString();
                        pdata.tgl_lahir = dr["tgl_lahir"].ToString();
                        pdata.area_id = dr["area_id"].ToString();
                        pdata.area_kode = dr["area_kode"].ToString();
                        pdata.area_nama = dr["area_nama"].ToString();
                        pdata.ba_id = dr["ba_id"].ToString();
                        pdata.ba_kode = dr["ba_kode"].ToString();
                        pdata.ba_nama = dr["ba_nama"].ToString();
                        pdata.pa_id = dr["pa_id"].ToString();
                        pdata.pa_kode = dr["pa_kode"].ToString();
                        pdata.pa_nama = dr["pa_nama"].ToString();
                        pdata.psa_id = dr["psa_id"].ToString();
                        pdata.psa_kode = dr["psa_kode"].ToString();
                        pdata.psa_nama = dr["psa_nama"].ToString();
                        pdata.mulai_bekerja = dr["mulai_bekerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["mulai_bekerja"]) : "";
                        pdata.akhir_bekerja = dr["akhir_bekerja"].ToString() != "" ? String.Format("{0:dd/MM/yyyy}", dr["akhir_bekerja"]) : "";
                        contex.Session.SetString(SESSION_KEY_PERSON_DATA, JsonConvert.SerializeObject(pdata));
                    }
                }
                return pdata;
            }
        }

        public static void SetRuleByGroupId(string groupid, HttpContext contex) {
            if (groupid != "") {
                OrderedDictionary param = new OrderedDictionary();
                param["groupid"] = groupid;
                string sql = "  select "
                + " distinct d.kode "
                + " from sys_groups as a "
                + " inner join sys_roles as b on a.roleid = b.id "
                + " inner join sys_role_rule as c on b.id = c.roleid "
                + " inner join sys_rules as d on c.ruleid = d.id "
                + " where a.groupid in(@groupid) ";
                DataTable data = SqlHelper.GetDataTable(sql, param);
                foreach (DataRow dr in data.Rows) {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + dr["kode"].ToString(), SESSION_KEY_RULE_LIST + "_" + dr["kode"].ToString());
                }
            }
             
        }
        public static void SetRuleByRoleId(string roleid, HttpContext contex)
        {
            if (roleid != "")
            {
                OrderedDictionary param = new OrderedDictionary();
                param["roleid"] = roleid;
                string sql = "  select "
                + " distinct d.kode "
                + " FROM sys_roles as b "
                + " inner join sys_role_rule as c on b.id = c.roleid "
                + " inner join sys_rules as d on c.ruleid = d.id "
                + " where b.id in(@roleid) ";
                DataTable data = SqlHelper.GetDataTable(sql,param);
                foreach (DataRow dr in data.Rows)
                {
                    contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + dr["kode"].ToString(), SESSION_KEY_RULE_LIST + "_" + dr["kode"].ToString());
                }
            }

        }
        public static bool onPageInit(HttpContext contex) {
            bool result = false;
            string SessionID = contex.Session.Id;
            string userid = CurrentUserId(contex);
            string username = CurrentUsername(contex);
            string CSessionId = "";
            if (contex.Request.Cookies[COOKIES_KEY_SESSIONID] != null && contex.Request.Cookies[COOKIES_KEY_SESSIONID].ToString() != "")
            {
                CSessionId = contex.Request.Cookies[COOKIES_KEY_SESSIONID].ToString();
            }
            if (CSessionId != "" && SessionID!= CSessionId) {
                OrderedDictionary param = new OrderedDictionary();
                param["session_id"] = CSessionId;
                string sqlceck = "select a.*" +
                    " ,case when c.nama_lengkap is not null then c.nama_lengkap else b.fullname end as fullname " +
                    " ,stuff ((select ',' + cast(d.group_id as varchar(20)) from sys_user_group as d left outer join sys_groups as e on d.group_id=e.id where d.userid=a.userid for XML PATH('') ), 1,1,'') AS group_id " +
                    " ,stuff ((select ', '+ e.nama from sys_user_group as d left outer join sys_groups as e on d.group_id=e.id where d.userid=a.userid for XML PATH('') ), 1,1,'') AS group_name " +
                    " from [sys_user_online] as a " +
                    " left outer join sys_users as b on a.userid=b.userid " +
                    " left outer join ta_mp as c on a.userid=c.nrp " +
                    " where a.[session_id]=@session_id ";
                DataTable dt = SqlHelper.GetDataTable(sqlceck, param);
                if (dt != null && dt.Rows.Count > 0) {
                    SessionID = contex.Session.Id;
                    userid = dt.Rows[0]["userid"].ToString();
                    username = dt.Rows[0]["username"].ToString();
                    string fullname = dt.Rows[0]["fullname"].ToString();
                    string group_id = dt.Rows[0]["group_id"].ToString();
                    string group_name = dt.Rows[0]["group_name"].ToString();
                    contex.Session.SetString(SESSION_KEY_USERID, userid);
                    contex.Session.SetString(SESSION_KEY_USERNAME, username);
                    contex.Session.SetString(SESSION_KEY_FULLNAME, fullname);
                    contex.Session.SetString(SESSION_KEY_GROUPID_LIST, group_id);
                    contex.Session.SetString(SESSION_KEY_GROUPNAME_LIST, group_name);
                    string data_param = dt.Rows[0]["data"].ToString();
                    UserParam userParam = JsonConvert.DeserializeObject<UserParam>(data_param);
                    //userParam.RoleCode;
                    contex.Session.SetString(SESSION_KEY_ROLE_CODE_LIST, userParam.RoleCode);
                    contex.Session.SetString(SESSION_KEY_ROLE_NAME_LIST, userParam.RoleName);
                    foreach (RuleParam item in userParam.Rules)
                    {
                        contex.Session.SetString(SESSION_KEY_RULE_LIST + "_" + item.kode, item.kode);
                    }
                    param = new OrderedDictionary();
                    param["session_id_old"] = CSessionId;
                    param["session_id_new"] = contex.Session.Id;
                    string sqlupdate = "UPDATE [sys_user_online] set [session_id]=@session_id_new where [session_id]=@session_id_old ";
                    SqlHelper.ExecuteNonQuery(sqlupdate, param);
                    contex.Response.Cookies.Delete(COOKIES_KEY_SESSIONID);
                    contex.Response.Cookies.Append(COOKIES_KEY_SESSIONID, contex.Session.Id);
                }
            }
            
            int SessionTimeOut = int.Parse(ConfigHelper.GetValue("SessionTimeOut"));
            OrderedDictionary parameter = new OrderedDictionary();
            parameter["CurrentDateTime"] = DateTime.Now;
            parameter["SessionTimeOut"] = SessionTimeOut;
            string sql = "Delete from sys_user_online where remember_me=0 and DATEDIFF(minute, last_visit,getdate() )> @SessionTimeOut";
            SqlHelper.ExecuteNonQuery(sql, parameter);

            OrderedDictionary param2 = new OrderedDictionary();
            param2["session_id"] = SessionID;
            string sql2 = "SELECT count(*) as jml from sys_user_online where session_id = @session_id";
            int jumlah = SqlHelper.ExecuteScalarInt(sql2, param2);
            if (jumlah == 1)
            {
                OrderedDictionary param4 = new OrderedDictionary();
                param4["session_id"] = SessionID;
                param4["userid"] = userid;
                param4["username"] = username;
                param4["host_address"] = contex.Request.Host.ToString();
                param4["user_agent"] = contex.Request.Headers["User-Agent"].ToString();
                param4["Uri"] = contex.Request.Path.ToString();
                param4["current_page"] = contex.Request.Path.ToString();
                param4["last_visit"] = DateTime.Now;
                //param4["Data"] = "";
                string sqlUpdate4 = " UPDATE sys_user_online SET userid=@userid,username=@username,host_address=@host_address,"
                                    + "	 uri=@uri,current_page=@current_page,last_visit=@last_visit where session_id=@session_id";
                SqlHelper.ExecuteNonQuery(sqlUpdate4, param4);
                result= true;
            }
            return result;
        }
        public static void onPageLoginInit(HttpContext contex)
        {
            string SessionID = "";
            if (!contex.Request.Cookies.ContainsKey(cookiesClienKey))
            {
                SessionID = contex.Session.Id;
                contex.Response.Cookies.Append(cookiesClienKey, SessionID);
            }
            else
            {
                SessionID = contex.Request.Cookies[cookiesClienKey].ToString();
            }
            
            int SessionTimeOut = int.Parse(ConfigHelper.GetValue("SessionTimeOut"));
            OrderedDictionary parameter = new OrderedDictionary();
            parameter["CurrentDateTime"] = DateTime.Now;
            parameter["SessionTimeOut"] = SessionTimeOut;
            string sql = "Delete from sys_user_attack where  DATEDIFF(minute, attemp_time,getdate() )> @SessionTimeOut";
            SqlHelper.ExecuteNonQuery(sql, parameter);

            OrderedDictionary param2 = new OrderedDictionary();
            param2["session_id"] = SessionID;
            string sql2 = "SELECT count(*) as jml from sys_user_attack where session_id = @session_id";
            int jumlah = SqlHelper.ExecuteScalarInt(sql2, param2);
            if (jumlah == 0)
            {
                OrderedDictionary param4 = new OrderedDictionary();
                param4["session_id"] = SessionID;
                param4["attemp_count"] = 0;
                param4["attemp_time"] = DateTime.Now;
                string sqlUpdate4 = " insert into sys_user_attack (session_id,attemp_count,attemp_time) values (@session_id,@attemp_count,@attemp_time) ";
                SqlHelper.ExecuteNonQuery(sqlUpdate4, param4);
            }
        }

        public static bool HasRule(HttpContext contex, string rolename)
        {
            string role_name = SecurityHelper.SESSION_KEY_RULE_LIST + "_" + rolename;
            if (contex.Session.GetString(role_name) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool HasRole(HttpContext contex,string rolename) {
            string role_name = SecurityHelper.SESSION_KEY_ROLE_LIST + "_" + rolename;
            if (contex.Session.GetString(role_name) != null)
            {
                return true;
            }
            else {
                return false;
            }
        }
        public static string EncryptString(string text) {
            var encryptedText = new AESCrypt(passPhrase, iv).Encrypt(text);
            return encryptedText;
        }
        public static string DecryptString(string text)
        {
            var sourceText = new AESCrypt(passPhrase, iv).Decrypt(text);
            return sourceText;
        }

        public static bool IsHeadArea(string PersonId)
        {
            bool result = false;
            if (PersonId != null && PersonId != "") {
                string sql = "select count(1) as jml from ref_ehs_area where head_id=" + PersonId;
                int jml = SqlHelper.ExecuteScalarInt(sql);
                if (jml > 0) {
                    result = true;
                }
            }
            return result;
        }
        public static bool IsHeadPSA(string PersonId)
        {
            bool result = false;
            if (PersonId != null && PersonId != "")
            {
                string sql = "select count(1) as jml from ref_personal_sub_area where head_id=" + PersonId;
                int jml = SqlHelper.ExecuteScalarInt(sql);
                if (jml > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        public static AreaData GetAreaOwner(string PersonId)
        {
            AreaData area = new AreaData();
            area.ehs_area_id = "";
            area.ehs_area_kode = "";
            area.ehs_area_nama = "";
            area.ba_id = "";
            area.ba_kode = "";
            area.ba_nama = "";
            area.pa_id = "";
            area.pa_kode = "";
            area.pa_nama = "";
            area.psa_id = "";
            area.psa_kode = "";
            area.psa_nama = "";
            if (PersonId != null && PersonId != "")
            {
                string sql = "select " + 
                    " psa.ehs_area_id  " +
                    " ,area.kode as ehs_area_kode " +
                    " ,area.nama as ehs_area_nama " +
                    " ,psa.ba_id " +
                    " ,ba.kode as ba_kode " +
                    " ,ba.nama as ba_nama " +
                    " ,psa.pa_id " +
                    " ,pa.kode as pa_kode " +
                    " ,pa.nama as pa_nama " +
                    " ,psa.id as psa_id " +
                    " ,psa.kode as psa_kode " +
                    " ,psa.nama as psa_nama " +
                    " from ref_personal_sub_area as psa " +
                    " left outer join ref_ehs_area as area on psa.ehs_area_id = area.id " +
                    " left outer join ref_business_area as ba on psa.ba_id = ba.id " +
                    " left outer join ref_personal_area as pa on psa.pa_id = pa.id " +
                    " WHERE psa.head_id=" + PersonId;
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    area.ehs_area_id = dr["ehs_area_id"].ToString();
                    area.ehs_area_kode = dr["ehs_area_kode"].ToString();
                    area.ehs_area_nama = dr["ehs_area_nama"].ToString();
                    area.ba_id = dr["ba_id"].ToString();
                    area.ba_kode = dr["ba_kode"].ToString();
                    area.ba_nama = dr["ba_nama"].ToString();
                    area.pa_id = dr["pa_id"].ToString();
                    area.pa_kode = dr["pa_kode"].ToString();
                    area.pa_nama = dr["pa_nama"].ToString();
                    area.psa_id = dr["psa_id"].ToString();
                    area.psa_kode = dr["psa_kode"].ToString();
                    area.psa_nama = dr["psa_nama"].ToString();
                }
                else {
                    sql = "select " +
                    " area.id as ehs_area_id  " +
                    " ,area.kode as ehs_area_kode " +
                    " ,area.nama as ehs_area_nama " +
                    " ,null as ba_id " +
                    " ,'' as ba_kode " +
                    " ,'' as ba_nama " +
                    " ,null as pa_id " +
                    " ,'' as pa_kode " +
                    " ,'' as pa_nama " +
                    " ,null as psa_id " +
                    " ,'' as psa_kode " +
                    " ,'' as psa_nama " +
                    " from ref_ehs_area as area " +
                    " WHERE area.head_id=" + PersonId;
                    dt = SqlHelper.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        area.ehs_area_id = dr["ehs_area_id"].ToString();
                        area.ehs_area_kode = dr["ehs_area_kode"].ToString();
                        area.ehs_area_nama = dr["ehs_area_nama"].ToString();
                        area.ba_id = dr["ba_id"].ToString();
                        area.ba_kode = dr["ba_kode"].ToString();
                        area.ba_nama = dr["ba_nama"].ToString();
                        area.pa_id = dr["pa_id"].ToString();
                        area.pa_kode = dr["pa_kode"].ToString();
                        area.pa_nama = dr["pa_nama"].ToString();
                        area.psa_id = dr["psa_id"].ToString();
                        area.psa_kode = dr["psa_kode"].ToString();
                        area.psa_nama = dr["psa_nama"].ToString();
                    }
                }
            }
            return area;
        }

    }
}
