using Bookstore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class HomeVM
    {
        public List<Book> listBooks { get; set; } = new List<Book>();
    }
}
