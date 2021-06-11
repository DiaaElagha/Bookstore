using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "يرجى ادخال التصنيف")]
        [Display(Name = "اسم تصنيف الكتاب")]
        public string Name { get; set; }

    }
}
