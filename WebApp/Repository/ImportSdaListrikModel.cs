using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportSdaListrikModel
    {
        public string ehs_area_id { get; set; }
        public string ba_id { get; set; }
        public string pa_id { get; set; }
        public string psa_id { get; set; }
        public string bulan { get; set; }
        public int tahun { get; set; }
        public string sumber_listrik_id { get; set; }
        public string no_rek_listrik { get; set; }
        public double konsumsi_listrik { get; set; }
        public string tagihan_listrik { get; set; }
        public int usaha_pengurangan_listrik_id { get; set; }
        public string usaha_pengurangan_listrik_desc { get; set; }
        public string usaha_pengurangan_listrik_desc_file_path { get; set; }
        public double usaha_pengurangan_listrik_jumlah { get; set; }
    }
}
