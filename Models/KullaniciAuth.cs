using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Tarantula_MTSK.Services
{
    public static class KullaniciAuth
    {
        /// <summary>
        /// Basit doğrulama: Eğer kullanıcı/şifre eşleşirse açık SqlConnection döner, değilse null.
        /// Caller dönen SqlConnection'ı dispose etmelidir.
        /// </summary>
        public static async Task<SqlConnection> AuthenticateAndGetConnectionAsync(
            string connectionString, string kullaniciAdi, string parola)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string boş olamaz.", nameof(connectionString));

            if (string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(parola))
                return null;

            try
            {
                var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string passwordToCheck = EncodeBase64(parola);

                string sql = "SELECT TOP 1 1 FROM KULLANICI WHERE KULLANICI_ADI = @kadi AND KULLANICI_SIFRE = @sifre";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@kadi", SqlDbType.NVarChar).Value = kullaniciAdi;
                    cmd.Parameters.Add("@sifre", SqlDbType.NVarChar).Value = passwordToCheck;

                    var res = await cmd.ExecuteScalarAsync();
                    if (res != null)
                    {
                        return conn; // Giriş başarılı, açık connection dön
                    }
                }

                conn.Close();
                conn.Dispose();
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Veritabanı doğrulaması sırasında bir hata oluştu.", ex);
            }
        }

        /// <summary>
        /// Şifreyi Base64 formatına çevirir
        /// </summary>
        public static string EncodeBase64(string input)
        {
            if (input == null) return "";
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }
    }
}
