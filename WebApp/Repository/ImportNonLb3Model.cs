using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportNonLb3Model
    {
        public string ehs_area_id { get; set; }
        public string ba_id { get; set; }
        public string pa_id { get; set; }
        public string psa_id { get; set; }
        public string bulan { get; set; }
        public int tahun { get; set; }
        public string jenis_limbah_id { get; set; }
        //public double timbulan_limbah_cair { get; set; }
        //public double timbulan_limbah_padat { get; set; }
        public string deskripsi_limbah { get; set; }
        public string pengelolaan_oleh_id { get; set; }
        public string usaha_kurang_limbah_id { get; set; }
        public string deskripsi_usaha { get; set; }
        public string deskripsi_usaha_file_path { get; set; }
        //public double usaha_kurang_limbah_m3 { get; set; }
        //public double usaha_kurang_limbah_kg { get; set; }
    }
}
