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
        public TelaPrincipal()
        {
            InitializeComponent();
            Usuario user = SessionUser.userLogado;
        }

        private void TelaPrincipal_Load(object sender, EventArgs e)
        {
            showLabelUser();
            userLogado.Text = SessionUser.userLogado.Nome;
        }

        private void showLabelUser() {

            if (SessionUser.userLogado.Email == "diego@diegoaquila.com.br")
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
