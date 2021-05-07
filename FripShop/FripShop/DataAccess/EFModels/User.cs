using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using AutoMapper;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    /// <summary>
    /// Class for EntityFrameworkCore user model
    /// </summary>
    public class User
    {
        /// <summary>
        /// Public constructor for EntityFrameworkCore user model
        /// </summary>
        public User()
        {
            Articles = new HashSet<Article>();
            Carts = new HashSet<Cart>();
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
        public long NbNotes { get; set; }

        [IgnoreMap]
        public virtual ICollection<Article> Articles { get; set; }
        [IgnoreMap]
        public virtual ICollection<Cart> Carts { get; set; }
        [IgnoreMap]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
