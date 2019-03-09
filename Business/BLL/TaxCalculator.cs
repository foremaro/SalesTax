using System;
using Business.BLL.Enums;
using Business.BLL.Interfaces;

namespace Business.BLL
{
    public class TaxCalculator : ITaxCalculator
    {
        public void SetProductTax(Product product)
        {
            var taxPercentage = 0.0;
            if (product.IsImport)
            {
                taxPercentage += 0.05;
            }

            if (product.ProductType != ProductType.Book && product.ProductType != ProductType.Food &&
                product.ProductType != ProductType.Medical)
            {
                taxPercentage += 0.10;
            }
            
            var total = product.BasePrice * (decimal) taxPercentage;
            
            // round tax amount up -.05
            product.ItemTaxAmount = Math.Ceiling(total * 20) / 20;
        }
    }
}
