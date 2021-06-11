using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Entities
{
    public class Book : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearRelease { get; set; }

        [Required]
        public int BookPublishHouseId { get; set; }
        [ForeignKey(nameof(BookPublishHouseId))]
        public BookPublishHouse BookPublishHouse { get; set; }

        [Required]
        public int BookAuthorId { get; set; }
        [ForeignKey(nameof(BookAuthorId))]
        public BookAuthor BookAuthor { get; set; }

        [Required]
        public int BookCategoryId { get; set; }
        [ForeignKey(nameof(BookCategoryId))]
        public BookCategory BookCategory { get; set; }

    }
}
