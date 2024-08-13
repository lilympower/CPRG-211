using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryBLL.Interfaces;
using LibraryDAL.Interfaces;
using CPRG211_Final_Library.Entities;

namespace LibraryBLL.Services
{
    public class DVDService : IDVDService
    {
        private readonly IDVDRepository _dvdRepository;

        public DVDService(IDVDRepository dvdRepository)
        {
            _dvdRepository = dvdRepository;
        }

        public async Task<IEnumerable<DVD>> GetAllDVDsAsync()
        {
            return await _dvdRepository.GetAllDVDsAsync();
        }

        public async Task<DVD> GetDVDByIdAsync(int id)
        {
            var dvd = await _dvdRepository.GetDVDByIdAsync(id);

            if (dvd == null)
            {
                throw new KeyNotFoundException($"No DVD found with ID {id}");
            }

            return dvd;
        }

        public async Task AddDVDAsync(DVD dvd)
        {
            await _dvdRepository.AddDVDAsync(dvd);
        }

        public async Task UpdateDVDAsync(DVD dvd)
        {
            var existingDVD = await _dvdRepository.GetDVDByIdAsync(dvd.ID);
            if (existingDVD == null)
            {
                throw new KeyNotFoundException($"No DVD found with ID {dvd.ID}");
            }

            await _dvdRepository.UpdateDVDAsync(dvd);
        }

        public async Task DeleteDVDAsync(int id)
        {
            var existingDVD = await _dvdRepository.GetDVDByIdAsync(id);
            if (existingDVD == null)
            {
                throw new KeyNotFoundException($"No DVD found with ID {id}");
            }

            await _dvdRepository.DeleteDVDAsync(id);
        }
    }
}
