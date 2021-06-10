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
    public partial class RegistrarCliente : Form
    {
        public RegistrarCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //**INSERTAR POR Query
                //_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
                string query = string.Format("INSERT INTO `candysistem`.`cliente` (`nombre`, `contacto_celular`, `contacto_correo`) " +
                    " VALUES('" + textBoxNombre.Text + "' , '" +  textBoxCelular.Text + "' , '" + textBoxCorreo.Text + "');");

                DBConnetc.Ejecutar(query);
                MessageBox.Show("Se ha guardado correctamente!..");
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error: " + error.Message);
            }
        }

        private void RegistrarCliente_Load(object sender, EventArgs e)
        {
            int aux = 0;

            string query = string.Format("SELECT MAX(id_cliente) FROM cliente;");
            DataSet ds = DBConnetc.Ejecutar(query);
            textBoxID.Text = ds.Tables[0].Rows[0]["MAX(id_cliente)"].ToString();
            if (textBoxID.Text == "")
            {
                textBoxID.Text = "1";
            }
            else
            {
                aux = Int32.Parse(textBoxID.Text) + 1;
                textBoxID.Text = aux.ToString();
            }
        }
    }
}
