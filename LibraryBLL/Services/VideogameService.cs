using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryBLL.Interfaces;
using LibraryDAL.Interfaces;
using CPRG211_Final_Library.Entities;

namespace LibraryBLL.Services
{
    public class VideogameService : IVideogameService
    {
        private readonly IVideogameRepository _videogameRepository;

        public VideogameService(IVideogameRepository videogameRepository)
        {
            _videogameRepository = videogameRepository;
        }

        public async Task<IEnumerable<Videogame>> GetAllVideogamesAsync()
        {
            return await _videogameRepository.GetAllVideogamesAsync();
        }

        public async Task<Videogame> GetVideogameByIdAsync(int id)
        {
            var videogame = await _videogameRepository.GetVideogameByIdAsync(id);

            if (videogame == null)
            {
                throw new KeyNotFoundException($"No Videogame found with ID {id}");
            }

            return videogame;
        }

        public async Task AddVideogameAsync(Videogame videogame)
        {
            await _videogameRepository.AddVideogameAsync(videogame);
        }

        public async Task UpdateVideogameAsync(Videogame videogame)
        {
            var existingVideogame = await _videogameRepository.GetVideogameByIdAsync(videogame.ID);
            if (existingVideogame == null)
            {
                throw new KeyNotFoundException($"No Videogame found with ID {videogame.ID}");
            }

            await _videogameRepository.UpdateVideogameAsync(videogame);
        }

        public async Task DeleteVideogameAsync(int id)
        {
            var existingVideogame = await _videogameRepository.GetVideogameByIdAsync(id);
            if (existingVideogame == null)
            {
                throw new KeyNotFoundException($"No Videogame found with ID {id}");
            }

            await _videogameRepository.DeleteVideogameAsync(id);
        }
    }
}
