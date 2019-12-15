using System;
using Trendyol.Core.Models;
using Xunit;

namespace Trendyol.Core.Test
{
    public class CategoriesTest
    {
        [Fact]
        public void Create_New_Category_ShouldBe_NotNull()
        {
            Category category = new Category("food");
            Assert.NotNull(category);
        }

        [Fact]
        public void Empty_Category_ShouldBe_Return_Error()
        {
            var result = Record.Exception(() => new Category(null));
            Assert.NotNull(result);

            var exception = Assert.IsType<ArgumentNullException>(result);

            Assert.Equal("title", exception.ParamName);
        }
    }
}
