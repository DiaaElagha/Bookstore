using Bookstore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Book { set; get; }
        public DbSet<BookAuthor> BookAuthor { set; get; }
        public DbSet<BookCategory> BookCategory { set; get; }
        public DbSet<BookPublishHouse> BookPublishHouse { set; get; }



    }
}
