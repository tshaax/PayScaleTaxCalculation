using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PayScale.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PayScale.WebRazor.Models
{
    public class TaxCalculation
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal AmountAfterTax { get; set; }
        [Required]
        public int PostalCodeId { get; set; }

        [ForeignKey("PostalCodeId")]
        [ValidateNever]
        [JsonIgnore]
        public required List<PostalCode> PostalCodes { get; set; }
    }
}
