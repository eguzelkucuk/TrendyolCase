using System.Collections.Generic;
using Trendyol.Core.Models;
using Xunit;

namespace Trendyol.Core.Test
{
    public class ProductList : TheoryData<string, double, Category>
    {
        public ProductList()
        {
            Add("MSI Notebook", 1000.98, new Category("Notebook"));
            Add("Lenovo Notebook", 200.17, new Category("Notebook"));
        }
    }
}