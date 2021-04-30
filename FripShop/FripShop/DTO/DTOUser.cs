using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FripShop.DTO
{
    public class DTOUser : IDTO
    {
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public double Note { get; set; }
    }
}
