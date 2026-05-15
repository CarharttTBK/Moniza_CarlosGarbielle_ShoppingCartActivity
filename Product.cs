using System;

namespace ShoppingCartActivity
{
    public class Product
    {
        // Notes lang;
        // Private backing fields
        private int _id;
        private string? _name;
        private double _price;
        private int _remainingStock;
        private string? _category;

        // Public properties — basically controlled access to the private fields above
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int RemainingStock
        {
            get { return _remainingStock; }
            set { _remainingStock = value; }
        }

        public string? Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public void DisplayProduct()
        {
            if (_remainingStock == 0)
            {
                Console.WriteLine($"{_id}. [{_category}] {_name} - ₱{_price} (OUT OF STOCK)");
            }
            else
            {
                Console.WriteLine($"{_id}. [{_category}] {_name} - ₱{_price} (Stock: {_remainingStock})");
            }
        }

        public double GetItemTotal(int quantity)
        {
            return _price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return quantity <= _remainingStock;
        }

        public void DeductStock(int quantity)
        {
            _remainingStock -= quantity;
        }

        public void RestoreStock(int quantity)
        {
            _remainingStock += quantity;
        }
    }
}
