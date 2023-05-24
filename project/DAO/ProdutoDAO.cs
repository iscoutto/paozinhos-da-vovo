using Microsoft.AspNetCore.Mvc;
using Proj_Interdisciplinar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Proj_Interdisciplinar.DAO
{
    public class ProdutoDAO
    {
        private SqlParameter[] CriarParametros(ProdutoViewModel produto)
        {
            object imgByte = produto.ImagemEmByte;

            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] parametros = new SqlParameter[7];

            parametros[0] = new SqlParameter("nome", produto.Nome);
            parametros[1] = new SqlParameter("info_nutricional", produto.Info_nutricional);
            parametros[2] = new SqlParameter("alergia", produto.Alergia);
            parametros[3] = new SqlParameter("preco", produto.Preco);
            parametros[4] = new SqlParameter("id_cat", produto.Categoria);
            parametros[5] = new SqlParameter("unidade", produto.Unidade);
            parametros[6] = new SqlParameter("imagem", imgByte);

            return parametros;
        }private SqlParameter[] CriarParametrosAlterar(ProdutoViewModel produto)
        {
            object imgByte = produto.ImagemEmByte;

            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] parametros = new SqlParameter[8];

            parametros[0] = new SqlParameter("id", produto.Id);
            parametros[1] = new SqlParameter("nome", produto.Nome);
            parametros[2] = new SqlParameter("info_nutricional", produto.Info_nutricional);
            parametros[3] = new SqlParameter("alergia", produto.Alergia);
            parametros[4] = new SqlParameter("preco", produto.Preco);
            parametros[5] = new SqlParameter("id_cat", produto.Categoria);
            parametros[6] = new SqlParameter("unidade", produto.Unidade);
            parametros[7] = new SqlParameter("imagem", imgByte);

            return parametros;
        }

        private ProdutoViewModel MontaProduto(DataRow produto)
        {
            ProdutoViewModel produtoView = new ProdutoViewModel();
            produtoView.Id = Convert.ToInt16(produto["id"].ToString());
            produtoView.Nome = produto["nome"].ToString();
            produtoView.Info_nutricional = produto["info_nutricional"].ToString();
            produtoView.Alergia= produto["alergia"].ToString();
            produtoView.Preco = produto["preco"].ToString();
            produtoView.Unidade = produto["unidade"].ToString();
            produtoView.Categoria = produto["categoria"].ToString();

            if (produto["imagem"] != DBNull.Value)
                produtoView.ImagemEmByte = produto["imagem"] as byte[];

            return produtoView;
        }

        public void Inserir(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spIncluiProduto", CriarParametros(produto));

        }

        public void Alterar(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spAlteraProduto", CriarParametrosAlterar(produto));

        }
        public ProdutoViewModel ConsultaId(int id)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaIdProduto", parametros);
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontaProduto(tabela.Rows[0]);
            }
        }
        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spExcluiProduto", p);
        }

        public ProdutoViewModel Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaProduto", p);
            if (tabela.Rows.Count == 0) return null;
            else
                return MontaProduto(tabela.Rows[0]);
        }

        /*
        public List<ProdutoViewModel> Listagem()
        {
            List<ProdutoViewModel> lista = new List<ProdutoViewModel>();
            string sql = "select * from Produtos order by nome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaProduto(registro));
            return lista;
        }
        */

        public List<ProdutoViewModel> Listar()
        {
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListaProduto", null);
            List<ProdutoViewModel> retorno = new List<ProdutoViewModel>();

            foreach (DataRow registro in tabela.Rows)
                retorno.Add(MontaProduto(registro));
            return retorno;
        }

        public List<ProdutoViewModel> ConsultaAvancadaProdutos(string nome,
            int categoria,
            string unidade,
            string alergia)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("nome", nome),
                new SqlParameter("id_cat", categoria),
                new SqlParameter("unidade", unidade),
                new SqlParameter("alergia", alergia)
            };

            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaProdutos", parametros);
            var lista = new List<ProdutoViewModel>();
            foreach(DataRow registro in tabela.Rows)
                lista.Add(MontaProduto(registro));
            return lista;
        }

        // Dashboard

        public List<ItensVendidosViewModel> PesquisarMaisVendidos()
        {
            List<ItensVendidosViewModel> retorno = new List<ItensVendidosViewModel>();

            var tabela = HelperDAO.ExecutaProcSelect("spItensVendidos", null);

            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                foreach (DataRow registro in tabela.Rows)
                    retorno.Add(MontaModelMaisVendidos(registro));
            }

            return retorno;
        }

        private ItensVendidosViewModel MontaModelMaisVendidos(DataRow registro)
        {
            ItensVendidosViewModel item = new ItensVendidosViewModel();
            item.Nome = registro["Nome"].ToString();
            item.QtdVendida = Convert.ToInt32(registro["Quantidade"]);

            return item;
        }
    }
}
