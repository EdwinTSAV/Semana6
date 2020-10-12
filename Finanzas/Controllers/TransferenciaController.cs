using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Finanzas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Finanzas.Controllers
{
    public class TransferenciaController : BaseController
    {
        private readonly ContextoFinanzas contexto;
        private readonly IHostEnvironment _hostEnv;
        public TransferenciaController(ContextoFinanzas contexto, IHostEnvironment hostEnv) : base(contexto)
        {
            this.contexto = contexto;
            _hostEnv = hostEnv;
        }
        [HttpGet]
        public ActionResult Transferir()
        {
            ViewBag.Cuentas = contexto.Cuentas
                .Where(o => o.UserId == LoggedUser().Id)
                .ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Transferir(int CuentaID1,int CuentaID2,decimal amount)
        {
            var transaccion1 = new Transaccion
            {
                CuentaId = CuentaID1,
                FechaHora = DateTime.Now,
                Tipo = "Gasto",
                Motivo = "Transferencia",
                Amount = amount * -1
            };
            var transaccion2 = new Transaccion
            {
                CuentaId = CuentaID2,
                FechaHora = DateTime.Now,
                Tipo = "Ingreso",
                Motivo = "Transferencia",
                Amount = amount
            };
            contexto.Transacciones.Add(transaccion1);
            contexto.Transacciones.Add(transaccion2);

            contexto.SaveChanges();

            ModificaMontoCuenta(CuentaID1);
            ModificaMontoCuenta(CuentaID2);

            return RedirectToAction("Index", "Crud");
        }
        private void ModificaMontoCuenta(int cuentaId)
        {
            var cuenta = contexto.Cuentas
                .Include(o => o.Transaccions)
                .FirstOrDefault(o => o.Id == cuentaId);

            var total = cuenta.Transaccions.Sum(o => o.Amount);
            cuenta.Amount = total;
            contexto.SaveChanges();
        }
    }
}
