using System.ComponentModel.DataAnnotations;

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO cart model
    /// </summary>
    public class DTOCart : IDTO
    {
        [Key]
        public long Id { get; set; }
        public long BuyerId { get; set; }
        public long ArticleId { get; set; }
        public int Quantity { get; set; }

        public virtual DTOArticle Article { get; set; }
        public virtual DTOUserPublic Buyer { get; set; }
    }
}
