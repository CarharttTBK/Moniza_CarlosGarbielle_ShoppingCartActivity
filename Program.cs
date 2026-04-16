using System;

namespace ShoppingCartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Store!");

            Product[] products = new Product[]
            {
                new Product { Id = 1, Name = "Laptop", Price = 30000, RemainingStock = 5 },
                new Product { Id = 2, Name = "Mouse", Price = 500, RemainingStock = 10 },
                new Product { Id = 3, Name = "Keyboard", Price = 1500, RemainingStock = 7 },
                new Product { Id = 4, Name = "Monitor", Price = 8000, RemainingStock = 4 },
                new Product { Id = 5, Name = "USB Flash Drive", Price = 350, RemainingStock = 20 },
                new Product { Id = 6, Name = "Headphones", Price = 1200, RemainingStock = 6 },
                new Product { Id = 7, Name = "Webcam", Price = 950, RemainingStock = 3 }
            };

            ShoppingCart[] cart = new ShoppingCart[5];
            int cartCount = 0;

            bool continueShopping = true;

            while (continueShopping)
            {
                Console.WriteLine("\n=== STORE MENU ===");
                foreach (var p in products)
                {
                    p.DisplayProduct();
                }

                // PRODUCT INPUT
                Console.Write("\nEnter product number: ");
                if (!int.TryParse(Console.ReadLine(), out int productChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                if (productChoice < 1 || productChoice > products.Length)
                {
                    Console.WriteLine("Invalid product number.");
                    continue;
                }

                Product selectedProduct = products[productChoice - 1];

                if (selectedProduct.RemainingStock == 0)
                {
                    Console.WriteLine("Product is out of stock.");
                    continue;
                }

                // QUANTITY INPUT
                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine("Quantity must be greater than 0.");
                    continue;
                }

                if (quantity > 100)
                {
                    Console.WriteLine("Quantity too large. Maximum is 100.");
                    continue;
                }

                if (!selectedProduct.HasEnoughStock(quantity))
                {
                    Console.WriteLine($"Not enough stock. Only {selectedProduct.RemainingStock} left.");
                    continue;
                }

                // CHECK DUPLICATE (FIXED LOGIC)
                bool found = false;

                for (int i = 0; i < cartCount; i++)
                {
                    if (cart[i].Product.Id == selectedProduct.Id)
                    {
                        int newQuantity = cart[i].Quantity + quantity;

                        // Prevent exceeding stock through multiple adds
                        if (newQuantity > cart[i].Product.RemainingStock + cart[i].Quantity)
                        {
                            Console.WriteLine("Not enough stock for additional quantity.");
                            found = true;
                            break;
                        }

                        cart[i].Quantity = newQuantity;
                        found = true;
                        break;
                    }
                }

                // ADD NEW ITEM
                if (!found)
                {
                    if (cartCount >= cart.Length)
                    {
                        Console.WriteLine("Cart is full.");
                        continue;
                    }

                    cart[cartCount] = new ShoppingCart
                    {
                        Product = selectedProduct,
                        Quantity = quantity
                    };
                    cartCount++;
                }

                selectedProduct.DeductStock(quantity);

                Console.WriteLine("Item added to cart!");

                // CONTINUE INPUT (MORE HUMAN)
                Console.Write("Add another item? (Y/N): ");
                string? choice = Console.ReadLine();

                if (choice != null)
                {
                    choice = choice.ToUpper();
                }

                if (choice == "Y")
                {
                    continueShopping = true;
                }
                else if (choice == "N")
                {
                    continueShopping = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Returning to menu.");
                    continueShopping = true;
                }
            }

            // RECEIPT
            Console.WriteLine("\n=== RECEIPT ===");
            Console.WriteLine("Item\tQty\tSubtotal");

            double grandTotal = 0;

            for (int i = 0; i < cartCount; i++)
            {
                double subtotal = cart[i].SubTotal();
                Console.WriteLine($"{cart[i].Product.Name}\t{cart[i].Quantity}\t₱{subtotal}");
                grandTotal += subtotal;
            }

            Console.WriteLine($"Grand Total: ₱{grandTotal}");

            double discount = 0;
            if (grandTotal >= 5000)
            {
                discount = grandTotal * 0.10;
                Console.WriteLine($"Discount (10%): ₱{discount}");
            }

            double finalTotal = grandTotal - discount;
            Console.WriteLine($"Final Total: ₱{finalTotal}");

            Console.WriteLine("\n=== UPDATED STOCK ===");
            foreach (var p in products)
            {
                p.DisplayProduct();
            }

            Console.WriteLine("\nThank you for shopping!");
        }
    }
}
