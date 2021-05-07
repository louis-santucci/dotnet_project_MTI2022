using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFripShop.CartTests
{
    class CartMockRepo
    {
        /// <summary>
        /// Converts a database entity to a dto entity asynchronously
        /// </summary>
        /// <param name="cartModel">The cart to convert</param>
        /// <returns>the converted cart</returns>
        public static async Task<DTOCart> DBOToDTOAsync(Cart cartModel)
        {
            var cart = new DTOCart();

            cart.Id = cartModel.Id;
            cart.ArticleId = cartModel.ArticleId;
            cart.BuyerId = cartModel.BuyerId;
            cart.Quantity = cartModel.Quantity;

            return cart;
        }

        /// <summary>
        /// Converts a database entity to a dto entity
        /// </summary>
        /// <param name="cartModel">The cart to convert</param>
        /// <returns>the converted cart</returns>
        public static DTOCart DBOToDTO(Cart cartModel)
        {
            var cart = new DTOCart();

            cart.Id = cartModel.Id;
            cart.ArticleId = cartModel.ArticleId;
            cart.BuyerId = cartModel.BuyerId;
            cart.Quantity = cartModel.Quantity;

            return cart;
        }

        /// <summary>
        /// Converts a database entity to a dto entity
        /// </summary>
        /// <param name="userModel">The cart to convert</param>
        /// <returns>the converted cart</returns>
        public static Cart DTOToDBO(DTOCart cartModel)
        {
            var cart = new Cart();

            cart.Id = cartModel.Id;
            cart.ArticleId = cartModel.ArticleId;
            cart.BuyerId = cartModel.BuyerId;
            cart.Quantity = cartModel.Quantity;

            return cart;
        }

        /// <summary>
        /// Converts a database entity to a dto entity list
        /// </summary>
        /// <param name="cartModels">The cart list to convert</param>
        /// <returns>the converted cart list</returns>
        public static List<DTOCart> DBOTODTOList(IEnumerable<Cart> cartModels)
        {
            List<DTOCart> result = new List<DTOCart>();
            foreach (var cartModel in cartModels)
            {
                var cart = new DTOCart();

                cart.Id = cartModel.Id;
                cart.ArticleId = cartModel.ArticleId;
                cart.BuyerId = cartModel.BuyerId;
                cart.Quantity = cartModel.Quantity;

                result.Add(cart);
            }

            return result;
        }

        public readonly ICartRepo _mockRepo;

        public List<Cart> _cartsMockList = new List<Cart>();

        /// <summary>
        /// Public constructor for the mocked repository
        /// </summary>
        /// <param name="carts">optional cart list to mock</param>
        public CartMockRepo(List<Cart> carts = null)
        {
            if (carts != null)
                _cartsMockList = carts;
            
            var mockRepo = new Mock<ICartRepo>();

            // Mocks the function Get()
            mockRepo.Setup(cartRepo => cartRepo.Get("")).ReturnsAsync(DBOTODTOList(_cartsMockList));

            // Mocks the function Insert()
            mockRepo.Setup(cartRepo => cartRepo.Insert(It.IsAny<DTOCart>())).ReturnsAsync((DTOCart cartModel) =>
            {
                long max = 1;
                if (_cartsMockList.Count() != 0)
                    max = _cartsMockList.Max(c => c.Id) + 1;

                var cart = DTOToDBO(cartModel);
                cart.Id = max;

                _cartsMockList.Add(cart);

                return DBOToDTO(cart);                
            });

            // Mocks the function Update()
            mockRepo.Setup(cartRepo => cartRepo.Update(It.IsAny<DTOCart>())).ReturnsAsync((DTOCart cartModel) =>
            {
                var result = _cartsMockList.Where(c => c.Id == cartModel.Id);
                if (result.Count() != 1)
                    return null;

                var cart = result.First();

                cart.ArticleId = cartModel.ArticleId;
                cart.BuyerId = cartModel.BuyerId;
                cart.Quantity = cartModel.Quantity;

                return DBOToDTO(cart);
            });

            // Mocks the function Delete()
            mockRepo.Setup(cartRepo => cartRepo.Delete(It.IsAny<long>())).ReturnsAsync((long i) =>
            {
                var cart = _cartsMockList.Where(c => c.Id == i);
                if (cart.Count() == 1)
                {
                    _cartsMockList.Remove(cart.First());
                    return true;
                }

                return false;
            });

            // Mocks the function GetById()
            mockRepo.Setup(cartRepo => cartRepo.GetById(It.IsAny<long>())).Returns((long i) =>
            {
                var count = _cartsMockList.Count(c => c.Id == i);
                if (count != 1)
                    return null;

                return DBOToDTOAsync(_cartsMockList.Single(c => c.Id == i));
            });

            // Mocks the function Count()
            mockRepo.Setup(cartRepo => cartRepo.Count()).ReturnsAsync(_cartsMockList.Count());

            // Mocks the function GetCartByUserId()
            mockRepo.Setup(cartRepo => cartRepo.GetCartByUserId(It.IsAny<long>())).ReturnsAsync((long i) =>
            {
                List<DTOCart> carts = new List<DTOCart>();

                var result = _cartsMockList.Where(c => c.BuyerId == i).ToList();
                foreach (Cart cart in result)
                {
                    carts.Add(DBOToDTO(cart));
                }

                return carts;
            });

            // Mocks the function GetCartItemByArticleId()
            mockRepo.Setup(cartRepo => cartRepo.GetCartItemByArticleId(It.IsAny<long>())).ReturnsAsync((long i) =>
            {
                var result = _cartsMockList.Find(c => c.ArticleId == i);
                if (result == null)
                    return null;

                DTOCart cart = DBOToDTO(result);

                return cart;
            });

            // Mocks the function UserCartAlreadyContains()
            mockRepo.Setup(cartRepo => cartRepo.UserCartAlreadyContains(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync((long articleId, long buyerId) =>
            {
                var result = _cartsMockList.Find(c => (c.ArticleId == articleId && c.BuyerId == buyerId));
                if (result == null)
                    return false;

                return true;
            });

            this._mockRepo = mockRepo.Object;
        }
    }
}
