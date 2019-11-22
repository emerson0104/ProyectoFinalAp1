using Hotel.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.UI.Reportes
{
    public partial class ClientesReport : Form
    {
        private List<Cliente> ListaClientes;
        
        public ClientesReport(List < Cliente > clientes)
        {
            this.ListaClientes = clientes;
            InitializeComponent();
        }

        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {
            ListdoClientes listado = new ListdoClientes();
            listado.SetDataSource(ListaClientes);

            crystalReportViewer1.ReportSource = ListaClientes;
            crystalReportViewer1.Refresh();
        }
    }
}
