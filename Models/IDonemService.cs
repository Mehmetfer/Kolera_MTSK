using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Tarantula_MTSK.Services
{
    public interface IDonemService
    {
        Task<List<string>> GetDonemListAsync();
        Task<string> GetKursiyerDonemiAsync(int kursiyerId);

        Task<(List<string> Donemler, string KursiyerDonemi)> GetDonemlerVeKursiyerDonemiAsync(int kursiyerId);

        Task<DataTable> GetGrupKartlariAsync();

        // 7 parametreli Add
        Task<int> AddGrupAsync(int yil, int ay, string sube, string donemAdi, string grupAdi, DateTime baslangic, DateTime bitis);

        Task<int> UpdateGrupAsync(int id, string donemAdi, string grupAdi, DateTime baslangic, DateTime bitis);
        Task<int> DeleteGrupAsync(int id);
    }
}
