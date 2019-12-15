using Trendyol.Core.Enums;

namespace Trendyol.Core.Models
{
    public class Campaign : BaseModel
    {
        public Campaign(Category category, double discount, int quantity, DiscountType discountType)
        {
            Category = category ?? throw new System.ArgumentNullException(nameof(category));
            Discount = discount;
            Quantity = quantity;
            DiscountType = DiscountType;
        }
        private Category _category { get; set; }
        private double _discount { get; set; }
        private int _quantity { get; set; }
        private DiscountType _discountType { get; set; }

        public Category Category { get { return _category; } set { _category = value; } }
        public double Discount { get { return _discount; } set { _discount = value; } }
        public int Quantity { get { return _quantity; } set { _quantity = value; } }
        public DiscountType DiscountType { get { return _discountType; } set { _discountType = value; } }
    }
}