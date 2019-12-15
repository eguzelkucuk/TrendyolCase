namespace Trendyol.Core.Models
{
    public class Product : BaseModel
    {
        public Product(string title, double price, Category category)
        {
            Category = category ?? throw new System.ArgumentNullException(nameof(category));

            if (price < 0) throw new System.ArgumentOutOfRangeException(nameof(price));
            Title = title;
            Price = price;
        }

        private string _title { get; set; }
        private double _price { get; set; }
        private Category _category { get; set; }

        public string Title { get { return _title; } set { _title = value; } }
        public double Price { get { return _price; } set { _price = value; } }
        public Category Category { get { return _category; } set { _category = value; } }
    }
}
