using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 100)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Price for 1 - 50")]
        [Range(1, 50)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+")]
        //[Range(50, int.MaxValue)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+")]
        //[Range(100,int.MaxValue)]
        public double Price100 { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
