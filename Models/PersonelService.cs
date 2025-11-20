using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Services
{
    public class PersonelService
    {
        private readonly string _connectionString;

        public PersonelService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Personel_Model>> GetPersonellerAsync()
        {
            var liste = new List<Personel_Model>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM PERSONEL";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            var p = new Personel_Model
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                PERSONEL_DURUMU = reader["PERSONEL_DURUMU"].ToString(),
                                TC_NO = reader["TC_NO"].ToString(),
                                ADI = reader["ADI"].ToString(),
                                SOYADI = reader["SOYADI"].ToString(),
                                EHLIYET_SINIFI = reader["EHLIYET_SINIFI"].ToString(),
                                EHLIYET_IKINCI = reader["EHLIYET_IKINCI"].ToString(),
                                CINSIYET = reader["CINSIYET"].ToString(),
                                MEDENI_DUR = reader["MEDENI_DUR"].ToString(),
                                DOGUM_TARIHI = reader["DOGUM_TARIHI"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DOGUM_TARIHI"]),
                                YONETICI_GOREVI = reader["YONETICI_GOREVI"].ToString(),
                                VERDIGI_DERS_1 = reader["VERDIGI_DERS_1"].ToString(),
                                SOZ_BASLAMA_TAR = reader["SOZ_BASLAMA_TAR"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["SOZ_BASLAMA_TAR"]),
                                RESIM = reader["RESIM"] == DBNull.Value ? null : (byte[])reader["RESIM"]
                            };
                            liste.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw new InvalidOperationException("Personeller yüklenirken hata oluştu.");
            }

            return liste;
        }

        public async Task AddPersonelAsync(Personel_Model p)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"
                        INSERT INTO PERSONEL
                        (PERSONEL_DURUMU, TC_NO, ADI, SOYADI, EHLIYET_SINIFI, EHLIYET_IKINCI, CINSIYET, MEDENI_DUR,
                         DOGUM_TARIHI, YONETICI_GOREVI, VERDIGI_DERS_1, SOZ_BASLAMA_TAR, RESIM)
                        VALUES
                        (@PERSONEL_DURUMU, @TC_NO, @ADI, @SOYADI, @EHLIYET_SINIFI, @EHLIYET_IKINCI, @CINSIYET, @MEDENI_DUR,
                         @DOGUM_TARIHI, @YONETICI_GOREVI, @VERDIGI_DERS_1, @SOZ_BASLAMA_TAR, @RESIM)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PERSONEL_DURUMU", p.PERSONEL_DURUMU ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TC_NO", p.TC_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADI", p.ADI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SOYADI", p.SOYADI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EHLIYET_SINIFI", p.EHLIYET_SINIFI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EHLIYET_IKINCI", p.EHLIYET_IKINCI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CINSIYET", p.CINSIYET ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MEDENI_DUR", p.MEDENI_DUR ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOGUM_TARIHI", p.DOGUM_TARIHI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@YONETICI_GOREVI", p.YONETICI_GOREVI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERDIGI_DERS_1", p.VERDIGI_DERS_1 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SOZ_BASLAMA_TAR", p.SOZ_BASLAMA_TAR ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RESIM", p.RESIM ?? (object)DBNull.Value);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw new InvalidOperationException("Personel eklenirken hata oluştu.");
            }
        }

        public async Task UpdatePersonelAsync(Personel_Model p)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = @"
                        UPDATE PERSONEL SET
                        PERSONEL_DURUMU=@PERSONEL_DURUMU,
                        TC_NO=@TC_NO,
                        ADI=@ADI,
                        SOYADI=@SOYADI,
                        EHLIYET_SINIFI=@EHLIYET_SINIFI,
                        EHLIYET_IKINCI=@EHLIYET_IKINCI,
                        CINSIYET=@CINSIYET,
                        MEDENI_DUR=@MEDENI_DUR,
                        DOGUM_TARIHI=@DOGUM_TARIHI,
                        YONETICI_GOREVI=@YONETICI_GOREVI,
                        VERDIGI_DERS_1=@VERDIGI_DERS_1,
                        SOZ_BASLAMA_TAR=@SOZ_BASLAMA_TAR,
                        RESIM=@RESIM
                        WHERE ID=@ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", p.ID);
                        cmd.Parameters.AddWithValue("@PERSONEL_DURUMU", p.PERSONEL_DURUMU ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TC_NO", p.TC_NO ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ADI", p.ADI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SOYADI", p.SOYADI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EHLIYET_SINIFI", p.EHLIYET_SINIFI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EHLIYET_IKINCI", p.EHLIYET_IKINCI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CINSIYET", p.CINSIYET ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MEDENI_DUR", p.MEDENI_DUR ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOGUM_TARIHI", p.DOGUM_TARIHI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@YONETICI_GOREVI", p.YONETICI_GOREVI ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@VERDIGI_DERS_1", p.VERDIGI_DERS_1 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SOZ_BASLAMA_TAR", p.SOZ_BASLAMA_TAR ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@RESIM", p.RESIM ?? (object)DBNull.Value);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw new InvalidOperationException("Personel güncellenirken hata oluştu.");
            }
        }

        public async Task DeletePersonelAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM PERSONEL WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw new InvalidOperationException("Personel silinirken hata oluştu.");
            }
        }
    }
}
