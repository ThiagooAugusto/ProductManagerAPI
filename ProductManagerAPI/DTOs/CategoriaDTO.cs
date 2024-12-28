using ProductManagerAPI.Vallidation;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class CategoriaDTO
    {
        [Required]
        public int Id {  get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="Nome deve ter no máximo 100 caracteres!")]
        [NaoContemNumero]
        public string Nome { get; set; }

    }
}
