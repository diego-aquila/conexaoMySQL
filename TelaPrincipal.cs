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
            userLogado.Text = SessionUser.userLogado.Nome;
        }
    }
}
