using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Services
{
    public class Kurs_Ayar_Service
    {
        private readonly string _connectionString;

        public Kurs_Ayar_Service(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public Kurs_Ayar_Model GetKursAyar()
        {
            Kurs_Ayar_Model model = new Kurs_Ayar_Model();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // PARAM_KURSBILGILERI
                    string sqlKurs = @"SELECT TOP 1 
                                            KURS_ADI, KURS_ADI_KISA, IL_KODU, ILCE_KODU, 
                                            TELEFON, ADRES, KURUCU_ADI, MUDUR_ADI, MUDUR_YRD_ADI, KURS_IZIN_TARIHI
                                       FROM PARAM_KURSBILGILERI";

                    using (SqlCommand cmd = new SqlCommand(sqlKurs, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model.KURS_ADI = reader["KURS_ADI"].ToString();
                            model.KURS_ADI_KISA = reader["KURS_ADI_KISA"].ToString();
                            model.IL_KODU = reader["IL_KODU"].ToString();
                            model.ILCE_KODU = reader["ILCE_KODU"].ToString();
                            model.TELEFON = reader["TELEFON"].ToString();
                            model.ADRES = reader["ADRES"].ToString();
                            model.KURUCU_ADI = reader["KURUCU_ADI"].ToString();
                            model.MUDUR_ADI = reader["MUDUR_ADI"].ToString();
                            model.MUDUR_YRD_ADI = reader["MUDUR_YRD_ADI"].ToString();
                            if (reader["KURS_IZIN_TARIHI"] != DBNull.Value)
                                model.KURS_IZIN_TARIHI = Convert.ToDateTime(reader["KURS_IZIN_TARIHI"]);
                        }
                    }

                    // PARAM_GENEL_PARAMETRELER
                    string sqlGenel = @"SELECT TOP 1
                                            MEBBIS_KUL_ADI_1, MEBBIS_KUL_SIF_1,
                                            MEBBIS_KUL_ADI_2, MEBBIS_KUL_SIF_2,
                                            MEBBIS_KUL_ADI_3, MEBBIS_KUL_SIF_3,
                                            MEBBIS_KUL_ADI_4, MEBBIS_KUL_SIF_4,
                                            MEBBIS_KUL_ADI_5, MEBBIS_KUL_SIF_5
                                        FROM PARAM_GENEL_PARAMETRELER";

                    using (SqlCommand cmd = new SqlCommand(sqlGenel, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model.MEBBIS_KUL_ADI_1 = reader["MEBBIS_KUL_ADI_1"].ToString();
                            model.MEBBIS_KUL_SIF_1 = reader["MEBBIS_KUL_SIF_1"].ToString();
                            model.MEBBIS_KUL_ADI_2 = reader["MEBBIS_KUL_ADI_2"].ToString();
                            model.MEBBIS_KUL_SIF_2 = reader["MEBBIS_KUL_SIF_2"].ToString();
                            model.MEBBIS_KUL_ADI_3 = reader["MEBBIS_KUL_ADI_3"].ToString();
                            model.MEBBIS_KUL_SIF_3 = reader["MEBBIS_KUL_SIF_3"].ToString();
                            model.MEBBIS_KUL_ADI_4 = reader["MEBBIS_KUL_ADI_4"].ToString();
                            model.MEBBIS_KUL_SIF_4 = reader["MEBBIS_KUL_SIF_4"].ToString();
                            model.MEBBIS_KUL_ADI_5 = reader["MEBBIS_KUL_ADI_5"].ToString();
                            model.MEBBIS_KUL_SIF_5 = reader["MEBBIS_KUL_SIF_5"].ToString();
                        }
                    }

                    // LINKLER
                    string sqlLink = "SELECT LINK FROM LINKLER";
                    using (SqlCommand cmd = new SqlCommand(sqlLink, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<string> links = new List<string>();
                        while (reader.Read())
                        {
                            links.Add(reader["LINK"].ToString());
                        }
                        model.LINKLER = links.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kurs ayarları getirilirken hata oluştu: " + ex.Message);
            }

            return model;
        }

        public void SaveKursAyar(Kurs_Ayar_Model model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // PARAM_KURSBILGILERI
                    string sqlKurs = @"UPDATE PARAM_KURSBILGILERI SET 
                                            KURS_ADI=@KURS_ADI,
                                            KURS_ADI_KISA=@KURS_ADI_KISA,
                                            IL_KODU=@IL_KODU,
                                            ILCE_KODU=@ILCE_KODU,
                                            TELEFON=@TELEFON,
                                            ADRES=@ADRES,
                                            KURUCU_ADI=@KURUCU_ADI,
                                            MUDUR_ADI=@MUDUR_ADI,
                                            MUDUR_YRD_ADI=@MUDUR_YRD_ADI,
                                            KURS_IZIN_TARIHI=@KURS_IZIN_TARIHI";

                    using (SqlCommand cmd = new SqlCommand(sqlKurs, conn))
                    {
                        cmd.Parameters.AddWithValue("@KURS_ADI", model.KURS_ADI ?? "");
                        cmd.Parameters.AddWithValue("@KURS_ADI_KISA", model.KURS_ADI_KISA ?? "");
                        cmd.Parameters.AddWithValue("@IL_KODU", model.IL_KODU ?? "");
                        cmd.Parameters.AddWithValue("@ILCE_KODU", model.ILCE_KODU ?? "");
                        cmd.Parameters.AddWithValue("@TELEFON", model.TELEFON ?? "");
                        cmd.Parameters.AddWithValue("@ADRES", model.ADRES ?? "");
                        cmd.Parameters.AddWithValue("@KURUCU_ADI", model.KURUCU_ADI ?? "");
                        cmd.Parameters.AddWithValue("@MUDUR_ADI", model.MUDUR_ADI ?? "");
                        cmd.Parameters.AddWithValue("@MUDUR_YRD_ADI", model.MUDUR_YRD_ADI ?? "");
                        cmd.Parameters.AddWithValue("@KURS_IZIN_TARIHI", model.KURS_IZIN_TARIHI ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }

                    // PARAM_GENEL_PARAMETRELER
                    string sqlGenelUpdate = @"UPDATE PARAM_GENEL_PARAMETRELER SET 
                                                MEBBIS_KUL_ADI_1=@MEBBIS_KUL_ADI_1,
                                                MEBBIS_KUL_SIF_1=@MEBBIS_KUL_SIF_1,
                                                MEBBIS_KUL_ADI_2=@MEBBIS_KUL_ADI_2,
                                                MEBBIS_KUL_SIF_2=@MEBBIS_KUL_SIF_2,
                                                MEBBIS_KUL_ADI_3=@MEBBIS_KUL_ADI_3,
                                                MEBBIS_KUL_SIF_3=@MEBBIS_KUL_SIF_3,
                                                MEBBIS_KUL_ADI_4=@MEBBIS_KUL_ADI_4,
                                                MEBBIS_KUL_SIF_4=@MEBBIS_KUL_SIF_4,
                                                MEBBIS_KUL_ADI_5=@MEBBIS_KUL_ADI_5,
                                                MEBBIS_KUL_SIF_5=@MEBBIS_KUL_SIF_5";

                    using (SqlCommand cmd = new SqlCommand(sqlGenelUpdate, conn))
                    {
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_ADI_1", model.MEBBIS_KUL_ADI_1 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_SIF_1", model.MEBBIS_KUL_SIF_1 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_ADI_2", model.MEBBIS_KUL_ADI_2 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_SIF_2", model.MEBBIS_KUL_SIF_2 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_ADI_3", model.MEBBIS_KUL_ADI_3 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_SIF_3", model.MEBBIS_KUL_SIF_3 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_ADI_4", model.MEBBIS_KUL_ADI_4 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_SIF_4", model.MEBBIS_KUL_SIF_4 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_ADI_5", model.MEBBIS_KUL_ADI_5 ?? "");
                        cmd.Parameters.AddWithValue("@MEBBIS_KUL_SIF_5", model.MEBBIS_KUL_SIF_5 ?? "");
                        cmd.ExecuteNonQuery();
                    }

                    // LINKLER
                    string sqlDeleteLink = "DELETE FROM LINKLER";
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteLink, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    if (model.LINKLER != null)
                    {
                        foreach (var link in model.LINKLER)
                        {
                            string sqlInsertLink = "INSERT INTO LINKLER(LINK) VALUES(@LINK)";
                            using (SqlCommand cmd = new SqlCommand(sqlInsertLink, conn))
                            {
                                cmd.Parameters.AddWithValue("@LINK", link.Trim());
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kurs ayarları kaydedilirken hata oluştu: " + ex.Message);
            }
        }
    }
}
