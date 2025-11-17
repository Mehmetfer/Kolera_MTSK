using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
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
        public static async Task<SqlConnection> AuthenticateAndGetConnectionAsync(string connectionString, string kullaniciAdi, string parola, bool passwordsAreHashed = false)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string boş olamaz.", nameof(connectionString));

            // Kullanıcı adı veya parola boş ise null döner
            if (string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(parola))
                return null;

            try
            {
                var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                string passwordToCheck = passwordsAreHashed ? parola : HashPassword(parola);

                string sql = "SELECT TOP 1 1 FROM KULLANICI WHERE KULLANICI_ADI = @kadi AND KULLANICI_SIFRE = @sifre";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@kadi", SqlDbType.NVarChar).Value = kullaniciAdi;
                    cmd.Parameters.Add("@sifre", SqlDbType.NVarChar).Value = passwordToCheck;

                    var res = await cmd.ExecuteScalarAsync();
                    if (res != null)
                    {
                        // Giriş doğrulandı -> açık bağlantıyı döndür
                        return conn;
                    }
                }

                // Doğrulama başarısız -> bağlantıyı kapat ve null döndür
                conn.Close();
                conn.Dispose();
                return null;
            }
            catch (Exception ex)
            {
                // Hata durumunda dışarıya fırlatabilir veya null döndürebilirsin.
                throw new InvalidOperationException("Veritabanı doğrulaması sırasında bir hata oluştu.", ex);
            }
        }

        /// <summary>
        /// Şifreyi hash'ler (SHA256 kullanarak)
        /// </summary>
        private static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
