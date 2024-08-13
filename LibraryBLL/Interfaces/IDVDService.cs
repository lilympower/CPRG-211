using System.Collections.Generic;
using System.Threading.Tasks;
using CPRG211_Final_Library.Entities;

namespace LibraryBLL.Interfaces
{
    public interface IDVDService
    {
        Task<IEnumerable<DVD>> GetAllDVDsAsync();
        Task<DVD> GetDVDByIdAsync(int id);
        Task AddDVDAsync(DVD dvd);
        Task UpdateDVDAsync(DVD dvd);
        Task DeleteDVDAsync(int id);
    }
}
