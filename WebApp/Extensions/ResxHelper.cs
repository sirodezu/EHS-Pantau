using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace WebApp
{
    public class ResxHelper
    {
        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        public static string CurrentCultureName
        {
            get { return System.Threading.Thread.CurrentThread.CurrentCulture.Name; }
        }
        public static string AddSpacesToSentence(string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
        public static string GetValue(string className, string key)
        {
            string defaultValue = "";
            return GetValue(className, key, defaultValue);
        }
        public static string GetValue(string className, string key,string defaultValue)
        {
            Hashtable messages = GetResource(className);
            if (messages[key] == null)
            {
                string sqlselect = "select key_value from sys_resources where lang_code=@lang_code and class_name=@class_name and key_name=@key_name";
                OrderedDictionary parameter = new OrderedDictionary();
                parameter["lang_code"] = CurrentCultureName;
                parameter["class_name"] = className;
                parameter["key_name"] = key;
                string valuedb = SqlHelper.ExecuteScalarString(sqlselect, parameter);
                if (valuedb != null && valuedb != "")
                {
                    messages[key] = valuedb;
                }
                else {
                    if (defaultValue == "")
                    {
                        string newValue = AddSpacesToSentence(key, true);
                        newValue = newValue.Replace("_", " ");
                        newValue = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(newValue.ToLower());
                        messages[key] = newValue;
                    }
                    else
                    {
                        messages[key] = defaultValue;
                    }
                    string sqlUpdate = "INSERT INTO [sys_resources]([lang_code],[class_name],[key_name],[key_value]) VALUES(@lang_code,@class_name,@key_name,@key_value);";
                    parameter = new OrderedDictionary();
                    parameter["lang_code"] = CurrentCultureName;
                    parameter["class_name"] = className;
                    parameter["key_name"] = key;
                    parameter["key_value"] = messages[key];
                    SqlHelper.ExecuteNonQuery(sqlUpdate, parameter);
                    messages = GetResource(className);
                }
                
            }
            return (string)messages[key];
        }
        public static string ReadFile(string FileName)
        {
            if (File.Exists(FileName)) {
                try
                {
                    using (StreamReader reader = File.OpenText(FileName))
                    {
                        string fileContent = reader.ReadToEnd();
                        if (fileContent != null && fileContent != "")
                        {
                            return fileContent;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log
                    throw ex;
                }
            }
            return null;
        }
        private static Hashtable GetResource(string className)
        {
            //string currentCulture = CurrentCultureName;
            //string defaultCulture = CultureInfo.CurrentCulture.ToString();

            //string data = ReadFile("./wwwroot/Resources/" + CurrentCultureName + "/" + className + ".json");
            //Hashtable resource = new Hashtable();
            //if (data != null)
            //{
            //    resource = JsonConvert.DeserializeObject<Hashtable>(data);
            //}
            //else
            //{
            //    resource = LoadResource(className, currentCulture);
            //    var logPath = System.IO.Path.GetFullPath("./wwwroot/Resources/" + CurrentCultureName + "/" + className + ".json");
            //    if (File.Exists(logPath))
            //    {
            //        File.Delete(logPath);
            //    }
            //    var logFile = System.IO.File.Create(logPath);
            //    var logWriter = new System.IO.StreamWriter(logFile);
            //    string output_data = JsonConvert.SerializeObject(resource).ToString();
            //    logWriter.WriteLine(output_data);
            //    logWriter.Dispose();
            //}
            //return resource;
            string currentCulture = CurrentCultureName;
            string chace_name = "Resx_" + CurrentCultureName + "_" + className;
            Hashtable resource = new Hashtable();
            // Look for cache key.
            if (!_cache.TryGetValue(chace_name, out resource))
            {
                resource = LoadResource(className, currentCulture);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(356));
                _cache.Set(chace_name, resource, cacheEntryOptions);
            }
            return resource;
        }
        public static Hashtable LoadResource(string className, string culture)
        {
            Hashtable resource =new Hashtable();
            string sqlSelect = " SELECT [class_name],[key_name],[key_value] ";
            sqlSelect += " FROM [dbo].[sys_resources] ";
            sqlSelect += " Where lang_code='" + culture + "' ";
            sqlSelect += " AND class_name='" + className + "' ";
            DataTable dt = SqlHelper.GetDataTable(sqlSelect);
            foreach (DataRow dr in dt.Rows)
            {
                resource[dr["key_name"].ToString()] = dr["key_value"].ToString();
            }
            return resource;
        }
        public static void ClearResource(string className)
        {
            string sqlSelect = "select distinct lang_code,class_name from sys_resources ";
            if (className != "")
            {
                sqlSelect = " WHERE class_name='" + className + "'";
            }
            DataTable dt = SqlHelper.GetDataTable(sqlSelect);
            foreach (DataRow dr in dt.Rows)
            {
                //string filePath = Path.GetFullPath("./wwwroot/Resources/" + dr["lang_code"].ToString() + "/" + dr["class_name"] + ".json");
                //if (File.Exists(filePath))
                //{
                //    File.Delete(filePath);
                //}
                string chace_name = "Resx_" + CurrentCultureName + "_" + className;
                _cache.Remove(chace_name);
            }
        }
        public static void ClearCahce()
        {
            string sqlSelect = "select distinct lang_code,class_name from sys_resources ";
            DataTable dt = SqlHelper.GetDataTable(sqlSelect);
            foreach (DataRow dr in dt.Rows)
            {
                string chace_name = "Resx_" + dr["lang_code"] + "_" + dr["class_name"];
                _cache.Remove(chace_name);
            }
        }
        public static void ReloadResource(string className)
        {
            ClearResource(className);
            string sqlSelect = "select distinct lang_code,class_name from sys_resources ";
            if (className != "")
            {
                sqlSelect += " WHERE class_name='" + className + "'";
            }
            DataTable dt = SqlHelper.GetDataTable(sqlSelect);
            foreach (DataRow dr in dt.Rows)
            {
                //Hashtable resource = LoadResource(className, dr["lang_code"].ToString());
                //var logPath = System.IO.Path.GetFullPath("./wwwroot/Resources/" + dr["lang_code"].ToString() + "/" + className + ".json");
                //var logFile = System.IO.File.Create(logPath);
                //var logWriter = new System.IO.StreamWriter(logFile);
                //string output_data = JsonConvert.SerializeObject(resource).ToString();
                //logWriter.WriteLine(output_data);
                //logWriter.Dispose();
                Hashtable resource = LoadResource(className, dr["lang_code"].ToString());
                string chace_name = "Resx_" + dr["lang_code"].ToString() + "_" + className;
                _cache.Remove(chace_name);
                if (!_cache.TryGetValue(chace_name, out resource))
                {
                    resource = LoadResource(className, dr["lang_code"].ToString());
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                       .SetSlidingExpiration(TimeSpan.FromDays(356));
                    _cache.Set(chace_name, resource, cacheEntryOptions);
                }

            }

        }
        public static string HtmlEncode(string value)
        {
            return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }
        public static string HtmlDecode(string value)
        {
            return value.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");
        }

    }
}
