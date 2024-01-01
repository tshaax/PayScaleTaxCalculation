using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PayScale.Models
{
    public class PostalCodeTaxType
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int? PostalCodeId { get; set; }

        [ForeignKey("PostalCodeId")]
        [ValidateNever]
        public PostalCode? PostalCode { get; set; }

        [JsonIgnore]
        public int? TaxTypeId { get; set; }

        [ForeignKey("TaxTypeId")]
        [ValidateNever] 
        public TaxType? TaxType { get; set; }
    }
}
