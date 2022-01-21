using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class HomeModel
    {
        public static DataTable GetCarouselData(HttpContext context)
        {
            string baseUrl = WebApp.WebHelper.GetBaseUrl(context);
            DataTable data = new DataTable();
            data.Clear();
            data.Columns.Add("no");
            data.Columns.Add("id");
            data.Columns.Add("nama_file");
            data.Columns.Add("link_img");
            data.Columns.Add("keterangan");
            string sql = "SELECT top(10) a.[id] " +
                  ", a.[ehs_area_id] " +
                  ",area.kode as area_kode " +
                  ",area.nama as area_nama " +
                  ",a.[ba_id] " +
                  ",ba.kode as ba_kode " +
                  ",ba.nama as ba_nama " +
                  ",a.[pa_id] " +
                  ",pa.kode as pa_kode " +
                  ",pa.nama as pa_nama " +
                  ",a.[psa_id] " +
                  ",psa.kode as psa_kode " +
                  ",psa.nama as psa_nama " +
                  ",a.[customer] " +
                  ",a.[tgl_kegiatan] " +
                  ",b.nama as [jenis_kegiatan_nama] " +
                  ",a.[materi_acara] " +
                  ",a.[keterlibatan_manajemen_id] " +
                  ",dbo.fn_get_lampiran_img([lampiran_kegiatan]) as [lampiran_kegiatan] " +
                  "  FROM[dbo].[ta_cmp] as a " +
            " left outer join ref_literal_data as b on a.jenis_kegiatan_id = b.id and b.cat_id='51' " +
            " left outer join ref_personal_sub_area as psa on a.psa_id = psa.id " +
            " left outer join ref_personal_area as pa on a.pa_id = pa.id " +
            " left outer join ref_business_area as ba on a.ba_id = ba.id " +
            " left outer join ref_ehs_area as area on a.ehs_area_id = area.id " +
            " where a.lampiran_kegiatan like '%.jpg%' OR a.lampiran_kegiatan like '%.png%' " +
            " order by a.[tgl_kegiatan] desc;";
            DataTable dt = SqlHelper.GetDataTable(sql);
            int i = 0;
            string tanggal = "";
            string keterangan = "";
            foreach (DataRow dr in dt.Rows) {
                string[] lampiran_kegiatan = dr["lampiran_kegiatan"].ToString().Split(",");
                foreach (string item in lampiran_kegiatan) {
                    i++;
                    DataRow _ravi = data.NewRow();
                    _ravi["no"] = i.ToString();
                    _ravi["id"] = dr["id"].ToString();
                    _ravi["nama_file"] = item;
                    OrderedDictionary PkValue = new OrderedDictionary();
                    PkValue["id"] = dr["id"].ToString();
                    _ravi["link_img"] = FileHelper.GetlinkImg(baseUrl, "ta_cmp", PkValue, "lampiran_kegiatan", item);
                    tanggal = dr["tgl_kegiatan"].ToString() != "" ? String.Format("{0:dd MMMM yyyy}", dr["tgl_kegiatan"]) : "";
                    keterangan = dr["jenis_kegiatan_nama"].ToString() ;
                    keterangan += tanggal != "" ? " "+ tanggal : "";
                    keterangan += " (PSA:"+dr["psa_nama"].ToString()+")";
                    _ravi["keterangan"] = keterangan;
                    data.Rows.Add(_ravi);
                    //
                    if (i++ >= 10) { goto label_return; }
                }
            }
label_return:
            return data;
        }

    }
    
}
