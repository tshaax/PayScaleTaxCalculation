using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PayScale.Models
{
    public class TaxCalculation
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Display(Name = "Salary")]
        [Required(ErrorMessage = "{0} is required")]
        public decimal Amount { get; set; } = 0.0M;
        [Required]
        public decimal AmountAfterTax { get; set; }
        [Required]
        public int PostalCodeId { get; set; }

        [ForeignKey("PostalCodeId")]
        [ValidateNever]
        [JsonIgnore]
        public  List<PostalCode>? PostalCodes { get; set; }
    }
}
