using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO rating model
    /// </summary>
    public class DTORating : IDTO
    {
        [Key]
        public long Id { get; set; }
        public long SellerId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }
    }
}
