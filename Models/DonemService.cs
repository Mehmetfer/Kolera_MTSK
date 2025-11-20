using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tarantula_MTSK.Services
{
    public class DonemService : IDonemService
    {
        private readonly string _connectionString;

        public DonemService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Boş metotlar artık Task.FromResult ile dönüyor
        public Task<List<string>> GetDonemListAsync()
        {
            return Task.FromResult(new List<string>());
        }

        public Task<string> GetKursiyerDonemiAsync(int kursiyerId)
        {
            return Task.FromResult(string.Empty);
        }

        public async Task<(List<string> Donemler, string KursiyerDonemi)> GetDonemlerVeKursiyerDonemiAsync(int kursiyerId)
        {
            var donemler = await GetDonemListAsync();
            var kursiyerDonemi = await GetKursiyerDonemiAsync(kursiyerId);
            return (donemler, kursiyerDonemi);
        }

        public async Task<DataTable> GetGrupKartlariAsync()
        {
            var dt = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM GRUP_KARTI ORDER BY BAS_TAR DESC, BIT_TAR DESC", connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt); // SqlDataAdapter.Fill senkron olduğu için await yok, sorun değil
                }
            }
            return dt;
        }

        // AddGrupAsync artık ay string olarak kaydediyor
        public async Task<int> AddGrupAsync(int yil, string ay, string sube, string donemAdi, string grupAdi, DateTime baslangic, DateTime bitis)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO GRUP_KARTI 
                      (DONEM_YILI, DONEM_AYI, DONEM_SUBESI, DONEM_ADI, DONEM_GRUBU, BAS_TAR, BIT_TAR) 
                      VALUES (@Yil, @Ay, @Sube, @DonemAdi, @GrupAdi, @Baslangic, @Bitis); 
                      SELECT SCOPE_IDENTITY();", connection))
                {
                    cmd.Parameters.AddWithValue("@Yil", yil);
                    cmd.Parameters.AddWithValue("@Ay", ay);
                    cmd.Parameters.AddWithValue("@Sube", sube);
                    cmd.Parameters.AddWithValue("@DonemAdi", donemAdi);
                    cmd.Parameters.AddWithValue("@GrupAdi", grupAdi);
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task<int> UpdateGrupAsync(int id, string donemAdi, string grupAdi, DateTime baslangic, DateTime bitis)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(
                    @"UPDATE GRUP_KARTI 
                      SET DONEM_ADI=@DonemAdi, DONEM_GRUBU=@GrupAdi, BAS_TAR=@Baslangic, BIT_TAR=@Bitis
                      WHERE ID=@Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@DonemAdi", donemAdi);
                    cmd.Parameters.AddWithValue("@GrupAdi", grupAdi);
                    cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                    cmd.Parameters.AddWithValue("@Bitis", bitis);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteGrupAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM GRUP_KARTI WHERE ID=@Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
