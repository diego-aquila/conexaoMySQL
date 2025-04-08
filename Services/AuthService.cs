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
                string query = "SELECT id, nome, email, senha_hash, data_cadastro, ativo FROM usuarios WHERE email = @emailDigitado";
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
                            return new Usuario
                            {
                                Id = Convert.ToInt32(respostaBanco["id"]),
                                Nome = respostaBanco["nome"].ToString(),
                                Email = respostaBanco["email"].ToString(),
                                SenhaHash = respostaBanco["senha_hash"].ToString(),
                                DataCadastro = Convert.ToDateTime(respostaBanco["data_cadastro"]),
                                Ativo = Convert.ToBoolean(respostaBanco["ativo"])
                            };
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