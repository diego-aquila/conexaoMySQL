using ConexaoMySQL.Models;
using ConexaoMySQL.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoMySQL.Model
{
    public class UsuarioRepositorio
    {
        DatabaseService _databaseService;

        public UsuarioRepositorio(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Usuario> getAllUser() { 
        
        List<Usuario> usuarios = new List<Usuario>();

            try {

                string query = "SELECT * FROM usuarios";
                MySqlDataReader resultadoBanco = _databaseService.ExecuteQuery(query);

                while (resultadoBanco.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(resultadoBanco["id"].ToString());
                    usuario.Nome = resultadoBanco["nome"].ToString();
                    usuario.Email = resultadoBanco["email"].ToString();
                    usuario.SenhaHash = resultadoBanco["senha_hash"].ToString();
                    usuario.DataCadastro = Convert.ToDateTime(resultadoBanco["data_cadastro"].ToString());
                    usuario.Ativo = Convert.ToBoolean(resultadoBanco["ativo"].ToString());
                    
                    usuarios.Add(usuario);
                }
                _databaseService.CloseConnection();

                return usuarios;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar usuários: " + ex.Message);
            }
         

        }
        

        public bool Register(Usuario usuario, string password)
        {
            try
            {
                string query = @"INSERT INTO usuarios 
                                (nome, email, senha_hash, data_cadastro, ativo) 
                                VALUES 
                                (@nome, @email, @senha_hash, @data_cadastro, @ativo)";

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@nome", usuario.Nome),
                    new MySqlParameter("@email", usuario.Email),
                    new MySqlParameter("@senha_hash", Criptografia.HashPassword(password)),
                    new MySqlParameter("@data_cadastro", DateTime.Now),
                    new MySqlParameter("@ativo", true)
                };

                int affectedRows = _databaseService.ExecuteNonQuery(query, parameters);
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao registrar usuário: " + ex.Message);
            }
        }
    }
}
