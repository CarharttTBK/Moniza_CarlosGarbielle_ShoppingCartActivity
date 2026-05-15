using System;

namespace ShoppingCartActivity
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int RemainingStock { get; set; }
        public string? Category { get; set; }

        public void DisplayProduct()
        {
            if (RemainingStock == 0)
            {
                Console.WriteLine($"{Id}. [{Category}] {Name} - ₱{Price} (OUT OF STOCK)");
            }
            else
            {
                Console.WriteLine($"{Id}. [{Category}] {Name} - ₱{Price} (Stock: {RemainingStock})");
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

        public void RestoreStock(int quantity)
        {
            RemainingStock += quantity;
        }
    }
}
