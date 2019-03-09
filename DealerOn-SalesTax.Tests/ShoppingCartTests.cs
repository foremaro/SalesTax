using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Business.BLL;
using Business.BLL.Enums;

namespace DealerOn_SalesTax.Tests
{
    [TestClass]
    public class ShoppingCartTests
    {
        public static List<Product> Products = Product.GetProducts();

        [TestMethod]
        public void CalculateBookTotalWithTaxTest()
        {
            var spc = new SalesPriceCalculator();
            var product = new Product()
            {
                Name = "Book",
                BasePrice = 12.49M,
                ProductType = ProductType.Book,
                ItemCode = "A",
                IsImport = false
            };

            spc.CalculateItemTotalWithTax(product);

            Assert.AreEqual(12.49M, product.TotalWithTax);
        }


        [TestMethod]
        public void CalculateMusicCDTotalWithTaxTest()
        {
            var cart = new ShoppingCart(new SalesPriceCalculator(), new TaxCalculator());

            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Music CD"));

            cart.GetCartTotals();

            Assert.AreEqual(16.49M, cart.CartTotal);
        }

        [TestMethod]
        public void Input_1_Test()
        {
            var cart = new ShoppingCart(new SalesPriceCalculator(), new TaxCalculator());

            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Book"));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Book"));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Music CD"));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Chocolate bar"));

            cart.GetCartTotals();

            Assert.AreEqual(1.50M, cart.TotalTax);
            Assert.AreEqual(42.32M, cart.CartTotal);
        }

        [TestMethod]
        public void Input_2_Test()
        {
            var cart = new ShoppingCart(new SalesPriceCalculator(), new TaxCalculator());

            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Imported box of chocolates" && x.BasePrice == 10.00M));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Imported bottle of perfume" && x.BasePrice == 47.50M));

            cart.GetCartTotals();

            Assert.AreEqual(7.65M, cart.TotalTax);
            Assert.AreEqual(65.15M, cart.CartTotal);
        }

        [TestMethod]
        public void Input_3_Test()
        {
            var cart = new ShoppingCart(new SalesPriceCalculator(), new TaxCalculator());

            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Imported bottle of perfume" && x.BasePrice == 27.99M));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Bottle of perfume"));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Packet of headache pills"));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Imported box of chocolates" && x.BasePrice == 11.25M));
            cart.Products.Add(Products.FirstOrDefault(x => x.Name == "Imported box of chocolates" && x.BasePrice == 11.25M));

            cart.GetCartTotals();

            Assert.AreEqual(7.30M, cart.TotalTax);
            Assert.AreEqual(86.53M, cart.CartTotal);
        }

    }
}