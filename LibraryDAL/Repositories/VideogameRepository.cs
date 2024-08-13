using CPRG211_Final_Library.Entities;
using LibraryDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace LibraryDAL.Repositories
{
    internal class VideogameRepository : IVideogameRepository
    {
        private readonly string _connectionString;
        public VideogameRepository(string connectionString)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "password",
                Database = "final"
            };

            _connectionString = builder.ConnectionString;
        }
        public async Task AddVideogameAsync(Videogame game)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("INSERT INTO Videogames (Title, Author, ISBN, Platform) VALUES (@Title, @Author, @ISBN, @Platform)", connection))
                {
                    command.Parameters.AddWithValue("@Title", game.Title);
                    command.Parameters.AddWithValue("@Author", game.Author);
                    command.Parameters.AddWithValue("@ISBN", game.ISBN);
                    command.Parameters.AddWithValue("@Duration", game.Platform);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteVideogameAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("DELETE FROM Videogames WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<Videogame>> GetAllVideogamesAsync()
        {
            var games = new List<Videogame>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("SELECT * FROM Videogames", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            games.Add(new Videogame
                            {
                                ID = reader.GetInt32("Id"),
                                Platform = reader.GetString("Platform"),
                                Title = reader.GetString("Title"),
                                Author = reader.GetString("Author"),
                                ISBN = reader.GetString("ISBN")
                            });
                        }
                    }
                }
            }

            return games;
        }

        public async Task<Videogame> GetVideogameByIdAsync(int id)
        {
            Videogame game = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("SELECT * FROM Videogamess WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            game = new Videogame
                            {
                                ID = reader.GetInt32("Id"),
                                Platform = reader.GetString("Platform"),
                                Title = reader.GetString("Title"),
                                Author = reader.GetString("Author"),
                                ISBN = reader.GetString("ISBN")
                            };
                        }
                    }
                }
            }

            return game;
        }

        public async Task UpdateVideogameAsync(Videogame game)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("UPDATE Videogames SET Title = @Title, Author = @Author, ISBN = @ISBN, Platform = @Platform WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Title", game.Title);
                    command.Parameters.AddWithValue("@Author", game.Author);
                    command.Parameters.AddWithValue("@ISBN", game.ISBN);
                    command.Parameters.AddWithValue("@Platform", game.Platform);
                    command.Parameters.AddWithValue("@Id", game.ID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
