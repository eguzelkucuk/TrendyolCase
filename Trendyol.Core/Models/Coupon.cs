using Trendyol.Core.Enums;

namespace Trendyol.Core.Models
{
    public class Coupon : BaseModel
    {
        public Coupon(double minPurchase, double discount, DiscountType discountType)
        {
            MinPurchase = minPurchase;
            Discount = discount;
            DiscountType = discountType;
        }

        private double _minPurchase { get; set; }
        private double _discount { get; set; }
        private DiscountType _discountType { get; set; }

        public double MinPurchase { get { return _minPurchase; } set { _minPurchase = value; } }
        public double Discount { get { return _discount; } set { _discount = value; } }
        public DiscountType DiscountType { get { return _discountType; } set { _discountType = value; } }
    }
}