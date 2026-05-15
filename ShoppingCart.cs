namespace ShoppingCartActivity
{
    public class ShoppingCart
    {
        // Notes lang: check program.cs pag di nagana
        // Private fields — cannot be accessed directly from outside this class
        private Product _product;
        private int _quantity;

        // Public properties — the only way to get or set the private values above
        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        // Constructor so that Product won't complain about being uninitialized
        public ShoppingCart(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }

        public double SubTotal()
        {
            return _product.Price * _quantity;
        }
    }
}
