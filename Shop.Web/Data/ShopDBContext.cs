using Microsoft.EntityFrameworkCore;
using Shop.Web.Models;

namespace Shop.Web.Data
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext(DbContextOptions Options) : base(Options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportMessage> ReportMessages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<AD> ADs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubComment>()
            .HasOne(s => s.User)
            .WithMany(u => u.SubComments)
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
            .HasOne(s => s.User)
            .WithMany(u => u.Comments)
            .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
            .HasOne(s => s.Discount)
            .WithOne(u => u.Product)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Discount>()
            .HasOne(s => s.Product)
            .WithOne(u => u.Discount)
            .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
