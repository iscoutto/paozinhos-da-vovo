using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proj_Interdisciplinar.Models;

namespace Proj_Interdisciplinar.Controllers
{
    public class PadraoController<T> : Controller where T : DefaultViewModel
    {
        public IActionResult Index()
        {
            return View();

        }

        protected bool ExigeAutenticacao { get; set; } = true;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ExigeAutenticacao && !HelperControllers.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Index", "Login");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }


    }

}
