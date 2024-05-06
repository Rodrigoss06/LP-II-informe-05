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
	public partial class ModalPlin : ContentPage
	{
        private Usuario usuario_main;
        private int numero;
        private double monto;
        private transferencias_plin _paginaTransferencias;
        public ModalPlin (transferencias_plin paginaTransferencias, Usuario usuario)
		{
			InitializeComponent ();
            _paginaTransferencias = paginaTransferencias;
            usuario_main = usuario;
        }

        private void btn_model_cancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void btn_model_plin_Clicked(object sender, EventArgs e)
        {
            bool continuar = false;
            if (!string.IsNullOrEmpty(numero_input.Text) && !string.IsNullOrEmpty(monto_input.Text))
            {
                foreach (Usuario usuario in GestorUsuarios.Instancia.ObtenerTodosLosUsuarios())
                {
                    if (Convert.ToInt32(numero_input.Text) == usuario.GetNumero() && Convert.ToInt32(numero_input.Text) != usuario_main.GetNumero())
                    {
                        numero = Convert.ToInt32(numero_input.Text);
                        continuar = true; 
                        break;
                    }
                }

                if (Convert.ToDouble(monto_input.Text) < usuario_main.GetSaldoUsuario())
                {
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
                Plin plin = new Plin(monto, user1,user2);
                bool status = plin.Pagar();
                if (status)
                {
                    _paginaTransferencias.RecargarPagina();
                    Navigation.PopModalAsync();
                }

            }
        }
    }
}