using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intento1
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            RegistroOperaciones registro = new RegistroOperaciones();
            this.Hide();
            registro.ShowDialog();

            this.Show();
        }

        private void btnReversion_Click(object sender, EventArgs e)
        {
            ReversionAbonos reversion = new ReversionAbonos();
            this.Hide();
            reversion.ShowDialog();

            this.Show();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            ConsultaOperaciones consulta = new ConsultaOperaciones();
            this.Hide();
            consulta.ShowDialog();

            this.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
