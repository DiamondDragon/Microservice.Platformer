using System.Threading.Tasks;
using Microservice.Platformer.DataLayer;

namespace Microservice.Platformer.Services
{
    public interface IBulkImportService
    {
        void GetData();
        void AddData();
        Task<NewBulkImport[]> GetDataAsync();
        Task AddDataAsync();
    }
}