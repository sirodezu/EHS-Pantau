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
    public class RolesModel
    {
        public static string _table_name = "sys_roles";
        public static string[] _pkKey = { "id" };
        public static string[] _un = { "kode", "nama" };
        public static string _columnOrder = "sys_roles.id";
        public static string _displayColumn = "nama";

        public static IList<RelationTable> _join = new List<RelationTable> {
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string kode { get; set; }
            public string nama { get; set; }
            public string keterangan { get; set; }
            public string level_id { get; set; }
            public string level_id_text { get; set; }
            public string rule_id { get; set; }
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
                btn_delete_modal_width = "700",
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
                    select= true,
                    display= true,
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
                    name= "id",
                    field= "sys_roles.id",
                    table_name = "sys_roles",
                    table_alias = "sys_roles",
                    title= @ResxHelper.GetValue("sys_roles","Id","ID"),
                    type= "number",
                    select= true,
                    display= true,
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
                    name= "kode",
                    field= "sys_roles.kode",
                    table_name = "sys_roles",
                    table_alias = "sys_roles",
                    title= @ResxHelper.GetValue("sys_roles","kode","Kode"),
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
                    name= "nama",
                    field= "sys_roles.nama",
                    table_name = "sys_roles",
                    table_alias = "sys_roles",
                    title= @ResxHelper.GetValue("sys_roles","nama","Nama"),
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
                    name= "keterangan",
                    field= "sys_roles.keterangan",
                    table_name = "sys_roles",
                    table_alias = "sys_roles",
                    title= @ResxHelper.GetValue("sys_roles","keterangan","Keterangan"),
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
                    name= "level_id",
                    field= "sys_roles.level_id",
                    table_name = "sys_roles",
                    table_alias = "sys_roles",
                    title= @ResxHelper.GetValue("sys_roles","level_id","Level"),
                    type= "number",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
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
                  }
            };
            return colum_list;

        }
        public class ActionRequest : ModelRequest
        {

        }
        public static GridData GetListData(HttpContext context, ActionRequest param)
        {
            string curUserId = SecurityHelper.CurrentUserId(context);
            IList<GridColumn> column_att = GetGridColumn();
            OrderedDictionary parameter = new OrderedDictionary();
            string whereCond = " WHERE 1=1 ";
            if (curUserId != "-1") {
                whereCond += " AND sys_roles.id > 1 ";
            }
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
        public static DataTable GetViewData(string id)
        {
            DataTable data = new DataTable();
            if (id != "")
            {
                string sql = "select * from sys_roles where id=@id";
                OrderedDictionary parameter = new OrderedDictionary();
                parameter["id"] = id;
                data = SqlHelper.GetDataTable(sql, parameter);
            }

            return data;
        }
        public static DataTable GetData(string id)
        {
            DataTable data = new DataTable();
            if (id != "")
            {
                string sql = "select * from sys_roles where id=@id";
                OrderedDictionary parameter = new OrderedDictionary();
                parameter["id"] = id;
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
                    foreach (string KeyUN in _un)
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
                //lastpk = $this->_pk[(count($this->_pk) - 1)];
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
                    if (_un.Count() > 0)
                    {
                        foreach (string KeyUN in _un)
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
        public static ProsesResult Insert(HttpContext context, OrderedDictionary data, string ruleId)
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
                    string[] rule_id = ruleId.Split(',');
                    sql = "delete from sys_role_rule where role_id=" + data["id"] + " ";
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (string val in rule_id)
                    {
                        if (val != "")
                        {
                            sql = "insert into sys_role_rule (role_id,rule_id) values (" + data["id"] + "," + val + ") ";
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }
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
        public static ProsesResult Update(HttpContext context, OrderedDictionary data, OrderedDictionary data_old, string ruleId)
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
                    string[] rule_id = ruleId.Split(',');
                    sql = "delete from sys_role_rule where role_id=" + data["id"] + " ";
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (string val in rule_id)
                    {
                        if (val != "")
                        {
                            sql = "insert into sys_role_rule (role_id,rule_id) values (" + data["id"] + "," + val + ") ";
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }
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
                param["id_old"] = data["id"];
                string sql = "DELETE FROM sys_roles "
                + " WHERE id = @id_old ";
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
                sql = "delete from sys_role_rule where role_id=" + data["id"] + " ";
                SqlHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ex.Message; ;
            }
            return result;
        }
        public static int GetNewId()
        {
            string sql = "select case when max(id) is null then 1 else max(id) + 1 end as jumlah from " + _table_name + " ";
            int result = SqlHelper.ExecuteScalarInt(sql);
            return result;
        }
        public static string GetRuleId(string id)
        {
            string result = "";
            string sql = "select rule_id from sys_role_rule where role_id = " + id + " ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                result += result != "" ? "," : "";
                result += dr["rule_id"];
            }
            return result;
        }
        public static IList<lookup_tree_check> getItemRuleTreeCheck(string roleId)
        {
            IList<lookup_tree_check> result = new List<lookup_tree_check>();
            string sql = "select a.id,a.nama,case when b.rule_id is not null then 1 else 0 end as checked from sys_rules as a "
            + " left outer join sys_role_rule as b on a.id = b.rule_id  and b.role_id=" + roleId + " "
            + " where a.parent_id is null and a.aktif_id=1 " +
            " order by a.ordinal";
            DataTable dtParent = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dtParent.Rows)
            {
                lookup_tree_check item = new lookup_tree_check();
                item.id = int.Parse(dr["id"].ToString());
                item.parent_id = null;
                item.text = dr["nama"].ToString();
                item.check = dr["checked"].ToString() == "1" ? true : false;
                item.item = getItemChaildRuleTreeCheck(roleId, dr["id"].ToString());
                item.expanded = true;
                if (item.item.Count > 0)
                {
                    item.has_child = true;
                }
                else
                {
                    item.item = null;
                }

                result.Add(item);
            }
            return result;
        }
        public static IList<lookup_tree_check> getItemChaildRuleTreeCheck(string roleId, string parent_id)
        {
            IList<lookup_tree_check> result = new List<lookup_tree_check>();
            string sql = "select a.id,a.nama,case when b.rule_id is not null then 1 else 0 end as checked from sys_rules as a "
            + " left outer join sys_role_rule as b on a.id = b.rule_id and b.role_id=" + roleId + " "
            + " where a.parent_id =" + parent_id + " and a.aktif_id=1 " +
            " order by a.ordinal";
            DataTable dtParent = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dtParent.Rows)
            {
                lookup_tree_check item = new lookup_tree_check();
                item.id = int.Parse(dr["id"].ToString());
                item.parent_id = int.Parse(parent_id);
                item.text = dr["nama"].ToString();
                item.check = dr["checked"].ToString() == "1" ? true : false;
                item.item = getItemChaildRuleTreeCheck(roleId, dr["id"].ToString());
                item.expanded = false;
                if (item.item.Count > 0)
                {
                    item.has_child = true;
                    item.expanded = true;
                }
                else
                {
                    item.item = null;
                }

                result.Add(item);
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
                sql_item += " id as value , "+_displayColumn+" as text ";
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
                            sql_where += " is null ";
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
    }
}
