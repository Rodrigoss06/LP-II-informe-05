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
    public partial class Transferencia_bancaria : ContentPage
    {
        private Usuario usuario1;
        public StackLayout getHistorial { get { return Historial; } }
        public Transferencia_bancaria(Usuario usuario)
        {
            InitializeComponent();
            usuario1 = usuario;
            Saldo.Text = Convert.ToString(usuario1.GetSaldoUsuario());
            foreach (HistorialItem historialItem in GestorUsuarios.Instancia.ObtenerUsuarioPorDni(usuario1.GetDni()).ObtenerHistorial())
            {
                Frame historialItem1 = StackLayoutCreator.HistorialItem(historialItem.Nombre, historialItem.Monto, historialItem.FechaYHoraActual);
                Historial.Children.Add(historialItem1);
            }
        }

        private void otras_cuentas_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Otras cuentas");
            Navigation.PushModalAsync(new ModalOtrasCuentas(this,usuario1));
        }

        private void entre_cuentas_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Entre cuentas");
            Navigation.PushModalAsync(new ModalEntreCuentas(this,usuario1));
        }
        public void RecargarPagina()
        {
            // Actualizar el saldo u otros datos en la página según sea necesario
            Saldo.Text = Convert.ToString(GestorUsuarios.Instancia.ObtenerUsuarioPorDni(usuario1.GetDni()).GetSaldoUsuario());
            Historial.Children.Clear();
            foreach (HistorialItem historialItem in GestorUsuarios.Instancia.ObtenerUsuarioPorDni(usuario1.GetDni()).ObtenerHistorial())
            {
                Frame historialItem1 = StackLayoutCreator.HistorialItem(historialItem.Nombre, historialItem.Monto, historialItem.FechaYHoraActual);
                Historial.Children.Add(historialItem1);
            }
        }
    }
}