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

        // Dönemleri ID DESC sıralı getir
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

        // Kursiyerleri ADI – SOYADI – TC ve eksik evrak ile getir
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

        // Tek kursiyer için evrak + kursiyer resmi getir
        public async Task<Mebbis_Model> GetEvrakByKursiyerIdAsync(int idKursiyer)
        {
            string query = @"
                SELECT TOP 1 
                    e.EKSK_OGRNIM_BEL,
                    e.EKSK_SAGLIK,
                    e.EKSK_SAVCILIK,
                    e.EKSK_SOZLESME,
                    e.EKSK_IMZA,

                    k.ADI,
                    k.SOYADI,
                    k.TC_NO,
                    k.ADAY_NO,
                    k.SERTIFIKA_SINIFI,
                    k.RESIM AS Foto,   -- Kursiyer resmi

                    e.OGR_BEL_VEREN_KURUM,
                    e.OGR_BEL_TARIHI,
                    e.OGR_BEL_SAYISI,
                    e.IMG_OGRNIM_BEL
                FROM KURSIYER_EVRAK e
                INNER JOIN KURSIYER k ON k.ID = e.ID_KURSIYER
                WHERE e.ID_KURSIYER = @ID_KURSIYER";

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
                        EkskOgrBel = EvrakBool(reader["EKSK_OGRNIM_BEL"]),
                        EkskSaglik = EvrakBool(reader["EKSK_SAGLIK"]),
                        EkskSavcilik = EvrakBool(reader["EKSK_SAVCILIK"]),
                        EkskSozlesme = EvrakBool(reader["EKSK_SOZLESME"]),
                        EkskImza = EvrakBool(reader["EKSK_IMZA"]),

                        ADI = reader["ADI"].ToString(),
                        SOYADI = reader["SOYADI"].ToString(),
                        TC_NO = reader["TC_NO"].ToString(),
                        ADAY_NO = reader["ADAY_NO"].ToString(),
                        SERTIFIKA_SINIFI = reader["SERTIFIKA_SINIFI"].ToString(),

                        OgrBelgeVerenKurum = reader["OGR_BEL_VEREN_KURUM"].ToString(),
                        OgrBelgeTarihi = reader["OGR_BEL_TARIHI"] != DBNull.Value ? (DateTime?)reader["OGR_BEL_TARIHI"] : null,
                        OgrBelgeNo = reader["OGR_BEL_SAYISI"].ToString(),

                        ImgOgrBel = reader["IMG_OGRNIM_BEL"] != DBNull.Value ? (byte[])reader["IMG_OGRNIM_BEL"] : null,
                        Foto = reader["Foto"] != DBNull.Value ? (byte[])reader["Foto"] : null
                    };
                }
            }
        }
        public async Task<(string KullaniciAdi, string Sifre)> GetMebbisLoginAsync()
        {
            string query = @"
        SELECT TOP 1 
            MEBBIS_KUL_ADI_1, 
            MEBBIS_KUL_SIF_1 
        FROM PARAM_GENEL_PARAMETRELER";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        string kullaniciAdi = reader["MEBBIS_KUL_ADI_1"].ToString();
                        string sifre = reader["MEBBIS_KUL_SIF_1"].ToString();
                        return (kullaniciAdi, sifre);
                    }
                }
            }

            return (string.Empty, string.Empty); // Eğer kayıt yoksa boş dön
        }
        private bool EvrakBool(object value)
        {
            if (value == null || value == DBNull.Value) return false;

            string v = value.ToString().Trim().ToUpper();
            return (v == "E" || v == "1" || v == "TRUE");
        }
    }
}
