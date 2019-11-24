using Hotel.BLL;
using Hotel.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public void InciarSesion()
        {
            RepositorioBase<Usuarios> Repositorio = new RepositorioBase<Usuarios>();
            Expression<Func<Usuarios, bool>> filtro = x => true;
            List<Usuarios> usuario = new List<Usuarios>();
            var username = UsuarioTextBox.Text;
            var password = ContrasenaTextBox.Text;
            filtro = x => x.Usuario.Equals(username);
            usuario = Repositorio.GetList(filtro);

            if (usuario.Exists(x => x.Usuario.Equals(username)))
            {
                if (usuario.Exists(x => x.Clave.Equals(Eramake.eCryptography.Encrypt(password))))
                {
                    List<Usuarios> id = Repositorio.GetList(U => U.Usuario == UsuarioTextBox.Text);
                  Menu f = new Menu(id[0].UsuarioId);
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nombre de usuarios o contraseñas incorrectos");
                    return;
                }
            }
            else
            {
                if (UsuarioTextBox.Text == string.Empty || ContrasenaTextBox.Text == string.Empty)
                    MessageBox.Show("LLene todos los campos");
                else if (!usuario.Exists(x => x.Usuario.Equals(username)))
                    MessageBox.Show("Nombre de usuarios o contraseñas incorrectos");
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

        }
    }
}
