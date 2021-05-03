using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO user login model
    /// </summary>
    public class DTOLoginUser : DTOUser, IDTO
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter your email...")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
