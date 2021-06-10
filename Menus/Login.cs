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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
       // public static int permisos = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("Select usuario, password From empleado WHERE usuario = '{0}' AND password = '{1}'", textBoxUsuario.Text.Trim(), textBoxPassword.Text.Trim());
                DataSet ds = DBConnetc.Ejecutar(query);

                string cuenta = ds.Tables[0].Rows[0]["usuario"].ToString().Trim();
                string contra = ds.Tables[0].Rows[0]["password"].ToString().Trim();
                //permisos = Convert.ToInt32(ds.Tables[0].Rows[0]["permiso"]);

                Menu mainMenu = new Menu(textBoxUsuario.Text.ToString());
                this.Hide();
                mainMenu.Show();                
            }
            catch (Exception error)
            {
                MessageBox.Show("Usuario o Contraseña incorrecto");
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUsuario_Enter(object sender, EventArgs e)
        {
            if (textBoxUsuario.Text == "USUARIO")
            {
                textBoxUsuario.Text = "";
                textBoxUsuario.ForeColor = Color.LightPink;
            }
        }

        private void textBoxUsuario_Leave(object sender, EventArgs e)
        {
            if (textBoxUsuario.Text == "")
            {
                textBoxUsuario.Text = "USUARIO";
                textBoxUsuario.ForeColor = Color.LightPink;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "CONTRASEÑA")
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.LightPink;
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "CONTRASEÑA";
                textBoxPassword.ForeColor = Color.LightPink;
                textBoxPassword.UseSystemPasswordChar = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
