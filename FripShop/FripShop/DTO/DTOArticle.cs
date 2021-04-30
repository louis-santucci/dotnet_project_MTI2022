using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FripShop.Dbo;

namespace FripShop.Dbo
{
    public class DboArticle : IDbo
    {
        public DboArticle()
        {
            Carts = new HashSet<DboCart>();
            Transactions = new HashSet<DboTransaction>();
        }

        [Key]
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
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<DboCart> Carts { get; set; }
        public virtual ICollection<DboTransaction> Transactions { get; set; }
    }
}
