using Microsoft.EntityFrameworkCore;
using Web.Entities;

namespace Web.Data
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CategoryDict> CategoryDict { get; set; }
        public DbSet<SubCategoryDict> SubCategoryDict { get; set; }
        public DbSet<User> Users { get; set; }

        // seeding the database with data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Contacts
            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jkowlaski@mail.com",
                Password = "b91f82cfd4e5a2201290f6de6c4b73322d03abdb",
                CategoryId = 1,
                SubCategory = "szef",
                PhoneNumber = "123123123",
                DateOfBirth = new DateOnly(2002, 2, 20),
            });
            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = 2,
                FirstName = "Anna",
                LastName = "Panna",
                Email = "apanna@mail.com",
                Password = "3572112c8b9ab0de9d437f9a2fc24999ffbd8c56",
                CategoryId = 2,
                SubCategory = string.Empty,
                PhoneNumber = "98712412",
                DateOfBirth = new DateOnly(2003, 3, 20),
            });
            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = 3,
                FirstName = "Krzysztof",
                LastName = "Kowalski",
                Email = "kkowlaski@mail.com",
                Password = "190af9a37aed7604bb36b7c831835961e18dd19c",
                CategoryId = 3,
                SubCategory = "dowolne",
                PhoneNumber = "098123456",
                DateOfBirth = new DateOnly(1999, 1, 1),
            });
            #endregion
            #region CategoryDict
            modelBuilder.Entity<CategoryDict>().HasData(new CategoryDict
            {
                Id = 1,
                Name = "służbowy"
            });
            modelBuilder.Entity<CategoryDict>().HasData(new CategoryDict
            {
                Id = 2,
                Name = "prywatny"
            });
            modelBuilder.Entity<CategoryDict>().HasData(new CategoryDict
            {
                Id = 3,
                Name = "inny"
            });
            #endregion
            #region SubCategoryDict
            modelBuilder.Entity<SubCategoryDict>().HasData(new SubCategoryDict
            {
                Id = 1,
                Name = "szef"
            });
            modelBuilder.Entity<SubCategoryDict>().HasData(new SubCategoryDict
            {
                Id = 2,
                Name = "klient"
            });
            modelBuilder.Entity<SubCategoryDict>().HasData(new SubCategoryDict
            {
                Id = 3,
                Name = "stazysta"
            });
            modelBuilder.Entity<SubCategoryDict>().HasData(new SubCategoryDict
            {
                Id = 4,
                Name = "pracownik"
            });
            #endregion
            #region User
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username= "aaaa",
                PasswordHashed = "AQAAAAIAAYagAAAAEM/ATZX4uplyyeW+2IgysvsVp5Fw7wg4WLaOtcVpeiWl2FhTR1er8HKeqdYY+NpBcw==",
            });
            #endregion
        }


    }
}
