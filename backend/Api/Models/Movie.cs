using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Titulo { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        
        public decimal Precio { get; set; }
        
        public int CategoryId { get; set; }
        
        public string Estado { get; set; } = "Activa";

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
