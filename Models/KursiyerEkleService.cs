using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tarantula_MTSK.Models;
using static Tarantula_MTSK.Models.Kursiyer_EkleModel;

namespace Tarantula_MTSK.Services
{
    public class KursiyerEkleService
    {
        private readonly string _connectionString;

        public KursiyerEkleService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Dönemleri getir
        public async Task<List<Donem>> GetDonemlerAsync()
        {
            var donemler = new List<Donem>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT DISTINCT DONEM_ADI, ID FROM GRUP_KARTI ORDER BY DONEM_ADI", conn))
            {
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        donemler.Add(new Donem
                        {
                            Id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                            DonemAdi = reader.IsDBNull(0) ? null : reader.GetString(0)
                        });
                    }
                }
            }
            return donemler;
        }

        // Sınıfları getir (mevcut + önceki opsiyonel)
        public async Task<List<string>> GetSertifikaSiniflariAsync()
        {
            var siniflar = new List<string>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT DISTINCT SERTIFIKA_SINIFI FROM KURSIYER WHERE SERTIFIKA_SINIFI IS NOT NULL ORDER BY SERTIFIKA_SINIFI", conn))
            {
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (!reader.IsDBNull(0))
                            siniflar.Add(reader.GetString(0));
                    }
                }
            }
            return siniflar;
        }

        // Var olan kursiyerin sınıflarını getir
        public async Task<Kursiyer_EkleModel> GetKursiyerSiniflariAsync(int id)
        {
            var model = new Kursiyer_EkleModel();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT SERTIFIKA_SINIFI, ONCE_SERT_SINIFI FROM KURSIYER WHERE ID = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        model.SERTIFIKA_SINIFI = reader.IsDBNull(0) ? null : reader.GetString(0);
                        model.ONCE_SERT_SINIFI = reader.IsDBNull(1) ? null : reader.GetString(1);
                    }
                }
            }
            return model;
        }

        // Yeni kursiyer ekle
        public async Task<int> AddKursiyerAsync(Kursiyer_EkleModel model)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                INSERT INTO KURSIYER 
                (ADI, SOYADI, ID_GRUP_KARTI, SERTIFIKA_SINIFI, ONCE_SERT_SINIFI, TC_NO, DOGUM_TARIHI, KAYIT_TARIHI, ADAY_NO, RESIM, KURSIYER_DURUMU )
                VALUES
                (@ADI, @SOYADI, @ID_GRUP_KARTI, @SERTIFIKA_SINIFI, @ONCE_SERT_SINIFI, @TC_NO, @DOGUM_TARIHI, @KAYIT_TARIHI, @ADAY_NO, @RESIM, @KURSIYER_DURUMU );
                SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@ADI", model.ADI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SOYADI", model.SOYADI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_GRUP_KARTI", model.ID_GRUP_KARTI);
                cmd.Parameters.AddWithValue("@SERTIFIKA_SINIFI", model.SERTIFIKA_SINIFI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ONCE_SERT_SINIFI", model.ONCE_SERT_SINIFI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TC_NO", model.TC_NO ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DOGUM_TARIHI", model.DOGUM_TARIHI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@KAYIT_TARIHI", model.KAYIT_TARIHI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ADAY_NO", model.ADAY_NO);
                cmd.Parameters.AddWithValue("@RESIM", model.RESIM ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@KURSIYER_DURUMU", model.KURSIYER_DURUMU);

                await conn.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
    }
}
