using LibraryManagmentDataAccessLayer.Interfaces;
using LibraryManagmentDomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentDataAccessLayer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext;
        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public async Task<List<Book>> GetAllBooks()
        {
            return await _libraryDbContext.Books.Select(b => b).ToListAsync();
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            return await _libraryDbContext.Books.Where(s=> s.Id == bookId).FirstOrDefaultAsync();
        }
        public void AddBook(Book book)
        {
            _libraryDbContext.Books.Add(book);
        }



        public void EditBook(Book book)
        {
            var bookDetail = _libraryDbContext.Books.Find(book.Id);
            bookDetail.Title = book.Title;
            bookDetail.Author = book.Author;
            bookDetail.Description = book.Description;
            _libraryDbContext.Update(bookDetail);

        }

        public void DeleteBook(Book book)
        {
            //Need to remove reviews first
            var reviews = _libraryDbContext.BookReviews.Where(s => s.BookId == book.Id).ToList();
            _libraryDbContext.RemoveRange(reviews);

            var userRecordByBook = _libraryDbContext.UserBooksRecords.Where(s => s.BookId == book.Id).ToList();
            _libraryDbContext.RemoveRange(userRecordByBook);

            //remove book
            _libraryDbContext.Remove(book);
        }

        public async Task<List<BookReview>> GetBookReviews(int bookId) {
            return await _libraryDbContext.BookReviews.Where(s => s.BookId == bookId).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _libraryDbContext.SaveChangesAsync() > 0;
        }


    }
}
