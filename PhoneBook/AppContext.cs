using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PhoneBook
{
    public class AppContext : DbContext
    {
        public AppContext() : base("PhoneBook") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Contact>()
            //         .HasKey(c => c.ID)
            //         .HasMany(c => c.Groups)
            //         .WithOptional()
            //         .WillCascadeOnDelete(true);
        }
    }
}