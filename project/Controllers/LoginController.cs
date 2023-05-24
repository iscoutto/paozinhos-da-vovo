using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Proj_Interdisciplinar.Models;
using Proj_Interdisciplinar.DAO;
using System.Data;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proj_Interdisciplinar.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Login(UserViewModel usuario)
        {
            ViewBag.Erro = "";
            return View(usuario);
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

                    return View("Login");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }


        [HttpPost]
        public IActionResult FazLogin(string usuario, string senha)
        {
            ViewBag.Erro = " ";
            try
            {
                UserDAO userDAO = new UserDAO();
                List<UserViewModel> listaUsuario = userDAO.ValidaLogin(usuario, senha);

                if (listaUsuario != null)
                {
                    HttpContext.Session.SetString("Logado", "true");
                    UserViewModel model = userDAO.Consulta(usuario, senha);

                    if (model != null)
                    {
                        if (model.TipoUsuario == "Administrador")
                            return RedirectToAction("InicialLogado", "Logado", model);
                        else
                            return RedirectToAction("InicialLogadoClient", "Logado", model);
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário ou senha inválidos!";
                        return View("Login", model);
                    }
                }
                else
                {
                    ViewBag.Erro = "Usuário ou senha inválidos!";
                    return RedirectToAction("Login", "Login");

                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }
    }
}
