using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Services
{
    public class AramaService
    {
        public readonly string ConnectionString;

        public AramaService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // 🔹 Tüm kursiyerleri getir (TOP 20)
        public async Task<DataTable> GetAllKursiyerAsync()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = @"
SELECT TOP 120 
    K.*,
    GK.DONEM_ADI AS DONEM
FROM KURSIYER K
LEFT JOIN GRUP_KARTI GK ON K.ID_GRUP_KARTI = GK.ID
ORDER BY K.ID DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

    


        // 🔹 Arama: Adı, Soyadı, TC, AdıSoyadı birleşik
       
public async Task<DataTable> SearchKursiyerAsync(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = @"
SELECT K.*, GK.DONEM_ADI AS DONEM
FROM KURSIYER K
LEFT JOIN GRUP_KARTI GK ON K.ID_GRUP_KARTI = GK.ID
WHERE UPPER(K.ADI) LIKE UPPER(@keyword) 
   OR UPPER(K.SOYADI) LIKE UPPER(@keyword) 
   OR K.TC_NO LIKE @keyword
   OR UPPER(CONCAT(K.ADI, ' ', K.SOYADI)) LIKE UPPER(@keyword)
ORDER BY K.ID DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        

            
        }

        // 🔹 Kursiyer resmini getir
        public async Task<byte[]> GetKursiyerResimByIdAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT RESIM FROM KURSIYER WHERE ID=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    return (result != null && result != DBNull.Value) ? (byte[])result : null;
                }
            }
        }

        // 🔹 Kursiyer Evrakları
        public KursiyerEvrak_Model GetKursiyerEvrak(int kursiyerId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = @"SELECT TOP 1 * FROM KURSIYER_EVRAK WHERE ID_KURSIYER = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", kursiyerId);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new KursiyerEvrak_Model
                            {
                                EkskOgrBel = dr["EKSK_OGRNIM_BEL"]?.ToString() == "VAR",
                                EkskSaglik = dr["EKSK_SAGLIK"]?.ToString() == "VAR",
                                EkskSavcilik = dr["EKSK_SAVCILIK"]?.ToString() == "VAR",
                                EkskSozlesme = dr["EKSK_SOZLESME"]?.ToString() == "VAR",
                                EkskImza = dr["EKSK_IMZA"]?.ToString() == "VAR",

                                OgrBelgeTuru = dr["OGR_BEL_TURU"]?.ToString(),
                                OgrBelgeVerenKurum = dr["OGR_BEL_VEREN_KURUM"]?.ToString(),
                                OgrBelgeSayisi = dr["OGR_BEL_SAYISI"]?.ToString(),
                                OgrBelgeTarihi = dr["OGR_BEL_TARIHI"] != DBNull.Value ? (DateTime?)dr["OGR_BEL_TARIHI"] : null,

                                SaglikBelgeNo = dr["SAG_RAPOR_BELGENO"]?.ToString(),
                                SaglikBelgeTarihi = dr["SAG_RAPOR_TARIHI"] != DBNull.Value ? (DateTime?)dr["SAG_RAPOR_TARIHI"] : null,
                                SaglikBelverenKurum = dr["SAG_RAPOR_VEREN_KURUM"]?.ToString(),
                                SaglikBelReferans = dr["SAG_RAPOR_REFERANS"]?.ToString(),
                                SavcilikBelgeTarihi = dr["SAVCILIK_BEL_TARIHI"] != DBNull.Value ? (DateTime?)dr["SAVCILIK_BEL_TARIHI"] : null,
                                SavcilikBelgeNo = dr["SAVCILIK_BEL_NO"]?.ToString(),
                                SavcilikBelgeVerenKurum = dr["SAVCILIK_BEL_VEREN_KURUM"]?.ToString(),

                                ImgOgrBel = dr["IMG_OGRNIM_BEL"] as byte[],
                                ImgSaglik = dr["IMG_SAGLIK"] as byte[],
                                ImgSavcilik = dr["IMG_SAVCILIK"] as byte[],
                                ImgSozlesme_On = dr["IMG_SOZLESME_ON"] as byte[],
                                ImgSozlesme_Arka = dr["IMG_SOZLESME_ARKA"] as byte[],
                                ImgImza = dr["IMG_IMZA"] as byte[],
                            };
                        }
                    }
                }
            }
            return null;
        }

        // 🔹 Kursiyer Evrak Kaydet
        public bool SaveKursiyerEvrak(KursiyerEvrak_Model evrak)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string sql = @"
IF EXISTS(SELECT 1 FROM KURSIYER_EVRAK WHERE ID_KURSIYER = @ID_Kursiyer)
BEGIN
    UPDATE KURSIYER_EVRAK SET
        EKSK_OGRNIM_BEL=@EkskOgrBel,
        EKSK_SAGLIK=@EkskSaglik,
        EKSK_SAVCILIK=@EkskSavcilik,
        EKSK_SOZLESME=@EkskSozlesme,
        EKSK_IMZA=@EkskImza,
        OGR_BEL_TURU=@OgrBelgeTuru,
        OGR_BEL_VEREN_KURUM=@OgrBelgeVerenKurum,
        OGR_BEL_SAYISI=@OgrBelgeSayisi,
        OGR_BEL_TARIHI=@OgrBelgeTarihi,
        SAG_RAPOR_BELGENO=@SaglikBelgeNo,
        SAG_RAPOR_TARIHI=@SaglikBelgeTarihi,
        SAG_RAPOR_VEREN_KURUM=@SaglikBelverenKurum,
        SAG_RAPOR_REFERANS=@SaglikBelReferans,
        SAVCILIK_BEL_NO=@SavcilikBelgeNo,
        SAVCILIK_BEL_TARIHI=@SavcilikBelgeTarihi,
        SAVCILIK_BEL_VEREN_KURUM=@SavcilikBelgeVerenKurum,
        IMG_OGRNIM_BEL=@ImgOgrBel,
        IMG_SAGLIK=@ImgSaglik,
        IMG_SAVCILIK=@ImgSavcilik,
        IMG_SOZLESME_ON=@ImgSozlesme_On,
        IMG_SOZLESME_ARKA=@ImgSozlesme_Arka,
        IMG_IMZA=@ImgImza
    WHERE ID_KURSIYER = @ID_Kursiyer
END
ELSE
BEGIN
    INSERT INTO KURSIYER_EVRAK
        (ID_KURSIYER, EKSK_OGRNIM_BEL, EKSK_SAGLIK, EKSK_SAVCILIK, EKSK_SOZLESME, EKSK_IMZA,
         OGR_BEL_TURU, OGR_BEL_VEREN_KURUM, OGR_BEL_SAYISI, OGR_BEL_TARIHI,
         SAG_RAPOR_BELGENO, SAG_RAPOR_TARIHI, SAG_RAPOR_VEREN_KURUM, SAG_RAPOR_REFERANS,
         SAVCILIK_BEL_NO, SAVCILIK_BEL_TARIHI, SAVCILIK_BEL_VEREN_KURUM,
         IMG_OGRNIM_BEL, IMG_SAGLIK, IMG_SAVCILIK, IMG_SOZLESME_ON, IMG_SOZLESME_ARKA, IMG_IMZA)
    VALUES
        (@ID_Kursiyer, @EkskOgrBel, @EkskSaglik, @EkskSavcilik, @EkskSozlesme, @EkskImza,
         @OgrBelgeTuru, @OgrBelgeVerenKurum, @OgrBelgeSayisi, @OgrBelgeTarihi,
         @SaglikBelgeNo, @SaglikBelgeTarihi, @SaglikBelverenKurum, @SaglikBelReferans,
         @SavcilikBelgeNo, @SavcilikBelgeTarihi, @SavcilikBelgeVerenKurum,
         @ImgOgrBel, @ImgSaglik, @ImgSavcilik, @ImgSozlesme_On, @ImgSozlesme_Arka, @ImgImza)
END";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // 🔹 Boolean alanlar -> CHAR(3)
                        cmd.Parameters.Add("@EkskOgrBel", SqlDbType.Char, 3).Value = evrak.EkskOgrBel ? "VAR" : "YOK";
                        cmd.Parameters.Add("@EkskSaglik", SqlDbType.Char, 3).Value = evrak.EkskSaglik ? "VAR" : "YOK";
                        cmd.Parameters.Add("@EkskSavcilik", SqlDbType.Char, 3).Value = evrak.EkskSavcilik ? "VAR" : "YOK";
                        cmd.Parameters.Add("@EkskSozlesme", SqlDbType.Char, 3).Value = evrak.EkskSozlesme ? "VAR" : "YOK";
                        cmd.Parameters.Add("@EkskImza", SqlDbType.Char, 3).Value = evrak.EkskImza ? "VAR" : "YOK";

                        // 🔹 ID
                        cmd.Parameters.Add("@ID_Kursiyer", SqlDbType.Int).Value = evrak.ID_Kursiyer;

                        // 🔹 String alanlar
                        cmd.Parameters.Add("@OgrBelgeTuru", SqlDbType.NVarChar, 50).Value = (object)evrak.OgrBelgeTuru ?? DBNull.Value;
                        cmd.Parameters.Add("@OgrBelgeVerenKurum", SqlDbType.NVarChar, 100).Value = (object)evrak.OgrBelgeVerenKurum ?? DBNull.Value;
                        cmd.Parameters.Add("@OgrBelgeSayisi", SqlDbType.NVarChar, 10).Value = (object)evrak.OgrBelgeSayisi ?? DBNull.Value;

                        cmd.Parameters.Add("@SaglikBelgeNo", SqlDbType.NVarChar, 50).Value = (object)evrak.SaglikBelgeNo ?? DBNull.Value;
                        cmd.Parameters.Add("@SaglikBelverenKurum", SqlDbType.NVarChar, 100).Value = (object)evrak.SaglikBelverenKurum ?? DBNull.Value;
                        cmd.Parameters.Add("@SaglikBelReferans", SqlDbType.NVarChar, 50).Value = (object)evrak.SaglikBelReferans ?? DBNull.Value;

                        cmd.Parameters.Add("@SavcilikBelgeNo", SqlDbType.NVarChar, 50).Value = (object)evrak.SavcilikBelgeNo ?? DBNull.Value;
                        cmd.Parameters.Add("@SavcilikBelgeVerenKurum", SqlDbType.NVarChar, 100).Value = (object)evrak.SavcilikBelgeVerenKurum ?? DBNull.Value;

                        // 🔹 Tarihler
                        cmd.Parameters.Add("@OgrBelgeTarihi", SqlDbType.DateTime).Value =
                            evrak.OgrBelgeTarihi.HasValue ? (object)evrak.OgrBelgeTarihi.Value : DBNull.Value;

                        cmd.Parameters.Add("@SaglikBelgeTarihi", SqlDbType.DateTime).Value =
                            evrak.SaglikBelgeTarihi.HasValue ? (object)evrak.SaglikBelgeTarihi.Value : DBNull.Value;

                        cmd.Parameters.Add("@SavcilikBelgeTarihi", SqlDbType.DateTime).Value =
                            evrak.SavcilikBelgeTarihi.HasValue ? (object)evrak.SavcilikBelgeTarihi.Value : DBNull.Value;

                        // 🔹 Resimler
                        cmd.Parameters.Add("@ImgOgrBel", SqlDbType.VarBinary, -1).Value = (object)evrak.ImgOgrBel ?? DBNull.Value;
                        cmd.Parameters.Add("@ImgSaglik", SqlDbType.VarBinary, -1).Value = (object)evrak.ImgSaglik ?? DBNull.Value;
                        cmd.Parameters.Add("@ImgSavcilik", SqlDbType.VarBinary, -1).Value = (object)evrak.ImgSavcilik ?? DBNull.Value;
                        cmd.Parameters.Add("@ImgSozlesme_On", SqlDbType.VarBinary, -1).Value = (object)evrak.ImgSozlesme_On ?? DBNull.Value;
                        cmd.Parameters.Add("@ImgSozlesme_Arka", SqlDbType.VarBinary, -1).Value = (object)evrak.ImgSozlesme_Arka ?? DBNull.Value;
                        cmd.Parameters.Add("@ImgImza", SqlDbType.VarBinary, -1).Value = (object)evrak.ImgImza ?? DBNull.Value;

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Hatası: {sqlEx.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 🔹 Kursiyer sınıf bilgileri
        public async Task<(string SertifikaSinifi, string OncekiSertSinifi)> GetKursiyerSinifBilgileriAsync(int kursiyerId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT SERTIFIKA_SINIFI, ONCE_SERT_SINIFI FROM KURSIYER WHERE ID = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", kursiyerId);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string sertSinif = reader["SERTIFIKA_SINIFI"]?.ToString();
                            string oncekiSertSinif = reader["ONCE_SERT_SINIFI"]?.ToString();
                            return (sertSinif, oncekiSertSinif);
                        }
                    }
                }
            }
            return (null, null);
        }

        // 🔹 Parametrelerden sertifika sınıfları
        public async Task<(List<string> YeniSiniflar, List<string> MevcutSiniflar)> GetParamSertifikaSiniflariAsync()
        {
            var yeniSiniflar = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var mevcutSiniflar = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = @"SELECT YENI_SINIF, MEVCUT_SINIF FROM PARAM_SERTIFIKA_SINIFLARI";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader["YENI_SINIF"] != DBNull.Value)
                                yeniSiniflar.Add(reader["YENI_SINIF"].ToString().Trim());
                            if (reader["MEVCUT_SINIF"] != DBNull.Value)
                                mevcutSiniflar.Add(reader["MEVCUT_SINIF"].ToString().Trim());
                        }
                    }
                }
            }

            return (yeniSiniflar.ToList(), mevcutSiniflar.ToList());
        }

        // 🔹 Dönemler ve kursiyer dönemi
        public async Task<(List<string> Donemler, string KursiyerDonemi)> GetDonemlerVeKursiyerDonemiAsync(int kursiyerId)
        {
            var donemler = new List<string>();
            string kursiyerDonemi = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                await conn.OpenAsync();

                string sqlDonemler = "SELECT DISTINCT DONEM_ADI FROM GRUP_KARTI ORDER BY DONEM_ADI";
                using (SqlCommand cmd = new SqlCommand(sqlDonemler, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        donemler.Add(reader["DONEM_ADI"].ToString());
                }

                string sqlKursiyerDonemi = @"
                    SELECT GK.DONEM_ADI
                    FROM KURSIYER K
                    INNER JOIN GRUP_KARTI GK ON K.ID_GRUP_KARTI = GK.ID
                    WHERE K.ID = @kursiyerId";

                using (SqlCommand cmd = new SqlCommand(sqlKursiyerDonemi, conn))
                {
                    cmd.Parameters.AddWithValue("@kursiyerId", kursiyerId);
                    var result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                        kursiyerDonemi = result.ToString();
                }
            }

            return (donemler, kursiyerDonemi);
        }

        // 🔹 Döneme göre grup ID
        public async Task<int?> GetGrupIdByDonemAsync(string donemAdi)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT TOP 1 ID FROM GRUP_KARTI WHERE DONEM_ADI = @donemAdi";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@donemAdi", donemAdi);
                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }
    }
}
