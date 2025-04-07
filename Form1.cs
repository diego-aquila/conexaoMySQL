using ConexaoMySQL.Controllers;
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
    public partial class Form1: Form
    {
        AuthController authController;
        public Form1()
        {
            InitializeComponent();
            DatabaseService databaseService = new DatabaseService();
            AuthService authService = new AuthService(databaseService);
            authController = new AuthController(authService);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Usuario user =  authController.Login(txtEmail.Text, txtSenha.Text);

            if (user == null)
            {
                MessageBox.Show("Usuário não encontrado");
                return;
            }

            MessageBox.Show($"Usuario: {user.Nome} \nEmail: {user.Email} \nData de Cadastro: {user.DataCadastro.ToString("dd/MM/yyyy")}");
            SessionUser.UserLogado(user);
            TelaPrincipal telaPrincipal = new TelaPrincipal();
            telaPrincipal.ShowDialog();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = "Diego Áquila Almeida Sampaio";
            usuario.Email = "diego@diegoaquila.com.br";
            
           bool result = authController.Register(usuario, txtSenha.Text);

            if (result)
            {
                MessageBox.Show("Usuario inserido");
            }
        }
    }
}
