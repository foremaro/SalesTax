namespace Business.BLL.Interfaces
{
    public interface ISalesPriceCalculator
    {
        void CalculateItemTotalWithTax(Product product);
    }
}
