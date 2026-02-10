using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        public string Nombre { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
    }
}
