using Microsoft.AspNetCore.Mvc;
using iTechShop.Datos;
using iTechShop.Models;
using iTechShop.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using iTechShop.Utilidades;

namespace iTechShop.Controllers
{
    public class ListadoController : Controller
    {
        private readonly ILogger<ListadoController> _logger;
        private readonly ApplicationDbContext _db;


        public ListadoController(ILogger<ListadoController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            ListadoVM listadoVM = new ListadoVM()
            {
                Productos= _db.Producto.Include(c=>c.Categoria).Include(t=>t.TipoAplicacion),
                Categorias = _db.Categoria
            };

            return View(listadoVM);
        }


        public IActionResult Detalle(int Id)
        {

            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null
                && HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }


            DetalleVM detalleVM = new DetalleVM()
            {
                Producto = _db.Producto.Include(c => c.Categoria).Include(t => t.TipoAplicacion)
                                       .Where(p => p.Id == Id).FirstOrDefault(),
                ExisteEnCarro = false
            };
            

            foreach ( var item in carroComprasLista)
            {
                if(item.ProductoId == Id)
                {
                    detalleVM.ExisteEnCarro = true;
                }
            }
            return View(detalleVM);

        }

        [HttpPost, ActionName("Detalle")]
        public IActionResult DetallePost(int Id)

        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if(HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras)!= null
                && HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count()>0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            carroComprasLista.Add(new CarroCompra { ProductoId = Id });
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));
                
           }

      
        public IActionResult RemoverDeCarro(int Id)

        {
            List<CarroCompra> carroComprasLista = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null
                && HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroComprasLista = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            var productoARemover = carroComprasLista.SingleOrDefault(x => x.ProductoId == Id);
            if(productoARemover != null)
            {
                carroComprasLista.Remove(productoARemover);
            }
            HttpContext.Session.Set(WC.SessionCarroCompras, carroComprasLista);

            return RedirectToAction(nameof(Index));

        }

    }
}
