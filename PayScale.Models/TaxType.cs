using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PayScale.Models
{
    public class TaxType
    {
        [Key]

        public int Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]

        public required string TaxCalculationType { get; set;}
    }
}
