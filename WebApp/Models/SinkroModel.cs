using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SinkroModel
    {
        public class ObjPerson
        {
            public string nrp { get; set; }
            public string nama { get; set; }
            public string aktif { get; set; }
            public string job_id { get; set; }
            public string job_title { get; set; }
            public string position_id { get; set; }
            public string position_nama { get; set; }
            public string ba_kode { get; set; }
            public string ba_nama { get; set; }
            public string pa_kode { get; set; }
            public string pa_nama { get; set; }
            public string psa_kode { get; set; }
            public string psa_nama { get; set; }
            //public string spv_nrp { get; set; }
            //public string spv_nama { get; set; }
            //public string sdh_nrp { get; set; }
            //public string sdh_nama { get; set; }
            //public string tch_area { get; set; }
            //public string tch_nrp { get; set; }
            //public string tch_nama { get; set; }

            public string tgl_masuk_kerja { get; set; }
            public string tgl_akhir_kerja { get; set; }

        }
        public class ObjPersonSpesialisasi
        {
            public string MANDT { get; set; }
            public string PERNR { get; set; }
            public string SUBTY { get; set; }
            public string OBJPS { get; set; }
            public string SPRPS { get; set; }
            public string ENDDA { get; set; }
            public string BEGDA { get; set; }
            public string SEQNR { get; set; }
            public string AEDTM { get; set; }
            public string UNAME { get; set; }
            public string HISTO { get; set; }
            public string ITXEX { get; set; }
            public string REFEX { get; set; }
            public string ORDEX { get; set; }
            public string ITBLD { get; set; }
            public string PREAS { get; set; }
            public string FLAG1 { get; set; }
            public string FLAG2 { get; set; }
            public string FLAG3 { get; set; }
            public string FLAG4 { get; set; }
            public string RESE1 { get; set; }
            public string RESE2 { get; set; }
            public string GRPVL { get; set; }
            public string SPC { get; set; }
            public string SPCTEXT { get; set; }
            public string SPCSTATUS { get; set; }
            public string VALIDFROM { get; set; }
            public string VALIDTO { get; set; }
        }
        public class ObjPersonQualification
        {
            public string PLVAR { get; set; }
            public string OTYPE { get; set; }
            public string SOBID { get; set; }
            public string SHORT { get; set; }
            public string STEXT { get; set; }
            public string MARK_X { get; set; }
            public string TTYPE { get; set; }
            public string TBJID { get; set; }
            public string SBEGD { get; set; }
            public string SENDD { get; set; }
            public string VBEGD { get; set; }
            public string VENDD { get; set; }
            public string ISTAT { get; set; }
            public string NOTE { get; set; }
            public string TSHORT { get; set; }
            public string TTEXT { get; set; }
            public string PROFCY { get; set; }
            public string PROFCY2 { get; set; }
            public string ESSENTIAL { get; set; }
            public string NOT_RATED { get; set; }
            public string INHER_TYPE { get; set; }
            public string INHER_ID { get; set; }
            public string AEDTM { get; set; }
            public string UNAME { get; set; }
            public string CLASS_ID { get; set; }
            public string CLASS_TEXT { get; set; }
            public string SCALE_ID { get; set; }
            public string SCALE_TEXT { get; set; }
            public string PROFC_TEXT { get; set; }
            public string PROF2_TEXT { get; set; }
        }
        public class ObjPersonQualificationValid
        {
            public string MANDT { get; set; }
            public string OTYPE { get; set; }
            public string OBJID { get; set; }
            public string PLVAR { get; set; }
            public string RSIGN { get; set; }
            public string RELAT { get; set; }
            public string ISTAT { get; set; }
            public string PRIOX { get; set; }
            public string BEGDA { get; set; }
            public string ENDDA { get; set; }
            public string VARYF { get; set; }
            public string SEQNR { get; set; }
            public string VALIDTO { get; set; }
            public string SCLAS { get; set; }
            public string SOBID { get; set; }
        }
        public static ProsesResult get_person()
        {
            ProsesResult result = new ProsesResult();
            string path_source = Settings.GetAppSetting("path_data_sources");
            string path_archive = Settings.GetAppSetting("path_data_archive");
            if (Directory.Exists(path_source))
            {
                string[] files = Directory.GetFiles(path_source, "OBJ_P*.csv");
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        read_person(files[i]);
                        //string file_name = Path.GetFileName(files_person[i]);
                    }
                    result.status = 1;
                    result.title = "";
                    result.message = ResxHelper.GetValue("Message", "Proses sinkronsisasi berhasil");
                }
                else
                {
                    result.status = 2;
                    result.title = "ErrorMessage";
                    result.message = ResxHelper.GetValue("Message", "File Sumber Tidak ditemukan");
                }

            }
            else
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "Folder Sumber Tidak ditemukan");
            }

            return result;
        }
        public static void read_person(string file_path)
        {
            string path_archive = Settings.GetAppSetting("path_data_archive");
            string file_name = Path.GetFileName(file_path);
            string real_table = "SAP_PERSON";
            string temp_table = "TMP_PERSON";
            List<ObjPerson> result;

            if (File.Exists(file_path))
            {
                using (TextReader fileReader = File.OpenText(file_path))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    result = csv.GetRecords<ObjPerson>().ToList();
                }
                //DROP TABLE IF EXISTS
                string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
                SqlHelper.ExecuteNonQuery(sql);
                foreach (ObjPerson item in result)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["nrp"] = item.nrp;
                    data["nama"] = item.nama;
                    data["aktif"] = item.aktif;
                    data["job_id"] = item.job_id;
                    data["job_title"] = item.job_title;
                    data["position_id"] = item.position_id;
                    data["position_nama"] = item.position_nama;
                    data["ba_kode"] = item.ba_kode;
                    data["ba_nama"] = item.ba_nama;
                    data["pa_kode"] = item.pa_kode;
                    data["pa_nama"] = item.pa_nama;
                    data["psa_kode"] = item.psa_kode;
                    data["psa_nama"] = item.psa_nama;
                    if (item.tgl_masuk_kerja != null && item.tgl_masuk_kerja != "")
                    {
                        string[] tgl_masuk_kerja = item.tgl_masuk_kerja.Split(".");
                        data["tgl_masuk_kerja"] = tgl_masuk_kerja[2] + "-" + tgl_masuk_kerja[1] + "-" + tgl_masuk_kerja[0];
                    }
                    else {
                        data["tgl_masuk_kerja"] = null;
                    }
                    if (item.tgl_masuk_kerja != null && item.tgl_masuk_kerja != "")
                    {
                        string[] tgl_akhir_kerja = item.tgl_akhir_kerja.Split(".");
                        data["tgl_akhir_kerja"] = tgl_akhir_kerja[2] + "-" + tgl_akhir_kerja[1] + "-" + tgl_akhir_kerja[0];
                    }
                    else {
                        data["tgl_akhir_kerja"] = null;
                    }

                    //data["spv_nrp"] = item.spv_nrp;
                    //data["spv_nama"] = item.spv_nama;
                    //data["sdh_nrp"] = item.sdh_nrp;
                    //data["sdh_nama"] = item.sdh_nama;
                    //data["tch_area"] = item.tch_area;
                    //data["tch_nrp"] = item.tch_nrp;
                    //data["tch_nama"] = item.tch_nama;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    string sql_field = "";
                    string sql_value = "";
                    foreach (DictionaryEntry entry in data)
                    {
                        sql_field += sql_field != "" ? "," : "";
                        sql_field += entry.Key;
                        sql_value += sql_value != "" ? "," : "";
                        sql_value += "@" + entry.Key;
                    }
                    sql = "INSERT INTO " + temp_table + " (" + sql_field + ")  VALUES(" + sql_value + ");  ";
                    SqlHelper.ExecuteNonQuery(sql, data);
                }
                sql = "EXECUTE read_obj_person ";
                SqlHelper.ExecuteNonQuery(sql);
                //===================================
                if (!Directory.Exists(path_archive))
                {
                    Directory.CreateDirectory(path_archive);
                }
                string file_dest = Path.Combine(path_archive, file_name);
                if (File.Exists(file_dest)) {
                    File.Delete(file_dest);
                }
                File.Move(file_path, file_dest);
            }
        }
        public static ProsesResult get_person_spesialisasi()
        {
            ProsesResult result = new ProsesResult();
            string path_source = Settings.GetAppSetting("path_data_sources");
            string path_archive = Settings.GetAppSetting("path_data_archive");
            if (Directory.Exists(path_source))
            {
                string[] files = Directory.GetFiles(path_source, "REL_P_S*.csv");
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        read_person_spesialisasi(files[i]);
                    }
                    result.status = 1;
                    result.title = "";
                    result.message = ResxHelper.GetValue("Message", "Proses sinkronsisasi berhasil");
                }
                else {
                    result.status = 2;
                    result.title = "ErrorMessage";
                    result.message = ResxHelper.GetValue("Message", "File Sumber Tidak ditemukan");
                }

            }
            else
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "Folder Sumber Tidak ditemukan");
            }

            return result;
        }
        public static void read_person_spesialisasi(string file_path)
        {
            string path_archive = Settings.GetAppSetting("path_data_archive");
            string file_name = Path.GetFileName(file_path);
            string real_table = "SAP_PERSON_SPESIALISASI";
            string temp_table = "TMP_PERSON_SPESIALISASI";
            List<ObjPersonSpesialisasi> result;

            if (File.Exists(file_path))
            {
                using (TextReader fileReader = File.OpenText(file_path))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    result = csv.GetRecords<ObjPersonSpesialisasi>().ToList();
                }
                //DROP TABLE IF EXISTS
                string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
                SqlHelper.ExecuteNonQuery(sql);
                foreach (ObjPersonSpesialisasi item in result)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["MANDT"] = item.MANDT;
                    data["PERNR"] = item.PERNR;
                    data["SUBTY"] = item.SUBTY;
                    data["OBJPS"] = item.OBJPS;
                    data["SPRPS"] = item.SPRPS;
                    if (item.ENDDA.Length == 10 && item.ENDDA != "00.00.0000")
                    {
                        data["ENDDA"] = item.ENDDA.Substring(6, 4) + "-" + item.ENDDA.Substring(3, 2) + "-" + item.ENDDA.Substring(0, 2);
                    }
                    else {
                        data["ENDDA"] = "";
                    }
                    if (item.BEGDA.Length == 10 && item.BEGDA != "00.00.0000")
                    {
                        data["BEGDA"] = item.BEGDA.Substring(6, 4) + "-" + item.BEGDA.Substring(3, 2) + "-" + item.BEGDA.Substring(0, 2);
                    }
                    else {
                        data["BEGDA"] = "";
                    }
                    data["SEQNR"] = item.SEQNR;
                    if (item.AEDTM.Length == 10 && item.AEDTM != "00.00.0000")
                    {
                        data["AEDTM"] = item.AEDTM.Substring(6, 4) + "-" + item.AEDTM.Substring(3, 2) + "-" + item.AEDTM.Substring(0, 2);
                    }
                    else
                    {
                        data["AEDTM"] = "";
                    }
                    data["UNAME"] = item.UNAME;
                    data["HISTO"] = item.HISTO;
                    data["ITXEX"] = item.ITXEX;
                    data["REFEX"] = item.REFEX;
                    data["ORDEX"] = item.ORDEX;
                    data["ITBLD"] = item.ITBLD;
                    data["PREAS"] = item.PREAS;
                    data["FLAG1"] = item.FLAG1;
                    data["FLAG2"] = item.FLAG2;
                    data["FLAG3"] = item.FLAG3;
                    data["FLAG4"] = item.FLAG4;
                    data["RESE1"] = item.RESE1;
                    data["RESE2"] = item.RESE2;
                    data["GRPVL"] = item.GRPVL;
                    data["SPC"] = item.SPC;
                    data["SPCTEXT"] = item.SPCTEXT;
                    data["SPCSTATUS"] = item.SPCSTATUS;
                    if (item.VALIDFROM.Length == 10 && item.VALIDFROM != "00.00.0000")
                    {
                        data["VALIDFROM"] = item.VALIDFROM.Substring(6, 4) + "-" + item.VALIDFROM.Substring(3, 2) + "-" + item.VALIDFROM.Substring(0, 2);
                    }
                    else
                    {
                        data["VALIDFROM"] = "";
                    }
                    if (item.VALIDTO.Length == 10 && item.VALIDTO != "00.00.0000")
                    {
                        data["VALIDTO"] = item.VALIDTO.Substring(6, 4) + "-" + item.VALIDTO.Substring(3, 2) + "-" + item.VALIDTO.Substring(0, 2);
                    }
                    else
                    {
                        data["VALIDTO"] = "";
                    }
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    string sql_field = "";
                    string sql_value = "";
                    foreach (DictionaryEntry entry in data)
                    {
                        sql_field += sql_field != "" ? "," : "";
                        sql_field += entry.Key;
                        sql_value += sql_value != "" ? "," : "";
                        sql_value += "@" + entry.Key;
                    }
                    sql = "INSERT INTO " + temp_table + " (" + sql_field + ")  VALUES(" + sql_value + ");  ";
                    SqlHelper.ExecuteNonQuery(sql, data);
                }
                sql = "EXECUTE read_obj_person_spesialisasi ";
                SqlHelper.ExecuteNonQuery(sql);
                //===================================
                if (!Directory.Exists(path_archive))
                {
                    Directory.CreateDirectory(path_archive);
                }
                string file_dest = Path.Combine(path_archive, file_name);
                if (File.Exists(file_dest))
                {
                    File.Delete(file_dest);
                }
                File.Move(file_path, file_dest);
            }
        }
        public static ProsesResult get_person_qualification()
        {
            ProsesResult result = new ProsesResult();
            string path_source = Settings.GetAppSetting("path_data_sources");
            string path_archive = Settings.GetAppSetting("path_data_archive");
            if (Directory.Exists(path_source))
            {
                string[] files = Directory.GetFiles(path_source, "REL_P_QL*.csv");
                if (files.Length > 0) {
                    for (int i = 0; i < files.Length; i++)
                    {
                        read_person_qualification(files[i]);
                    }
                    result.status = 1;
                    result.title = "";
                    result.message = ResxHelper.GetValue("Message", "Proses sinkronsisasi berhasil");
                }
                else
                {
                    result.status = 2;
                    result.title = "ErrorMessage";
                    result.message = ResxHelper.GetValue("Message", "File Sumber Tidak ditemukan");
                }

            }
            else
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "Folder Sumber Tidak ditemukan");
            }

            return result;
        }
        public static void read_person_qualification(string file_path)
        {
            string path_archive = Settings.GetAppSetting("path_data_archive");
            string file_name = Path.GetFileName(file_path);
            string real_table = "SAP_PERSON_QUALIFICATION";
            string temp_table = "TMP_PERSON_QUALIFICATION";
            List<ObjPersonQualification> result;

            if (File.Exists(file_path))
            {
                using (TextReader fileReader = File.OpenText(file_path))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    result = csv.GetRecords<ObjPersonQualification>().ToList();
                }
                //DROP TABLE IF EXISTS
                string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
                SqlHelper.ExecuteNonQuery(sql);
                foreach (ObjPersonQualification item in result)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["PLVAR"] = item.PLVAR;
                    data["OTYPE"] = item.OTYPE;
                    data["SOBID"] = item.SOBID;
                    data["SHORT"] = item.SHORT;
                    data["STEXT"] = item.STEXT;
                    data["MARK_X"] = item.MARK_X;
                    data["TTYPE"] = item.TTYPE;
                    data["TBJID"] = item.TBJID;
                    if (item.SBEGD.Length == 8 && item.SBEGD != "00000000")
                    {
                        data["SBEGD"] = item.SBEGD.Substring(0, 4) + "-" + item.SBEGD.Substring(4, 2) + "-" + item.SBEGD.Substring(6, 2);
                    }
                    else
                    {
                        data["SBEGD"] = "";
                    }
                    if (item.SENDD.Length == 8 && item.SENDD != "00000000")
                    {
                        data["SENDD"] = item.SENDD.Substring(0, 4) + "-" + item.SENDD.Substring(4, 2) + "-" + item.SENDD.Substring(6, 2);
                    }
                    else
                    {
                        data["SENDD"] = "";
                    }
                    if (item.VBEGD.Length == 8 && item.VBEGD != "00000000")
                    {
                        data["VBEGD"] = item.VBEGD.Substring(0, 4) + "-" + item.VBEGD.Substring(4, 2) + "-" + item.VBEGD.Substring(6, 2);
                    }
                    else
                    {
                        data["VBEGD"] = "";
                    }
                    if (item.VENDD.Length == 8 && item.VENDD != "00000000")
                    {
                        data["VENDD"] = item.VENDD.Substring(0, 4) + "-" + item.VENDD.Substring(4, 2) + "-" + item.VENDD.Substring(6, 2);
                    }
                    else
                    {
                        data["VENDD"] = "";
                    }
                    data["ISTAT"] = item.ISTAT;
                    data["NOTE"] = item.NOTE;
                    data["TSHORT"] = item.TSHORT;
                    data["TTEXT"] = item.TTEXT;
                    data["PROFCY"] = item.PROFCY;
                    data["PROFCY2"] = item.PROFCY2;
                    data["ESSENTIAL"] = item.ESSENTIAL;
                    data["NOT_RATED"] = item.NOT_RATED;
                    data["INHER_TYPE"] = item.INHER_TYPE;
                    data["INHER_ID"] = item.INHER_ID;
                    if (item.AEDTM.Length == 8 && item.AEDTM != "00000000")
                    {
                        data["AEDTM"] = item.AEDTM.Substring(0, 4) + "-" + item.AEDTM.Substring(4, 2) + "-" + item.AEDTM.Substring(6, 2);
                    }
                    else
                    {
                        data["AEDTM"] = "";
                    }
                    data["UNAME"] = item.UNAME;
                    data["CLASS_ID"] = item.CLASS_ID;
                    data["CLASS_TEXT"] = item.CLASS_TEXT;
                    data["SCALE_ID"] = item.SCALE_ID;
                    data["SCALE_TEXT"] = item.SCALE_TEXT;
                    data["PROFC_TEXT"] = item.PROFC_TEXT;
                    data["PROF2_TEXT"] = item.PROF2_TEXT;
                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    string sql_field = "";
                    string sql_value = "";
                    foreach (DictionaryEntry entry in data)
                    {
                        sql_field += sql_field != "" ? "," : "";
                        sql_field += entry.Key;
                        sql_value += sql_value != "" ? "," : "";
                        sql_value += "@" + entry.Key;
                    }
                    sql = "INSERT INTO " + temp_table + " (" + sql_field + ")  VALUES(" + sql_value + ");  ";
                    SqlHelper.ExecuteNonQuery(sql, data);
                }
                sql = "EXECUTE read_obj_person_qualification ";
                SqlHelper.ExecuteNonQuery(sql);

                //===================================
                if (!Directory.Exists(path_archive))
                {
                    Directory.CreateDirectory(path_archive);
                }
                string file_dest = Path.Combine(path_archive, file_name);
                if (File.Exists(file_dest))
                {
                    File.Delete(file_dest);
                }
                File.Move(file_path, file_dest);
            }
        }
        public static ProsesResult get_person_qualification_valid()
        {
            ProsesResult result = new ProsesResult();
            string path_source = Settings.GetAppSetting("path_data_sources");
            string path_archive = Settings.GetAppSetting("path_data_archive");
            if (Directory.Exists(path_source))
            {
                string[] files = Directory.GetFiles(path_source, "REL_P_QV*.csv");
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        read_person_qualification_valid(files[i]);
                    }
                    result.status = 1;
                    result.title = "";
                    result.message = ResxHelper.GetValue("Message", "Proses sinkronsisasi berhasil");
                }
                else
                {
                    result.status = 2;
                    result.title = "ErrorMessage";
                    result.message = ResxHelper.GetValue("Message", "File Sumber Tidak ditemukan");
                }

            }
            else
            {
                result.status = 2;
                result.title = ResxHelper.GetValue("Message", "ErrorMessage");
                result.message = ResxHelper.GetValue("Message", "Folder Sumber Tidak ditemukan");
            }

            return result;
        }
        public static void read_person_qualification_valid(string file_path)
        {
            string path_archive = Settings.GetAppSetting("path_data_archive");
            string file_name = Path.GetFileName(file_path);
            string real_table = "SAP_PERSON_QUALIFICATION_VALID";
            string temp_table = "TMP_PERSON_QUALIFICATION_VALID";
            List<ObjPersonQualificationValid> result;

            if (File.Exists(file_path))
            {
                using (TextReader fileReader = File.OpenText(file_path))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    result = csv.GetRecords<ObjPersonQualificationValid>().ToList();
                }
                //DROP TABLE IF EXISTS
                string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
                SqlHelper.ExecuteNonQuery(sql);
                foreach (ObjPersonQualificationValid item in result)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["MANDT"] = item.MANDT;
                    data["OTYPE"] = item.OTYPE;
                    data["OBJID"] = item.OBJID;
                    data["PLVAR"] = item.PLVAR;
                    data["RSIGN"] = item.RSIGN;
                    data["RELAT"] = item.RELAT;
                    data["ISTAT"] = item.ISTAT;
                    data["PRIOX"] = item.PRIOX;
                    if (item.BEGDA.Length == 10 && item.BEGDA != "00.00.0000")
                    {
                        data["BEGDA"] = item.BEGDA.Substring(6, 4) + "-" + item.BEGDA.Substring(3, 2) + "-" + item.BEGDA.Substring(0, 2);
                    }
                    else
                    {
                        data["BEGDA"] = "";
                    }
                    if (item.ENDDA.Length == 10 && item.ENDDA != "00.00.0000")
                    {
                        data["ENDDA"] = item.ENDDA.Substring(6, 4) + "-" + item.ENDDA.Substring(3, 2) + "-" + item.ENDDA.Substring(0, 2);
                    }
                    else
                    {
                        data["ENDDA"] = "";
                    }
                    data["VARYF"] = item.VARYF;
                    data["SEQNR"] = item.SEQNR;
                    if (item.VALIDTO.Length == 10 && item.VALIDTO != "00.00.0000")
                    {
                        data["VALIDTO"] = item.VALIDTO.Substring(6, 4) + "-" + item.VALIDTO.Substring(3, 2) + "-" + item.VALIDTO.Substring(0, 2);
                    }
                    else
                    {
                        data["VALIDTO"] = "";
                    }
                    data["SCLAS"] = item.SCLAS;
                    data["SOBID"] = item.SOBID;

                    SqlHelper.ConvertEmptyValuesToDBNull(data);
                    string sql_field = "";
                    string sql_value = "";
                    foreach (DictionaryEntry entry in data)
                    {
                        sql_field += sql_field != "" ? "," : "";
                        sql_field += entry.Key;
                        sql_value += sql_value != "" ? "," : "";
                        sql_value += "@" + entry.Key;
                    }
                    sql = "INSERT INTO " + temp_table + " (" + sql_field + ")  VALUES(" + sql_value + ");  ";
                    SqlHelper.ExecuteNonQuery(sql, data);
                }
                sql = "EXECUTE read_obj_person_qualification_valid ";
                SqlHelper.ExecuteNonQuery(sql);
                //===================================
                if (!Directory.Exists(path_archive))
                {
                    Directory.CreateDirectory(path_archive);
                }
                string file_dest = Path.Combine(path_archive, file_name);
                if (File.Exists(file_dest))
                {
                    File.Delete(file_dest);
                }
                File.Move(file_path, file_dest);
            }
        }
    }
}
