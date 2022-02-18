using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class ImportModel
    {
        public int? ehs_area_id { get; set; }
        public int? ba_id { get; set; }
        public int? pa_id { get; set; }
        public int? psa_id { get; set; }
        public string jenis_limbah_dihasilkan_id { get; set; }
        public string kode_limbah { get; set; }
        public int? sumber_limbah_id { get; set; }
        public int? kegiatan_id { get; set; }
        public DateTime? tgl_dihasilkan { get; set; }
        public double? jenis_limbah_dihasilkan { get; set; }
        public int? pengelolaan_oleh_id { get; set; }
        public int? masa_simpan_limbah_id { get; set; }
        public DateTime? tgl_dikeluarkan { get; set; }
        public double? jumlah_limbah_dikelola { get; set; }
        public string kode_manifest { get; set; }
        public int? perusahaan_angkut_id { get; set; }
        public int? diserahkan_ke_id { get; set; }
        public int? perusahaan_serah_id { get; set; }
        public double? sisa_di_tps { get; set; }
    }
}
