# Moniza_CarlosGarbielle_ShoppingCartActivity

---

## How to Run

For VS Code:
Select `Program.cs` and type `dotnet run` on the console.

Visual Studio:
Load the folder from downloading the code zip from the repo. Run it after opening in the app.

---

## Description

This project is a console-based shopping cart system built in C#. It allows users to browse products, search by name or category, manage a cart, checkout with payment validation, and view order history across a session.

---

## Screenshots
<img width="447" height="415" alt="Image" src="https://github.com/user-attachments/assets/87e286b2-913b-480f-938d-a96fab88b3ab" /> <img width="625" height="290" alt="Image" src="https://github.com/user-attachments/assets/1cf548bb-d5f5-4e22-9b13-666ef96fd81a" /> <img width="572" height="124" alt="Image" src="https://github.com/user-attachments/assets/c73a04c0-c7ad-46d6-ad47-f7bfb7dc9e91" /> <img width="664" height="302" alt="Image" src="https://github.com/user-attachments/assets/a81c430f-1d44-4fc1-a3d3-f98375a31076" /> <img width="333" height="211" alt="Image" src="https://github.com/user-attachments/assets/4bdffda3-0ced-402b-8281-1376df83c0a8" /> <img width="694" height="315" alt="Image" src="https://github.com/user-attachments/assets/028d41bf-f864-484d-a5ed-56f57ec13a9d" />

<img width="735" height="863" alt="Image" src="https://github.com/user-attachments/assets/13f5911e-fcb5-45b3-8ec3-b2efd6b3687c" />

---

## Part 1 – Features

- Displays a store menu with available products and their stock
- Lets the user select items and input quantities with full input validation
- Prevents invalid inputs (non-numeric, negative, zero, exceeding maximum of 100)
- Handles out-of-stock products and prevents buying more than available stock
- Deducts stock after adding to cart; restores it on removal
- Prevents duplicate cart entries by updating existing item quantities instead
- Displays itemized receipt with Grand Total
- Applies a 10% discount when the Grand Total is ₱5,000 or more
- Shows Final Total after discount

---

## Part 2 – Enhanced Features

- **Cart Management Menu** — View, remove, update quantity, clear, or checkout from a dedicated cart submenu
- **Product Search** — Search products by name (partial match supported)
- **Product Categories** — Products tagged with a category; users can filter/browse by category
- **Low Stock Alert** — After checkout, products with stock ≤ 5 are flagged; out-of-stock products are marked separately
- **Payment Validation** — User must enter a numeric payment amount ≥ Final Total; re-prompts on invalid or insufficient input; change is computed and displayed
- **Receipt with Number and Date/Time** — Each receipt shows a padded receipt number (e.g., 0001), checkout date and time, itemized list, Grand Total, discount, Final Total, payment, and change
- **Order History** — Completed transactions are stored in an array and viewable from the main menu at any time during the session
- **Strict Y/N Validation** — All yes/no prompts loop and re-prompt until a valid `Y` or `N` is entered

---

## Part 1 – Issues Addressed

| Issue | Fix Applied |
|---|---|
| README said `CartItem.cs` but actual file is `ShoppingCart.cs` | Updated all references in README to `ShoppingCart.cs` |
| Duplicate product range validation block in `Program.cs` | Removed the second identical `if` block |
| Invalid Y/N input returned to menu instead of re-prompting | All Y/N prompts now use `GetYesNo()` which loops until valid input |
| AI usage section lacked specific prompts asked | Detailed below |

---

## Naming Convention

**Pascal Case** — used throughout for class names, method names, and properties.

---

## Flowchart

Process Flowchart (provided as a PNG).

---

## Project Structure

| File | Purpose |
|---|---|
| `Program.cs` | Main program logic, all menus, checkout, order history |
| `Product.cs` | Product class with stock management methods |
| `ShoppingCart.cs` | Cart item structure and subtotal computation |
| `OrderRecord.cs` | Stores completed transaction data for order history |
| `README.md` | Project documentation |

---

## Classes and Methods

- **Classes:** 3 (`Product`, `ShoppingCart`, `OrderRecord`)
- **Methods:** `DisplayProduct`, `GetItemTotal`, `HasEnoughStock`, `DeductStock`, `RestoreStock`, `SubTotal`, `BrowseAndAddProduct`, `SearchProduct`, `BrowseByCategory`, `ManageCart`, `ViewCart`, `RemoveItem`, `UpdateQuantity`, `ClearCart`, `Checkout`, `ViewOrderHistory`, `GetYesNo`
- **Main Variables:** 10+

---

## AI Usage

### Tools Used
1. **Claude Sonnet** (claude.ai) — used for Part 1 architecture and Part 2 planning
2. **GitHub Copilot** (VS Code built-in) — used during active coding for inline suggestions

---

### Part 1 — Exact Prompts Asked

**Prompt 1 (Claude):**
> "I need to build a console shopping cart in C#. What files and classes should I create? Give me an outline."

Used to get the initial file structure: `Product.cs`, `ShoppingCart.cs`, and `Program.cs`. Claude suggested separating product logic from cart logic into separate classes.

**Prompt 2 (Claude):**
> "How do I use int.TryParse in C# to validate numeric input from the console?"

Used to understand and implement input validation in `Program.cs`. The actual code was written manually after understanding the method.

**Prompt 3 (Copilot — inline suggestion):**
While typing the for loop for the duplicate cart check, Copilot auto-suggested the comparison `cart[i].Product.Id == selectedProduct.Id`. The logic was reviewed and accepted.

**Prompt 4 (Copilot — inline suggestion):**
While writing the stock deduction, Copilot suggested `RemainingStock -= quantity` inside `DeductStock()`. Accepted as-is since it matched the intended behavior.

---

### Part 2 — Exact Prompts Asked

**Prompt 5 (Claude):**
> "How do I format a DateTime in C# to show something like 'April 24, 2026 8:30 PM'?"

Used to implement the receipt date/time format. Answer: `DateTime.Now.ToString("MMMM dd, yyyy h:mm tt")`. Applied directly.

**Prompt 6 (Claude):**
> "How do I shift elements in a C# array after removing one element at a specific index without using List<T>?"

Used to implement the `RemoveItem` method. Claude explained the left-shift loop pattern. I wrote the loop myself after understanding the approach.

**Prompt 7 (Claude):**
> "What is a good way to enforce that a Y/N prompt in a console app keeps looping until the user types exactly Y or N?"

Used to write the `GetYesNo()` helper method. The structure was explained by Claude and then coded manually.

---

The good majority of the code was written by hand. AI was used to answer specific technical questions and explain C# behavior — not to generate complete solutions.

---

## Author: Carlos Moñiza

67676767676767


