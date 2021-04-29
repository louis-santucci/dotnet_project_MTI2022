using System;
using System.Collections.Generic;
using FripShop.Dbo;

#nullable disable

namespace FripShop.Dbo
{
    public class DboUser : IDbo
    {
        public DboUser()
        {
            Articles = new HashSet<DboArticle>();
            Carts = new HashSet<DboCart>();
            Ratings = new HashSet<DboRating>();
            Transactions = new HashSet<DboTransaction>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public double Note { get; set; }

        public virtual ICollection<DboArticle> Articles { get; set; }
        public virtual ICollection<DboCart> Carts { get; set; }
        public virtual ICollection<DboRating> Ratings { get; set; }
        public virtual ICollection<DboTransaction> Transactions { get; set; }
    }
}
