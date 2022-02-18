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
    public class Lb3Repository
    {
        private string ConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["MainConnection"];
        private IDbConnection db;
        public Lb3Repository()
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
                            (@id
                              ,@ehs_area_id
                              ,@ba_id
                              ,@pa_id
                              ,@psa_id
                              ,@jenis_limbah_dihasilkan_id
                              ,@kode_limbah
                              ,@sumber_limbah_id
                              ,@kegiatan_id
                              ,@tgl_dihasilkan
                              ,@jenis_limbah_dihasilkan
                              ,@pengelolaan_oleh_id
                              ,@masa_simpan_limbah_id
                              ,@tgl_dikeluarkan
                              ,@jumlah_limbah_dikelola
                              ,@kode_manifest
                              ,@perusahaan_angkut_id
                              ,@diserahkan_ke_id
                              ,@perusahaan_serah_id
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
                              ,[timbulan_limbah_cair]
                              ,[timbulan_limbah_padat]
                              ,[deskripsi_limbah]
                              ,[pengelolaan_oleh_id]
                              ,[usaha_kurang_limbah_id]
                              ,[deskripsi_usaha]
                              ,[deskripsi_usaha_file_path]
                              ,[usaha_kurang_limbah_m3]
                              ,[usaha_kurang_limbah_kg]
                              ,[insert_by]
                              ,[insert_at]
                              ,[update_by]
                              ,[update_at])
                        Values
                            (@id
                              ,@ehs_area_id
                              ,@ba_id
                              ,@pa_id
                              ,@psa_id
                              ,@bulan
                              ,@tahun
                              ,@jenis_limbah_id
                              ,@timbulan_limbah_cair
                              ,@timbulan_limbah_padat
                              ,@deskripsi_limbah
                              ,@pengelolaan_oleh_id
                              ,@usaha_kurang_limbah_id
                              ,@deskripsi_usaha
                              ,@deskripsi_usaha_file_path
                              ,@usaha_kurang_limbah_m3
                              ,@usaha_kurang_limbah_kg
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
                importModel.timbulan_limbah_cair,
                importModel.timbulan_limbah_padat,
                importModel.deskripsi_limbah,
                importModel.pengelolaan_oleh_id,
                importModel.usaha_kurang_limbah_id,
                importModel.deskripsi_usaha,
                importModel.deskripsi_usaha_file_path,
                importModel.usaha_kurang_limbah_m3,
                importModel.usaha_kurang_limbah_kg,
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
    }
}
