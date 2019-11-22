using Hotel.BLL;
using Hotel.Entidades;
using Hotel.UI.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.UI.Consultas
{
    public partial class cCliente : Form
    {
        private List<Cliente> Lista;
        public cCliente()
        {
            InitializeComponent();
        }

        private void Consultarbutton_Click(object sender, EventArgs e)
        {

            var listado = new List<Cliente>();
            RepositorioBase<Cliente> r = new RepositorioBase<Cliente>();

            if (RangocheckBox.Checked == true)
            {
                if (CriteriotextBox.Text.Trim().Length > 0)
                {
                    switch (FiltrocomboBox.Text)
                    {
                        case "Todo":
                            listado = r.GetList(p => true);
                            break;

                        case "ID":
                            int parse;
                            if (!int.TryParse(CriteriotextBox.Text, out parse))
                            {
                                MessageBox.Show("Solo numeros.");
                            }
                            else
                            {
                                int id = Convert.ToInt32(CriteriotextBox.Text);
                                listado = r.GetList(p => p.ClienteId == id);
                            }
                            break;

                        case "Nombres":
                            listado = r.GetList(p => p.Nombres.Contains(CriteriotextBox.Text));
                            break;

                        case "Apellidos":
                            listado = r.GetList(p => p.Apellidos.Contains(CriteriotextBox.Text));
                            break;
                        case "Telefono":
                            listado = r.GetList(p => p.Celular.Contains(CriteriotextBox.Text));
                            break;
                        case "Direccion":
                            listado = r.GetList(p => p.Direccion.Contains(CriteriotextBox.Text));
                            break;
                    }
               //     listado = listado.Where(c => c.Fecha.Date >= DesdedateTimePicker.Value.Date && c.Fecha.Date <= HastadateTimePicker.Value.Date).ToList();
                }
                else
                {
                    if (FiltrocomboBox.Text == string.Empty)
                    {
                        MessageBox.Show("El filtro no puede estar vacio.");
                    }
                    else
                       if ((string)FiltrocomboBox.Text != "Todo")
                    {
                        if (CriteriotextBox.Text == string.Empty)
                        {
                            MessageBox.Show("Al seleccionar uno de ID, Nombres, Cedula, Telefono o Direccion necesita escribir algo en el criterio.");
                        }
                    }
                    listado = r.GetList(p => true);
                  //  listado = listado.Where(c => c.Fecha.Date >= DesdedateTimePicker.Value.Date && c.Fecha.Date <= HastadateTimePicker.Value.Date).ToList();
                }
                Lista = listado;
                ConsultadataGridView.DataSource = listado;
            }
            else
            {
                if (CriteriotextBox.Text.Trim().Length > 0)
                {
                    switch (FiltrocomboBox.Text)
                    {
                        case "Todo":
                            listado = r.GetList(p => true);
                            break;

                        case "ID":
                            int parse;
                            if (!int.TryParse(CriteriotextBox.Text, out parse))
                            {
                                MessageBox.Show("Solo numeros.");
                            }
                            else
                            {
                                int id = Convert.ToInt32(CriteriotextBox.Text);
                                listado = r.GetList(p => p.ClienteId == id);
                            }
                            break;

                        case "Nombres":
                            listado = r.GetList(p => p.Nombres.Contains(CriteriotextBox.Text));
                            break;

                        case "Apellido":
                            listado = r.GetList(p => p.Apellidos.Contains(CriteriotextBox.Text));
                            break;
                        case "Telefono":
                            listado = r.GetList(p => p.Celular.Contains(CriteriotextBox.Text));
                            break;
                        case "Direccion":
                            listado = r.GetList(p => p.Direccion.Contains(CriteriotextBox.Text));
                            break;
                    }
                }
                else
                {
                    if (FiltrocomboBox.Text == string.Empty)
                    {
                        MessageBox.Show("El filtro no puede estar vacio.");
                    }
                    else
                       if ((string)FiltrocomboBox.Text != "Todo")
                    {
                        if (CriteriotextBox.Text == string.Empty)
                        {
                            MessageBox.Show("Al seleccionar uno de ID, Nombres, Cedula, Telefono o Direccion necesita escribir algo en el criterio.");
                        }
                    }
                    else
                    {
                        listado = r.GetList(p => true);
                    }
                }
                Lista = listado;
                ConsultadataGridView.DataSource = listado;
            }
        }

        private void Imprimirbutton_Click(object sender, EventArgs e)
        {
            if (ConsultadataGridView.RowCount == 0)
            {
                MessageBox.Show("No se puede imprimir");
                return;
            }
            else
            {
                ClientesReport r = new ClientesReport(Lista);
                r.ShowDialog();
            }
        }
    }
}
