using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tarantula_MTSK.Services
{
    public class KullaniciService
    {
        private readonly string _connectionString;

        public KullaniciService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("ConnectionString BOŞ!");
            _connectionString = connectionString;
        }

        /// <summary>
        /// Kullanıcı adlarını getir
        /// </summary>
        public async Task<List<string>> GetKullaniciAdlariAsync()
        {
            List<string> liste = new List<string>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string sql = "SELECT KULLANICI_ADI FROM KULLANICI";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        if (!dr.IsDBNull(0))
                            liste.Add(dr.GetString(0));
                    }
                }
            }

            return liste;
        }

        /// <summary>
        /// Kullanıcı + parola kontrolü
        /// </summary>
        public async Task<bool> IsValidKullanici(string kullaniciAdi, string parola)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();

                    // 🔥 ADMİN özel durumu (şifre boşsa)
                    if (kullaniciAdi.ToUpper() == "ADMİN" && string.IsNullOrWhiteSpace(parola))
                    {
                        string adminSql =
                            "SELECT COUNT(*) FROM KULLANICI WHERE KULLANICI_ADI = 'ADMİN'";

                        using (SqlCommand adminCmd = new SqlCommand(adminSql, con))
                        {
                            int adminCount = (int)await adminCmd.ExecuteScalarAsync();
                            return adminCount > 0;
                        }
                    }

                    // 🔐 Normal kullanıcılar (Base64 kontrol)
                    string sql =
                        @"SELECT COUNT(*) 
                  FROM KULLANICI
                  WHERE KULLANICI_ADI=@u
                  AND KULLANICI_SIFRE=@p;";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@u", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@p", KullaniciAuth.EncodeBase64(parola));

                        int sonuc = (int)await cmd.ExecuteScalarAsync();
                        return sonuc > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }

}


