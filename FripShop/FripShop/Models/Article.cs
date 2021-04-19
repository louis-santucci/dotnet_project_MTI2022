using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.Models
{
    public partial class Article
    {
        public long Id { get; set; }
        public string ImageSource { get; set; }
        public long SellerId { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Sex { get; set; }
        public string Brand { get; set; }
        public int Condition { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Rating Rating { get; set; }
    }
}
