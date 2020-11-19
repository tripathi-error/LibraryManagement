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
    public class UserActionDbRepository : IUserActionRepository
    {
        private readonly LibraryDbContext _libraryDbContext;
        public UserActionDbRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public void AddBookToUserRecord(UserBooksRecord userBooksRecord)
        {
            _libraryDbContext.Add(userBooksRecord);
        }

        public void EditUserBookRecord(UserBooksRecord userBooksRecord) {
            var bookDetail = _libraryDbContext.UserBooksRecords.Find(userBooksRecord.ApplicationUser.Id);
            if (bookDetail != null)
            {
                bookDetail.IsFavorite = userBooksRecord.IsFavorite;
                bookDetail.IsRead = userBooksRecord.IsRead;
                _libraryDbContext.Update(bookDetail);
            }
        }

        public void PostReview(BookReview bookReview)
        {
            _libraryDbContext.Add(bookReview);
        }

        public async Task<UserBooksRecord> GetUserRecordByBookId(int bookId, string userId) {
            return await _libraryDbContext.UserBooksRecords.Where(s => s.BookId == bookId && s.ApplicationUser.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _libraryDbContext.SaveChangesAsync() > 0;
        }

    }
}
