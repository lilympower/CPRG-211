using LibraryDAL.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CPRG211_Final_Library.Entities;

namespace LibraryDAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
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

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = new List<Book>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("SELECT * FROM Books", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new Book
                            {
                                ID = reader.GetInt32("id"),
                                NumberOfPages = reader.GetInt32("Number of pages"),
                                Title = reader.GetString("Title"),
                                Author = reader.GetString("Author"),
                                ISBN = reader.GetString("ISBN")
                            });
                        }
                    }
                }
            }

            return books;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            Book book = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("SELECT * FROM Books WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            book = new Book
                            {
                                ID = reader.GetInt32("Id"),
                                NumberOfPages = reader.GetInt32("Number of pages"),
                                Title = reader.GetString("Title"),
                                Author = reader.GetString("Author"),
                                ISBN = reader.GetString("ISBN")
                            };
                        }
                    }
                }
            }

            return book;
        }

        public async Task AddBookAsync(Book book)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("INSERT INTO Books (Title, Author, ISBN, NumberOfPages) VALUES (@Title, @Author, @ISBN, @NumberOfPages)", connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@NumberOfPages", book.NumberOfPages);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("UPDATE Books SET Title = @Title, Author = @Author, ISBN = @ISBN, NumberOfPages = @NumberOfPages WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@NumberOfPages", book.NumberOfPages);
                    command.Parameters.AddWithValue("@Id", book.ID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("DELETE FROM Books WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
