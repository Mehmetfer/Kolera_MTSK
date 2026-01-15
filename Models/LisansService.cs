using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tarantula_MTSK.Services
{
    public class LisansService
    {
        private readonly string _connectionString;
        private const string lisansUrl = "http://mehmetfer.com.tr/lisans.txt";

        public LisansService(string cs)
        {
            _connectionString = cs;
        }

        // Online lisans artık tamamen async ve engelleyici değil
        public async Task<LisansModel> GetOnlineLisansAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string content = await client.GetStringAsync(lisansUrl); // await ile network çağrısı beklenir

                    string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        if (!line.Contains("|")) continue;

                        var p = line.Split('|');

                        if (p.Length < 6) continue;

                        return new LisansModel
                        {
                            Firma = p[0],
                            LisansNo = p[1],
                            BitisTarihi = p[2],
                            ProgramAdi = p[3],
                            Versiyon = p[4],
                            Durum = p[5]
                        };
                    }
                }

                return null;
            }
            catch
            {
                return null; // İnternet yoksa null dönecek
            }
        }

        public async Task<string> GetLisansNoFromDBAsync()
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                SqlCommand cmd = new SqlCommand("SELECT LSN_LISANS_NO FROM PARAM_SETTINGS", cn);

                object o = await cmd.ExecuteScalarAsync();
                return o == null ? "" : o.ToString();
            }
        }

        public async Task UpdateLisansExpireDateAsync(string date)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE PARAM_SETTINGS SET LSN_BITIS_TARIHI = @t", cn);

                cmd.Parameters.AddWithValue("@t", date);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public class LisansModel
    {
        public string Firma { get; set; }
        public string LisansNo { get; set; }
        public string BitisTarihi { get; set; }
        public string ProgramAdi { get; set; }
        public string Versiyon { get; set; }
        public string Durum { get; set; }
    }
}
