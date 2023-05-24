using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proj_Interdisciplinar.DAO;
using Proj_Interdisciplinar.Models;
using System;

namespace Proj_Interdisciplinar.Controllers
{
    public class LogadoController : Controller
    {
        protected bool ExigeAutenticacao { get; set; } = true;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ExigeAutenticacao && !HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Login", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        public IActionResult InicialLogado(UserViewModel model)
        {
            return View("InicialLogado", model);
        }

        public IActionResult InicialLogadoClient(UserViewModel model)
        {
            return View("InicialLogadoClient", model);
        }

        public IActionResult LogOff()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }
    }
}
