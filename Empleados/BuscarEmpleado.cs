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
    public partial class BuscarEmpleado : Form
    {
        public BuscarEmpleado()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModificarEmpleado modificarEmpleado = new ModificarEmpleado();
            modificarEmpleado.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Empleados.RegistrarEmpleado registrarEmpleado = new Empleados.RegistrarEmpleado();
            registrarEmpleado.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string Codigo = "";
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Codigo = textBoxNRC.Text.ToString();

            string query = string.Format("Select * From empleado WHERE id_empleado = '{0}'", textBoxNRC.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);

            textBoxNombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            textBoxCelular.Text = ds.Tables[0].Rows[0]["celular"].ToString();
            textBoxCorreo.Text = ds.Tables[0].Rows[0]["correo"].ToString();
            textBoxPuesto.Text = ds.Tables[0].Rows[0]["puesto"].ToString();
            textBoxSueldo.Text = ds.Tables[0].Rows[0]["sueldo"].ToString();

            textBoxHoraEntrada.Text = ds.Tables[0].Rows[0]["hora_entrada"].ToString();
            textBoxHoraSalida.Text = ds.Tables[0].Rows[0]["hora_salida"].ToString();
            textBoxDiasTrabajo.Text = ds.Tables[0].Rows[0]["numero_dias_trabaja"].ToString();
        }
        
        private void BuscarEmpleado_Load(object sender, EventArgs e)
        {
            MySqlConnection connection;
            connection = new MySqlConnection("server=localhost; Uid=root; Password=nino; Database=candysistem; port=3306");
            connection.Open();
            textBoxNRC.Items.Clear();
            string query = "SELECT id_empleado from empleado";
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
    }
}
