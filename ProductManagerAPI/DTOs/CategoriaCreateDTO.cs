using ProductManagerAPI.Vallidation;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class CategoriaCreateDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres!")]
        [NaoContemNumero]
        public string Nome { get; set; }
    }
}
