namespace ShoppingCartActivity

{
    public class ShoppingCart
    {
        public required Product Product { get; set; }
        public int Quantity { get; set; }

        public double SubTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
