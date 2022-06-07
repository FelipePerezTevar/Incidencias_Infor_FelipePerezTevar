using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using Incidencias_Infor.MVVM;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Incidencias_Infor.Fronted.Dialogo
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {

        private incidencias_informaticasEntities inciEnt;
        private ProfesorServicio profServ;
        private MVProfesor mvProf;

        /// <summary>
        /// Compruba si se ha obtenido la conexión con la base de datos
        /// e inicializa las variable de ProfesorServicio y MVProfesor
        /// </summary>
        public Login()
        {
            InitializeComponent();
            if (!conectar())
            {
                MessageBox.Show("No se ha podido acceder a la base de datos",
                    "CONEXIÓN BASE DE DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            profServ = new ProfesorServicio(inciEnt);
            mvProf = new MVProfesor(inciEnt);
        }

        /// <summary>
        /// Comprueba que el usuario y la contraseña corresponden
        /// con un profesor de la base de datos, y le permite entrar
        /// en la aplicación.
        /// </summary>
        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            
            
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(passClaveAcceso.Password))
                {
                    await this.ShowMessageAsync("LOGIN", "La contraseña y el usuario no pueden ser vacios");
                }
                else if (profServ.login(txtUsername.Text, mvProf.cifrarContraseña(passClaveAcceso.Password)))
                {
                    MainWindow ventaPrincipal = new MainWindow(inciEnt, profServ.profLogin);
                    ventaPrincipal.Show();
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("LOGIN", "El usuario y la contraseña son incorrectas");
                }
            

            
        }

        /// <summary>
        /// Realiza la conexión con la base de datos.
        /// </summary>
        /// <returns>
        /// Devuelve true si se ha podido conectar con la base de datos y
        /// devuelve false si ha habido un error al conectarse.
        /// </returns>
        private bool conectar()
        {
            bool conecta = true;
            inciEnt = new incidencias_informaticasEntities();
            try
            {
                inciEnt.Database.Connection.Open();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,
                   "CONEXIÓN BASE DE DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                conecta = false;
            }
            return conecta;
        }
    }
}
