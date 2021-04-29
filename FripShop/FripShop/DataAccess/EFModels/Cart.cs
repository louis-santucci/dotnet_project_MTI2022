namespace FripShop.DataAccess.EFModels
{
    public partial class DboCart
    {
        public long Id { get; set; }
        public long BuyerId { get; set; }
        public long ArticleId { get; set; }
        public int Quantity { get; set; }

        public virtual Models.EfModels.DboArticle Article { get; set; }
        public virtual DboUser Buyer { get; set; }
    }
}
