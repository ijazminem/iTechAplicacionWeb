using System.ComponentModel.DataAnnotations;

namespace iTechShop.Models
{
    public class Contacto
    {
        [Key]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Mensaje { get; set; }
    }
}
