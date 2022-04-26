using iTechShop.Datos;
using iTechShop.Models;
using iTechShop.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace iTechShop.Controllers
{
    public class CarroController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CarroController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            List<CarroCompra> carroCompraList = new List<CarroCompra>();
            if(HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras)!= null && 
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count()>0)
            {
                carroCompraList = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            List<int> prodEnCarro = carroCompraList.Select(i => i.ProductoId).ToList();
            IEnumerable<Producto> prodList = _db.Producto.Where(p => prodEnCarro.Contains(p.Id));

           
            return View(prodList);
        }

        public IActionResult Remover(int Id)
        {

            List<CarroCompra> carroCompraList = new List<CarroCompra>();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraList = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            carroCompraList.Remove(carroCompraList.FirstOrDefault(p => p.ProductoId == Id));
            HttpContext.Session.Set(WC.SessionCarroCompras, carroCompraList);

            return RedirectToAction(nameof(Index));

        }




    }
}
