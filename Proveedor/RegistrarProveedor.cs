using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CandySystem
{
    public partial class RegistrarProveedor : Form
    {
        public RegistrarProveedor()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrarProveedor_Load(object sender, EventArgs e)
        {
            int aux;
            string query = string.Format("SELECT MAX(id_proveedor) FROM proveedor;");
            DataSet ds = DBConnetc.Ejecutar(query);
            //textBoxID.Text = ds.Tables[0].Rows[0]["MAX(id_proveedor)"].ToString();
            //aux = Int32.Parse(textBoxID.Text) + 1;
            //textBoxID.Text = aux.ToString();

            textBoxID.Text = ds.Tables[0].Rows[0]["MAX(id_proveedor)"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            int disponible = 0;
        
            if (comboBoxDisponible.SelectedIndex == 1)
                disponible = 1;

            try
            {
                //INSERT INTO `candysistem`.`proveedor` (`id_proveedor`, `nombre`, `direccion`, `estatus`, `celular`, `correo`) VALUES ('2', 'Nino', 'algo bien lejos', '1', '121212', 'nino.com');

                string query = string.Format("INSERT INTO `candysistem`.`proveedor` (`id_proveedor`, `nombre`, `direccion`, `estatus`, `celular`, `correo`)" +
                                    " VALUES('" + textBoxID.Text + "','" + textBoxNombre.Text + "','" + textBoxDireccion.Text + "','" + disponible.ToString() + "','" + textBoxCelular.Text + "" +
                                    "','" + textBoxCorreo.Text +"');");
                
                DBConnetc.Ejecutar(query);             
                MessageBox.Show("Se ha guardado correctamente!..");
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error: " + error.Message);
            }
        }
    }
}
