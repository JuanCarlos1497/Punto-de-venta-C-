using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CandySystem
{
    public partial class Menu : Form
    {
        private string nombreUsuario = "";
        public Menu(string name)
        {
            nombreUsuario = name;
            InitializeComponent();
        }

        private Form activeForm = null;
        public void openForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(childForm);
            panelPrincipal.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void panelPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new RegistrarVenta());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new BuscarEmpleado());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new BuscarProducto());
        }

        private void ptbMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
            {
                panelMenu.Width = 54;
            }
            else
            {
                panelMenu.Width = 151;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new BuscarCliente());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new Consulta());
        }

        private void buttonCompra_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new RegistraCompra());

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (panelMenu.Width == 151)
                panelMenu.Width = 54;
            openForm(new BuscarProveedor());
        }
    }
}
