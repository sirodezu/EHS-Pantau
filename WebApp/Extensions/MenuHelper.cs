using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class MenuHelper
    {
        public static string GetMenu(HttpContext context) {
            string appCode = Settings.GetAppSetting("AppCode");
            string sessName = appCode+ "_Menus";
            string objMenus = context.Session.GetString(sessName);
            if (objMenus == null)
            {
                string userid = SecurityHelper.CurrentUserId(context);
                string rule_id = GetRuleByUserId(context);
                string baseUrl = context.Request.Scheme + "://" + context.Request.Host + "" + context.Request.PathBase + "/";
                IList<MenuParam> result = new List<MenuParam>();
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += " WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id is null ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "") {
                    sql += " or  b.rule_id  in ("+ rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                string menu_item = "";
                string class_active = "";
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    MenuParam item = new MenuParam();
                    item.id = dr["id"].ToString();
                    item.parent_id = dr["parent_id"].ToString();
                    item.name = dr["name"].ToString();
                    item.label_id = dr["label_id"].ToString();
                    item.label_en = dr["label_en"].ToString();
                    item.link = dr["link"].ToString();
                    string url = item.link != "" ? baseUrl + item.link : "#";
                    class_active = i == 0 ? "active" : "";
                    string item_child = GetMenuChild(context, rule_id,dr["id"].ToString());
                    if (item_child == "")
                    {
                        menu_item += "<li id=\"nav_menu_" + item.id + "\" class=\"nav-item " + class_active + "\"><a class=\"nav-link\" href=\"" + url + "\">" + item.name + "</a></li>";
                    }
                    else
                    {
                        menu_item += "<li class=\"nav-item dropdown\">";
                        menu_item += "<a class=\"nav-link dropdown-toggle\" href=\"#\" id=\"nav_menu_" + item.id + "\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">";
                        menu_item += item.name;
                        menu_item += "</a>";
                        menu_item += "<ul class=\"dropdown-menu\" aria-labelledby=\"nav_menu_" + item.id + "\">";
                        menu_item += item_child;
                        menu_item += "</ul>";
                        menu_item += "</li>";
                    }
                    i++;
                }
                objMenus = menu_item;
                context.Session.SetString(sessName,objMenus);
            }
            return objMenus;
        }
        public static string GetMenuChild(HttpContext context,string rule_id,string parent_id)
        {
            string baseUrl = context.Request.Scheme + "://" + context.Request.Host + "" + context.Request.PathBase + "/";
            //IList<MenuParam> result = new List<MenuParam>();
            string menu_item = "";
            if (parent_id != "") {
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += "WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id="+parent_id+"  ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MenuParam item = new MenuParam();
                        item.id = dr["id"].ToString();
                        item.parent_id = dr["parent_id"].ToString();
                        item.name = dr["name"].ToString();
                        item.label_id = dr["label_id"].ToString();
                        item.label_en = dr["label_en"].ToString();
                        item.link = dr["link"].ToString();
                        //item.items = GetMenuChild(context, item.id);
                        //result.Add(item);
                        string url = item.link != "" ? baseUrl + item.link : "#";
                        string item_child = GetMenuSubChild(context, rule_id, dr["id"].ToString());
                        if (item_child == "")
                        {
                            menu_item += "<li id=\"nav_menu_" + item.id + "\" class=\"dropdown-item\"><a class=\"nav-link\" href=\"" + url + "\">" + item.name + "</a></li>";
                        }
                        else
                        {
                            menu_item += "<li class=\"dropdown-submenu\">";
                            menu_item += "    <a class=\"dropdown-item dropdown-toggle\" href=\"#\">" + item.name + "</a>";
                            menu_item += "    <ul class=\"dropdown-menu\">";
                            menu_item += item_child;
                            menu_item += "    </ul>";
                            menu_item += "</li>";
                        }

                    }
                }
            }
            return menu_item;
        }
        public static string GetMenuSubChild(HttpContext context, string rule_id, string parent_id)
        {
            string baseUrl = context.Request.Scheme + "://" + context.Request.Host + "" + context.Request.PathBase + "/";
            //IList<MenuParam> result = new List<MenuParam>();
            string menu_item = "";
            if (parent_id != "")
            {
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += "WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id=" + parent_id + "  ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MenuParam item = new MenuParam();
                        item.id = dr["id"].ToString();
                        item.parent_id = dr["parent_id"].ToString();
                        item.name = dr["name"].ToString();
                        item.label_id = dr["label_id"].ToString();
                        item.label_en = dr["label_en"].ToString();
                        item.link = dr["link"].ToString();
                        //item.items = GetMenuChild(context, item.id);
                        //result.Add(item);
                        string url = item.link != "" ? baseUrl + item.link : "#";
                        string item_child = GetMenuSubChild(context, rule_id,dr["id"].ToString());
                        if (item_child == "")
                        {
                            menu_item += "<li id=\"nav_menu_" + item.id + "\"><a class=\"dropdown-item\" href=\"" + url + "\">" + item.name + "</a></li>";
                        }
                        else
                        {
                            menu_item += "<li class=\"dropdown-submenu\">";
                            menu_item += "    <a class=\"dropdown-item dropdown-toggle\" href=\"#\">" + item.name + "</a>";
                            menu_item += "    <ul class=\"dropdown-menu\">";
                            menu_item += item_child;
                            menu_item += "    </ul>";
                            menu_item += "</li>";
                        }

                    }
                }
            }
            return menu_item;
        }

        public static string GetMainMenu(HttpContext context)
        {
            string appCode = Settings.GetAppSetting("AppCode");
            string sessName = appCode + "_Menus";
            string objMenus = context.Session.GetString(sessName);
            if (objMenus == null)
            {
                string userid = SecurityHelper.CurrentUserId(context);
                string rule_id = rule_id = GetRuleByUserId(context);
                string baseUrl = context.Request.Scheme + "://" + context.Request.Host + "" + context.Request.PathBase + "/";
                string urlImg = baseUrl + "img/menu/";
                IList<MenuItem> result = new List<MenuItem>();
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,imgurl,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += " WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id is null ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    MenuItem item = new MenuItem();
                    item.text = dr["name"].ToString();
                    item.url = dr["link"].ToString() != "" ? baseUrl + dr["link"].ToString() : "";
                    item.imageUrl = dr["imgurl"].ToString() != "" ? urlImg + dr["imgurl"].ToString() : ""; 
                    IList<MenuItem> item_child = GetMainMenuChild(context, rule_id, dr["id"].ToString());
                    if (item_child.Count>0)
                    {
                        item.items = item_child;
                    }
                    result.Add(item);
                }
                objMenus = JsonConvert.SerializeObject(result);
                context.Session.SetString(sessName, objMenus);
            }
            return objMenus;
        }
        public static IList<MenuItem> GetMainMenuChild(HttpContext context, string rule_id, string parent_id)
        {
            string baseUrl = context.Request.Scheme + "://" + context.Request.Host + "" + context.Request.PathBase + "/";
            string urlImg = baseUrl + "img/menu/";
            IList<MenuItem> result = new List<MenuItem>();
            if (parent_id != "")
            {
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,imgurl,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += "WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id=" + parent_id + "  ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MenuItem item = new MenuItem();
                        item.text = dr["name"].ToString();
                        if (dr["link"].ToString() != "") {
                            item.url = baseUrl + dr["link"].ToString() ;
                        }
                        item.imageUrl = dr["imgurl"].ToString() != "" ? urlImg + dr["imgurl"].ToString() : "";
                        IList<MenuItem> item_child = GetMainMenuChild(context, rule_id, dr["id"].ToString());
                        if (item_child.Count>0)
                        {
                            item.items = item_child;
                        }
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public static string GetRuleByUserId(HttpContext context)
        {
            string userid = SecurityHelper.CurrentUserId(context);
            string rule_id = "";
            if (userid != "")
            {
                DataTable dtRule = new DataTable();
                string sql = "";
                if (userid == "-1")
                {
                    sql = "SELECT DISTINCT id as rule_id FROM sys_rules ";
                    dtRule = SqlHelper.GetDataTable(sql);
                }
                else
                {
                    if (Settings.GetAppSetting("AuthenticateType") == "internal")
                    {
                        sql = "SELECT DISTINCT sys_role_rule.rule_id "
                        + " FROM sys_user_group "
                        + " LEFT OUTER JOIN sys_groups on sys_user_group.group_id = sys_groups.id "
                        + " LEFT OUTER JOIN sys_role_rule ON sys_groups.role_id = sys_role_rule.role_id ";
                        sql += " WHERE sys_user_group.userid=" + userid + " ";
                        dtRule = SqlHelper.GetDataTable(sql);
                    }
                    else
                    {
                        string roleCode = SecurityHelper.CurrentRoleCode(context);
                        string[] roleCodes = roleCode.Split(',');
                        string sc_roleCode = "";
                        foreach (string item in roleCodes)
                        {
                            sc_roleCode += sc_roleCode != "" ? ", " : "";
                            sc_roleCode += "'" + item.Trim() + "'";
                        }
                        sql = "SELECT DISTINCT sys_role_rule.rule_id "
                        + " FROM sys_role_rule "
                        + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                        + " LEFT JOIN sys_roles ON sys_role_rule.role_id = sys_roles.id "
                        + " where sys_roles.kode in  (" + sc_roleCode + ") ";
                        dtRule = SqlHelper.GetDataTable(sql);
                    }

                }
                //DataTable dtRule = SqlHelper.GetDataTable(sql);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    rule_id += rule_id != "" ? "," : "";
                    rule_id += drRule["rule_id"];
                }
            }
            return rule_id;
        }
        public static string GetRuleByGroupId(HttpContext context,string groupId)
        {
            string userid = SecurityHelper.CurrentUserId(context);
            string rule_id = "";
            string sql = "";
            if (groupId != "" && userid != "-1")
            {
                sql = "SELECT DISTINCT sys_groups.rule_id  "
                + " FROM sys_groups "
                + " LEFT OUTER JOIN sys_role_rule ON sys_groups.role_id = sys_role_rule.role_id ";
                sql += " WHERE sys_groups.id in (" + groupId + ") ";
            }
            else {
                sql = "SELECT DISTINCT id as rule_id FROM sys_rules ";
            }
            DataTable dtRule = SqlHelper.GetDataTable(sql);
            foreach (DataRow drRule in dtRule.Rows)
            {
                rule_id += rule_id != "" ? "," : "";
                rule_id += drRule["rule_id"];
            }
            return rule_id;
        }
        public static string GetRuleByRoleId(HttpContext context,string roleId)
        {
            string userid = SecurityHelper.CurrentUserId(context);
            string rule_id = "";
            string sql = "";
            if (roleId != "" && userid != "-1")
            {
                sql = "SELECT DISTINCT sys_role_rule.rule_id "
                + " FROM sys_role_rule ";
                sql += " where sys_role_rule.role_id in  (" + roleId + ") ";
            }
            else
            {
                sql = "SELECT DISTINCT id as rule_id FROM sys_rules ";
            }
            DataTable dtRule = SqlHelper.GetDataTable(sql);
            foreach (DataRow drRule in dtRule.Rows)
            {
                rule_id += rule_id != "" ? "," : "";
                rule_id += drRule["rule_id"];
            }
            return rule_id;
        }
        public static string GetRuleByRoleKode(HttpContext context, string[] roleKode)
        {
            string userid = SecurityHelper.CurrentUserId(context);
            string rule_id = "";
            string sql = "";
            if (roleKode.Length > 0 && userid != "-1")
            {
                string kode = "";
                foreach (string item in roleKode) {
                    kode += kode != "" ? "," : "";
                    kode +=  "'"+ item + "'";
                }
                sql = "SELECT DISTINCT sys_role_rule.rule_id FROM sys_role_rule left outer join  sys_roles on sys_role_rule.role_id=sys_roles.id ";
                sql += " where sys_roles.kode in  (" + kode + ") ";
            }
            else
            {
                sql = "SELECT DISTINCT id as rule_id FROM sys_rules ";
            }
            if (sql != "") {
                DataTable dtRule = SqlHelper.GetDataTable(sql);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    rule_id += rule_id != "" ? "," : "";
                    rule_id += drRule["rule_id"];
                }
            }
            return rule_id;
        }
        public static string GetRuleByRoleKode(HttpContext context,string roleKode)
        {
            string userid = SecurityHelper.CurrentUserId(context);
            string rule_id = "";
            string sql = "";
            if (roleKode != "" && userid != "-1")
            {
                string[] kodeRole = roleKode.Split(',');
                string where_role = "";
                foreach (string item in kodeRole) {
                    if (item != "") {
                        where_role += where_role != "" ? "," : "";
                        where_role += "'"+item+"'";
                    }
                }
                if (where_role != "") {
                    sql = "SELECT DISTINCT sys_role_rule.rule_id "
                    + " FROM sys_role_rule "
                    + " LEFT JOIN sys_rules ON sys_role_rule.rule_id = sys_rules.id "
                    + " LEFT JOIN sys_roles ON sys_role_rule.role_id = sys_roles.id ";
                    sql += " where sys_roles.kode in  (" + where_role + ") ";
                }
            }
            else
            {
                sql = "SELECT DISTINCT id as rule_id FROM sys_rules ";
            }
            if (sql != "") {
                DataTable dtRule = SqlHelper.GetDataTable(sql);
                foreach (DataRow drRule in dtRule.Rows)
                {
                    rule_id += rule_id != "" ? "," : "";
                    rule_id += drRule["rule_id"];
                }
            }
            return rule_id;
        }
        public static string get_nav() {
            navigation data = new navigation();

            return JsonConvert.SerializeObject(data);
        }
        public static string GetNavMenu(HttpContext context)
        {
            //HttpContext context = _HttpContextAccessor.HttpContext;
            string appCode = Settings.GetAppSetting("AppCode");
            string sessName = appCode + "_Menus";
            string objMenus = context.Session.GetString(sessName);
            if (objMenus == null)
            {
                string baseUrl = WebHelper.GetBaseUrl(context);
                navigation data = new navigation();
                data.version = "4";
                IList<nav_item> lists = new List<nav_item>();
                string userid = SecurityHelper.CurrentUserId(context);
                string rule_id = GetRuleByUserId(context);
                
                IList<MenuParam> result = new List<MenuParam>();
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += " WHERE 1=1 and a.menu_type_id=1  and a.active_id=1 ";
                sql += " and a.parent_id is null ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    nav_item item = new nav_item();
                    item.title = System.Net.WebUtility.HtmlDecode(dr["name"].ToString());
                    item.icon = System.Net.WebUtility.HtmlDecode(dr["icon"].ToString());
                    if (dr["link"].ToString() != "")
                    {
                        item.href = baseUrl;
                        item.href += dr["link"].ToString().Substring(0, 1) != "/" ? "/" : "";
                        item.href += dr["link"].ToString().ToLower();
                    }
                    item.items = GetNavMenuChild(context, rule_id, dr["id"].ToString());
                    lists.Add(item);
                }
                data.lists = lists;
                objMenus = JsonConvert.SerializeObject(data);
                context.Session.SetString(sessName, objMenus);
            }
            return objMenus;
        }
        public static IList<nav_item> GetNavMenuChild(HttpContext context, string rule_id, string parent_id)
        {
            IList<nav_item> lists = new List<nav_item>();
            if (parent_id != "")
            {
                string baseUrl = WebHelper.GetBaseUrl(context);
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += "WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id=" + parent_id + "  ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nav_item item = new nav_item();
                        item.title = System.Net.WebUtility.HtmlDecode(dr["name"].ToString());
                        item.icon = System.Net.WebUtility.HtmlDecode(dr["icon"].ToString()); 
                        if (dr["link"].ToString() != "")
                        {
                            item.href = baseUrl;
                            item.href += dr["link"].ToString().Substring(0, 1) != "/" ? "/" : "";
                            item.href += dr["link"].ToString().ToLower();
                        }
                        item.items = GetNavMenuChild(context, rule_id, dr["id"].ToString());
                        lists.Add(item);

                    }
                }
            }
            return lists;
        }
        //===================
        public static string PwaGetNavMenu(HttpContext context)
        {
            //HttpContext context = _HttpContextAccessor.HttpContext;
            string appCode = Settings.GetAppSetting("AppCode");
            string sessName = appCode + "_Menus";
            string objMenus = context.Session.GetString(sessName);
            if (objMenus == null)
            {
                string baseUrl = WebHelper.GetBaseUrl(context);
                navigation data = new navigation();
                data.version = "4";
                IList<nav_item> lists = new List<nav_item>();
                string userid = SecurityHelper.CurrentUserId(context);
                string rule_id = GetRuleByUserId(context);

                IList<MenuParam> result = new List<MenuParam>();
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += " WHERE 1=1 and a.menu_type_id=2  and a.active_id=1 ";
                sql += " and a.parent_id is null ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    nav_item item = new nav_item();
                    item.title = System.Net.WebUtility.HtmlDecode(dr["name"].ToString());
                    item.icon = System.Net.WebUtility.HtmlDecode(dr["icon"].ToString());
                    if (dr["link"].ToString() != "")
                    {
                        item.href = baseUrl;
                        item.href += dr["link"].ToString().Substring(0, 1) != "/" ? "/" : "";
                        item.href += dr["link"].ToString().ToLower();
                    }
                    item.items = PwaGetNavMenuChild(context, rule_id, dr["id"].ToString());
                    lists.Add(item);
                }
                data.lists = lists;
                objMenus = JsonConvert.SerializeObject(data);
                context.Session.SetString(sessName, objMenus);
            }
            return objMenus;
        }
        public static IList<nav_item> PwaGetNavMenuChild(HttpContext context, string rule_id, string parent_id)
        {
            IList<nav_item> lists = new List<nav_item>();
            if (parent_id != "")
            {
                string baseUrl = WebHelper.GetBaseUrl(context);
                string sql = "select distinct a.id,a.parent_id,name,label_id,label_en,icon,link,ordinal from sys_menus as a ";
                sql += " left outer join sys_menu_rule as b on a.id = b.menu_id ";
                sql += "WHERE 1=1 and a.active_id=1 ";
                sql += " and a.parent_id=" + parent_id + "  ";
                sql += " and (";
                sql += " b.rule_id is null ";
                if (rule_id != "")
                {
                    sql += " or  b.rule_id  in (" + rule_id + ") ";
                }
                sql += " ) ";
                sql += " order by a.ordinal ";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nav_item item = new nav_item();
                        item.title = System.Net.WebUtility.HtmlDecode(dr["name"].ToString());
                        item.icon = System.Net.WebUtility.HtmlDecode(dr["icon"].ToString());
                        if (dr["link"].ToString() != "")
                        {
                            item.href = baseUrl;
                            item.href += dr["link"].ToString().Substring(0, 1) != "/" ? "/" : "";
                            item.href += dr["link"].ToString().ToLower();
                        }
                        item.items = PwaGetNavMenuChild(context, rule_id, dr["id"].ToString());
                        lists.Add(item);

                    }
                }
            }
            return lists;
        }

    }
    public class MenuParam {
        public string id { get; set; }
        public string parent_id { get; set; }
        public string ordinal { get; set; }
        public string name { get; set; }
        public string label_id { get; set; }
        public string label_en { get; set; }
        public string link { get; set; }
        public IList<MenuParam> items { get; set; }
    }
    public class MenuItem
    {
        public string text { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public IList<MenuItem> items { get; set; }
    }

    public class navigation
    {
        public string version { get; set; }
        public IList<nav_item> lists { get; set; }
    }
    public class nav_item
    {
        public string title { get; set; }
        public string icon { get; set; }
        public string href { get; set; }
        public Boolean showOnSeed { get; set; }
        public IList<nav_item> items { get; set; }
        public nav_span span { get; set; }
    }
    public class nav_span {
        public string span_class { get; set; }
        public string span_text { get; set; }
    }

    
      
}
