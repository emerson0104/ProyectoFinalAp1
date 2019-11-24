using Hotel.UI;
using Hotel.UI.Consultas;
using Hotel.UI.Registros;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class FMenu : Form
    {
        public int id { get; set; }
        public FMenu(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void ReservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FReservas r = new FReservas(id);
            r.Show();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rUsuarios u = new rUsuarios();
            u.Show();
        }

        private void HabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rHabitaciones h = new rHabitaciones(id);
            h.Show();
        }

        private void ClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCliente rCliente = new rCliente(id);
            rCliente.Show();

        }

        private void ClienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cCliente cC = new cCliente();
            cC.Show();
        }

        private void HabitacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cHabitaciones ch = new cHabitaciones();
            ch.Show();
        }
    }
}
