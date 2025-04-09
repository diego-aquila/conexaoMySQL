// Services/AuthService.cs
using ConexaoMySQL.Models;
using ConexaoMySQL.Services;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ConexaoMySQL.Services
{
    public class AuthService
    {
        private readonly DatabaseService _databaseService;

        public AuthService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public Usuario Authenticate(string email, string password)
        {
            try
            {
                // Primeiro busca o usuário pelo email
                string query = "SELECT * FROM usuarios JOIN regras ON usuarios.idRegra = regras.idregra WHERE email = @emailDigitado";
                var parameters = new MySqlParameter[] 
                {

                    new MySqlParameter("@emailDigitado", email),

                };

                using (var respostaBanco = _databaseService.ExecuteQuery(query, parameters))
                {
                    if (respostaBanco.Read())
                        
                    {
                        // Verifica a senha
                        string storedHash = respostaBanco["senha_hash"].ToString();
                        string inputHash = Criptografia.HashPassword(password);

                        if (storedHash == inputHash)
                        {
                            Usuario usuario = new Usuario();
                            usuario = Usuario.UserFromDataReader(respostaBanco);

                            return usuario;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante autenticação: " + ex.Message);
            }
        }

        

       
    }
}