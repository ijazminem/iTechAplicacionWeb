using System.ComponentModel.DataAnnotations;

namespace iTechShop.Models
{
    public class TipoAplicacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage= "El campo no puede estar vacío")]
        public string Nombre { get; set; }

      

    }
}
