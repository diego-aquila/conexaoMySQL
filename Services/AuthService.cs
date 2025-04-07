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
                string query = "SELECT id, nome, email, senha_hash, data_cadastro, ativo FROM usuarios WHERE email = @email";
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@email", email)
                };

                using (var reader = _databaseService.ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        // Verifica a senha
                        string storedHash = reader["senha_hash"].ToString();
                        string inputHash = Criptografia.HashPassword(password);

                        if (storedHash == inputHash)
                        {
                            return new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Email = reader["email"].ToString(),
                                SenhaHash = storedHash,
                                DataCadastro = Convert.ToDateTime(reader["data_cadastro"]),
                                Ativo = Convert.ToBoolean(reader["ativo"])
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