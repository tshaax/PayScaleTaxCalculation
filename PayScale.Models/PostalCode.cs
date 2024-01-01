using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayScale.Models
{
    public class PostalCode
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDateTine { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]

        public required string Code { get; set; }
    }
}
