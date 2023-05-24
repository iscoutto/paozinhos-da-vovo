using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Proj_Interdisciplinar.DAO;
using Proj_Interdisciplinar.Models;

namespace Proj_Interdisciplinar.DAO
{
    public class ComprasDAO
    {

        private ProdutoViewModel MontaProduto(DataRow registro)
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            produto.Nome = registro["nome"].ToString();
            produto.Info_nutricional = registro["info_nutricional"].ToString();
            produto.Alergia = registro["alergia"].ToString();
            produto.Preco = registro["preco"].ToString();
            produto.Unidade = registro["unidade"].ToString();
            produto.Categoria = registro["id_cat"].ToString();

            if (registro["imagem"] != DBNull.Value)
                produto.ImagemEmByte = registro["imagem"] as byte[];
            return produto;
        }

        public ProdutoViewModel Consulta(int id)
        {
            string sql = "select * from Produtos where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaProduto(tabela.Rows[0]);
        }

        public ProdutoViewModel ConsultaId(int id)
        {
            string sql = "select * from Usuarios where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaProduto(tabela.Rows[0]);
        }

        /*public void Inserir(ProdutoViewModel produto)
        {
            string sql = "INSERT INTO Usuarios(nome, cpf, email, senha, tipoUsuario, username) " +
                        "VALUES (@nome, @cpf, @email, @senha, @tipoUsuario, @username)";
            HelperDAO.ExecutaSQL(sql, CriarParametros(produto));
        }
        public void Alterar(UserViewModel usuario)
        {
            string sql =
            "UPDATE Usuarios SET cpf=@cpf, nome=@nome, email=@email, senha=@senha, tipoUsuario=@tipoUsuario, username=@username " +
            "where cpf=@cpf";
            HelperDAO.ExecutaSQL(sql, CriarParametros(usuario));
        }

        public void Excluir(int id)
        {
            string sql = "delete Usuarios where id = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        
        
        private SqlParameter[] CriarParametros(ProdutoViewModel produto)
        {
            SqlParameter[] sqlParameters = new SqlParameter[6];


            sqlParameters[0] = new SqlParameter("nome", produto.Nome);
            sqlParameters[1] = new SqlParameter("cpf", produto.Cpf);
            sqlParameters[2] = new SqlParameter("email", produto.Email);
            sqlParameters[3] = new SqlParameter("senha", produto.Senha);
            sqlParameters[4] = new SqlParameter("tipoUsuario", produto.TipoUsuario);
            if (produto.Username != null)
                sqlParameters[5] = new SqlParameter("username", produto.Username);
            else
            {
                sqlParameters[5] = new SqlParameter("username", produto.Cpf);
            }

            return sqlParameters;
        }*/
    }
}

