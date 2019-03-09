using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Business.BLL;

namespace DealerOn_SalesTax
{
    class Program
    {
        public static ShoppingCart Cart = new ShoppingCart(new SalesPriceCalculator(), new TaxCalculator());
        public static List<Product> Products = Product.GetProducts();

        static void Main(string[] args)
        {
            var addItemInputRegex = new Regex(@"[a-zA-Z] \d");

            InitializeUi();

            while (true)
            {
                var userInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    if (addItemInputRegex.IsMatch(userInput))
                    {
                        AddItemToCart(userInput);
                    }

                    else if (userInput == "~")
                    {
                        Checkout();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command.");
                    }

                }
            }

        }

        private static void AddItemToCart(string userInput)
        {
            var inputValues = userInput.Split(' ');
            var productCodeToAdd = inputValues[0];
            var productQuantity = Convert.ToInt32(inputValues[1]);

            var productToAdd = Products.FirstOrDefault(x => x.ItemCode.ToLower() == productCodeToAdd);

            if (productToAdd == null)
            {
                Console.WriteLine($"Product with code {productCodeToAdd} was not found.");
            }
            else
            {
                for (var i = 0; i < productQuantity; i++)
                {
                    Cart.Products.Add(productToAdd);
                }

                Console.WriteLine($"{productQuantity} {productToAdd.Name} at {productToAdd.BasePrice:F}");
            }
        }

        private static void Checkout()
        {
            Cart.GetCartTotals();

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------");

            // get grouped cart items
            var groupedCartItems = Cart.Products.GroupBy(x => x.ItemCode).Select(x => x.ToList()).ToList();

            foreach (var group in groupedCartItems)
            {
                if (group.Count > 1)
                {
                    var groupItemTotal = 0.0;
                    foreach (var prod in group)
                    {
                        groupItemTotal += (double) prod.TotalWithTax;
                    }

                    Console.WriteLine($"{group.FirstOrDefault()?.Name}: {groupItemTotal:F} ({group.Count} @ {group.FirstOrDefault()?.TotalWithTax:F})");
                }
                else
                {
                    Console.WriteLine($"{group.FirstOrDefault()?.Name}: {group.FirstOrDefault()?.TotalWithTax:F}");
                }
            }

            Console.WriteLine($"Sales Taxes: {Cart.TotalTax:F}");
            Console.WriteLine($"Total: {Cart.CartTotal:F}");
            Console.WriteLine("--------------------------------------------------------------");

            Console.ReadLine();
        }

        private static void InitializeUi()
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Please select the letter code (A,B,C, etc.) followed by quantity (Example: C 3): \n Enter '~' to complete order.");
            Console.WriteLine("Item Code\t| Product\t| Price\t");
            Console.WriteLine("--------------------------------------------------------------");

            foreach (var prod in Products)
            {
                Console.WriteLine($"[{prod.ItemCode}]\t\t| {prod.Name}\t\t|\t{prod.BasePrice}");
            }



            Console.WriteLine("--------------------------------------------------------------");
        }

        

    }
}
