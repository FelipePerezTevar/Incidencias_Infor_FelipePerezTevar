using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Fronted.ControlesUsuario;
using Incidencias_Infor.Fronted.Dialogo;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Incidencias_Infor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private incidencias_informaticasEntities inciEnt;
        private profesor profLogin;
        public MainWindow(incidencias_informaticasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;
            profLogin = prof;
            //btnAjustes.Content = profLogin.nombre;
            comprobarPermiso();

            UCIncidencias uc = new UCIncidencias(inciEnt, profLogin);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        /// <summary>
        /// Coge los permisos que tiene el rol del profesor que se registra
        /// y dependiendo de los que tenga mostrará unos menús u otros.
        /// </summary>

        public void comprobarPermiso()
        {
            foreach(permiso permi in profLogin.rol1.permiso)
            {
                switch (permi.codigo)
                {
                    case 1:
                        menuNuevo.Visibility = Visibility.Visible;
                        break;

                    

                    case 4:
                        menuPermisos.Visibility = Visibility.Visible;
                        break;

                    

                    case 5:
                        menuInformes.Visibility = Visibility.Visible;
                        break;
                };
                    

            }
        }

        /// <summary>
        /// Abre la ventana de edición de incidencias vacia, para poder 
        /// crear una nueva incidencia.
        /// </summary>
        
        private void menuNuevo_Click(object sender, RoutedEventArgs e)
        {
            DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin,null, null, null);
            diag.ShowDialog();
        }

        /// <summary>
        /// Muestra en el centro de la ventana principal la lista
        /// de incidencias registradas en la base de datos.
        /// </summary>
        private void menuListar_Click(object sender, RoutedEventArgs e)
        {
            
            UCIncidencias uc = new UCIncidencias(inciEnt, profLogin);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }

        
        /// <summary>
        /// Muestra en el centro de la ventana principal un informe
        /// con el gráfico de cuantas incidencias hay por departamento.
        /// </summary>
        private void informeDpto_Click(object sender, RoutedEventArgs e)
        {
            UCInformeDepartamento uci = new UCInformeDepartamento();
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uci);
        }

        /// <summary>
        /// Muestra en el centro de la ventana principal un informe
        /// con el informe de cuantas incidencias de cada tipo de hardware.
        /// </summary>
        private void informeTipoHw_Click(object sender, RoutedEventArgs e)
        {
            UCInformeTipoHardware uci = new UCInformeTipoHardware();
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uci);
        }


        /// <summary>
        /// Muestra en el centro de la ventana principal un informe
        /// con la cantidad de incedencias que ha habido cada mes.
        /// </summary>
        private void informeMes_Click(object sender, RoutedEventArgs e)
        {
            UCInformeMes uci = new UCInformeMes();
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uci);
        }

        /// <summary>
        /// Este menú permite cerrar la sesión.
        /// </summary>
        private async void cerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Salir",
                NegativeButtonText = "Volver",
                AnimateShow = true,
                AnimateHide = false
            };

            MessageDialogResult result = await this.ShowMessageAsync("Lincidencias warning", "¿Quieres cerrar la sesión?",
                            MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if(result == MessageDialogResult.Affirmative)
            {
                Login login = new Login();
                login.Show();

                this.Close();
            }
            
        }

        /// <summary>
        /// Abre una ventana que permite cambiar la contraseña del 
        /// profesor que se ha registrado.
        /// </summary>
        private void cambioContrasenya_Click(object sender, RoutedEventArgs e)
        {
            CambioContrasenya diag = new CambioContrasenya(inciEnt, profLogin);
            diag.ShowDialog();
        }

        /// <summary>
        /// Abre una ventana que muestra los permisos que tiene el rol
        /// del profesor registrado, además de poder editar que permisos tiene
        /// cada rol.
        /// </summary>
        private void permisos_Click(object sender, RoutedEventArgs e)
        {
            GestionPermisos diag = new GestionPermisos(inciEnt, profLogin );
            diag.ShowDialog();
        }
    }
}
