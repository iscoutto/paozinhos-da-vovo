using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using Proj_Interdisciplinar.DAO;
using Proj_Interdisciplinar.Models;

namespace Proj_Interdisciplinar.DAO
{
    public class UserDAO
    {
        private SqlParameter[] CriarParametros(UserViewModel usuario)
        {
            SqlParameter[] sqlParameters = new SqlParameter[6];

            sqlParameters[0] = new SqlParameter("nome", usuario.Nome);
            sqlParameters[1] = new SqlParameter("cpf", usuario.Cpf);
            sqlParameters[2] = new SqlParameter("email", usuario.Email);
            sqlParameters[3] = new SqlParameter("senha", usuario.Senha);
            sqlParameters[4] = new SqlParameter("tipoUsuario", usuario.TipoUsuario);
            if (usuario.Username != null)
                sqlParameters[5] = new SqlParameter("username", usuario.Username);
            else
            {
                sqlParameters[5] = new SqlParameter("username", usuario.Cpf);
            }

            return sqlParameters;
        }

        public List<UserViewModel> ValidaLogin(string username, string senha)
        {
            List<UserViewModel> lista = new List<UserViewModel>();
            string sql = "select * from Usuarios where username like '" + username + "' and senha like '" + senha + "'";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUser(registro));
            return lista;
        }

        private UserViewModel MontaUser(DataRow registro)
        {
            UserViewModel usuario = new UserViewModel();
            usuario.Id = Convert.ToInt32(registro["id"]);
            usuario.Nome = registro["nome"].ToString();
            usuario.Email = registro["email"].ToString();
            usuario.Cpf = registro["cpf"].ToString();
            usuario.TipoUsuario = registro["tipoUsuario"].ToString();
            usuario.Senha = registro["senha"].ToString();
            usuario.Username = registro["username"].ToString();
            return usuario;
        }
        /*
        public UserViewModel Consulta(string username, string senha)
        {
            string sql = "select * from Usuarios where username like '" + username + "' and senha like '" + senha + "'";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUser(tabela.Rows[0]);
        }
        */

        public UserViewModel Consulta(string username, string senha)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@senha", senha)
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaUsuario", parametros);
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontaUser(tabela.Rows[0]);
            }
        }


        /*
        public UserViewModel ConsultaId(int id)
        {
            string sql = "select * from Usuarios where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUser(tabela.Rows[0]);
        }
        */

        public UserViewModel ConsultaId(int id)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaId", parametros);
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontaUser(tabela.Rows[0]);
            }
        }


        /*
        public void Inserir(UserViewModel usuario)
        {
            string sql = "INSERT INTO Usuarios(nome, cpf, email, senha, tipoUsuario, username) " +
                        "VALUES (@nome, @cpf, @email, @senha, @tipoUsuario, @username)";
            HelperDAO.ExecutaSQL(sql, CriarParametros(usuario));
        }
        */

        public void Inserir(UserViewModel usuario)
        {
            HelperDAO.ExecutaProc("spIncluiUsuario", CriarParametros(usuario));
        }

        /*
        public void Alterar(UserViewModel usuario)
        {
            string sql =
            "UPDATE Usuarios SET cpf=@cpf, nome=@nome, email=@email, senha=@senha, tipoUsuario=@tipoUsuario, username=@username " +
            "where cpf=@cpf";
            HelperDAO.ExecutaSQL(sql, CriarParametros(usuario));
        }
        */

        public void Alterar(UserViewModel usuario)
        {
            HelperDAO.ExecutaProc("spAlteraUsuario", CriarParametros(usuario));
        }

        /*
        public void Excluir(int id)
        {
            string sql = "delete Usuarios where id = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        */

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spExcluiUsuario", p);
        }

        /*
        public List<UserViewModel> Listagem(int tipo)
        {
            if (tipo == 0)
            {
                List<UserViewModel> lista = new List<UserViewModel>();
                string sql = "select * from Usuarios WHERE tipoUsuario like 'Cliente' order by nome";
                DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
                foreach (DataRow registro in tabela.Rows)
                    lista.Add(MontaUser(registro));
                return lista;
            }
            else
            {
                List<UserViewModel> lista = new List<UserViewModel>();
                string sql = "select * from Usuarios WHERE tipoUsuario like 'Administrador' order by nome";
                DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
                foreach (DataRow registro in tabela.Rows)
                    lista.Add(MontaUser(registro));
                return lista;
            }
        }
        */

        public List<UserViewModel> Listagem(int tipo)
        {
            if (tipo == 0)
            {
                DataTable tabela = HelperDAO.ExecutaProcSelect("spListaClientes", null);
                List<UserViewModel> retorno = new List<UserViewModel>();

                foreach (DataRow registro in tabela.Rows)
                    retorno.Add(MontaUser(registro));
                return retorno;
            }
            else if (tipo == 1)
            {
                DataTable tabela = HelperDAO.ExecutaProcSelect("spListaAdministradores", null);
                List<UserViewModel> retorno = new List<UserViewModel>();

                foreach (DataRow registro in tabela.Rows)
                    retorno.Add(MontaUser(registro));
                return retorno;
            }
            else
            {
                DataTable tabela = HelperDAO.ExecutaProcSelect("spListaTodosUsuarios", null);
                List<UserViewModel> retorno = new List<UserViewModel>();

                foreach (DataRow registro in tabela.Rows)
                    retorno.Add(MontaUser(registro));
                return retorno;
            }
        }
    }
}

