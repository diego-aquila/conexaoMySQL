// Services/AuthService.cs
using ConexaoMySQL.Models;
using ConexaoMySQL.Services;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

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
                        string inputHash = HashPassword(password);

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
                    new MySqlParameter("@senha_hash", HashPassword(password)),
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

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}