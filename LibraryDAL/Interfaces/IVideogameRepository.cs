using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPRG211_Final_Library.Entities;

namespace LibraryDAL.Interfaces
{
    public interface IVideogameRepository
    {
        Task<IEnumerable<Videogame>> GetAllVideogamesAsync();
        Task<Videogame> GetVideogameByIdAsync(int id);
        Task AddVideogameAsync(Videogame videoGame);
        Task UpdateVideogameAsync(Videogame videoGame);
        Task DeleteVideogameAsync(int id);
    }
}
