using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportSdaAirModel
    {
        public int ehs_area_id { get; set; }
        public int ba_id { get; set; }
        public int pa_id { get; set; }
        public int psa_id { get; set; }
        public int bulan { get; set; }
        public int tahun { get; set; }
        public int sumber_air_id { get; set; }
        public string no_rek_air { get; set; }
        public double konsumsi_air { get; set; }
        public string tagihan_air { get; set; }
        public int usaha_pengurangan_air_id { get; set; }
        public string usaha_pengurangan_air_desc { get; set; }
        public string usaha_pengurangan_air_desc_file_path { get; set; }
        public double usaha_pengurangan_air_jumlah { get; set; }
    }
}
