using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPRG211_Final_Library.Entities;

namespace LibraryDAL.Interfaces
{
    public interface IDVDRepository
    {
        Task<IEnumerable<DVD>> GetAllDVDsAsync();
        Task<DVD> GetDVDByIdAsync(int id);
        Task AddDVDAsync(DVD dvd);
        Task UpdateDVDAsync(DVD dvd);
        Task DeleteDVDAsync(int id);
    }
}
