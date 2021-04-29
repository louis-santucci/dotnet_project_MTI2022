using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    public partial class User
    {
        public User()
        {
            Articles = new HashSet<Article>();
            Carts = new HashSet<Cart>();
            Ratings = new HashSet<Rating>();
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public double Note { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
