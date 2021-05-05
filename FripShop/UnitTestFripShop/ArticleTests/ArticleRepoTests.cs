using System;
using System.Collections.Generic;
using System.Linq;
using FripShop.DataAccess;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestFripShop.ArticleTests
{
    /// <summary>
    /// Class for article CRUD unit testing
    /// </summary>
    [TestClass]
    class ArticleRepoTests
    {

        public List<Article> articles = new List<Article>()
        {
            new Article()
            {
                Id = 1, ImageSource = null, SellerId = 1, State = "free", Name = "Joli haut", Description = "Joli haut tout bleu", Category = "top", Sex = "woman", Brand = "Converse", Condition = 10, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 2, ImageSource = null, SellerId = 1, State = "free", Name = "Joli bas", Description = "Joli bas tout vert T36", Category = "pants", Sex = "man", Brand = "Levis", Condition = 3, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 3, ImageSource = null, SellerId = 1, State = "free", Name = "Joli veste", Description = "", Category = "top", Sex = "woman", Brand = "Adidas", Condition = 2, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 4, ImageSource = null, SellerId = 1, State = "free", Name = "Jolies chaussures", Description = "", Category = "shoes", Sex = "woman", Brand = "Converse", Condition = 10, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 5, ImageSource = null, SellerId = 1, State = "free", Name = "pantalon", Description = "", Category = "pants", Sex = "woman", Brand = "Dickies", Condition = 9, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 6, ImageSource = null, SellerId = 1, State = "free", Name = "pull kaki", Description = "", Category = "top", Sex = "woman", Brand = "Supreme", Condition = 8, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 7, ImageSource = null, SellerId = 1, State = "free", Name = "serviette verte", Description = "", Category = "accessories", Sex = "woman", Brand = "Nike", Condition = 6, CreatedAt = DateTime.Now
            },
            new Article()
            {
                Id = 8, ImageSource = null, SellerId = 1, State = "free", Name = "Lunettes noires", Description = "Ray Ban noires", Category = "accessories", Sex = "woman", Brand = "RayBan", Condition = 10, CreatedAt = DateTime.Now
            },

        };



        [TestMethod]
        public void Test()
        {

        }
    }
}
