using ConexaoMySQL.Controllers;
using ConexaoMySQL.Model;
using ConexaoMySQL.Models;
using ConexaoMySQL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexaoMySQL
{
    public partial class TelaPrincipal : Form
    {
        private UserController _userController;

        public TelaPrincipal()
        {
            InitializeComponent();
            Usuario user = SessionUser.userLogado;
            _userController = new UserController(new UsuarioRepositorio(new DatabaseService()));

        }

        private void TelaPrincipal_Load(object sender, EventArgs e)
        {
            showLabelUser();
            userLogado.Text = SessionUser.userLogado.Nome;
            List<Usuario> usuarios = _userController.getAllUsers();

            if (usuarios == null)
            {
                MessageBox.Show("Nenhum usuário encontrado");
                dataGridUsuarios.Visible = false;
                return;
            }

            dataGridUsuarios.DataSource = usuarios;






        }

        private void showLabelUser() {

            if (SessionUser.userLogado.Ativo)
            {
                userLogado.Text = SessionUser.userLogado.Nome;
            }
            else
            {
                userLogado.Visible = false;
                menuStrip1.Visible = false;


            }

        }
    }
}
