using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Core.Enums;
using Trendyol.Core.Models;
using Xunit;

namespace Trendyol.Core.Test
{
    public class DeliveryTest
    {

        [Theory]
        [InlineData(0, 3.2, 9.1)]
        [InlineData(3.3, 0, 7.1)]
        [InlineData(5.3, 4.4, 0)]
        public void ArgumentNullException_Return_When_Delivery_Cost_Is_Zero(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            ShoppingCart cart = new ShoppingCart();
            var category = new Category("PC");
            cart.AddItem(new Product("Apple Notbook", 128.90, category), 5);

            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(costPerDelivery, costPerProduct, fixedCost);

            var result = Record.Exception(() => deliveryCostCalculator.calculateFor(cart));
            Assert.NotNull(result);
            var exception = Assert.IsType<ArgumentNullException>(result);

            var expected = nameof(costPerDelivery);
            if (costPerProduct == 0) expected = nameof(costPerProduct);
            else if (fixedCost == 0) expected = nameof(fixedCost);
            Assert.Equal(expected, exception.ParamName);
        }

        [Fact]
        public void ArgumentNullException_Return_When_ShoppingCart_Null()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(3.0, 2.0, 2.99);
            var result = Record.Exception(() => deliveryCostCalculator.calculateFor(null));
            Assert.NotNull(result);

            var exception = Assert.IsType<ArgumentNullException>(result);

            Assert.Equal("cart", exception.ParamName);
        }
    }
}
