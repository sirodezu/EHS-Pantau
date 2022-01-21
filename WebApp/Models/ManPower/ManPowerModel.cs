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
    public class ManPowerModel
    {
        public static string _table_name = "ta_mp";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 1;
        public static string[] _unKey = { "nrp" }; /**---*/
        public static string _columnOrder = "ta_mp.id DESC";
        public static string _displayColumn = "nama_lengkap";
        public static IList<RelationTable> _join = new List<RelationTable> {
            new RelationTable{
                field_name="karyawan_nonkaryawan_id"
                ,field_alias = "karyawan_nonkaryawan_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_karyawan_nonkaryawan_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.karyawan_nonkaryawan_id = lk_karyawan_nonkaryawan_id.id AND lk_karyawan_nonkaryawan_id.cat_id=147"
                ,select_column = "lk_karyawan_nonkaryawan_id.nama as karyawan_nonkaryawan_id_text"
            },
            new RelationTable{
                field_name="user_nonuser_id"
                ,field_alias = "user_nonuser_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_user_nonuser_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.user_nonuser_id = lk_user_nonuser_id.id AND lk_user_nonuser_id.cat_id=148"
                ,select_column = "lk_user_nonuser_id.nama as user_nonuser_id_text"
            },
            new RelationTable{
                field_name="person_id"
                ,field_alias = "person_id_text"
                ,table_name = "ta_person"
                ,table_alias = "lk_person_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.person_id = lk_person_id.id"
                ,select_column = "(cast(lk_person_id.nrp as varchar(50)) + ' - '+cast(lk_person_id.nama as varchar(50))) as person_id_text"
            },
            new RelationTable{
                field_name="jenis_kelamin_id"
                ,field_alias = "jenis_kelamin_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_jenis_kelamin_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.jenis_kelamin_id = lk_jenis_kelamin_id.id AND lk_jenis_kelamin_id.cat_id=5"
                ,select_column = "lk_jenis_kelamin_id.nama as jenis_kelamin_id_text"
            },
            new RelationTable{
                field_name="company_id"
                ,field_alias = "company_id_text"
                ,table_name = "ref_company"
                ,table_alias = "lk_company_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.company_id = lk_company_id.id"
                ,select_column = "lk_company_id.nama as company_id_text"
            },
            new RelationTable{
                field_name="ehs_area_id"
                ,field_alias = "ehs_area_id_text"
                ,table_name = "ref_ehs_area"
                ,table_alias = "lk_ehs_area_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.ehs_area_id = lk_ehs_area_id.id"
                ,select_column = "lk_ehs_area_id.nama as ehs_area_id_text"
            },
            new RelationTable{
                field_name="business_area_id"
                ,field_alias = "business_area_id_text"
                ,table_name = "ref_business_area"
                ,table_alias = "lk_business_area_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.business_area_id = lk_business_area_id.id"
                ,select_column = "(cast(lk_business_area_id.kode as varchar(50)) + ' - '+cast(lk_business_area_id.nama as varchar(50))) as business_area_id_text"
            },
            new RelationTable{
                field_name="personal_area_id"
                ,field_alias = "personal_area_id_text"
                ,table_name = "ref_personal_area"
                ,table_alias = "lk_personal_area_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.personal_area_id = lk_personal_area_id.id"
                ,select_column = "(cast(lk_personal_area_id.kode as varchar(50)) + ' - '+cast(lk_personal_area_id.nama as varchar(50))) as personal_area_id_text"
            },
            new RelationTable{
                field_name="personal_sub_area_id"
                ,field_alias = "personal_sub_area_id_text"
                ,table_name = "ref_personal_sub_area"
                ,table_alias = "lk_personal_sub_area_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.personal_sub_area_id = lk_personal_sub_area_id.id"
                ,select_column = "(cast(lk_personal_sub_area_id.kode as varchar(50)) + ' - '+cast(lk_personal_sub_area_id.nama as varchar(50))) as personal_sub_area_id_text"
            },
            new RelationTable{
                field_name="departemen_id"
                ,field_alias = "departemen_id_text"
                ,table_name = "ref_personal_sub_area"
                ,table_alias = "lk_departemen_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.departemen_id = lk_departemen_id.id"
                ,select_column = "lk_departemen_id.nama as departemen_id_text"
            },
            new RelationTable{
                field_name="level_jabatan_id"
                ,field_alias = "level_jabatan_id_text"
                ,table_name = "ref_literal_data"
                ,table_alias = "lk_level_jabatan_id"
                ,join_type = "LEFT OUTER JOIN "
                ,join_on = "ta_mp.level_jabatan_id = lk_level_jabatan_id.id AND lk_level_jabatan_id.cat_id=154"
                ,select_column = "lk_level_jabatan_id.nama as level_jabatan_id_text"
            }            
        };
        public class ViewModel
        {
            public string id { get; set; }
            public string id_old { get; set; }
            public string karyawan_nonkaryawan_id { get; set; }
            public string karyawan_nonkaryawan_id_text { get; set; }
            public string user_nonuser_id { get; set; }
            public string user_nonuser_id_text { get; set; }
            public string person_id { get; set; }
            public string person_id_text { get; set; }
            public string file_path_photo { get; set; }
            public string file_path_photo_init { get; set; }
            public string photo { get; set; }
            public string photo_link { get; set; }

            public string nama_lengkap { get; set; }
            public string nrp { get; set; }
            public string tempat_lahir { get; set; }
            public string tgl_lahir { get; set; }
            public string dt_tgl_lahir { get; set; }
            public string jenis_kelamin_id { get; set; }
            public string jenis_kelamin_id_text { get; set; }
            public string company_id { get; set; }
            public string company_id_text { get; set; }
            public string ehs_area_id { get; set; }
            public string ehs_area_id_text { get; set; }
            public string business_area_id { get; set; }
            public string business_area_id_text { get; set; }
            public string personal_area_id { get; set; }
            public string personal_area_id_text { get; set; }
            public string personal_sub_area_id { get; set; }
            public string personal_sub_area_id_text { get; set; }
            public string departemen_id { get; set; }
            public string departemen_id_text { get; set; }
            public string level_jabatan_id { get; set; }
            public string level_jabatan_id_text { get; set; }
            public string level_jabatan_nama { get; set; }
            public string tgl_masuk_kerja { get; set; }
            public string dt_tgl_masuk_kerja { get; set; }
            public string tgl_akhir_kerja { get; set; }
            public string dt_tgl_akhir_kerja { get; set; }
            public string job_id { get; set; }
            public string job { get; set; }
            public string job_start { get; set; }
            public string dt_job_start { get; set; }
            public string position { get; set; }
            public string position_start { get; set; }
            public string dt_position_start { get; set; }
            public string behaviour_screen_nilai { get; set; }
            public string behaviour_screen_objective { get; set; }
            public string toc_nilai { get; set; }
            public string toc_objective { get; set; }
            public string bmc_nilai { get; set; }
            public string bmc_objective { get; set; }
            public string english_nilai { get; set; }
            public string english_objective { get; set; }
            public string insert_by { get; set; }
            public string insert_at { get; set; }
            public string update_by { get; set; }
            public string update_at { get; set; }
            public string fn { get; set; }

            public string screening_time { get; set; }
            public string dt_screening_time { get; set; }

            public string f_umur_personal { get; set; }
            public string f_umur_kerja { get; set; }

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
                    name= "karyawan_nonkaryawan_id",
                    field= _table_name+".karyawan_nonkaryawan_id",
                    field_order= _table_name+".karyawan_nonkaryawan_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"karyawan_nonkaryawan_id"),
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
                    name= "karyawan_nonkaryawan_id_text",
                    field= "lk_karyawan_nonkaryawan_id.nama",
                    field_order= "lk_karyawan_nonkaryawan_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_karyawan_nonkaryawan_id",
                    title= @ResxHelper.GetValue(_table_name,"karyawan_nonkaryawan_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "100px",
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
                    name= "user_nonuser_id",
                    field= _table_name+".user_nonuser_id",
                    field_order= _table_name+".user_nonuser_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"user_nonuser_id"),
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
                    name= "user_nonuser_id_text",
                    field= "lk_user_nonuser_id.nama",
                    field_order= "lk_user_nonuser_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_user_nonuser_id",
                    title= @ResxHelper.GetValue(_table_name,"user_nonuser_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "80px",
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
                    name= "person_id",
                    field= _table_name+".person_id",
                    field_order= _table_name+".person_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"person_id"),
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
                //new GridColumn{
                //    name= "person_id_text",
                //    field= "(cast(lk_person_id.nrp as varchar(50)) + ' - '+cast(lk_person_id.nama as varchar(50)))",
                //    field_order= "(cast(lk_person_id.nrp as varchar(50)) + ' - '+cast(lk_person_id.nama as varchar(50)))",
                //    table_name = "ta_person",
                //    table_alias = "lk_person_id",
                //    title= @ResxHelper.GetValue(_table_name,"person_id"),
                //    type= "string",
                //    select= false,
                //    display= false,
                //    hidden= true,
                //    width= "",
                //    format= "",
                //    attributes= "{ \"style\": \"text-align: left; \" }",
                //    template= "",
                //    encoded= false,
                //    filterable= false,
                //    sortable= true,
                //    qsearch= false,
                //    tooltip= "",
                //    aggregate= "",
                //    footerTemplate= ""
                //},
                new GridColumn{
                    name= "file_path_photo",
                    field= _table_name+".file_path_photo",
                    field_order= _table_name+".file_path_photo",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"file_path_photo"),
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
                    sortable= true,
                    qsearch= true,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                },
                new GridColumn{
                    name= "nama_lengkap",
                    field= _table_name+".nama_lengkap",
                    field_order= _table_name+".nama_lengkap",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"nama_lengkap"),
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
                    name= "nrp",
                    field= _table_name+".NRP",
                    field_order= _table_name+".NRP",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"NRP"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "100px",
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
                    name= "tempat_lahir",
                    field= _table_name+".tempat_lahir",
                    field_order= _table_name+".tempat_lahir",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tempat_lahir"),
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
                    name= "tgl_lahir",
                    field= _table_name+".tgl_lahir",
                    field_order= _table_name+".tgl_lahir",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tgl_lahir"),
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
                    name= "jenis_kelamin_id",
                    field= _table_name+".jenis_kelamin_id",
                    field_order= _table_name+".jenis_kelamin_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"jenis_kelamin_id"),
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
                    name= "jenis_kelamin_id_text",
                    field= "lk_jenis_kelamin_id.nama",
                    field_order= "lk_jenis_kelamin_id.nama",
                    table_name = "ref_literal_data",
                    table_alias = "lk_jenis_kelamin_id",
                    title= @ResxHelper.GetValue(_table_name,"jenis_kelamin_id"),
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
                    name= "company_id",
                    field= _table_name+".company_id",
                    field_order= _table_name+".company_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"company_id"),
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
                    name= "company_id_text",
                    field= "lk_company_id.nama",
                    field_order= "lk_company_id.nama",
                    table_name = "ref_company",
                    table_alias = "lk_company_id",
                    title= @ResxHelper.GetValue(_table_name,"company_id"),
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
                    field_order= "lk_ehs_area_id.nama",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id",
                    title= @ResxHelper.GetValue(_table_name,"ehs_area_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "70px",
                    format= "",
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
                    name= "business_area_id",
                    field= _table_name+".business_area_id",
                    field_order= _table_name+".business_area_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"business_area_id"),
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
                    name= "business_area_id_text",
                    field= "lk_business_area_id.kode",
                    field_order= "lk_business_area_id.kode",
                    table_name = "ref_business_area",
                    table_alias = "lk_business_area_id",
                    title= @ResxHelper.GetValue(_table_name,"business_area_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "70px",
                    format= "",
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
                    name= "personal_area_id",
                    field= _table_name+".personal_area_id",
                    field_order= _table_name+".personal_area_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"personal_area_id"),
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
                    name= "personal_area_id_text",
                    field= "lk_personal_area_id.kode",
                    field_order= "lk_personal_area_id.kode",
                    table_name = "ref_personal_area",
                    table_alias = "lk_personal_area_id",
                    title= @ResxHelper.GetValue(_table_name,"personal_area_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= true,
                    width= "70px",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
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
                    name= "personal_sub_area_id",
                    field= _table_name+".personal_sub_area_id",
                    field_order= _table_name+".personal_sub_area_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"personal_sub_area_id"),
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
                    name= "personal_sub_area_id_text",
                    field= "lk_personal_sub_area_id.kode",
                    field_order= "lk_personal_sub_area_id.kode",
                    table_name = "ref_personal_sub_area",
                    table_alias = "lk_personal_sub_area_id",
                    title= @ResxHelper.GetValue(_table_name,"personal_sub_area_id"),
                    type= "string",
                    select= true,
                    display= true,
                    hidden= false,
                    width= "90px",
                    format= "",
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
                    name= "level_jabatan_id",
                    field= _table_name+".level_jabatan_id",
                    field_order= _table_name+".level_jabatan_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"level_jabatan_id"),
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
                //new GridColumn{
                //    name= "level_jabatan_id_text",
                //    //field= "lk_level_jabatan_id.nama",
                //    //field_order= "lk_level_jabatan_id.nama",
                //    field= "case when "+_table_name+".karyawan_nonkaryawan_id=1 then "+_table_name+".job else (case when "+_table_name+".user_nonuser_id=1 then lk_level_jabatan_id.nama else "+_table_name+".level_jabatan_nama end) end",
                //    field_order= "case when "+_table_name+".karyawan_nonkaryawan_id=1 then "+_table_name+".job else (case when "+_table_name+".user_nonuser_id=1 then lk_level_jabatan_id.nama else "+_table_name+".level_jabatan_nama end) end",
                //    table_name = "ref_literal_data",
                //    table_alias = "lk_level_jabatan_id",
                //    title= @ResxHelper.GetValue(_table_name,"level_jabatan_id"),
                //    type= "string",
                //    select= true,
                //    display= true,
                //    hidden= true,
                //    width= "",
                //    format= "",
                //    attributes= "{ \"style\": \"text-align: left; \" }",
                //    template= "",
                //    encoded= false,
                //    filterable= false,
                //    sortable= true,
                //    qsearch= false,
                //    tooltip= "",
                //    aggregate= "",
                //    footerTemplate= ""
                //},
                //new GridColumn{
                //    name= "level_jabatan_nama",
                //    //field= _table_name+".level_jabatan_nama",
                //    //field_order= _table_name+".level_jabatan_nama",
                //    field= "case when "+_table_name+".karyawan_nonkaryawan_id=1 then "+_table_name+".job else "+_table_name+".level_jabatan_nama end",
                //    field_order= "case when "+_table_name+".karyawan_nonkaryawan_id=1 then "+_table_name+".job else "+_table_name+".level_jabatan_nama end",
                //    table_name = _table_name,
                //    table_alias = _table_name,
                //    title= @ResxHelper.GetValue(_table_name,"level_jabatan_nama"),
                //    type= "string",
                //    select= true,
                //    display= true,
                //    hidden= true,
                //    width= "",
                //    format= "",
                //    attributes= "{ \"style\": \"text-align: left; \" }",
                //    template= "",
                //    encoded= false,
                //    filterable= false,
                //    sortable= true,
                //    qsearch= true,
                //    tooltip= "",
                //    aggregate= "",
                //    footerTemplate= ""
                //},
                //new GridColumn{
                //    name= "departemen_id",
                //    field= _table_name+".departemen_id",
                //    field_order= _table_name+".departemen_id",
                //    table_name = _table_name,
                //    table_alias = _table_name,
                //    title= @ResxHelper.GetValue(_table_name,"departemen_id"),
                //    type= "number",
                //    select= true,
                //    display= false,
                //    hidden= true,
                //    width= "",
                //    format= "{0:N0}",
                //    attributes= "{ \"style\": \"text-align: right; \" }",
                //    template= "",
                //    encoded= false,
                //    filterable= true,
                //    sortable= true,
                //    qsearch= false,
                //    tooltip= "",
                //    aggregate= "",
                //    footerTemplate= ""
                //},
                //new GridColumn{
                //    name= "departemen_id_text",
                //    //field= "lk_departemen_id.nama",
                //    //field_order= "lk_departemen_id.nama",
                //    field= "case when "+_table_name+".karyawan_nonkaryawan_id=1 then "+_table_name+".position else lk_departemen_id.nama end",
                //    field_order= "case when "+_table_name+".karyawan_nonkaryawan_id=1 then "+_table_name+".position else lk_departemen_id.nama end",
                //    table_name = "ref_personal_sub_area",
                //    table_alias = "lk_departemen_id",
                //    title= @ResxHelper.GetValue(_table_name,"departemen_id"),
                //    type= "string",
                //    select= true,
                //    display= true,
                //    hidden= true,
                //    width= "",
                //    format= "",
                //    attributes= "{ \"style\": \"text-align: left; \" }",
                //    template= "",
                //    encoded= false,
                //    filterable= false,
                //    sortable= true,
                //    qsearch= false,
                //    tooltip= "",
                //    aggregate= "",
                //    footerTemplate= ""
                //},
                new GridColumn{
                    name= "job_id",
                    field= _table_name+".job_id",
                    field_order= _table_name+".job_id",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"job_id"),
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
                    name= "job",
                    field= _table_name+".job",
                    field_order= _table_name+".job",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"job"),
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
                    name= "position",
                    field= _table_name+".position",
                    field_order= _table_name+".position",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"position"),
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
                    name= "tgl_masuk_kerja",
                    field= _table_name+".tgl_masuk_kerja",
                    field_order= _table_name+".tgl_masuk_kerja",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tgl_masuk_kerja"),
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
                    name= "tgl_akhir_kerja",
                    field= _table_name+".tgl_akhir_kerja",
                    field_order= _table_name+".tgl_akhir_kerja",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"tgl_akhir_kerja"),
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
                    name= "behaviour_screen_nilai",
                    field= _table_name+".behaviour_screen_nilai",
                    field_order= _table_name+".behaviour_screen_nilai",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"behaviour_screen_nilai"),
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
                    name= "behaviour_screen_objective",
                    field= _table_name+".behaviour_screen_objective",
                    field_order= _table_name+".behaviour_screen_objective",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"behaviour_screen_objective"),
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
                    name= "toc_nilai",
                    field= _table_name+".toc_nilai",
                    field_order= _table_name+".toc_nilai",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"toc_nilai"),
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
                    name= "toc_objective",
                    field= _table_name+".toc_objective",
                    field_order= _table_name+".toc_objective",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"toc_objective"),
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
                    name= "bmc_nilai",
                    field= _table_name+".bmc_nilai",
                    field_order= _table_name+".bmc_nilai",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"bmc_nilai"),
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
                    name= "bmc_objective",
                    field= _table_name+".bmc_objective",
                    field_order= _table_name+".bmc_objective",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"bmc_objective"),
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
                    name= "english_nilai",
                    field= _table_name+".english_nilai",
                    field_order= _table_name+".english_nilai",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"english_nilai"),
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
                    name= "english_objective",
                    field= _table_name+".english_objective",
                    field_order= _table_name+".english_objective",
                    table_name = _table_name,
                    table_alias = _table_name,
                    title= @ResxHelper.GetValue(_table_name,"english_objective"),
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
                ,new GridColumn{
                    name= "officer_id",
                    field= "lk_personal_sub_area_id.head_id",
                    field_order= "lk_personal_sub_area_id.head_id",
                    table_name = "ref_personal_sub_area",
                    table_alias = "lk_personal_sub_area_id",
                    title= @ResxHelper.GetValue("ref_personal_sub_area","head_id"),
                    type= "number",
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
                    qsearch= false,
                    tooltip= "",
                    aggregate= "",
                    footerTemplate= ""
                }
                ,new GridColumn{
                    name= "ehs_head_id",
                    field= "lk_ehs_area_id.head_id",
                    field_order= "lk_ehs_area_id.head_id",
                    table_name = "ref_ehs_area",
                    table_alias = "lk_ehs_area_id",
                    title= @ResxHelper.GetValue("ref_ehs_area","head_id"),
                    type= "number",
                    select= true,
                    display= false,
                    hidden= false,
                    width= "",
                    format= "",
                    attributes= "{ \"style\": \"text-align: left; \" }",
                    template= "",
                    encoded= false,
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
        public static GridData GetListData(HttpContext context, ActionRequest param)
        {
            IList<GridColumn> column_att = GetGridColumn();
            OrderedDictionary parameter = new OrderedDictionary();
            string whereCond = " WHERE 1=1 ";
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            PersonData personData = SecurityHelper.GetPersonData(context);
            if (!(SecurityHelper.HasRule(context, "ManPowerViewAll") || SecurityHelper.HasRule(context, "ManPowerEditAll")))
            {
                if (personData.id != null && personData.id != "")
                {
                    parameter["personData_id"] = personData.id;
                    parameter["personData_psa_id"] = personData.psa_id;
                    whereCond +=
                        " AND " +
                        "   ( " +
                        "       " + _table_name + ".personal_sub_area_id=@personData_psa_id " +
                        "       OR lk_ehs_area_id.head_id= @personData_id" +
                        "       OR lk_personal_sub_area_id.head_id= @personData_id" +
                        "   ) ";

                }

            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------
            if (param.filter_by_column != null)
            {
                foreach (ItemFilterColumn itemFilter in param.filter_by_column)
                {
                    if (itemFilter.value != null && itemFilter.value != "")
                    {

                        if (itemFilter.name == "f_usia_personal")
                        {
                            whereCond += " AND (" +
                                "DATEDIFF(YEAR, " + itemFilter.field + ", GETDATE()) - " +
                                "   CASE " +
                                "       WHEN MONTH(GETDATE()) < MONTH(" + itemFilter.field + ") THEN 1 " +
                                "       WHEN MONTH(GETDATE()) > MONTH(" + itemFilter.field + ") THEN 0 " +
                                "       WHEN DAY(GETDATE()) < DAY(" + itemFilter.field + ") THEN 1 ELSE 0 " +
                                "   END" +
                                ") BETWEEN " + itemFilter.value.Replace("-", " AND ");

                        }
                        else if (itemFilter.name == "f_usia_kerja")
                        {
                            whereCond += " AND (" +
                                "DATEDIFF( YEAR, ta_mp.tgl_masuk_kerja, ISNULL(ta_mp.tgl_akhir_kerja, GETDATE()) ) - " +
                                "   CASE " +
                                "       WHEN MONTH( ISNULL(ta_mp.tgl_akhir_kerja, GETDATE()) ) < MONTH(ta_mp.tgl_masuk_kerja) THEN 1 " +
                                "       WHEN MONTH( ISNULL(ta_mp.tgl_akhir_kerja, GETDATE()) ) > MONTH(ta_mp.tgl_masuk_kerja) THEN 0 " +
                                "       WHEN DAY( ISNULL(ta_mp.tgl_akhir_kerja, GETDATE()) ) < DAY(ta_mp.tgl_masuk_kerja) THEN 1 ELSE 0 " +
                                "   END" +
                                ") BETWEEN " + itemFilter.value.Replace("-", " AND ");

                        }
                        else
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
                            else
                            {
                                whereCond += " AND " + myColumn.field + " " + itemFilter.opr + " @" + itemFilter.name;
                            }
                            parameter[itemFilter.name] = itemFilter.value;
                        }
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
                    data["file_path_photo"] = FileHelper.CopyToBase(_hostingEnvironment, context, _table_name, "file_path_photo", PkValue, data["file_path_photo"].ToString());
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
                    if (data["karyawan_nonkaryawan_id"].ToString() != "1") {
                        data["file_path_photo"] = FileHelper.CopyToBase(_hostingEnvironment, context, _table_name, "file_path_photo", PkValue, data["file_path_photo"].ToString());
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
                            if (parent.opr == "is null" || parent.opr == "is not null")
                            {
                                sql_where += " " + parent.opr + " ";
                            }
                            else {
                                sql_where += "=''";
                            }
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
        public static DataTable GetRiwayatPelatihan(string peserta_id)
        {
            DataTable data = new DataTable();
            OrderedDictionary param = new OrderedDictionary();
            param["peserta_id"] = peserta_id;
            string sql = "";
            data = SqlHelper.GetDataTable(sql, param);
            return data;
        }

    }
}
