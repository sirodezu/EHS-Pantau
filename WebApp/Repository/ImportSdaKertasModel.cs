using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportSdaKertasModel
    {
        public int ehs_area_id { get; set; }
        public int ba_id { get; set; }
        public int pa_id { get; set; }
        public int psa_id { get; set; }
        public int bulan { get; set; }
        public int tahun { get; set; }
        public double konsumsi_kertas { get; set; }
        public int usaha_pengurangan_kertas_id { get; set; }
        public string usaha_pengurangan_kertas_desc { get; set; }
        public string usaha_pengurangan_kertas_desc_file_path { get; set; }
        public double usaha_pengurangan_kertas_jumlah { get; set; }
    }
}
