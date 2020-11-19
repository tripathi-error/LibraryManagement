using LibraryManagementAPIServices.Interfaces;
using LibraryManagmentDataAccessLayer.Interfaces;
using LibraryManagmentDomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementAPIServices.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetBooks() {
            return await _bookRepository.GetAllBooks();
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            return await _bookRepository.GetBookAsync(bookId);
        }
        public void AddBook(Book book) {
            _bookRepository.AddBook(book);
        }

        public void EditBook(Book book) {
            _bookRepository.EditBook(book);
        }

        public void DeleteBook(Book book)
        {
            _bookRepository.DeleteBook(book);
        }

        public async Task<List<BookReview>> GetBookReviews(int bookId) {
            return await _bookRepository.GetBookReviews(bookId);
        }

        public async Task<bool> SaveAsync() {
            return await _bookRepository.SaveAsync();
        }
    }
}
