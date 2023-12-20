using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayScale.Models
{
    public class PostalCode
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateTine { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public required string Code { get; set; }
    }
}
