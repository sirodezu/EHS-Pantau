using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DataModel
    {
        //string att = DataModel.ReadFileJson("sys_roles");
        //IList<GridColumn> column_att = JsonConvert.DeserializeObject<IList<GridColumn>>(att);
        public enum ActionType { Add, Edit, Delete, Select, SelectAll, Login, Logout };
        public static string GetOperatorValue(string type,string opr, string fieldValue)
        {
            string nilai = fieldValue.Replace("'", "''");
            string quwot = type == "number" ? "" : "'";
            string hasil = "=";
            if (opr == "eq"){
                hasil = " = "+ quwot+ nilai + quwot;
            }else if (opr == "neq"){
                hasil = " <> " + quwot + nilai + quwot;
            }
            else if (opr == "startswith"){
                hasil = " like " + quwot + nilai +"%"+ quwot;
            }
            else if (opr == "contains"){
                hasil = " like " + quwot +"%"+ nilai + "%" + quwot;
            }
            else if (opr == "doesnotcontain"){
                hasil = " not like " + quwot + "%" + nilai + "%" + quwot;
            }
            else if (opr == "endswith"){
                hasil = " like " + quwot + "%" + nilai + quwot;
            }
            else if (opr == "gte"){
                hasil = " >= " + quwot + nilai + quwot;
            }
            else if (opr == "gt"){
                hasil = " > " + quwot + nilai + quwot;
            }
            else if (opr == "lte"){
                hasil = " <= " + quwot + nilai + quwot;
            }
            else if (opr == "lt"){
                hasil = " < " + quwot + nilai + quwot;
            }
            else if (opr == "i"){
                hasil = " is null  ";
            }else if (opr == "inn"){
                hasil = " is not null  ";
            }
            return hasil;
        }
        public static string ReadFileJson(string className)
        {
            string file_path = "./wwwroot/Resources/parameter/" + className + ".json";
            if (File.Exists(file_path))
            {
                try
                {
                    using (StreamReader reader = File.OpenText(file_path))
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
        public static OrderedDictionary DtToDictionary(DataTable dt) {
            OrderedDictionary param = new OrderedDictionary();
            if (dt.Rows.Count > 0) {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    param[dt.Columns[i].ColumnName] = dt.Rows[0][dt.Columns[i].ColumnName];//.ToString();
                }
            }
            
            return param;
        }
        
        public static bool HasUpdate(OrderedDictionary data, OrderedDictionary data_old)
        {
            foreach (DictionaryEntry item in data)
            {
                if (item.Value != null && data_old[item.Key] != null)
                {
                    if (item.Value.ToString() != data_old[item.Key].ToString())
                    {
                        return true;
                    }
                }
                else {
                    if (item.Value != data_old[item.Key])
                    {
                        return true;
                    }
                }

            }                
            return false;
        }
        public static void AuditForm(HttpContext context ,string table_name,string[] pk, string actionType,OrderedDictionary data, OrderedDictionary data_old,string display_column,string display_separator)
        {
            string sql = "select enable_audit from md_tbl where tbl_name ='"+ table_name + "' ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            string enable_audit = "0";
            if (dt.Rows.Count > 0) {
                enable_audit = dt.Rows[0]["enable_audit"].ToString();
            }
            if (enable_audit == "1") {
                OrderedDictionary param = new OrderedDictionary();
                param["action_time"] = DateTime.Now;
                param["ipaddress"] = context.Connection.RemoteIpAddress.ToString();
                param["username"] = context.Session.GetString(SecurityHelper.SESSION_KEY_USERNAME);
                param["obj_data"] = table_name;
                param["obj_key"] = "";
                param["url"] = context.Request.Scheme + "://" + context.Request.Host + "" + context.Request.PathBase + context.Request.Path + context.Request.QueryString.ToString();
                param["user_agent"] = context.Request.Headers["User-Agent"].ToString();
                param["action_type"] = actionType;
                param["action_title"] = "";
                param["action_data"] = "";

                string columnName = "";
                string columnType = "";

                if (actionType == ActionType.Add.ToString())
                {

                    OrderedDictionary param_pk = new OrderedDictionary();
                    foreach (string key in pk)
                    {
                        param_pk[key] = data[key].ToString();
                    }
                    param["obj_key"] = JsonConvert.SerializeObject(param_pk);
                    string[] dsp_column = display_column.Split(',');
                    string item_display = "";
                    foreach (string dsp in dsp_column)
                    {
                        item_display += item_display != "" ? " - " : "";
                        item_display += data[dsp].ToString();
                    }
                    param["action_title"] = ResxHelper.GetValue(table_name, "AddTitle") + " [" + item_display + "]";
                    string sqlColumn = "select  COLUMN_NAME, DATA_TYPE from information_schema.columns where TABLE_NAME = '" + table_name + "' "
                                        + " and COLUMN_NAME<>'Lvl' and COLUMN_NAME<>'Lft' and COLUMN_NAME<>'Rgt' and COLUMN_NAME<>'lock_by' "
                                        + " and COLUMN_NAME<>'lock_at' and COLUMN_NAME<>'lock_address' "
                                        + " and COLUMN_NAME<>'insert_by' and COLUMN_NAME<>'insert_at' "
                                        + " and COLUMN_NAME<>'update_by' and COLUMN_NAME<>'update_at' order by ORDINAL_POSITION ";
                    DataTable dtColumn = SqlHelper.GetDataTable(sqlColumn);
                    IList<AuditColumn> param_data = new List<AuditColumn>();

                    foreach (DataRow dr in dtColumn.Rows)
                    {
                        columnName = dr["COLUMN_NAME"].ToString();
                        columnType = dr["DATA_TYPE"].ToString();
                        foreach (DictionaryEntry entry in data)
                        {
                            AuditColumn itemData = new AuditColumn();
                            itemData.column_name = columnName;
                            if (entry.Key.ToString() == columnName)
                            {
                                itemData.column_title = ResxHelper.GetValue(table_name, columnName);
                                if (columnType != "text" && columnType != "image" && columnType != "varbinary")
                                {
                                    itemData.new_val = data[columnName].ToString();
                                    itemData.old_val = "";
                                }
                                else
                                {
                                    itemData.new_val = "...";
                                    itemData.old_val = "";
                                }
                                param_data.Add(itemData);
                            }
                        }
                    }
                    param["action_data"] = JsonConvert.SerializeObject(param_data);
                    sql = "INSERT INTO sys_audittrail (action_time,ipaddress,username,obj_data,obj_key,url,user_agent,action_type,action_title,action_data) "
                   + " VALUES(@action_time, @ipaddress, @username, @obj_data,@obj_key, @url, @user_agent, @action_type, @action_title, @action_data) ";
                    SqlHelper.ExecuteNonQuery(sql, param);
                }
                else if (actionType == ActionType.Edit.ToString())
                {

                    OrderedDictionary param_pk = new OrderedDictionary();
                    foreach (string key in pk)
                    {
                        param_pk[key] = data_old[key].ToString();
                    }
                    param["obj_key"] = JsonConvert.SerializeObject(param_pk).ToString();
                    string[] dsp_column = display_column.Split(',');
                    string item_display = "";
                    foreach (string dsp in dsp_column)
                    {
                        item_display += item_display != "" ? " - " : "";
                        item_display += data[dsp].ToString();
                    }
                    param["action_title"] = ResxHelper.GetValue(table_name, "EditTitle") + " [" + item_display + "]";
                    string sqlColumn = "select  COLUMN_NAME, DATA_TYPE from information_schema.columns where TABLE_NAME = '" + table_name + "' "
                                        + " and COLUMN_NAME<>'Lvl' and COLUMN_NAME<>'Lft' and COLUMN_NAME<>'Rgt' and COLUMN_NAME<>'lock_by' "
                                        + " and COLUMN_NAME<>'lock_at' and COLUMN_NAME<>'lock_address' "
                                        + " and COLUMN_NAME<>'insert_by' and COLUMN_NAME<>'insert_at' "
                                        + " and COLUMN_NAME<>'update_by' and COLUMN_NAME<>'update_at' order by ORDINAL_POSITION ";
                    DataTable dtColumn = SqlHelper.GetDataTable(sqlColumn);
                    IList<AuditColumn> param_data = new List<AuditColumn>();
                    foreach (DataRow dr in dtColumn.Rows)
                    {
                        columnName = dr["COLUMN_NAME"].ToString();
                        columnType = dr["DATA_TYPE"].ToString();
                        foreach (DictionaryEntry entry in data)
                        {
                            if (entry.Key.ToString() == columnName)
                            {
                                if (data[columnName].ToString() != data_old[columnName].ToString())
                                {
                                    AuditColumn itemData = new AuditColumn();
                                    itemData.column_name = columnName;
                                    itemData.column_title = ResxHelper.GetValue(table_name, columnName);
                                    if (columnType != "text" && columnType != "image" && columnType != "varbinary")
                                    {
                                        itemData.new_val = data[columnName].ToString();
                                        itemData.old_val = data_old[columnName].ToString();
                                    }
                                    else
                                    {
                                        itemData.new_val = "....";
                                        itemData.old_val = "...";
                                    }
                                    param_data.Add(itemData);
                                }
                            }
                        }
                    }
                    param["action_data"] = JsonConvert.SerializeObject(param_data).ToString();
                    sql = "INSERT INTO sys_audittrail (action_time,ipaddress,username,obj_data,obj_key,url,user_agent,action_type,action_title,action_data) "
                    + " VALUES(@action_time, @ipaddress, @username, @obj_data,@obj_key, @url, @user_agent, @action_type, @action_title, @action_data) ";
                    SqlHelper.ExecuteNonQuery(sql, param);
                }
                else if (actionType == ActionType.Delete.ToString())
                {

                    OrderedDictionary param_pk = new OrderedDictionary();
                    foreach (string key in pk)
                    {
                        param_pk[key] = data[key].ToString();
                    }
                    param["obj_key"] = JsonConvert.SerializeObject(param_pk);
                    string[] dsp_column = display_column.Split(',');
                    string item_display = "";
                    foreach (string dsp in dsp_column)
                    {
                        item_display += item_display != "" ? " - " : "";
                        item_display += data[dsp].ToString();
                    }
                    param["action_title"] = ResxHelper.GetValue(table_name, "DeleteTitle") + " [" + item_display + "]";
                    string sqlColumn = "select  COLUMN_NAME, DATA_TYPE from information_schema.columns where TABLE_NAME = '" + table_name + "' "
                                        + " and COLUMN_NAME<>'Lvl' and COLUMN_NAME<>'Lft' and COLUMN_NAME<>'Rgt' and COLUMN_NAME<>'lock_by' "
                                        + " and COLUMN_NAME<>'lock_at' and COLUMN_NAME<>'lock_address' "
                                        + " and COLUMN_NAME<>'insert_by' and COLUMN_NAME<>'insert_at' "
                                        + " and COLUMN_NAME<>'update_by' and COLUMN_NAME<>'update_at' order by ORDINAL_POSITION ";
                    DataTable dtColumn = SqlHelper.GetDataTable(sqlColumn);
                    IList<AuditColumn> param_data = new List<AuditColumn>();
                    foreach (DataRow dr in dtColumn.Rows)
                    {
                        columnName = dr["COLUMN_NAME"].ToString();
                        columnType = dr["DATA_TYPE"].ToString();
                        foreach (DictionaryEntry entry in data)
                        {
                            if (entry.Key.ToString() == columnName)
                            {
                                AuditColumn itemData = new AuditColumn();
                                itemData.column_name = columnName;
                                itemData.column_title = ResxHelper.GetValue(table_name, columnName);
                                if (columnType != "text" && columnType != "image" && columnType != "varbinary")
                                {
                                    itemData.new_val = "";
                                    itemData.old_val = data[columnName].ToString();
                                }
                                else
                                {
                                    itemData.new_val = "";
                                    itemData.old_val = "...";
                                }
                                param_data.Add(itemData);
                            }
                        }
                    }
                    param["action_data"] = JsonConvert.SerializeObject(param_data);
                    sql = "INSERT INTO sys_audittrail (action_time,ipaddress,username,obj_data,obj_key,url,user_agent,action_type,action_title,action_data) "
                    + " VALUES(@action_time, @ipaddress, @username, @obj_data,@obj_key, @url, @user_agent, @action_type, @action_title, @action_data) ";
                    SqlHelper.ExecuteNonQuery(sql, param);
                }
            }
        }
        
        public static void ReorderTree(string action, string TableName, string PrimaryKeyName, string ParentIdName, int PrimaryKeyValue, int ParentIdValue, int order_newvalue, int order_odlvalue)
        {
            ReorderTree(action, TableName, PrimaryKeyName, ParentIdName, PrimaryKeyValue, ParentIdValue, order_newvalue, order_odlvalue, "ordinal");
        }
        public static void ReorderTree(string action, string TableName, string PrimaryKeyName, string ParentIdName, int PrimaryKeyValue, int ParentIdValue, int order_newvalue, int order_odlvalue, string ordinalName)
        {
            string whereParent = ParentIdValue != 0 ? " " + ParentIdName + "=" + ParentIdValue.ToString() + " " : " " + ParentIdName + " is null";
            if (action.ToLower() != "delete")
            {
                int range_order = order_newvalue - order_odlvalue;
                string update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + order_newvalue + " where " + PrimaryKeyName + "=" + PrimaryKeyValue.ToString();
                SqlHelper.ExecuteNonQuery(update_sql);
                if (range_order < 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " + 1 where " + ordinalName + " < " + order_odlvalue + " and " + ordinalName + ">= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString() + " and " + whereParent;
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
                else if (range_order > 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + ordinalName + "<= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString() + " and " + whereParent;
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
            }
            else
            {
                string update_sql2 = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + whereParent;
                SqlHelper.ExecuteNonQuery(update_sql2);
            }

            string check_sql = "SELECT " + PrimaryKeyName + " FROM " + TableName + " where " + whereParent + " order by " + ordinalName;
            DataTable dt = SqlHelper.GetDataTable(check_sql);
            int order_value = 0;
            string reorder_sql = "";
            foreach (DataRow dr in dt.Rows)
            {
                order_value += 1;
                reorder_sql = "update " + TableName + " set " + ordinalName + " = " + order_value.ToString() + " where " + PrimaryKeyName + "=" + dr[PrimaryKeyName].ToString() + ";";
                SqlHelper.ExecuteNonQuery(reorder_sql);
            }
        }

        public static void ReorderTree2(string action, string TableName, string PrimaryKeyName, string ParentIdName1, string ParentIdName2, int PrimaryKeyValue, int ParentIdValue1, int ParentIdValue2, int order_newvalue, int order_odlvalue)
        {
            ReorderTree2(action, TableName, PrimaryKeyName, ParentIdName1, ParentIdName2, PrimaryKeyValue, ParentIdValue1, ParentIdValue2, order_newvalue, order_odlvalue, "ordinal");
        }
        public static void ReorderTree2(string action, string TableName, string PrimaryKeyName, string ParentIdName1, string ParentIdName2, int PrimaryKeyValue, int ParentIdValue1, int ParentIdValue2, int order_newvalue, int order_odlvalue, string ordinalName)
        {
            string whereParent = ParentIdValue1 != 0 ? " " + ParentIdName1 + "=" + ParentIdValue1.ToString() + " " : " ";
            whereParent += ParentIdValue2 != 0 ? " AND " + ParentIdName2 + "=" + ParentIdValue2.ToString() + " " : " AND " + ParentIdName2 + " is null";
            if (action.ToLower() != "delete")
            {
                int range_order = order_newvalue - order_odlvalue;
                string update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + order_newvalue + " where " + PrimaryKeyName + "=" + PrimaryKeyValue.ToString();
                SqlHelper.ExecuteNonQuery(update_sql);
                if (range_order < 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " + 1 where " + ordinalName + " < " + order_odlvalue + " and " + ordinalName + ">= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString() + " and " + whereParent;
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
                else if (range_order > 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + ordinalName + "<= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString() + " and " + whereParent;
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
            }
            else
            {
                string update_sql2 = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + whereParent;
                SqlHelper.ExecuteNonQuery(update_sql2);
            }

            string check_sql = "SELECT " + PrimaryKeyName + " FROM " + TableName + " where " + whereParent + " order by " + ordinalName;
            DataTable dt = SqlHelper.GetDataTable(check_sql);
            int order_value = 0;
            string reorder_sql = "";
            foreach (DataRow dr in dt.Rows)
            {
                order_value += 1;
                reorder_sql = "update " + TableName + " set " + ordinalName + " = " + order_value.ToString() + " where " + PrimaryKeyName + "=" + dr[PrimaryKeyName].ToString() + ";";
                SqlHelper.ExecuteNonQuery(reorder_sql);
            }
        }

        public static void ReorderByParent(string action, string TableName, string PrimaryKeyName, string ParentIdName, int PrimaryKeyValue, int ParentIdNewValue, int ParentIdOldValue, int order_newvalue, int order_odlvalue)
        {
            ReorderByParent(action, TableName, PrimaryKeyName, ParentIdName, PrimaryKeyValue, ParentIdNewValue, ParentIdOldValue, order_newvalue, order_odlvalue, "ordinal");
        }
        public static void ReorderByParent(string action, string TableName, string PrimaryKeyName, string ParentIdName, int PrimaryKeyValue, int ParentIdNewValue, int ParentIdOldValue, int order_newvalue, int order_odlvalue, string ordinalName)
        {
            string whereParent = ParentIdNewValue != 0 ? " " + ParentIdName + "=" + ParentIdNewValue.ToString() + " " : " " + ParentIdName + " is null";
            if (action.ToLower() != "delete")
            {
                int range_order = order_newvalue - order_odlvalue;
                string update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + order_newvalue + " where " + PrimaryKeyName + "=" + PrimaryKeyValue.ToString();
                SqlHelper.ExecuteNonQuery(update_sql);
                if (range_order < 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " + 1 where " + ordinalName + " < " + order_odlvalue + " and " + ordinalName + ">= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString() + " and " + whereParent;
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
                else if (range_order > 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + ordinalName + "<= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString() + " and " + whereParent;
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
            }
            else
            {
                string update_sql2 = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + whereParent;
                SqlHelper.ExecuteNonQuery(update_sql2);
            }

            string check_sql = "SELECT " + PrimaryKeyName + " FROM " + TableName + " where " + whereParent + " order by " + ordinalName;
            DataTable dt = SqlHelper.GetDataTable(check_sql);
            int order_value = 0;
            string reorder_sql = "";
            foreach (DataRow dr in dt.Rows)
            {
                order_value += 1;
                reorder_sql = "update " + TableName + " set " + ordinalName + " = " + order_value.ToString() + " where " + PrimaryKeyName + "=" + dr[PrimaryKeyName].ToString() + ";";
                SqlHelper.ExecuteNonQuery(reorder_sql);
            }

            if (ParentIdOldValue != ParentIdNewValue)
            {
                //Old parent
                whereParent = ParentIdOldValue != 0 ? " " + ParentIdName + "=" + ParentIdOldValue.ToString() + " " : " " + ParentIdName + " is null";
                check_sql = "SELECT " + PrimaryKeyName + " FROM " + TableName + " where " + whereParent + " order by " + ordinalName;
                DataTable dtold = SqlHelper.GetDataTable(check_sql);
                order_value = 0;
                reorder_sql = "";
                foreach (DataRow dr in dtold.Rows)
                {
                    order_value += 1;
                    reorder_sql = "update " + TableName + " set " + ordinalName + " = " + order_value.ToString() + " where " + PrimaryKeyName + "=" + dr[PrimaryKeyName].ToString() + ";";
                    SqlHelper.ExecuteNonQuery(reorder_sql);
                }
            }
        }

        public static void Reorder(string action, string TableName, string PrimaryKeyName, int PrimaryKeyValue, int order_newvalue, int order_odlvalue)
        {
            Reorder(action, TableName, PrimaryKeyName, PrimaryKeyValue, order_newvalue, order_odlvalue, "ordinal");
        }
        public static void Reorder(string action, string TableName, string PrimaryKeyName, int PrimaryKeyValue, int order_newvalue, int order_odlvalue, string ordinalName)
        {
            if (action.ToLower() != "delete")
            {
                int range_order = order_newvalue - order_odlvalue;
                string update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + order_newvalue + " where " + PrimaryKeyName + "=" + PrimaryKeyValue.ToString();
                SqlHelper.ExecuteNonQuery(update_sql);
                if (range_order < 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " + 1 where " + ordinalName + " < " + order_odlvalue + " and " + ordinalName + ">= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString();
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
                else if (range_order > 0)
                {
                    update_sql = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue + " and " + ordinalName + "<= " + order_newvalue + " and " + PrimaryKeyName + "<>" + PrimaryKeyValue.ToString();
                    SqlHelper.ExecuteNonQuery(update_sql);
                }
            }
            else
            {
                string update_sql2 = "UPDATE " + TableName + " set " + ordinalName + " = " + ordinalName + " - 1 where " + ordinalName + " > " + order_odlvalue;
                SqlHelper.ExecuteNonQuery(update_sql2);
            }

            string check_sql = "SELECT " + PrimaryKeyName + " FROM " + TableName + " order by " + ordinalName;
            DataTable dt = SqlHelper.GetDataTable(check_sql);
            int order_value = 0;
            string reorder_sql = "";
            foreach (DataRow dr in dt.Rows)
            {
                order_value += 1;
                reorder_sql = "update " + TableName + " set " + ordinalName + " = " + order_value.ToString() + " where " + PrimaryKeyName + "=" + dr[PrimaryKeyName].ToString() + ";";
                SqlHelper.ExecuteNonQuery(reorder_sql);
            }
        }
        public static DataTable LookupData(lookup_param param,string _table_name,string _displayColumn)
        {
            DataTable data = new DataTable();
            string sql_item = "";
            string select_distinct = "";
            if (param.item != null)
            {
                if (param.item.value != null)
                {
                    sql_item += sql_item != "" ? ", " : "";
                    sql_item += param.item.value + " as value ";
                }
                if (param.item.text != null)
                {
                    string separator = param.item.separator != null && param.item.separator != "" ? param.item.separator : " - ";
                    string[] col_text = param.item.text.Split(',');
                    if (col_text.Length > 1)
                    {
                        string item_text = "";
                        foreach (string item in col_text)
                        {
                            item_text += item_text != "" ? "+'"+ separator + "'+" : "";
                            item_text += item;
                        }
                        sql_item += ",(" + item_text + ") as text ";
                    }
                    else
                    {
                        sql_item += "," + param.item.text + " as text ";
                    }
                }
                if (param.item.attribute != null && param.item.attribute.Length>0)
                {
                    foreach (string item in param.item.attribute) {
                        sql_item += ",case when " + item + "=1 then 'Aktif' else 'Tidak Aktif' end as " + item + " ";
                    }

                }
                if (param.item.distinct == "1")
                {
                    select_distinct = "DISTINCT";
                }
            }
            else
            {
                string[] col_text = _displayColumn.Split(',');
                if (col_text.Length > 1)
                {
                    string item_text = "";
                    foreach (string item in col_text)
                    {
                        item_text += item_text != "" ? "+' - '+" : "";
                        item_text += item;
                    }
                    sql_item += ",(" + item_text + ") as text ";
                }
                else
                {
                    sql_item += "," + _displayColumn + " as text ";
                }
            }

            string sql = "SELECT " + select_distinct + " " + sql_item + " from " + _table_name;
            string sql_where = " WHERE 1=1 ";
            OrderedDictionary parameter = new OrderedDictionary();
            if (param.parent != null)
            {
                foreach (lookup_parent item in param.parent)
                {
                    if (item.field != null)
                    {
                        sql_where += " AND " + item.field;
                        if (item.value != null)
                        {
                            if (item.opr != null)
                            {
                                sql_where += " " + item.opr + " ";
                            }
                            else
                            {
                                sql_where += " = ";
                            }
                            sql_where += " @" + item.field;
                            parameter[item.field] = item.value;
                        }
                        else
                        {
                            sql_where += "=''";
                        }
                    }
                }
            }
            sql += sql_where;
            string sql_order = "";
            if (param.order != null)
            {
                foreach (lookup_order item in param.order)
                {
                    if (item.field != null)
                    {
                        sql_order += sql_order != "" ? "," : "";
                        sql_order += item.field;
                        if (item.dir != null)
                        {
                            sql_order += " " + item.dir;
                        }
                    }
                }
            }
            if (sql_order != "" && select_distinct == "")
            {
                sql += " order by " + sql_order;
            }
            else
            {
                sql += " order by text";
            }
            data = SqlHelper.GetDataTable(sql, parameter);
            return data;
        }
        public static int GetCountDataUsed(string tbl_name, string pkValue) {
            int result = 0;
            string sql = "SELECT tbl_name,col_name FROM md_tbl_column "
            + " WHERE lu_table_name = '"+ tbl_name + "' and (lu_cascade_delete = 0 or lu_cascade_delete is null) ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            if (dt.Rows.Count > 0) {

                sql = " select sum(jumlah) from ( ";
                string sql_item = "";
                foreach (DataRow dr in dt.Rows) {
                    sql_item += sql_item != "" ? " UNION " : "";
                    sql_item += "select count(1) as jumlah from "+dr["tbl_name"] + " where " + dr["col_name"] + " = '" + pkValue + "' ";
                }
                sql += sql_item;
                sql += ") as a ";
                result = SqlHelper.ExecuteScalarInt(sql);
            }
            return result;
        }
        public static void DeleteCascade(HttpContext context, string tbl_name, string pkValue)
        {
            string sql = "SELECT c.tbl_name, c.col_name, t.fk_col, t.pk_col, t.display_col, t.enable_audit "
            +" FROM md_tbl_column as c left outer join md_tbl as t on c.tbl_name = t.tbl_name "
            +" WHERE c.lu_table_name = '"+ tbl_name + "' AND c.lu_cascade_delete = 1 ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                string pkKey;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["enable_audit"].ToString() == "1")
                    {
                        if (dr["fk_col"].ToString() != "")
                        {
                            pkKey = dr["fk_col"].ToString() + "," + dr["pk_col"].ToString();
                        }
                        else
                        {
                            pkKey = dr["pk_col"].ToString();
                        }
                        string[] _pkKey = pkKey.Split(',');
                        sql = "select * from " + dr["tbl_name"].ToString() + " where " + dr["col_name"].ToString() + " = '" + pkValue + "' ";
                        DataTable dataAll = SqlHelper.GetDataTable(sql);
                        sql = "DELETE from " + dr["tbl_name"].ToString() + " where " + dr["col_name"].ToString() + " = '" + pkValue + "' ";
                        SqlHelper.ExecuteNonQuery(sql);
                        foreach (DataRow item in dataAll.Rows)
                        {
                            OrderedDictionary data = new OrderedDictionary();
                            for (int i = 0; i < dataAll.Columns.Count; i++)
                            {
                                data[dataAll.Columns[i].ColumnName] = item[dataAll.Columns[i].ColumnName];
                            }
                            AuditForm(context, dr["tbl_name"].ToString(), _pkKey, ActionType.Delete.ToString(), data, null, dr["display_col"].ToString(), ",");
                        }
                    }
                    else {
                        sql = "DELETE from " + dr["tbl_name"].ToString() + " where " + dr["col_name"].ToString() + " = '" + pkValue + "' ";
                        SqlHelper.ExecuteNonQuery(sql);
                    }                    
                }
            }            
        }

        public static IList<UploadItem> GetParamUpload(string tbl_name)
        {
            List<UploadItem> data = new List<UploadItem>();
            string sql = "select col_name ,upload_file_location,upload_allow_ext,upload_max_file_size,upload_max_file_count ";
            sql += "from md_tbl_column where tbl_name = '" + tbl_name + "' and control_type='FileUpload'";
            DataTable dt = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                UploadItem item = new UploadItem();
                item.col_name = dr["col_name"].ToString();
                UploadParam param = new UploadParam();
                param.file_location = dr["upload_file_location"].ToString();
                param.allow_ext = dr["upload_allow_ext"].ToString();
                param.max_file_size = dr["upload_max_file_size"].ToString()!="" ? int.Parse(dr["upload_max_file_size"].ToString()) : 2097152;
                param.max_file_size_text = param.max_file_size.ToString();
                if (param.max_file_size >= 1048576)
                {
                    int maxSize  = param.max_file_size / 1048576;
                    param.max_file_size_text = maxSize.ToString()+"Mb";
                }
                else if (param.max_file_size >= 1024)
                {
                    int maxSize = param.max_file_size / 1024;
                    param.max_file_size_text = maxSize.ToString() + "Kb";
                }
                param.max_file_count = dr["upload_max_file_count"].ToString() != "" ? int.Parse(dr["upload_max_file_count"].ToString()) : 1;
                item.param = param;
                data.Add(item);
            }
            return data;
        }
    }
    public class InfoUser {
        public string userid { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string groupid { get; set; }
        public string groupname { get; set; }
        public string rolename { get; set; }
        public PersonData person { get; set; }
    }
    public class ColumnAtt
    {
        public string name { get; set; }
        public string type { get; set; }
        public int? length { get; set; }
    }

    public class ColumnList
    {
        IList<ColumnData> column_list { get; set; }
    }
    public class ColumnData
    {
        public string item { get; set; }
        public GridColumn attr { get; set; }
    }

    public class GridData
    {
        public int total { get; set; }
        public DataTable data { get; set; }
        public Hashtable aggregates { get; set; }
    }
    public class ModelRequest
    {
        public string take { get; set; }
        public string skip { get; set; }
        public string page { get; set; }
        public string pageSize { get; set; }
        public IList<ItemFilterColumn> filter_by_column { get; set; }
        public IList<GroupSort> sort { get; set; }
        public GroupFilter filter { get; set; }
        public IList<GroupAggregate> aggregate { get; set; }

    }
    public class ItemFilterColumn
    {
        public string name { get; set; }
        public string field { get; set; }
        public string type { get; set; }
        public string opr { get; set; }
        public string value { get; set; }
    }
    public class GroupSort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }
    public class GroupFilter
    {
        public string logic { get; set; }
        public IList<GroupFilterItem> filters { get; set; }
    }
    public class GroupFilterItem
    {
        public string name { get; set; }
        public string field { get; set; }
        public string type { get; set; }
        public string opr { get; set; }
        public string value { get; set; }
    }
    public class GroupAggregate
    {
        public string field { get; set; }
        public string aggregate { get; set; }
    }
    //public class ItemAggregateResult
    //{
    //    public double sum { get; set; }
    //    public int count { get; set; }
    //}
    public class GridColumn
    {
        //table_alias
        public string name { get; set; }
        public string table_name { get; set; }
        public string table_alias { get; set; }
        public string field { get; set; }
        public string field_order { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public bool select { get; set; }
        public bool display { get; set; }
        public bool hidden { get; set; }
        public string width { get; set; }
        public string format { get; set; }
        public string attributes { get; set; }
        public string template { get; set; }
        public bool encoded { get; set; }
        public bool filterable { get; set; }
        public bool sortable { get; set; }
        public bool qsearch { get; set; }
        public string tooltip { get; set; }
        public string aggregate { get; set; }
        public string footerTemplate { get; set; }
    }
    public class ParamGrid
    {
        public string ShowFilter { get; set; }
        public string quick_search { get; set; }
        public string adv_search { get; set; }
        public string search_by_column { get; set; }
        public string display_line_number { get; set; }
        public string groupable { get; set; }
        public string wrapable { get; set; }
        public string show_hide_column { get; set; }
        public string grid_width { get; set; }
        public string grid_width_unit { get; set; }
        public string export_exl { get; set; }
        public string export_word { get; set; }
        public string export_csv { get; set; }
        public string export_pdf { get; set; }
        public string btnView { get; set; }
        public string btn_view_rule { get; set; }
        public string btn_view_target { get; set; }
        public string btn_view_modal_width { get; set; }
        public string btn_view_modal_height { get; set; }
        public string btnAdd { get; set; }
        public string btn_add_rule { get; set; }
        public string btn_add_target { get; set; }
        public string btn_add_modal_width { get; set; }
        public string btn_add_modal_height { get; set; }
        public string btnCopy { get; set; }
        public string btn_copy_rule { get; set; }
        public string btn_copy_target { get; set; }
        public string btn_copy_modal_width { get; set; }
        public string btn_copy_modal_height { get; set; }
        public string btnEdit { get; set; }
        public string btn_edit_rule { get; set; }
        public string btn_edit_target { get; set; }
        public string btn_edit_modal_width { get; set; }
        public string btn_edit_modal_height { get; set; }
        public string btnDelete { get; set; }
        public string btn_delele_rule { get; set; }
        public string btn_delete_target { get; set; }
        public string btn_delete_modal_width { get; set; }
        public string btn_delete_modal_height { get; set; }
    }
    public class ProsesResult
    {
        public int? status { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string pk { get; set; }
    }
    public class AuditColumn
    {
        public string column_name { get; set; }
        public string column_title { get; set; }
        public string new_val { get; set; }
        public string old_val { get; set; }
    }

    public class RelationTable
    {
        public string field_name { get; set; }
        public string field_alias { get; set; }
        public string table_name { get; set; }
        public string table_alias { get; set; }
        public string join_type { get; set; }
        public string join_on { get; set; }
        public string select_column { get; set; }
        public bool casecade_delete { get; set; }
        public bool casecade_update { get; set; }
    }

    //var ordinal_param = {item : {value:"id",text:"nama"},parent: [{field:"parent_id", opr:"=" ,value:""}],order: [{field:"kode", dir:"asc"}]};
    public class lookup_param
    {
        public lookup_item item { get; set; }
        public IList<lookup_order> order { get; set; }
        public IList<lookup_parent> parent { get; set; }
        public string add_blank { get; set; }
    }
    public class lookup_item
    {
        public string value { get; set; }
        public string text { get; set; }
        public string separator { get; set; } = " - ";
        public string distinct { get; set; } = "0";
        public string[] attribute { get; set; }
    }
    public class lookup_parent
    {
        public string field { get; set; }
        public string opr { get; set; }
        public string value { get; set; }
    }
    public class lookup_order
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class lookup_tree
    {
        public int? value { get; set; }
        public string text { get; set; }
        public int? parent_id { get; set; }
        public bool has_child { get; set; }
        public bool expanded { get; set; }
        public IList<lookup_tree> item { get; set; }
    }

    public class lookup_tree_check
    {
        public int? id { get; set; }
        public int? parent_id { get; set; }
        public string text { get; set; }
        public bool has_child { get; set; }
        public bool check { get; set; }
        public bool expanded { get; set; }
        public IList<lookup_tree_check> item { get; set; }
    }
    public class display_item
    {
        public string name { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
    public class PersonData
    {
        public string id { get; set; }
        public string person_type_id { get; set; }
        public string person_type_id_text { get; set; }
        public string person_type_nama { get; set; }
        public string company_id { get; set; }
        public string company_kode { get; set; }
        public string company_nama { get; set; }
        public string nrp { get; set; }
        public string nama { get; set; }
        public string jenis_kelamin_id { get; set; }
        public string jenis_kelamin_id_text { get; set; }
        public string jenis_kelamin_nama { get; set; }
        public string tempat_lahir { get; set; }
        public string tgl_lahir { get; set; }
        public string dt_tgl_lahir { get; set; }
        public string tta_id { get; set; }
        public string tta_kode { get; set; }
        public string tta_nama { get; set; }

        public string area_id { get; set; }
        public string area_kode { get; set; }
        public string area_nama { get; set; }

        public string ba_id { get; set; }
        public string ba_kode { get; set; }
        public string ba_nama { get; set; }
        public string pa_id { get; set; }
        public string pa_kode { get; set; }
        public string pa_nama { get; set; }
        public string psa_id { get; set; }
        public string psa_kode { get; set; }
        public string psa_nama { get; set; }
        public string jabatan_id { get; set; }
        public string jabatan { get; set; }
        public string jabatan_kode { get; set; }
        public string jabatan_nama { get; set; }
        public string job { get; set; }
        public string job_start { get; set; }
        public string dt_job_start { get; set; }
        public string position { get; set; }
        public string position_start { get; set; }
        public string dt_position_start { get; set; }
        public string photo { get; set; }
        public string aktif_id { get; set; }
        public string aktif_id_text { get; set; }
        public string mulai_bekerja { get; set; }
        public string dt_mulai_bekerja { get; set; }
        public string akhir_bekerja { get; set; }
        public string dt_akhir_bekerja { get; set; }
        //=======================================
        public string tch_id { get; set; }
        public string tch_nrp { get; set; }
        public string tch_nama { get; set; }
        public string sdh_id { get; set; }
        public string sdh_nrp { get; set; }
        public string sdh_nama { get; set; }
        public string superior_id { get; set; }
        public string superior_nrp { get; set; }
        public string superior_nama { get; set; }
    }
    public class UploadItem
    {
        //col_name ,upload_file_location,upload_allow_ext,upload_max_file_size,upload_max_file_count
        public string col_name { get; set; }
        public UploadParam param { get; set; }
    }
    public class UploadParam
    {
        public string file_location { get; set; }
        public string allow_ext { get; set; }
        public int max_file_size { get; set; }
        public string max_file_size_text { get; set; }
        public int max_file_count { get; set; }
    }
}
