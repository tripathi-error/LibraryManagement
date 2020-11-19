﻿using LibraryManagmentDomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentDataAccessLayer.Interfaces
{
    public interface IUserActionRepository
    {
        void AddBookToUserRecord(UserBooksRecord userBooksRecord);
        Task<UserBooksRecord> GetUserRecordByBookId(int bookId, string userId);
        void EditUserBookRecord(UserBooksRecord userBooksRecord);
        void PostReview(BookReview bookReview);
        Task<bool> SaveAsync();
    }
}
