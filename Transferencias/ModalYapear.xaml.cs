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
	public partial class ModalYapear : ContentPage
	{
        private Usuario usuario_main;
        private int numero;
        private double monto;
        private transferencias_yape _paginaTransferencias;

        public ModalYapear(transferencias_yape paginaTransferencias, Usuario usuario)
        {
            InitializeComponent();
            _paginaTransferencias = paginaTransferencias;
            usuario_main = usuario;
        }

        private void btn_model_yapea_Clicked(object sender, EventArgs e)
        {
            bool continuar = false;
            if (!string.IsNullOrEmpty(numero_input.Text) && !string.IsNullOrEmpty(monto_input.Text))
            {
                foreach (Usuario usuario in GestorUsuarios.Instancia.ObtenerTodosLosUsuarios())
                {
                    if (Convert.ToInt32(numero_input.Text) == usuario.GetNumero() && Convert.ToInt32(numero_input.Text) != usuario_main.GetNumero())
                    {
                        Console.WriteLine("numero encontrado");
                        numero = Convert.ToInt32(numero_input.Text);
                        continuar = true; 
                        break;
                    }
                }

                if (Convert.ToDouble(monto_input.Text) < usuario_main.GetSaldoUsuario())
                {
                    Console.WriteLine("monto aceptado");
                    monto = Convert.ToDouble(monto_input.Text);
                }else{
                    continuar = false;
                }

            }
            if (continuar)
            {
                // Crear una instancia de Yape

                Usuario user1 = GestorUsuarios.Instancia.ObtenerUsuarioPorDni(usuario_main.GetDni());
                Usuario user2 = GestorUsuarios.Instancia.ObtenerUsuarioPorNumero(numero);

                // Crear una instancia de Yape
                Yape yape = new Yape(monto, user1,user2);
                bool status = yape.Pagar();
                if (status)
                {
                    _paginaTransferencias.RecargarPagina();
                    Navigation.PopModalAsync();
                }

            }
        }

        private void btn_model_cancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}