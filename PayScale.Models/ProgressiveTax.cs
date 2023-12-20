using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.Models
{
    public class ProgressiveTax
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateTine { get; set; } = DateTime.Now;

        [Required]
        public int Rate { get; set; }
        [Required]
        public decimal From { get; set; }
        [Required]
        public decimal To { get; set; }
    }
}
