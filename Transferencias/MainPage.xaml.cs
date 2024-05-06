using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Transferencias;

namespace Transferencias
{

    public class HistorialItem
    {
        public string Nombre { get; set; }
        public double Monto { get; set; }
        public DateTime FechaYHoraActual { get; set; }
    }

    public class Usuario
    {
        private string nombre_completo;
        private int dni;
        private int numero;
        private string correo;
        private double saldo_usuario;
        private string cuentaBancaria;
        private string password;
        private List<HistorialItem> historial;
        public Usuario(string nombre_completo, int dni, int numero, string correo, double saldo_usuario, string password)
        {
            this.nombre_completo = nombre_completo;
            this.dni = dni;
            this.numero = numero;
            this.correo = correo;
            this.saldo_usuario = saldo_usuario;
            this.password = password;
            historial = new List<HistorialItem>();
        }

        public Usuario(string nombre_completo, int dni, int numero, string correo, double saldo_usuario, string cuentaBancaria, string password)
        {
            this.nombre_completo = nombre_completo;
            this.dni = dni;
            this.numero = numero;
            this.correo = correo;
            this.saldo_usuario = saldo_usuario;
            this.cuentaBancaria = cuentaBancaria;
            this.password = password;
            historial = new List<HistorialItem>();
        }

        public string GetNombreCompleto()
        {
            return this.nombre_completo;
        }

        public void SetNombreCompleto(string nombreComp)
        {
            this.nombre_completo = nombreComp;
        }

        public int GetDni()
        {
            return this.dni;
        }

        public void SetDni(int _dni)
        {
            this.dni = _dni;
        }
        public int GetNumero()
        {
            return this.numero;
        }

        public void SetNumero(int numero)
        {
            this.numero = numero;
        }
        public string GetCorreo()
        {
            return this.correo;
        }

        public void SetCorreo(string _correo)
        {
            this.correo = _correo;
        }

        public double GetSaldoUsuario()
        {
            return this.saldo_usuario;
        }

        public void SetSaldoUsuario(double saldoUsuario)
        {
            this.saldo_usuario = saldoUsuario;
        }

        public string GetCuentaBancaria()
        {
            return this.cuentaBancaria;
        }

        public void SetCuentaBancaria(string _cuentaBancaria)
        {
            this.cuentaBancaria = _cuentaBancaria;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }
        public void AgregarAlHistorial(HistorialItem item)
        {
            historial.Add(item);
        }

        public List<HistorialItem> ObtenerHistorial()
        {
            return historial;
        }

    }
    public class FormaDePago
    {
        protected double monto;
        protected Usuario usuario1;
        protected Usuario usuario2;


        public FormaDePago(double monto, Usuario usuario1, Usuario usuario2)
        {
            this.monto = monto;
            this.usuario1 = usuario1;
            this.usuario2 = usuario2;
        }

        public FormaDePago()
        {
            this.monto = 0.0;
            this.usuario1 = null;
            this.usuario2 = null;
        }

        public virtual bool Pagar()
        {
            // Modificar el saldo del usuario1
            double nuevoSaldo1 = usuario1.GetSaldoUsuario() - this.monto;
            usuario1.SetSaldoUsuario(nuevoSaldo1);

            // Agregar el pago al historial del usuario
            usuario1.AgregarAlHistorial(new HistorialItem { Nombre = usuario2.GetNombreCompleto(), Monto = this.monto*-1, FechaYHoraActual = DateTime.Now });

            // Modificar el saldo del usuario
            double nuevoSaldo2 = usuario2.GetSaldoUsuario() + this.monto;
            usuario2.SetSaldoUsuario(nuevoSaldo2);
            // Agregar el pago al historial del usuario
            usuario2.AgregarAlHistorial(new HistorialItem { Nombre = usuario1.GetNombreCompleto(), Monto = this.monto, FechaYHoraActual = DateTime.Now });
            return true;
        }

        ~FormaDePago()
        {
            Console.WriteLine("Borrando Ingreso de Datos");
        }
    }



    public class Yape : FormaDePago
    {
        private const double MAXIMO_YAPE = 500.00;

        public Yape(double monto, Usuario usuario1, Usuario usuario2) : base(monto, usuario1, usuario2){ }

        public Yape() : base() { }

        public override bool Pagar()
        {
            Console.WriteLine("Pagando " + this.monto + " soles a " + this.usuario1.GetNombreCompleto() + " mediante Yape");
            if (monto > MAXIMO_YAPE)
            {
                Application.Current.MainPage.DisplayAlert("Error", "El monto de " + monto + " soles excede el límite de " + MAXIMO_YAPE + " soles para Yape", "Aceptar");
                return false;
            }
            
            base.Pagar(); // Llama al método Pagar de la clase base para actualizar el saldo y el historial
            return true;
        }

        ~Yape()
        {
                Console.WriteLine("Borrando Ingreso de Datos");
        }
    }


    public class Plin : FormaDePago
    {
        private const double MAXIMO_PLIN = 700.00;

        public Plin(double monto, Usuario usuario1, Usuario usuario2) : base(monto, usuario1, usuario2) {}

        public Plin() : base() { }

        public override bool Pagar()
        {
            Console.WriteLine("Pagando " + this.monto + " soles a " + this.usuario1.GetNombreCompleto() + " mediante Yape");
            if (monto > MAXIMO_PLIN)
            {
                Application.Current.MainPage.DisplayAlert("Error", "El monto de " + monto + " soles excede el límite de " + MAXIMO_PLIN + " soles para Plin", "Aceptar");
                return false;
            }

            base.Pagar(); // Llama al método Pagar de la clase base para actualizar el saldo y el historial
            return true;
        }

        ~Plin()
        {
            Console.WriteLine("Borrando Ingreso de Datos");
        }
    }


    public class TransferenciaBancaria : FormaDePago
    {
        private const double MAXIMO_TBANCARIA = 1000.00;

        public TransferenciaBancaria(double monto, Usuario usuario1, Usuario usuario2) : base(monto, usuario1, usuario2) {}

        public TransferenciaBancaria() : base() { }

        public override bool Pagar()
        {
            Console.WriteLine("Pagando " + this.monto + " soles a " + this.usuario1.GetNombreCompleto() + " mediante Yape");
            if (monto > MAXIMO_TBANCARIA)
            {
                Application.Current.MainPage.DisplayAlert("Error", "El monto de " + monto + " soles excede el límite de " + MAXIMO_TBANCARIA + " soles para Plin", "Aceptar");
                return false;
            }

            base.Pagar(); // Llama al método Pagar de la clase base para actualizar el saldo y el historial
            return true;
        }

        ~TransferenciaBancaria()
        {
            Console.WriteLine("Borrando Ingreso de Datos");
        }
    }

    public class GestorUsuarios
    {

        private static GestorUsuarios instancia;
        private List<Usuario> ListaUsuarios;

        // Propiedad estática para acceder a la instancia de GestorUsuarios
        public static GestorUsuarios Instancia
        {
            get
            {
                // Si la instancia aún no ha sido creada, crear una nueva
                if (instancia == null)
                {
                    instancia = new GestorUsuarios();
                }
                return instancia;
            }
        }

        // Constructor
        public GestorUsuarios()
        {
            ListaUsuarios = new List<Usuario>();
        }

        // Método para agregar un nuevo usuario al sistema
        public void AgregarUsuario(Usuario nuevoUsuario)
        {
            ListaUsuarios.Add(nuevoUsuario);
        }

        // Método para eliminar un usuario existente del sistema
        public void EliminarUsuario(Usuario usuario)
        {
            ListaUsuarios.Remove(usuario);
        }

        // Método para actualizar la información de un usuario existente en el sistema
        public void ActualizarUsuario(Usuario usuario)
        {
            // Implementa la lógica para actualizar la información del usuario
        }

        // Método para obtener un usuario específico basado en su número de identificación (DNI)
        public Usuario ObtenerUsuarioPorDni(int dni)
        {
            return ListaUsuarios.Find(u => u.GetDni() == dni);
        }

        // Método para obtener un usuario específico basado en su número de identificación (DNI)
        public Usuario ObtenerUsuarioPorNumero(int numero)
        {
            return ListaUsuarios.Find(u => u.GetNumero() == numero);
        }

        // Método para obtener todos los usuarios registrados en el sistema
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            return ListaUsuarios;
        }

        // Método para autenticar a un usuario
        public bool AutenticarUsuario(string nombreUsuario, string contraseña)
        {
            // Implementa la lógica para autenticar al usuario
            return false;
        }

        // Método para cambiar la contraseña de un usuario
        public void CambiarContraseña(Usuario usuario, string nuevaContraseña)
        {
            // Implementa la lógica para cambiar la contraseña del usuario
        }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CrearCuenta_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearCuenta_1());
        }

        private void InciarSesion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IniciarSesion());
        }
    }
};
