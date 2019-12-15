using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trendyol.Core.Enums;
using Trendyol.Core.Models;
using Xunit;

namespace Trendyol.Core.Test
{
    public class ShoppingCartTests
    {
        [Fact]
        public void Create_New_ShoppingCart_WithOut_Product()
        {
            ShoppingCart cart = new ShoppingCart();
            var result = Record.Exception(() => cart.AddItem(null, 5));
            Assert.NotNull(result);
            var exception = Assert.IsType<ArgumentNullException>(result);

            Assert.Equal("product", exception.ParamName);
        }

        [Fact]
        public void Coupon_Use_ShoppingCart_ShouldBe_Discounted_Price()
        {
            ShoppingCart cart = new ShoppingCart();
            var category = new Category("PC");
            cart.AddItem(new Product("Apple", 128.90, category), 5);

            List<Campaign> campaignList = new List<Campaign>();
            campaignList.Add(new Campaign(category, 20.0, 3, DiscountType.Rate));
            cart.applyDiscounts(campaignList);

            Assert.Equal(515.6, cart.ShoppingCartList.Sum(s => s.TotalPrice));
        }

        [Fact]
        public void Reapply_An_Applied_Campaign_And_Return_Of_Exception()
        {
            ShoppingCart cart = new ShoppingCart();
            var category = new Category("PC");
            cart.AddItem(new Product("Apple Notbook", 128.90, category), 5);

            List<Campaign> campaignList = new List<Campaign>();
            campaignList.Add(new Campaign(category, 20.0, 3, DiscountType.Rate));
            cart.applyDiscounts(campaignList);

            var result = Record.Exception(() => cart.applyDiscounts(campaignList));
            Assert.NotNull(result);
            var exception = Assert.IsType<Exception>(result);
            Assert.Equal("Bu kampanya daha önce uygulanmış!", exception.Message);
        }


        [Fact]
        public void Reapply_An_Applied_Coupon_And_Return_Of_Exception()
        {
            ShoppingCart cart = new ShoppingCart();
            var category = new Category("PC");
            cart.AddItem(new Product("Apple Notbook", 128.90, category), 5);
            Coupon coupon = new Coupon(100, 10, DiscountType.Rate);
            cart.applyCoupon(coupon);

            var result = Record.Exception(() => cart.applyCoupon(coupon));
            Assert.NotNull(result);
            var exception = Assert.IsType<Exception>(result);
            Assert.Equal("Bu kupon kodu daha önce uygulanmış!", exception.Message);
        }

        [Fact]
        public void Total_Amount_After_Discounts_Null_Cart_Error_Returns()
        {
            ShoppingCart cart = new ShoppingCart();

            var result = Record.Exception(() => cart.getTotalAmountAfterDiscounts());
            Assert.NotNull(result);
            var exception = Assert.IsType<NullReferenceException>(result);
            Assert.Equal("ShoppingCart Boş!", exception.Message);
        }

        [Fact]
        public void Total_Coupon_Discount_Null_Cart_Error_Returns()
        {
            ShoppingCart cart = new ShoppingCart();

            var result = Record.Exception(() => cart.getCouponDiscount());
            Assert.NotNull(result);
            var exception = Assert.IsType<NullReferenceException>(result);
            Assert.Equal("ShoppingCart Boş!", exception.Message);
        }

        [Fact]
        public void Total_Campaign_Discount_Null_Cart_Error_Returns()
        {
            ShoppingCart cart = new ShoppingCart();

            var result = Record.Exception(() => cart.getCampaignDiscount());
            Assert.NotNull(result);
            var exception = Assert.IsType<NullReferenceException>(result);
            Assert.Equal("ShoppingCart Boş!", exception.Message);
        }

        [Fact]
        public void Total_Delivert_Cost_Null_Cart_Error_Returns()
        {
            ShoppingCart cart = new ShoppingCart();

            var result = Record.Exception(() => cart.getDeliveryCost());
            Assert.NotNull(result);
            var exception = Assert.IsType<NullReferenceException>(result);
            Assert.Equal("ShoppingCart Boş!", exception.Message);
        }

        [Fact]
        public void Total_Delivert_Cost_ShouldBe_Return_Equal()
        {
            ShoppingCart cart = new ShoppingCart();
            var category = new Category("PC");
            cart.AddItem(new Product("Apple", 128.90, category), 5);
            var actual = cart.getDeliveryCost();
            Assert.Equal(8.45, actual);
        }




    }
}
