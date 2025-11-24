using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Services
{
    public class MebbisService
    {
        private readonly string _connectionString;

        public MebbisService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // -----------------------------
        // Dönemleri getir
        // -----------------------------
        public async Task<DataTable> GetDonemlerAsync()
        {
            DataTable dt = new DataTable();
            string query = "SELECT ID, DONEM_ADI FROM GRUP_KARTI ORDER BY ID DESC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                await conn.OpenAsync();
                da.Fill(dt);
            }

            return dt;
        }

        // -----------------------------
        // Kursiyerleri getir (donemId ile)
        // -----------------------------
        public async Task<DataTable> GetKursiyerByDonemIdAsync(int donemId)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    k.ID AS ID_KURSIYER,
                    k.ADI,
                    k.SOYADI,
                    k.TC_NO,
                    k.SERTIFIKA_SINIFI,
                    k.ID_GRUP_KARTI AS DONEM_ADI,
                    e.EKSK_OGRNIM_BEL AS EkskOgrBel,
                    e.EKSK_SAGLIK AS EkskSaglik,
                    e.EKSK_SAVCILIK AS EkskSavcilik,
                    e.EKSK_SOZLESME AS EkskSozlesme,
                    e.EKSK_IMZA AS EkskImza
                FROM KURSIYER k
                INNER JOIN KURSIYER_EVRAK e ON k.ID = e.ID_KURSIYER
                WHERE k.ID_GRUP_KARTI = @DonemId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@DonemId", donemId);
                await conn.OpenAsync();
                da.Fill(dt);
            }

            return dt;
        }

        // -----------------------------
        // Tek kursiyer için evrak ve bilgileri getir
        // -----------------------------
        public async Task<Mebbis_Model> GetEvrakByKursiyerIdAsync(int idKursiyer)
        {
            string query = @"
                SELECT TOP 1 *
                FROM KURSIYER_EVRAK e
                INNER JOIN KURSIYER k ON k.ID = e.ID_KURSIYER
                WHERE k.ID = @ID_KURSIYER";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID_KURSIYER", idKursiyer);
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) return null;
                    await reader.ReadAsync();

                    return new Mebbis_Model
                    {
                        ADI = reader["ADI"].ToString(),
                        SOYADI = reader["SOYADI"].ToString(),
                        TC_NO = reader["TC_NO"].ToString(),
                        ADAY_NO = reader["ADAY_NO"].ToString(),
                        SERTIFIKA_SINIFI = reader["SERTIFIKA_SINIFI"].ToString(),
                        Foto = reader["RESIM"] != DBNull.Value ? (byte[])reader["RESIM"] : null,

                        EkskOgrBel = EvrakBool(reader["EKSK_OGRNIM_BEL"]),
                        EkskSaglik = EvrakBool(reader["EKSK_SAGLIK"]),
                        EkskSavcilik = EvrakBool(reader["EKSK_SAVCILIK"]),
                        EkskSozlesme = EvrakBool(reader["EKSK_SOZLESME"]),
                        EkskImza = EvrakBool(reader["EKSK_IMZA"]),

                        OgrBelTuru = reader["OGR_BEL_TURU"].ToString(),
                        OgrBelgeVerenKurum = reader["OGR_BEL_VEREN_KURUM"].ToString(),
                        OgrBelgeTarihi = reader["OGR_BEL_TARIHI"] != DBNull.Value ? (DateTime?)reader["OGR_BEL_TARIHI"] : null,
                        OgrBelgeNo = reader["OGR_BEL_SAYISI"].ToString(),

                        SaglikRaporVerenKurum = reader["SAG_RAPOR_VEREN_KURUM"].ToString(),
                        SaglikRaporTarihi = reader["SAG_RAPOR_TARIHI"] != DBNull.Value ? (DateTime?)reader["SAG_RAPOR_TARIHI"] : null,
                        SaglikRaporNo = reader["SAG_RAPOR_BELGENO"].ToString(),
                        SaglikRaporReferans = reader["SAG_RAPOR_REFERANS"].ToString(),

                        SavcilikBelgeVerenKurum = reader["SAVCILIK_BEL_VEREN_KURUM"].ToString(),
                        SavcilikBelgeTarihi = reader["SAVCILIK_BEL_TARIHI"] != DBNull.Value ? (DateTime?)reader["SAVCILIK_BEL_TARIHI"] : null,
                        SavcilikBelgeNo = reader["SAVCILIK_BEL_NO"].ToString(),

                        ImgOgrBel = reader["IMG_OGRNIM_BEL"] != DBNull.Value ? (byte[])reader["IMG_OGRNIM_BEL"] : null,
                        ImgSaglik = reader["IMG_SAGLIK"] != DBNull.Value ? (byte[])reader["IMG_SAGLIK"] : null,
                        ImgSavcilik = reader["IMG_SAVCILIK"] != DBNull.Value ? (byte[])reader["IMG_SAVCILIK"] : null,
                        ImgSozlesme_On = reader["IMG_SOZLESME_ON"] != DBNull.Value ? (byte[])reader["IMG_SOZLESME_ON"] : null,
                        ImgSozlesme_Arka = reader["IMG_SOZLESME_ARKA"] != DBNull.Value ? (byte[])reader["IMG_SOZLESME_ARKA"] : null,
                        ImgImza = reader["IMG_IMZA"] != DBNull.Value ? (byte[])reader["IMG_IMZA"] : null,
                        ImgFatura = reader["IMG_FATURA"] != DBNull.Value ? (byte[])reader["IMG_FATURA"] : null,

                        OzurDurumu = reader["OZUR_DURUMU"].ToString(),
                        YabanciDil = reader["YABANCI_DIL"].ToString(),
                        FaturaNo = reader["FATURA_NO"].ToString(),
                        FaturaTarihi = reader["FATURA_TARIHI"] != DBNull.Value ? (DateTime?)reader["FATURA_TARIHI"] : null,
                        FaturaTutari = reader["FATURA_TUTARI"] != DBNull.Value ? (decimal?)reader["FATURA_TUTARI"] : null
                    };
                }
            }
        }

        // -----------------------------
        // MEBBİS login bilgileri
        // -----------------------------
        public async Task<(string KullaniciAdi, string Sifre)> GetMebbisLoginAsync()
        {
            string query = "SELECT TOP 1 MEBBIS_KUL_ADI_1, MEBBIS_KUL_SIF_1 FROM PARAM_GENEL_PARAMETRELER";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                        return (reader["MEBBIS_KUL_ADI_1"].ToString(),
                                reader["MEBBIS_KUL_SIF_1"].ToString());
                }
            }
            return (string.Empty, string.Empty);
        }

        private bool EvrakBool(object value)
        {
            if (value == null || value == DBNull.Value) return false;
            string v = value.ToString().Trim().ToUpper();
            return v == "E" || v == "1" || v == "TRUE";
        }

        // -----------------------------
        // Link çek (ID ile)
        // -----------------------------
        public async Task<Mebbis_Model> GetLinkByIdAsync(int id)
        {
            string query = "SELECT ID, URL FROM LINKLER WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Mebbis_Model
                        {
                            URL = reader["URL"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
