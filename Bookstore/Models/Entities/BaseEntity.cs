using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateAt = DateTime.Now;
        }
        [ScaffoldColumn(false)]
        public DateTime? CreateAt { get; set; }

        public string CreateByUserId { get; set; }
        [ForeignKey(nameof(CreateByUserId))]
        public IdentityUser IdentityUserCreate { get; set; }

        public string UpdateByUserId { get; set; }
        [ForeignKey(nameof(UpdateByUserId))]
        public IdentityUser IdentityUserUpdate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdateAt { get; set; }
    }
}
