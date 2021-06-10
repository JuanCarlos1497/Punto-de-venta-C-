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
    public partial class BuscarProveedor : Form
    {
        public BuscarProveedor()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrarProveedor registrarProveedor = new RegistrarProveedor();
            registrarProveedor.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModificarProveedor modificarProveedor = new ModificarProveedor();
            modificarProveedor.Show();
        }
        
        private void BuscarProveedor_Load(object sender, EventArgs e)
        {
            MySqlConnection connection;
            connection = new MySqlConnection("server=localhost; Uid=root; Password=nino; Database=candysistem; port=3306");
            connection.Open();
            textBoxID.Items.Clear();
            string query = "SELECT id_proveedor from proveedor";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxID.Refresh();
                textBoxID.Items.Add(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            textBoxID.Items.Insert(0, "---Seleccione un item----");
            textBoxID.SelectedIndex = 0;
        }
        public static string Codigo = "";
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Codigo = textBoxID.Text.ToString();

            

            string query = string.Format("Select * From proveedor WHERE id_proveedor= '{0}'", textBoxID.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);

            textBoxNombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            textBoxDireccion.Text = ds.Tables[0].Rows[0]["direccion"].ToString();

            textBoxCelular.Text = ds.Tables[0].Rows[0]["celular"].ToString();
            textBoxCorreo.Text = ds.Tables[0].Rows[0]["correo"].ToString();
            textBoxEstatus.Text = ds.Tables[0].Rows[0]["estatus"].ToString();
        }
    }
}
