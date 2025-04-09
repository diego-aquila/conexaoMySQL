using ConexaoMySQL.Model;
using ConexaoMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexaoMySQL.Controllers
{
    class UserController
    {
        UsuarioRepositorio usuarioRepositorio;
        public UserController(UsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public List<Usuario> getAllUsers() {
            List<Usuario> usuarios = new List<Usuario>();

            usuarios = usuarioRepositorio.getAllUser();

            if (usuarios == null)
            {
                MessageBox.Show("Nenhum usuário encontrado");
                return null;
            }


            return usuarios;
        
        }

    }
}
