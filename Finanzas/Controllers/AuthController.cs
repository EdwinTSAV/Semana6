using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Finanzas.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ContextoFinanzas context;
        private readonly IConfiguration configuration;
        public AuthController(ContextoFinanzas context, IConfiguration configuration) : base(context)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [Authorize]
        public string LoggedUserView()
        {
            return "El usuario Logueado es:" + LoggedUser().Id;
        }
        [HttpGet]
        public string Index(string input)
        {
            return CreateHash(input);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //validar si el usuario existe, y si el password es correcto
            var user = context.Users
                .Where(o => o.Username == username && o.Password == CreateHash(password))
                .FirstOrDefault();

            if (user != null)
            {
                // Autenticaremos
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(claimsPrincipal);//guarda cookie

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Login", "Usuario o contraseña incorrectos.");
                return View();
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(User user, string password, string passwordConf)
        {
            if (password != passwordConf)
                ModelState.AddModelError("PasswordConf", "Las contraseñas no coinciden");

            if (ModelState.IsValid)
            {
                user.Password = CreateHash(password);
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Login");
            }else
                return View("Registrar", user);
        }
    }
}
