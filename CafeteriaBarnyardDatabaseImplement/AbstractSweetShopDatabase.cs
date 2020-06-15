using CafeteriaBarnyardDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaBarnyardDatabaseImplement
{
    public class AbstractSweetShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-2ACH9S6\SQLEXPRESS;Initial Catalog=CafeteriaBarnyard;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Product> Products { set; get; }

        public virtual DbSet<DishProduct> DishProducts { set; get; }

        public virtual DbSet<Dish> Dishes { set; get; }

        public virtual DbSet<Client> Clients { set; get; }

        public virtual DbSet<Request> Requests { set; get; }

        public virtual DbSet<RequestProduct> RequestProducts { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<OrderDish> OrderDishes { set; get; }

        public virtual DbSet<ProductAdding> AddProducts { set; get; }
    }
}
