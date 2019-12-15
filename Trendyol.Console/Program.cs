using System;
using System.Collections.Generic;
using Trendyol.Core;
using Trendyol.Core.Enums;
using Trendyol.Core.Models;

namespace Trendyol.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Category category = new Category("food");

            Product apple = new Product("Apple", 100.0, category);
            Product almond = new Product("Almonds", 150.0, category);

            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(apple, 3);
            cart.AddItem(almond, 1);

            List<Campaign> campaignList = new List<Campaign>();
            campaignList.Add(new Campaign(category, 20.0, 3, DiscountType.Rate));
            campaignList.Add(new Campaign(category, 50.0, 5, DiscountType.Rate));
            campaignList.Add(new Campaign(category, 5.0, 5, DiscountType.Amount));
            cart.applyDiscounts(campaignList);

            Coupon coupon = new Coupon(100, 10, DiscountType.Rate);
            cart.applyCoupon(coupon);

            cart.Print();

            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(DeliveryCostVariable.costPerDelivery, DeliveryCostVariable.costPerProduct, DeliveryCostVariable.fixedCost);
            deliveryCostCalculator.calculateFor(cart);
        }
    }
}
