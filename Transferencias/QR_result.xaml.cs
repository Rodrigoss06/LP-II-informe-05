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
    public partial class QR_result : ContentPage
    {
        private string cuentaBancaria;
        private double monto;
        public QR_result(string cuenta)
        {
            InitializeComponent();
            cuenta_bancaria_input.Text = cuenta;
        }
        


        private void btn_transferir_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cuenta_bancaria_input.Text))
            {
                cuentaBancaria = cuenta_bancaria_input.Text;
            }
            if (!string.IsNullOrEmpty(monto_input.Text))
            {
                monto = Convert.ToDouble(monto_input.Text);
            }
            Navigation.PopModalAsync();
        }

        private void btn_model_cancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}