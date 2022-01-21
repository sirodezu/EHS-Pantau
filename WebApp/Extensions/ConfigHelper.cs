using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;

namespace WebApp
{
    public class ConfigHelper
    {
        public static string GetValue(string keyName, HttpContext contex)
        {
            string AppCodePrefix = Settings.GetAppSetting("AppCode").ToString()+"_";
            string result = "";
            if (contex.Session.GetString(AppCodePrefix + keyName) == null || contex.Session.GetString(AppCodePrefix + keyName).ToString() == "")
            {
                if (Settings.GetAppSetting(keyName) != null)
                {
                    result = Settings.GetAppSetting(keyName).ToString();
                }
                else
                {
                    DataTable dt = SqlHelper.GetDataTable("Select value from sys_config where Name ='" + keyName + "'");
                    if (dt.Rows.Count == 1)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                }
                contex.Session.SetString(AppCodePrefix + keyName, result);
            }
            else
            {
                result = contex.Session.GetString(AppCodePrefix + keyName).ToString();
            }
            return result;
        }
        public static string GetValue(string keyName)
        {
            string result = "";
            if (Settings.GetAppSetting(keyName) != null)
            {
                result = Settings.GetAppSetting(keyName).ToString();
            }
            else
            {
                DataTable dt = SqlHelper.GetDataTable("Select value from sys_config where Name ='" + keyName + "'");
                if (dt.Rows.Count == 1)
                {
                    result = dt.Rows[0][0].ToString();
                }
                else
                {
                    result = "";
                }
            }
            return result;
        }
        public static string GetSessionName(string keyName)
        {
            string AppCodePrefix = ConfigHelper.GetValue("AppCode")+"_";
            string result = AppCodePrefix + keyName;
            return result;
        }
        public static void SetSessionValue(string keyName, object obj, HttpContext contex)
        {
            string AppCode = ConfigHelper.GetValue("AppCode");
            string sessionName = AppCode + keyName;
            contex.Session.SetString(sessionName, JsonConvert.SerializeObject(obj));
        }
        public static string GetSessionValue(string keyName, HttpContext contex)
        {
            string AppCodePrefix = ConfigHelper.GetValue("AppCode")+"_";
            string sessionName = AppCodePrefix + keyName;
            string result = "";
            if (contex.Session.GetString(sessionName) != null && contex.Session.GetString(sessionName).ToString() != "")
            {
                result = contex.Session.GetString(sessionName).ToString();
            }
            return result;
        }
        public static object GetSessionData(string keyName, HttpContext contex)
        {
            string AppCodePrefix = ConfigHelper.GetValue("AppCode")+"_";
            string sessionName = AppCodePrefix + keyName;
            object result = "";
            if (contex.Session.GetString(sessionName) != null && contex.Session.GetString(sessionName).ToString() != "")
            {
                result = JsonConvert.DeserializeObject(contex.Session.GetString(sessionName).ToString()) ;
            }
            return result;
        }

        public static bool IsEnableLockData(string tableName, HttpContext contex)
        {
            bool result = false;
            string sessionName = ConfigHelper.GetSessionName("Config_LockData_" + tableName);
            if (contex.Session.GetString(sessionName) == null || contex.Session.GetString(sessionName).ToString() == "")
            {
                string strConfigValue = ConfigHelper.GetValue("EnableRecordLocking", contex);
                if (strConfigValue == "1")
                {
                    DataTable dt = SqlHelper.GetDataTable("Select lock_option from sys_recordlock_config where table_name ='" + tableName + "'");
                    if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "1")
                    {
                        contex.Session.SetString(sessionName, "1");
                        result = true;
                    }
                    else
                    {
                        contex.Session.SetString(sessionName, "0");
                        result = false;
                    }
                }
            }
            else
            {
                result = bool.Parse(contex.Session.GetString(sessionName).ToString());
            }

            return result;
        }
        public static bool IsEnableAudit(string tableName, HttpContext contex)
        {
            bool result = false;
            string sessionName = ConfigHelper.GetSessionName("Config_Audit_" + tableName);
            if (contex.Session.GetString(sessionName) == null || contex.Session.GetString(sessionName).ToString() == "")
            {
                string strConfigValue = ConfigHelper.GetValue("EnableAuditTrail", contex);
                if (strConfigValue == "1")
                {
                    DataTable dt = SqlHelper.GetDataTable("SELECT audit_option FROM sys_audit_config WHERE table_name='" + tableName + "'");
                    if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "1")
                    {
                        contex.Session.SetString(sessionName, "1");
                        result = true;
                    }
                    else
                    {
                        contex.Session.SetString(sessionName, "0");
                        result = false;
                    }
                }
            }
            else
            {
                result = bool.Parse(contex.Session.GetString(sessionName).ToString());
            }

            return result;
        }

        public static string GetDatabaseName(HttpContext contex)
        {
            string result = "";
            string sessionName = ConfigHelper.GetSessionName("Config_Databasename");
            if (contex.Session.GetString(sessionName) == null || contex.Session.GetString(sessionName).ToString() == "")
            {
                string[] connStrings = SqlHelper.ConnString.Split(';');
                for (int i = 0; i < connStrings.Length; i++)
                {
                    string[] itemConn = connStrings[i].Split('=');
                    if (itemConn[0].Trim().ToLower() == "database")
                    {
                        result = itemConn[1].Trim().ToLower();
                    }
                }

                contex.Session.SetString(sessionName, result);
            }
            else
            {
                result = contex.Session.GetString(sessionName).ToString();
            }
            return result;
        }

        public static string GetEntityId(HttpContext contex)
        {
            string result = "";
            string sessionName = ConfigHelper.GetSessionName("Config_EntityId");
            if (contex.Session.GetString(sessionName) == null || contex.Session.GetString(sessionName).ToString() == "")
            {
                string decriptCode = SecurityHelper.DecryptString(GetValue("LicenseCode", contex));
                string[] decriptCodes = decriptCode.Split(',');
                result = decriptCodes[0] != null && decriptCodes[0] != "" ? decriptCodes[0] : "0";
                contex.Session.SetString(sessionName, result);
            }
            else
            {
                result = contex.Session.GetString(sessionName).ToString();
            }
            return result;
        }

    }
}
