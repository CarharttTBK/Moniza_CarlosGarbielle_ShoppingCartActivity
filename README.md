# Moniza_CarlosGarbielle_ShoppingCartActivity

---

How to run? 

Select Program.cs and type `dotnet run` on console.

---

This project is a simple console-based shopping cart system developed using C#. It allows users to select products, add them to a cart, and compute the total amount with stock management and discount features.

---

## Features

* Displays a store menu with available products
* Allows user to select items and input quantity and validates user input (product number and quantity)
* Prevents invalid inputs (non-numeric, negative, zero) and handles out-of-stock products
* Prevents purchasing more than available stock
* Updates remaining stock after each purchase and prevents duplicate cart entries by updating existing items
* Displays receipt with itemized list
* Calculates Grand Total
* Applies 10% discount if total is ₱5000 or more
* Displays Final Total after discount

---

## Naming Convention Used

*Pascal Case* has been chosen.

---

## Flowchart Used

Process Flowchart, in a png

---

## Project Details

* Main Variables: 10+
* Classes: 2 (Product, CartItem)
* Methods: Multiple (DisplayProduct, GetItemTotal, HasEnoughStock, DeductStock, SubTotal)

---

## AI USAGE

1. Parts where AI helped:

   * Structuring the classes (Product and CartItem)
   * Organizing the program flow in Program.cs
   * Implementing input validation using int.TryParse()
   * Handling edge cases such as stock limits and duplicate cart entries

2. Why I used AI:

   * I struggled with understanding the process with C# so I used AI to give me an outline of the files and architecture of the program to be made. The AI used was Claude Sonna Ext.
   * I also used it for ideas on how to check the logic, as well to ensure proper validation and avoid runtime errors.
The AI used was VS Code's inbuilt Copilot.
   - The good majority of the code was handwritten and made by me. AI only served as to guide me with the outline and errors along the way. 

---

## Structure

* Program.cs → Main program logic
* Product.cs → Product class and stock handling
* CartItem.cs → Cart item structure and subtotal computation
* README.md → Project documentation
* Flowchart → Visual representation of system process



Author:
Moniza, Gab

6 7
6 7 
6 7
