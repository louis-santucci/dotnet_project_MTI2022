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
        public new long Id { get; set; }

        [Required(ErrorMessage = "Please enter your email...")]
        [Display(Name = "Email")]
        public new string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public new string Password { get; set; }
    }
}
