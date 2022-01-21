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
    public class ProfileModel
    {
        public static string _table_name = "ta_profile";
        public static string[] _pkKey = { "id" };
        public static int auto_increament = 1;
        public static int increament_type = 0;
        public static string[] _unKey = {  };
        public static string _columnOrder = "ta_profile.id DESC";
        public static string _displayColumn = "id";
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
            public string psa_id { get; set; }
            public string psa_id_text { get; set; }
            public string date_start { get; set; }
            public string dt_date_start { get; set; }
            public string date_finish { get; set; }
            public string dt_date_finish { get; set; }
        }

        public static DataSet GetValidData(string date_begin, string date_end, string ehs_area_id, string ba_id, string pa_id, string psa_id)
        {
            DataSet ds = new DataSet();
            if (date_begin != "" & date_end != "")
            {
                string sqlCommand = "EXEC dbo.spGetChartData @DATE_BEGIN = '" + date_begin + "', @DATE_END = '" + date_end + "', @EHS_AREA_ID = '" + ehs_area_id + "', @BA_ID = '" + ba_id + "', @PA_ID = '" + pa_id + "', @PSA_ID = '" + psa_id + "' ";
                ds = SqlHelper.GetDataSetFromSP(sqlCommand);
            }
            return ds;
        }
    }
}
