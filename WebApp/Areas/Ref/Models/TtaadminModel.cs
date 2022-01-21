using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using WebApp.Models;

namespace WebApp.Areas.Ref.Models
{
    public class TtaadminModel
    {
        public static string _table_name = "ref_tta_admin";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 1;
        public static string[] _unKey = { "tta_id,start_date" };
        public static string _columnOrder = "ref_tta_admin.id";
        public static string _displayColumn = "tta_id,admin_nama";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="tta_id"
                ,field_alias = "tta_id_text"
                ,table_name = "ref_tta"
                ,table_alias = "lk_tta_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_tta_admin.tta_id = lk_tta_id.id"
                ,select_column = "(cast(lk_tta_id.kode as varchar(50)) + ' - '+cast(lk_tta_id.nama as varchar(50))) as tta_id_text"
            },
            new RelationTable{
                field_name="admin_id"
                ,field_alias = "admin_id_text"
                ,table_name = "ta_person"
                ,table_alias = "lk_admin_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_tta_admin.admin_id = lk_admin_id.id"
                ,select_column = "(cast(lk_admin_id.nrp as varchar(50)) + ' - '+cast(lk_admin_id.nama as varchar(50))) as admin_id_text"
            },
            new RelationTable{
                field_name="aktif_id"
                ,field_alias = "aktif_id_text"
                ,table_name = "ref_literal"
                ,table_alias = "lk_aktif_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_tta_admin.aktif_id = lk_aktif_id.id and lk_aktif_id.jenis='aktif_tidak' "
                ,select_column = "lk_aktif_id.nama as aktif_id_text"
            }            
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string tta_id { get; set; }
            public string tta_id_text { get; set; }
            public string admin_id { get; set; }
            public string admin_id_text { get; set; }
            public string admin_nrp { get; set; }
            public string admin_nama { get; set; }
            public string aktif_id { get; set; }
            public string aktif_id_text { get; set; }
            public string start_date { get; set; }
            public string dt_start_date { get; set; }
            public string end_date { get; set; }
            public string dt_end_date { get; set; }
            public string insert_by { get; set; }
            public string insert_at { get; set; }
            public string update_by { get; set; }
            public string update_at { get; set; }
        }
        public static ParamGrid GetListParam()
        {
            ParamGrid param = new ParamGrid
            {
                ShowFilter = "",
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
                export_word = "0",
                export_csv = "0",
                export_pdf = "0",
                btnView = "1",
                btn_view_rule = "",
                btn_view_target = "modal",
                btn_view_modal_width = "700",
                btn_view_modal_height = "400",
                btnAdd = "1",
                btn_add_rule = "",
                btn_add_target = "modal",
                btn_add_modal_width = "700",
                btn_add_modal_height = "400",
                btnCopy = "0",
                btn_copy_rule = "",
                btn_copy_target = "modal",
                btn_copy_modal_width = "700",
                btn_copy_modal_height = "400",
                btnEdit = "1",
                btn_edit_rule = "",
                btn_edit_target = "modal",
                btn_edit_modal_width = "700",
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
                new GridColumn{
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
                    encoded= false,
                    filterable= false,
                    sortable= false,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "id",
                    field= _table_name+".id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "{0:N0}",
                    attributes= "{ \"style\": \"text-align: right; \" }",
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
                    name= "tta_id",
                    field= _table_name+".tta_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tta_id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "{0:N0}",
                    attributes= "{ \"style\": \"text-align: right; \" }",
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
                    name= "tta_id_text",
                    field= "(cast(lk_tta_id.kode as varchar(50)) + ' - '+cast(lk_tta_id.nama as varchar(50)))",
                    table_name = "ref_tta",
                    table_alias = "lk_tta_id",
                    title= @ResxHelper.GetValue(_table_name,"tta_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
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
                    name= "admin_id",
                    field= _table_name+".admin_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"admin_id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "{0:N0}",
                    attributes= "{ \"style\": \"text-align: right; \" }",
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
                    name= "admin_id_text",
                    field= "(cast(lk_admin_id.nrp as varchar(50)) + ' - '+cast(lk_admin_id.nama as varchar(50)))",
                    table_name = "ta_person",
                    table_alias = "lk_admin_id",
                    title= @ResxHelper.GetValue(_table_name,"admin_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
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
                    name= "admin_nrp",
                    field= _table_name+".admin_nrp",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"admin_nrp"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "{0:#}",
                    attributes= "{ \"style\": \"text-align: right; \" }",
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
                    name= "admin_nama",
                    field= _table_name+".admin_nama",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"admin_nama"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= false,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "aktif_id",
                    field= _table_name+".aktif_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"aktif_id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "{0:N0}",
                    attributes= "{ \"style\": \"text-align: right; \" }",
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
                    name= "aktif_id_text",
                    field= "lk_aktif_id.nama",
                    table_name = "ref_literal",
                    table_alias = "lk_aktif_id",
                    title= @ResxHelper.GetValue(_table_name,"aktif_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= aktif_id_bg_color#; color:#= aktif_id_font_color#;\" }",
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
                    name= "aktif_id_bg_color",
                    field= "lk_aktif_id.bg_color",
                    table_name = "ref_literal",
                    table_alias = "lk_aktif_id",
                    title= @ResxHelper.GetValue(_table_name,"aktif_id"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= false,
                    sortable= false,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "aktif_id_font_color",
                    field= "lk_aktif_id.font_color",
                    table_name = "ref_literal",
                    table_alias = "lk_aktif_id",
                    title= @ResxHelper.GetValue(_table_name,"aktif_id"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= false,
                    sortable= false,
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "start_date",
                    field= _table_name+".start_date",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"start_date"),
                    type= "date",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
                    format= "{0:dd/MM/yyyy}",
                    attributes= "{ \"style\": \"text-align: center; \" }",
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
                    name= "end_date",
                    field= _table_name+".end_date",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"end_date"),
                    type= "date",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
                    format= "{0:dd/MM/yyyy}",
                    attributes= "{ \"style\": \"text-align: center; \" }",
                    template= "",
                    encoded= false,
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
                        var myColumn = column_att.ToList().Find(item => item.name == itemFilter.name);
                        whereCond += " AND " + myColumn.field + " " + itemFilter.opr + " @" + itemFilter.name;
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
            if (sort_by == "")
            {
                string OrderBy = "";
                if (_columnOrder != "") {
                    string[] colOrders = _columnOrder.Split(',');
                    foreach (string item in colOrders)
                    {
                        if (item != "")
                        {
                            string[] items = item.Split('.');
                            if (items.Length == 1)
                            {
                                OrderBy += OrderBy != "" ? "," : "";
                                OrderBy += _table_name + "." + item;
                            }
                            else
                            {
                                OrderBy += OrderBy != "" ? "," : "";
                                OrderBy += item;
                            }
                        }
                    }
                }
                if (OrderBy == "") {
                    foreach (string item in _pkKey) {
                        OrderBy += OrderBy != "" ? "," : "";
                        OrderBy += _table_name + "." + item;
                    }
                }
                sort_by = OrderBy;
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
                    sql_join_field += "," + tr.select_column ;
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
                if (auto_increament == 1 && increament_type == 0) {
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
                }
                
                if (result.status == 1)
                {
                    result = IsExistUN(data, dataOld);
                }
            }
            else
            {
                //Check Existing PK
                if (auto_increament == 1 && increament_type == 0)
                {
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
                    }
                }
                if(result.status==1)
                {
                    result = IsExistUN(data, dataOld);
                }
            }
            return result;
        }
        public static ProsesResult IsExistUN(OrderedDictionary data, OrderedDictionary dataOld)
        {
            ProsesResult result = new ProsesResult();
            result.status = 1;
            result.message = "";
            string sql = "";
            string msg = "";
            int jml;
            OrderedDictionary param = new OrderedDictionary();
            if (dataOld == null)
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
                            msg += ResxHelper.GetValue(_table_name, itemKey) ;
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
            else
            {
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
                            msg += ResxHelper.GetValue(_table_name, itemKey) ;

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
            return result;
        }
        public static int GetLastId()
        {
            int id = 0;
            string pk ="";
            foreach (string key in _pkKey)
            {
                pk = key;
            }
            string sql = "select case when max("+ pk + ") is null then 0 else max("+ pk + ") end as last_id from " + _table_name + " ";
            id = SqlHelper.ExecuteScalarInt(sql);
            return id;
        }
        public static int GetNewId()
        {
            int id = 0;
            string pk = "";
            foreach (string key in _pkKey)
            {
                pk = key;
            }
            string sql = "select case when max("+ pk + ") is null then 1 else (max("+ pk + ") + 1) end as last_id from " + _table_name + " ";
            id = SqlHelper.ExecuteScalarInt(sql);
            return id;
        }
        public static ProsesResult Insert(IHostingEnvironment _hostingEnvironment, HttpContext context, OrderedDictionary data)
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
                        if (!(auto_increament == 1 && increament_type == 1 && _pkKey.Contains(entry.Key)))
                        {
                            sql_field += sql_field != "" ? "," : "";
                            sql_field += entry.Key;
                            sql_value += sql_value != "" ? "," : "";
                            sql_value += "@" + entry.Key;
                        }
                    }
                    string sql = "INSERT INTO " + _table_name + " (" + sql_field + ")  VALUES(" + sql_value + ");  ";
                    SqlHelper.ExecuteNonQuery(sql, data);
					var PkValue = new OrderedDictionary();
                    if (auto_increament == 1 && increament_type == 1)
                    {
                        result.pk = GetLastId().ToString();
                        string pk = "";
                        foreach (string key in _pkKey)
                        {
                            pk = key;
                        }
                        data[pk] = result.pk;
						PkValue[pk] = result.pk;
                    }else {
                        foreach (string key in _pkKey)
                        {
                            result.pk += result.pk != "" ? "," : "";
                            result.pk += data[key];
							PkValue[key] = data[key];
                        }
                    }

                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "SuccessAdd");
                    result.message = ResxHelper.GetValue("Message", "SuccessAdd");
                    UpdateLastActiveAdmin(data["tta_id"].ToString());
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
        public static ProsesResult Update(IHostingEnvironment _hostingEnvironment, HttpContext context, OrderedDictionary data, OrderedDictionary data_old)
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
                        if (!(auto_increament == 1 && increament_type == 1 && _pkKey.Contains(entry.Key))) {
                            sql_set += sql_set != "" ? "," : "";
                            sql_set += entry.Key + "=@" + entry.Key;
                        }
                    }
                    string sql = "UPDATE " + _table_name + " SET " + sql_set + " "
                    + " WHERE 1=1 ";
                    OrderedDictionary param = data;
					var PkValue = new OrderedDictionary();
                    foreach (string key in _pkKey)
                    {
                        sql += " AND " + key + "=@" + key + "_old";
                        param[key + "_old"] = data_old[key];
						PkValue[key] = data[key];
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
                    UpdateLastActiveAdmin(data["tta_id"].ToString());
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
        public static ProsesResult Delete(IHostingEnvironment _hostingEnvironment, HttpContext context, OrderedDictionary data)
        {
            ProsesResult result = new ProsesResult();
            try
            {
                OrderedDictionary param = data;
                string last_val = "";
                foreach (string key in _pkKey)
                {
                    param[key + "_old"] = data[key];
                    last_val = data[key].ToString();
                }
                DataModel.DeleteCascade(context,_table_name, last_val);
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
				var PkValue = new OrderedDictionary();
                foreach (string key in _pkKey)
                {
                    result.pk += result.pk != "" ? "," : "";
                    result.pk += data[key];
					PkValue[key] = data[key];
                }
                UpdateLastActiveAdmin(data["tta_id"].ToString());
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
        public static DataTable LookupData(lookup_param param)
        {
            IList<GridColumn> column_att = GetGridColumn();
            var table_join_list = new List<string>();
            DataTable data = new DataTable();
            string sql_item = "";
            string select_distinct = "";
            if (param.item != null)
            {
                if (param.item.value != null)
                {
                    sql_item += sql_item != "" ? ", " : "";
                    sql_item += _table_name + "." + param.item.value + " as value ";
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
                            item_text += item_text != "" ? "+'" + separator + "'+" : "";
                            item_text += " CAST(" + _table_name + "." + item+" as varchar(50)) ";
                        }
                        sql_item += ",(" + item_text + ") as text ";
                    }
                    else
                    {
                        sql_item += "," + _table_name + "." + param.item.text + " as text ";
                    }
                }
                if (param.item.attribute != null && param.item.attribute.Length > 0)
                {
                    foreach (string col_name in param.item.attribute)
                    {
                        var joinData = _join.ToList().Find(item => item.field_name == col_name);
                        if (joinData != null)
                        {
                            if (!table_join_list.Contains(joinData.table_name))
                            {
                                table_join_list.Add(joinData.table_name);
                            }
                            sql_item += "," + joinData.select_column.Replace("_text","");
                        }
                        else {
                            sql_item += "," + _table_name+"."+col_name;
                        }
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
                        item_text += " CAST(" + item+" as varchar(50)) ";
                    }
                    sql_item += ",(" + item_text + ") as text ";
                }
                else
                {
                    sql_item += "," + _displayColumn + " as text ";
                }
            }

            string sql = "SELECT " + select_distinct + " " + sql_item + " from " + _table_name;

            //=============================================
            string sql_join_list = "";
            foreach (RelationTable tr in _join)
            {
                if (table_join_list.Contains(tr.table_name))
                {
                    sql_join_list += " " + tr.join_type + tr.table_name + " as " + tr.table_alias + " ON " + tr.join_on;
                }
            }
            sql += sql_join_list;
            //==============================================
            string sql_where = " WHERE 1=1 ";
            OrderedDictionary parameter = new OrderedDictionary();
            if (param.parent != null)
            {
                foreach (lookup_parent parent in param.parent)
                {
                    if (parent.field != null)
                    {
                        var gc = column_att.ToList().Find(item => item.name == parent.field);
                        sql_where += " AND " + gc.field;
                        if (parent.value != null)
                        {
                            if (parent.opr != null)
                            {
                                sql_where += " " + parent.opr + " ";
                            }
                            else
                            {
                                sql_where += " = ";
                            }
                            sql_where += " @" + parent.field;
                            parameter[parent.field] = parent.value;
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
                foreach (lookup_order order in param.order)
                {
                    if (order.field != null)
                    {
                        var gc = column_att.ToList().Find(item => item.name == order.field);
                        sql_order += sql_order != "" ? "," : "";
                        sql_order += gc.field;
                        if (order.dir != null)
                        {
                            sql_order += " " + order.dir;
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

        public static DataTable GetAdminData(string tta_id)
        {
            if (tta_id != "")
            {
                string sql = "select   " +
                " a.id " +
                " ,a.tta_id " +
                " ,a.admin_id " +
                " ,a.admin_nrp " +
                " ,a.admin_nama " +
                " ,a.aktif_id " +
                " ,b.nama as aktif_id_text " +
                " ,a.start_date " +
                " ,a.end_date " +
                " from[ref_tta_admin] as a " +
                " left outer join ref_literal as b " +
                "     on a.aktif_id = b.id and b.jenis = 'aktif_tidak' " +
                " where a.tta_id = " + tta_id + " " +
                " order by a.start_date desc ";
                DataTable data = SqlHelper.GetDataTable(sql);
                return data;
            }
            else
            {
                return null;
            }
        }
        public static void UpdateLastActiveAdmin(string tta_id)
        {
            string sql = "select *,DATEADD(day,-1, start_date) as endda from ref_tta_admin where tta_id='" + tta_id + "' order by start_date desc ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            string end_date = "null";
            string aktif_id = "1";
            foreach (DataRow dr in dt.Rows)
            {
                sql = "update ref_tta_admin set aktif_id=" + aktif_id + ", end_date=" + end_date + " where id=" + dr["id"].ToString() + " ";
                SqlHelper.ExecuteNonQuery(sql);
                aktif_id = "0";
                end_date = String.Format("'{0:yyyy-MM-dd}'", dr["endda"]);
            }

            sql = "select tta_id as id,id as last_admin_id,admin_id,admin_nrp,admin_nama from ref_tta_admin where tta_id='" + tta_id + "' and aktif_id=1";
            dt = SqlHelper.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                OrderedDictionary data = new OrderedDictionary();
                data["id"] = dr["id"];
                data["last_admin_id"] = dr["last_admin_id"];
                data["admin_id"] = dr["admin_id"];
                data["admin_nrp"] = dr["admin_nrp"];
                data["admin_nama"] = dr["admin_nama"];
                SqlHelper.ConvertEmptyValuesToDBNull(data);
                sql = "update ref_tta set last_admin_id=@last_admin_id, admin_id=@admin_id, admin_nrp=@admin_nrp, admin_nama=@admin_nama where id=@id";
                SqlHelper.ExecuteNonQuery(sql, data);
            }
            else
            {
                sql = "update ref_tta set last_admin_id=null, admin_id=null, admin_nrp=null, admin_nama=null where id=" + tta_id + " ";
                SqlHelper.ExecuteNonQuery(sql);
            }
        }
    }
}
