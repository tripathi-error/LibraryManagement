using LibraryManagmentDomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementAPIServices.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks();
        void AddBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
        Task<Book> GetBookAsync(int bookId);

        Task<List<BookReview>> GetBookReviews(int bookId);
        Task<bool> SaveAsync();


    }
}
