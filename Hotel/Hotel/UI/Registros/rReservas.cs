using Hotel.BLL;
using Hotel.DAL;
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

namespace Hotel.UI
{
    public partial class FReservas : Form
    {

        public List<ReservasDetalle> Detalle { get; set; }
        public FReservas()
        {

            InitializeComponent();
            Detalle = new List<ReservasDetalle>();
            Cliente();
            Habitacion();

        }

        private void Cliente()
        {
            RepositorioBase<Cliente> db = new RepositorioBase<Cliente>();
            var listado = new List<Cliente>();
            listado = db.GetList(p => true);
            ClientecomboBox.DataSource = listado;
            ClientecomboBox.DisplayMember = "Nombres";
            ClientecomboBox.ValueMember = "ClienteId";

        }
               private decimal fechaAdia(DateTime Finicio, DateTime Fsalida,decimal d)
        {
            decimal dia;
         
            TimeSpan T =Fsalida - Finicio;
            dia = (decimal)T.Days;
            
            return d *dia;
        }
        private void Habitacion()
        {
            RepositorioBase<Habitaciones> db = new RepositorioBase<Habitaciones>();
            var listado = new List<Habitaciones>();
            listado = db.GetList(p => true);
            NumerocomboBox.DataSource = listado;
            NumerocomboBox.DisplayMember = "numero";

            NumerocomboBox.ValueMember = "habitacionId";

        }
        private void CargarGrid()
        {//List<ReservasDetalle>lista
         //     DetalledataGridView.Rows.Clear();
         // foreach (var item in lista)
         //  {
         //     DetalledataGridView.Rows.Add(item.HabitacionId, item.Numero, item.Tipo, item.Descripcion, item.Precio);
         // }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = this.Detalle;

        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            ClientecomboBox.Text = null;
            DescripciontextBox.Text = string.Empty;
            TipotextBox.Text = string.Empty;
            PreciotextBox.Text = string.Empty;
            NumerocomboBox.Text = null;
            FechaReservadateTimePicker.Value = DateTime.Now;
            FechaLlegadateTimePicker.Value = DateTime.Now;
            FechaSalidadateTimePicker.Value = DateTime.Now;

            MontotextBox.Text = string.Empty; ;
            this.Detalle = new List<ReservasDetalle>();
            CargarGrid();
            MyErrorProvider.Clear();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Reservas> db = new RepositorioBase<Reservas>();
            Reservas re = db.Buscar((int)IdnumericUpDown.Value);
            return (re != null);
        }

        private bool Validar()
        {
            bool paso = true;
            Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
            MyErrorProvider.Clear();
            if (string.IsNullOrWhiteSpace(ClientecomboBox.Text))
            {
                MyErrorProvider.SetError(ClientecomboBox, "No puede ser vacio.");
                paso = false;
            }

            if (FechaSalidadateTimePicker.Value <= FechaReservadateTimePicker.Value)
            {
                MyErrorProvider.SetError(FechaSalidadateTimePicker,"No puede ser igual A Fecha de salida");
                paso = false;
            }

            if (Detalle.Count == 0)
            {
                MessageBox.Show("El grid esta vacio.", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                paso = false;
            }
            return paso;
        }
       
        private void LlenaCampo(Reservas v)
        {
            IdnumericUpDown.Value = v.ReservaId;
            ClientecomboBox.Text = Convert.ToString(v.ClienteId);
            FechaLlegadateTimePicker.Value = v.FechaLlegada;
            FechaSalidadateTimePicker.Value = v.FechaSalida;
            FechaReservadateTimePicker.Value = v.FechaReserva;
            MontotextBox.Text = Convert.ToString(v.MontroReserva);
            this.Detalle = v.Detalle;
            CargarGrid();
        }
        private Reservas LlenaClase()
        {
            Reservas r = new Reservas();
            r.Detalle = this.Detalle;
            r.ReservaId = Convert.ToInt32(IdnumericUpDown.Value);
            r.FechaReserva = FechaReservadateTimePicker.Value;
            r.MontroReserva = Convert.ToDecimal(MontotextBox.Text);
            r.FechaLlegada = FechaLlegadateTimePicker.Value;
            r.FechaSalida = FechaSalidadateTimePicker.Value;
            Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
          
            //   r.UsuarioId = id;

            r.Detalle = this.Detalle;
            CargarGrid();
            return r;

        }
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }


        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void NumerocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal r;
             Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
            if (p != null)
            {
                TipotextBox.Text = p.Tipo;
                DescripciontextBox.Text = p.Descripcion;
                PreciotextBox.Text = Convert.ToString(fechaAdia(FechaLlegadateTimePicker.Value, FechaSalidadateTimePicker.Value, p.Valor));
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {

            Reservas v = new Reservas();
            bool paso = false;


            if (!Validar())
                return;

            v = LlenaClase();

            if (IdnumericUpDown.Value == 0)
            {
                paso = ReservasBLL.Guardar(v);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede guardar.", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = ReservasBLL.Modificar(v);
            }

            if (paso)
                MessageBox.Show("Guardado", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Limpiar();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            if (ReservasBLL.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                MyErrorProvider.SetError(IdnumericUpDown, "No existe.");
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {

            if (NumerocomboBox.SelectedValue != null)
            {
                int d = (int)NumerocomboBox.SelectedValue;

                foreach (var item in Detalle)
                {
                    if (d == item.HabitacionId)
                    {
                        MyErrorProvider.SetError(Agregarbutton, "La habitacion ya esta agregada");
                        return;
                    }
                }



                Contexto db = new Contexto();
                if (dataGridView1.DataSource != null)
                {
                    this.Detalle = (List<ReservasDetalle>)dataGridView1.DataSource;
                }
                /*  this.Detalle.Add(new ReservasDetalle(

                      reservaId :0,
                      habitacionId :NumerocomboBox.SelectedIndex,
                      numero:NumerocomboBox.Text,
                      tipo:TipotextBox.Text,
                      descripcion:DescripciontextBox.Text,
                      precio:Convert.ToDecimal(PreciotextBox.Text)
                      )
                      );*/

                Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
                if (NumerocomboBox.SelectedValue != null)
                {
                    this.Detalle.Add(new ReservasDetalle()
                    {
                        HabitacionId = (int)NumerocomboBox.SelectedValue,
                        Numero = NumerocomboBox.Text,
                        Descripcion = DescripciontextBox.Text,
                        Tipo = TipotextBox.Text,
                        Precio = Convert.ToDecimal(PreciotextBox.Text),

                    });

                }
                NumerocomboBox.Text = null;
                PreciotextBox.Text = null;
                TipotextBox.Text = null;

                CargarGrid();
                CalcularTotal();
            }
        }
        public void CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Detalle)
            {
                total += item.Precio;
            }
            MontotextBox.Text = total.ToString();
        }

        public void precio(
            decimal d)
        {
     
           
        }
        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null)
            {
                Detalle.RemoveAt(dataGridView1.CurrentRow.Index);
                CargarGrid();
                CalcularTotal();
            }
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Reservas> Repositorio = new RepositorioBase<Reservas>();
            Reservas v = new Reservas();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);
            Limpiar();

            v = Repositorio.Buscar(id);
            if (v != null)
                LlenaCampo(v);
            else
                MessageBox.Show("No encontrado.");
        }

        private void Checkinbutton_Click(object sender, EventArgs e)
        {
            Reservas r = new Reservas();
            bool paso; if (!ExisteEnLaBaseDeDatos())
                paso = false;
            paso =ReservasBLL.checkins(r);
            if (paso)
                MessageBox.Show("Habitacion Reservas", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se reservo esta ocupada", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
          

        }


        private void Checkoutbutton_Click(object sender, EventArgs e)
        {
            Reservas r = new Reservas();
            bool paso;
            paso = false;
            
            paso = ReservasBLL.checkout(r);
            if (paso)
                MessageBox.Show("Habitacion Reservas", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se reservo esta ocupada", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
