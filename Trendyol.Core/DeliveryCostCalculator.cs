using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trendyol.Core.Models;

namespace Trendyol.Core
{
    public class DeliveryCostCalculator
    {
        private double costPerDelivery;
        private double costPerProduct;
        private double fixedCost;

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            this.costPerDelivery = costPerDelivery;
            this.costPerProduct = costPerProduct;
            this.fixedCost = fixedCost;
        }

        public void calculateFor(ShoppingCart cart)
        {
            if (costPerDelivery == 0) throw new ArgumentNullException(nameof(costPerDelivery));
            if (costPerProduct == 0) throw new ArgumentNullException(nameof(costPerProduct));
            if (fixedCost == 0) throw new ArgumentNullException(nameof(fixedCost));

            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var numberOfDeliveries = cart.ShoppingCartList.GroupBy(g => g.Product.Category).Count();
            var numberOfProduct = cart.ShoppingCartList.GroupBy(g => g.Product).Count();

            Console.WriteLine($"Total Amount : {cart.getTotalAmountAfterDiscounts()} TL  Delivery Cost : {(costPerDelivery * numberOfDeliveries) + (costPerProduct * numberOfProduct) + fixedCost} TL");
        }
    }
}
