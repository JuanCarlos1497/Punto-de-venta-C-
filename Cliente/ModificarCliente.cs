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
    public partial class ModificarCliente : Form
    {
        public ModificarCliente()
        {
            InitializeComponent();
        }

        private void ModificarCliente_Load(object sender, EventArgs e)
        {
            textBoxNRC.Text = BuscarCliente.CodigoCliente;

            string query = string.Format("Select * From cliente WHERE id_cliente = '{0}'", textBoxNRC.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);

            textBoxNombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            textBoxCelular.Text = ds.Tables[0].Rows[0]["contacto_celular"].ToString();
            textBoxCorreo.Text = ds.Tables[0].Rows[0]["contacto_correo"].ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {   //UPDATE `candysistem`.`cliente` SET `nombre` = 'Edaurdi', `contacto_celular` = '331211', `contacto_correo` = 'lalo_lalo.com' WHERE (`id_cliente` = '1');
                string query = string.Format("UPDATE `candysistem`.`cliente` SET `nombre` = '{0}', `contacto_celular` = '{1}', `contacto_correo` = '{2}' WHERE (`id_cliente` = '{3}');", textBoxNombre.Text, textBoxCelular.Text, textBoxCorreo.Text, textBoxNRC.Text);


                DBConnetc.Ejecutar(query);
                MessageBox.Show("Se ha guardado correctamente!..");
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error: " + error.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
