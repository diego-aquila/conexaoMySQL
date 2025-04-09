// Models/Usuario.cs
using ConexaoMySQL.Model.Regra;
using MySql.Data.MySqlClient;
using System;

namespace ConexaoMySQL.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public Regra Regra { get; set; }


        public static Usuario UserFromDataReader(MySqlDataReader reader)
        {

            return new Usuario
            {
                Id = Convert.ToInt32(reader["id"].ToString()),
                Nome = reader["nome"].ToString(),
                Email = reader["email"].ToString(),
                SenhaHash = reader["senha_hash"].ToString(),
                DataCadastro = Convert.ToDateTime(reader["data_cadastro"].ToString()),
                Ativo = Convert.ToBoolean(reader["ativo"].ToString()),
                Regra = new Regra
                {
                    idregra = Convert.ToInt32(reader["idregra"].ToString()),
                    nomeRegra = reader["nomeRegra"].ToString()
                }

            };


        }

    }
}