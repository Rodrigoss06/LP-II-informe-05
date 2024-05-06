using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transferencias
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class IniciarSesion : ContentPage
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }
        
        int dni;
        string correo;
        string cuentaBancaria;
        string password;
        private void btn_submit_Clicked(object sender, EventArgs e)
        {
            GestorUsuarios.Instancia.AgregarUsuario(new Usuario("Nombre1", 123456789, 987654123, "correo1@example.com", 1000.0, "cuenta1", "contrasena1"));
            GestorUsuarios.Instancia.AgregarUsuario(new Usuario("Nombre2", 987654321, 999999999, "correo2@example.com", 2000.0, "cuenta2", "contrasena2"));

            if (!string.IsNullOrEmpty(DNI.Text))
            {
                dni = Convert.ToInt32(DNI.Text);
            }
            if (!string.IsNullOrEmpty(Correo.Text))
            {
                correo = Correo.Text.Trim();
            }
            if (!string.IsNullOrEmpty(Cuenta.Text))
            {
                cuentaBancaria = Cuenta.Text;
            }
            if (!string.IsNullOrEmpty(Contrasena.Text))
            {
                password = Contrasena.Text;
            }
            foreach (Usuario usuario in GestorUsuarios.Instancia.ObtenerTodosLosUsuarios())
            {
                if (usuario.GetDni() == dni && usuario.GetCorreo() == correo && usuario.GetCuentaBancaria() == cuentaBancaria && usuario.GetPassword() == password)
                {
                    Console.WriteLine("se ingreso correctamente.");
                    Console.WriteLine(dni);
                    Console.WriteLine(correo);
                    Console.WriteLine(cuentaBancaria);
                    Console.WriteLine(password);
                    Navigation.PushAsync(new FormaPago(usuario));

                    break;
                }
            }
        }
    }
}