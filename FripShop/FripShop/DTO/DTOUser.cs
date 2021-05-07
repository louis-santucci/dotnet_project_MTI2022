using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO private user model
    /// </summary>
    public class DTOUser : IDTO
    {
        [Key]
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public double Note { get; set; }

        public long NbNotes { get; set; }
    }
}
