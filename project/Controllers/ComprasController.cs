using Microsoft.AspNetCore.Mvc;
using Proj_Interdisciplinar.DAO;
using Proj_Interdisciplinar.Models;
using System;

namespace Proj_Interdisciplinar.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Compras()
        {
            return View("Compras");
        }
        public IActionResult CompraRealizada()
        {
            return View("CompraRealizada");
        }
        public IActionResult BuscaProdutoAjax(int id)
        {
            try
            {
                ComprasDAO dao = new ComprasDAO();
                ProdutoViewModel produto = dao.Consulta(id);
                if (produto == null)
                {
                    return View("Compras");
                }
                else
                {
                    return Json(new
                    {
                        nome = produto.Nome,
                        info_nutricional = produto.Info_nutricional,
                        alergia = produto.Alergia,
                        preco = produto.Preco,
                        unidade = produto.Unidade,
                        categoria = produto.Categoria
                    });
                }
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
}
