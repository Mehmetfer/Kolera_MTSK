using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Services
{
    public class AracService
    {
        private readonly string _connectionString;

        public AracService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Arac_Model>> GetAraclarAsync()
        {
            var liste = new List<Arac_Model>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT ID, ARAC_TIPI, ARAC_PLAKA, DURUMU, RENGI, VITES_TURU, MODEL, MUHAYENE_TAR, SIGORTA_BAS_TAR, AKT FROM PARAM_ARAC_TANIMLARI";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            var arac = new Arac_Model
                            {
                                ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                                ARAC_TIPI = reader["ARAC_TIPI"] != DBNull.Value ? reader["ARAC_TIPI"].ToString() : "",
                                ARAC_PLAKA = reader["ARAC_PLAKA"] != DBNull.Value ? reader["ARAC_PLAKA"].ToString() : "",
                                DURUMU = reader["DURUMU"] != DBNull.Value ? reader["DURUMU"].ToString() : "",
                                RENGI = reader["RENGI"] != DBNull.Value ? reader["RENGI"].ToString() : "",
                                VITES_TURU = reader["VITES_TURU"] != DBNull.Value ? reader["VITES_TURU"].ToString() : "",
                                MODEL = reader["MODEL"] != DBNull.Value ? reader["MODEL"].ToString() : "",
                                MUHAYENE_TAR = reader["MUHAYENE_TAR"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MUHAYENE_TAR"]) : null,
                                SIGORTA_BAS_TAR = reader["SIGORTA_BAS_TAR"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["SIGORTA_BAS_TAR"]) : null,
                                AKT = reader["AKT"] != DBNull.Value ? Convert.ToInt32(reader["AKT"]) : 0
                            };
                            liste.Add(arac);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw new InvalidOperationException("Araçlar yüklenirken bir hata oluştu.");
            }

            return liste;
        }

        public async Task AddAracAsync(Arac_Model arac)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"INSERT INTO PARAM_ARAC_TANIMLARI
                        (ARAC_TIPI, ARAC_PLAKA, DURUMU, RENGI, VITES_TURU, MODEL, MUHAYENE_TAR, SIGORTA_BAS_TAR, AKT)
                        VALUES (@ARAC_TIPI, @ARAC_PLAKA, @DURUMU, @RENGI, @VITES_TURU, @MODEL, @MUHAYENE_TAR, @SIGORTA_BAS_TAR, @AKT)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ARAC_TIPI", string.IsNullOrEmpty(arac.ARAC_TIPI) ? (object)DBNull.Value : arac.ARAC_TIPI);
                        cmd.Parameters.AddWithValue("@ARAC_PLAKA", string.IsNullOrEmpty(arac.ARAC_PLAKA) ? (object)DBNull.Value : arac.ARAC_PLAKA);
                        cmd.Parameters.AddWithValue("@DURUMU", string.IsNullOrEmpty(arac.DURUMU) ? (object)DBNull.Value : arac.DURUMU);
                        cmd.Parameters.AddWithValue("@RENGI", string.IsNullOrEmpty(arac.RENGI) ? (object)DBNull.Value : arac.RENGI);
                        cmd.Parameters.AddWithValue("@VITES_TURU", string.IsNullOrEmpty(arac.VITES_TURU) ? (object)DBNull.Value : arac.VITES_TURU);
                        cmd.Parameters.AddWithValue("@MODEL", string.IsNullOrEmpty(arac.MODEL) ? (object)DBNull.Value : arac.MODEL);
                        cmd.Parameters.AddWithValue("@MUHAYENE_TAR", arac.MUHAYENE_TAR.HasValue ? (object)arac.MUHAYENE_TAR.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@SIGORTA_BAS_TAR", arac.SIGORTA_BAS_TAR.HasValue ? (object)arac.SIGORTA_BAS_TAR.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@AKT", arac.AKT);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw new InvalidOperationException("Araç eklenirken bir hata oluştu.");
            }
        }

        public async Task UpdateAracAsync(Arac_Model arac)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"UPDATE PARAM_ARAC_TANIMLARI SET
                        ARAC_TIPI=@ARAC_TIPI,
                        ARAC_PLAKA=@ARAC_PLAKA,
                        DURUMU=@DURUMU,
                        RENGI=@RENGI,
                        VITES_TURU=@VITES_TURU,
                        MODEL=@MODEL,
                        MUHAYENE_TAR=@MUHAYENE_TAR,
                        SIGORTA_BAS_TAR=@SIGORTA_BAS_TAR,
                        AKT=@AKT
                        WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ARAC_TIPI", string.IsNullOrEmpty(arac.ARAC_TIPI) ? (object)DBNull.Value : arac.ARAC_TIPI);
                        cmd.Parameters.AddWithValue("@ARAC_PLAKA", string.IsNullOrEmpty(arac.ARAC_PLAKA) ? (object)DBNull.Value : arac.ARAC_PLAKA);
                        cmd.Parameters.AddWithValue("@DURUMU", string.IsNullOrEmpty(arac.DURUMU) ? (object)DBNull.Value : arac.DURUMU);
                        cmd.Parameters.AddWithValue("@RENGI", string.IsNullOrEmpty(arac.RENGI) ? (object)DBNull.Value : arac.RENGI);
                        cmd.Parameters.AddWithValue("@VITES_TURU", string.IsNullOrEmpty(arac.VITES_TURU) ? (object)DBNull.Value : arac.VITES_TURU);
                        cmd.Parameters.AddWithValue("@MODEL", string.IsNullOrEmpty(arac.MODEL) ? (object)DBNull.Value : arac.MODEL);
                        cmd.Parameters.AddWithValue("@MUHAYENE_TAR", arac.MUHAYENE_TAR.HasValue ? (object)arac.MUHAYENE_TAR.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@SIGORTA_BAS_TAR", arac.SIGORTA_BAS_TAR.HasValue ? (object)arac.SIGORTA_BAS_TAR.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@AKT", arac.AKT);
                        cmd.Parameters.AddWithValue("@ID", arac.ID);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw new InvalidOperationException("Araç güncellenirken bir hata oluştu.");
            }
        }

        public async Task DeleteAracAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM PARAM_ARAC_TANIMLARI WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw new InvalidOperationException("Araç silinirken bir hata oluştu.");
            }
        }

        private void LogError(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
