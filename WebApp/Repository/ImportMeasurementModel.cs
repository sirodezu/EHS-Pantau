using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportMeasurementModel
    {
        public string ehs_area_id { get; set; }
        public string ba_id { get; set; }
        public string pa_id { get; set; }
        public string psa_id { get; set; }
        public string jenis_pemeriksaan_pengujian_id { get; set; }
        public string keterangan_series_kode { get; set; }
        public int jumlah_titik_penaatan { get; set; }
        public string periode_pemeriksaan_pengujian_id { get; set; }
        public string status_penaatan_id { get; set; }
        public double hasil_pengukuran { get; set; }
        public DateTime tanggal_pengujian { get; set; }
        public string file_path_hasil_uji { get; set; }
        public string file_path_baku_mutu { get; set; }
        public string keterangan_uji { get; set; }
    }
}
