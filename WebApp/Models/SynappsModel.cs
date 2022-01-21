using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SynappsModel
    {

        public static string GetCurrentPeriode()
        {
            string sql = "select dbo.CurrentPeriode() as tahun ";
            int hasil = SqlHelper.ExecuteScalarInt(sql);
            return hasil.ToString();
        }
        public static bool GetAvaliableProses(string tahun, string proses_code)
        {
            string proses_id = "1";
            if (proses_code == "CreateTNA") {
                proses_id = "1";
            }else if (proses_code == "ApprovalTNA"){
                proses_id = "2";
            }
            else if (proses_code == "FinalRMT")
            {
                proses_id = "3";
            }
            else if (proses_code == "CreateIDP")
            {
                proses_id = "4";
            }
            else if (proses_code == "ApprovalIDP")
            {
                proses_id = "5";
            }
            else if (proses_code == "CreateATS")
            {
                proses_id = "6";
            }
            else if (proses_code == "FinalATS")
            {
                proses_id = "7";
            }
            else if (proses_code == "Training")
            {
                proses_id = "8";
            }
            string sql = "select dbo.GetAvaliableProses(" + tahun + "," + proses_id + ")";
            int hasil = SqlHelper.ExecuteScalarInt(sql);
            bool result = hasil == 1 ? true : false;
            return result;
        }
        public static void SetInitPeriode()
        {
            string curperiode = GetCurrentPeriode();
            string sql = "EXECUTE sp_ref_periode_proses_Init " + curperiode + " ";
            SqlHelper.ExecuteNonQuery(sql);
            SetInitTna();
        }
        public static void SetSinkroPeriode()
        {
            string curperiode = GetCurrentPeriode();
            string sql = "EXECUTE sp_ref_periode_proses_sinkro " + curperiode + " ";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void SinkroTna()
        {
            string curperiode = GetCurrentPeriode();
            string sql = "EXECUTE sp_tna_sinkro " + curperiode + ";";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void SetInitTna()
        {
            string curperiode = GetCurrentPeriode();
            string sql = "EXECUTE sp_tna_init "+ curperiode + ";";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void SetClearTna()
        {
            string sql = "execute sp_tna_clear ;";
            SqlHelper.ExecuteNonQuery(sql);
        }

        public static DataTable GetNotifTna(HttpContext context)
        {
            DataTable data = new DataTable();
            PersonData personData = SecurityHelper.GetPersonData(context);
            string sql = "";
            if(SecurityHelper.HasRule(context, "TNAAdd"))
            {
                sql = "select a.id,a.nama,a.keterangan,a.bg_color,a.font_color " +
                " ,sum(case when b.status_id = a.id then 1 else 0 end) as jumlah " +
                " from ref_literal as a " +
                " left outer join ta_tna as b ";
                sql += "     on a.id = b.status_id ";
                if (personData.id != null) {
                    sql += " and b.sdh_id = " + personData.id + " ";
                }
                sql += " where a.jenis = 'tna_status' ";
                sql += " and a.id < 2 ";
                sql += " group by a.id,a.nama,a.keterangan,a.bg_color,a.font_color ";
            }
            if (SecurityHelper.HasRule(context, "TNAApproval"))
            {
                sql += sql != "" ? " UNION " : "";
                sql += "select a.id,a.nama,a.keterangan,a.bg_color,a.font_color " +
                " ,sum(case when b.status_id = a.id then 1 else 0 end) as jumlah " +
                " from ref_literal as a " +
                " left outer join ta_tna as b ";
                sql += "     on a.id = b.status_id ";
                if (personData.id != null)
                {
                    sql += " and b.tch_id = " + personData.id + " ";
                }
                sql += " where a.jenis = 'tna_status' ";
                sql += " and  1 < a.id and a.id < 4 ";
                sql += " group by a.id,a.nama,a.keterangan,a.bg_color,a.font_color ";
            }
            if (sql != "") {
                sql = "select a.* from ("+sql+") as a where a.jumlah>0 ";
                data = SqlHelper.GetDataTable(sql);
            }
            return data;
        }
        public static DataTable GetStatusTna(HttpContext context)
        {
            DataTable data = new DataTable();
            PersonData personData = SecurityHelper.GetPersonData(context);
            string sql = "";
            if (personData.id != null)
            {
                sql = "select a.id,a.nama,a.bg_color,a.font_color " +
                " ,sum(case when b.status_id = a.id then 1 else 0 end) as jumlah " +
                " from ref_literal as a " +
                " left outer join ta_tna as b " +
                "     on a.id = b.status_id and b.sdh_id = " + personData.id + " or b.tch_id = " + personData.id + "  or b.admin_id = " + personData.id + " " +
                " where jenis = 'tna_status' " +
                " group by a.id,a.nama,a.bg_color,a.font_color ";
                data = SqlHelper.GetDataTable(sql);
            }
            else
            {
                sql = "select a.id,a.nama,a.bg_color,a.font_color " +
                " ,sum(case when b.status_id = a.id then 1 else 0 end) as jumlah " +
                " from ref_literal as a " +
                " left outer join ta_tna as b " +
                "     on a.id = b.status_id " +
                " where jenis = 'tna_status' " +
                " group by a.id,a.nama,a.bg_color,a.font_color ";
                data = SqlHelper.GetDataTable(sql);
            }
            return data;
        }

    }
}
