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

        // Örnek dönem listesi
        public async Task<List<string>> GetDonemListAsync()
        {
            await Task.CompletedTask; // async yapıyı korumak için
            return new List<string>();
        }

        // Örnek kursiyer dönemi
        public async Task<string> GetKursiyerDonemiAsync(int kursiyerId)
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        public async Task<(List<string> Donemler, string KursiyerDonemi)> GetDonemlerVeKursiyerDonemiAsync(int kursiyerId)
        {
            var donemler = await GetDonemListAsync();
            var kursiyerDonemi = await GetKursiyerDonemiAsync(kursiyerId);
            return (donemler, kursiyerDonemi);
        }

        public async Task<DataTable> GetGrupKartlariAsync()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM GRUP_KARTI", connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public async Task<int> AddGrupAsync(int yil, int ay, string sube, string donemAdi, string grupAdi, DateTime baslangic, DateTime bitis)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"INSERT INTO GRUP_KARTI 
                               (DONEM_YILI, DONEM_AYI, DONEM_SUBESI, DONEM_ADI, DONEM_GRUBU, BAS_TAR, BIT_TAR)
                               VALUES (@Yil, @Ay, @Sube, @DonemAdi, @GrupAdi, @Baslangic, @Bitis);
                               SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Yil", yil);
                    command.Parameters.AddWithValue("@Ay", ay);
                    command.Parameters.AddWithValue("@Sube", sube);
                    command.Parameters.AddWithValue("@DonemAdi", donemAdi);
                    command.Parameters.AddWithValue("@GrupAdi", grupAdi);
                    command.Parameters.AddWithValue("@Baslangic", baslangic);
                    command.Parameters.AddWithValue("@Bitis", bitis);

                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task<int> UpdateGrupAsync(int id, string donemAdi, string grupAdi, DateTime baslangic, DateTime bitis)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"UPDATE GRUP_KARTI 
                               SET DONEM_ADI=@DonemAdi, DONEM_GRUBU=@GrupAdi, BAS_TAR=@Baslangic, BIT_TAR=@Bitis
                               WHERE ID=@Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@DonemAdi", donemAdi);
                    command.Parameters.AddWithValue("@GrupAdi", grupAdi);
                    command.Parameters.AddWithValue("@Baslangic", baslangic);
                    command.Parameters.AddWithValue("@Bitis", bitis);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteGrupAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DELETE FROM GRUP_KARTI WHERE ID=@Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
