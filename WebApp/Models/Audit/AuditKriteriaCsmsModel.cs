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
    public class AuditKriteriaCsmsModel
    {
        public static string _table_name = "ta_audit_kriteria_csms";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 0;
        public static string[] _unKey = {  };
        public static string _columnOrder = "ta_audit_kriteria_csms.id DESC";
        public static string _displayColumn = "id";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="csms_kategori_id"
                ,field_alias = "csms_kategori_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_csms_kategori_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.csms_kategori_id = lk_csms_kategori_id.id AND lk_csms_kategori_id.cat_id=42"
                ,select_column = "lk_csms_kategori_id.nama as csms_kategori_id_text"
            },
            new RelationTable{
                field_name="csms_tahapan_id"
                ,field_alias = "csms_tahapan_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_csms_tahapan_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.csms_tahapan_id = lk_csms_tahapan_id.id AND lk_csms_tahapan_id.cat_id=43"
                ,select_column = "lk_csms_tahapan_id.nama as csms_tahapan_id_text"
            },
            new RelationTable{
                field_name="csms_resiko_id"
                ,field_alias = "csms_resiko_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_csms_resiko_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.csms_resiko_id = lk_csms_resiko_id.id AND lk_csms_resiko_id.cat_id=41"
                ,select_column = "lk_csms_resiko_id.nama as csms_resiko_id_text"
            },
            new RelationTable{
                field_name="green_strategy_penuh"
                ,field_alias = "green_strategy_penuh_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_green_strategy_penuh"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.green_strategy_penuh = lk_green_strategy_penuh.id AND lk_green_strategy_penuh.cat_id=49"
                ,select_column = "lk_green_strategy_penuh.nama as green_strategy_penuh_text"
            },
            new RelationTable{
                field_name="green_process_penuh"
                ,field_alias = "green_process_penuh_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_green_process_penuh"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.green_process_penuh = lk_green_process_penuh.id AND lk_green_process_penuh.cat_id=49"
                ,select_column = "lk_green_process_penuh.nama as green_process_penuh_text"
            },
            new RelationTable{
                field_name="green_product_penuh"
                ,field_alias = "green_product_penuh_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_green_product_penuh"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.green_product_penuh = lk_green_product_penuh.id AND lk_green_product_penuh.cat_id=49"
                ,select_column = "lk_green_product_penuh.nama as green_product_penuh_text"
            },
            new RelationTable{
                field_name="green_employee_penuh"
                ,field_alias = "green_employee_penuh_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_green_employee_penuh"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.green_employee_penuh = lk_green_employee_penuh.id AND lk_green_employee_penuh.cat_id=49"
                ,select_column = "lk_green_employee_penuh.nama as green_employee_penuh_text"
            },
            new RelationTable{
                field_name="legal_penuh"
                ,field_alias = "legal_penuh_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_legal_penuh"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.legal_penuh = lk_legal_penuh.id AND lk_legal_penuh.cat_id=49"
                ,select_column = "lk_legal_penuh.nama as legal_penuh_text"
            },
            new RelationTable{
                field_name="nilai_bendera_id"
                ,field_alias = "nilai_bendera_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_nilai_bendera_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_csms.nilai_bendera_id = lk_nilai_bendera_id.id AND lk_nilai_bendera_id.cat_id=165"
                ,select_column = "lk_nilai_bendera_id.nama as nilai_bendera_id_text"
            }            
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string audit_id { get; set; }
            public string kriteria_audit_id { get; set; }
            public string csms_kategori_id { get; set; }
            public string csms_kategori_id_text { get; set; }
            public string csms_tahapan_id { get; set; }
            public string csms_tahapan_id_text { get; set; }
            public string nama_vendor { get; set; }
            public string csms_resiko_id { get; set; }
            public string csms_resiko_id_text { get; set; }
            public string green_strategy_bobot { get; set; }
            public string green_process_bobot { get; set; }
            public string green_product_bobot { get; set; }
            public string green_employee_bobot { get; set; }
            public string legal_bobot { get; set; }
            public string green_strategy_actual { get; set; }
            public string green_process_actual { get; set; }
            public string green_product_actual { get; set; }
            public string green_employee_actual { get; set; }
            public string legal_actual { get; set; }
            public string green_strategy_penuh { get; set; }
            public string green_strategy_penuh_text { get; set; }
            public string green_process_penuh { get; set; }
            public string green_process_penuh_text { get; set; }
            public string green_product_penuh { get; set; }
            public string green_product_penuh_text { get; set; }
            public string green_employee_penuh { get; set; }
            public string green_employee_penuh_text { get; set; }
            public string legal_penuh { get; set; }
            public string legal_penuh_text { get; set; }
            public string presentasi_pencapaian { get; set; }
            public string nilai_bendera_id { get; set; }
            public string nilai_bendera_id_text { get; set; }
            public string lampiran_file_path { get; set; }
            public string lampiran_file_path_init { get; set; }
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
                    name= "audit_id",
                    field= _table_name+".audit_id",
                    field_order= _table_name+".audit_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"audit_id"),
                    type= "number",
                    select= true,
                    display= true,
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
                    name= "kriteria_audit_id",
                    field= _table_name+".kriteria_audit_id",
                    field_order= _table_name+".kriteria_audit_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"kriteria_audit_id"),
                    type= "number",
                    select= true,
                    display= true,
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
                    name= "csms_kategori_id",
                    field= _table_name+".csms_kategori_id",
                    field_order= _table_name+".csms_kategori_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"csms_kategori_id"),
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
                    name= "csms_kategori_id_text",
                    field= "lk_csms_kategori_id.nama",
                    field_order= "lk_csms_kategori_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_csms_kategori_id",
                    title= @ResxHelper.GetValue(_table_name,"csms_kategori_id"),
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
                    name= "csms_tahapan_id",
                    field= _table_name+".csms_tahapan_id",
                    field_order= _table_name+".csms_tahapan_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"csms_tahapan_id"),
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
                    name= "csms_tahapan_id_text",
                    field= "lk_csms_tahapan_id.nama",
                    field_order= "lk_csms_tahapan_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_csms_tahapan_id",
                    title= @ResxHelper.GetValue(_table_name,"csms_tahapan_id"),
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
                    name= "nama_vendor",
                    field= _table_name+".nama_vendor",
                    field_order= _table_name+".nama_vendor",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"nama_vendor"),
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
                    name= "csms_resiko_id",
                    field= _table_name+".csms_resiko_id",
                    field_order= _table_name+".csms_resiko_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"csms_resiko_id"),
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
                    name= "csms_resiko_id_text",
                    field= "lk_csms_resiko_id.nama",
                    field_order= "lk_csms_resiko_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_csms_resiko_id",
                    title= @ResxHelper.GetValue(_table_name,"csms_resiko_id"),
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
                    name= "green_strategy_bobot",
                    field= _table_name+".green_strategy_bobot",
                    field_order= _table_name+".green_strategy_bobot",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_strategy_bobot"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_process_bobot",
                    field= _table_name+".green_process_bobot",
                    field_order= _table_name+".green_process_bobot",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_process_bobot"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_product_bobot",
                    field= _table_name+".green_product_bobot",
                    field_order= _table_name+".green_product_bobot",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_product_bobot"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_employee_bobot",
                    field= _table_name+".green_employee_bobot",
                    field_order= _table_name+".green_employee_bobot",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_employee_bobot"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "legal_bobot",
                    field= _table_name+".legal_bobot",
                    field_order= _table_name+".legal_bobot",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"legal_bobot"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_strategy_actual",
                    field= _table_name+".green_strategy_actual",
                    field_order= _table_name+".green_strategy_actual",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_strategy_actual"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_process_actual",
                    field= _table_name+".green_process_actual",
                    field_order= _table_name+".green_process_actual",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_process_actual"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_product_actual",
                    field= _table_name+".green_product_actual",
                    field_order= _table_name+".green_product_actual",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_product_actual"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_employee_actual",
                    field= _table_name+".green_employee_actual",
                    field_order= _table_name+".green_employee_actual",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_employee_actual"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "legal_actual",
                    field= _table_name+".legal_actual",
                    field_order= _table_name+".legal_actual",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"legal_actual"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
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
                    name= "green_strategy_penuh",
                    field= _table_name+".green_strategy_penuh",
                    field_order= _table_name+".green_strategy_penuh",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_strategy_penuh"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "green_strategy_penuh_text",
                    field= "lk_green_strategy_penuh.nama",
                    field_order= "lk_green_strategy_penuh.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_strategy_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_strategy_penuh"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= green_strategy_penuh_bg_color#; \" }",
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
                    name= "green_strategy_penuh_bg_color",
                    field= "lk_green_strategy_penuh.bg_color",
                    field_order= "lk_green_strategy_penuh.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_strategy_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_strategy_penuh"),
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
                    name= "green_process_penuh",
                    field= _table_name+".green_process_penuh",
                    field_order= _table_name+".green_process_penuh",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_process_penuh"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "green_process_penuh_text",
                    field= "lk_green_process_penuh.nama",
                    field_order= "lk_green_process_penuh.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_process_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_process_penuh"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= green_process_penuh_bg_color#; \" }",
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
                    name= "green_process_penuh_bg_color",
                    field= "lk_green_process_penuh.bg_color",
                    field_order= "lk_green_process_penuh.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_process_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_process_penuh"),
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
                    name= "green_product_penuh",
                    field= _table_name+".green_product_penuh",
                    field_order= _table_name+".green_product_penuh",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_product_penuh"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "green_product_penuh_text",
                    field= "lk_green_product_penuh.nama",
                    field_order= "lk_green_product_penuh.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_product_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_product_penuh"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= green_product_penuh_bg_color#; \" }",
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
                    name= "green_product_penuh_bg_color",
                    field= "lk_green_product_penuh.bg_color",
                    field_order= "lk_green_product_penuh.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_product_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_product_penuh"),
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
                    name= "green_employee_penuh",
                    field= _table_name+".green_employee_penuh",
                    field_order= _table_name+".green_employee_penuh",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"green_employee_penuh"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "green_employee_penuh_text",
                    field= "lk_green_employee_penuh.nama",
                    field_order= "lk_green_employee_penuh.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_employee_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_employee_penuh"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= green_employee_penuh_bg_color#; \" }",
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
                    name= "green_employee_penuh_bg_color",
                    field= "lk_green_employee_penuh.bg_color",
                    field_order= "lk_green_employee_penuh.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_green_employee_penuh",
                    title= @ResxHelper.GetValue(_table_name,"green_employee_penuh"),
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
                    name= "legal_penuh",
                    field= _table_name+".legal_penuh",
                    field_order= _table_name+".legal_penuh",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"legal_penuh"),
                    type= "string",
                    select= true,
                    display= false,
                    hidden= true,
                    width= "",
                    format= "{0:N2}",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
                    filterable= true,
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "legal_penuh_text",
                    field= "lk_legal_penuh.nama",
                    field_order= "lk_legal_penuh.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_legal_penuh",
                    title= @ResxHelper.GetValue(_table_name,"legal_penuh"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= legal_penuh_bg_color#; \" }",
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
                    name= "legal_penuh_bg_color",
                    field= "lk_legal_penuh.bg_color",
                    field_order= "lk_legal_penuh.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_legal_penuh",
                    title= @ResxHelper.GetValue(_table_name,"legal_penuh"),
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
                    name= "presentasi_pencapaian",
                    field= _table_name+".presentasi_pencapaian",
                    field_order= _table_name+".presentasi_pencapaian",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"presentasi_pencapaian"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "{0:N2}",
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
                    name= "nilai_bendera_id",
                    field= _table_name+".nilai_bendera_id",
                    field_order= _table_name+".nilai_bendera_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"nilai_bendera_id"),
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
                    name= "nilai_bendera_id_text",
                    field= "lk_nilai_bendera_id.nama",
                    field_order= "lk_nilai_bendera_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_nilai_bendera_id",
                    title= @ResxHelper.GetValue(_table_name,"nilai_bendera_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= nilai_bendera_id_bg_color#; \" }",
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
                    name= "nilai_bendera_id_bg_color",
                    field= "lk_nilai_bendera_id.bg_color",
                    field_order= "lk_nilai_bendera_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_nilai_bendera_id",
                    title= @ResxHelper.GetValue(_table_name,"nilai_bendera_id"),
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
                    name= "lampiran_file_path",
                    field= _table_name+".lampiran_file_path",
                    field_order= _table_name+".lampiran_file_path",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"lampiran_file_path"),
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
                    data["lampiran_file_path"] = FileHelper.CopyToBase(_hostingEnvironment, context, _table_name, "lampiran_file_path", PkValue, data["lampiran_file_path"].ToString());
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
                    data["lampiran_file_path"] = FileHelper.CopyToBase(_hostingEnvironment, context, _table_name, "lampiran_file_path", PkValue, data["lampiran_file_path"].ToString());
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
                FileHelper.DeleteFromBase(_hostingEnvironment, context, _table_name, PkValue);
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


        public static DataTable GetViewDataByColumn(OrderedDictionary dataValue)
        {
            DataTable data = new DataTable();
            bool valid = true;
            foreach (DictionaryEntry entry in dataValue)
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
                foreach (DictionaryEntry entry in dataValue)
                {
                    sql += " AND " + _table_name + "." + entry.Key + "=@" + entry.Key + " ";
                    parameter[entry.Key] = entry.Value;
                }
                data = SqlHelper.GetDataTable(sql, parameter);
            }
            return data;
        }
    }
}
