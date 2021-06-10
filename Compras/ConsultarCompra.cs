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
    public partial class ConsultarCompra : Form
    {
        public ConsultarCompra()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConsultarCompra_Load(object sender, EventArgs e)
        {
            MySqlConnection connection;
            connection = new MySqlConnection("server=localhost; Uid=root; Password=nino; Database=candysistem; port=3306");
            connection.Open();
            textBoxFolio.Items.Clear();
            string query = "SELECT id_compra from compras";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxFolio.Refresh();
                textBoxFolio.Items.Add(dataReader.GetValue(0).ToString());
            }
            connection.Close();
            textBoxFolio.Items.Insert(0, "---Seleccione un item----");
            textBoxFolio.SelectedIndex = 0;
        }

        private void textBoxFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_P, id_E;
            if (textBoxFolio.Text != "---Seleccione un item----")
            {
                string fecha;
                string fecha2;
                int pago;
                string query = string.Format("Select * From compras WHERE id_compra = '{0}'", textBoxFolio.Text.Trim());
                DataSet ds = DBConnetc.Ejecutar(query);

                textBoxCodigoEmpleado.Text = ds.Tables[0].Rows[0]["id_empleado"].ToString();
                textBoxProveedor.Text = ds.Tables[0].Rows[0]["id_proveedor"].ToString();
                textBoxDescrip.Text = ds.Tables[0].Rows[0]["observaciones"].ToString();
                //textBoxEstatus.Text = ds.Tables[0].Rows[0]["estatus"].ToString();

                fecha = ds.Tables[0].Rows[0]["fecha_peticion"].ToString();
                fecha2 = ds.Tables[0].Rows[0]["fecha_entrega"].ToString();
                textBoxFecha.Text = fecha.Substring(0, 10);
                textBoxFecha2.Text = fecha2.Substring(0, 10);

                id_P = Int32.Parse(textBoxProveedor.Text);
                id_E = Int32.Parse(textBoxCodigoEmpleado.Text);

                cargaProveedor(id_P);
                cargaEmpleado(id_E);
                cargarDataGrid();
                mostrarTotal();

            }
        }

        void cargaProveedor(int id)
        {
            string query = string.Format("Select nombre From proveedor WHERE id_proveedor = '{0}'", id);
            DataSet ds = DBConnetc.Ejecutar(query);
            textBoxNombreProveedor.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
        }

        void cargaEmpleado(int id)
        {
            string query = string.Format("Select nombre From empleado WHERE id_empleado = '{0}'", id);
            DataSet ds = DBConnetc.Ejecutar(query);
            textBoxNombreEmpleado.Text = ds.Tables[0].Rows[0]["nombre"].ToString();

        }

        void cargarDataGrid()
        {

            string query = string.Format("select p.id_producto As ID, p.nombre As Nombre ,p.precio as Precio, dc.cantidad as Cantidad" +
                " from producto p left join detalle_compra dc on p.id_producto = dc.id_producto WHERE dc.id_compra = {0};", textBoxFolio.Text.Trim());
            DataSet ds = DBConnetc.Ejecutar(query);

            dataGridDetalles.DataSource = ds.Tables[0];


        }

        void mostrarTotal()
        {
            double stotal = 0, total = 0, iva = 0.16;

            foreach (DataGridViewRow fila in dataGridDetalles.Rows)
            {
                stotal += Convert.ToDouble(fila.Cells[3].Value);
            }

            total = stotal;// + (stotal * iva);
            lblTotal.Text = "Total: $" + total.ToString();
        }

    }
}
