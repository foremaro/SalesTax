using System.Collections.Generic;
using Business.BLL.Enums;

namespace Business.BLL
{
    public class Product
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string ItemCode { get; set; }
        public decimal ItemTaxAmount { get; set; }
        public decimal TotalWithTax { get; set; }
        public bool IsImport { get; set; }
        public ProductType ProductType { get; set; }

        public static List<Product> GetProducts()
        {
            var results = new List<Product>
            {
                new Product
                {
                    Name = "Book", BasePrice = 12.49M, ProductType = ProductType.Book, ItemCode = "A", IsImport = false
                },
                new Product
                {
                    Name = "Music CD", BasePrice = 14.99M, ProductType = ProductType.Music, ItemCode = "B",
                    IsImport = false
                },
                new Product
                {
                    Name = "Chocolate bar", BasePrice = 0.85M, ProductType = ProductType.Food, ItemCode = "C",
                    IsImport = false
                },
                new Product
                {
                    Name = "Imported box of chocolates", BasePrice = 10.00M, ProductType = ProductType.Food,
                    ItemCode = "D", IsImport = true
                },
                new Product
                {
                    Name = "Imported bottle of perfume", BasePrice = 47.50M, ProductType = ProductType.Misc,
                    ItemCode = "E", IsImport = true
                },
                new Product
                {
                    Name = "Packet of headache pills", BasePrice = 9.75M, ProductType = ProductType.Medical,
                    ItemCode = "F", IsImport = false
                },
                new Product
                {
                    Name = "Imported bottle of perfume", BasePrice = 27.99M, ProductType = ProductType.Misc,
                    ItemCode = "G", IsImport = true
                },
                new Product
                {
                    Name = "Bottle of perfume", BasePrice = 18.99M, ProductType = ProductType.Misc,
                    ItemCode = "H", IsImport = false
                },
                new Product
                {
                    Name = "Imported box of chocolates", BasePrice = 11.25M, ProductType = ProductType.Food,
                    ItemCode = "I", IsImport = true
                },

            };

            return results;
        }
    }


}
