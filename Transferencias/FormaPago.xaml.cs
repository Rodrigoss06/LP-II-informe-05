using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Transferencias
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

    public class StackLayoutCreator
    {
        public static Frame HistorialItem(string cuentaBancaria, double monto, DateTime fechaYHoraActual)
        {
            Console.WriteLine(cuentaBancaria + " " + monto);
            // Crear los elementos Label para cuentaBancaria, fechaYHoraActual y monto
            Label labelCuentaBancaria = new Label { Text = cuentaBancaria, TextColor= Color.Black, FontAttributes= FontAttributes.Bold };
            Label labelFechaYHoraActual = new Label { Text = fechaYHoraActual.ToString() };
            Label labelMonto = new Label { Text = "S/ " + monto.ToString() , TextColor = Color.Black, FontAttributes = FontAttributes.Bold };

            // Crear un StackLayout horizontal para colocar fechaYHoraActual y monto
            StackLayout stackCuentaYFechaYHora = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    labelCuentaBancaria,
                    labelFechaYHoraActual
                    
                },
            };

            // Crear un StackLayout vertical para organizar cuentaBancaria encima de stackFechaYHoraYmonto
            StackLayout stackCuentaYFechaYHoraYmonto = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    stackCuentaYFechaYHora,
                    labelMonto
                },
                BackgroundColor = Color.White,
            };
            Frame frame = new Frame
            {
                Content = stackCuentaYFechaYHoraYmonto,
                BackgroundColor = Color.White,
                HasShadow = false, // Opcional: desactivar la sombra si no la deseas
                Padding = new Thickness(10, 0, 0, 5), // Espacio de relleno en la parte inferior para el borde
                Margin = new Thickness(0, 10, 0, 10), // Margen en la parte inferior para el borde
                CornerRadius = 0, // Opcional: establecer el radio de esquina del Frame
                BorderColor = Color.Black, // Color del borde
            };
            return frame;
        }
         
    }
	public partial class FormaPago : ContentPage
	{
        private Usuario usuario1;
		public FormaPago (Usuario usuario)
		{   
            usuario1 = usuario;
			InitializeComponent ();
		}

        private void btn_transferencia_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Transferencia Bancaria");
            Navigation.PushAsync(new Transferencia_bancaria(usuario1));
        }

        private void btn_plin_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Plin");
            Navigation.PushAsync(new transferencias_plin(usuario1));
        }

        private void btn_yape_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Yape");
            Navigation.PushAsync(new transferencias_yape(usuario1));
        }
    }
}