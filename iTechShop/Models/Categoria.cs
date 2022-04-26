using System.ComponentModel.DataAnnotations;

namespace iTechShop.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre de Categoria es obligatorio")]
        public string NombreCategoria { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="El orden debe ser mayor a cero")]
        public int MostrarOrden { get; set; }

    }
}
