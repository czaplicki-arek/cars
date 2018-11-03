using System.ComponentModel.DataAnnotations;

namespace CarStorage.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}