using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportModel
    {
        public string ehs_area_id { get; set; }
        public string ba_id { get; set; }
        public string pa_id { get; set; }
        public string psa_id { get; set; }
        public string jenis_limbah_dihasilkan_id { get; set; }
        public string kode_limbah { get; set; }
        public string sumber_limbah_id { get; set; }
        public string kegiatan_id { get; set; }
        public DateTime tgl_dihasilkan { get; set; }
        public string jenis_limbah_dihasilkan { get; set; }
        public string pengelolaan_oleh_id { get; set; }
        public string masa_simpan_limbah_id { get; set; }
        public DateTime tgl_dikeluarkan { get; set; }
        public string jumlah_limbah_dikelola { get; set; }
        public string kode_manifest { get; set; }
        public string perusahaan_angkut_id { get; set; }
        public string diserahkan_ke_id { get; set; }
        public string perusahaan_serah_id { get; set; }
        public string sisa_di_tps { get; set; }

    }
}
