using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transferencias
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCuenta_1 : ContentPage
    {
        public CrearCuenta_1()
        {
            InitializeComponent();
        }

            
        string nombre;
        int dni;
        int numero;
        string correo;
        double saldo;
        string cuentaBancaria;
        string password;
        private void btn_submit_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Nombre.Text))
            {
                nombre = Nombre.Text.Trim();
            }
            if (!string.IsNullOrEmpty(DNI.Text))
            {
                dni = Convert.ToInt32(DNI.Text);
            }
            if (!string.IsNullOrEmpty(Numero.Text))
            {
                numero = Convert.ToInt32(Numero.Text);
            }
            if (!string.IsNullOrEmpty(Correo.Text))
            {
                correo = Correo.Text.Trim();
            }
            if (!string.IsNullOrEmpty(Saldo.Text))
            {
                saldo = Convert.ToDouble(Saldo.Text);
            }
            if (!string.IsNullOrEmpty(Cuenta.Text))
            {
                cuentaBancaria = Cuenta.Text;
            }
            if (!string.IsNullOrEmpty(Contrasena.Text))
            {
                password = Contrasena.Text;
            }


            Usuario newUsuario = new Usuario(nombre, dni, numero, correo, saldo, cuentaBancaria, password);
            GestorUsuarios.Instancia.AgregarUsuario(newUsuario);
            Navigation.PopAsync();

        }
    }
}