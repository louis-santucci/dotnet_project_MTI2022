using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DTObjects
{
    public class User
    {
        public User()
        {
            Articles = new HashSet<Article>();
            Carts = new HashSet<Cart>();
            Ratings = new HashSet<Rating>();
            Transactions = new HashSet<Transaction>();
        }
        public User(long id, string userName, string email, string password, string name, string address, string? gender)
        {
            this.Id = id;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
            this.Name = name;
            this.Address = address;
            this.Gender = gender;

            this.Articles = new HashSet<Article>();
            this.Carts = new HashSet<Cart>();
            this.Ratings = new HashSet<Rating>();
            this.Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Gender { get; set; }
        public double Note { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
