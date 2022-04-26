using iTechShop.Datos;
using iTechShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace iTechShop.Controllers
{
    public class ContactoController : Controller

    {
        private readonly ApplicationDbContext _db;

        public ContactoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Enviar()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enviar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _db.Contacto.Add(contacto);
                _db.SaveChanges();
                return RedirectToAction(nameof(Enviar));
            }
            return View();
        }


    }
}
