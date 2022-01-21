using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ChartModel
    {
        public class paramTna {
            public string tahun { get; set; }
            public string ba_id { get; set; }
            public string pa_id { get; set; }
            public string psa_id { get; set; }
            public string tdhead_id { get; set; }
            public string sdh_id { get; set; }
        }
        public static DataTable get_status_tna(HttpContext context,paramTna param) {
            PersonData personData = SecurityHelper.GetPersonData(context);
            string where_on = "";
            if (param.tahun != null) {
                where_on += " AND b.tahun="+ param.tahun;
            }
            if (param.ba_id != null)
            {
                where_on += " AND b.ba_id=" + param.ba_id;
            }
            if (param.pa_id != null)
            {
                where_on += " AND b.pa_id=" + param.pa_id;
            }
            if (param.psa_id != null)
            {
                where_on += " AND b.psa_id=" + param.psa_id;
            }
            if (!SecurityHelper.HasRule(context, "TNAViewAll") && personData.id != null && personData.id != "")
            {
                where_on += " AND (b.sdh_id=" + personData.id + " OR b.tch_id=" + personData.id + " OR b.admin_id=" + personData.id + " )";
            }
            string sql = "select a.id,a.nama,a.bg_color,a.font_color,sum(case when b.status_id = a.id then 1 else 0 end) as jumlah " +
            " from ref_literal as a" +
            " left outer join ta_tna as b" +
            "     on a.id = b.status_id and a.jenis='tna_status' " + where_on +
            " Where a.jenis='tna_status' " +
            " group by a.id,a.nama,a.bg_color,a.font_color " +
            " order by a.id ";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }
        public static DataTable get_status_idp(HttpContext context, paramTna param)
        {
            PersonData personData = SecurityHelper.GetPersonData(context);
            string where = "";
            string where_or = "";
            if (param.tahun != null)
            {
                where += " AND b.tahun=" + param.tahun;
            }
            if (param.ba_id != null)
            {
                where += " AND b.ba_id=" + param.ba_id;
            }
            if (param.pa_id != null)
            {
                where += " AND b.pa_id=" + param.pa_id;
            }
            if (param.psa_id != null)
            {
                where_or += " AND b.psa_id=" + param.psa_id;
            }
            
            if (!SecurityHelper.HasRule(context,"IDPViewAll") && personData.id != null && personData.id != "")
            {
                where_or += " and (";
                where_or += " psa.sdh_id=" + personData.id + "";
                where_or += " or tta.tch_id=" + personData.id + " ";
                where_or += " or tta.admin_id=" + personData.id + " ";
                where_or += " or person.superior_id=" + personData.id + " ";
                where_or += " or b.person_id=" + personData.id + " ";
                where_or += " ) ";
            }
            string sql = "select a.id,a.nama,a.bg_color,a.font_color " +
            " ,sum(case when b.status_id = a.id then 1 else 0 end) as jumlah " +
            " from ref_literal as a " +
            " left outer join (" +
            " select b.id,b.person_id,b.status_id from  ta_idp as b " +
            " left outer join ta_person as person on b.person_id = person.id " + 
            " left outer join ref_personal_sub_area as psa on b.psa_id = psa.id " + 
            " left outer join ref_personal_area as pa on psa.pa_id = pa.id " +
            " left outer join ref_business_area as ba on psa.ba_id = ba.id " +
            " left outer join ref_tta as tta on ba.tta_id = tta.id " +
            " where 1=1 " + where + where_or +" "+
            " ) as b on a.id = b.status_id and a.jenis='idp_status' " +
            " Where a.jenis='idp_status' " +
            " group by a.id,a.nama,a.bg_color,a.font_color " +
            " order by a.id ";
            DataTable data = SqlHelper.GetDataTable(sql);
            return data;
        }
    }
}
