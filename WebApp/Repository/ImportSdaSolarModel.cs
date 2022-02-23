using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportSdaSolarModel
    {
        public string ehs_area_id { get; set; }
        public string ba_id { get; set; }
        public string pa_id { get; set; }
        public string psa_id { get; set; }
        public string bulan { get; set; }
        public int tahun { get; set; }
        public string jenis_bahan_bakar_id { get; set; }
        public string peruntukan_solar_id { get; set; }
        public string sumber_solar_id { get; set; }
        public double konsumsi_solar { get; set; }
        public string tagihan_solar { get; set; }
        public string usaha_pengurangan_solar_id { get; set; }
        public string usaha_pengurangan_solar_desc { get; set; }
        public string usaha_pengurangan_solar_desc_file_path { get; set; }
        public double usaha_pengurangan_solar_jumlah { get; set; }
    }
}
