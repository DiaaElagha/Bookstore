using AutoMapper;
using Bookstore.Models.Entities;
using Bookstore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TayarDelivery.Helper
{
    public class ModelsProfiles : Profile
    {
        public ModelsProfiles()
        {

            CreateMap<CategoryVM, BookCategory>().ReverseMap();
            CreateMap<BookAuthorVM, BookAuthor>().ReverseMap();
            CreateMap<BookPublishHouseVM, BookPublishHouse>().ReverseMap();
            CreateMap<BookVM, Book>().ReverseMap();

        }
    }
}
