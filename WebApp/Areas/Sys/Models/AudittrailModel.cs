using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using WebApp.Models;

namespace WebApp.Areas.Sys.Models
{
    public class AudittrailModel
    {
        public static string _table_name = "sys_audittrail";
        public static string[] _pkKey = { "id" };
        public static string[] _un = { };
        public static string _columnOrder = "id desc";
        public static string _displayColumn = "action_time";

        public static IList<RelationTable> _join = new List<RelationTable> { };

        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string action_time { get; set; }
            public string ipaddress { get; set; }
            public string username { get; set; }
            public string obj_data { get; set; }
            public string obj_key { get; set; }
            public string url { get; set; }
            public string user_agent { get; set; }
            public string action_type { get; set; }
            public string action_title { get; set; }
            public IList<ActionDataModel> action_data { get; set; }

        }
        public class ActionDataModel
        {
            public string column_name { get; set; }
            public string column_title { get; set; }
            public string new_val { get; set; }
            public string old_val { get; set; }
        }
        public static ParamGrid GetListParam()
        {
            ParamGrid param = new ParamGrid
            {
                ShowFilter = "1",
                quick_search = "1",
                adv_search = "0",
                search_by_column = "0",
                display_line_number = "true",
                groupable = "false",
                wrapable = "true",
                show_hide_column = "1",
                grid_width = "100",
                grid_width_unit = "%",
                export_exl = "0",
                export_word = "1",
                export_csv = "1",
                export_pdf = "0",
                btnView = "1",
                btn_view_rule = "",
                btn_view_target = "modal",
                btn_view_modal_width = "700",
                btn_view_modal_height = "500",
                btnAdd = "0",
                btn_add_rule = "",
                btn_add_target = "modal",
                btn_add_modal_width = "700",
                btn_add_modal_height = "400",
                btnCopy = "0",
                btn_copy_rule = "",
                btn_copy_target = "modal",
                btn_copy_modal_width = "700",
                btn_copy_modal_height = "400",
                btnEdit = "0",
                btn_edit_rule = "",
                btn_edit_target = "modal",
                btn_edit_modal_width = "700",
                btn_edit_modal_height = "400",
                btnDelete = "0",
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
                    field= "sys_audittrail.id",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","Id","ID"),
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
                    name= "action_time",
                    field= "sys_audittrail.action_time",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","action_time","Waktu"),
                    type= "date",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "160px",
                    format= "{0:dd/MM/yyyy HH:mm:ss}",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= true,
                    filterable= true,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "ipaddress",
                    field= "sys_audittrail.ipaddress",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","ipaddress","IP Address"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "120px",
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
                    name= "username",
                    field= "sys_audittrail.username",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","username","User"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "100px",
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
                    name= "obj_data",
                    field= "sys_audittrail.obj_data",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","obj_data","Objek"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "120px",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: left;\" }",
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
                    name= "obj_key",
                    field= "sys_audittrail.obj_key",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","obj_key","Key"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:#}",
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
                    name= "url",
                    field= "sys_audittrail.url",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","url","Url"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: left;\" }",
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
                    name= "user_agent",
                    field= "sys_audittrail.user_agent",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","user_agent","Browser"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:#}",
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
                    name= "action_type",
                    field= "sys_audittrail.action_type",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","action_type","Action Type"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "100px",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: left;\" }",
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
                    name= "action_title",
                    field= "sys_audittrail.action_title",
                    table_name = "sys_audittrail",
                    table_alias = "sys_audittrail",
                    title= @ResxHelper.GetValue("sys_audittrail","action_title","Subjek"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "{0:#}",
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
            if (param.filter_by_column != null) {
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
        public static ProsesResult Delete(string id)
        {
            ProsesResult result = new ProsesResult();
            try
            {
                OrderedDictionary param = new OrderedDictionary();
                param["id_old"] = id;
                string sql = "DELETE FROM " + _table_name
                + " WHERE id = @id_old ";
                SqlHelper.ExecuteNonQuery(sql, param);
                result.status = 1;
                result.title = ResxHelper.GetValue("Message", "SuccessDelete");
                result.message = ResxHelper.GetValue("Message", "SuccessDelete");
                result.pk = id;
            }
            catch (Exception ex)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ex.Message; ;
            }
            return result;
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
        public static DataTable LookupObjData(string obj_data)
        {
            string sql = "select distinct obj_data as value, obj_data as text from " + _table_name + " order by obj_data";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }
        public static DataTable LookupUsername(string username)
        {
            string sql = "select distinct username as value, username as text from " + _table_name + " order by username";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }

        public static ProsesResult Reset()
        {
            ProsesResult result = new ProsesResult();
            try
            {
                string sql = "TRUNCATE TABLE " + _table_name ;
                SqlHelper.ExecuteNonQuery(sql);
                result.status = 1;
                result.title = ResxHelper.GetValue("Message", "SuccessDelete");
                result.message = ResxHelper.GetValue("Message", "SuccessDelete");
                result.pk = null;
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
