using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.Models
{
    public class TaxRate
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateTine { get; set; } = DateTime.Now;

        [Required]
        public decimal Rate { get; set; }
        [Required]
        public decimal? From { get; set; }
        [Required]
        public decimal? To { get; set; }

        public int TaxTypeId { get; set; }

        [ForeignKey("TaxTypeId")]
        [ValidateNever]
        public TaxType? TaxType { get; set; }
    }
}
