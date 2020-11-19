using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPIServices.Interfaces;
using LibraryManagmentDomainLayer.Entities;
using LibraryManagmentDomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    [Route("api/Books")]
    [Authorize(Roles = "Admin")]

    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        // GET: /<controller>/
        [HttpGet(Name = "GetAllBooks")]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await _bookService.GetBooks();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<BookDto>>> AddBooks(BookCreationDto bookCreationDto)
        {

            var book = _mapper.Map<Book>(bookCreationDto);
            _bookService.AddBook(book);
            if (!await _bookService.SaveAsync()) {
                throw new Exception("Creation of Books failed to save. PLease try after some time");
            }
            var bookDetail = _mapper.Map<BookDto>(book);
            return CreatedAtRoute("GetAllBooks", bookDetail);
        }

        [HttpGet("{id}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BookDto>> GetBookAsync(int id) {
            var book = await _bookService.GetBookAsync(id);
            if (book == null) {
                return NotFound($"The book with the given id {id} is not present");
            }
            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }

        [HttpDelete("id", Name = "DeleteBook")]
        public async Task<ActionResult> DeleteBook(int id) {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound($"The book with the given id {id} is not present");
            }
            _bookService.DeleteBook(book);
            if (!await _bookService.SaveAsync()) {
                throw new Exception("Failed to delete book. Please try after some time");
            }
            return Content("Deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, BookDto bookCreationDto) {
            var bookSaved = await _bookService.GetBookAsync(id);
            if (bookSaved == null)
            {
                return NotFound($"The book with the given id {id} is not present");
            }
            _mapper.Map(bookCreationDto, bookSaved);
            _bookService.EditBook(bookSaved);
            if (!await _bookService.SaveAsync())
            {
                throw new Exception("Failed to delete book. Please try after some time");
            }
            return Content("Updated successfully");
        }

        [HttpGet("id", Name ="BookReview")]

        public async Task<ActionResult<List<BookReview>>> GetBookReviews(int id) {
           return await _bookService.GetBookReviews(id);
        }
    }
}
