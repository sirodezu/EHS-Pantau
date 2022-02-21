using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportSdaListrikModel
    {
        public int ehs_area_id { get; set; }
        public int ba_id { get; set; }
        public int pa_id { get; set; }
        public int psa_id { get; set; }
        public int bulan { get; set; }
        public int tahun { get; set; }
        public int sumber_listrik_id { get; set; }
        public string no_rek_listrik { get; set; }
        public double konsumsi_listrik { get; set; }
        public string tagihan_listrik { get; set; }
        public int usaha_pengurangan_listrik_id { get; set; }
        public string usaha_pengurangan_listrik_desc { get; set; }
        public string usaha_pengurangan_listrik_desc_file_path { get; set; }
        public double usaha_pengurangan_listrik_jumlah { get; set; }
    }
}
