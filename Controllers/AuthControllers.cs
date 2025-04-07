// Controllers/AuthController.cs
using ConexaoMySQL.Models;
using ConexaoMySQL.Services;
using System;
using System.Windows.Forms;

namespace ConexaoMySQL.Controllers
{
    public class AuthController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        public Usuario Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email e senha são obrigatórios");
            }

            return _authService.Authenticate(email, password);
        }

        public bool Register(Usuario usuario, string password)
        {
           
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Senha é obrigatória");
            }

            return _authService.Register(usuario, password);
        }
    }
}