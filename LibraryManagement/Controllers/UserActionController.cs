using AutoMapper;
using LibraryManagementAPIServices.Interfaces;
using LibraryManagmentDomainLayer.Entities;
using LibraryManagmentDomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserActionController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IUserActionService _userActionService;

        public UserActionController(UserManager<ApplicationUser> userManager, IMapper mapper, IUserActionService userActionService, IBookService bookService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userActionService = userActionService;
            _bookService = bookService;
        }
        // GET: /<controller>/
        [HttpPost("AddBookToUserRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddBookToUserRecord([FromBody]UsersBookRecordDto bookRecord)
        {
            // System.Security.Claims.ClaimsPrincipal currentUser = this.User;


            //  var user = await _userManager.GetUserAsync(HttpContext.User);
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == "Betsy@gmail.com");

            var book = await _bookService.GetBookAsync(bookRecord.BookId);
            if (book == null)
            {
                return NotFound($"The book with the given id {bookRecord.BookId} is not present");
            }

            var bookDto = new UsersBookRecordDto
            {
                BookId = bookRecord.BookId,
                IsFaviourite = bookRecord.IsFaviourite,
                IsRead = bookRecord.IsRead
            };

            var userBookDetail = await _userActionService.GetUserRecordByBookId(bookRecord.BookId, appUser.Id);
            if (userBookDetail == null)
            {
                // Add book to user record

                var userBook = new UserBooksRecord()
                {
                    BookId = bookRecord.BookId,
                    IsFavorite= bookRecord.IsFaviourite,
                    IsRead = bookRecord.IsRead,
                    ApplicationUser = appUser
                };
                _userActionService.AddBookToUserRecord(userBook);
                if (!await _userActionService.SaveAsync())
                {
                    throw new Exception("Failed to add record. Please try after some time");
                }
                return Ok();
            }
            else
            {
                var userBook = new UserBooksRecord()
                {
                    BookId = bookRecord.BookId,
                    IsFavorite = bookRecord.IsFaviourite,
                    IsRead = bookRecord.IsRead,
                    ApplicationUser = appUser
                };
                _userActionService.EditUserBookRecord(userBook);
                if (!await _userActionService.SaveAsync())
                {
                    throw new Exception("Failed to update book in user record. Please try after some time");
                }
                return Ok();
            }
        }

        [HttpPost("AddReview")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddReview(BookReviewDto bookReviewDto) {
            var review = new BookReview()
            {
                BookId = bookReviewDto.BookId,
                ReviewDescription = bookReviewDto.ReviewDescription
            };

            _userActionService.PostReview(review);
            return Ok();
        }

     
    }
}
