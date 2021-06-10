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
    public partial class ModificarEmpleado : Form
    {
        public ModificarEmpleado()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxCelular_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModificarEmpleado_Load(object sender, EventArgs e)
        {
            textBoxNRC.Text = BuscarEmpleado.Codigo;
            string query = string.Format("Select * From empleado WHERE id_empleado = '{0}'", textBoxNRC.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);

            textBoxNombre.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            textBoxCelular.Text = ds.Tables[0].Rows[0]["celular"].ToString();
            textBoxCorreo.Text = ds.Tables[0].Rows[0]["correo"].ToString();

            comboBoxPuesto.Text = ds.Tables[0].Rows[0]["puesto"].ToString();
            textBoxSueldo.Text = ds.Tables[0].Rows[0]["sueldo"].ToString();

            textBoxHoraEntrada.Text = ds.Tables[0].Rows[0]["hora_entrada"].ToString();
            textBoxHoraSalida.Text = ds.Tables[0].Rows[0]["hora_salida"].ToString();
            textBoxUsuario.Text = ds.Tables[0].Rows[0]["usuario"].ToString();
            textBoxPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
            textBoxDepartamento.Text = ds.Tables[0].Rows[0]["id_deprtamento"].ToString();

            textBoxDiasTrabajo.Text = ds.Tables[0].Rows[0]["numero_dias_trabaja"].ToString();

            cargarDepartamento();
        }

        void cargarDepartamento()
        {
            //_________COMBO_BOX´S___________________________________________

            MySqlConnection connection;
            connection = new MySqlConnection("server=localhost; Uid=root; Password=nino; Database=candysistem; port=3306");
            connection.Open();

            textBoxDepartamento.Items.Clear();

            string query = "SELECT nombre_departamento from departamento";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxDepartamento.Refresh();
                textBoxDepartamento.Items.Add(dataReader.GetValue(0).ToString());
            }

            textBoxDepartamento.Items.Insert(0, "---Seleccione un item----");
            textBoxDepartamento.Items.Insert(0, "---Seleccione un item----");

            textBoxDepartamento.SelectedIndex = 0;
            textBoxDepartamento.SelectedIndex = 0;

            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int departamento = 0;
            departamento = textBoxDepartamento.SelectedIndex;
            try
            {
                //INSERTAR POR Proceso almacenado
                string query = string.Format("UPDATE `candysistem`.`empleado` SET `nombre` = '{0}', `puesto` = '{1}', `hora_entrada` = '{2}', `hora_salida` = '{3}', `celular` = '{4}', `correo` = '{5}', `password` = '{6}', `sueldo` = '{7}', `numero_dias_trabaja` = '{8}', `id_deprtamento` = '{9}', `usuario` = '{10}' WHERE(`id_empleado` = '{11}');",
                    textBoxNombre.Text, comboBoxPuesto.Text, textBoxHoraEntrada.Text, textBoxHoraSalida.Text, textBoxCelular.Text,
                    textBoxCorreo.Text, textBoxPassword.Text, textBoxSueldo.Text, textBoxDiasTrabajo.Text, departamento, textBoxUsuario.Text ,textBoxNRC.Text);

                DBConnetc.Ejecutar(query);

                //if (comboBoxPuesto.Text == "ventas" || comboBoxPuesto.Text == "encargado_ventas")
                //{
                 //   query = string.Format("UPDATE `surtisistema`.`empleado` SET `permiso` = '1' WHERE(`id_empleado` = '{0}');", textBoxNRC.Text);
                  //  DBConnetc.Ejecutar(query);
                //}

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
