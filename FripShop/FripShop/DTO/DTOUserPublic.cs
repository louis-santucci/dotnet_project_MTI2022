using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO public user model
    /// </summary>
    public class DTOUserPublic : IDTO
    {
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public double Note { get; set; }
    }
}
