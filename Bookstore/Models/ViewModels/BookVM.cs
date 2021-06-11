using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class BookVM
    {
        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [Display(Name = "عنوان الكتاب")]
        public string Title { get; set; }

        [Display(Name = "تفاصيل الكتاب")]
        public string Description { get; set; }

        [Required(ErrorMessage = "يرجى ادخال سنة الاصدار")]
        [Display(Name = "سنة الاصدار")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:0/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearRelease { get; set; }

        [Required(ErrorMessage = "يرجى ادخال دار النشر")]
        [Display(Name = "دار النشر")]
        public int BookPublishHouseId { get; set; }

        [Required(ErrorMessage = "يرجى ادخال المؤلف")]
        [Display(Name = "مؤلف الكتاب")]
        public int BookAuthorId { get; set; }

        [Required(ErrorMessage = "يرجى ادخال التصنيف")]
        [Display(Name = "التصنيف الكتاب")]
        public int BookCategoryId { get; set; }

    }
}
