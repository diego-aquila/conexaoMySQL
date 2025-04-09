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
           
            userLogado.Text = $"Usuário: {SessionUser.userLogado.Nome} | Regra: {SessionUser.userLogado.Regra.nomeRegra}";
            List<Usuario> usuarios = _userController.getAllUsers();

            ShowDataGrid(usuarios);

            if (usuarios == null)
            {
                MessageBox.Show("Nenhum usuário encontrado");
                dataGridUsuarios.Visible = false;
                return;
            }

            


            comboBox1.DisplayMember = "Nome";  // Propriedade que será exibida
            comboBox1.ValueMember = "Id";      // Propriedade que será o valor associado
            comboBox1.DataSource = usuarios;







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

        private void ShowDataGrid(List<Usuario> usuarios) {
            dataGridUsuarios.Columns.Add("Id", "ID");
            dataGridUsuarios.Columns.Add("Nome", "Nome");
            dataGridUsuarios.Columns.Add("Email", "E-mail");
            dataGridUsuarios.Columns.Add("Regra", "Regra");

            foreach (Usuario usuario in usuarios)
            {
                dataGridUsuarios.Rows.Add(
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Regra.nomeRegra

                    );
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUser.Text = comboBox1.SelectedValue.ToString();
        }
    }
}
