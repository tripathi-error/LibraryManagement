using AutoMapper;
using LibraryManagmentDomainLayer.Entities;
using LibraryManagmentDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagmentDomainLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookCreationDto, Book>().ReverseMap();
        }
    }
}
