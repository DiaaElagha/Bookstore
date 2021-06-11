using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class BookPublishHouseVM
    {
        [Required(ErrorMessage = "يرجى ادخال اسم الدار")]
        [Display(Name = "اسم دار النشر")]
        public string Name { get; set; }

        [Display(Name = "عنوان دار النشر")]
        public string Address { get; set; }

    }
}
