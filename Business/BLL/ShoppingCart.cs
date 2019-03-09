using System;
using System.Collections.Generic;
using Business.BLL.Interfaces;

namespace Business.BLL
{
    public class ShoppingCart
    {
        private readonly ISalesPriceCalculator _salesPriceCalculator;
        private readonly ITaxCalculator _taxCalculator;
        

        public List<Product> Products { get; set; }
        public decimal CartTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalItemPrice { get; set; }

        public ShoppingCart(ISalesPriceCalculator salesPriceCalculator,ITaxCalculator taxCalculator)
        {
            _salesPriceCalculator = salesPriceCalculator;
            _taxCalculator = taxCalculator;

            Products = new List<Product>();
        }

        public void GetCartTotals()
        {
            var cartTotal = 0.0;
            var taxTotal = 0.0;
            var totalItemPrice = 0.0;

            foreach (var product in Products)
            {
                _taxCalculator.SetProductTax(product);
                _salesPriceCalculator.CalculateItemTotalWithTax(product);

                taxTotal += (double) product.ItemTaxAmount;
                totalItemPrice += (double) product.BasePrice;
                cartTotal += (double) product.TotalWithTax;
            }

            TotalItemPrice = (decimal) totalItemPrice;
            TotalTax = (decimal)Math.Ceiling(taxTotal * 20) / 20; 
            CartTotal = (decimal) cartTotal;
        }

    }
}
