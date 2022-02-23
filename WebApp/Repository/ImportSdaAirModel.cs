using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportSdaAirModel
    {
        public string ehs_area_id { get; set; }
        public string ba_id { get; set; }
        public string pa_id { get; set; }
        public string psa_id { get; set; }
        public string bulan { get; set; }
        public int tahun { get; set; }
        public string sumber_air_id { get; set; }
        public string no_rek_air { get; set; }
        public double konsumsi_air { get; set; }
        public string tagihan_air { get; set; }
        public string usaha_pengurangan_air_id { get; set; }
        public string usaha_pengurangan_air_desc { get; set; }
        public string usaha_pengurangan_air_desc_file_path { get; set; }
        public double usaha_pengurangan_air_jumlah { get; set; }
    }
}
