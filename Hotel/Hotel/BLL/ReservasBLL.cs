using Hotel.DAL;
using Hotel.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL
{
  public  class ReservasBLL
    {
        public static bool Guardar(Reservas r)
        { 
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                foreach (var item in r.Detalle)
                {
                    var habita = db.Habitaciones.Find(item.HabitacionId);
                    if (item.HabitacionId!=0)
                    {
                        habita.Estado = true;
                    }
                }
                if (db.Reservas.Add(r) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Modificar(Reservas reservas)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Reservas> Reser = new RepositorioBase<Reservas>();
          
            //     RepositorioBase<Productos> prod = new RepositorioBase<Productos>();
            /*
     bool paso = false;
     Contexto db = new Contexto();
     RepositorioBase<Reservas> Reser = new RepositorioBase<Reservas>();
            //     RepositorioBase<Productos> prod = new RepositorioBase<Productos>();
                 try
                 {
                     var Anterior = ReservasBLL.Buscar(reservas.ReservaId);
                     foreach (var item in Anterior.Detalle)
                     {
                         if (!reservas.Detalle.Exists(d => d.Id == item.Id))
                         {
                             db.Entry(item).State = EntityState.Deleted;
                         }
                     }
                     */
            try
            {
                var Anterior = ReservasBLL.Buscar(reservas.ReservaId);
                foreach (var item in Anterior.Detalle)
                {
                   // var habitacion = repositorio.Buscar(item.HabitacionId);
                    if (!reservas.Detalle.Exists(d => d.Id == item.Id))
                    {
                        
                            db.Entry(item).State = EntityState.Deleted;
                        
                        
                    }
                }
                db.Entry(reservas).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            Reservas r =new Reservas();
            bool paso = false;
            Contexto db = new Contexto();
            try
            {


                foreach (var item in r.Detalle)
                {
                    var habita = db.Habitaciones.Find(item.HabitacionId);
                    if (item.HabitacionId != 0)
                    {
                        habita.Estado = true;
                    }
                }
                var eliminar = db.Reservas.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Reservas Buscar(int id)
        {
            
           Reservas reservas = new Reservas();
            Contexto db = new Contexto();
            try
            {
                reservas = db.Reservas.Find(id);


                reservas.Detalle.Count();
                
              
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return reservas;
        }

        public static List<Reservas> GetList(Expression<Func<Reservas, bool>> reser)
        {
            List<Reservas> lista = new List<Reservas>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Reservas.Where(reser).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return lista;
        }
        public static bool checkins(Reservas r )
        {
           
            Contexto db = new Contexto();
            bool paso = false;
            try
            {
                foreach (var item in r.Detalle)
                {
                    var habita = db.Habitaciones.Find(item.HabitacionId);
                    if (item.HabitacionId != 0)
                    {
                        habita.Estado = false;
                    }
                    if (db.Reservas.Add(r) != null)
                        db.Entry(r).State = EntityState.Modified;
                    paso = db.SaveChanges() > 0;
                }


            }
            catch (Exception) {
            }finally
            {
                db.Dispose();
            }
            return paso;
        } 
        public static bool checkout(Reservas r)
        {
            Contexto db = new Contexto();
            bool paso = false;
            try
            {
                foreach (var item in r.Detalle)
                {
                    var habita = db.Habitaciones.Find(item.HabitacionId);
                    if (item.HabitacionId != 0)
                    {
                        habita.Estado = true;
                    }
                    if(db.Reservas.Add(r) != null)
                   
                    db.Entry(r).State = EntityState.Modified;
                    paso = db.SaveChanges() > 0;
                
               
                    
                }


            }
            catch (Exception)
            {
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
    }
}