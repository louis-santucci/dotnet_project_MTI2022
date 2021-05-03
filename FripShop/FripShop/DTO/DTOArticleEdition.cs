using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO article edition model
    /// </summary>
    public class DTOArticleEdition : IDTO
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a photo for your article...")]
        [DataType(DataType.Upload)]
        [Display(Name = "Name")]
        public string ImageSource { get; set; }

        [Required(ErrorMessage = "Please enter a name for your article...")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a price for your article...")]
        [DataType(DataType.Currency)]
        [Display(Name = "Name")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter a description for your article...")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Name")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a category for your article...")]
        [DataType(DataType.Text)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter a gender for your article...")]
        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Please enter a brand for your article...")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Please enter a condition for your article...")]
        [DataType(DataType.Custom)]
        [Display(Name = "Condition")]
        public int Condition { get; set; }
    }
}
