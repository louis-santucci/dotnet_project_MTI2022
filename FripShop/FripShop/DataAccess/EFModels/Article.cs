using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    /// <summary>
    /// Class for EntityFrameworkCore article model
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Public constructor for EntityFrameworkCore article model
        /// </summary>
        public Article()
        {
            Carts = new HashSet<Cart>();
            Transactions = new HashSet<Transaction>();
        }

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

        [IgnoreMap]
        public virtual User Seller { get; set; }
        [IgnoreMap]
        public virtual ICollection<Cart> Carts { get; set; }
        [IgnoreMap]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
