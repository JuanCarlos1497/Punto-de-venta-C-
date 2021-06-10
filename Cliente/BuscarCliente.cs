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
    public partial class BuscarCliente : Form

    {
        public BuscarCliente()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void BuscarCliente_Load(object sender, EventArgs e)
        {
            MySqlConnection connection;
            connection = new MySqlConnection("server=localhost; Uid=root; Password=nino; Database=candysistem; port=3306");
            connection.Open();
            textBoxNRC.Items.Clear();
            string query = "SELECT id_cliente from cliente";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxNRC.Refresh();
                textBoxNRC.Items.Add(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            textBoxNRC.Items.Insert(0, "---Seleccione un item----");
            textBoxNRC.SelectedIndex = 0;
        }

        public static string CodigoCliente = "";

        private void textBoxNRC_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModificarCliente modificarCliente = new ModificarCliente();
            modificarCliente.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CodigoCliente = textBoxNRC.Text.ToString();

            string query = string.Format("Select * From cliente WHERE id_cliente = '{0}'", textBoxNRC.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);


            textBoxNombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();

            textBoxCelular.Text = ds.Tables[0].Rows[0]["contacto_celular"].ToString();
            textBoxCorreo.Text = ds.Tables[0].Rows[0]["contacto_correo"].ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RegistrarCliente registrarCliente = new RegistrarCliente();
            registrarCliente.Show();
        }
    }
}
