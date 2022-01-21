using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using WebApp.Models;

namespace WebApp.Models
{
    public class EHSPerformanceModel
    {
        public static string _table_name = "ta_ehs_performance";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 0;
        public static string[] _unKey = { };
        public static string _columnOrder = "ta_ehs_performance.id DESC";
        public static string _displayColumn = "id";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="kategori_filter"
                ,field_alias = "kategori_filter_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_kategori_filter"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.kategori_filter = lk_kategori_filter.id AND lk_kategori_filter.cat_id=167"
                ,select_column = "lk_kategori_filter.nama as kategori_filter_text"
            },
            new RelationTable{
                field_name="periode0"
                ,field_alias = "periode0_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_periode0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.periode0 = lk_periode0.id AND lk_periode0.cat_id=166"
                ,select_column = "lk_periode0.nama as periode0_text"
            },
            new RelationTable{
                field_name="ehs_area_id0"
                ,field_alias = "ehs_area_id0_text"
                ,table_name = "ref_ehs_area"
                ,table_alias = "lk_ehs_area_id0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ehs_area_id0 = lk_ehs_area_id0.id"
                ,select_column = "lk_ehs_area_id0.kode as ehs_area_id0_text"
            },
            new RelationTable{
                field_name="ba_id0"
                ,field_alias = "ba_id0_text"
                ,table_name = "ref_business_area"
                ,table_alias = "lk_ba_id0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ba_id0 = lk_ba_id0.id"
                ,select_column = "lk_ba_id0.kode as ba_id0_text"
            },
            new RelationTable{
                field_name="pa_id0"
                ,field_alias = "pa_id0_text"
                ,table_name = "ref_personal_area"
                ,table_alias = "lk_pa_id0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.pa_id0 = lk_pa_id0.id"
                ,select_column = "lk_pa_id0.kode as pa_id0_text"
            },
            new RelationTable{
                field_name="psa_id0"
                ,field_alias = "psa_id0_text"
                ,table_name = "ref_personal_sub_area"
                ,table_alias = "lk_psa_id0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.psa_id0 = lk_psa_id0.id"
                ,select_column = "lk_psa_id0.kode as psa_id0_text"
            },
            new RelationTable{
                field_name="tahun0"
                ,field_alias = "tahun0_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_tahun0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.tahun0 = lk_tahun0.id AND lk_tahun0.cat_id=151"
                ,select_column = "lk_tahun0.nama as tahun0_text"
            },
            new RelationTable{
                field_name="bulan0"
                ,field_alias = "bulan0_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_bulan0"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.bulan0 = lk_bulan0.id AND lk_bulan0.cat_id=150"
                ,select_column = "lk_bulan0.nama as bulan0_text"
            },
            new RelationTable{
                field_name="ehs_area_id1"
                ,field_alias = "ehs_area_id1_text"
                ,table_name = "ref_ehs_area"
                ,table_alias = "lk_ehs_area_id1"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ehs_area_id1 = lk_ehs_area_id1.id"
                ,select_column = "lk_ehs_area_id1.kode as ehs_area_id1_text"
            },
            new RelationTable{
                field_name="ba_id1"
                ,field_alias = "ba_id1_text"
                ,table_name = "ref_business_area"
                ,table_alias = "lk_ba_id1"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ba_id1 = lk_ba_id1.id"
                ,select_column = "lk_ba_id1.kode as ba_id1_text"
            },
            new RelationTable{
                field_name="pa_id1"
                ,field_alias = "pa_id1_text"
                ,table_name = "ref_personal_area"
                ,table_alias = "lk_pa_id1"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.pa_id1 = lk_pa_id1.id"
                ,select_column = "lk_pa_id1.kode as pa_id1_text"
            },
            new RelationTable{
                field_name="psa_id1"
                ,field_alias = "psa_id1_text"
                ,table_name = "ref_personal_sub_area"
                ,table_alias = "lk_psa_id1"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.psa_id1 = lk_psa_id1.id"
                ,select_column = "lk_psa_id1.kode as psa_id1_text"
            },
            new RelationTable{
                field_name="tahun1"
                ,field_alias = "tahun1_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_tahun1"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.tahun1 = lk_tahun1.id AND lk_tahun1.cat_id=151"
                ,select_column = "lk_tahun1.nama as tahun1_text"
            },
            new RelationTable{
                field_name="bulan1"
                ,field_alias = "bulan1_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_bulan1"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.bulan1 = lk_bulan1.id AND lk_bulan1.cat_id=150"
                ,select_column = "lk_bulan1.nama as bulan1_text"
            },
            new RelationTable{
                field_name="ehs_area_id2"
                ,field_alias = "ehs_area_id2_text"
                ,table_name = "ref_ehs_area"
                ,table_alias = "lk_ehs_area_id2"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ehs_area_id2 = lk_ehs_area_id2.id"
                ,select_column = "lk_ehs_area_id2.kode as ehs_area_id2_text"
            },
            new RelationTable{
                field_name="ba_id2"
                ,field_alias = "ba_id2_text"
                ,table_name = "ref_business_area"
                ,table_alias = "lk_ba_id2"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ba_id2 = lk_ba_id2.id"
                ,select_column = "lk_ba_id2.kode as ba_id2_text"
            },
            new RelationTable{
                field_name="pa_id2"
                ,field_alias = "pa_id2_text"
                ,table_name = "ref_personal_area"
                ,table_alias = "lk_pa_id2"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.pa_id2 = lk_pa_id2.id"
                ,select_column = "lk_pa_id2.kode as pa_id2_text"
            },
            new RelationTable{
                field_name="psa_id2"
                ,field_alias = "psa_id2_text"
                ,table_name = "ref_personal_sub_area"
                ,table_alias = "lk_psa_id2"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.psa_id2 = lk_psa_id2.id"
                ,select_column = "lk_psa_id2.kode as psa_id2_text"
            },
            new RelationTable{
                field_name="tahun2"
                ,field_alias = "tahun2_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_tahun2"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.tahun2 = lk_tahun2.id AND lk_tahun2.cat_id=151"
                ,select_column = "lk_tahun2.nama as tahun2_text"
            },
            new RelationTable{
                field_name="bulan2"
                ,field_alias = "bulan2_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_bulan2"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.bulan2 = lk_bulan2.id AND lk_bulan2.cat_id=150"
                ,select_column = "lk_bulan2.nama as bulan2_text"
            },
            new RelationTable{
                field_name="ehs_area_id3"
                ,field_alias = "ehs_area_id3_text"
                ,table_name = "ref_ehs_area"
                ,table_alias = "lk_ehs_area_id3"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ehs_area_id3 = lk_ehs_area_id3.id"
                ,select_column = "lk_ehs_area_id3.kode as ehs_area_id3_text"
            },
            new RelationTable{
                field_name="ba_id3"
                ,field_alias = "ba_id3_text"
                ,table_name = "ref_business_area"
                ,table_alias = "lk_ba_id3"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.ba_id3 = lk_ba_id3.id"
                ,select_column = "lk_ba_id3.kode as ba_id3_text"
            },
            new RelationTable{
                field_name="pa_id3"
                ,field_alias = "pa_id3_text"
                ,table_name = "ref_personal_area"
                ,table_alias = "lk_pa_id3"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.pa_id3 = lk_pa_id3.id"
                ,select_column = "lk_pa_id3.kode as pa_id3_text"
            },
            new RelationTable{
                field_name="psa_id3"
                ,field_alias = "psa_id3_text"
                ,table_name = "ref_personal_sub_area"
                ,table_alias = "lk_psa_id3"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.psa_id3 = lk_psa_id3.id"
                ,select_column = "lk_psa_id3.kode as psa_id3_text"
            },
            new RelationTable{
                field_name="tahun3"
                ,field_alias = "tahun3_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_tahun3"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.tahun3 = lk_tahun3.id AND lk_tahun3.cat_id=151"
                ,select_column = "lk_tahun3.nama as tahun3_text"
            },
            new RelationTable{
                field_name="bulan3"
                ,field_alias = "bulan3_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_bulan3"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_ehs_performance.bulan3 = lk_bulan3.id AND lk_bulan3.cat_id=150"
                ,select_column = "lk_bulan3.nama as bulan3_text"
            }
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string kategori_filter { get; set; }
            public string kategori_filter_text { get; set; }
            public string periode0 { get; set; }
            public string periode0_text { get; set; }
            public string ehs_area_id0 { get; set; }
            public string ehs_area_id0_text { get; set; }
            public string ba_id0 { get; set; }
            public string ba_id0_text { get; set; }
            public string pa_id0 { get; set; }
            public string pa_id0_text { get; set; }
            public string psa_id0 { get; set; }
            public string psa_id0_text { get; set; }
            public string tahun0 { get; set; }
            public string tahun0_text { get; set; }
            public string bulan0 { get; set; }
            public string bulan0_text { get; set; }
            public string ehs_area_id1 { get; set; }
            public string ehs_area_id1_text { get; set; }
            public string ba_id1 { get; set; }
            public string ba_id1_text { get; set; }
            public string pa_id1 { get; set; }
            public string pa_id1_text { get; set; }
            public string psa_id1 { get; set; }
            public string psa_id1_text { get; set; }
            public string tahun1 { get; set; }
            public string tahun1_text { get; set; }
            public string bulan1 { get; set; }
            public string bulan1_text { get; set; }
            public string ehs_area_id2 { get; set; }
            public string ehs_area_id2_text { get; set; }
            public string ba_id2 { get; set; }
            public string ba_id2_text { get; set; }
            public string pa_id2 { get; set; }
            public string pa_id2_text { get; set; }
            public string psa_id2 { get; set; }
            public string psa_id2_text { get; set; }
            public string tahun2 { get; set; }
            public string tahun2_text { get; set; }
            public string bulan2 { get; set; }
            public string bulan2_text { get; set; }
            public string ehs_area_id3 { get; set; }
            public string ehs_area_id3_text { get; set; }
            public string ba_id3 { get; set; }
            public string ba_id3_text { get; set; }
            public string pa_id3 { get; set; }
            public string pa_id3_text { get; set; }
            public string psa_id3 { get; set; }
            public string psa_id3_text { get; set; }
            public string tahun3 { get; set; }
            public string tahun3_text { get; set; }
            public string bulan3 { get; set; }
            public string bulan3_text { get; set; }
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
                    hidden= true,
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
                    name= "kategori_filter",
                    field= _table_name+".kategori_filter",
                    field_order= _table_name+".kategori_filter",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"kategori_filter"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "kategori_filter_text",
                    field= "lk_kategori_filter.nama",
                    field_order= "lk_kategori_filter.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_kategori_filter",
                    title= @ResxHelper.GetValue(_table_name,"kategori_filter"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "periode0",
                    field= _table_name+".periode0",
                    field_order= _table_name+".periode0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"periode0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "periode0_text",
                    field= "lk_periode0.nama",
                    field_order= "lk_periode0.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_periode0",
                    title= @ResxHelper.GetValue(_table_name,"periode0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ehs_area_id0",
                    field= _table_name+".ehs_area_id0",
                    field_order= _table_name+".ehs_area_id0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ehs_area_id0_text",
                    field= "lk_ehs_area_id0.kode",
                    field_order= "lk_ehs_area_id0.kode",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id0",
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ba_id0",
                    field= _table_name+".ba_id0",
                    field_order= _table_name+".ba_id0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ba_id0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ba_id0_text",
                    field= "lk_ba_id0.kode",
                    field_order= "lk_ba_id0.kode",
                    table_name = "ref_business_area",
                    table_alias = "lk_ba_id0",
                    title= @ResxHelper.GetValue(_table_name,"ba_id0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "pa_id0",
                    field= _table_name+".pa_id0",
                    field_order= _table_name+".pa_id0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pa_id0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "pa_id0_text",
                    field= "lk_pa_id0.kode",
                    field_order= "lk_pa_id0.kode",
                    table_name = "ref_personal_area",
                    table_alias = "lk_pa_id0",
                    title= @ResxHelper.GetValue(_table_name,"pa_id0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "psa_id0",
                    field= _table_name+".psa_id0",
                    field_order= _table_name+".psa_id0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"psa_id0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "psa_id0_text",
                    field= "lk_psa_id0.kode",
                    field_order= "lk_psa_id0.kode",
                    table_name = "ref_personal_sub_area",
                    table_alias = "lk_psa_id0",
                    title= @ResxHelper.GetValue(_table_name,"psa_id0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "tahun0",
                    field= _table_name+".tahun0",
                    field_order= _table_name+".tahun0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tahun0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "tahun0_text",
                    field= "lk_tahun0.nama",
                    field_order= "lk_tahun0.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_tahun0",
                    title= @ResxHelper.GetValue(_table_name,"tahun0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "bulan0",
                    field= _table_name+".bulan0",
                    field_order= _table_name+".bulan0",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"bulan0"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "bulan0_text",
                    field= "lk_bulan0.nama",
                    field_order= "lk_bulan0.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_bulan0",
                    title= @ResxHelper.GetValue(_table_name,"bulan0"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ehs_area_id1",
                    field= _table_name+".ehs_area_id1",
                    field_order= _table_name+".ehs_area_id1",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id1"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ehs_area_id1_text",
                    field= "lk_ehs_area_id1.kode",
                    field_order= "lk_ehs_area_id1.kode",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id1",
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id1"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ba_id1",
                    field= _table_name+".ba_id1",
                    field_order= _table_name+".ba_id1",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ba_id1"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ba_id1_text",
                    field= "lk_ba_id1.kode",
                    field_order= "lk_ba_id1.kode",
                    table_name = "ref_business_area",
                    table_alias = "lk_ba_id1",
                    title= @ResxHelper.GetValue(_table_name,"ba_id1"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "pa_id1",
                    field= _table_name+".pa_id1",
                    field_order= _table_name+".pa_id1",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pa_id1"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "pa_id1_text",
                    field= "lk_pa_id1.kode",
                    field_order= "lk_pa_id1.kode",
                    table_name = "ref_personal_area",
                    table_alias = "lk_pa_id1",
                    title= @ResxHelper.GetValue(_table_name,"pa_id1"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "psa_id1",
                    field= _table_name+".psa_id1",
                    field_order= _table_name+".psa_id1",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"psa_id1"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "psa_id1_text",
                    field= "lk_psa_id1.kode",
                    field_order= "lk_psa_id1.kode",
                    table_name = "ref_personal_sub_area",
                    table_alias = "lk_psa_id1",
                    title= @ResxHelper.GetValue(_table_name,"psa_id1"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "tahun1",
                    field= _table_name+".tahun1",
                    field_order= _table_name+".tahun1",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tahun1"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "tahun1_text",
                    field= "lk_tahun1.nama",
                    field_order= "lk_tahun1.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_tahun1",
                    title= @ResxHelper.GetValue(_table_name,"tahun1"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "bulan1",
                    field= _table_name+".bulan1",
                    field_order= _table_name+".bulan1",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"bulan1"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "bulan1_text",
                    field= "lk_bulan1.nama",
                    field_order= "lk_bulan1.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_bulan1",
                    title= @ResxHelper.GetValue(_table_name,"bulan1"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ehs_area_id2",
                    field= _table_name+".ehs_area_id2",
                    field_order= _table_name+".ehs_area_id2",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id2"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ehs_area_id2_text",
                    field= "lk_ehs_area_id2.kode",
                    field_order= "lk_ehs_area_id2.kode",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id2",
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id2"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ba_id2",
                    field= _table_name+".ba_id2",
                    field_order= _table_name+".ba_id2",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ba_id2"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ba_id2_text",
                    field= "lk_ba_id2.kode",
                    field_order= "lk_ba_id2.kode",
                    table_name = "ref_business_area",
                    table_alias = "lk_ba_id2",
                    title= @ResxHelper.GetValue(_table_name,"ba_id2"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "pa_id2",
                    field= _table_name+".pa_id2",
                    field_order= _table_name+".pa_id2",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pa_id2"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "pa_id2_text",
                    field= "lk_pa_id2.kode",
                    field_order= "lk_pa_id2.kode",
                    table_name = "ref_personal_area",
                    table_alias = "lk_pa_id2",
                    title= @ResxHelper.GetValue(_table_name,"pa_id2"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "psa_id2",
                    field= _table_name+".psa_id2",
                    field_order= _table_name+".psa_id2",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"psa_id2"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "psa_id2_text",
                    field= "lk_psa_id2.kode",
                    field_order= "lk_psa_id2.kode",
                    table_name = "ref_personal_sub_area",
                    table_alias = "lk_psa_id2",
                    title= @ResxHelper.GetValue(_table_name,"psa_id2"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "tahun2",
                    field= _table_name+".tahun2",
                    field_order= _table_name+".tahun2",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tahun2"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "tahun2_text",
                    field= "lk_tahun2.nama",
                    field_order= "lk_tahun2.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_tahun2",
                    title= @ResxHelper.GetValue(_table_name,"tahun2"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "bulan2",
                    field= _table_name+".bulan2",
                    field_order= _table_name+".bulan2",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"bulan2"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "bulan2_text",
                    field= "lk_bulan2.nama",
                    field_order= "lk_bulan2.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_bulan2",
                    title= @ResxHelper.GetValue(_table_name,"bulan2"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ehs_area_id3",
                    field= _table_name+".ehs_area_id3",
                    field_order= _table_name+".ehs_area_id3",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id3"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ehs_area_id3_text",
                    field= "lk_ehs_area_id3.kode",
                    field_order= "lk_ehs_area_id3.kode",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id3",
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id3"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "ba_id3",
                    field= _table_name+".ba_id3",
                    field_order= _table_name+".ba_id3",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"ba_id3"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "ba_id3_text",
                    field= "lk_ba_id3.kode",
                    field_order= "lk_ba_id3.kode",
                    table_name = "ref_business_area",
                    table_alias = "lk_ba_id3",
                    title= @ResxHelper.GetValue(_table_name,"ba_id3"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "pa_id3",
                    field= _table_name+".pa_id3",
                    field_order= _table_name+".pa_id3",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pa_id3"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "pa_id3_text",
                    field= "lk_pa_id3.kode",
                    field_order= "lk_pa_id3.kode",
                    table_name = "ref_personal_area",
                    table_alias = "lk_pa_id3",
                    title= @ResxHelper.GetValue(_table_name,"pa_id3"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "psa_id3",
                    field= _table_name+".psa_id3",
                    field_order= _table_name+".psa_id3",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"psa_id3"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "psa_id3_text",
                    field= "lk_psa_id3.kode",
                    field_order= "lk_psa_id3.kode",
                    table_name = "ref_personal_sub_area",
                    table_alias = "lk_psa_id3",
                    title= @ResxHelper.GetValue(_table_name,"psa_id3"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "tahun3",
                    field= _table_name+".tahun3",
                    field_order= _table_name+".tahun3",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tahun3"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "tahun3_text",
                    field= "lk_tahun3.nama",
                    field_order= "lk_tahun3.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_tahun3",
                    title= @ResxHelper.GetValue(_table_name,"tahun3"),
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "bulan3",
                    field= _table_name+".bulan3",
                    field_order= _table_name+".bulan3",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"bulan3"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= true,
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
                    name= "bulan3_text",
                    field= "lk_bulan3.nama",
                    field_order= "lk_bulan3.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_bulan3",
                    title= @ResxHelper.GetValue(_table_name,"bulan3"),
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
                            whereCond += " AND " + myColumn.field + " " + itemFilter.opr + " '%" + itemFilter.value + "%'";
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
                    sql_join_field += "," + tr.select_column;
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
                if (result.status == 1)
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
                            msg += ResxHelper.GetValue(_table_name, itemKey);
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
                            msg += ResxHelper.GetValue(_table_name, itemKey);

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
            string pk = "";
            foreach (string key in _pkKey)
            {
                pk = key;
            }
            string sql = "select case when max(" + pk + ") is null then 0 else max(" + pk + ") end as last_id from " + _table_name + " ";
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
            string sql = "select case when max(" + pk + ") is null then 1 else (max(" + pk + ") + 1) end as last_id from " + _table_name + " ";
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
                    } else {
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
                DataModel.DeleteCascade(context, _table_name, last_val);
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
                            item_text += " CAST(" + _table_name + "." + item + " as varchar(50)) ";
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
                            sql_item += "," + joinData.select_column.Replace("_text", "");
                        }
                        else {
                            sql_item += "," + _table_name + "." + col_name;
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
                        item_text += " CAST(" + item + " as varchar(50)) ";
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

        //public static DataSet GetValidData(
        //    string cur_year_1, 
        //    string cur_month_1, 
        //    string ehs_area_id_1, 
        //    string ba_id_1, 
        //    string pa_id_1, 
        //    string psa_id_1,
        //    string cur_year_2,
        //    string cur_month_2,
        //    string ehs_area_id_2,
        //    string ba_id_2,
        //    string pa_id_2,
        //    string psa_id_2,
        //    string cur_year_3,
        //    string cur_month_3,
        //    string ehs_area_id_3,
        //    string ba_id_3,
        //    string pa_id_3,
        //    string psa_id_3
        //)
        //{
        //    DataSet ds = new DataSet();
            
        //    if (cur_year_1 == null || cur_year_1 == "") { cur_year_1 = "NULL"; }
        //    if (cur_month_1 == null || cur_month_1 == "") { cur_month_1 = "NULL"; }
        //    if (ehs_area_id_1 == null || ehs_area_id_1 == "") { ehs_area_id_1 = "NULL"; }
        //    if (ba_id_1 == null || ba_id_1 == "") { ba_id_1 = "NULL"; }
        //    if (pa_id_1 == null || pa_id_1 == "") { pa_id_1 = "NULL"; }
        //    if (psa_id_1 == null || psa_id_1 == "") { psa_id_1 = "NULL"; }

        //    if (cur_year_2 == null || cur_year_2 == "") { cur_year_2 = "NULL"; }
        //    if (cur_month_2 == null || cur_month_2 == "") { cur_month_2 = "NULL"; }
        //    if (ehs_area_id_2 == null || ehs_area_id_2 == "") { ehs_area_id_2 = "NULL"; }
        //    if (ba_id_2 == null || ba_id_2 == "") { ba_id_2 = "NULL"; }
        //    if (pa_id_2 == null || pa_id_2 == "") { pa_id_2 = "NULL"; }
        //    if (psa_id_2 == null || psa_id_2 == "") { psa_id_2 = "NULL"; }

        //    if (cur_year_3 == null || cur_year_3 == "") { cur_year_3 = "NULL"; }
        //    if (cur_month_3 == null || cur_month_3 == "") { cur_month_3 = "NULL"; }
        //    if (ehs_area_id_3 == null || ehs_area_id_3 == "") { ehs_area_id_3 = "NULL"; }
        //    if (ba_id_3 == null || ba_id_3 == "") { ba_id_3 = "NULL"; }
        //    if (pa_id_3 == null || pa_id_3 == "") { pa_id_3 = "NULL"; }
        //    if (psa_id_3 == null || psa_id_3 == "") { psa_id_3 = "NULL"; }


        //    string sqlCommand = "EXEC dbo.spGetEHSPerformanceAll " +
        //        " @CUR_YEAR_1 = " + cur_year_1 +
        //        " ,@CUR_MONTH_1 = " + cur_month_1 +
        //        " ,@EHS_AREA_ID_1 = " + ehs_area_id_1 +
        //        " ,@BA_ID_1 = " + ba_id_1 +
        //        " ,@PA_ID_1 = " + pa_id_1 +
        //        " ,@PSA_ID_1 = " + psa_id_1 +
        //        " ,@CUR_YEAR_2 = " + cur_year_2 +
        //        " ,@CUR_MONTH_2 = " + cur_month_2 +
        //        " ,@EHS_AREA_ID_2 = " + ehs_area_id_2 +
        //        " ,@BA_ID_2 = " + ba_id_2 +
        //        " ,@PA_ID_2 = " + pa_id_2 +
        //        " ,@PSA_ID_2 = " + psa_id_2 +
        //        " ,@CUR_YEAR_3 = " + cur_year_3 +
        //        " ,@CUR_MONTH_3 = " + cur_month_3 +
        //        " ,@EHS_AREA_ID_3 = " + ehs_area_id_3 +
        //        " ,@BA_ID_3 = " + ba_id_3 +
        //        " ,@PA_ID_3 = " + pa_id_3 +
        //        " ,@PSA_ID_3 = " + psa_id_3;
        //    ds = SqlHelper.GetDataSetFromSP(sqlCommand);
        //    return ds;
        //}

        public static DataSet GetValidData(string cur_year, string cur_month, string ehs_area_id, string ba_id, string pa_id, string psa_id)
        {
            DataSet ds = new DataSet();
            if (cur_year == null || cur_year == "") { cur_year = "NULL"; }
            if (cur_month == null || cur_month == "") { cur_month = "NULL"; }
            if (ehs_area_id == null || ehs_area_id == "") { ehs_area_id = "NULL"; }
            if (ba_id == null || ba_id == "") { ba_id = "NULL"; }
            if (pa_id == null || pa_id == "") { pa_id = "NULL"; }
            if (psa_id == null || psa_id == "") { psa_id = "NULL"; }
            ds = SqlHelper.GetDataSetFromSP("EXEC dbo.spGetEHSPerformance @CUR_YEAR = " + cur_year + ", @CUR_MONTH = " + cur_month + ", @EHS_AREA_ID = " + ehs_area_id + ", @BA_ID = " + ba_id + ", @PA_ID = " + pa_id + ", @PSA_ID = " + psa_id + " ");
            return ds;
        }
    }
}
