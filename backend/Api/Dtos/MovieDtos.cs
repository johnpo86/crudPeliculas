using System.ComponentModel.DataAnnotations;
using Api.Validations;

namespace Api.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }

    public class MovieCreateDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        
        //Validacion personalizada
        [PositivePrice]
        public decimal Precio { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        public string Estado { get; set; } = "Activa";
    }

    public class MovieUpdateDto : MovieCreateDto
    {
        public int Id { get; set; }
    }

    public class MovieReadDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioUsd { get; set; }
        public decimal PrecioCop { get; set; }
        public int CategoryId { get; set; }
        public string CategoryNombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
