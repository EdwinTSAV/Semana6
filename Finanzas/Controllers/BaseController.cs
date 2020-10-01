using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finanzas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ContextoFinanzas context;
        public BaseController(ContextoFinanzas context)
        {
            this.context = context;
        }

        protected User LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = context.Users.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
