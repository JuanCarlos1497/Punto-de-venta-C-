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
    public partial class Consulta : Form
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string query = string.Format("Select * From {0}", comboBox2.Text.Trim());
                DataSet ds = DBConnetc.Ejecutar(query);

                dataGridInfGeneral.DataSource = ds.Tables[0];
            }
            catch
            {
                MessageBox.Show("Seleccione un valor");
            }
           
        }

        private void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
