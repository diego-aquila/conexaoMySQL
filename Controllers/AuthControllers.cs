// Controllers/AuthController.cs
using ConexaoMySQL.Model;
using ConexaoMySQL.Models;
using ConexaoMySQL.Services;
using System;
using System.Windows.Forms;

namespace ConexaoMySQL.Controllers
{
    public class AuthController
    {
        private readonly AuthService _authService;
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public AuthController(AuthService authService, UsuarioRepositorio usuarioRepositorio)
        {
            _authService = authService;
            _usuarioRepositorio = usuarioRepositorio;
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

            return _usuarioRepositorio.Register(usuario, password);
        }
    }
}