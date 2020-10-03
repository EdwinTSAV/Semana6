using System;
using System.Collections.Generic;
using System.Linq;
using Finanzas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly ContextoFinanzas _context;
        public TransaccionController(ContextoFinanzas context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index(int id)
        {
            var transacciones = _context.Transacciones.Where(o => o.CuentaId == id).ToList();
            ViewBag.Cuenta = _context.Cuentas.First(o => o.Id == id);  // tener en cuenta al editar bbcita brrr
            return View(transacciones);
        }

        [HttpGet]
        public ActionResult Crear(int id)
        {
            ViewBag.Tipos = new List<string> { "Gasto", "Ingreso" };
            ViewBag.CuentaId = id;
            return View("Crear");
        }

        [HttpPost]
        public ActionResult Crear(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Transacciones.Add(transaccion);
                _context.SaveChanges();
                var cuenta = _context.Cuentas.Where(o => o.Id == transaccion.CuentaId).FirstOrDefault();
                SumaResta(transaccion, cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = transaccion.CuentaId });
            }
            else
            {
                ViewBag.Tipos = new List<string> { "Gasto", "Ingreso" };
                ViewBag.CuentaId = transaccion.CuentaId;
                return View("Crear", transaccion);
            }
        }
        [HttpGet]
        public ActionResult Editar(int id, int cuentaId)
        {
            ViewBag.Tipos = new List<string> { "Gasto", "Ingreso" };
            var transaccion = _context.Transacciones.Where(o => o.Id == id).FirstOrDefault();
            ViewBag.CuentaId = cuentaId;

            ViewBag.montOld = transaccion.Amount;
            ViewBag.tipoOld = transaccion.Tipo;

            return View("Editar", transaccion);
        }
        [HttpPost]
        public ActionResult Editar(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Transacciones.Update(transaccion);
                _context.SaveChanges();
                var cuenta = _context.Cuentas.Where(o => o.Id == transaccion.CuentaId).FirstOrDefault();
                SumaRestaUpdate(transaccion, cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = transaccion.CuentaId });
            }
            else
            {
                ViewBag.montOld = transaccion.Amount;
                ViewBag.tipoOld = transaccion.Tipo;
                ViewBag.Tipos = new List<string> { "Gasto", "Ingreso" };
                ViewBag.CuentaId = transaccion.CuentaId;
                return View("Editar", transaccion);
            }
        }
        [HttpPost]
        private void SumaResta(Transaccion transaccion, Cuenta cuenta)
        {

            if (transaccion.Tipo == "Gasto")
            {
                cuenta.Amount -= transaccion.Amount;
            }
            if (transaccion.Tipo == "Ingreso")
            {
                cuenta.Amount += transaccion.Amount;
            }
        }
        private void SumaRestaUpdate(Transaccion transaccion, Cuenta cuenta)
        {

            if (transaccion.TipoAnt == "Ingreso" && transaccion.Tipo == "Gasto")
            {
                decimal x;
                x = cuenta.Amount - transaccion.AmountAnt;
                cuenta.Amount = x - transaccion.Amount;
            }
            if (transaccion.TipoAnt == "Gasto" && transaccion.Tipo == "Ingreso")
            {
                decimal x;
                x = cuenta.Amount + transaccion.AmountAnt;
                cuenta.Amount = x + transaccion.Amount;
            }

            if (transaccion.TipoAnt == "Gasto" && transaccion.Tipo == "Gasto")
            {
                decimal x;
                x = cuenta.Amount + transaccion.AmountAnt;
                cuenta.Amount = x - transaccion.Amount;
            }
            if (transaccion.TipoAnt == "Ingreso" && transaccion.Tipo == "Ingreso")
            {
                decimal x;
                x = cuenta.Amount - transaccion.AmountAnt;
                cuenta.Amount = x + transaccion.Amount;
            }
        }

    }
}
