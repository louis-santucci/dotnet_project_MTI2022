using System.Collections.Generic;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    public partial class DboUser
    {
        public DboUser()
        {
            Articles = new HashSet<Models.EfModels.DboArticle>();
            Carts = new HashSet<Models.EfModels.DboCart>();
            Ratings = new HashSet<Models.EfModels.DboRating>();
            Transactions = new HashSet<Models.EfModels.DboTransaction>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public double Note { get; set; }

        public virtual ICollection<Models.EfModels.DboArticle> Articles { get; set; }
        public virtual ICollection<Models.EfModels.DboCart> Carts { get; set; }
        public virtual ICollection<Models.EfModels.DboRating> Ratings { get; set; }
        public virtual ICollection<Models.EfModels.DboTransaction> Transactions { get; set; }
    }
}
