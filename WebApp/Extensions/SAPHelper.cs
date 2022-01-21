using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class SAPHelper
    {
        private static string pathSource = @"D:\Projects\2019\UT\SYNAPSE\Dokumen\Development\MasterData\";
        public static void sinkro_group_event() {
            string sql = "INSERT INTO [dbo].[ref_group_event] ([tt_id],[gt_id],[kode],[nama],[aktif_id],[tgl_mulai],[tgl_akhir],[sap_otype],[sap_objid],[sap_short],[sap_stext]) " +
            " select " +
            " 1 as tt_id " +
            " ,1 as gt_id " +
            " ,b.SHORT as kode " +
            " ,b.STEXT as nama " +
            " ,case when b.[ENDDA]='9999-12-31' then 1 else 0 end as aktif_id  " +
            " , b.BEGDA as tgl_mulai " +
            " , b.ENDDA as tgl_akhir " +
            " ,b.OTYPE as otype " +
            " ,b.OBJID as objid " +
            " ,b.SHORT " +
            " ,b.STEXT " +
            " from[dbo].[SAP_MTR] as a " +
            " left outer join SAP_OBJ_L as b " +
            " on a.OTYPE=b.OTYPE " +
            " AND a.OBJID=b.OBJID " +
            " where level_id=2 " +
            " order by nama ";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void sinkro_event_type()
        {
            string sql = "INSERT INTO [dbo].[ref_event_type] ([tt_id],[gt_id],[ge_id],[kode],[nama],[aktif_id],[tgl_mulai],[tgl_akhir],[sap_otype],[sap_objid],[sap_otjid],[sap_short],[sap_stext]) " +
            " select distinct " +
            " 1 as tt_id " +
            " ,1 as gt_id " +
            " ,g.id as ge_id " +
            " ,a.SHORT as kode " +
            " ,a.STEXT as nama " +
            " ,case when a.ENDDA = '9999-12-31' then 1 else 0 end as aktif_id " +
            " ,a.BEGDA as tgl_mulai " +
            " ,a.ENDDA as tgl_akhir " +
            " ,a.OTYPE as sap_otype " +
            " ,a.OBJID as sap_objid " +
            " ,a.OTJID as sap_otjid " +
            " ,a.SHORT as sap_short " +
            " ,a.STEXT as sap_stext " +
            " from[SAP_OBJ_D] as a " +
            " INNER JOIN( " +
            "     select OBJID, max(ENDDA) as ENDDA from[SAP_OBJ_D] group by OBJID " +
            " ) as lastd " +
            "     ON a.OBJID = lastd.OBJID and a.ENDDA = lastd.ENDDA " +
            " INNER JOIN[SAP_MAP_TR] as b " +
            "     on a.OTYPE = b.OTYPE " +
            "     and a.OBJID = b.OBJID " +
            "     and a.ENDDA = b.ENDDA " +
            " left outer join ref_group_event as g " +
            "     on b.otype2 = g.sap_otype " +
            "     and b.objid2 = g.sap_objid ";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void sinkro_location()
        {
            string sql = "insert into ref_location (kode,nama,aktif_id,tgl_mulai,tgl_akhir,sap_otype,sap_objid,[sap_short],[sap_stext]) "
            + " select "
            + " a.SHORT as kode "
            + " , a.STEXT as nama "
            + " ,case when a.[ENDDA]='9999-12-31' then 1 else 0 end as aktif_id  " 
            + " , a.BEGDA as tgl_mulai " 
            + " , a.ENDDA as tgl_akhir "
            + " , a.OTYPE "
            + " , a.OBJID  "
            + " a.SHORT "
            + " , a.STEXT"
            + " from[dbo].[SAP_OBJ_F] as a "
            + " left outer join ref_location as b on a.SHORT=b.kode "
            + " where a.[ENDDA] ='9999-12-31' "
            + " and b.kode is null ";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void sinkro_room()
        {
            string sql = "insert into ref_room (loc_id,kode,nama,aktif_id,tgl_mulai,tgl_akhir,sap_otype,sap_objid,[sap_short],[sap_stext])  "
            + " select distinct  "
            + " c.id as loc_id  "
            + " , a.SHORT as kode  "
            + " , a.STEXT as nama  "
            + " ,case when a.[ENDDA]='9999-12-31' then 1 else 0 end as aktif_id  "
            + " , a.BEGDA as tgl_mulai "
            + " , a.ENDDA as tgl_akhir "
            + " , a.OTYPE "
            + " , a.OBJID  "
            + " a.SHORT "
            + " , a.STEXT"
            + " from[SAP_G] as a "
            + " left outer join[SAP_REL_F_G] as b "
            + "   on a.OTYPE=b.SCLAS "
            + "   and a.OBJID=b.SOBID "
            + " left outer join[ref_location] as c "
            + "  on b.OTYPE=c.OTYPE "
            + "  and b.OBJID=c.OBJID "
            + " left outer join ref_room as d on a.SHORT=d.kode "
            + " where a.[ENDDA] ='9999-12-31' "
            + " and b.[ENDDA]= '9999-12-31' "
            + " and d.kode is null";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void sinkro_qualification()
        {
            string sql = "INSERT INTO [dbo].[ref_qualification]([kode],[nama],[aktif_id],[tgl_mulai],[tgl_akhir],[sap_otype],[sap_objid],[sap_otjid],[sap_short],[sap_stext],[insert_by],[insert_at],[update_by],[update_at]) " +
            " select distinct " +
            "  a.SHORT as kode " +
            "  ,a.STEXT as nama " +
            "  ,case when a.ENDDA = '9999-12-31' then 1 else 0 end as aktif_id " +
            "  ,a.BEGDA as tgl_mulai " +
            "  ,a.ENDDA as tgl_akhir " +
            "  ,a.OTYPE as sap_otype " +
            "  ,a.OBJID as sap_objid " +
            "  ,a.OTJID as sap_otjid " +
            "  ,a.SHORT as sap_short " +
            "  ,a.STEXT as sap_stext " +
            "  ,-1 as insert_by " +
            "  ,CURRENT_TIMESTAMP as insert_at " +
            "  ,-1 as update_by " +
            "  ,CURRENT_TIMESTAMP as update_at " +
            "  from[SAP_OBJ_Q] as a " +
            "  INNER JOIN( " +
            "      select OBJID, max(ENDDA) as ENDDA from[SAP_OBJ_Q] group by OBJID " +
            "  ) as lastd " +
            "      ON a.OBJID = lastd.OBJID and a.ENDDA = lastd.ENDDA " +
            " order by a.OBJID ";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public static void sinkro_event_type_qualification()
        {
            string sql = "INSERT INTO [dbo].[ref_event_type_qualification] ([tt_id],[gt_id],[ge_id],[et_id],[ql_id],[aktif_id],[tgl_mulai],[tgl_akhir],[insert_by],[insert_at],[update_by],[update_at]) " +
            " select distinct " +
            " b.tt_id " +
            " ,b.gt_id " +
            " ,b.ge_id " +
            " ,b.id as et_id " +
            " ,c.id as ql_id " +
            " ,case when a.ENDDA = '9999-12-31' then 1 else 0 end as aktif_id " +
            " ,a.BEGDA as tgl_mulai " +
            " ,a.ENDDA as tgl_akhir " +
            " ,-1 as insert_by " +
            " ,CURRENT_TIMESTAMP as insert_at " +
            " ,-1 as update_by " +
            " ,CURRENT_TIMESTAMP as update_at " +
            " from( " +
            "     select " +
            "     OTYPE, OBJID, SCLAS, SOBID, max(ENDDA) as ENDDA " +
            "     from sap_reL_D_Q " +
            "     where RSIGN = 'A' " +
            "     group by OTYPE, OBJID, SCLAS, SOBID " +
            " ) as m " +
            " INNER join SAP_REL_D_Q as a " +
            "     on m.OTYPE = a.OTYPE " +
            "     and m.OBJID = a.OBJID " +
            "     AND m.SCLAS = a.SCLAS " +
            "     and m.SOBID = a.SOBID " +
            "     and m.ENDDA = a.ENDDA " +
            " INNER join ref_event_type as b " +
            "     on m.OTYPE = b.sap_otype " +
            "     and m.OBJID = b.sap_objid " +
            " INNER join ref_qualification as c " +
            "     on m.SCLAS = c.sap_otype " +
            "     and m.SOBID = c.sap_objid ";
            SqlHelper.ExecuteNonQuery(sql);
        }
        public class SapObjData
        {
            public string MANDT { get; set; }
            public string PLVAR { get; set; }
            public string OTYPE { get; set; }
            public string OBJID { get; set; }
            public string ISTAT { get; set; }
            public string BEGDA { get; set; }
            public string ENDDA { get; set; }
            public string LANGU { get; set; }
            public string SEQNR { get; set; }
            public string OTJID { get; set; }
            public string INFTY { get; set; }
            public string AEDTM { get; set; }
            public string UNAME { get; set; }
            public string REASN { get; set; }
            public string HISTO { get; set; }
            public string ITXNR { get; set; }
            public string SHORT { get; set; }
            public string STEXT { get; set; }
            public string GDATE { get; set; }
            public string MC_SHORT { get; set; }
            public string MC_STEXT { get; set; }
            public string MC_SEARK { get; set; }
        }
        public class SapRelData
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
            public string INFTY { get; set; }
            public string OTJID { get; set; }
            public string SUBTY { get; set; }
            public string AEDTM { get; set; }
            public string UNAME { get; set; }
            public string REASN { get; set; }
            public string HISTO { get; set; }
            public string ITXNR { get; set; }
            public string SCLAS { get; set; }
            public string SOBID { get; set; }
            public string PROZT { get; set; }
            public string ADATANR { get; set; }
        }
        public static void GetDataObject(string objName)
        {
            string real_table = "SAP_OBJ_"+ objName;
            string temp_table = "TMP_OBJ_"+ objName;
            List<SapObjData> result;
            string filename = pathSource + "SAP_OBJ_"+ objName + ".csv";
            if (File.Exists(filename)) {
                using (TextReader fileReader = File.OpenText(filename))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    result = csv.GetRecords<SapObjData>().ToList();
                }
                //DROP TABLE IF EXIXT
                string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
                SqlHelper.ExecuteNonQuery(sql);
                foreach (SapObjData item in result)
                {
                    OrderedDictionary data = new OrderedDictionary();
                    data["MANDT"] = item.MANDT;
                    data["PLVAR"] = item.PLVAR;
                    data["OTYPE"] = item.OTYPE;
                    data["OBJID"] = item.OBJID;
                    data["ISTAT"] = item.ISTAT;
                    data["BEGDA"] = item.BEGDA.Substring(6,4) + "-" + item.BEGDA.Substring(3, 2) + "-" + item.BEGDA.Substring(0, 2);
                    data["ENDDA"] = item.ENDDA.Substring(6, 4) + "-" + item.ENDDA.Substring(3, 2) + "-" + item.ENDDA.Substring(0, 2);
                    data["LANGU"] = item.LANGU;
                    data["SEQNR"] = item.SEQNR;
                    data["OTJID"] = item.OTJID;
                    data["INFTY"] = item.INFTY;
                    data["AEDTM"] = item.AEDTM.Substring(6, 4) + "-" + item.AEDTM.Substring(3, 2) + "-" + item.AEDTM.Substring(0, 2);
                    data["UNAME"] = item.UNAME;
                    data["REASN"] = item.REASN;
                    data["HISTO"] = item.HISTO;
                    data["ITXNR"] = item.ITXNR;
                    data["SHORT"] = item.SHORT;
                    data["STEXT"] = item.STEXT;
                    data["GDATE"] = item.GDATE;
                    data["MC_SHORT"] = item.MC_SHORT;
                    data["MC_STEXT"] = item.MC_STEXT;
                    data["MC_SEARK"] = item.MC_SEARK;
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
                sql = "insert into " + real_table + " " +
                " select a.* from " + temp_table + " as a " +
                " left outer join " + real_table + " as b " +
                "     on a.[MANDT]=b.[MANDT] " +
                " and a.[PLVAR]=b.[PLVAR] " +
                " and a.[OTYPE]=b.[OTYPE] " +
                " and a.[OBJID]=b.[OBJID] " +
                " and a.[ISTAT]=b.[ISTAT] " +
                " and a.[BEGDA]=b.[BEGDA] " +
                " and a.[ENDDA]=b.[ENDDA] " +
                " where b.[ENDDA] is null ";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "DROP TABLE " + temp_table + ";";
                //SqlHelper.ExecuteNonQuery(sql);
            }
            
        }
        public static void GetDataRelation(string objName)
        {
            string real_table = "SAP_REL_"+ objName;
            string temp_table = "TMP_REL_" + objName;
            List<SapRelData> result;

            string filename = pathSource + "SAP_REL_"+ objName + ".csv";
            if (File.Exists(filename)) {
                using (TextReader fileReader = File.OpenText(filename))
                {
                    var csv = new CsvReader(fileReader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    result = csv.GetRecords<SapRelData>().ToList();
                }
                string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
                SqlHelper.ExecuteNonQuery(sql);
                foreach (SapRelData item in result)
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
                    data["BEGDA"] = item.BEGDA.Substring(6, 4) + "-" + item.BEGDA.Substring(3, 2) + "-" + item.BEGDA.Substring(0, 2);
                    data["ENDDA"] = item.ENDDA.Substring(6, 4) + "-" + item.ENDDA.Substring(3, 2) + "-" + item.ENDDA.Substring(0, 2);
                    data["VARYF"] = item.VARYF;
                    data["SEQNR"] = item.SEQNR;
                    data["INFTY"] = item.INFTY;
                    data["OTJID"] = item.OTJID;
                    data["SUBTY"] = item.SUBTY;
                    data["AEDTM"] = item.AEDTM.Substring(6, 4) + "-" + item.AEDTM.Substring(3, 2) + "-" + item.AEDTM.Substring(0, 2);
                    data["UNAME"] = item.UNAME;
                    data["REASN"] = item.REASN;
                    data["HISTO"] = item.HISTO;
                    data["ITXNR"] = item.ITXNR;
                    data["SCLAS"] = item.SCLAS;
                    data["SOBID"] = item.SOBID;
                    data["PROZT"] = item.PROZT;
                    data["ADATANR"] = item.ADATANR;
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
                sql = "insert into " + real_table + " " +
                " select a.* from " + temp_table + " as a " +
                " left outer join " + real_table + " as b " +
                "     on a.[MANDT]=b.[MANDT] " +
                " and a.[OBJID]=b.[OBJID] " +
                " and a.[PLVAR]=b.[PLVAR] " +
                " and a.[RSIGN]=b.[RSIGN] " +
                " and a.[RELAT]=b.[RELAT] " +
                " and a.[ISTAT]=b.[ISTAT] " +
                " and a.[PRIOX]=b.[PRIOX] " +
                " and a.[BEGDA]=b.[BEGDA] " +
                " and a.[ENDDA]=b.[ENDDA] " +
                " and a.[SEQNR]=b.[SEQNR] " +
                " and a.[AEDTM]=b.[AEDTM] " +
                " and a.[UNAME]=b.[UNAME] " +
                " and a.[SCLAS]=b.[SCLAS] " +
                " and a.[SOBID]=b.[SOBID] " +
                " where b.[ENDDA] is null ";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "DROP TABLE " + temp_table + ";";
                //SqlHelper.ExecuteNonQuery(sql);
            }
            
        }

        public static void GetDataPerson()
        {
            string path_source = Settings.GetAppSetting("path_data_sources");
            string path_archive = Settings.GetAppSetting("path_data_archive");
            if (Directory.Exists(path_source)) {
                string[] files = Directory.GetFiles(path_source, "OBJ_PERSON_*.csv");
                //for (int i = 0; i < files.Length; i++) {
                //    files[i] = Path.GetFileName(files[i]);
                //}
            }
            //string real_table = "SAP_OBJ_" + objName;
            //string temp_table = "TMP_OBJ_" + objName;
            //List<SapObjData> result;
            //string filename = pathSource + "SAP_OBJ_" + objName + ".csv";
            //if (File.Exists(filename))
            //{
            //    using (TextReader fileReader = File.OpenText(filename))
            //    {
            //        var csv = new CsvReader(fileReader);
            //        csv.Configuration.HasHeaderRecord = false;
            //        csv.Read();
            //        result = csv.GetRecords<SapObjData>().ToList();
            //    }
            //    //DROP TABLE IF EXIXT
            //    string sql = "IF OBJECT_ID('dbo." + temp_table + "', 'U') IS NOT NULL  DROP TABLE dbo." + temp_table + ";";
            //    SqlHelper.ExecuteNonQuery(sql);
            //    sql = "SELECT * into " + temp_table + " FROM " + real_table + " where 0=1";
            //    SqlHelper.ExecuteNonQuery(sql);
            //    foreach (SapObjData item in result)
            //    {
            //        OrderedDictionary data = new OrderedDictionary();
            //        data["MANDT"] = item.MANDT;
            //        data["PLVAR"] = item.PLVAR;
            //        data["OTYPE"] = item.OTYPE;
            //        data["OBJID"] = item.OBJID;
            //        data["ISTAT"] = item.ISTAT;
            //        data["BEGDA"] = item.BEGDA.Substring(6, 4) + "-" + item.BEGDA.Substring(3, 2) + "-" + item.BEGDA.Substring(0, 2);
            //        data["ENDDA"] = item.ENDDA.Substring(6, 4) + "-" + item.ENDDA.Substring(3, 2) + "-" + item.ENDDA.Substring(0, 2);
            //        data["LANGU"] = item.LANGU;
            //        data["SEQNR"] = item.SEQNR;
            //        data["OTJID"] = item.OTJID;
            //        data["INFTY"] = item.INFTY;
            //        data["AEDTM"] = item.AEDTM.Substring(6, 4) + "-" + item.AEDTM.Substring(3, 2) + "-" + item.AEDTM.Substring(0, 2);
            //        data["UNAME"] = item.UNAME;
            //        data["REASN"] = item.REASN;
            //        data["HISTO"] = item.HISTO;
            //        data["ITXNR"] = item.ITXNR;
            //        data["SHORT"] = item.SHORT;
            //        data["STEXT"] = item.STEXT;
            //        data["GDATE"] = item.GDATE;
            //        data["MC_SHORT"] = item.MC_SHORT;
            //        data["MC_STEXT"] = item.MC_STEXT;
            //        data["MC_SEARK"] = item.MC_SEARK;
            //        SqlHelper.ConvertEmptyValuesToDBNull(data);
            //        string sql_field = "";
            //        string sql_value = "";
            //        foreach (DictionaryEntry entry in data)
            //        {
            //            sql_field += sql_field != "" ? "," : "";
            //            sql_field += entry.Key;
            //            sql_value += sql_value != "" ? "," : "";
            //            sql_value += "@" + entry.Key;
            //        }
            //        sql = "INSERT INTO " + temp_table + " (" + sql_field + ")  VALUES(" + sql_value + ");  ";
            //        SqlHelper.ExecuteNonQuery(sql, data);
            //    }
            //    sql = "insert into " + real_table + " " +
            //    " select a.* from " + temp_table + " as a " +
            //    " left outer join " + real_table + " as b " +
            //    "     on a.[MANDT]=b.[MANDT] " +
            //    " and a.[PLVAR]=b.[PLVAR] " +
            //    " and a.[OTYPE]=b.[OTYPE] " +
            //    " and a.[OBJID]=b.[OBJID] " +
            //    " and a.[ISTAT]=b.[ISTAT] " +
            //    " and a.[BEGDA]=b.[BEGDA] " +
            //    " and a.[ENDDA]=b.[ENDDA] " +
            //    " where b.[ENDDA] is null ";
            //    SqlHelper.ExecuteNonQuery(sql);
            //    sql = "DROP TABLE " + temp_table + ";";
            //    //SqlHelper.ExecuteNonQuery(sql);
            //}

        }
        #region Random data
        // Generate a random number between two numbers    
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static void GeneratePSAMT()
        {
            string sql = "select id as psa_id from ref_personal_sub_area";
            DataTable dtPSA = SqlHelper.GetDataTable(sql);
            foreach (DataRow drpsa in dtPSA.Rows) {
                sql = "select top 10 id as mt_id  from ref_machine_type where aktif_id=1 ORDER BY NEWID() ";
                DataTable dtMT = SqlHelper.GetDataTable(sql);
                foreach (DataRow drmt in dtMT.Rows)
                {
                    string jml = RandomNumber(0, 10).ToString();
                    sql = "insert into ref_personal_sub_area_mt (psa_id,mt_id,jumlah) values ("+ drpsa["psa_id"].ToString() + "," + drmt["mt_id"].ToString() + ","+jml+")";
                    SqlHelper.ExecuteNonQuery(sql);
                }
            }
        }

        #endregion
    }
}
