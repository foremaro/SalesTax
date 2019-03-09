using Business.BLL.Interfaces;

namespace Business.BLL
{
    public class SalesPriceCalculator : ISalesPriceCalculator
    {
        public void CalculateItemTotalWithTax(Product product)
        {
            var total = product.BasePrice + product.ItemTaxAmount;

            product.TotalWithTax = total;
        }
    }
}
