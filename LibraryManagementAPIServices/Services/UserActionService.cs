using LibraryManagementAPIServices.Interfaces;
using LibraryManagmentDataAccessLayer.Interfaces;
using LibraryManagmentDomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementAPIServices.Services
{
    public class UserActionService : IUserActionService
    {
        private readonly IUserActionRepository _userActionRepository;
        public UserActionService(IUserActionRepository userActionRepository) {
            _userActionRepository = userActionRepository;
        }
        public void AddBookToUserRecord(UserBooksRecord userBooksRecord)
        {
            _userActionRepository.AddBookToUserRecord(userBooksRecord);

        }

        public void EditUserBookRecord(UserBooksRecord userBooksRecord) {
            _userActionRepository.EditUserBookRecord(userBooksRecord);
        }

        public void PostReview(BookReview bookReview)
        {
            _userActionRepository.PostReview(bookReview);

        }

        public async Task<UserBooksRecord> GetUserRecordByBookId(int bookId, string userId)
        {
            return await _userActionRepository.GetUserRecordByBookId(bookId, userId);
        }

        public async Task<bool> SaveAsync(){
            return await _userActionRepository.SaveAsync();
        }

    } 
}
