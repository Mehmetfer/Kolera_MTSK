using System;
using System.Data.SqlClient;  // 👈 Eklendi
using Tarantula_MTSK.Models;

namespace Tarantula_MTSK.Models
{
    public class LisansService
    {
        private readonly string _connectionString;

        public LisansService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Lisans_Model GetLisans()
        {
            Lisans_Model model = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT LSN_LISANS_NO, LSN_BITIS_TARIHI, LSN_GUID_SFR, LSN_MAKINE_SAYISI 
                                 FROM PARAM_SETTINGS";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        model = new Lisans_Model
                        {
                            LisansNo = dr["LSN_LISANS_NO"].ToString(),
                            BitisTarihi = dr["LSN_BITIS_TARIHI"].ToString(),
                            GuidSfr = dr["LSN_GUID_SFR"].ToString(),
                            MakineSayisi = dr["LSN_MAKINE_SAYISI"] != DBNull.Value
                                ? Convert.ToInt32(dr["LSN_MAKINE_SAYISI"])
                                : 0
                        };
                    }
                }
            }

            return model;
        }
    }
}
