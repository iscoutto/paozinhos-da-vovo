using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proj_Interdisciplinar.DAO;
using Proj_Interdisciplinar.Models;
using System;
using System.Collections.Generic;

namespace Proj_Interdisciplinar.Controllers
{
    public class CategoriaController : Controller
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
        public IActionResult CategoriasCadastradas()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            List<CategoriaViewModel> lista = categoriaDAO.Listar();
            return View("CategoriasCadastradas", lista);
        }
        public IActionResult CreateCategoria(int tipo)
        {
            try
            {
                CategoriaViewModel categoria = new CategoriaViewModel();
                ViewBag.Operacao = "I";
                CategoriaDAO dao = new CategoriaDAO();

                return View("CadastrarCategoria", categoria);

            }

            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }
        public IActionResult Delete(int id)
        {
            try
            {
                CategoriaDAO dao = new CategoriaDAO();
                dao.Excluir(id);
                return RedirectToAction("InicialLogado", "Logado");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Save(string Operacao, CategoriaViewModel categoria)
        {
            try
            {
                ValidaDados(categoria);

                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    return View("CadatrarCategoria", categoria);
                }
                else
                {
                    CategoriaDAO dao = new CategoriaDAO();
                    if (Operacao == "I")
                        dao.Inserir(categoria, Operacao);
                    else
                        dao.Alterar(categoria, Operacao);

                    return RedirectToAction("InicialLogado", "Logado");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        private void ValidaDados(CategoriaViewModel categoria)
        {
            ModelState.Clear(); //apaga os eventuas erros em inglês

            if (string.IsNullOrEmpty(categoria.Nome))
                ModelState.AddModelError("Nome", "Preencha o Nome.");

        }
        public IActionResult Editar(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                CategoriaDAO dao = new CategoriaDAO();
                CategoriaViewModel categoria = dao.Consulta(id);
                if (categoria == null)
                    return View("CategoriasCadastradas");
                else
                {
                    return View("CadastrarCategoria", categoria);
                }

            }
            catch (Exception erro)
            {
                return View("error",
                    new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
