using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Entidades
{
   public class ReservasDetalle
    {[Key]
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public ReservasDetalle()
        {
            Id = 0;
            ReservaId = 0;
            HabitacionId = 0;
            Numero = string.Empty;
            Tipo = string.Empty;
            Descripcion = string.Empty;
            Precio = 0;
        }

        public ReservasDetalle(int id, int reservaId, int habitacionId, string numero, string tipo, string descripcion, decimal precio)
        {
            Id = id;
            ReservaId = reservaId;
            HabitacionId = habitacionId;
            Numero = numero;
            Tipo = tipo;
            Descripcion = descripcion;
            Precio = precio;
        }
    }
}
