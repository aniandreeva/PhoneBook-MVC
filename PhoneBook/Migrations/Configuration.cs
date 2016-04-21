namespace PhoneBook.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBook.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhoneBook.AppContext context)
        {
            context.Users.AddOrUpdate(
                u => u.Username,
                new Models.User { Username = "admin", Password = "adminpass", Email = "admin.adm@gga.com", FirstName = "Admin", LastName = "Aminov" }
                );
        }
    }
}
