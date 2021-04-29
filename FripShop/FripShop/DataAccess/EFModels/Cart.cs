namespace FripShop.DataAccess.EFModels
{
    public partial class Cart
    {
        public long Id { get; set; }
        public long BuyerId { get; set; }
        public long ArticleId { get; set; }
        public int Quantity { get; set; }

        public virtual Article Article { get; set; }
        public virtual User Buyer { get; set; }
    }
}
