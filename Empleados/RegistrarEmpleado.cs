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

namespace CandySystem.Empleados
{
    public partial class RegistrarEmpleado : Form
    {
        public RegistrarEmpleado()
        {
            InitializeComponent();
        }

        private void RegistrarEmpleado_Load(object sender, EventArgs e)
        {
            int aux;
            string query = string.Format("SELECT MAX(id_empleado) FROM empleado;");
            DataSet ds = DBConnetc.Ejecutar(query);
            textBoxID.Text = ds.Tables[0].Rows[0]["MAX(id_empleado)"].ToString();
            aux = Int32.Parse(textBoxID.Text) + 1;
            textBoxID.Text = aux.ToString();
            cargarComboBox();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int departamento = textBoxDepartamento.SelectedIndex;
            try
            {
                //**INSERTAR POR Query
                //_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

                string query = string.Format("INSERT INTO `candysistem`.`empleado` (`nombre`,`puesto`, `hora_entrada`, `hora_salida`, `celular`, `correo`, `password`, `sueldo`, `numero_dias_trabaja`, `id_deprtamento`,`usuario`)" +
                                    " VALUES('" + textBoxNombre.Text + "','" + textBoxPuesto.Text + "','" + textBoxHoraEntrada.Text + "','" + textBoxHoraSalida.Text + "','" + textBoxCelular.Text + "" +
                                    "','" + textBoxCorreo.Text + "','" + textBoxPassword.Text + "','" + textBoxSueldo.Text + "','" + textBoxDiasTrabajo.Text + "','" + departamento + "','" + textBoxUsuario.Text + "');"); 
                //_____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


                DBConnetc.Ejecutar(query);

               // if (textBoxPuesto.Text == "ventas" || textBoxPuesto.Text == "encargado_ventas")
                //{
                 //   query = string.Format("UPDATE `candysistem`.`empleado` SET `permiso` = '1' WHERE(`id_empleado` = '{0}');", textBoxID.Text);
                  //  DBConnetc.Ejecutar(query);
               // }

                MessageBox.Show("Se ha guardado correctamente!..");
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error: " + error.Message);
            }
        }
        void cargarComboBox()
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

    }
}
