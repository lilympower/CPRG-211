using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryBLL.Interfaces;
using LibraryDAL.Interfaces;
using CPRG211_Final_Library.Entities;

namespace LibraryBLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"No book found with ID {id}");
            }

            return book;
        }

        public async Task AddBookAsync(Book book)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(book.ID);
            if (existingBook != null)
            {
                throw new InvalidOperationException($"Book with ISBN {book.ISBN} already exists.");
            }

            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(book.ID);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"No book found with ID {book.ID}");
            }

            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"No book found with ID {id}");
            }

            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
