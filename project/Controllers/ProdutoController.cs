using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proj_Interdisciplinar.DAO;
using Proj_Interdisciplinar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Proj_Interdisciplinar.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Produtos()
        {
            try
            {
                MontaComboCategoria();
                return View("Produtos");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }
        public IActionResult ProdutosCadastrados()
        {
            ProdutoDAO dao = new ProdutoDAO();
            List<ProdutoViewModel> lista = dao.Listar();
            return View(lista);
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }
        public IActionResult CreateProduct()
        {
            try
            {
                ProdutoViewModel produto = new ProdutoViewModel();
                ViewBag.Operacao = "I";
                ProdutoDAO dao = new ProdutoDAO();
                MontaComboCategoria();
                return View("CadastrarProduto", produto);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult CadastrarProduto()
        {
            MontaComboCategoria();
            ViewBag.Categorias.Insert(0, new SelectListItem("TODAS", "0"));
            return View("CadastrarProduto");
        }

        public IActionResult Save(ProdutoViewModel produto, string Operacao)
        {
            MontaComboCategoria();
            ProdutoDAO produtoDAO = new ProdutoDAO();
            ValidaDados(produto);
            if(ModelState.IsValid == false)
            {
                ViewBag.Operacao = Operacao;
                return View("CadastrarProduto", produto);
            }
            else
            {
                if (Operacao == "I")
                    produtoDAO.Inserir(produto);
                else
                    produtoDAO.Alterar(produto);
                return RedirectToAction("ProdutosCadastrados", "Produto");
            }
        }

        private void ValidaDados(ProdutoViewModel model)
        {
            ModelState.Clear(); //apaga os eventuas erros em inglês

            if (model.Imagem != null && model.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");
            if (model.Imagem == null)
                ModelState.AddModelError("Imagem", "Selecione uma Imagem");
            if (ModelState.IsValid)
            { //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.

                model.ImagemEmByte = ConvertImageToByte(model.Imagem);

            }

        }

        private void MontaComboCategoria()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            CategoriaDAO dao = new CategoriaDAO();
            foreach (var c in dao.Listar())
                lista.Add(
                    new SelectListItem(c.Nome, c.Id.ToString()));

            ViewBag.Categorias = lista;
        }

        public IActionResult ObtemDadosConsultaAvancada(string nome,
            int categoria,
            string unidade,
            string alergia
            )
        {
            try
            {
                ProdutoDAO dao = new ProdutoDAO();
                if (string.IsNullOrEmpty(nome))
                    nome = "%";
                if (string.IsNullOrEmpty(unidade))
                    unidade = "%";
                else if (unidade == "TODAS")
                    unidade = "%";
                if (string.IsNullOrEmpty(alergia))
                    alergia = "%";

                var listaProdutos = dao.ConsultaAvancadaProdutos(nome, categoria, unidade, alergia);

                return PartialView("ProdutoFiltrado", listaProdutos);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
        public IActionResult Editar(int id)
        {
            try
            {
                MontaComboCategoria();
                ViewBag.Operacao = "A";
                ProdutoDAO dao = new ProdutoDAO();
                ProdutoViewModel produto = dao.ConsultaId(id);
                if (produto == null)
                    return View("ProdutosCadastrados");
                else
                {
                    return View("CadastrarProduto", produto);
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
                ProdutoDAO dao = new ProdutoDAO();
                dao.Excluir(id);
                return RedirectToAction("InicialLogado", "Logado");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        // Dashboard
        public virtual IActionResult Dashboards()
        {
            try
            {
                DashboardViewModel model = new DashboardViewModel();
                ProdutoDAO dao = new ProdutoDAO();
                List<ProdutoViewModel> estoque = new List<ProdutoViewModel>();
                List<ItensVendidosViewModel> vendidos = new List<ItensVendidosViewModel>();

                List<ProdutoViewModel> items = dao.Listar().ToList();

                if (items != null)
                {
                    ConverterItensEstoque(items, estoque, model);
                }

                List<ItensVendidosViewModel> vendas = dao.PesquisarMaisVendidos().OrderBy(o => o.QtdVendida).ToList();

                if (vendas != null)
                {
                    ConverterMenosVendidos(vendas, vendidos, model);

                    ConverterMaisVendidos(vendas, vendidos, model);
                }

                return View("Dashboards", model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void ConverterMenosVendidos(List<ItensVendidosViewModel> vendas, List<ItensVendidosViewModel> vendidos, DashboardViewModel model)
        {
            for (int i = 0; i < 3; i++)
            {
                vendidos.Add(vendas[i]);
            }

            model.MenosVendido1 = vendidos[0].Nome;
            model.QtdMenosVendido1 = vendidos[0].QtdVendida;

            model.MenosVendido2 = vendidos[1].Nome;
            model.QtdMenosVendido2 = vendidos[1].QtdVendida;

            model.MenosVendido3 = vendidos[2].Nome;
            model.QtdMenosVendido3 = vendidos[2].QtdVendida;
        }

        private void ConverterMaisVendidos(List<ItensVendidosViewModel> vendas, List<ItensVendidosViewModel> vendidos, DashboardViewModel model)
        {
            vendas = vendas.OrderByDescending(o => o.QtdVendida).ToList();
            vendidos = new List<ItensVendidosViewModel>();

            for (int i = 0; i < 3; i++)
            {
                vendidos.Add(vendas[i]);
            }

            model.MaisVendido1 = vendidos[0].Nome;
            model.QtdMaisVendido1 = vendidos[0].QtdVendida;

            model.MaisVendido2 = vendidos[1].Nome;
            model.QtdMaisVendido2 = vendidos[1].QtdVendida;

            model.MaisVendido3 = vendidos[2].Nome;
            model.QtdMaisVendido3 = vendidos[2].QtdVendida;
        }

        private void ConverterItensEstoque(List<ProdutoViewModel> items, List<ProdutoViewModel> estoque, DashboardViewModel model)
        {
            for (int i = 0; i < 3; i++)
            {
                estoque.Add(items[i]);
            }

            model.Item1 = estoque[0].Nome;
            //model.Qtd1 = estoque[0].Quantidade;

            model.Item2 = estoque[1].Nome;
            //model.Qtd2 = estoque[1].Quantidade;

            model.Item3 = estoque[2].Nome;
            //model.Qtd3 = estoque[2].Quantidade;

        }
    }
}
