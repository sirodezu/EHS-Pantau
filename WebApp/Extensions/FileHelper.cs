using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace WebApp
{
    public class FileHelper
    {
        public class initialFiles
        {
            public string name { get; set; }
            public string extension { get; set; }
            public long size { get; set; }
        }
        public static string[] GetinitialFiles(IHostingEnvironment _hostingEnvironment, HttpContext context, string tbl_name, string col_name, OrderedDictionary PkValue, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            IList<FileHelper.initialFiles> _initialFiles = new List<FileHelper.initialFiles>();
            string new_val = "";
            string initfiles = "";
            if (col_val != "")
            {
                string[] col_vals = col_val.Split(',');
                foreach (string item_file in col_vals)
                {
                    string pathSource = Path.Combine(_hostingEnvironment.ContentRootPath, "data");
                    pathSource = Path.Combine(pathSource, tbl_name);
                    foreach (DictionaryEntry entry in PkValue)
                    {
                        pathSource = Path.Combine(pathSource, entry.Value.ToString());
                    }
                    pathSource = Path.Combine(pathSource, col_name);
                    string file_source = Path.Combine(pathSource, item_file);
                    if (System.IO.File.Exists(file_source))
                    {
                        new_val += new_val != "" ? "," : "";
                        new_val += item_file;
                        string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
                        string pathSession = Path.Combine(upload_temp, context.Session.Id);
                        string pathDest = Path.Combine(pathSession, tbl_name);
                        pathDest = Path.Combine(pathDest, col_name);
                        FileHelper.CreateDirectoryRecursively(pathDest);
                        string file_Dest = Path.Combine(pathDest, item_file);
                        if (System.IO.File.Exists(file_Dest)) { System.IO.File.Delete(file_Dest); }
                        System.IO.File.Copy(file_source, file_Dest);
                        FileInfo fi = new FileInfo(file_Dest);
                        FileHelper.initialFiles initialFiles = new FileHelper.initialFiles();
                        initialFiles.name = fi.Name;
                        initialFiles.extension = fi.Extension;
                        initialFiles.size = fi.Length;
                        _initialFiles.Add(initialFiles);
                    }
                }
                //initfiles = JsonConvert.SerializeObject(_initialFiles);
            }
            initfiles = JsonConvert.SerializeObject(_initialFiles);
            string[] result = { new_val, initfiles };
            return result;
        }
        public static string CopyToBase(IHostingEnvironment _hostingEnvironment, HttpContext context, string tbl_name, string col_name, OrderedDictionary PkValue, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string new_file = "";
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string pathSession = Path.Combine(upload_temp, context.Session.Id);
            string pathSource = Path.Combine(pathSession, tbl_name);
            pathSource = Path.Combine(pathSource, col_name);
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            pathDest = Path.Combine(pathDest, tbl_name);
            foreach (DictionaryEntry entry in PkValue)
            {
                pathDest = Path.Combine(pathDest, entry.Value.ToString());
            }
            pathDest = Path.Combine(pathDest, col_name);
            if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
            if (col_val != "")
            {
                if (!Directory.Exists(pathDest)) { CreateDirectoryRecursively(pathDest); }
                string[] fileNames = col_val.Split(',');
                IList<string> fileNew = new List<string>();
                foreach (string itemFile in fileNames)
                {
                    if (!fileNew.Contains(itemFile))
                    {
                        string fileSource = Path.Combine(pathSource, itemFile);
                        string fileDest = Path.Combine(pathDest, itemFile);
                        if (System.IO.File.Exists(fileDest)) { System.IO.File.Delete(fileDest); }
                        if (System.IO.File.Exists(fileSource))
                        {
                            System.IO.File.Copy(fileSource, fileDest);
                            new_file += new_file != "" ? "," : "";
                            new_file += itemFile;
                            fileNew.Add(itemFile);
                        }

                    }
                }
            }
            return new_file;
        }
        public static void DeleteFromBase(IHostingEnvironment _hostingEnvironment, HttpContext context, string tbl_name, OrderedDictionary PkValue)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            pathDest = Path.Combine(pathDest, tbl_name);
            foreach (DictionaryEntry entry in PkValue)
            {
                pathDest = Path.Combine(pathDest, entry.Value.ToString());
            }
            if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
        }
        public static void CreateDirectoryRecursively(string path)
        {
            string[] pathParts = path.Split('\\');

            for (int i = 0; i < pathParts.Length; i++)
            {
                if (i > 0)
                    pathParts[i] = Path.Combine(pathParts[i - 1], pathParts[i]);

                if (!Directory.Exists(pathParts[i]))
                    Directory.CreateDirectory(pathParts[i]);
            }
        }
        public static void DeleteOldSessionFolder()
        {
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string[] dir = Directory.GetDirectories(upload_temp);
            foreach (string itemDir in dir)
            {
                string pathItem = Path.Combine(upload_temp, itemDir);
                DateTime lastCreate = Directory.GetLastWriteTime(pathItem);
                int beda = (DateTime.Now - lastCreate).Hours;
                if (beda > 2)
                {
                    Directory.Delete(pathItem, true);
                }
            }
        }
        public static string GetParamDownload(string tbl_name, OrderedDictionary PkValue, string col_name, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string pk = "";
            foreach (DictionaryEntry entry in PkValue)
            {
                pk += pk != "" ? "," : "";
                pk += entry.Value.ToString();
            }
            string param = tbl_name + ";" + pk + ";" + col_name + ";" + col_val;
            string encriptlink = SecurityHelper.EncryptString(param);
            return encriptlink;
        }
        public static string GetParamDownload(string tbl_name, string PkValue, string col_name, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string param = tbl_name + ";" + PkValue + ";" + col_name + ";" + col_val;
            string encriptlink = SecurityHelper.EncryptString(param);
            return encriptlink;
        }
        public static string GetlinkImg(string baseUrl, string tbl_name, OrderedDictionary PkValue, string col_name, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string listLink = "";
            if (col_val != null && col_val != "")
            {
                string[] colvals = col_val.Split(',');
                if (colvals.Length == 1 && col_val != null && col_val != "")
                {
                    string qs_param = GetParamDownload(tbl_name, PkValue, col_name, col_val);
                    string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                    listLink = baseUrl + "/Download?id=" + encode_param;
                }
            }
            return listLink;
        }
        public static string GetViewImg(string baseUrl, string tbl_name, OrderedDictionary PkValue, string col_name, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string listLink = "";
            if (col_val != null && col_val != "")
            {
                string[] colvals = col_val.Split(',');
                if (colvals.Length == 1)
                {
                    string qs_param = GetParamDownload(tbl_name, PkValue, col_name, col_val);
                    string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                    string link = baseUrl + "/Download?id=" + encode_param;
                    listLink = "<a href=\"" + link + "\" target=\"_blank\"><img src=\"" + link + "\" width=\"100\" title=\"" + col_val + "\" /></a>";
                }
                else
                {
                    foreach (string item in colvals)
                    {
                        string qs_param = GetParamDownload(tbl_name, PkValue, col_name, item);
                        string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                        string link = baseUrl + "/Download?id=" + encode_param;
                        listLink += listLink != "" ? "<br/>" : "";
                        listLink += "<a href=\"" + link + "\" target=\"_blank\"><img src=\"" + link + "\" width=\"100\" title=\"" + item + "\" /></a>";
                    }

                }
            }
            return listLink;
        }
        public static string GetlinkDownload(string baseUrl, string tbl_name, OrderedDictionary PkValue, string col_name, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string[] colvals = col_val.Split(',');
            string listLink = "";
            if (colvals.Length == 1)
            {
                string qs_param = GetParamDownload(tbl_name, PkValue, col_name, col_val);
                string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                string link = baseUrl + "/Download?id=" + encode_param;
                listLink = "<a href=\"" + link + "\" target=\"_blank\">" + col_val + "</a>";
            }
            else
            {
                foreach (string item in colvals)
                {
                    string qs_param = GetParamDownload(tbl_name, PkValue, col_name, item);
                    string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                    string link = baseUrl + "/Download?id=" + encode_param;
                    listLink += listLink != "" ? "<br/>" : "";
                    listLink += "<a href=\"" + link + "\" target=\"_blank\">" + item + "</a>";
                }

            }
            return listLink;
        }
        public static string GetlinkDownload(string baseUrl, string tbl_name, string PkValue, string col_name, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string[] colvals = col_val.Split(',');
            string listLink = "";
            if (colvals.Length == 1)
            {
                string link = baseUrl + "/Download?id=" + GetParamDownload(tbl_name, PkValue, col_name, col_val);
                listLink = "<a href=\"" + link + "\" target=\"_blank\">" + col_val + "</a>";
            }
            else
            {
                foreach (string item in colvals)
                {
                    string link = baseUrl + "/Download?id=" + GetParamDownload(tbl_name, PkValue, col_name, item);
                    listLink += listLink != "" ? "<br/>" : "";
                    listLink += "<a href=\"" + link + "\" target=\"_blank\">" + item + "</a>";
                }

            }
            return listLink;
        }


        public static string GetModuleName(string tbl_name)
        {
            string moduleName = string.Empty;
            moduleName = tbl_name;
            /*
            switch (tbl_name.ToLower())
            {
                case "ref_area_type":
                    moduleName = "Area";
                    break;
                case "ref_business_area":
                    moduleName = "Business Area";
                    break;
                case "ref_company":
                    moduleName = "Company";
                    break;
                case "ref_compcat":
                    moduleName = "Company Category";
                    break;
                case "ref_comptype":
                    moduleName = "Company Type";
                    break;
                case "ref_ehs_area":
                    moduleName = "EHS Area";
                    break;
                case "ref_ehs_area_head":
                    moduleName = "EHS Area Head";
                    break;
                case "ref_hari_libur":
                    moduleName = "Hari Libur";
                    break;
                case "ref_hari_libur_tetap":
                    moduleName = "Hari Libur Tetap";
                    break;
                case "ref_jabatan":
                    moduleName = "Jabatan";
                    break;
                case "ref_jabatan_position":
                    moduleName = "Position";
                    break;
                case "ref_jenis_bangunan":
                    moduleName = "Jenis Bangunan";
                    break;
                case "ref_jenis_kegiatan":
                    moduleName = "Jenis Kegiatan";
                    break;
                case "ref_jenis_perizinan":
                    moduleName = "Jenis Perizinan";
                    break;
                case "ref_job":
                    moduleName = "Job";
                    break;
                case "ref_kabupaten":
                    moduleName = "Kabupaten";
                    break;
                case "ref_kategori_perizinan":
                    moduleName = "Kategori Perizinan";
                    break;
                case "ref_kecamatan":
                    moduleName = "Kecamatan";
                    break;
                case "ref_lb3_pemanfaat":
                    moduleName = "Perusahaan Pemanfaat Limbah";
                    break;
                case "ref_lb3_pemanfaat_dokumen":
                    moduleName = "Dokumen Pemanfaat Limbah";
                    break;
                case "ref_lb3_pengangkut":
                    moduleName = "Perusahaan Pengangkut Limbah";
                    break;
                case "ref_lb3_pengangkut_dokumen":
                    moduleName = "Dokumen Pengangkut Limbah";
                    break;
                case "ref_lb3_pengolah":
                    moduleName = "Perusahaan Pengolah Limbah";
                    break;
                case "ref_lb3_pengolah_dokumen":
                    moduleName = "Dokumen Pengolah Limbah";
                    break;
                case "ref_lb3_pengumpul":
                    moduleName = "Perusahaan Pengumpul Limbah";
                    break;
                case "ref_lb3_pengumpul_dokumen":
                    moduleName = "Dokumen Pengumpul Limbah";
                    break;
                case "ref_lb3_penimbun":
                    moduleName = "Perusahaan Penimbun Limbah";
                    break;
                case "ref_lb3_penimbun_dokumen":
                    moduleName = "Dokumen Penimbun Limbah";
                    break;
                case "ref_lb3_usaha":
                    moduleName = "Usaha Limbah B3";
                    break;
                case "ref_literal":
                    moduleName = "Literal";
                    break;
                case "ref_literal_data":
                    moduleName = "Literal Data";
                    break;
                case "ref_literal_kategori":
                    moduleName = "Literal Kategori";
                    break;
                case "ref_location":
                    moduleName = "Location";
                    break;
                case "ref_org_unit":
                    moduleName = "Organization Unit";
                    break;
                case "ref_org_unit_pa":
                    moduleName = "Organization Unit - Personal Area";
                    break;
                case "ref_periode":
                    moduleName = "Periode";
                    break;
                case "ref_periode_proses":
                    moduleName = "Periode Proses";
                    break;
                case "ref_person_type":
                    moduleName = "Person Type";
                    break;
                case "ref_personal_area":
                    moduleName = "Personal Area";
                    break;
                case "ref_personal_sub_area":
                    moduleName = "Personal Sub Area";
                    break;
                case "ref_personal_sub_area_head":
                    moduleName = "Personal Sub Area Head";
                    break;
                case "ref_perusahaan":
                    moduleName = "Perusahaan";
                    break;
                case "ref_proses":
                    moduleName = "Proses";
                    break;
                case "ref_provinsi":
                    moduleName = "Provinsi";
                    break;
                case "ref_room":
                    moduleName = "Room";
                    break;
                //case "ref_tta":
                //    moduleName = "Area";
                //    break;
                //case "ref_tta_admin":
                //    moduleName = "Area";
                //    break;
                //case "ref_tta_tch":
                //    moduleName = "Area";
                //    break;
                case "ref_unit_type":
                    moduleName = "Unit Type";
                    break;
                case "ta_acc":
                    moduleName = "Accident";
                    break;
                case "ta_acc_followup":
                    moduleName = "Accident - Followup";
                    break;
                case "ta_acc_korban":
                    moduleName = "Accident - Korban";
                    break;
                case "ta_acc_pelaku":
                    moduleName = "Accident - Pelaku";
                    break;
                case "ta_acc_saksi":
                    moduleName = "Accident - Saksi";
                    break;
                case "ta_audit":
                    moduleName = "Audit";
                    break;
                case "ta_audit_kriteria":
                    moduleName = "Audit Kriteria";
                    break;
                case "ta_audit_kriteria_agc":
                    moduleName = "Audit Kriteria AGC";
                    break;
                case "ta_audit_kriteria_agc_tm":
                    moduleName = "Audit Kriteria AGC Tambang Manufaktur";
                    break;
                case "ta_audit_kriteria_csms":
                    moduleName = "Audit Kriteria CSMS";
                    break;
                case "ta_audit_kriteria_smk3":
                    moduleName = "Audit Kriteria SMK3";
                    break;
                case "ta_audit_kriteria_smkp":
                    moduleName = "Audit Kriteria SMKP";
                    break;
                case "ta_cmp":
                    moduleName = "Campaign";
                    break;
                case "ta_cmp_peserta":
                    moduleName = "Campaign - Peserta";
                    break;
                case "ta_emg":
                    moduleName = "Emergency";
                    break;
                case "ta_emg_pihak_terlibat":
                    moduleName = "Emergency - Pihak Terlibat";
                    break;
                case "ta_emg_sarana":
                    moduleName = "Emergency - Sarana";
                    break;
                case "ta_fas":
                    moduleName = "Fasilitas";
                    break;
                case "ta_fas_spek_alarm_kebakaran":
                    moduleName = "Fasilitas - Alarm Kebakaran";
                    break;
                case "ta_fas_spek_bejana_tekan":
                    moduleName = "Fasilitas - Bejana Tekan";
                    break;
                case "ta_fas_spek_forklift":
                    moduleName = "Fasilitas - Forklift";
                    break;
                case "ta_fas_spek_genset":
                    moduleName = "Fasilitas - Genset";
                    break;
                case "ta_fas_spek_hydrant":
                    moduleName = "Fasilitas - Hydrant";
                    break;
                case "ta_fas_spek_instal_listrik":
                    moduleName = "Fasilitas - Instalasi Listrik";
                    break;
                case "ta_fas_spek_instal_petir":
                    moduleName = "Fasilitas - Instalasi Petir";
                    break;
                case "ta_fas_spek_ipal_domestik":
                    moduleName = "Fasilitas - IPAL Domestik";
                    break;
                case "ta_fas_spek_ipal_produksi":
                    moduleName = "Fasilitas - IPAL Produksi";
                    break;
                case "ta_fas_spek_iplc":
                    moduleName = "Fasilitas - IPLC";
                    break;
                case "ta_fas_spek_lift":
                    moduleName = "Fasilitas - Lift";
                    break;
                case "ta_fas_spek_mobil_crane":
                    moduleName = "Fasilitas - Mobile Crane";
                    break;
                case "ta_fas_spek_steady_crane":
                    moduleName = "Fasilitas - Steady Crane";
                    break;
                case "ta_fas_spek_tangki_timbun":
                    moduleName = "Fasilitas - Tangki Timbun";
                    break;
                case "ta_fas_spek_tps_lb3":
                    moduleName = "Fasilitas - TPS LB3";
                    break;
                case "ta_health":
                    moduleName = "Health";
                    break;
                case "ta_health_ganggu_fisik":
                    moduleName = "Health - Gangguan Fisik";
                    break;
                case "ta_health_gcu":
                    moduleName = "Health - GCU";
                    break;
                case "ta_health_sakit_kerja":
                    moduleName = "Health - Sakit";
                    break;
                case "ta_lb3":
                    moduleName = "Limbah B3";
                    break;
                case "ta_legal":
                    moduleName = "Legal";
                    break;
                case "ta_mp":
                    moduleName = "Man Power Profile";
                    break;
                case "ta_mp_riwayat_kerja":
                    moduleName = "Man Power Profile - Riwayat Kerja";
                    break;
                case "ta_nonlb3":
                    moduleName = "Limbah Non B3";
                    break;
                case "ta_pelatihan":
                    moduleName = "Training";
                    break;
                case "ta_pelatihan_peserta":
                    moduleName = "Training - Peserta";
                    break;
                case "ta_performance":
                    moduleName = "Performance";
                    break;
                case "ta_person":
                    moduleName = "Person";
                    break;
                case "ta_person_area":
                    moduleName = "Person Area";
                    break;
                //case "ta_person_superior":
                //    moduleName = "Area";
                //    break;
                case "ta_sda_air":
                    moduleName = "Sumber Daya Alam - Air";
                    break;
                case "ta_sda_kertas":
                    moduleName = "Sumber Daya Alam - Kertas";
                    break;
                case "ta_sda_listrik":
                    moduleName = "Sumber Daya Alam - Listrik";
                    break;
                case "ta_sda_solar":
                    moduleName = "Sumber Daya Alam - Solar";
                    break;
                case "ta_sda_tisu":
                    moduleName = "Sumber Daya Alam - Tisu";
                    break;
                default:
                    break;
            }
            */
            return moduleName;
        }

        public static string CopyToBaseDocument(IHostingEnvironment _hostingEnvironment, HttpContext context, string org_table_name, string org_col_name, OrderedDictionary org_PkValue, string tbl_name, string col_name, OrderedDictionary PkValue, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string new_file = "";
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string pathSession = Path.Combine(upload_temp, context.Session.Id);
            //
            string pathSource = Path.Combine(pathSession, tbl_name);
            pathSource = Path.Combine(pathSource, col_name);
            //
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            pathDest = Path.Combine(pathDest, tbl_name);
            foreach (DictionaryEntry entry in PkValue)
            {
                pathDest = Path.Combine(pathDest, entry.Value.ToString());
            }
            pathDest = Path.Combine(pathDest, col_name);
            if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
            if (col_val != "")
            {
                if (!Directory.Exists(pathDest)) { CreateDirectoryRecursively(pathDest); }
                string[] fileNames = col_val.Split(',');
                IList<string> fileNew = new List<string>();
                foreach (string itemFile in fileNames)
                {
                    if (!fileNew.Contains(itemFile))
                    {
                        string fileSource = Path.Combine(pathSource, itemFile);
                        string fileDest = Path.Combine(pathDest, itemFile);
                        if (System.IO.File.Exists(fileDest)) { System.IO.File.Delete(fileDest); }
                        if (System.IO.File.Exists(fileSource))
                        {
                            System.IO.File.Copy(fileSource, fileDest);
                            new_file += new_file != "" ? "," : "";
                            new_file += itemFile;
                            fileNew.Add(itemFile);
                        }

                    }
                }
            }
            return new_file;
        }

        public class initialFilesDocument
        {
            public string name { get; set; }
            public string extension { get; set; }
            public long size { get; set; }
            public string tbl_name { get; set; }
            public string tbl_id { get; set; }
            public string tbl_col_name { get; set; }
        }
        public static string[] GetinitialFilesDocument(IHostingEnvironment _hostingEnvironment, HttpContext context, string tbl_name, string col_name, OrderedDictionary PkValue, string col_val)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            IList<FileHelper.initialFilesDocument> _initialFilesDocument = new List<FileHelper.initialFilesDocument>();
            string new_val = "";
            string initfiles = "";
            string cur_id = "";
            if (col_val != "")
            {
                string[] col_vals = col_val.Split(',');
                foreach (string item_file in col_vals)
                {
                    string pathSource = Path.Combine(_hostingEnvironment.ContentRootPath, "data");


                    string folder_path_nm = "";
                    if (tbl_name == "ta_document_nm")
                    {
                        foreach (DictionaryEntry entry in PkValue)
                        {
                            cur_id = entry.Value.ToString();
                        }
                        folder_path_nm = SqlHelper.ExecuteScalarString("SELECT folder_path FROM ta_document_nm WHERE id = " + cur_id);
                        pathSource = Path.Combine(pathSource, folder_path_nm);
                    }
                    else
                    {
                        pathSource = Path.Combine(pathSource, tbl_name);
                        foreach (DictionaryEntry entry in PkValue)
                        {
                            pathSource = Path.Combine(pathSource, entry.Value.ToString());
                            cur_id = entry.Value.ToString();
                        }
                        pathSource = Path.Combine(pathSource, col_name);

                    }

                    string file_source = Path.Combine(pathSource, item_file);
                    if (System.IO.File.Exists(file_source))
                    {
                        new_val += new_val != "" ? "," : "";
                        new_val += item_file;
                        string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
                        string pathSession = Path.Combine(upload_temp, context.Session.Id);


                        string pathDest = "";
                        if (tbl_name == "ta_document_nm")
                        {
                            pathDest = Path.Combine(pathSession, folder_path_nm);
                        }
                        else
                        {
                            pathDest = Path.Combine(pathSession, tbl_name);
                            pathDest = Path.Combine(pathDest, col_name);
                        }


                        FileHelper.CreateDirectoryRecursively(pathDest);
                        string file_Dest = Path.Combine(pathDest, item_file);
                        if (System.IO.File.Exists(file_Dest)) { System.IO.File.Delete(file_Dest); }
                        System.IO.File.Copy(file_source, file_Dest);
                        FileInfo fi = new FileInfo(file_Dest);
                        FileHelper.initialFilesDocument initialFilesDocument = new FileHelper.initialFilesDocument();
                        initialFilesDocument.name = fi.Name;
                        initialFilesDocument.extension = fi.Extension;
                        initialFilesDocument.size = fi.Length;
                        initialFilesDocument.tbl_name = tbl_name;
                        initialFilesDocument.tbl_col_name = col_name;
                        initialFilesDocument.tbl_id = cur_id;
                        _initialFilesDocument.Add(initialFilesDocument);
                    }
                }
                //initfiles = JsonConvert.SerializeObject(_initialFiles);
            }
            initfiles = JsonConvert.SerializeObject(_initialFilesDocument);
            string[] result = { new_val, initfiles };
            return result;
        }

        public static void DeleteFromBaseDocument(IHostingEnvironment _hostingEnvironment, HttpContext context, string tbl_name, string col_name, string id, string file_name)
        {
            // -------------------------------------------------------------------------------------
            tbl_name = GetModuleName(tbl_name);
            // -------------------------------------------------------------------------------------
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            pathDest = Path.Combine(pathDest, tbl_name);
            pathDest = Path.Combine(pathDest, id);
            //if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
            pathDest = Path.Combine(pathDest, col_name, file_name);
            if (File.Exists(pathDest)) { File.Delete(pathDest); }
        }

        public static string CopyToBaseDocumentNM(IHostingEnvironment _hostingEnvironment, HttpContext context, string path_name, string uploadedFiles)
        {
            string new_file = "";
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string pathSession = Path.Combine(upload_temp, context.Session.Id);
            // -----------------------------------------------------------------------------
            string originalPathSessionSource = pathSession;
            originalPathSessionSource = Path.Combine(originalPathSessionSource, path_name);
            // -----------------------------------------------------------------------------
            string pathSource = Path.Combine(pathSession, "ta_document_nm");
            pathSource = Path.Combine(pathSource, "file_name");
            //
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            foreach (var s in path_name.Split(@"\"))
            {
                pathDest = Path.Combine(pathDest, s);
            }
            if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
            if (uploadedFiles != "")
            {
                if (!Directory.Exists(pathDest)) { CreateDirectoryRecursively(pathDest); }
                string[] fileNames = uploadedFiles.Split(',');
                IList<string> fileNew = new List<string>();
                foreach (string itemFile in fileNames)
                {
                    if (!fileNew.Contains(itemFile))
                    {
                        string fileSource = Path.Combine(pathSource, itemFile);
                        string fileDest = Path.Combine(pathDest, itemFile);
                        if (System.IO.File.Exists(fileDest)) { System.IO.File.Delete(fileDest); }
                        if (System.IO.File.Exists(fileSource))
                        {
                            System.IO.File.Copy(fileSource, fileDest);
                            new_file += new_file != "" ? "," : "";
                            new_file += itemFile;
                            fileNew.Add(itemFile);
                        }

                    }
                }
                foreach (string itemFile in fileNames)
                {
                    if (!fileNew.Contains(itemFile))
                    {
                        string fileSource = Path.Combine(originalPathSessionSource, itemFile);
                        string fileDest = Path.Combine(pathDest, itemFile);
                        if (System.IO.File.Exists(fileDest)) { System.IO.File.Delete(fileDest); }
                        if (System.IO.File.Exists(fileSource))
                        {
                            System.IO.File.Copy(fileSource, fileDest);
                            new_file += new_file != "" ? "," : "";
                            new_file += itemFile;
                            fileNew.Add(itemFile);
                        }

                    }
                }
            }
            return new_file;
        }

        public static string GetlinkDownloadNM(string baseUrl, string tbl_name, OrderedDictionary PkValue, string col_name, string col_val)
        {
            string[] colvals = col_val.Split(',');
            string listLink = "";
            if (colvals.Length == 1)
            {
                string qs_param = GetParamDownload(tbl_name, PkValue, col_name, col_val);
                string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                string link = baseUrl + "/Download?id=" + encode_param;
                listLink = "<a href=\"" + link + "\" target=\"_blank\">" + col_val + "</a>";
            }
            else
            {
                foreach (string item in colvals)
                {
                    string qs_param = GetParamDownload(tbl_name, PkValue, col_name, item);
                    string encode_param = System.Net.WebUtility.UrlEncode(qs_param);
                    string link = baseUrl + "/Download?id=" + encode_param;
                    listLink += listLink != "" ? "<br/>" : "";
                    listLink += "<a href=\"" + link + "\" target=\"_blank\">" + item + "</a>";
                }

            }
            return listLink;
        }

        public static void DeleteFromBaseNM(IHostingEnvironment _hostingEnvironment, HttpContext context, string folderPath)
        {
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            pathDest = Path.Combine(pathDest, folderPath);
            if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
        }

        public static string CopyToBaseDocumentNMUpdate(IHostingEnvironment _hostingEnvironment, HttpContext context, string new_path_name, string old_path_name, string uploadedFiles)
        {
            string new_file = "";
            string upload_temp = Settings.GetAppSetting("path_upload_temp") != null ? Settings.GetAppSetting("path_upload_temp") : "C:\\Temp";
            string pathSession = Path.Combine(upload_temp, context.Session.Id);
            //
            string pathSource = Path.Combine(pathSession, old_path_name);
            //
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            foreach (var s in new_path_name.Split(@"\"))
            {
                pathDest = Path.Combine(pathDest, s);
            }
            if (Directory.Exists(pathDest)) { Directory.Delete(pathDest, true); }
            if (uploadedFiles != "")
            {
                if (!Directory.Exists(pathDest)) { CreateDirectoryRecursively(pathDest); }
                string[] fileNames = uploadedFiles.Split(',');
                IList<string> fileNew = new List<string>();
                foreach (string itemFile in fileNames)
                {
                    if (!fileNew.Contains(itemFile))
                    {
                        string fileSource = Path.Combine(pathSource, itemFile);
                        string fileDest = Path.Combine(pathDest, itemFile);
                        if (System.IO.File.Exists(fileDest)) { System.IO.File.Delete(fileDest); }
                        if (System.IO.File.Exists(fileSource))
                        {
                            System.IO.File.Copy(fileSource, fileDest);
                            new_file += new_file != "" ? "," : "";
                            new_file += itemFile;
                            fileNew.Add(itemFile);
                        }
                    }
                }
            }
            return new_file;
        }

        public static void EmptyFolderData(IHostingEnvironment _hostingEnvironment)
        {
            string pathDest = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            var directories = Directory.GetDirectories(pathDest);
            foreach (string item in directories) {
                var fd_name = item.Split(Path.DirectorySeparatorChar).Last();
                if (fd_name != "Template") {
                    if (Directory.Exists(item)) { Directory.Delete(item, true); }
                }
            }
        }

    }
}
