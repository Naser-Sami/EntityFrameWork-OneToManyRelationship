using System;
using Microsoft.EntityFrameworkCore;

namespace OneToManyRelationship
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _context = new ApplicationDbContext();

            _context.SaveChanges();
        }
    }

    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlServer("Server=localhost; Initial Catalog=EFCore-OneToManyRelationship; user=sa;Password=helloMeJSPythonJ_1.618;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            ///<summary>
            ///
            /// The Blog has many Posts with one Blog
            /// modelBuilder.Entity<Blog>()
            ///     .HasMany(b => b.Posts)
            ///     .WithOne();
            ///     
            ///</summary>


            ///<summary>
            ///
            /// The Post has one Blog with many Posts
            /// modelBuilder.Entity<Post>()
            ///     .HasOne(b => b.Blog)
            ///     .WithMany(p => p.Posts);
            ///</summary>


            /// <summary>
            ///
            /// In Case we do not have the Collection Navigation Property
            /// In the Blog table for Posts [List<Post>]
            /// And we do not have Blog REference Navigation Property
            /// In the Post table [Blog Blog]
            /// We use floant api
            /// 1. Add to DbSet because there's no reference has been set
            /// 2. Add from modelBuilder ..
            /// Post has one Blog with many BlogId's
            /// modelBuilder.Entity<Post>()
            ///     .HasOne<Blog>()
            ///     .WithMany()
            ///     .HasForeignKey(p => p.BlogId);
            ///     
            /// </summary>

            //modelBuilder.Entity<RecordOfSale>()
            //    .HasOne(r => r.Car)
            //    .WithMany(c => c.SaleHistory)
            //    .HasForeignKey(r => r.CarLicensePlate)
            //    .HasPrincipalKey(c => c.LicensePlate);

            modelBuilder.Entity<RecordOfSale>()
                .HasOne(r => r.Car)
                .WithMany(c => c.SaleHistory)
                .HasForeignKey(r => new { r.CarLicensePlate, r.CarState })
                .HasPrincipalKey(c => new { c.LicensePlate, c.State });
        }

        public DbSet<Blog> Blogs { get; set; }
        // public DbSet<Post> Posts { get; set; }
        public DbSet<Car> Cars { get; set; }
    }

  
    public class Blog                                                               // Parent - Principal Entity
    {
        public int BlogId { get; set; }
        public string Url { get; set; } = string.Empty;                             // Parent - Prinipal - Primary Key
        public List<Post> Posts { get; set; } = new List<Post>();                   // Collection Navigation Property
    }


    public class Post                                                               // Child - Dependent Entity
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int BlogId { get; set; }                                             // Foreign -  Key
        public Blog Blog { get; set; } = new Blog();                                // Reference Navigation Property
    }

    
    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public List<RecordOfSale> SaleHistory { get; set; } = new List<RecordOfSale>();
    }


    public class RecordOfSale
    {
        public int RecordOfSaleId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal Price { get; set; }
        public string CarLicensePlate { get; set; } = string.Empty;
        public string CarState { get; set; } = string.Empty;
        public Car Car { get; set; } = new Car();
    }
}
