using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace WebApp.Repository
{
    public class ImportRepository
    {
        private string ConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["MainConnection"];
        private IDbConnection db;
        public ImportRepository()
        {
            db = new SqlConnection(ConnStr);
        }
        public int Import(ImportModel importModel)
        {
            var lastId = getLastId();
            var query = @"INSERT INTO ta_lb3
                            ([id]
							  ,[ehs_area_id]
                              ,[ba_id]
                              ,[pa_id]
                              ,[psa_id]
                              ,[jenis_limbah_dihasilkan_id]
                              ,[kode_limbah]
                              ,[sumber_limbah_id]
                              ,[kegiatan_id]
                              ,[tgl_dihasilkan]
                              ,[jenis_limbah_dihasilkan]
                              ,[pengelolaan_oleh_id]
                              ,[masa_simpan_limbah_id]
                              ,[tgl_dikeluarkan]
                              ,[jumlah_limbah_dikelola]
                              ,[kode_manifest]
                              ,[perusahaan_angkut_id]
                              ,[diserahkan_ke_id]
                              ,[perusahaan_serah_id]
                              ,[sisa_di_tps]
                              ,[insert_by]
                              ,[insert_at]
                              ,[update_by]
                              ,[update_at])
                        Values
                            ((select max (id) + 1 from ta_lb3),(select id from ref_ehs_area where nama = @ehs_area_id)
                              ,(select id from ref_business_area where nama = @ba_id)
                              ,(select id from ref_personal_area where nama = @pa_id)
                              ,(select id from ref_personal_sub_area where nama = @psa_id)
                              ,(select id from ref_literal_data where nama = @jenis_limbah_dihasilkan_id and cat_id = 90)
                              ,@kode_limbah
                              ,(select id from ref_literal_data where nama = @sumber_limbah_id and cat_id = 97)
                              ,(select id from ref_literal_data where nama = @kegiatan_id and cat_id = 91)
                              ,@tgl_dihasilkan
                              ,@jenis_limbah_dihasilkan
                              ,(select id from ref_literal_data where nama = @pengelolaan_oleh_id and cat_id = 93)
                              ,(select id from ref_literal_data where nama = @masa_simpan_limbah_id and cat_id = 92)
                              ,@tgl_dikeluarkan
                              ,@jumlah_limbah_dikelola
                              ,@kode_manifest
                              ,(select id from ref_perusahaan where nama_perusahaan = @perusahaan_angkut_id)
                              ,(select id from ref_lb3_usaha where nama = @diserahkan_ke_id)
                              ,(select id from ref_perusahaan where nama_perusahaan = @perusahaan_serah_id)
                              ,@sisa_di_tps
                              ,1
                              ,getdate()
                              ,1
                              ,getdate())";
            var parameters = new
            {
                id = lastId + 1,
                importModel.ehs_area_id,
                importModel.ba_id,
                importModel.pa_id,
                importModel.psa_id,
                importModel.jenis_limbah_dihasilkan_id,
                importModel.kode_limbah,
                importModel.sumber_limbah_id,
                importModel.kegiatan_id,
                importModel.tgl_dihasilkan,
                importModel.jenis_limbah_dihasilkan,
                importModel.pengelolaan_oleh_id,
                importModel.masa_simpan_limbah_id,
                importModel.tgl_dikeluarkan,
                importModel.jumlah_limbah_dikelola,
                importModel.kode_manifest,
                importModel.perusahaan_angkut_id,
                importModel.diserahkan_ke_id,
                importModel.perusahaan_serah_id,
                importModel.sisa_di_tps,
            };
            db.Open();
            var result = db.Execute(query, parameters);
            db.Close();
            return result;
        }
        private int getLastId()
        {
            var query = "select MAX(id) as id from ta_lb3";
            db.Open();
            var result = db.ExecuteScalar<int>(query);
            db.Close();
            return result;
        }
        public int ImportNonLb3(ImportNonLb3Model importModel)
        {
            var lastId = getLastIdNonLb3();
            var query = @"INSERT INTO ta_nonlb3
                            ([id]
                              ,[ehs_area_id]
                              ,[ba_id]
                              ,[pa_id]
                              ,[psa_id]
                              ,[bulan]
                              ,[tahun]
                              ,[jenis_limbah_id]
                              --,[timbulan_limbah_cair]
                              --,[timbulan_limbah_padat]
                              ,[deskripsi_limbah]
                              ,[pengelolaan_oleh_id]
                              ,[usaha_kurang_limbah_id]
                              ,[deskripsi_usaha]
                              ,[deskripsi_usaha_file_path]
                              --,[usaha_kurang_limbah_m3]
                             -- ,[usaha_kurang_limbah_kg]
                              ,[insert_by]
                              ,[insert_at]
                              ,[update_by]
                              ,[update_at])
                        Values
                            ((select max (id) + 1 from ta_nonlb3),(select id from ref_ehs_area where nama = @ehs_area_id)
                              ,(select id from ref_business_area where nama = @ba_id)
                              ,(select id from ref_personal_area where nama = @pa_id)
                              ,(select id from ref_personal_sub_area where nama = @psa_id)
                              ,(select id from ref_literal_data where cat_id = 150 and nama = @bulan)
                              ,(select id from ref_literal_data where cat_id = 151 and nama = @tahun)
                              ,(select id from ref_literal_data where cat_id = 98 and nama = @jenis_limbah_id)
                              --,@timbulan_limbah_cair
                              --,@timbulan_limbah_padat
                              ,@deskripsi_limbah
                              ,(select id from ref_literal_data where cat_id = 99 and nama = @pengelolaan_oleh_id)
                              ,(select id from ref_literal_data where cat_id = 137 and nama = @usaha_kurang_limbah_id)
                              ,@deskripsi_usaha
                              ,@deskripsi_usaha_file_path
                              --,@usaha_kurang_limbah_m3
                              --,@usaha_kurang_limbah_kg
                              ,1
                              ,getdate()
                              ,1
                              ,getdate())";
            var parameters = new
            {
                id = lastId + 1,
                importModel.ehs_area_id,
                importModel.ba_id,
                importModel.pa_id,
                importModel.psa_id,
                importModel.bulan,
                importModel.tahun,
                importModel.jenis_limbah_id,
                //importModel.timbulan_limbah_cair,
                //importModel.timbulan_limbah_padat,
                importModel.deskripsi_limbah,
                importModel.pengelolaan_oleh_id,
                importModel.usaha_kurang_limbah_id,
                importModel.deskripsi_usaha,
                importModel.deskripsi_usaha_file_path,
                //importModel.usaha_kurang_limbah_m3,
                //importModel.usaha_kurang_limbah_kg,
            };
            db.Open();
            var result = db.Execute(query, parameters);
            db.Close();
            return result;
        }
        private int getLastIdNonLb3()
        {
            var query = "select MAX(id) as id from ta_nonlb3";
            db.Open();
            var result = db.ExecuteScalar<int>(query);
            db.Close();
            return result;
        }
        public int ImportSdaAir(ImportSdaAirModel importModel)
        {
            var lastId = getLastIdSdaAir();
            var query = @"INSERT INTO ta_sda_air
                            ([id]
                              ,[ehs_area_id]
                              ,[ba_id]
                              ,[pa_id]
                              ,[psa_id]
                              ,[bulan]
                              ,[tahun]
                              ,[sumber_air_id]
                              ,[no_rek_air]
                              ,[konsumsi_air]
                              ,[tagihan_air]
                              ,[usaha_pengurangan_air_id]
                              ,[usaha_pengurangan_air_desc]
                              ,[usaha_pengurangan_air_desc_file_path]
                              ,[usaha_pengurangan_air_jumlah]
                              ,[insert_at])
                        Values
                            ((select max (id) + 1 from ta_sda_air)
							  ,(select id from ref_ehs_area where nama = @ehs_area_id)
                              ,(select id from ref_business_area where nama = @ba_id)
                              ,(select id from ref_personal_area where nama = @pa_id)
                              ,(select id from ref_personal_sub_area where nama = @psa_id)
                              ,(select id from ref_literal_data where cat_id = 150 and nama = @bulan)
                              ,(select id from ref_literal_data where cat_id = 151 and nama = @tahun)
                              ,(select id from ref_literal_data where cat_id = 124 and nama = @sumber_air_id)
                              ,@no_rek_air
                              ,@konsumsi_air
                              ,@tagihan_air
                              ,(select id from ref_literal_data where cat_id = 127 and nama = @usaha_pengurangan_air_id)
                              ,@usaha_pengurangan_air_desc
                              ,@usaha_pengurangan_air_desc_file_path
                              ,@usaha_pengurangan_air_jumlah
                              ,getdate())";
            var parameters = new
            {
                id = lastId + 1,
                importModel.ehs_area_id,
                importModel.ba_id,
                importModel.pa_id,
                importModel.psa_id,
                importModel.bulan,
                importModel.tahun,
                importModel.sumber_air_id,
                importModel.no_rek_air,
                importModel.konsumsi_air,
                importModel.tagihan_air,
                importModel.usaha_pengurangan_air_id,
                importModel.usaha_pengurangan_air_desc,
                importModel.usaha_pengurangan_air_desc_file_path,
                importModel.usaha_pengurangan_air_jumlah,
            };
            db.Open();
            var result = db.Execute(query, parameters);
            db.Close();
            return result;
        }
        private int getLastIdSdaAir()
        {
            var query = "select MAX(id) as id from ta_sda_air";
            db.Open();
            var result = db.ExecuteScalar<int>(query);
            db.Close();
            return result;
        }
        public int ImportSdaKertas(ImportSdaKertasModel importModel)
        {
            var lastId = getLastIdSdaKertas();
            var query = @"INSERT INTO ta_sda_kertas
                            ([id]
                              ,[ehs_area_id]
                              ,[ba_id]
                              ,[pa_id]
                              ,[psa_id]
                              ,[bulan]
                              ,[tahun]
                              ,[konsumsi_kertas]
                              ,[usaha_pengurangan_kertas_id]
                              ,[usaha_pengurangan_kertas_desc]
                              ,[usaha_pengurangan_kertas_desc_file_path]
                              ,[usaha_pengurangan_kertas_jumlah]
                              ,[insert_at])
                        Values
                            ((select max (id) + 1 from ta_sda_kertas)
							  ,(select id from ref_ehs_area where nama = @ehs_area_id)
                              ,(select id from ref_business_area where nama = @ba_id)
                              ,(select id from ref_personal_area where nama = @pa_id)
                              ,(select id from ref_personal_sub_area where nama = @psa_id)
                              ,(select id from ref_literal_data where cat_id = 150 and nama = @bulan)
                              ,(select id from ref_literal_data where cat_id = 151 and nama = @tahun)
                              ,@konsumsi_kertas
                              ,(select id from ref_literal_data where cat_id = 128 and nama = @usaha_pengurangan_kertas_id)
                              ,@usaha_pengurangan_kertas_desc
                              ,@usaha_pengurangan_kertas_desc_file_path
                              ,@usaha_pengurangan_kertas_jumlah
                              ,getdate())";
            var parameters = new
            {
                id = lastId + 1,
                importModel.ehs_area_id,
                importModel.ba_id,
                importModel.pa_id,
                importModel.psa_id,
                importModel.bulan,
                importModel.tahun,
                importModel.konsumsi_kertas,
                importModel.usaha_pengurangan_kertas_id,
                importModel.usaha_pengurangan_kertas_desc,
                importModel.usaha_pengurangan_kertas_desc_file_path,
                importModel.usaha_pengurangan_kertas_jumlah,
            };
            db.Open();
            var result = db.Execute(query, parameters);
            db.Close();
            return result;
        }
        private int getLastIdSdaKertas()
        {
            var query = "select MAX(id) as id from ta_sda_kertas";
            db.Open();
            var result = db.ExecuteScalar<int>(query);
            db.Close();
            return result;
        }
        public int ImportSdaListrik(ImportSdaListrikModel importModel)
        {
            var lastId = getLastIdSdaListrik();
            var query = @"INSERT INTO ta_sda_listrik
                            ([id]
                              ,[ehs_area_id]
                              ,[ba_id]
                              ,[pa_id]
                              ,[psa_id]
                              ,[bulan]
                              ,[tahun]
                              ,[sumber_listrik_id]
                              ,[no_rek_listrik]
                              ,[konsumsi_listrik]
                              ,[tagihan_listrik]
                              ,[usaha_pengurangan_listrik_id]
                              ,[usaha_pengurangan_listrik_desc]
                              ,[usaha_pengurangan_listrik_desc_file_path]
                              ,[usaha_pengurangan_listrik_jumlah]
                              ,[insert_at])
                        Values
                            ((select max (id) + 1 from ta_sda_listrik)
							  ,(select id from ref_ehs_area where nama = @ehs_area_id)
                              ,(select id from ref_business_area where nama = @ba_id)
                              ,(select id from ref_personal_area where nama = @pa_id)
                              ,(select id from ref_personal_sub_area where nama = @psa_id)
                              ,(select id from ref_literal_data where cat_id = 150 and nama = @bulan)
                              ,(select id from ref_literal_data where cat_id = 151 and nama = @tahun)
                              ,@no_rek_listrik
                              ,@konsumsi_listrik
                              ,@tagihan_listrik
                              ,@usaha_pengurangan_listrik_id
                              ,@usaha_pengurangan_listrik_desc
                              ,@usaha_pengurangan_listrik_desc_file_path
                              ,@usaha_pengurangan_listrik_jumlah
                              ,getdate())";
            var parameters = new
            {
                id = lastId + 1,
                importModel.ehs_area_id,
                importModel.ba_id,
                importModel.pa_id,
                importModel.psa_id,
                importModel.bulan,
                importModel.tahun,
                importModel.sumber_listrik_id,
                importModel.no_rek_listrik,
                importModel.konsumsi_listrik,
                importModel.tagihan_listrik,
                importModel.usaha_pengurangan_listrik_id,
                importModel.usaha_pengurangan_listrik_desc,
                importModel.usaha_pengurangan_listrik_desc_file_path,
                importModel.usaha_pengurangan_listrik_jumlah,
            };
            db.Open();
            var result = db.Execute(query, parameters);
            db.Close();
            return result;
        }
        private int getLastIdSdaListrik()
        {
            var query = "select MAX(id) as id from ta_sda_listrik";
            db.Open();
            var result = db.ExecuteScalar<int>(query);
            db.Close();
            return result;
        }
    }
}
