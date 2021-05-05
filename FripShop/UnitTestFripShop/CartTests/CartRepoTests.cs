using System;
using System.Collections.Generic;
using System.Text;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestFripShop.CartTests
{
    /// <summary>
    /// Claxs for article CRUD unit testing
    /// </summary>
    [TestClass]
    public class CartRepoTests
    {

        public readonly ICartRepo _MockRepo;

        public List<Cart> _carts = new List<Cart>
        {
            new Cart()
            {
                Id = 1,
                Quantity = 1,

            }
        }
    }
}
