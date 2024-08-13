using System.Collections.Generic;
using System.Threading.Tasks;
using CPRG211_Final_Library.Entities;

namespace LibraryBLL.Interfaces
{
    public interface IVideogameService
    {
        Task<IEnumerable<Videogame>> GetAllVideogamesAsync();
        Task<Videogame> GetVideogameByIdAsync(int id);
        Task AddVideogameAsync(Videogame videogame);
        Task UpdateVideogameAsync(Videogame videogame);
        Task DeleteVideogameAsync(int id);
    }
}
