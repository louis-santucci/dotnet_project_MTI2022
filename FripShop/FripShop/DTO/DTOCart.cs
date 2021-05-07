using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public virtual DTOArticle Article { get; set; }
        [NotMapped]
        public virtual DTOUserPublic Buyer { get; set; }
    }
}
