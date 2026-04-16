using System;

namespace ShoppingCartApp
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int RemainingStock { get; set; }

      public void DisplayProduct()
    {
        if (RemainingStock == 0)
        {
            Console.WriteLine($"{Id}. {Name} - ₱{Price} (OUT OF STOCK)");
        }
        else
        {
            Console.WriteLine($"{Id}. {Name} - ₱{Price} (Stock: {RemainingStock})");
        }
    }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }
    }
}