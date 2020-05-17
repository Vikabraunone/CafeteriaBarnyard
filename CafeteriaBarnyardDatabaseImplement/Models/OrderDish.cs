namespace CafeteriaBarnyardDatabaseImplement.Models
{
    public class OrderDish
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int DishId { get; set; }

        public decimal DishPrice { get; set; }

        public virtual Order Order { get; set; }

        public virtual Dish Dish { get; set; }
    }
}
