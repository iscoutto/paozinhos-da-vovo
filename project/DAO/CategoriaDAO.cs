using Proj_Interdisciplinar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Proj_Interdisciplinar.DAO
{
    public class CategoriaDAO
    {
        private SqlParameter[] CriarParametros(CategoriaViewModel categoria, string Operacao)
        {
            if(Operacao == "I")
            {
                SqlParameter[] parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter("nome", categoria.Nome);
                return parametros;
            }
            else
            {
                SqlParameter[] parametros = new SqlParameter[2];

                parametros[0] = new SqlParameter("id_cat", categoria.Id);
                parametros[1] = new SqlParameter("nome", categoria.Nome);
                return parametros;
            }
        }

        private CategoriaViewModel Montacategoria(DataRow categoria)
        {
            CategoriaViewModel categoriaView = new CategoriaViewModel();
            categoriaView.Id = Convert.ToInt32(categoria["id_cat"]);
            categoriaView.Nome = categoria["nome"].ToString();
         
            return categoriaView;
        }
        private string MontacategoriaNome(DataRow categoria)
        {
            
            var nome = categoria["nome"].ToString();
         
            return nome;
        }
        public void Inserir(CategoriaViewModel categoria,string Operacao)
        {
            HelperDAO.ExecutaProc("spIncluiCategoria", CriarParametros(categoria, Operacao));

        }

        public void Alterar(CategoriaViewModel categoria,string Operacao)
        {
            HelperDAO.ExecutaProc("spAlteraCategoria", CriarParametros(categoria, Operacao));

        }

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id_cat", id)
            };
            HelperDAO.ExecutaProc("spExcluiCategoria", p);
        }

        public CategoriaViewModel Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id_cat", id)
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaCategoria", p);
            if (tabela.Rows.Count == 0) 
                return null;
            else
                return Montacategoria(tabela.Rows[0]);
        }
        public string ConsultaNome(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id_cat", id)
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaCategoriaNome", p);
            if (tabela.Rows.Count == 0) 
                return null;
            else
            {
                var nome = MontacategoriaNome(tabela.Rows[0]);
                return nome;
            }
        }

        /*
        public List<CategoriaViewModel> Listagem()
        {
            List<CategoriaViewModel> lista = new List<CategoriaViewModel>();
            string sql = "select * from categorias order by nome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(Montacategoria(registro));
            return lista;
        }
        */

        public List<CategoriaViewModel> Listar()
        {
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListaCategoria", null);
            List<CategoriaViewModel> retorno = new List<CategoriaViewModel>();

            foreach (DataRow registro in tabela.Rows)
                retorno.Add(Montacategoria(registro));

            return retorno;
        }
    }
}
