using BusinessOjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FUMiniHotelManagementDBContext : DbContext
    {
        public FUMiniHotelManagementDBContext() { }
        public DbSet<BookingDetail> BookingDetail { get; set; }
        public DbSet<BookingReservation> BookingReservation { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<RoomInformation> RoomInformation { get; set; }
        public DbSet<RoomType> RoomType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FUMiniHotelDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Entity BookingDetail
            modelBuilder.Entity<BookingDetail>()
                .HasKey(b => new { b.BookingReservationID, b.RoomID });

            modelBuilder.Entity<BookingDetail>()
                .Property(b => b.StartDate)
                .IsRequired();

            modelBuilder.Entity<BookingDetail>()
                .Property(b => b.EndDate)
                .IsRequired();

            modelBuilder.Entity<BookingDetail>()
                .Property(b => b.ActualPrice)
                .HasColumnType("money");

            modelBuilder.Entity<BookingDetail>().HasData(
                new BookingDetail { BookingReservationID = 1, RoomID = 3, StartDate = new DateOnly(2024, 1, 1), EndDate = new DateOnly(2024, 1, 2), ActualPrice = 199.00m },
                new BookingDetail { BookingReservationID = 1, RoomID = 7, StartDate = new DateOnly(2024, 1, 1), EndDate = new DateOnly(2024, 1, 2), ActualPrice = 179.00m },
                new BookingDetail { BookingReservationID = 2, RoomID = 3, StartDate = new DateOnly(2024, 1, 5), EndDate = new DateOnly(2024, 1, 6), ActualPrice = 199.00m },
                new BookingDetail { BookingReservationID = 2, RoomID = 5, StartDate = new DateOnly(2024, 1, 5), EndDate = new DateOnly(2024, 1, 9), ActualPrice = 219.00m }
            );

            // Entity BookingReservation
            modelBuilder.Entity<BookingReservation>()
                .HasKey(b => b.BookingReservationID);

            modelBuilder.Entity<BookingReservation>()
                .Property(b => b.BookingDate)
                .IsRequired();

            modelBuilder.Entity<BookingReservation>()
                .Property(b => b.TotalPrice)
                .HasColumnType("money");

            modelBuilder.Entity<BookingReservation>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.BookingReservations)
                .HasForeignKey(b => b.CustomerID)
                .OnDelete(DeleteBehavior.Cascade); // Ensure referential integrity

            modelBuilder.Entity<BookingReservation>().HasData(
                new BookingReservation { BookingReservationID = 1, BookingDate = new DateOnly(2023, 12, 20), TotalPrice = 378.00m, CustomerID = 3, BookingStatus = 1 },
                new BookingReservation { BookingReservationID = 2, BookingDate = new DateOnly(2023, 12, 21), TotalPrice = 1493.00m, CustomerID = 3, BookingStatus = 1 }
            );

            // Entity Customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerFullName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Telephone)
                .HasMaxLength(12);

            modelBuilder.Entity<Customer>()
                .Property(c => c.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerBirthday)
                .IsRequired(false);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerStatus)
                .IsRequired(false);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 3, CustomerFullName = "William Shakespeare", Telephone = "0903939393", EmailAddress = "WilliamShakespeare@FUMiniHotel.org", CustomerBirthday = new DateOnly(1990, 2, 2), CustomerStatus = 1, Password = "123@" },
                new Customer { CustomerID = 5, CustomerFullName = "Elizabeth Taylor", Telephone = "0903939377", EmailAddress = "ElizabethTaylor@FUMiniHotel.org", CustomerBirthday = new DateOnly(1991, 3, 3), CustomerStatus = 1, Password = "144@" },
                new Customer { CustomerID = 8, CustomerFullName = "James Cameron", Telephone = "0903946582", EmailAddress = "JamesCameron@FUMiniHotel.org", CustomerBirthday = new DateOnly(1992, 11, 10), CustomerStatus = 1, Password = "443@" },
                new Customer { CustomerID = 9, CustomerFullName = "Charles Dickens", Telephone = "0903955633", EmailAddress = "CharlesDickens@FUMiniHotel.org", CustomerBirthday = new DateOnly(1991, 12, 5), CustomerStatus = 1, Password = "563@" },
                new Customer { CustomerID = 10, CustomerFullName = "George Orwell", Telephone = "0913933493", EmailAddress = "GeorgeOrwell@FUMiniHotel.org", CustomerBirthday = new DateOnly(1993, 12, 24), CustomerStatus = 1, Password = "177@" }
            );

            // Entity RoomInformation
            modelBuilder.Entity<RoomInformation>()
                .HasKey(r => r.RoomID);

            modelBuilder.Entity<RoomInformation>()
                .Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<RoomInformation>()
                .Property(r => r.RoomDetailDescription)
                .HasMaxLength(220);

            modelBuilder.Entity<RoomInformation>()
                .Property(r => r.RoomMaxCapacity)
                .IsRequired(false);

            modelBuilder.Entity<RoomInformation>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.RoomInformations)
                .HasForeignKey(r => r.RoomTypeID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of RoomType if RoomInformation exists

            modelBuilder.Entity<RoomInformation>()
                .Property(r => r.RoomPricePerDay)
                .HasColumnType("money")
                .IsRequired(false);

            modelBuilder.Entity<RoomInformation>().HasData(
                new RoomInformation { RoomID = 1, RoomNumber = "2364", RoomDetailDescription = "A basic room with essential amenities, suitable for individual travelers or couples.", RoomMaxCapacity = 3, RoomTypeID = 1, RoomStatus = 1, RoomPricePerDay = 149.00m },
                new RoomInformation { RoomID = 2, RoomNumber = "3345", RoomDetailDescription = "Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.", RoomMaxCapacity = 5, RoomTypeID = 3, RoomStatus = 1, RoomPricePerDay = 299.00m },
                new RoomInformation { RoomID = 3, RoomNumber = "4432", RoomDetailDescription = "A luxurious and spacious room with separate living and sleeping areas, ideal for guests seeking extra comfort and space.", RoomMaxCapacity = 4, RoomTypeID = 2, RoomStatus = 1, RoomPricePerDay = 199.00m },
                new RoomInformation { RoomID = 5, RoomNumber = "3342", RoomDetailDescription = "Floor 3, Window in the North West", RoomMaxCapacity = 5, RoomTypeID = 5, RoomStatus = 1, RoomPricePerDay = 219.00m },
                new RoomInformation { RoomID = 7, RoomNumber = "4434", RoomDetailDescription = "Floor 4, main window in the South", RoomMaxCapacity = 4, RoomTypeID = 1, RoomStatus = 1, RoomPricePerDay = 179.00m }
            );

            // Entity RoomType
            modelBuilder.Entity<RoomType>()
                .HasKey(rt => rt.RoomTypeID);

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.RoomTypeName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.TypeDescription)
                .HasMaxLength(250);

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.TypeNote)
                .HasMaxLength(250);

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { RoomTypeID = 1, RoomTypeName = "Standard room", TypeDescription = "This is typically the most affordable option and provides basic amenities such as a bed, dresser, and TV.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 2, RoomTypeName = "Suite", TypeDescription = "Suites usually offer more space and amenities than standard rooms, such as a separate living area, kitchenette, and multiple bathrooms.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 3, RoomTypeName = "Deluxe room", TypeDescription = "Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 4, RoomTypeName = "Executive room", TypeDescription = "Executive rooms are designed for business travelers and offer perks such as free breakfast, evening drink, and high-speed internet.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 5, RoomTypeName = "Family Room", TypeDescription = "A room specifically designed to accommodate families, often with multiple beds and additional space for children.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 6, RoomTypeName = "Connecting Room", TypeDescription = "Two or more rooms with a connecting door, providing flexibility for larger groups or families traveling together.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 7, RoomTypeName = "Penthouse Suite", TypeDescription = "An extravagant, top-floor suite with exceptional views and exclusive amenities, typically chosen for special occasions or VIP guests.", TypeNote = "N/A" },
                new RoomType { RoomTypeID = 8, RoomTypeName = "Bungalow", TypeDescription = "A standalone cottage-style accommodation, providing privacy and a sense of seclusion often within a resort setting", TypeNote = "N/A" }
            );
        }
    }
}
