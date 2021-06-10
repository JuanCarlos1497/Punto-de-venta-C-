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
    public partial class RegistraCompra : Form
    {
        public static int cont_fila = 0;
        public static double subtotal = 0;
        public static double total = 0;
        public static double IVA = 0.16;

        public RegistraCompra()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ConsultarCompra consultarCompra = new ConsultarCompra();
            consultarCompra.Show();
        }

        private void textBoxNombreProveedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegistraCompra_Load(object sender, EventArgs e)
        {
            
            int aux;
            string query = string.Format("SELECT MAX(id_compra) FROM compras;");
            DataSet ds = DBConnetc.Ejecutar(query);
            textBoxFolio.Text = ds.Tables[0].Rows[0]["MAX(id_compra)"].ToString();
            if (textBoxFolio.Text == "")
                aux = 1;
            else
                aux = Int32.Parse(textBoxFolio.Text) + 1;
            textBoxFolio.Text = aux.ToString();

            var fecha = DateTime.Now.ToString("yyyy-MM-dd");
            textBoxFecha.Text = fecha.ToString();
            //_________COMBO_BOX´S___________________________________________

            MySqlConnection connection;
            ///-----------Mdificar
            connection = new MySqlConnection("server=localhost; Uid=root; Password=nino; Database=candysistem; port=3306");
            connection.Open();

            textBoxCodigoEmplaeado.Items.Clear();
            textBoxIdProveedor.Items.Clear();
            cmbProducto.Items.Clear();

            query = "SELECT id_empleado from empleado";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxCodigoEmplaeado.Refresh();
                textBoxCodigoEmplaeado.Items.Add(dataReader.GetValue(0).ToString());
            }
            connection.Close();

            connection.Open();
            query = "SELECT id_proveedor from proveedor";
            cmd = new MySqlCommand(query, connection);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                textBoxIdProveedor.Refresh();
                textBoxIdProveedor.Items.Add(dataReader.GetValue(0).ToString());
            }
            connection.Close();

            connection.Open();
            query = "SELECT nombre from producto";
            cmd = new MySqlCommand(query, connection);
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                cmbProducto.Refresh();
                cmbProducto.Items.Add(dataReader.GetValue(0).ToString());
            }

            textBoxCodigoEmplaeado.Items.Insert(0, "---Seleccione un item----");
            cmbProducto.Items.Insert(0, "---Seleccione un item----");
            textBoxIdProveedor.Items.Insert(0, "---Seleccione un item----");
            textBoxCodigoEmplaeado.SelectedIndex = 0;
            cmbProducto.SelectedIndex = 0;
            textBoxIdProveedor.SelectedIndex = 0;

            connection.Close();
        }

        private void textBoxCodifoEmplaeado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = string.Format("Select nombre From empleado WHERE id_empleado = '{0}'", textBoxCodigoEmplaeado.Text);
            if (textBoxCodigoEmplaeado.Text != "---Seleccione un item----")
            {
                DataSet ds = DBConnetc.Ejecutar(query);
                textBoxNombreEmpleado.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            }
        }

        private void textBoxIdProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = string.Format("Select nombre From proveedor WHERE id_proveedor = '{0}'", textBoxIdProveedor.Text);
            if (textBoxIdProveedor.Text != "---Seleccione un item----")
            {
                DataSet ds = DBConnetc.Ejecutar(query);
                textBoxNombreProveedor.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Codigo, Cantidad, disponible = 0;
            string Descripcion, aux;
            double Precio, Importe;

            bool existe = false;
            int num_fila = 0;


            string query = string.Format("SELECT stock_max ,disponibilidad FROM producto where nombre = '{0}';", cmbProducto.Text);//SelectedIndex
            DataSet ds = DBConnetc.Ejecutar(query);

            int exiten = Convert.ToInt32(ds.Tables[0].Rows[0]["stock_max"]);
            disponible = Convert.ToInt32(ds.Tables[0].Rows[0]["disponibilidad"]);

            if (exiten < Convert.ToInt32(txtCantidad.Text))
            {
                MessageBox.Show("Stock Maximo alcanzado.");
            }
            else if (disponible == 0)
            {
                MessageBox.Show("Este producto no esta disponible.");
            }
            else
            {
                // Agregar al apretar registrar
                query = string.Format("update producto p set p.existencia = p.existencia + " + Convert.ToInt32(txtCantidad.Text) + " where p.nombre = '{0}'", cmbProducto.Text);
                DBConnetc.Ejecutar(query);


                query = string.Format(" SELECT id_producto, precio FROM producto where nombre = '{0}'", cmbProducto.Text);
                ds = DBConnetc.Ejecutar(query);

                aux = ds.Tables[0].Rows[0]["id_producto"].ToString();
                Codigo = Int32.Parse(aux);
                //Descripcion = ds.Tables[0].Rows[0]["descripcion"].ToString();
                Precio = Convert.ToDouble(ds.Tables[0].Rows[0]["precio"]);


                Cantidad = Int32.Parse(txtCantidad.Text);
                Importe = Precio * Cantidad;

                if (cont_fila == 0)
                {
                    dataGridDetalles.Rows.Add(Codigo, Precio, Cantidad);// ,Importe
                    cont_fila++;
                }
                else
                {
                    foreach (DataGridViewRow fila in dataGridDetalles.Rows)
                    {

                        int pos = Convert.ToInt32(fila.Cells[0].Value);
                        if (pos == Codigo)
                        {
                            existe = true;
                            num_fila = fila.Index;
                        }
                    }
                    if (existe == true)
                    {
                        dataGridDetalles.Rows[num_fila].Cells[2].Value = Cantidad + Convert.ToInt32(dataGridDetalles.Rows[num_fila].Cells[2].Value.ToString());
                        //Importe = Convert.ToDouble(dataGridDetalles.Rows[num_fila].Cells[2].Value) * Convert.ToDouble(dataGridDetalles.Rows[num_fila].Cells[3].Value);
                       // dataGridDetalles.Rows[num_fila].Cells[4].Value = Importe;
                    }
                    else
                    {
                        dataGridDetalles.Rows.Add(Codigo, Precio, Cantidad);
                        cont_fila++;
                    }
                }
                subtotal = 0;
                foreach (DataGridViewRow fila in dataGridDetalles.Rows)
                {
                    subtotal += Convert.ToDouble(fila.Cells[2].Value);
                }
                //lblSubTotal.Text = "Sub total: $" + subtotal.ToString();
                total = subtotal; //+ (subtotal * IVA);
                lblTotal.Text = "Total: $" + total.ToString();

            }
            //validarStock();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //INSERT INTO `candysistem`.`compras` (`id_compra`, `id_empleado`, `id_proveedor`, `fecha_peticion`, `fecha_entrega`, `observaciones`, `estatus`) VALUES ('2', '1', '1', '2020-12-11', '2020-12-11', 'Todo mal', '2');

            string query = string.Format("INSERT INTO `candysistem`.`compras` (`id_empleado`, `id_proveedor`, `fecha_peticion`, `fecha_entrega`, `observaciones`, `estatus`)" +
                "VALUES('" + textBoxCodigoEmplaeado.Text + "','" + textBoxIdProveedor.Text + "','" + textBoxFecha.Text + "','" + textBoxFechaEntrega.Text + "','" + textBoxDescrip.Text + "','" + "0" + "');");
            DBConnetc.Ejecutar(query);
            MessageBox.Show("Se ha guardado correctamente!..");           

            foreach (DataGridViewRow fila in dataGridDetalles.Rows)
            {
                //INSERT INTO `candysistem`.`detalle_compra` (`id_producto`, `id_compra`, `cantidad`, `precio`, `importe`) VALUES ('1', '2', '5', '2', '15');

                query = string.Format("INSERT INTO `candysistem`.`detalle_compra` (`id_producto`, `id_compra`, `cantidad`, `precio`) " +
                "VALUES('" + fila.Cells[0].Value.ToString() + "', '" + textBoxFolio.Text + "', '" + fila.Cells[2].Value.ToString() + "', '" + fila.Cells[1].Value.ToString() + "');");
                DBConnetc.Ejecutar(query);

            }

            this.Close();
        }
    }
}
