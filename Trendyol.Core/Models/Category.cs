using System;

namespace Trendyol.Core.Models
{
    public class Category : BaseModel
    {
        public Category(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
        private string _title { get; set; }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
    }
}
