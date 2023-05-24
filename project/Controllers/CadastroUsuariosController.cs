using Microsoft.AspNetCore.Mvc;
using Proj_Interdisciplinar.Models;
using Proj_Interdisciplinar.DAO;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proj_Interdisciplinar.Controllers
{
    public class CadastroUsuariosController : Controller
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
        public IActionResult CadastroUsuarios()
        {
            return View();
        }

        public IActionResult UsuariosCadastrados(int tipo)
        {
            ViewBag.TipoUser = tipo;
            UserDAO dao = new UserDAO();
            List<UserViewModel> lista = dao.Listagem(tipo);
            return View(lista);
        }

        public IActionResult CreateUser(int tipo)
        {
            try
            {
                if (tipo == 0)
                {
                    UserViewModel usuario = new UserViewModel();
                    ViewBag.Operacao = "I";
                    ViewBag.TipoUser = "Cliente";
                    UserDAO dao = new UserDAO();

                    return View("CadastroUsuarios", usuario);
                }
                else
                {
                    UserViewModel usuario = new UserViewModel();
                    ViewBag.Operacao = "I";
                    ViewBag.TipoUser = "Administrador";
                    UserDAO dao = new UserDAO();

                    return View("CadastroUsuarios", usuario);
                }
            }

            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public IActionResult Save(string Operacao, UserViewModel usuario, string TipoUsuario)
        {
            try
            {
                ValidaDados(Operacao, usuario);

                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    ViewBag.TipoUser = TipoUsuario;
                    return View("CadastroUsuarios", usuario);
                }
                else
                {
                    UserDAO dao = new UserDAO();
                    if (Operacao == "I")
                        dao.Inserir(usuario);
                    else
                        dao.Alterar(usuario);

                    return RedirectToAction("InicialLogado", "Logado");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Editar(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                UserDAO dao = new UserDAO();
                UserViewModel usuario = dao.ConsultaId(id);
                if (usuario == null)
                    return View("UsuariosCadastrados");
                else
                {
                    ViewBag.TipoUser = usuario.TipoUsuario;
                    return View("CadastroUsuarios", usuario);
                }

            }
            catch (Exception erro)
            {
                return View("error",
                    new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                UserDAO dao = new UserDAO();
                dao.Excluir(id);
                return RedirectToAction("InicialLogado","Logado");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void ValidaDados(string operacao, UserViewModel usuario)
        {
            ModelState.Clear(); //apaga os eventuas erros em inglês

            if (string.IsNullOrEmpty(usuario.Cpf))
                ModelState.AddModelError("CPF", "Preencha o CPF.");
            if (string.IsNullOrEmpty(usuario.Nome))
                ModelState.AddModelError("Nome", "Preencha o Nome.");
            if (string.IsNullOrEmpty(usuario.Email))
                ModelState.AddModelError("Email", "Preencha o Email.");
            if (string.IsNullOrEmpty(usuario.Senha))
                ModelState.AddModelError("Senha", "Preencha o Senha.");
            if (string.IsNullOrEmpty(usuario.TipoUsuario))
                ModelState.AddModelError("TipoUsuario", "Preencha o Usuário.");

        }
    }
}
