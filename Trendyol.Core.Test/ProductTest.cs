using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Core.Models;
using Xunit;

namespace Trendyol.Core.Test
{
    public class ProductTest
    {
        [Theory]
        [ClassData(typeof(ProductList))]
        public void Create_New_Product_ShouldAssert_NotNull_And_ShouldAssert_Equal_Price(string productTitle, double price, Category category)
        {
            Product product = new Product(productTitle, price, category);
            Assert.NotNull(product);
            Assert.Equal(price, product.Price);
        }

        [Fact]
        public void Create_New_Product_WithOut_Category()
        {
            var result = Record.Exception(() => new Product("MSI Laptop", 1000.28, null));
            Assert.NotNull(result);
            var exception = Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("category", exception.ParamName);
        }

        [Fact]
        public void Create_New_Product_Out_Of_Price_ShouldBe_Error()
        {
            var result = Record.Exception(() => new Product("MSI Laptop", -500.22, new Category("Notebook")));
            Assert.NotNull(result);
            var exception = Assert.IsType<ArgumentOutOfRangeException>(result);
            Assert.Equal("price", exception.ParamName);
        }
    }
}
