using ShoppingCartActivity;
using System;
using System.Collections.Generic;
using System.Text;
using System;

namespace ShoppingCartApp
{
    class Program
    {
        // ─── Order history (stores up to 50 completed transactions) ───
        static OrderRecord[] orderHistory = new OrderRecord[50];
        static int orderCount = 0;
        static int receiptCounter = 1;

        // ─── Shared cart state across the main menu ───
        static ShoppingCart[] cart = new ShoppingCart[10];
        static int cartCount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║    Welcome to the Store!     ║");
            Console.WriteLine("║                              ║");
            Console.WriteLine("║        Made by C.G.M         ║");
            Console.WriteLine("║    @CarharttTBK on Github    ║");
            Console.WriteLine("╚══════════════════════════════╝");

            Product[] products = new Product[]
            {
                new Product { Id = 1, Name = "Laptop",        Price = 30000, RemainingStock = 5,  Category = "Electronics" },
                new Product { Id = 2, Name = "Mouse",         Price = 500,   RemainingStock = 10, Category = "Peripherals" },
                new Product { Id = 3, Name = "Keyboard",      Price = 1500,  RemainingStock = 7,  Category = "Peripherals" },
                new Product { Id = 4, Name = "Monitor",       Price = 8000,  RemainingStock = 4,  Category = "Electronics" },
                new Product { Id = 5, Name = "USB Flash Drive",Price = 350,  RemainingStock = 20, Category = "Storage"     },
                new Product { Id = 6, Name = "Headphones",    Price = 1200,  RemainingStock = 6,  Category = "Audio"       },
                new Product { Id = 7, Name = "Webcam",        Price = 950,   RemainingStock = 3,  Category = "Peripherals" }
            };

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("1. Browse Products");
                Console.WriteLine("2. Search Product by Name");
                Console.WriteLine("3. Browse by Category");
                Console.WriteLine("4. Manage Cart" + (cartCount > 0 ? $" ({cartCount} item/s)" : ""));
                Console.WriteLine("5. View Order History");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int mainChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (mainChoice)
                {
                    case 1:
                        BrowseAndAddProduct(products);
                        break;
                    case 2:
                        SearchProduct(products);
                        break;
                    case 3:
                        BrowseByCategory(products);
                        break;
                    case 4:
                        ManageCart(products);
                        break;
                    case 5:
                        ViewOrderHistory();
                        break;
                    case 0:
                        Console.WriteLine("\nThank you for shopping! Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 0–5.");
                        break;
                }
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  1. BROWSE & ADD PRODUCT
        // ─────────────────────────────────────────────────────────────
        static void BrowseAndAddProduct(Product[] products)
        {
            Console.WriteLine("\n=== PRODUCTS ===");
            Console.WriteLine("0. Back");
            foreach (var p in products)
                p.DisplayProduct();

            Console.Write("\nEnter product number (0 to go back): ");
            if (!int.TryParse(Console.ReadLine(), out int productChoice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            if (productChoice == 0) return;

            // Validate range (no duplicate check — only one block needed)
            if (productChoice < 1 || productChoice > products.Length)
            {
                Console.WriteLine("Invalid product number.");
                return;
            }

            if (cartCount >= cart.Length)
            {
                Console.WriteLine("Cart is full (10 unique items). Please manage your cart first.");
                return;
            }

            Product selected = products[productChoice - 1];

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Sorry, that product is out of stock.");
                return;
            }

            Console.Write($"Enter quantity for {selected.Name} (Available: {selected.RemainingStock}): ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            if (quantity > 100)
            {
                Console.WriteLine("Quantity too large. Maximum per entry is 100.");
                return;
            }

            if (!selected.HasEnoughStock(quantity))
            {
                Console.WriteLine($"Not enough stock. Only {selected.RemainingStock} left.");
                return;
            }

            // Check for duplicate — update quantity if already in cart
            bool found = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == selected.Id)
                {
                    // selected.RemainingStock is what's still available (already deducted previous adds)
                    if (!selected.HasEnoughStock(quantity))
                    {
                        Console.WriteLine($"Not enough stock to add {quantity} more. Only {selected.RemainingStock} left.");
                        found = true;
                        break;
                    }
                    cart[i].Quantity += quantity;
                    selected.DeductStock(quantity);
                    Console.WriteLine($"Updated cart: {cart[i].Product.Name} x{cart[i].Quantity}");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                cart[cartCount] = new ShoppingCart { Product = selected, Quantity = quantity };
                cartCount++;
                selected.DeductStock(quantity);
                Console.WriteLine($"✓ {selected.Name} x{quantity} added to cart!");
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  2. SEARCH PRODUCT BY NAME
        // ─────────────────────────────────────────────────────────────
        static void SearchProduct(Product[] products)
        {
            Console.Write("\nEnter product name to search: ");
            string query = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (query == "")
            {
                Console.WriteLine("Search query cannot be empty.");
                return;
            }

            Console.WriteLine("\n=== SEARCH RESULTS ===");
            bool found = false;
            foreach (var p in products)
            {
                if (p.Name != null && p.Name.ToLower().Contains(query))
                {
                    p.DisplayProduct();
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine($"No products found matching \"{query}\".");
        }

        // ─────────────────────────────────────────────────────────────
        //  3. BROWSE BY CATEGORY
        // ─────────────────────────────────────────────────────────────
        static void BrowseByCategory(Product[] products)
        {
            // Collect unique categories
            string[] categories = new string[10];
            int catCount = 0;

            foreach (var p in products)
            {
                if (p.Category == null) continue;
                bool exists = false;
                for (int i = 0; i < catCount; i++)
                    if (categories[i] == p.Category) { exists = true; break; }
                if (!exists && catCount < categories.Length)
                    categories[catCount++] = p.Category;
            }

            Console.WriteLine("\n=== CATEGORIES ===");
            Console.WriteLine("0. Back");
            for (int i = 0; i < catCount; i++)
                Console.WriteLine($"{i + 1}. {categories[i]}");

            Console.Write("Choose a category: ");
            if (!int.TryParse(Console.ReadLine(), out int catChoice))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            if (catChoice == 0) return;

            if (catChoice < 1 || catChoice > catCount)
            {
                Console.WriteLine("Invalid category number.");
                return;
            }

            string selectedCat = categories[catChoice - 1];
            Console.WriteLine($"\n=== {selectedCat.ToUpper()} ===");
            foreach (var p in products)
                if (p.Category == selectedCat)
                    p.DisplayProduct();
        }

        // ─────────────────────────────────────────────────────────────
        //  4. MANAGE CART
        // ─────────────────────────────────────────────────────────────
        static void ManageCart(Product[] products)
        {
            bool inCartMenu = true;
            while (inCartMenu)
            {
                Console.WriteLine("\n=== CART MENU ===");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Update Item Quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ViewCart();
                        break;
                    case 2:
                        RemoveItem();
                        break;
                    case 3:
                        UpdateQuantity();
                        break;
                    case 4:
                        ClearCart();
                        break;
                    case 5:
                        Checkout(products);
                        inCartMenu = false; // After checkout, return to main menu
                        break;
                    case 0:
                        inCartMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 0–5.");
                        break;
                }
            }
        }

        // ─── View Cart ───
        static void ViewCart()
        {
            if (cartCount == 0)
            {
                Console.WriteLine("\nYour cart is empty.");
                return;
            }

            Console.WriteLine("\n=== YOUR CART ===");
            Console.WriteLine($"{"#",-4}{"Item",-22}{"Qty",-6}{"Price",-12}{"Subtotal"}");
            Console.WriteLine(new string('-', 52));

            double runningTotal = 0;
            for (int i = 0; i < cartCount; i++)
            {
                double sub = cart[i].SubTotal();
                Console.WriteLine($"{i + 1,-4}{cart[i].Product.Name,-22}{cart[i].Quantity,-6}₱{cart[i].Product.Price,-11}₱{sub}");
                runningTotal += sub;
            }

            Console.WriteLine(new string('-', 52));
            Console.WriteLine($"{"Running Total:",-42}₱{runningTotal}");

            if (runningTotal >= 5000)
                Console.WriteLine($"{"Estimated after 10% discount:",-42}₱{runningTotal * 0.90}");
        }

        // ─── Remove Item ───
        static void RemoveItem()
        {
            if (cartCount == 0) { Console.WriteLine("Cart is empty."); return; }

            ViewCart();
            Console.Write("\nEnter item number to remove (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int idx))
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            if (idx == 0) return;
            if (idx < 1 || idx > cartCount)
            {
                Console.WriteLine("Invalid item number.");
                return;
            }

            string removedName = cart[idx - 1].Product.Name ?? "Item";
            cart[idx - 1].Product.RestoreStock(cart[idx - 1].Quantity);

            // Shift array left
            for (int i = idx - 1; i < cartCount - 1; i++)
                cart[i] = cart[i + 1];
            cart[cartCount - 1] = null!;
            cartCount--;

            Console.WriteLine($"✓ {removedName} removed from cart.");
        }

        // ─── Update Quantity ───
        static void UpdateQuantity()
        {
            if (cartCount == 0) { Console.WriteLine("Cart is empty."); return; }

            ViewCart();
            Console.Write("\nEnter item number to update (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int idx))
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            if (idx == 0) return;
            if (idx < 1 || idx > cartCount)
            {
                Console.WriteLine("Invalid item number.");
                return;
            }

            ShoppingCart item = cart[idx - 1];
            Console.Write($"Enter new quantity for {item.Product.Name} (current: {item.Quantity}): ");
            if (!int.TryParse(Console.ReadLine(), out int newQty))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            if (newQty <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0. Use Remove to delete an item.");
                return;
            }

            if (newQty > 100)
            {
                Console.WriteLine("Maximum quantity per item is 100.");
                return;
            }

            int diff = newQty - item.Quantity;

            if (diff > 0)
            {
                // Need more stock
                if (!item.Product.HasEnoughStock(diff))
                {
                    Console.WriteLine($"Not enough stock. Only {item.Product.RemainingStock} more available.");
                    return;
                }
                item.Product.DeductStock(diff);
            }
            else if (diff < 0)
            {
                // Returning stock
                item.Product.RestoreStock(-diff);
            }

            item.Quantity = newQty;
            Console.WriteLine($"✓ Quantity updated to {newQty}.");
        }

        // ─── Clear Cart ───
        static void ClearCart()
        {
            if (cartCount == 0) { Console.WriteLine("Cart is already empty."); return; }

            Console.Write("Are you sure you want to clear the entire cart? (Y/N): ");
            if (GetYesNo() == "N")
            {
                Console.WriteLine("Clear cancelled.");
                return;
            }

            for (int i = 0; i < cartCount; i++)
            {
                cart[i].Product.RestoreStock(cart[i].Quantity);
                cart[i] = null!;
            }
            cartCount = 0;
            Console.WriteLine("✓ Cart cleared. All stock has been restored.");
        }

        // ─────────────────────────────────────────────────────────────
        //  5. CHECKOUT
        // ─────────────────────────────────────────────────────────────
        static void Checkout(Product[] products)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Your cart is empty. Nothing to checkout.");
                return;
            }

            // Compute totals
            double grandTotal = 0;
            for (int i = 0; i < cartCount; i++)
                grandTotal += cart[i].SubTotal();

            double discount = 0;
            if (grandTotal >= 5000)
                discount = grandTotal * 0.10;

            double finalTotal = grandTotal - discount;

            // ─── Payment Validation ───
            double payment = 0;
            while (true)
            {
                Console.Write($"\nFinal Total: ₱{finalTotal}\nEnter payment amount: ₱");
                string? input = Console.ReadLine();
                if (!double.TryParse(input, out payment))
                {
                    Console.WriteLine("Invalid input. Please enter a numeric amount.");
                    continue;
                }
                if (payment < finalTotal)
                {
                    Console.WriteLine($"Insufficient payment. You are short by ₱{finalTotal - payment:F2}.");
                    continue;
                }
                break;
            }

            double change = payment - finalTotal;

            // ─── Generate Receipt ───
            string receiptNo = receiptCounter.ToString("D4");
            string dateTime = DateTime.Now.ToString("MMMM dd, yyyy h:mm tt");

            Console.WriteLine("\n╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              OFFICIAL RECEIPT               ║");
            Console.WriteLine("╠══════════════════════════════════════════════╣");
            Console.WriteLine($"  Receipt No : {receiptNo}");
            Console.WriteLine($"  Date/Time  : {dateTime}");
            Console.WriteLine("╠══════════════════════════════════════════════╣");
            Console.WriteLine($"  {"Item",-22}{"Qty",-6}{"Subtotal"}");
            Console.WriteLine(new string('─', 48));

            for (int i = 0; i < cartCount; i++)
            {
                double sub = cart[i].SubTotal();
                Console.WriteLine($"  {cart[i].Product.Name,-22}{cart[i].Quantity,-6}₱{sub}");
            }

            Console.WriteLine(new string('─', 48));
            Console.WriteLine($"  {"Grand Total:",-28}₱{grandTotal}");

            if (discount > 0)
                Console.WriteLine($"  {"Discount (10%):",-28}₱{discount}");

            Console.WriteLine($"  {"Final Total:",-28}₱{finalTotal}");
            Console.WriteLine($"  {"Payment:",-28}₱{payment}");
            Console.WriteLine($"  {"Change:",-28}₱{change:F2}");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            // ─── Low Stock Alert ───
            Console.WriteLine("\n=== LOW STOCK ALERT ===");
            bool hasAlert = false;
            foreach (var p in products)
            {
                if (p.RemainingStock == 0)
                {
                    Console.WriteLine($"  ✗ {p.Name} is OUT OF STOCK");
                    hasAlert = true;
                }
                else if (p.RemainingStock <= 5)
                {
                    Console.WriteLine($"  ⚠ {p.Name} — only {p.RemainingStock} left");
                    hasAlert = true;
                }
            }
            if (!hasAlert)
                Console.WriteLine("  All products have sufficient stock.");

            // ─── Save to Order History ───
            if (orderCount < orderHistory.Length)
            {
                orderHistory[orderCount++] = new OrderRecord
                {
                    ReceiptNumber = receiptNo,
                    CheckoutDateTime = dateTime,
                    GrandTotal = grandTotal,
                    Discount = discount,
                    FinalTotal = finalTotal,
                    Payment = payment,
                    Change = change
                };
            }

            receiptCounter++;

            // Clear cart after checkout
            for (int i = 0; i < cartCount; i++)
                cart[i] = null!;
            cartCount = 0;

            Console.WriteLine("\nThank you for shopping! See you next time!");
        }

        // ─────────────────────────────────────────────────────────────
        //  6. VIEW ORDER HISTORY
        // ─────────────────────────────────────────────────────────────
        static void ViewOrderHistory()
        {
            if (orderCount == 0)
            {
                Console.WriteLine("\nNo completed transactions yet.");
                return;
            }

            Console.WriteLine("\n=== ORDER HISTORY ===");
            Console.WriteLine($"{"Receipt",-12}{"Date/Time",-30}{"Final Total"}");
            Console.WriteLine(new string('-', 55));

            for (int i = 0; i < orderCount; i++)
            {
                OrderRecord o = orderHistory[i];
                Console.WriteLine($"#{o.ReceiptNumber,-11}{o.CheckoutDateTime,-30}₱{o.FinalTotal}");
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  HELPER: Strict Y/N prompt — re-prompts until valid input
        // ─────────────────────────────────────────────────────────────
        static string GetYesNo()
        {
            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToUpper();
                if (input == "Y" || input == "N")
                    return input;
                Console.Write("Invalid input. Please enter Y or N only: ");
            }
        }
    }
}
