using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class BookAuthorVM
    {
        [Required(ErrorMessage = "يرجى ادخال المؤلف")]
        [Display(Name = "اسم مؤلف الكتاب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الموبايل")]
        [RegularExpression(@"^([0|\+[0-9]{1,5})?([0-9]{10})$", ErrorMessage = "يرجى ادخال رقم جوال صالح")]
        [Display(Name = "الموبايل")]
        public string Mobile { get; set; }

        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

    }
}
