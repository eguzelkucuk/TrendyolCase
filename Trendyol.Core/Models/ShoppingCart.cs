using System;
using System.Collections.Generic;
using System.Linq;

namespace Trendyol.Core.Models
{
    public class ShoppingCart : BaseModel
    {
        private ShoppingCart(Product product, int quantity, double totalPrice)
        {
            Product = product;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        public List<ShoppingCart> ShoppingCartList;
        public ShoppingCart()
        {
            ShoppingCartList = new List<ShoppingCart>();
        }

        private List<Coupon> AppliedCoupon { get; set; }
        private List<Campaign> AppliedCampaign { get; set; }
        private double _totalPrice { get; set; }
        private double _appliedCouponTotal { get; set; }
        private double _appliedCampaignTotal { get; set; }
        private int _quantity { get; set; }


        public Product Product { get; set; }
        public double TotalPrice { get { return _totalPrice; } set { _totalPrice = value; } }
        public double AppliedCouponTotal { get { return _appliedCouponTotal; } set { _appliedCouponTotal = value; } }
        public double AppliedCampaignTotal { get { return _appliedCampaignTotal; } set { _appliedCampaignTotal = value; } }
        public int Quantity { get { return _quantity; } set { _quantity = value; } }

        public void applyCoupon(Coupon coupon)
        {
            if (coupon != null)
            {
                if (ShoppingCartList.Sum(x => x.TotalPrice) >= coupon.MinPurchase)
                    foreach (var cart in ShoppingCartList)
                    {
                        if (cart.AppliedCoupon == null) cart.AppliedCoupon = new List<Coupon>();

                        if (cart.AppliedCoupon.Count(a => a == coupon) == 0)
                        {
                            var couponDiscount = coupon.DiscountType == Enums.DiscountType.Amount ? (coupon.Discount) : (cart.TotalPrice / 100) * coupon.Discount;
                            cart.TotalPrice = cart.TotalPrice - couponDiscount;
                            cart.AppliedCouponTotal = couponDiscount;
                            cart.AppliedCoupon.Add(coupon);
                        }
                        else throw new Exception("Bu kupon kodu daha önce uygulanmış!");
                    }
            }
        }
        public void applyDiscounts(List<Campaign> campaignList)
        {
            if (campaignList != null)
                foreach (var item in campaignList)
                {
                    foreach (var cart in ShoppingCartList.Where(x => x.Quantity >= item.Quantity))
                    {
                        if (cart.AppliedCampaign == null) cart.AppliedCampaign = new List<Campaign>();

                        if (cart.AppliedCampaign.Count(a => a == item) == 0)
                        {
                            var campaignDiscount = item.DiscountType == Enums.DiscountType.Amount
                                ? item.Discount
                                : cart.Product.Price / 100.0 * (item.Discount);

                            cart.TotalPrice = (cart.Product.Price - campaignDiscount) * cart.Quantity;

                            cart.AppliedCampaignTotal = campaignDiscount * cart.Quantity;
                            cart.AppliedCampaign.Add(item);
                        }
                        else throw new Exception("Bu kampanya daha önce uygulanmış!");
                    }
                }
        }
        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            if (quantity > 0)
                ShoppingCartList.Add(new ShoppingCart(product, quantity, product.Price));
        }
        public double getTotalAmountAfterDiscounts()
        {
            if (ShoppingCartList.Count() == 0) throw new NullReferenceException("ShoppingCart Boş!");
            return ShoppingCartList.Sum(s => s.TotalPrice);
        }
        public double getCouponDiscount()
        {
            if (ShoppingCartList.Count() == 0) throw new NullReferenceException("ShoppingCart Boş!");
            return ShoppingCartList.Sum(s => s.AppliedCouponTotal);
        }
        public double getCampaignDiscount()
        {
            if (ShoppingCartList.Count() == 0) throw new NullReferenceException("ShoppingCart Boş!");
            return ShoppingCartList.Sum(s => s.AppliedCampaignTotal);
        }
        public double getDeliveryCost()
        {
            if (ShoppingCartList.Count() == 0) throw new NullReferenceException("ShoppingCart Boş!");
            var numberOfDeliveries = ShoppingCartList.GroupBy(g => g.Product.Category).Count();
            var numberOfProduct = ShoppingCartList.GroupBy(g => g.Product).Count();
            return (DeliveryCostVariable.costPerDelivery * numberOfDeliveries) + (DeliveryCostVariable.costPerProduct * numberOfProduct) + DeliveryCostVariable.fixedCost;
        }
        public void Print()
        {
            foreach (var item in ShoppingCartList.GroupBy(g => g.Product.Category))
            {
                Console.WriteLine(item.Key.Title);
                foreach (var cartItem in ShoppingCartList.Where(x => x.Product.Category == item.Key))
                {
                    Console.WriteLine($"{cartItem.Product.Title} {cartItem.Quantity} {cartItem.Product.Price} TL {cartItem.TotalPrice} TL {cartItem.AppliedCampaignTotal + cartItem.AppliedCouponTotal} TL (Coupon Discount : {cartItem.AppliedCouponTotal} TL, Campaign Discount : {cartItem.AppliedCampaignTotal} TL)");
                }
            }
        }
    }
}