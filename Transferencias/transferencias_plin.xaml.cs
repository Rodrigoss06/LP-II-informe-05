using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Transferencias
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class transferencias_plin : ContentPage
	{
        private Usuario usuario1;
        public StackLayout getHistorial { get { return Historial; } }
        public transferencias_plin (Usuario usuario)
		{
            usuario1 = usuario;
			InitializeComponent ();
            Saldo.Text = Convert.ToString(usuario1.GetSaldoUsuario());
            foreach (HistorialItem historialItem in GestorUsuarios.Instancia.ObtenerUsuarioPorDni(usuario1.GetDni()).ObtenerHistorial())
            {
                Frame historialItem1 = StackLayoutCreator.HistorialItem(historialItem.Nombre, historialItem.Monto, historialItem.FechaYHoraActual);
                Historial.Children.Add(historialItem1);
            }
        }

        private async void EscanearQR_Clicked(object sender, EventArgs e)
        {
            string cuenta = await Scanner();
            await Navigation.PushModalAsync(new QR_result(cuenta));
        }

        private async Task<string> Scanner()
        {
            var scannerPage = new ZXingScannerPage();

            scannerPage.Title = "Lector QR";

            var tcs = new TaskCompletionSource<string>(); // TCS para completar la tarea cuando se escanea un código

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    tcs.SetResult(result.Text); // Establecer el resultado de la tarea
                });
            };

            await Navigation.PushAsync(scannerPage);

            // Esperar a que se complete la tarea (escaneo del código QR)
            return await tcs.Task;
        }


        private void transferencia_plin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ModalPlin(this,usuario1));
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