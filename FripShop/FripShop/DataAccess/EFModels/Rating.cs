#nullable disable

namespace FripShop.DataAccess.EFModels
{
    public partial class DboRating
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long SellerId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }

        public virtual DboArticle Article { get; set; }
        public virtual DboUser Seller { get; set; }
    }
}
