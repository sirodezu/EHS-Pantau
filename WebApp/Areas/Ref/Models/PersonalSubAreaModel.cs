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
    public class PersonalSubAreaModel
    {
        public static string _table_name = "ref_personal_sub_area";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 0;
        public static string[] _unKey = { "kode","nama" };
        public static string _columnOrder = "ref_personal_sub_area.kode";
        public static string _displayColumn = "kode,nama";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="ehs_area_id"
                ,field_alias = "ehs_area_id_text"
                ,table_name = "ref_ehs_area"
                ,table_alias = "lk_ehs_area_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_personal_sub_area.ehs_area_id = lk_ehs_area_id.id"
                ,select_column = "(cast(lk_ehs_area_id.kode as varchar(50)) + ' - '+cast(lk_ehs_area_id.nama as varchar(50))) as ehs_area_id_text"
            },
            new RelationTable{
                field_name="ba_id"
                ,field_alias = "ba_id_text"
                ,table_name = "ref_business_area"
                ,table_alias = "lk_ba_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_personal_sub_area.ba_id = lk_ba_id.id"
                ,select_column = "(cast(lk_ba_id.kode as varchar(50)) + ' - '+cast(lk_ba_id.nama as varchar(50))) as ba_id_text"
            },
            new RelationTable{
                field_name="pa_id"
                ,field_alias = "pa_id_text"
                ,table_name = "ref_personal_area"
                ,table_alias = "lk_pa_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_personal_sub_area.pa_id = lk_pa_id.id"
                ,select_column = "(cast(lk_pa_id.kode as varchar(50)) + ' - '+cast(lk_pa_id.nama as varchar(50))) as pa_id_text"
            },
            new RelationTable{
                field_name="head_type_id"
                ,field_alias = "head_type_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_head_type_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_personal_sub_area.head_type_id = lk_head_type_id.id AND lk_head_type_id.cat_id=147"
                ,select_column = "lk_head_type_id.nama as head_type_id_text"
            },
            new RelationTable{
                field_name="head_id"
                ,field_alias = "head_id_text"
                ,table_name = "ta_mp"
                ,table_alias = "lk_head_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_personal_sub_area.head_id = lk_head_id.id"
                ,select_column = "(cast(lk_head_id.NRP as varchar(50)) + ' - '+cast(lk_head_id.nama_lengkap as varchar(50))) as head_id_text"
            },
            new RelationTable{
                field_name="status_id"
                ,field_alias = "status_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ref_personal_sub_area.status_id = lk_status_id.id AND lk_status_id.cat_id=3"
                ,select_column = "lk_status_id.nama as status_id_text"
            }
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string ehs_area_id { get; set; }
            public string ehs_area_id_text { get; set; }
            public string ba_id { get; set; }
            public string ba_id_text { get; set; }
            public string pa_id { get; set; }
            public string pa_id_text { get; set; }
            public string kode { get; set; }
            public string nama { get; set; }
            public string keterangan { get; set; }
            public string head_type_id { get; set; }
            public string head_type_id_text { get; set; }
            public string head_id { get; set; }
            public string head_id_text { get; set; }
            public string head_nrp { get; set; }
            public string head_nama { get; set; }
            public string status_id { get; set; }
            public string status_id_text { get; set; }
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
                export_exl = "1",
                export_word = "0",
                export_csv = "0",
                export_pdf = "0",
                btnView = "1",
                btn_view_rule = "",
                btn_view_target = "modal",
                btn_view_modal_width = "0",
                btn_view_modal_height = "400",
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
                    field= _table_name+".id",
                    field_order= _table_name+".id",
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
                    name= "ehs_area_id",
                    field= _table_name+".ehs_area_id",
                    field_order= _table_name+".ehs_area_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id"),
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
                    name= "ehs_area_id_text",
                    field= "lk_ehs_area_id.kode",
                    field_order= "lk_ehs_area_id.kode",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id",
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "60px",
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
                    name= "ba_id",
                    field= _table_name+".ba_id",
                    field_order= _table_name+".ba_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ba_id"),
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
                    name= "ba_id_text",
                    field= "lk_ba_id.kode",
                    field_order= "lk_ba_id.kode",
                    table_name = "ref_business_area",
                    table_alias = "lk_ba_id",
                    title= @ResxHelper.GetValue(_table_name,"ba_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "60px",
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
                    name= "pa_id",
                    field= _table_name+".pa_id",
                    field_order= _table_name+".pa_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pa_id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "200px",
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
                    name= "pa_id_text",
                    field= "lk_pa_id.kode",
                    field_order= "lk_pa_id.kode",
                    table_name = "ref_personal_area",
                    table_alias = "lk_pa_id",
                    title= @ResxHelper.GetValue(_table_name,"pa_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "60px",
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
                    name= "kode",
                    field= _table_name+".kode",
                    field_order= _table_name+".kode",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"kode"),
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
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "nama",
                    field= _table_name+".nama",
                    field_order= _table_name+".nama",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"nama"),
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
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "keterangan",
                    field= _table_name+".keterangan",
                    field_order= _table_name+".keterangan",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"keterangan"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
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
                    name= "head_type_id",
                    field= _table_name+".head_type_id",
                    field_order= _table_name+".head_type_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"head_type_id"),
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
                    name= "head_type_id_text",
                    field= "lk_head_type_id.nama",
                    field_order= "lk_head_type_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_head_type_id",
                    title= @ResxHelper.GetValue(_table_name,"head_type_id"),
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
                    name= "head_id",
                    field= _table_name+".head_id",
                    field_order= _table_name+".head_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"head_id"),
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
                    name= "head_id_text",
                    field= "(cast(lk_head_id.NRP as varchar(50)) + ' - '+cast(lk_head_id.nama_lengkap as varchar(50)))",
                    field_order= "(cast(lk_head_id.NRP as varchar(50)) + ' - '+cast(lk_head_id.nama_lengkap as varchar(50)))",
                    table_name = "ta_mp",
                    table_alias = "lk_head_id",
                    title= @ResxHelper.GetValue(_table_name,"head_id"),
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
                    name= "head_nrp",
                    field= _table_name+".head_nrp",
                    field_order= _table_name+".head_nrp",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"head_nrp"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
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
                    name= "head_nama",
                    field= _table_name+".head_nama",
                    field_order= _table_name+".head_nama",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"head_nama"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
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
                    name= "status_id",
                    field= _table_name+".status_id",
                    field_order= _table_name+".status_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "90px",
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
                    name= "status_id_text",
                    field= "lk_status_id.nama",
                    field_order= "lk_status_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_id",
                    title= @ResxHelper.GetValue(_table_name,"status_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_id_bg_color#; color:#= status_id_font_color#;\" }",
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
                    name= "status_id_bg_color",
                    field= "lk_status_id.bg_color",
                    field_order= "lk_status_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_id",
                    title= @ResxHelper.GetValue(_table_name,"status_id"),
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
                    name= "status_id_font_color",
                    field= "lk_status_id.font_color",
                    field_order= "lk_status_id.font_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_id",
                    title= @ResxHelper.GetValue(_table_name,"status_id"),
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
                    field_order= _table_name+".start_date",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"start_date"),
                    type= "date",
                    select= true,
                    display= true,
                    hidden= true,
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
                    field_order= _table_name+".end_date",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"end_date"),
                    type= "date",
                    select= true,
                    display= true,
                    hidden= true,
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
                        if (itemFilter.opr == "like")
                        {
                            whereCond += " AND " + myColumn.field + " " + itemFilter.opr + " '%" + itemFilter.value+"%'";
                        }
                        else {
                            whereCond += " AND " + myColumn.field + " " + itemFilter.opr + " @" + itemFilter.name;
                        }
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
                        if (mysort.field_order != null)
                        {
                            sort_by += mysort.field_order + " " + param.sort[i].dir;
                        }
                        else {
                            sort_by += mysort.field + " " + param.sort[i].dir;
                        }
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
            string aggregateitem = "";
            if (param.aggregate != null)
            {
                if (param.aggregate.Count > 0)
                {
                    for (int i = 0; i < param.aggregate.Count; i++)
                    {
                        var myagg = column_att.ToList().Find(item => item.name == param.aggregate[i].field);
                        aggregateitem += aggregateitem != "" ? ", " : "";
                        aggregateitem += param.aggregate[i].aggregate + "(" + myagg.field + ") as " + param.aggregate[i].field;
                    }
                    string sql_aggr = "select " + aggregateitem + " from " + _table_name + " ";
                    sql_aggr += sql_join_list;
                    sql_aggr += whereCond;
                    DataTable dt = SqlHelper.GetDataTable(sql_aggr, parameter);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < param.aggregate.Count; i++)
                        {
                            Hashtable item = new Hashtable();
                            item.Add(param.aggregate[i].aggregate, dt.Rows[0][i].ToString());
                            aggregates.Add(param.aggregate[i].field, item);
                        }
                    }
                }

            }
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
                            result.pk += result.pk != null && result.pk != "" ? "," : "";
                            result.pk += data[key];
							PkValue[key] = data[key];
                        }
                    }

                    result.status = 1;
                    result.title = ResxHelper.GetValue("Message", "SuccessAdd");
                    result.message = ResxHelper.GetValue("Message", "SuccessAdd");
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
        //=================================================
        public static DataTable GetHeadData(string psa_id)
        {
            if (psa_id != "")
            {
                string sql = "select   " +
                " a.id " +
                " ,a.psa_id " +
                " ,a.head_type_id " +
                " ,c.nama as head_type_id_text " +
                " ,a.head_id " +
                " ,a.head_nrp " +
                " ,a.head_nama " +
                " ,a.status_id " +
                " ,b.nama as status_id_text " +
                " ,a.start_date " +
                " ,a.end_date " +
                " from[ref_personal_sub_area_head] as a " +
                " left outer join ref_literal_data as b on a.status_id = b.id and b.cat_id = 3 " +
                " left outer join ref_literal_data as c on a.head_type_id = c.id and c.cat_id = 147 " +
                " where a.psa_id = " + psa_id + " " +
                " order by a.start_date desc ";
                DataTable data = SqlHelper.GetDataTable(sql);
                return data;
            }
            else
            {
                return null;
            }
        }
        public static void UpdateLastActive(string psa_id)
        {
            string sql = "select *,DATEADD(day,-1, start_date) as endda from ref_personal_sub_area_head where psa_id='" + psa_id + "' order by start_date desc ";
            DataTable dt = SqlHelper.GetDataTable(sql);
            string end_date = "null";
            string status_id = "1";
            foreach (DataRow dr in dt.Rows)
            {
                sql = "update ref_personal_sub_area_head set status_id=" + status_id + ", end_date=" + end_date + " where id=" + dr["id"].ToString() + " ";
                SqlHelper.ExecuteNonQuery(sql);
                status_id = "0";
                end_date = String.Format("'{0:yyyy-MM-dd}'", dr["endda"]);
            }

            sql = "select psa_id as id,head_type_id,head_id,head_nrp,head_nama from ref_personal_sub_area_head where psa_id='" + psa_id + "' and status_id=1";
            dt = SqlHelper.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                OrderedDictionary data = new OrderedDictionary();
                data["id"] = dr["id"];
                data["head_type_id"] = dr["head_type_id"];
                data["head_id"] = dr["head_id"];
                data["head_nrp"] = dr["head_nrp"];
                data["head_nama"] = dr["head_nama"];
                SqlHelper.ConvertEmptyValuesToDBNull(data);
                sql = "update ref_personal_sub_area set head_type_id=@head_type_id,head_id=@head_id, head_nrp=@head_nrp, head_nama=@head_nama where id=@id";
                SqlHelper.ExecuteNonQuery(sql, data);
            }
            else
            {
                sql = "update ref_personal_sub_area set head_type_id=null, head_id=null, head_nrp=null, head_nama=null where id=" + psa_id + " ";
                SqlHelper.ExecuteNonQuery(sql);
            }
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
        public static DataTable LookupDataPSA(HttpContext context, lookup_param param)
        {
            string obj_data = "";
            string sql = "";
            string whereCond = " WHERE 1=1 and lk_psa_id.status_id=1";
            string sql_item = "";
            if (param.item.value != null)
            {
                sql_item += sql_item != "" ? ", " : "";
                sql_item += "lk_psa_id." + param.item.value + " as value ";
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
                        item_text += " CAST(lk_psa_id." + item + " as varchar(50)) ";
                    }
                    sql_item += ",(" + item_text + ") as text ";
                }
                else
                {
                    sql_item += ",lk_psa_id." + param.item.text + " as text ";
                }
            }

            if (param.parent != null)
            {
                foreach (lookup_parent parent in param.parent)
                {
                    if (parent.field == "obj_data") {
                        obj_data = parent.value;
                    }
                    else if (parent.field != null)
                    {
                        whereCond += " AND lk_psa_id." + parent.field;
                        if (parent.value != null)
                        {
                            whereCond += " = " + parent.value;
                        }
                        else
                        {
                            whereCond += " = '' ";
                        }
                    }
                }
            }
            if (obj_data != "")
            {
                //string rule_view_all = "";
                //string rule_edit_all = "";
                //switch (obj_data)
                //{
                //    case "Accident":
                //        rule_view_all = "AccidentViewAll";
                //        rule_edit_all = "AccidentEditAll";
                //        break;
                //    case "Audit":
                //        rule_view_all = "AuditViewAll";
                //        rule_edit_all = "AuditEditAll";
                //        break;
                //    case "Campaign":
                //        rule_view_all = "CampaignViewAll";
                //        rule_edit_all = "CampaignEditAll";
                //        break;
                //    case "Document":
                //        rule_view_all = "DocumentViewAll";
                //        rule_edit_all = "DocumentEditAll";
                //        break;
                //    case "Emergency":
                //        rule_view_all = "EmergencyViewAll";
                //        rule_edit_all = "EmergencyEditAll";
                //        break;
                //    case "Environment":
                //        rule_view_all = "EnvironmentViewAll";
                //        rule_edit_all = "EnvironmentEditAll";
                //        break;
                //    case "Fasilitas":
                //        rule_view_all = "FasilitasViewAll";
                //        rule_edit_all = "FasilitasEditAll";
                //        break;
                //    case "FreeManHours":
                //        rule_view_all = "FreeManHoursViewAll";
                //        rule_edit_all = "FreeManHoursEditAll";
                //        break;
                //    case "General":
                //        rule_view_all = "GeneralViewAll";
                //        rule_edit_all = "GeneralEditAll";
                //        break;
                //    case "Health":
                //        rule_view_all = "HealthViewAll";
                //        rule_edit_all = "HealthEditAll";
                //        break;
                //    case "Legal":
                //        rule_view_all = "LegalViewAll";
                //        rule_edit_all = "LegalEditAll";
                //        break;
                //    case "LiteralData":
                //        rule_view_all = "LiteralDataViewAll";
                //        rule_edit_all = "LiteralDataEditAll";
                //        break;
                //    case "ManPower":
                //        rule_view_all = "ManPowerViewAll";
                //        rule_edit_all = "ManPowerEditAll";
                //        break;
                //    case "Organization":
                //        rule_view_all = "OrganizationViewAll";
                //        rule_edit_all = "OrganizationEditAll";
                //        break;
                //    case "Performance":
                //        rule_view_all = "PerformanceViewAll";
                //        rule_edit_all = "PerformanceEditAll";
                //        break;
                //    case "Person":
                //        rule_view_all = "PersonViewAll";
                //        rule_edit_all = "PersonEditAll";
                //        break;
                //    case "Training":
                //        rule_view_all = "TrainingViewAll";
                //        rule_edit_all = "TrainingEditAll";
                //        break;
                //    case "Compliance":
                //        rule_view_all = "ComplianceViewAll";
                //        rule_edit_all = "ComplianceEditAll";
                //        break;
                //    case "Condition":
                //        rule_view_all = "ConditionViewAll";
                //        rule_edit_all = "ConditionEditAll";
                //        break;
                //    case "Profile":
                //        rule_view_all = "ProfileViewAll";
                //        rule_edit_all = "ProfileEditAll";
                //        break;
                //    case "DashboardEHSPerformance":
                //        rule_view_all = "DashboardEHSPerformance";
                //        rule_edit_all = "DashboardEHSPerformance";
                //        break;
                //    case "DashboardServiceReport":
                //        rule_view_all = "DashboardServiceReport";
                //        rule_edit_all = "DashboardServiceReport";
                //        break;
                //    default:
                //        rule_view_all = "";
                //        rule_edit_all = "";
                //        break;

                //}
                string rule_allow = "";
                switch (obj_data)
                {
                    case "Accident":
                        rule_allow = "AccidentViewAll,AccidentEditAll";
                        break;
                    case "Audit":
                        rule_allow = "AuditViewAll,AuditEditAll";
                        break;
                    case "Campaign":
                        rule_allow = "CampaignViewAll,CampaignEditAll";
                        break;
                    case "Document":
                        rule_allow = "DocumentViewAll,DocumentEditAll";
                        break;
                    case "Emergency":
                        rule_allow = "EmergencyViewAll,EmergencyEditAll";
                        break;
                    case "Environment":
                        rule_allow = "EnvironmentViewAll,EnvironmentEditAll";
                        break;
                    case "Fasilitas":
                        rule_allow = "FasilitasViewAll,FasilitasEditAll";
                        break;
                    case "FreeManHours":
                        rule_allow = "FreeManHoursViewAll,FreeManHoursEditAll";
                        break;
                    case "General":
                        rule_allow = "GeneralViewAll,GeneralEditAll";
                        break;
                    case "Health":
                        rule_allow = "HealthViewAll,HealthEditAll";
                        break;
                    case "Legal":
                        rule_allow = "LegalViewAll,LegalEditAll";
                        break;
                    case "LiteralData":
                        rule_allow = "LiteralDataViewAll,LiteralDataEditAll";
                        break;
                    case "ManPower":
                        rule_allow = "ManPowerViewAll,ManPowerEditAll";
                        break;
                    case "Organization":
                        rule_allow = "OrganizationViewAll,OrganizationEditAll";
                        break;
                    case "Performance":
                        rule_allow = "PerformanceViewAll,PerformanceEditAll";
                        break;
                    case "Person":
                        rule_allow = "PersonViewAll,PersonEditAll";
                        break;
                    case "Training":
                        rule_allow = "TrainingViewAll,TrainingEditAll";
                        break;
                    case "Compliance":
                        rule_allow = "FasilitasViewAll,FasilitasEditAll,LegalViewAll,LegalEditAll,PengukuranViewAll,PengukuranEditAll,EnvironmentViewAll,EnvironmentEditAll";
                        break;
                    case "Condition":
                        rule_allow = "EmergencyViewAll,EmergencyEditAll,AuditViewAll,AuditEditAll,CampaignViewAll,CampaignEditAll";
                        break;
                    case "Profile":
                        rule_allow = "ManPowerViewAll,ManPowerEditAll,TrainingViewAll,TrainingEditAll,FreeManHoursViewAll,FreeManHoursEditAll,AccidentViewAll,AccidentEditAll,PerformanceViewAll,PerformanceEditAll";
                        break;
                    case "DashboardEHSPerformance":
                        rule_allow = "DashboardEHSPerformance";
                        break;
                    case "DashboardServiceReport":
                        rule_allow = "DashboardServiceReport";
                        break;
                    default:
                        rule_allow = "";
                        break;
                }
                PersonData personData = SecurityHelper.GetPersonData(context);
                bool allowAll = false;
                string[] paramAllow = rule_allow.Split(",");
                foreach (string item in paramAllow)
                {
                    if (SecurityHelper.HasRule(context, item))
                    {
                        allowAll = true;
                        break;
                    }
                }
                //if (SecurityHelper.HasRule(context, rule_view_all) || SecurityHelper.HasRule(context, rule_edit_all))
                if (allowAll)
                {
                    sql = "select " + sql_item +
                    " from ref_personal_sub_area as lk_psa_id ";
                    sql += whereCond;
                }
                else
                {
                    if (personData.id != null && personData.id != "")
                    {
                        whereCond += " AND (lk_mp_id.id = " + personData.id + "  or lk_ehs_area_id.head_id= " + personData.id + " or lk_psa_id.head_id= " + personData.id + " ) ";
                    }
                    sql = "select distinct " + sql_item +
                    " from ref_personal_sub_area as lk_psa_id " +
                    " left outer join ref_personal_area as lk_pa_id on lk_psa_id.pa_id = lk_pa_id.id " +
                    " left outer join ref_business_area as lk_ba_id on lk_psa_id.ba_id = lk_ba_id.id " +
                    " left outer join ref_ehs_area as lk_ehs_area_id on lk_psa_id.ehs_area_id = lk_ehs_area_id.id " +
                    " left outer join ta_mp as lk_mp_id on lk_psa_id.id = lk_mp_id.personal_sub_area_id ";
                    sql += whereCond;
                    
                }
            }
            else {
                sql = "select " + sql_item +
                    " from ref_personal_sub_area as lk_psa_id ";
                sql += whereCond ;
            }

            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }

        public static DataTable GetListPSAByUser(HttpContext context, string obj_data)
        {
            string sql = "";
            string whereCond = " WHERE 1=1 and a.status_id=1";
            if (obj_data != null && obj_data != "")
            {
                //string rule_view_all = "";
                //string rule_edit_all = "";
                //switch (obj_data)
                //{
                //    case "Accident":
                //        rule_view_all = "AccidentViewAll";
                //        rule_edit_all = "AccidentEditAll";
                //        break;
                //    case "Audit":
                //        rule_view_all = "AuditViewAll";
                //        rule_edit_all = "AuditEditAll";
                //        break;
                //    case "Campaign":
                //        rule_view_all = "CampaignViewAll";
                //        rule_edit_all = "CampaignEditAll";
                //        break;
                //    case "Document":
                //        rule_view_all = "DocumentViewAll";
                //        rule_edit_all = "DocumentEditAll";
                //        break;
                //    case "Emergency":
                //        rule_view_all = "EmergencyViewAll";
                //        rule_edit_all = "EmergencyEditAll";
                //        break;
                //    case "Environment":
                //        rule_view_all = "EnvironmentViewAll";
                //        rule_edit_all = "EnvironmentEditAll";
                //        break;
                //    case "Fasilitas":
                //        rule_view_all = "FasilitasViewAll";
                //        rule_edit_all = "FasilitasEditAll";
                //        break;
                //    case "FreeManHours":
                //        rule_view_all = "FreeManHoursViewAll";
                //        rule_edit_all = "FreeManHoursEditAll";
                //        break;
                //    case "General":
                //        rule_view_all = "GeneralViewAll";
                //        rule_edit_all = "GeneralEditAll";
                //        break;
                //    case "Health":
                //        rule_view_all = "HealthViewAll";
                //        rule_edit_all = "HealthEditAll";
                //        break;
                //    case "Legal":
                //        rule_view_all = "LegalViewAll";
                //        rule_edit_all = "LegalEditAll";
                //        break;
                //    case "LiteralData":
                //        rule_view_all = "LiteralDataViewAll";
                //        rule_edit_all = "LiteralDataEditAll";
                //        break;
                //    case "ManPower":
                //        rule_view_all = "ManPowerViewAll";
                //        rule_edit_all = "ManPowerEditAll";
                //        break;
                //    case "Organization":
                //        rule_view_all = "OrganizationViewAll";
                //        rule_edit_all = "OrganizationEditAll";
                //        break;
                //    case "Performance":
                //        rule_view_all = "PerformanceViewAll";
                //        rule_edit_all = "PerformanceEditAll";
                //        break;
                //    case "Person":
                //        rule_view_all = "PersonViewAll";
                //        rule_edit_all = "PersonEditAll";
                //        break;
                //    case "Training":
                //        rule_view_all = "TrainingViewAll";
                //        rule_edit_all = "TrainingEditAll";
                //        break;
                //    case "Compliance":
                //        rule_view_all = "ComplianceViewAll";
                //        rule_edit_all = "ComplianceEditAll";
                //        break;
                //    case "Condition":
                //        rule_view_all = "ConditionViewAll";
                //        rule_edit_all = "ConditionEditAll";
                //        break;
                //    case "Profile":
                //        rule_view_all = "ProfileViewAll";
                //        rule_edit_all = "ProfileEditAll";
                //        break;
                //    case "DashboardEHSPerformance":
                //        rule_view_all = "DashboardEHSPerformance";
                //        rule_edit_all = "DashboardEHSPerformance";
                //        break;
                //    case "DashboardServiceReport":
                //        rule_view_all = "DashboardServiceReport";
                //        rule_edit_all = "DashboardServiceReport";
                //        break;
                //    default:
                //        rule_view_all = "";
                //        rule_edit_all = "";
                //        break;

                //}
                string rule_allow = "";
                switch (obj_data)
                {
                    case "Accident":
                        rule_allow = "AccidentViewAll,AccidentEditAll";
                        break;
                    case "Audit":
                        rule_allow = "AuditViewAll,AuditEditAll";
                        break;
                    case "Campaign":
                        rule_allow = "CampaignViewAll,CampaignEditAll";
                        break;
                    case "Document":
                        rule_allow = "DocumentViewAll,DocumentEditAll";
                        break;
                    case "Emergency":
                        rule_allow = "EmergencyViewAll,EmergencyEditAll";
                        break;
                    case "Environment":
                        rule_allow = "EnvironmentViewAll,EnvironmentEditAll";
                        break;
                    case "Fasilitas":
                        rule_allow = "FasilitasViewAll,FasilitasEditAll";
                        break;
                    case "FreeManHours":
                        rule_allow = "FreeManHoursViewAll,FreeManHoursEditAll";
                        break;
                    case "General":
                        rule_allow = "GeneralViewAll,GeneralEditAll";
                        break;
                    case "Health":
                        rule_allow = "HealthViewAll,HealthEditAll";
                        break;
                    case "Legal":
                        rule_allow = "LegalViewAll,LegalEditAll";
                        break;
                    case "LiteralData":
                        rule_allow = "LiteralDataViewAll,LiteralDataEditAll";
                        break;
                    case "ManPower":
                        rule_allow = "ManPowerViewAll,ManPowerEditAll";
                        break;
                    case "Organization":
                        rule_allow = "OrganizationViewAll,OrganizationEditAll";
                        break;
                    case "Performance":
                        rule_allow = "PerformanceViewAll,PerformanceEditAll";
                        break;
                    case "Person":
                        rule_allow = "PersonViewAll,PersonEditAll";
                        break;
                    case "Training":
                        rule_allow = "TrainingViewAll,TrainingEditAll";
                        break;
                    case "Compliance":
                        rule_allow = "FasilitasViewAll,FasilitasEditAll,LegalViewAll,LegalEditAll,PengukuranViewAll,PengukuranEditAll,EnvironmentViewAll,EnvironmentEditAll";
                        break;
                    case "Condition":
                        rule_allow = "EmergencyViewAll,EmergencyEditAll,AuditViewAll,AuditEditAll,CampaignViewAll,CampaignEditAll";
                        break;
                    case "Profile":
                        rule_allow = "ManPowerViewAll,ManPowerEditAll,TrainingViewAll,TrainingEditAll,FreeManHoursViewAll,FreeManHoursEditAll,AccidentViewAll,AccidentEditAll,PerformanceViewAll,PerformanceEditAll";
                        break;
                    case "DashboardEHSPerformance":
                        rule_allow = "DashboardEHSPerformance";
                        break;
                    case "DashboardServiceReport":
                        rule_allow = "DashboardServiceReport";
                        break;
                    default:
                        rule_allow = "";
                        break;
                }
                bool allowAll = false;
                string[] paramAllow = rule_allow.Split(",");
                foreach (string item in paramAllow)
                {
                    if (SecurityHelper.HasRule(context, item))
                    {
                        allowAll = true;
                        break;
                    }
                }
                PersonData personData = SecurityHelper.GetPersonData(context);
                //if (SecurityHelper.HasRule(context, rule_view_all) || SecurityHelper.HasRule(context, rule_edit_all))
                if(allowAll)
                {
                    sql = "select  " +
                    "a.ehs_area_id " +
                    ",b.kode as ehs_area_kode " +
                    ",b.nama as ehs_area_nama " +
                    ",a.ba_id " +
                    ",c.kode as ba_kode " +
                    ",c.nama as ba_nama " +
                    ",a.pa_id " +
                    ",d.kode as pa_kode " +
                    ",d.nama as pa_nama " +
                    ",a.id as psa_id " +
                    ",a.kode as psa_kode " +
                    ",b.nama as psa_nama " +
                    "from ref_personal_sub_area as a " +
                    "left outer join ref_ehs_area as b on a.ehs_area_id = b.id " +
                    "left outer join ref_business_area as c on a.ba_id = c.id " +
                    "left outer join ref_personal_area as d on a.pa_id = d.id  ";
                    sql += whereCond;

                }
                else
                {
                    if (personData.id != null && personData.id != "")
                    {
                        whereCond += " AND (mp.id = " + personData.id + "  or b.head_id= " + personData.id + " or a.head_id= " + personData.id + " ) ";
                    }
                    sql = "select  distinct " +
                    "a.ehs_area_id " +
                    ",b.kode as ehs_area_kode " +
                    ",b.nama as ehs_area_nama " +
                    ",a.ba_id " +
                    ",c.kode as ba_kode " +
                    ",c.nama as ba_nama " +
                    ",a.pa_id " +
                    ",d.kode as pa_kode " +
                    ",d.nama as pa_nama " +
                    ",a.id as psa_id " +
                    ",a.kode as psa_kode " +
                    ",b.nama as psa_nama " +
                    " from ref_personal_sub_area as a " +
                    " left outer join ref_ehs_area as b on a.ehs_area_id = b.id " +
                    " left outer join ref_business_area as c on a.ba_id = c.id " +
                    " left outer join ref_personal_area as d on a.pa_id = d.id  " +
                    " left outer join ta_mp as mp on a.id = mp.personal_sub_area_id ";
                    sql += whereCond;

                }
            }
            else
            {
                sql = "select  " +
                    "a.ehs_area_id " +
                    ",b.kode as ehs_area_kode " +
                    ",b.nama as ehs_area_nama " +
                    ",a.ba_id " +
                    ",c.kode as ba_kode " +
                    ",c.nama as ba_nama " +
                    ",a.pa_id " +
                    ",d.kode as pa_kode " +
                    ",d.nama as pa_nama " +
                    ",a.id as psa_id " +
                    ",a.kode as psa_kode " +
                    ",b.nama as psa_nama " +
                    "from ref_personal_sub_area as a " +
                    "left outer join ref_ehs_area as b on a.ehs_area_id = b.id " +
                    "left outer join ref_business_area as c on a.ba_id = c.id " +
                    "left outer join ref_personal_area as d on a.pa_id = d.id  ";
                sql += whereCond;
            }
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }

    }
}
