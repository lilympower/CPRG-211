using LibraryDAL.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CPRG211_Final_Library.Entities;

namespace LibraryDAL.Repositories
{
    internal class DVDRepository : IDVDRepository
    {
        private readonly string _connectionString;

        public DVDRepository(string connectionString)
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
        public async Task AddDVDAsync(DVD dvd)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("INSERT INTO DVDs (Title, Author, ISBN, Duration) VALUES (@Title, @Author, @ISBN, @Duration)", connection))
                {
                    command.Parameters.AddWithValue("@Title", dvd.Title);
                    command.Parameters.AddWithValue("@Author", dvd.Author);
                    command.Parameters.AddWithValue("@ISBN", dvd.ISBN);
                    command.Parameters.AddWithValue("@Duration", dvd.Duration);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteDVDAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("DELETE FROM DVDs WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<DVD>> GetAllDVDsAsync()
        {
            var dvds = new List<DVD>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("SELECT * FROM DVDs", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            dvds.Add(new DVD
                            {
                                ID = reader.GetInt32("Id"),
                                Duration = reader.GetInt32("Duration"),
                                Title = reader.GetString("Title"),
                                Author = reader.GetString("Author"),
                                ISBN = reader.GetString("ISBN")
                            });
                        }
                    }
                }
            }

            return dvds;
        }

        public async Task<DVD> GetDVDByIdAsync(int id)
        {
            DVD dvd = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("SELECT * FROM DVDs WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dvd = new DVD
                            {
                                ID = reader.GetInt32("Id"),
                                Duration = reader.GetInt32("Duration"),
                                Title = reader.GetString("Title"),
                                Author = reader.GetString("Author"),
                                ISBN = reader.GetString("ISBN")
                            };
                        }
                    }
                }
            }

            return dvd;
        }

        public async Task UpdateDVDAsync(DVD dvd)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("UPDATE DVDs SET Title = @Title, Author = @Author, ISBN = @ISBN, Duration = @Duration WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Title", dvd.Title);
                    command.Parameters.AddWithValue("@Author", dvd.Author);
                    command.Parameters.AddWithValue("@ISBN", dvd.ISBN);
                    command.Parameters.AddWithValue("@Duration", dvd.Duration);
                    command.Parameters.AddWithValue("@Id", dvd.ID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
