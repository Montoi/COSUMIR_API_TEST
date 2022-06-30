using COSUMIR_API_TEST.Models;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Text;


using Newtonsoft.Json;

using COSUMIR_API_TEST.Servicios;

namespace COSUMIR_API_TEST.Controllers
{
    public class HomeController : Controller
    {
        private IServicio_API _servicioApi;
        
        public   HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Book> lista =  await _servicioApi.Lista();
            return View(lista);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> Book(int id) {

            Book modelo_producto = new Book();

            ViewBag.Accion = "Nuevo Libro";

            if (id != 0) {

                ViewBag.Accion = "Editar Libro";
                modelo_producto = await _servicioApi.Obtener(id);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Book ob_book) {

            bool respuesta;

            if (ob_book.Id == 0)
            {
                respuesta = await _servicioApi.Guardar(ob_book);
            }
            else {
                respuesta = await _servicioApi.Editar(ob_book);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id) {

            var respuesta = await _servicioApi.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}