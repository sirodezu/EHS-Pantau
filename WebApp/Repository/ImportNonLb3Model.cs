using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportNonLb3Model
    {
        public int ehs_area_id { get; set; }
        public int ba_id { get; set; }
        public int pa_id { get; set; }
        public int psa_id { get; set; }
        public int bulan { get; set; }
        public int tahun { get; set; }
        public string jenis_limbah_id { get; set; }
        public double timbulan_limbah_cair { get; set; }
        public double timbulan_limbah_padat { get; set; }
        public string deskripsi_limbah { get; set; }
        public int pengelolaan_oleh_id { get; set; }
        public int usaha_kurang_limbah_id { get; set; }
        public string deskripsi_usaha { get; set; }
        public string deskripsi_usaha_file_path { get; set; }
        public double usaha_kurang_limbah_m3 { get; set; }
        public double usaha_kurang_limbah_kg { get; set; }
    }
}
