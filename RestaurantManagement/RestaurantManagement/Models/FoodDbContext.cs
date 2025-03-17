using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagement.Models
{
    public class FoodDbContext : IdentityDbContext<Customer, Role, int>
    {
        public FoodDbContext()
        {

        }
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {
            //..
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<FoodImage> FoodImages { get; set; }
        public DbSet<FoodFavorite> FoodFavorites { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }
        public DbSet<FoodOrderDetail> FoodOrderDetails { get; set; }
        public DbSet<FoodFeedback> FoodFeedbacks { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình FoodImage
            modelBuilder.Entity<FoodImage>()
                .HasKey(fi => new { fi.FoodID, fi.ImageID });

            modelBuilder.Entity<FoodImage>()
                .HasOne(fi => fi.Food)
                .WithMany(f => f.FoodImages)
                .HasForeignKey(fi => fi.FoodID);

            // Cấu hình FoodFavorite
            modelBuilder.Entity<FoodFavorite>()
                .HasKey(ff => ff.FoodFavoriteID);

            modelBuilder.Entity<FoodFavorite>()
                .HasOne(ff => ff.Food)
                .WithMany(f => f.FoodFavorites)
                .HasForeignKey(ff => ff.FoodID);

            modelBuilder.Entity<FoodFavorite>()
                .HasOne(ff => ff.Customer)
                .WithMany(c => c.FoodFavorites)
                .HasForeignKey(ff => ff.UserID);



            // Cấu hình Food
            modelBuilder.Entity<Food>()
                .HasKey(f => f.FoodID);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.FoodImages)
                .WithOne(fi => fi.Food)
                .HasForeignKey(fi => fi.FoodID);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.FoodFavorites)
                .WithOne(ff => ff.Food)
                .HasForeignKey(ff => ff.FoodID);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.FoodOrderDetails)
                .WithOne(fod => fod.Food)
                .HasForeignKey(fod => fod.FoodID);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.FoodFeedbacks)
                .WithOne(ff => ff.Food)
                .HasForeignKey(ff => ff.FoodID);

            modelBuilder.Entity<Food>()
                .HasOne(f => f.FoodCategory)
                .WithMany(fc => fc.Foods)
                .HasForeignKey(f => f.FoodCategoryID);

            // Cấu hình FoodOrder
            // Cấu hình FoodOrder
            modelBuilder.Entity<FoodOrder>()
                .HasKey(fo => fo.OrderID);

            modelBuilder.Entity<FoodOrder>()
                .HasOne(fo => fo.Customer)
                .WithMany(c => c.FoodOrders)
                .HasForeignKey(fo => fo.UserID);

            modelBuilder.Entity<FoodOrder>()
                .HasMany(fo => fo.FoodOrderDetails)
                .WithOne(fod => fod.FoodOrder)
                .HasForeignKey(fod => fod.OrderID);

            modelBuilder.Entity<FoodOrder>()
                .HasOne(fo => fo.PaymentMethod)
                .WithMany(pm => pm.FoodOrders)
                .HasForeignKey(fo => fo.PaymentMethodID);

            // Cấu hình FoodOrderDetail
            modelBuilder.Entity<FoodOrderDetail>()
            .HasKey(fod => fod.OrderDetailID);

            modelBuilder.Entity<FoodOrderDetail>()
                .HasOne(fod => fod.FoodOrder)
                .WithMany(fo => fo.FoodOrderDetails)
                .HasForeignKey(fod => fod.OrderID);

            modelBuilder.Entity<FoodOrderDetail>()
                .HasOne(fod => fod.Food)
                .WithMany(f => f.FoodOrderDetails)
                .HasForeignKey(fod => fod.FoodID);

            // Cấu hình FoodFeedback
            modelBuilder.Entity<FoodFeedback>()
                .HasKey(ff => ff.FoodFeedbackID);

            modelBuilder.Entity<FoodFeedback>()
                .HasOne(ff => ff.Food)
                .WithMany(f => f.FoodFeedbacks)
                .HasForeignKey(ff => ff.FoodID);

            modelBuilder.Entity<FoodFeedback>()
                .HasOne(ff => ff.Customer)
                .WithMany(c => c.FoodFeedbacks)
                .HasForeignKey(ff => ff.UserID);

            // Cấu hình CartItem
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.CartItemID);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Customer)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.UserID);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Food)
                .WithMany(f => f.CartItems)
                .HasForeignKey(ci => ci.FoodID);

            // Cấu hình PaymentMethod
            modelBuilder.Entity<PaymentMethod>()
                .HasKey(pm => pm.PaymentMethodID);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(pm => pm.FoodOrders)
                .WithOne(fo => fo.PaymentMethod)
                .HasForeignKey(fo => fo.PaymentMethodID);

            // Cấu hình FoodCategory
            modelBuilder.Entity<FoodCategory>()
            .HasKey(fc => fc.FoodCategoryID);

            modelBuilder.Entity<FoodCategory>()
                .HasMany(fc => fc.Foods)
                .WithOne(f => f.FoodCategory)
                .HasForeignKey(f => f.FoodCategoryID);


        }
    }
}