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
    public class AuditKriteriaAgcModel
    {
        public static string _table_name = "ta_audit_kriteria_agc";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 0;
        public static string[] _unKey = {  };
        public static string _columnOrder = "ta_audit_kriteria_agc.id DESC";
        public static string _displayColumn = "id";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="status_akhir_acp_id"
                ,field_alias = "status_akhir_acp_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_akhir_acp_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.status_akhir_acp_id = lk_status_akhir_acp_id.id AND lk_status_akhir_acp_id.cat_id=49"
                ,select_column = "lk_status_akhir_acp_id.nama as status_akhir_acp_id_text"
            },
            new RelationTable{
                field_name="amdal_id"
                ,field_alias = "amdal_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_amdal_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.amdal_id = lk_amdal_id.id AND lk_amdal_id.cat_id=49"
                ,select_column = "lk_amdal_id.nama as amdal_id_text"
            },
            new RelationTable{
                field_name="pencemaran_air_id"
                ,field_alias = "pencemaran_air_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_pencemaran_air_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.pencemaran_air_id = lk_pencemaran_air_id.id AND lk_pencemaran_air_id.cat_id=49"
                ,select_column = "lk_pencemaran_air_id.nama as pencemaran_air_id_text"
            },
            new RelationTable{
                field_name="pencemaran_udara_id"
                ,field_alias = "pencemaran_udara_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_pencemaran_udara_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.pencemaran_udara_id = lk_pencemaran_udara_id.id AND lk_pencemaran_udara_id.cat_id=49"
                ,select_column = "lk_pencemaran_udara_id.nama as pencemaran_udara_id_text"
            },
            new RelationTable{
                field_name="limbah_lb3_id"
                ,field_alias = "limbah_lb3_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_limbah_lb3_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.limbah_lb3_id = lk_limbah_lb3_id.id AND lk_limbah_lb3_id.cat_id=49"
                ,select_column = "lk_limbah_lb3_id.nama as limbah_lb3_id_text"
            },
            new RelationTable{
                field_name="limbah_nonlb3_id"
                ,field_alias = "limbah_nonlb3_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_limbah_nonlb3_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.limbah_nonlb3_id = lk_limbah_nonlb3_id.id AND lk_limbah_nonlb3_id.cat_id=49"
                ,select_column = "lk_limbah_nonlb3_id.nama as limbah_nonlb3_id_text"
            },
            new RelationTable{
                field_name="comdev_id"
                ,field_alias = "comdev_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_comdev_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.comdev_id = lk_comdev_id.id AND lk_comdev_id.cat_id=49"
                ,select_column = "lk_comdev_id.nama as comdev_id_text"
            },
            new RelationTable{
                field_name="status_akhir_cpproper_id"
                ,field_alias = "status_akhir_cpproper_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_akhir_cpproper_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.status_akhir_cpproper_id = lk_status_akhir_cpproper_id.id AND lk_status_akhir_cpproper_id.cat_id=49"
                ,select_column = "lk_status_akhir_cpproper_id.nama as status_akhir_cpproper_id_text"
            },
            new RelationTable{
                field_name="sarana_k3_id"
                ,field_alias = "sarana_k3_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_sarana_k3_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.sarana_k3_id = lk_sarana_k3_id.id AND lk_sarana_k3_id.cat_id=49"
                ,select_column = "lk_sarana_k3_id.nama as sarana_k3_id_text"
            },
            new RelationTable{
                field_name="sarana_ktd_id"
                ,field_alias = "sarana_ktd_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_sarana_ktd_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.sarana_ktd_id = lk_sarana_ktd_id.id AND lk_sarana_ktd_id.cat_id=49"
                ,select_column = "lk_sarana_ktd_id.nama as sarana_ktd_id_text"
            },
            new RelationTable{
                field_name="simulasi_ktd_id"
                ,field_alias = "simulasi_ktd_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_simulasi_ktd_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.simulasi_ktd_id = lk_simulasi_ktd_id.id AND lk_simulasi_ktd_id.cat_id=49"
                ,select_column = "lk_simulasi_ktd_id.nama as simulasi_ktd_id_text"
            },
            new RelationTable{
                field_name="status_akhir_incident_rate_id"
                ,field_alias = "status_akhir_incident_rate_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_akhir_incident_rate_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.status_akhir_incident_rate_id = lk_status_akhir_incident_rate_id.id AND lk_status_akhir_incident_rate_id.cat_id=49"
                ,select_column = "lk_status_akhir_incident_rate_id.nama as status_akhir_incident_rate_id_text"
            },
            new RelationTable{
                field_name="status_akhir_cpsafety_id"
                ,field_alias = "status_akhir_cpsafety_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_akhir_cpsafety_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.status_akhir_cpsafety_id = lk_status_akhir_cpsafety_id.id AND lk_status_akhir_cpsafety_id.cat_id=49"
                ,select_column = "lk_status_akhir_cpsafety_id.nama as status_akhir_cpsafety_id_text"
            },
            new RelationTable{
                field_name="status_akhir_legal_comp_id"
                ,field_alias = "status_akhir_legal_comp_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_akhir_legal_comp_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.status_akhir_legal_comp_id = lk_status_akhir_legal_comp_id.id AND lk_status_akhir_legal_comp_id.cat_id=49"
                ,select_column = "lk_status_akhir_legal_comp_id.nama as status_akhir_legal_comp_id_text"
            },
            new RelationTable{
                field_name="status_akhir_agc_id"
                ,field_alias = "status_akhir_agc_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_status_akhir_agc_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_audit_kriteria_agc.status_akhir_agc_id = lk_status_akhir_agc_id.id AND lk_status_akhir_agc_id.cat_id=49"
                ,select_column = "lk_status_akhir_agc_id.nama as status_akhir_agc_id_text"
            }            
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string audit_id { get; set; }
            public string kriteria_audit_id { get; set; }
            public string strategy { get; set; }
            public string process { get; set; }
            public string product { get; set; }
            public string employee { get; set; }
            public string status_akhir_acp_id { get; set; }
            public string status_akhir_acp_id_text { get; set; }
            public string amdal_id { get; set; }
            public string amdal_id_text { get; set; }
            public string pencemaran_air_id { get; set; }
            public string pencemaran_air_id_text { get; set; }
            public string pencemaran_udara_id { get; set; }
            public string pencemaran_udara_id_text { get; set; }
            public string limbah_lb3_id { get; set; }
            public string limbah_lb3_id_text { get; set; }
            public string limbah_nonlb3_id { get; set; }
            public string limbah_nonlb3_id_text { get; set; }
            public string comdev_id { get; set; }
            public string comdev_id_text { get; set; }
            public string status_akhir_cpproper_id { get; set; }
            public string status_akhir_cpproper_id_text { get; set; }
            public string sarana_k3_id { get; set; }
            public string sarana_k3_id_text { get; set; }
            public string sarana_ktd_id { get; set; }
            public string sarana_ktd_id_text { get; set; }
            public string simulasi_ktd_id { get; set; }
            public string simulasi_ktd_id_text { get; set; }
            public string fr_tk { get; set; }
            public string sr_tf { get; set; }
            public string status_akhir_incident_rate_id { get; set; }
            public string status_akhir_incident_rate_id_text { get; set; }
            public string status_akhir_cpsafety_id { get; set; }
            public string status_akhir_cpsafety_id_text { get; set; }
            public string status_akhir_legal_comp_id { get; set; }
            public string status_akhir_legal_comp_id_text { get; set; }
            public string status_akhir_agc_id { get; set; }
            public string status_akhir_agc_id_text { get; set; }
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
                    name= "strategy",
                    field= _table_name+".strategy",
                    field_order= _table_name+".strategy",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"strategy"),
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
                    name= "process",
                    field= _table_name+".process",
                    field_order= _table_name+".process",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"process"),
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
                    name= "product",
                    field= _table_name+".product",
                    field_order= _table_name+".product",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"product"),
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
                    name= "employee",
                    field= _table_name+".employee",
                    field_order= _table_name+".employee",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"employee"),
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
                    name= "status_akhir_acp_id",
                    field= _table_name+".status_akhir_acp_id",
                    field_order= _table_name+".status_akhir_acp_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_acp_id"),
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
                    name= "status_akhir_acp_id_text",
                    field= "lk_status_akhir_acp_id.nama",
                    field_order= "lk_status_akhir_acp_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_acp_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_acp_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_akhir_acp_id_bg_color#; \" }",
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
                    name= "status_akhir_acp_id_bg_color",
                    field= "lk_status_akhir_acp_id.bg_color",
                    field_order= "lk_status_akhir_acp_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_acp_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_acp_id"),
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
                    name= "amdal_id",
                    field= _table_name+".amdal_id",
                    field_order= _table_name+".amdal_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"amdal_id"),
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
                    name= "amdal_id_text",
                    field= "lk_amdal_id.nama",
                    field_order= "lk_amdal_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_amdal_id",
                    title= @ResxHelper.GetValue(_table_name,"amdal_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= amdal_id_bg_color#; \" }",
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
                    name= "amdal_id_bg_color",
                    field= "lk_amdal_id.bg_color",
                    field_order= "lk_amdal_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_amdal_id",
                    title= @ResxHelper.GetValue(_table_name,"amdal_id"),
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
                    name= "pencemaran_air_id",
                    field= _table_name+".pencemaran_air_id",
                    field_order= _table_name+".pencemaran_air_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pencemaran_air_id"),
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
                    name= "pencemaran_air_id_text",
                    field= "lk_pencemaran_air_id.nama",
                    field_order= "lk_pencemaran_air_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_pencemaran_air_id",
                    title= @ResxHelper.GetValue(_table_name,"pencemaran_air_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= pencemaran_air_id_bg_color#; \" }",
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
                    name= "pencemaran_air_id_bg_color",
                    field= "lk_pencemaran_air_id.bg_color",
                    field_order= "lk_pencemaran_air_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_pencemaran_air_id",
                    title= @ResxHelper.GetValue(_table_name,"pencemaran_air_id"),
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
                    name= "pencemaran_udara_id",
                    field= _table_name+".pencemaran_udara_id",
                    field_order= _table_name+".pencemaran_udara_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"pencemaran_udara_id"),
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
                    name= "pencemaran_udara_id_text",
                    field= "lk_pencemaran_udara_id.nama",
                    field_order= "lk_pencemaran_udara_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_pencemaran_udara_id",
                    title= @ResxHelper.GetValue(_table_name,"pencemaran_udara_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= pencemaran_udara_id_bg_color#; \" }",
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
                    name= "pencemaran_udara_id_bg_color",
                    field= "lk_pencemaran_udara_id.bg_color",
                    field_order= "lk_pencemaran_udara_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_pencemaran_udara_id",
                    title= @ResxHelper.GetValue(_table_name,"pencemaran_udara_id"),
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
                    name= "limbah_lb3_id",
                    field= _table_name+".limbah_lb3_id",
                    field_order= _table_name+".limbah_lb3_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"limbah_lb3_id"),
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
                    name= "limbah_lb3_id_text",
                    field= "lk_limbah_lb3_id.nama",
                    field_order= "lk_limbah_lb3_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_limbah_lb3_id",
                    title= @ResxHelper.GetValue(_table_name,"limbah_lb3_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= limbah_lb3_id_bg_color#; \" }",
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
                    name= "limbah_lb3_id_bg_color",
                    field= "lk_limbah_lb3_id.bg_color",
                    field_order= "lk_limbah_lb3_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_limbah_lb3_id",
                    title= @ResxHelper.GetValue(_table_name,"limbah_lb3_id"),
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
                    name= "limbah_nonlb3_id",
                    field= _table_name+".limbah_nonlb3_id",
                    field_order= _table_name+".limbah_nonlb3_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"limbah_nonlb3_id"),
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
                    name= "limbah_nonlb3_id_text",
                    field= "lk_limbah_nonlb3_id.nama",
                    field_order= "lk_limbah_nonlb3_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_limbah_nonlb3_id",
                    title= @ResxHelper.GetValue(_table_name,"limbah_nonlb3_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= limbah_nonlb3_id_bg_color#; \" }",
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
                    name= "limbah_nonlb3_id_bg_color",
                    field= "lk_limbah_nonlb3_id.bg_color",
                    field_order= "lk_limbah_nonlb3_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_limbah_nonlb3_id",
                    title= @ResxHelper.GetValue(_table_name,"limbah_nonlb3_id"),
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
                    name= "comdev_id",
                    field= _table_name+".comdev_id",
                    field_order= _table_name+".comdev_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"comdev_id"),
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
                    name= "comdev_id_text",
                    field= "lk_comdev_id.nama",
                    field_order= "lk_comdev_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_comdev_id",
                    title= @ResxHelper.GetValue(_table_name,"comdev_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= comdev_id_bg_color#; \" }",
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
                    name= "comdev_id_bg_color",
                    field= "lk_comdev_id.bg_color",
                    field_order= "lk_comdev_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_comdev_id",
                    title= @ResxHelper.GetValue(_table_name,"comdev_id"),
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
                    name= "status_akhir_cpproper_id",
                    field= _table_name+".status_akhir_cpproper_id",
                    field_order= _table_name+".status_akhir_cpproper_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_cpproper_id"),
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
                    name= "status_akhir_cpproper_id_text",
                    field= "lk_status_akhir_cpproper_id.nama",
                    field_order= "lk_status_akhir_cpproper_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_cpproper_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_cpproper_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_akhir_cpproper_id_bg_color#; \" }",
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
                    name= "status_akhir_cpproper_id_bg_color",
                    field= "lk_status_akhir_cpproper_id.bg_color",
                    field_order= "lk_status_akhir_cpproper_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_cpproper_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_cpproper_id"),
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
                    name= "sarana_k3_id",
                    field= _table_name+".sarana_k3_id",
                    field_order= _table_name+".sarana_k3_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"sarana_k3_id"),
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
                    name= "sarana_k3_id_text",
                    field= "lk_sarana_k3_id.nama",
                    field_order= "lk_sarana_k3_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_sarana_k3_id",
                    title= @ResxHelper.GetValue(_table_name,"sarana_k3_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= sarana_k3_id_bg_color#; \" }",
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
                    name= "sarana_k3_id_bg_color",
                    field= "lk_sarana_k3_id.bg_color",
                    field_order= "lk_sarana_k3_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_sarana_k3_id",
                    title= @ResxHelper.GetValue(_table_name,"sarana_k3_id"),
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
                    name= "sarana_ktd_id",
                    field= _table_name+".sarana_ktd_id",
                    field_order= _table_name+".sarana_ktd_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"sarana_ktd_id"),
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
                    name= "sarana_ktd_id_text",
                    field= "lk_sarana_ktd_id.nama",
                    field_order= "lk_sarana_ktd_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_sarana_ktd_id",
                    title= @ResxHelper.GetValue(_table_name,"sarana_ktd_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= sarana_ktd_id_bg_color#; \" }",
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
                    name= "sarana_ktd_id_bg_color",
                    field= "lk_sarana_ktd_id.bg_color",
                    field_order= "lk_sarana_ktd_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_sarana_ktd_id",
                    title= @ResxHelper.GetValue(_table_name,"sarana_ktd_id"),
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
                    name= "simulasi_ktd_id",
                    field= _table_name+".simulasi_ktd_id",
                    field_order= _table_name+".simulasi_ktd_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"simulasi_ktd_id"),
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
                    name= "simulasi_ktd_id_text",
                    field= "lk_simulasi_ktd_id.nama",
                    field_order= "lk_simulasi_ktd_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_simulasi_ktd_id",
                    title= @ResxHelper.GetValue(_table_name,"simulasi_ktd_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= simulasi_ktd_id_bg_color#; \" }",
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
                    name= "simulasi_ktd_id_bg_color",
                    field= "lk_simulasi_ktd_id.bg_color",
                    field_order= "lk_simulasi_ktd_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_simulasi_ktd_id",
                    title= @ResxHelper.GetValue(_table_name,"simulasi_ktd_id"),
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
                    name= "fr_tk",
                    field= _table_name+".fr_tk",
                    field_order= _table_name+".fr_tk",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"fr_tk"),
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
                    name= "sr_tf",
                    field= _table_name+".sr_tf",
                    field_order= _table_name+".sr_tf",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"sr_tf"),
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
                    name= "status_akhir_incident_rate_id",
                    field= _table_name+".status_akhir_incident_rate_id",
                    field_order= _table_name+".status_akhir_incident_rate_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_incident_rate_id"),
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
                    name= "status_akhir_incident_rate_id_text",
                    field= "lk_status_akhir_incident_rate_id.nama",
                    field_order= "lk_status_akhir_incident_rate_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_incident_rate_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_incident_rate_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_akhir_incident_rate_id_bg_color#; \" }",
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
                    name= "status_akhir_incident_rate_id_bg_color",
                    field= "lk_status_akhir_incident_rate_id.bg_color",
                    field_order= "lk_status_akhir_incident_rate_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_incident_rate_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_incident_rate_id"),
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
                    name= "status_akhir_cpsafety_id",
                    field= _table_name+".status_akhir_cpsafety_id",
                    field_order= _table_name+".status_akhir_cpsafety_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_cpsafety_id"),
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
                    name= "status_akhir_cpsafety_id_text",
                    field= "lk_status_akhir_cpsafety_id.nama",
                    field_order= "lk_status_akhir_cpsafety_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_cpsafety_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_cpsafety_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_akhir_cpsafety_id_bg_color#; \" }",
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
                    name= "status_akhir_cpsafety_id_bg_color",
                    field= "lk_status_akhir_cpsafety_id.bg_color",
                    field_order= "lk_status_akhir_cpsafety_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_cpsafety_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_cpsafety_id"),
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
                    name= "status_akhir_legal_comp_id",
                    field= _table_name+".status_akhir_legal_comp_id",
                    field_order= _table_name+".status_akhir_legal_comp_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_legal_comp_id"),
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
                    name= "status_akhir_legal_comp_id_text",
                    field= "lk_status_akhir_legal_comp_id.nama",
                    field_order= "lk_status_akhir_legal_comp_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_legal_comp_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_legal_comp_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_akhir_legal_comp_id_bg_color#; \" }",
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
                    name= "status_akhir_legal_comp_id_bg_color",
                    field= "lk_status_akhir_legal_comp_id.bg_color",
                    field_order= "lk_status_akhir_legal_comp_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_legal_comp_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_legal_comp_id"),
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
                    name= "status_akhir_agc_id",
                    field= _table_name+".status_akhir_agc_id",
                    field_order= _table_name+".status_akhir_agc_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_agc_id"),
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
                    name= "status_akhir_agc_id_text",
                    field= "lk_status_akhir_agc_id.nama",
                    field_order= "lk_status_akhir_agc_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_agc_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_agc_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left;background-color:#= status_akhir_agc_id_bg_color#; \" }",
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
                    name= "status_akhir_agc_id_bg_color",
                    field= "lk_status_akhir_agc_id.bg_color",
                    field_order= "lk_status_akhir_agc_id.bg_color",
                    table_name = "ref_literal_data",
                    table_alias = "lk_status_akhir_agc_id",
                    title= @ResxHelper.GetValue(_table_name,"status_akhir_agc_id"),
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





        public static string GetStatusAkhirACP(string strStrategy, string strProcess, string strProduct, string strEmployee)
        {
            int result = 0;

            double strategy = 0;
            double process = 0;
            double product = 0;
            double employee = 0;
            double divider = 4;

            if (strStrategy != "") { strategy = double.Parse(strStrategy); }
            if (strProcess != "") { process = double.Parse(strProcess); }
            if (strProduct != "") { product = double.Parse(strProduct); }
            if (strEmployee != "") { employee = double.Parse(strEmployee); }
            double x = (strategy + process + product + employee) / divider;

            if (x >= 90 & x <= 100)
            {
                result = 5;
            }
            else if (x >= 76 & x <= 89)
            {
                result = 4;
            }
            else if (x >= 51 & x <= 75)
            {
                result = 3;
            }
            else if (x >= 21 & x <= 50)
            {
                result = 2;
            }
            else if (x >= 0 & x <= 20)
            {
                result = 1;
            }
            return result.ToString();       
        }


        public static string GetStatusAkhirCPProper(string strAmdal, string strPencemaranAir, string strPencemaranUdara, string strLimbahB3, string strLimbahNonB3, string strComDev)
        {
            int Amdal = 0;
            int PencemaranAir = 0;
            int PencemaranUdara = 0;
            int LimbahB3 = 0;
            int LimbahNonB3 = 0;
            int ComDev = 0;

            if (strAmdal != "") { Amdal = int.Parse(strAmdal); }
            if (strPencemaranAir != "") { PencemaranAir = int.Parse(strPencemaranAir); }
            if (strPencemaranUdara != "") { PencemaranUdara = int.Parse(strPencemaranUdara); }
            if (strLimbahB3 != "") { LimbahB3 = int.Parse(strLimbahB3); }
            if (strLimbahNonB3 != "") { LimbahNonB3 = int.Parse(strLimbahNonB3); }
            if (strComDev != "") { ComDev = int.Parse(strComDev); }


            List<int> cp = new List<int>() { Amdal, PencemaranAir, PencemaranUdara, LimbahB3, LimbahNonB3, ComDev };
            var result = cp.OrderByDescending(m => m).First<int>();
            return result.ToString();
        }


        public static string GetStatusAkhirIncidentRate(string curAuditId, string strFR, string strSR)
        {
            double FR = 0;
            double SR = 0;

            if (strFR != "") { FR = double.Parse(strFR); }
            if (strSR != "") { SR = double.Parse(strSR); }



            // jumlah karyawan for FR calculation
            string sql = "select count(p.id) from ta_mp p inner join ta_audit a on a.psa_id = p.personal_sub_area_id where (p.tgl_akhir_kerja is null or p.tgl_akhir_kerja <= GETDATE()) and a.id = " + curAuditId;
            int empCount = SqlHelper.ExecuteScalarInt(sql);


            //FR
            double x = FR;
            int FRStatus = 0;
            if (empCount < 100)
            {
                if (x < 1.5)
                {
                    FRStatus = 5;
                }
                else if (1.5 <= x & x <= 5.4)
                {
                    FRStatus = 4;
                }
                else if (5.4 < x & x <= 10.4)
                {
                    FRStatus = 3;

                }
                else if (10.4 < x & x <= 15)
                {
                    FRStatus = 2;
                }                
                else if (x > 15)
                {
                    FRStatus = 1;
                }

            }
            else if (empCount >= 100 & empCount <= 299) 
            {
                if (x < 1)
                {
                    FRStatus = 5;
                }
                else if (1 <= x & x < 3)
                {
                    FRStatus = 4;
                }
                else if (3 <= x & x < 5)
                {
                    FRStatus = 3;

                }
                else if (5 <= x & x <= 6.4)
                {
                    FRStatus = 2;
                }
                else if (x >= 6.5)
                {
                    FRStatus = 1;
                }
            }
            else if (empCount >= 300 & empCount <= 499)
            {
                if (x < 0.5)
                {
                    FRStatus = 5;
                }
                else if (0.5 <= x & x <= 1.4)
                {
                    FRStatus = 4;
                }
                else if (1.5 <= x & x <= 2.4)
                {
                    FRStatus = 3;

                }
                else if (2.5 <= x & x <= 3.4)
                {
                    FRStatus = 2;
                }
                else if (x >= 3.5)
                {
                    FRStatus = 1;
                }
            }
            else if (empCount >= 500)
            {
                if (x < 0.5)
                {
                    FRStatus = 5;
                }
                else if (0.5 <= x & x <= 1.9)
                {
                    FRStatus = 4;
                }
                else if (1.5 <= x & x < 2)
                {
                    FRStatus = 3;

                }
                else if (2 <= x & x < 3)
                {
                    FRStatus = 2;
                }
                else if (x >= 3)
                {
                    FRStatus = 1;
                }

            }




            //SR
            x = SR;
            int SRStatus = 0;
            if (x < 20)
            {
                SRStatus = 5;
            }
            else if (20 <= x & x < 170)
            {
                SRStatus = 4;
            }
            else if (170 <= x & x < 375)
            {
                SRStatus = 3;

            }
            else if (375 <= x & x < 750)
            {
                SRStatus = 2;
            }
            else if (x >= 750)
            {
                SRStatus = 1;
            }
            


            List<int> cp = new List<int>() { FRStatus, SRStatus };
            var result = cp.OrderByDescending(m => m).First<int>();
            return result.ToString();
        }


        public static string GetStatusAkhirCPSafety(string strSaranaK3, string strSaranaKTD, string strSimulasiKTD, string curAuditId, string strFR, string strSR)
        {
            int SaranaK3 = 0;
            int SaranaKTD = 0;
            int SimulasiKTD = 0;
            int IncidentRate = int.Parse(GetStatusAkhirIncidentRate(curAuditId, strFR, strSR));

            if (strSaranaK3 != "") { SaranaK3 = int.Parse(strSaranaK3); }
            if (strSaranaKTD != "") { SaranaKTD = int.Parse(strSaranaKTD); }
            if (strSimulasiKTD != "") { SimulasiKTD = int.Parse(strSimulasiKTD); }            


            List<int> cp = new List<int>() { SaranaK3, SaranaKTD, SimulasiKTD, IncidentRate };
            var result = cp.OrderByDescending(m => m).First<int>(); //sort and get first row
            return result.ToString();
        }


        public static string GetStatusAkhirAGC(string strACP, string strCPProper, string strCPSafety, string strLegalComp)
        {
            int ACP = 0;
            int CPProper = 0;
            int CPSafety = 0;
            int LegalComp = 0;

            if (strACP != "") { ACP = int.Parse(strACP); }
            if (strCPProper != "") { CPProper = int.Parse(strCPProper); }
            if (strCPSafety != "") { CPSafety = int.Parse(strCPSafety); }
            if (strLegalComp != "") { LegalComp = int.Parse(strLegalComp); }


            List<int> cp = new List<int>() { ACP, CPProper, CPSafety, LegalComp };
            var result = cp.OrderByDescending(m => m).First<int>();
            return result.ToString();
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
