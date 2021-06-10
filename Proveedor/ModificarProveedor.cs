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
    public partial class ModificarProveedor : Form
    {
        public ModificarProveedor()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            textBoxID.Text = BuscarProveedor.Codigo;
            string query = string.Format("Select * From proveedor WHERE id_proveedor = '{0}'", textBoxID.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);

            textBoxNombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            textBoxCelular.Text = ds.Tables[0].Rows[0]["celular"].ToString();
            textBoxCorreo.Text = ds.Tables[0].Rows[0]["correo"].ToString();
            comboBoxDisponible.Text = ds.Tables[0].Rows[0]["estatus"].ToString();


            textBoxDireccion.Text = ds.Tables[0].Rows[0]["direccion"].ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int disponible = 0;

            // departamento = comboBoxCategoria.SelectedIndex + 1;
            if (comboBoxDisponible.SelectedIndex == 0)
                disponible = 1;

            try
            {
                //UPDATE `candysistem`.`proveedor` SET `nombre` = '{0}', `direccion` = '{1}', `estatus` = '{2}', `celular` = '{3}', `correo` = '{4}' WHERE (`id_proveedor` = '{5}');

                string query = string.Format("UPDATE `candysistem`.`proveedor` SET `nombre` = '{0}', `direccion` = '{1}', `estatus` = '{2}', `celular` = '{3}', `correo` = '{4}' WHERE (`id_proveedor` = '{5}');",
                    textBoxNombre.Text, textBoxDireccion.Text, disponible.ToString(), textBoxCelular.Text, textBoxCorreo.Text, textBoxID.Text);

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
