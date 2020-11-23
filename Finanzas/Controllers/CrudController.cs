using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Finanzas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Finanzas.Controllers
{
    [Authorize]
    public class CrudController : BaseController
    {
        private readonly ContextoFinanzas _context;
        private readonly IHostEnvironment _hostEnv;
        public CrudController(ContextoFinanzas context, IHostEnvironment hostEnv) : base(context)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //var cuenta = _context.Cuentas;

            var cuentas = _context.Cuentas
                .Where(o => o.UserId == LoggedUser().Id)
                .Include(o => o.Tipo)
                .ToList();

            ViewBag.Types = _context.Types.ToList();
            ViewBag.Currency = new List<string> { "Euro", "Dolar", "Soles" };

            ViewBag.Total = cuentas.Sum(o => o.Amount);
            //var cuentas = _context.Cuentas.Include(o => o.Tipo).ToList();
            // ViewBag.Cuentas = cuentas; // forma A
            return View(cuentas); // forma B 
            // Si no se envia el nombre de la vista, se usara una vista con el mismo nombre del metodo return View("Index",cuentas);

            // de lo contrario se usa asi:
            //ViewBag.Cuentas = _context.Cuentas.ToList();
            //return View("Index");
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            ViewBag.Types = _context.Types.ToList();
            ViewBag.Currency = new List<string> { "Euro", "Dolar", "Soles" };
            return View("Registrar", new Cuenta()); // se manda new cuentapara guardar los datos y en la vista model.atributo para pintar los datos nuevamente 
        }

        /*Se modifica aca lo que vamos a redireccionar por si salga error ;-;*/

        [HttpPost]
        public ActionResult Registrar(Cuenta cuenta, IFormFile image)
        {
            cuenta.UserId = LoggedUser().Id;
            if (ModelState.IsValid)
            {
                // Guardar archivos en el servidor:
                if (image != null && image.Length > 0)
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\Files\" + image.FileName;
                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        image.CopyTo(strem);
                        cuenta.Image = ruta;
                    }
                }
                if(cuenta.TypeId == 3)
                {
                    cuenta.Limite = cuenta.Amount;
                    cuenta.Amount = 0;
                    
                }
                if (cuenta.Amount != 0 && cuenta.TypeId != 3)
                {
                    cuenta.Transaccions = new List<Transaccion>
                {
                    new Transaccion
                    {
                        FechaHora = DateTime.Now,
                        Tipo = "Ingreso",
                        Amount = cuenta.Amount,
                        Motivo = "Monto Inicial"
                    }
                };
                }
                _context.Cuentas.Add(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Response.StatusCode = 400; // mando el estado 400 solo cuando falla
                ViewBag.Types = _context.Types.ToList();
                ViewBag.Currency = new List<string> { "Euro", "Dolar", "Soles" };
                return View("Registrar", cuenta);
            }
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Types = _context.Types.ToList();
            ViewBag.Currency = new List<string> { "Euro", "Dolar", "Soles" };

            var cuenta = _context.Cuentas.Where(o => o.Id == id).FirstOrDefault(); // si no lo encuentra retorna un null
            return View("Editar", cuenta);

            // o tambien se hace asi:
            //ViewBag.Cuentas = _context.Cuentas.Where(o => o.Id == id).FirstOrDefault();
            //return View("Editar");
        }
        [HttpPost]
        public ActionResult Editar(Cuenta cuenta)
        {
            cuenta.UserId = LoggedUser().Id;
            // no se xde cuenta.UserId = LoggedUser().Id;
            if (ModelState.IsValid)
            {
                _context.Cuentas.Update(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Response.StatusCode = 400; // mando el estado 400 solo cuando falla
                ViewBag.Types = _context.Types.ToList();
                ViewBag.Currency = new List<string> { "Euro", "Dolar", "Soles" };
                return View("Editar", cuenta);
            }
        }
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var cuenta = _context.Cuentas.Where(o => o.Id == id).FirstOrDefault();
            _context.Cuentas.Remove(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}



