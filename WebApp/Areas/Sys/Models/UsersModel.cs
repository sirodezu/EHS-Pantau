using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using WebApp.Models;

namespace WebApp.Areas.Sys.Models
{
    public class UsersModel
    {
        public static string _table_name = "sys_users";
        public static string[] _pkKey = { "userid" };
        public static string[] _unKey = { "username" };
        public static string _columnOrder = "sys_users.userid";
        public static string _displayColumn = "username";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="block"
                ,field_alias = "block_text"
                ,table_name = "ref_literal"
                ,table_alias = "lk_block"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "sys_users.block=lk_block.id and lk_block.jenis='yes_no'"
                ,select_column = "lk_block.nama"
            }
        };
        public static IList<ColumnAtt> _columnList = new List<ColumnAtt> {
            new ColumnAtt{ name = "userid", type ="int",length= null},
            new ColumnAtt{ name = "domain_name", type ="varchar",length= 50},
            new ColumnAtt{ name = "username", type ="varchar",length= 50},
            new ColumnAtt{ name = "fullname", type ="varchar",length= 255},
            new ColumnAtt{ name = "password", type ="varchar",length= 255},
            new ColumnAtt{ name = "old_password", type ="varchar",length= 500},
            new ColumnAtt{ name = "email", type ="varchar",length= 100},
            new ColumnAtt{ name = "alternative_email", type ="varchar",length= 255},
            new ColumnAtt{ name = "allow_concurrent_login", type ="tinyint",length= 255},
            new ColumnAtt{ name = "has_change_password", type ="tinyint",length= 255},
            new ColumnAtt{ name = "enable_change_password", type ="tinyint",length= null},
            new ColumnAtt{ name = "last_change_password", type ="datetime",length= null},
            new ColumnAtt{ name = "enable_password_expired", type ="tinyint",length= null},
            new ColumnAtt{ name = "password_expired_remainder", type ="tinyint",length= null},
            new ColumnAtt{ name = "attemp_count", type ="int",length= null},
            new ColumnAtt{ name = "attemp_time", type ="datetime",length= null},
            new ColumnAtt{ name = "user_expired", type ="datetime",length= null},
            new ColumnAtt{ name = "must_change_password", type ="tinyint",length= null},
            new ColumnAtt{ name = "login_count", type ="int",length= null},
            new ColumnAtt{ name = "last_login", type ="datetime",length= null},
            new ColumnAtt{ name = "block", type ="tinyint",length= null},
            new ColumnAtt{ name = "activation", type ="varchar",length= 100},
            new ColumnAtt{ name = "code_activation", type ="varchar",length= 100},
            new ColumnAtt{ name = "code_forget_password", type ="varchar",length= 100},
            new ColumnAtt{ name = "params", type ="varchar",length= 1000},
            new ColumnAtt{ name = "insert_by", type ="int",length= null},
            new ColumnAtt{ name = "insert_at", type ="datetime",length= null},
            new ColumnAtt{ name = "update_by", type ="int",length= null},
            new ColumnAtt{ name = "update_at", type ="datetime",length= null}
        };

        public class ViewModel
        {
            public string userid { get; set; }
            public string userid_old { get; set; }
            public string domain_name { get; set; }
            public string username { get; set; }
            public string fullname { get; set; }
            public string password { get; set; }
            public string old_password { get; set; }
            public string email { get; set; }
            public string alternative_email { get; set; }
            public string allow_concurrent_login { get; set; }
            public string has_change_password { get; set; }
            public string enable_change_password { get; set; }
            public string last_change_password { get; set; }
            public string enable_password_expired { get; set; }
            public string password_expired_remainder { get; set; }
            public string attemp_count { get; set; }
            public string attemp_time { get; set; }
            public string user_expired { get; set; }
            public string must_change_password { get; set; }
            public string login_count { get; set; }
            public string last_login { get; set; }
            public string block { get; set; }
            public string activation { get; set; }
            public string code_activation { get; set; }
            public string code_forget_password { get; set; }
            public string parameter { get; set; }
            public string group_id { get; set; }
            public string group_id_text { get; set; }
        }
        public static ParamGrid GetListParam()
        {
            ParamGrid param = new ParamGrid
            {
                ShowFilter = "0",
                quick_search = "1",
                adv_search = "0",
                search_by_column = "0",
                display_line_number = "true",
                groupable = "false",
                wrapable = "true",
                show_hide_column = "1",
                grid_width = "100",
                grid_width_unit = "%",
                export_exl = "1",
                export_word = "1",
                export_csv = "1",
                export_pdf = "0",
                btnView = "1",
                btn_view_rule = "",
                btn_view_target = "modal",
                btn_view_modal_width = "0",
                btn_view_modal_height = "500",
                btnAdd = "1",
                btn_add_rule = "",
                btn_add_target = "modal",
                btn_add_modal_width = "0",
                btn_add_modal_height = "400",
                btnCopy = "0",
                btn_copy_rule = "",
                btn_copy_target = "modal",
                btn_copy_modal_width = "0",
                btn_copy_modal_height = "400",
                btnEdit = "1",
                btn_edit_rule = "",
                btn_edit_target = "modal",
                btn_edit_modal_width = "0",
                btn_edit_modal_height = "400",
                btnDelete = "1",
                btn_delele_rule = "",
                btn_delete_target = "modal",
                btn_delete_modal_width = "0",
                btn_delete_modal_height = "400"
            };
            return param;
        }
        public static IList<GridColumn> GetGridColumn()
        {
            IList<GridColumn> colum_list = new List<GridColumn>
            {
                new GridColumn
                {
                    name= "row_no",
                    field= "row_no",
                    table_name = "",
                    table_alias = "",
                    title= "No",
                    type= "number",
                    select= false,
                    display= false,
                    hidden= false,
                    width= "50px",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: center;\" }",
                    template= "",
                    encoded= true,
                    filterable= false,
                    sortable= false,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "userid",
                    field= _table_name+".userid",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"userid","USERID"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "40px",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: center;\" }",
                    template= "",
                    encoded= true,
                    filterable= false,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "username",
                    field= _table_name+".username",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"username","Username"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= true,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "fullname",
                    field= _table_name+".fullname",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"fullname","Nama Lengkap"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= true,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "email",
                    field= _table_name+".email",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"email","Email"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= true,
                    filterable= false,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "last_login",
                    field= _table_name+".last_login",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"last_login","Login Terakhir"),
                    type= "date",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "160px",
                    format= "{0:dd/MM/yyyy HH:mm}",
                    attributes= "{ \"style\": \"text-align: center;\" }",
                    template= "",
                    encoded= true,
                    filterable= false,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "group_user",
                    field= "(STUFF((SELECT  ',' + sg.nama FROM sys_user_group sug LEFT OUTER JOIN sys_groups sg on sug.group_id = sg.id WHERE sug.userid = sys_users.userid FOR XML PATH('')), 1, 1, ''))",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"group_user","Group User"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= true,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  }
            };
            return colum_list;

        }
        public class ActionRequest : ModelRequest
        {

        }
        public static GridData GetListData(ActionRequest param)
        {
            IList<GridColumn> column_att = GetGridColumn();
            OrderedDictionary parameter = new OrderedDictionary();
            string whereCond = " WHERE 1=1 ";
            if (param.filter_by_column != null)
            {
                foreach (ItemFilterColumn itemFilter in param.filter_by_column)
                {
                    if (itemFilter.value != null && itemFilter.value != "")
                    {
                        if (itemFilter.opr == null || itemFilter.opr == "")
                        {
                            itemFilter.opr = "=";
                        }
                        whereCond += " AND " + itemFilter.field + " " + itemFilter.opr + " @" + itemFilter.name;
                        parameter[itemFilter.name] = itemFilter.value;
                    }
                }
            }
            if (param.filter != null)
            {
                if (param.filter.filters.Count > 0)
                {
                    string filter_item = "";
                    for (int i = 0; i < param.filter.filters.Count; i++)
                    {
                        var myColumn = column_att.ToList().Find(item => item.name == param.filter.filters[i].name);
                        filter_item += filter_item != "" ? param.filter.logic + " " : "";
                        filter_item += myColumn.field + DataModel.GetOperatorValue(myColumn.type, param.filter.filters[i].opr, param.filter.filters[i].value);
                    }
                    whereCond += " AND (" + filter_item + ") ";
                }
            }
            string sort_by = "";
            if (param.sort != null)
            {
                if (param.sort.Count > 0)
                {
                    for (int i = 0; i < param.sort.Count; i++)
                    {
                        sort_by += sort_by != "" ? "," : "";
                        var mysort = column_att.ToList().Find(item => item.name == param.sort[i].field);
                        sort_by += mysort.field + " " + param.sort[i].dir;
                    }
                }
            }
            if (sort_by == "" && _columnOrder != "")
            {
                sort_by = _columnOrder;
            }
            if (sort_by != "")
            {
                sort_by = " ORDER BY " + sort_by + " ";
            }
            string take = param.take;
            string skip = param.skip;
            string page = param.page;
            string pageSize = param.pageSize;

            //==============================
            IList<GridColumn> item_column = GetGridColumn();
            string sql_item = "";
            var table_join_list = new List<string>();
            var table_join_count = new List<string>();
            foreach (GridColumn gc in item_column)
            {
                if (gc.name == "row_no")
                {
                    if (gc.select == true)
                    {
                        sql_item += sql_item != "" ? "," : "";
                        sql_item += " ROW_NUMBER() OVER(" + sort_by + ") AS " + gc.name;
                    }
                }
                else
                {
                    if (gc.select == true)
                    {
                        sql_item += sql_item != "" ? "," : "";
                        sql_item += gc.field + " as " + gc.name;
                        if (gc.table_alias != _table_name)
                        {
                            if (!table_join_list.Contains(gc.table_name))
                            {
                                table_join_list.Add(gc.table_name);
                            }
                            if (gc.filterable == true || gc.qsearch == true)
                            {
                                if (!table_join_count.Contains(gc.table_name))
                                {
                                    table_join_count.Add(gc.table_name);
                                }
                            }
                        }
                    }
                }
            }

            string sql_join_list = "";
            string sql_join_cout = "";
            foreach (RelationTable tr in _join)
            {
                if (table_join_list.Contains(tr.table_name))
                {
                    sql_join_list += " " + tr.join_type + tr.table_name + " as " + tr.table_alias + " ON " + tr.join_on;
                }
                if (table_join_count.Contains(tr.table_name))
                {
                    sql_join_cout += " " + tr.join_type + tr.table_name + " as " + tr.table_alias + " ON " + tr.join_on;
                }
            }

            GridData datalist = new GridData();
            //Get Total
            string sql = "select count(*) as jumlah from " + _table_name + " " + sql_join_cout + " "
                + whereCond;
            datalist.total = SqlHelper.ExecuteScalarInt(sql, parameter);

            //Get Data
            sql = "select " + sql_item + " from " + _table_name + " ";
            sql += sql_join_list;
            sql += whereCond;
            sql += sort_by;
            sql += "  OFFSET " + skip + " ROWS FETCH NEXT " + take + " ROWS ONLY ";
            datalist.data = SqlHelper.GetDataTable(sql, parameter);
            Hashtable aggregates = new Hashtable();
            datalist.aggregates = aggregates;
            return datalist;
        }
        public static DataTable GetData(OrderedDictionary PkValue)
        {
            DataTable data = new DataTable();
            bool valid = true;
            foreach (DictionaryEntry entry in PkValue)
            {
                if (entry.Value.ToString() == "")
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
            {
                OrderedDictionary parameter = new OrderedDictionary();
                string sql = "select * from " + _table_name + " ";
                sql += " where 1=1 ";
                foreach (string pk in _pkKey)
                {
                    sql += " AND " + pk + "=@" + pk + " ";
                    parameter[pk] = PkValue[pk];
                }
                data = SqlHelper.GetDataTable(sql, parameter);
            }
            return data;
        }
        public static DataTable GetViewData(OrderedDictionary PkValue)
        {
            DataTable data = new DataTable();
            bool valid = true;
            foreach (DictionaryEntry entry in PkValue)
            {
                if (entry.Value.ToString() == "")
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
            {
                OrderedDictionary parameter = new OrderedDictionary();
                string sql = "select " + _table_name + ".* ";
                string sql_join_field = "";
                string sql_join_table = "";
                foreach (RelationTable tr in _join)
                {
                    sql_join_field += "," + tr.select_column + " as " + tr.field_alias + " ";
                    sql_join_table += " " + tr.join_type + tr.table_name + " as " + tr.table_alias + " ON " + tr.join_on;
                }
                sql += sql_join_field;
                sql += " from " + _table_name + " ";
                sql += sql_join_table;
                sql += " where 1=1 ";
                foreach (string pk in _pkKey)
                {
                    sql += " AND " + _table_name + "." + pk + "=@" + pk + " ";
                    parameter[pk] = PkValue[pk];
                }
                data = SqlHelper.GetDataTable(sql, parameter);
            }
            return data;
        }

        public static ProsesResult IsExist(OrderedDictionary data, OrderedDictionary dataOld)
        {
            ProsesResult result = new ProsesResult();
            result.status = 1;
            result.message = "";
            OrderedDictionary param = new OrderedDictionary();
            if (dataOld == null)
            {
                //Check Existing PK
                string sql = "select count(*) as total from " + _table_name + " ";
                sql += " WHERE 1=1 ";
                string msg = "";
                foreach (string key in _pkKey)
                {
                    sql += " AND " + key + "= @" + key + " ";
                    param[key] = data[key];
                    msg += msg != "" ? " " + ResxHelper.GetValue("Message", "and") + " " : "";
                    msg += ResxHelper.GetValue(_table_name, key) + ": " + data[key];
                }
                int jml = SqlHelper.ExecuteScalarInt(sql, param);
                if (jml > 0)
                {
                    msg += " " + ResxHelper.GetValue("Message", "AlreadyExists");
                    result.status = 2;
                    result.title = ResxHelper.GetValue("Message", "TitleAlreadyExists", "Data Sudah ada");
                    result.message = msg;
                    return result;
                }
                else
                {
                    //Check Existing UN
                    param = new OrderedDictionary();
                    foreach (string KeyUN in _unKey)
                    {
                        sql = "select count(*) as total from " + _table_name + " ";
                        sql += " WHERE 1=1 ";
                        msg = "";
                        string[] fieldUN = KeyUN.Split(',');
                        foreach (string itemKey in fieldUN)
                        {
                            if (itemKey != "")
                            {
                                sql += " AND " + itemKey + "= @" + itemKey + " ";
                                param[itemKey] = data[itemKey];
                                msg += msg != "" ? " " + ResxHelper.GetValue("Message", "and") + " " : "";
                                msg += ResxHelper.GetValue(_table_name, itemKey) + ": " + data[itemKey];
                            }

                        }
                        jml = SqlHelper.ExecuteScalarInt(sql, param);
                        if (jml > 0)
                        {
                            msg += " " + ResxHelper.GetValue("Message", "AlreadyExists");
                            result.status = 2;
                            result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                            result.message = msg;
                            return result;
                        }
                    }
                }
            }
            else
            {
                //Check Existing PK
                param = new OrderedDictionary();
                string sql = "select count(*) as total from " + _table_name + " ";
                sql += " WHERE 1=1 ";
                string msg = "";
                foreach (string key in _pkKey)
                {
                    sql += " AND " + key + "= @" + key + " ";
                    param[key] = data[key];
                    msg += msg != "" ? " " + ResxHelper.GetValue("Message", "and") + " " : "";
                    msg += ResxHelper.GetValue(_table_name, key) + ": " + data[key];
                }
                string key_last = _pkKey[(_pkKey.Count() - 1)];
                sql += " AND " + key_last + " <> @" + key_last + "_old ";
                param[key_last + "_old "] = dataOld[key_last];
                int jml = SqlHelper.ExecuteScalarInt(sql, param);
                if (jml > 0)
                {
                    msg += " " + ResxHelper.GetValue("Message", "AlreadyExists");
                    result.status = 2;
                    result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                    result.message = msg;
                    return result;
                }
                else
                {
                    //Check Existing UN
                    if (_unKey.Count() > 0)
                    {
                        foreach (string KeyUN in _unKey)
                        {
                            string sql2 = "";
                            sql = "select count(*) as total from " + _table_name + " ";
                            sql += " WHERE 1=1 ";
                            msg = "";
                            string[] fieldUN = KeyUN.Split(',');
                            foreach (string itemKey in fieldUN)
                            {
                                sql += " AND " + itemKey + "= @" + itemKey + " ";
                                param[itemKey] = data[itemKey];
                                msg += msg != "" ? " " + ResxHelper.GetValue("Message", "and") + " " : "";
                                msg += ResxHelper.GetValue(_table_name, itemKey) + ": " + data[itemKey];

                            }
                            foreach (string key in _pkKey)
                            {
                                if (!fieldUN.Contains(key))
                                {
                                    sql2 += " AND " + key + " <> @" + key + " ";
                                    param[key] = dataOld[key];
                                }
                            }
                            sql += sql2;
                            jml = SqlHelper.ExecuteScalarInt(sql, param);
                            if (jml > 0)
                            {
                                msg += " " + ResxHelper.GetValue("Message", "AlreadyExists");
                                result.status = 2;
                                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                                result.message = msg;
                                return result;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static int GetLastId()
        {
            string sql = "select case when max(userid) is null then 0 else max(userid) end as last_id from " + _table_name + " ";
            int id = SqlHelper.ExecuteScalarInt(sql);
            return id;
        }
        public static int GetNewId(string username)
        {
            string sql = "SELECT ISNUMERIC('"+ username + "') as hasil";
            int hasil = SqlHelper.ExecuteScalarInt(sql);
            if (hasil == 0) {
                sql = "select case when max(userid) is null then 1 else (max(userid) + 1) end as last_id from " + _table_name + " where ISNUMERIC(username) = 0";
                int id = SqlHelper.ExecuteScalarInt(sql);
                return id;
            }
            else{
                return int.Parse(username);
            }
        }
        public static ProsesResult Insert(HttpContext context, OrderedDictionary data)
        {
            ProsesResult result = new ProsesResult();
            try
            {
                result = IsExist(data, null);
                if (result.status == 1)
                {
                    string sql_field = "";
                    string sql_value = "";
                    foreach (DictionaryEntry entry in data)
                    {
                        sql_field += sql_field != "" ? "," : "";
                        sql_field += entry.Key;
                        sql_value += sql_value != "" ? "," : "";
                        sql_value += "@" + entry.Key;
                    }
                    string sql = "INSERT INTO " + _table_name + " (" + sql_field + ")  VALUES(" + sql_value + ") ";
                    SqlHelper.ExecuteNonQuery(sql, data);
                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "SuccessAdd");
                    result.message = ResxHelper.GetValue("Message", "SuccessAdd");
                    result.pk = "";
                    foreach (string key in _pkKey)
                    {
                        result.pk += result.pk != "" ? "," : "";
                        result.pk += data[key];
                    }
                    DataModel.AuditForm(context, _table_name, _pkKey, DataModel.ActionType.Add.ToString(), data, null, _displayColumn, ",");
                }

            }
            catch (Exception ex)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ex.Message; ;
            }
            return result;
        }
        public static ProsesResult Update(HttpContext context, OrderedDictionary data, OrderedDictionary data_old)
        {
            ProsesResult result = new ProsesResult();
            try
            {
                result = IsExist(data, data_old);
                if (result.status == 1)
                {
                    string sql_set = "";
                    foreach (DictionaryEntry entry in data)
                    {
                        sql_set += sql_set != "" ? "," : "";
                        sql_set += entry.Key + "=@" + entry.Key;
                    }
                    string sql = "UPDATE " + _table_name + " SET " + sql_set + " "
                    + " WHERE 1=1 ";
                    OrderedDictionary param = data;
                    foreach (string key in _pkKey)
                    {
                        sql += " AND " + key + "=@" + key + "_old";
                        param[key + "_old"] = data_old[key];
                    }
                    SqlHelper.ExecuteNonQuery(sql, param);
                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "SuccessUpdate");
                    result.message = ResxHelper.GetValue("Message", "SuccessUpdate");
                    result.pk = "";
                    foreach (string key in _pkKey)
                    {
                        result.pk += result.pk != "" ? "," : "";
                        result.pk += data[key];
                    }
                    DataModel.AuditForm(context, _table_name, _pkKey, DataModel.ActionType.Edit.ToString(), data, data_old, _displayColumn, ",");
                }

            }
            catch (Exception ex)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ex.Message; ;
            }
            return result;
        }
        public static ProsesResult Delete(HttpContext context, OrderedDictionary data)
        {
            ProsesResult result = new ProsesResult();
            try
            {
                OrderedDictionary param = data;
                foreach (string key in _pkKey)
                {
                    param[key + "_old"] = data[key];
                }
                string sql = "DELETE FROM " + _table_name + " WHERE 1=1 ";
                foreach (string key in _pkKey)
                {
                    sql += " AND " + key + " = @" + key + "_old ";
                }
                SqlHelper.ExecuteNonQuery(sql, param);
                result.status = 1;
                result.title = ResxHelper.GetValue("Message", "SuccessDelete");
                result.message = ResxHelper.GetValue("Message", "SuccessDelete");
                result.pk = "";
                foreach (string key in _pkKey)
                {
                    result.pk += result.pk != "" ? "," : "";
                    result.pk += data[key];
                }
                DataModel.AuditForm(context, _table_name, _pkKey, DataModel.ActionType.Delete.ToString(), data, null, _displayColumn, ",");
            }
            catch (Exception ex)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ex.Message; ;
            }
            return result;
        }
        public static DataTable LookupData(lookup_param param)
        {
            DataTable data = new DataTable();
            string sql_item = "";
            if (param.item != null)
            {
                if (param.item.value != null)
                {
                    sql_item += sql_item != "" ? ", " : "";
                    sql_item += param.item.value + " as value ";
                }
                if (param.item.text != null)
                {
                    sql_item += sql_item != "" ? ", " : "";
                    sql_item += param.item.text + " as text ";
                }
            }
            else
            {
                sql_item += " id as value , " + _displayColumn + " as text ";
            }
            string sql = "SELECT " + sql_item + " from " + _table_name;
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
            if (sql_order != "")
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
        public static IList<display_item> GetDisplayItem()
        {
            string[] displayName = _displayColumn.Split(',');
            IList<display_item> displayItem = new List<display_item>();
            foreach (string item in displayName)
            {
                display_item new_item = new display_item();
                new_item.name = item;
                new_item.label = ResxHelper.GetValue(_table_name, item);
                displayItem.Add(new_item);
            }
            return displayItem;
        }
        public static string GetGroupId(OrderedDictionary PkValue)
        {
            string result = "";
            bool valid = true;
            foreach (DictionaryEntry entry in PkValue)
            {
                if (entry.Value.ToString() == "")
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
            {
                OrderedDictionary parameter = new OrderedDictionary();
                string sql = "select group_id from sys_user_group ";
                sql += " where 1=1 ";
                foreach (string pk in _pkKey)
                {
                    sql += " AND " + pk + "=@" + pk + " ";
                    parameter[pk] = PkValue[pk];
                }
                DataTable data = SqlHelper.GetDataTable(sql, parameter);
                foreach (DataRow dr in data.Rows)
                {
                    result += result != "" ? "," : "";
                    result += dr["group_id"];
                }
            }
            return result;
        }
        public static ProsesResult UpdateGroup(HttpContext context, string userid, string group_id)
        {
            ProsesResult result = new ProsesResult();

            try
            {
                if (userid != "") {
                    string sql = "DELETE FROM sys_user_group "
                    + " WHERE userid=" + userid;
                    SqlHelper.ExecuteNonQuery(sql);
                    string[] groupid = group_id.Split(',');
                    foreach (string item in groupid)
                    {
                        if (item != "")
                        {
                            sql = "insert into sys_user_group (userid,group_id) values(" + userid + "," + item + "); ";
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }
                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "SuccessUpdate");
                    result.message = ResxHelper.GetValue("Message", "SuccessUpdate");
                    result.pk = userid;
                }
                
            }
            catch (Exception ex)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ex.Message; ;
            }
            return result;
        }

    }
}
