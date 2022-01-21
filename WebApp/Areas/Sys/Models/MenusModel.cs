using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using WebApp.Models;

namespace WebApp.Areas.Sys.Models
{
    public class MenusModel
    {
        
        public static string _table_name = "sys_menus";
        public static string[] _pkKey = { "id" };
        public static string[] _un = { };
        public static string _columnOrder = "sys_menus.ordinal";
        public static string _displayColumn = "name";

        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{ field_name = "parent_id"
                ,field_alias = "parent_id_text"
                , table_name = "sys_menus"
                , table_alias = "lk_parent_id"
                , join_type = "LEFT OUTER JOIN "
                , join_on = "sys_menus.parent_id=lk_parent_id.id"
                , select_column = "lk_parent_id.name"
            },
            new RelationTable{ field_name = "menu_type_id"
                ,field_alias = "menu_type_id_text"
                , table_name = "sys_menu_type"
                , table_alias = "lk_menu_type_id"
                , join_type = "LEFT OUTER JOIN "
                , join_on = "sys_menus.menu_type_id=lk_menu_type_id.id"
                , select_column = "lk_menu_type_id.name"
            }
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string parent_id { get; set; }
            public string parent_id_text { get; set; }
            public string parent_id_old { get; set; }
            public string name { get; set; }
            public string label_id { get; set; }
            public string label_en { get; set; }
            public string link { get; set; }
            public string icon { get; set; }
            public string imgurl { get; set; }
            public string param { get; set; }
            public string target { get; set; }
            public string active_id { get; set; }
            public string active_id_text { get; set; }
            public string active_id0 { get; set; }
            public string active_id1 { get; set; }
            public string ordinal { get; set; }
            public string rule_id { get; set; }
            public string menu_type_id { get; set; }
            public string menu_type_id_text { get; set; }
        }
        public static ParamGrid GetListParam()
        {
            ParamGrid param = new ParamGrid
            {
                ShowFilter = "0",
                quick_search = "0",
                adv_search = "0",
                search_by_column = "0",
                display_line_number = "true",
                groupable = "false",
                wrapable = "true",
                show_hide_column = "0",
                grid_width = "100",
                grid_width_unit = "%",
                export_exl = "1",
                export_word = "1",
                export_csv = "1",
                export_pdf = "0",
                btnView = "0",
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
                    field= "sys_menus.id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
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
                    name= "parent_id",
                    field= "sys_menus.parent_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","parent_id","Parent"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
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
                    name= "parent_id_text",
                    field= "lk_parent_id.name",
                    table_name = "sys_menus",
                    table_alias = "lk_parent_id",
                    title= @ResxHelper.GetValue("sys_menus","parent_id","Parent"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "200px",
                    format= "",
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
                    name= "name",
                    field= "sys_menus.name",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","name","Nama"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "200px",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "label_id",
                    field= "sys_menus.label_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","label_id","Title(Indonesia)"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "label_en",
                    field= "sys_menus.label_en",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","label_en","Title(English)"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "link",
                    field= "sys_menus.link",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","link","Link"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "icon",
                    field= "sys_menus.icon",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","icon","Icon"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "active_id",
                    field= "sys_menus.active_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","active_id","Aktif"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
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
                    name= "active_id_text",
                    field= "case when sys_menus.active_id=1 then '"+ResxHelper.GetValue("Message","Yes")+"' else '"+ResxHelper.GetValue("Message","No")+"' end",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","active_id","Aktif"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
                    format= "",
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
                    name= "ordinal",
                    field= "sys_menus.ordinal",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","ordinal","Urutan"),
                    type= "number",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "70px",
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
        public static IList<GridColumn> GetTreeColumn()
        {
            IList<GridColumn> colum_list = new List<GridColumn>
            {
                  new GridColumn{
                    name= "id",
                    field= "sys_menus.id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
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
                    name= "menu_type_id",
                    field= "sys_menus.menu_type_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","menu_type_id","Jenis Menu"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "40px",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: center;\" }",
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
                    name= "parent_id",
                    field= "sys_menus.parent_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","parent_id","Parent"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
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
                    name= "parent_id_text",
                    field= "lk_parent_id.name",
                    table_name = "sys_menus",
                    table_alias = "lk_parent_id",
                    title= @ResxHelper.GetValue("sys_menus","parent_id","Parent"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "200px",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= false,
                    filterable= false,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "name",
                    field= "concat(cast(sys_menus.ordinal as varchar(10)),'. ',sys_menus.name)",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","name","Nama"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "200px",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                  },
                  new GridColumn{
                    name= "label_id",
                    field= "sys_menus.label_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","label_id","Title(Indonesia)"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "",
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
                    name= "label_en",
                    field= "sys_menus.label_en",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","label_en","Title(English)"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;\" }",
                    template= "",
                    encoded= false,
                    filterable= false,
                    sortable= true,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "link",
                    field= "sys_menus.link",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","link","Link"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "icon",
                    field= "sys_menus.icon",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","icon","Icon"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""

                  },
                  new GridColumn{
                    name= "active_id",
                    field= "sys_menus.active_id",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","active_id","Aktif"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
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
                    name= "active_id_text",
                    field= "case when sys_menus.active_id=1 then '"+ResxHelper.GetValue("Message","Yes")+"' else '"+ResxHelper.GetValue("Message","No")+"' end",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","active_id","Aktif"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
                    format= "",
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
                    name= "ordinal",
                    field= "sys_menus.ordinal",
                    table_name = "sys_menus",
                    table_alias = "sys_menus",
                    title= @ResxHelper.GetValue("sys_menus","ordinal","Urutan"),
                    type= "number",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "70px",
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
            //public GroupFilterColumn filter_by_column { get; set; }
        }
        public class GroupFilterColumn
        {
            public string parent_id { get; set; }
        }

        private static IHostingEnvironment _hostingEnvironment;
        public MenusModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public static GridData GetListData(ActionRequest param)
        {
            IList<GridColumn> column_att = GetGridColumn();
            OrderedDictionary parameter = new OrderedDictionary();
            string whereCond = " WHERE 1=1 ";
            if (param.filter_by_column != null)
            {
                //if (param.filter_by_column.parent_id != null && param.filter_by_column.parent_id != "")
                //{
                //    whereCond += " AND parent_id=@parent_id";
                //    parameter["parent_id"] = param.filter_by_column.parent_id;
                //}
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
        public static DataTable GetTreeData(ActionRequest param)
        {
            OrderedDictionary parameter = new OrderedDictionary();
            string whereCond = " WHERE 1=1 ";
            if (param.filter_by_column != null)
            {
                //if (param.filter_by_column.parent_id != null && param.filter_by_column.parent_id != "")
                //{
                //    whereCond += " AND parent_id=@parent_id";
                //    parameter["parent_id"] = param.filter_by_column.parent_id;
                //}
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
                        filter_item += filter_item != "" ? param.filter.logic + " " : "";
                        filter_item += param.filter.filters[i].field + DataModel.GetOperatorValue(param.filter.filters[i].type, param.filter.filters[i].opr, param.filter.filters[i].value);
                        parameter[param.filter.filters[i].field] = param.filter.filters[i].value;
                    }
                    whereCond += " AND (" + filter_item + ") ";

                }
            }

            //==============================
            IList<GridColumn> item_column = GetTreeColumn();
            string sql_item = "";
            var table_join_list = new List<string>();
            foreach (GridColumn gc in item_column)
            {
                if (gc.name != "row_no")
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
                        }
                    }
                }
            }

            string sql_join_list = "";
            foreach (RelationTable tr in _join)
            {
                if (table_join_list.Contains(tr.table_name))
                {
                    sql_join_list += " " + tr.join_type + tr.table_name + " as " + tr.table_alias + " ON " + tr.join_on;
                }
            }

            //Get Data
            string sql = "select " + sql_item + " from " + _table_name + " ";
            sql += sql_join_list;
            sql += whereCond;
            sql += " order by  " + _table_name + ".ordinal";
            //
            DataTable data = SqlHelper.GetDataTable(sql, parameter);
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
                sql += ",case when " + _table_name + ".active_id=1 then '" + ResxHelper.GetValue("Message", "Yes") + "' else '" + ResxHelper.GetValue("Message", "No") + "' end as active_id_text ";
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
        public static int GetLastId()
        {
            int id = 0;
            string sql = "select case when max(id) is null then 0 else max(id) end as last_id from " + _table_name + " ";
            id = SqlHelper.ExecuteScalarInt(sql);
            return id;
        }
        public static int GetNewId()
        {
            int id = 0;
            string sql = "select case when max(id) is null then 1 else (max(id) + 1) end as last_id from " + _table_name + " ";
            id = SqlHelper.ExecuteScalarInt(sql);
            return id;
        }
        public static ProsesResult IsExist(OrderedDictionary data, OrderedDictionary dataOld)
        {
            ProsesResult result = new ProsesResult();
            result.status = 1;
            result.message = "";
            OrderedDictionary param = new OrderedDictionary();
            if (dataOld == null)
            {
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
        public static ProsesResult hasChild(string id)
        {
            ProsesResult result = new ProsesResult();
            string sql = "select count(*) from " + _table_name + " where parent_id=" + id;
            int jml = SqlHelper.ExecuteScalarInt(sql);
            if (jml > 0)
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "CanNotBeDeleteHasChild", "Tidak dapat dihapus karena mempunyai anak");
            }
            else
            {
                result.status = 1;
                result.title = "";
                result.message = "";
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
                    sql = "delete from sys_menu_rule where menu_id=" + data["id"] + " ";
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (string val in rule_id)
                    {
                        if (val != "")
                        {
                            sql = "insert into sys_menu_rule (menu_id,rule_id) values (" + data["id"] + "," + val + ") ";
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
                    int parentId = 0;
                    if (data["parent_id"] != DBNull.Value)
                    {
                        parentId = int.Parse(data["parent_id"].ToString());
                    }
                    //DataModel.ReorderTree("Update", _table_name, "id", "parent_id", int.Parse(data["id"].ToString()), parentId, int.Parse(data["ordinal"].ToString()), int.Parse(data_old["ordinal"].ToString()));
                    DataModel.ReorderTree2("Update", _table_name, "id", "menu_type_id", "parent_id", int.Parse(data["id"].ToString()), int.Parse(data["menu_type_id"].ToString()), parentId, int.Parse(data["ordinal"].ToString()), int.Parse(data_old["ordinal"].ToString()));
                    DataModel.AuditForm(context, _table_name, _pkKey, DataModel.ActionType.Edit.ToString(), data, data_old, _displayColumn, ",");
                    string[] rule_id = ruleId.Split(',');
                    sql = "delete from sys_menu_rule where menu_id=" + data["id"] + " ";
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (string val in rule_id)
                    {
                        if (val != "")
                        {
                            sql = "insert into sys_menu_rule (menu_id,rule_id) values (" + data["id"] + "," + val + ") ";
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
                foreach (string key in _pkKey)
                {
                    param[key + "_old"] = data[key];
                }
                string sql = "DELETE FROM  " + _table_name + " "
                + " WHERE 1=1 ";
                foreach (string key in _pkKey)
                {
                    sql += " AND " + key + "=@" + key + "_old";
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
                int parentId = 0;
                if (data["parent_id"] != DBNull.Value)
                {
                    parentId = int.Parse(data["parent_id"].ToString());
                }
                //DataModel.ReorderTree("Delete", _table_name, "id", "parent_id", int.Parse(data["id"].ToString()), parentId, 0, int.Parse(data["ordinal"].ToString()));
                DataModel.ReorderTree2("Delete", _table_name, "id", "menu_type_id", "parent_id", int.Parse(data["id"].ToString()), int.Parse(data["menu_type_id"].ToString()), parentId, 0, int.Parse(data["ordinal"].ToString()));
                DataModel.AuditForm(context, _table_name, _pkKey, DataModel.ActionType.Delete.ToString(), data, null, _displayColumn, ",");
                sql = "delete from sys_menu_rule where menu_id=" + data["id"] + " ";
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
        public static IList<lookup_tree> LookupTreeData(lookup_param param)
        {
            IList<lookup_tree> data = new List<lookup_tree>();
            DataTable dt = new DataTable();
            
            string sql = "select id as value, name as text,parent_id from " + _table_name + " where parent_id is null ";
            foreach (lookup_parent parent in param.parent)
            {
                sql += " AND  "+ parent.field+"="+ parent.value+" ";
            }
            sql += " order by ordinal ";
            dt = SqlHelper.GetDataTable(sql);
            //dt = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                lookup_tree item = new lookup_tree();
                item.value = int.Parse(dr["value"].ToString());
                item.text = dr["text"].ToString();
                if (dr["parent_id"].ToString() != "")
                {
                    item.parent_id = int.Parse(dr["parent_id"].ToString());
                }
                else
                {
                    item.parent_id = null;
                }
                item.item = LookupChildData(dr["value"].ToString());
                item.has_child = item.item != null ? true : false;
                item.expanded = item.has_child;
                data.Add(item);
            }
            return data;
        }
        public static IList<lookup_tree> LookupChildData(string parentId)
        {
            IList<lookup_tree> data = new List<lookup_tree>();
            if (parentId != "")
            {
                DataTable dt = new DataTable();
                string sql = "select id as value, name as text,parent_id from " + _table_name + " where parent_id =" + parentId + " order by ordinal";
                dt = SqlHelper.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    lookup_tree item = new lookup_tree();
                    item.value = int.Parse(dr["value"].ToString());
                    item.text = dr["text"].ToString();
                    if (dr["parent_id"].ToString() != "")
                    {
                        item.parent_id = int.Parse(dr["parent_id"].ToString());
                    }
                    else
                    {
                        item.parent_id = null;
                    }
                    item.item = LookupChildData(dr["value"].ToString());
                    item.has_child = item.item != null ? true : false;
                    data.Add(item);
                }
            }
            return data;
        }

        public static DataTable LookupOrderByParent(lookup_param param)
        {
            string whereCond = " WHERE 1=1 ";
            if (param.parent != null)
            {
                foreach (lookup_parent pr in param.parent)
                {
                    if (pr.field == "menu_type_id" && pr.value != null && pr.value != "")
                    {
                        whereCond += " AND " + pr.field + " = " + pr.value + " ";
                    }
                    else {
                        if (pr.value != null && pr.value != "")
                        {
                            whereCond += " AND " + pr.field + " = " + pr.value + " ";
                        }
                        else
                        {
                            whereCond += " AND " + pr.field + " is null ";
                        }
                    }
                    
                }
            }

            string sql = "select ordinal as value , concat(cast(ordinal as varchar(100)),'. ',name) as text from " + _table_name + " " + whereCond + " ";
            sql += " UNION select case when max(ordinal) is null then 1  else max(ordinal) + 1 end  as value , concat(cast((case when max(ordinal) is null then 1  else max(ordinal) + 1 end) as varchar(100)),'. ...')  as text from " + _table_name + " " + whereCond + " ";
            sql += " order by value ";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }
        public static DataTable LookupMenuType(lookup_param param)
        {
            string sql = "select id as value , name as text from sys_menu_type order by value ";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }
        public static int GetLastOrdinalByParent(string menu_type_id,string parentid)
        {
            string whereCond = " WHERE 1=1 ";
            if (parentid != null && parentid != "")
            {
                whereCond += " AND parent_id= " + parentid + " ";
            }
            else
            {
                if (menu_type_id != null && menu_type_id != "") {
                    whereCond += " AND menu_type_id="+ menu_type_id + " AND parent_id is null ";
                }
            }
            string sql = " select case when max(ordinal) is null then 1  else max(ordinal) + 1 end  as lastordinal from " + _table_name + " " + whereCond + " ";
            int data = SqlHelper.ExecuteScalarInt(sql);
            return data;
        }
        public static string GetRuleId(string id)
        {
            string result = "";
            string sql = "select rule_id from sys_menu_rule where menu_id = " + id + " ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                result += result != "" ? "," : "";
                result += dr["rule_id"];
            }
            return result;
        }
        public static IList<lookup_tree_check> getItemRuleTreeCheck(string menuId)
        {
            IList<lookup_tree_check> result = new List<lookup_tree_check>();
            string sql = "select a.id,a.nama,case when b.rule_id is not null then 1 else 0 end as checked from sys_rules as a "
            + " left outer join sys_menu_rule as b on a.id = b.rule_id  and b.menu_id=" + menuId + " "
            + " where a.parent_id is null " +
            " order by a.ordinal";
            DataTable dtParent = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dtParent.Rows)
            {
                lookup_tree_check item = new lookup_tree_check();
                item.id = int.Parse(dr["id"].ToString());
                item.parent_id = null;
                item.text = dr["nama"].ToString();
                item.check = dr["checked"].ToString() == "1" ? true : false;
                item.item = getItemChaildRuleTreeCheck(menuId, dr["id"].ToString());
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
        public static IList<lookup_tree_check> getItemChaildRuleTreeCheck(string menuId, string parent_id)
        {
            IList<lookup_tree_check> result = new List<lookup_tree_check>();
            string sql = "select a.id,a.nama,case when b.rule_id is not null then 1 else 0 end as checked from sys_rules as a "
            + " left outer join sys_menu_rule as b on a.id = b.rule_id and b.menu_id=" + menuId + " "
            + " where a.parent_id =" + parent_id + " " +
            " order by a.ordinal";
            DataTable dtParent = SqlHelper.GetDataTable(sql);
            foreach (DataRow dr in dtParent.Rows)
            {
                lookup_tree_check item = new lookup_tree_check();
                item.id = int.Parse(dr["id"].ToString());
                item.parent_id = int.Parse(parent_id);
                item.text = dr["nama"].ToString();
                item.check = dr["checked"].ToString() == "1" ? true : false;
                item.item = getItemChaildRuleTreeCheck(menuId, dr["id"].ToString());
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

    }
}
