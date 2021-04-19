using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.Models
{
    public partial class User
    {
        public User()
        {
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

        public virtual Article Article { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Rating Rating { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
