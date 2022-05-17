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

        private incidenciasEntities inciEnt;
        private ProfesorServicio profServ;
        private MVProfesor mvProf;

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
        }

        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(passClaveAcceso.Password))
            {
                await this.ShowMessageAsync("LOGIN", "La contraseña y el usuario no pueden ser vacios");
            }
            else if(profServ.login(txtUsername.Text, passClaveAcceso.Password))
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

        private bool conectar()
        {
            bool conecta = true;
            inciEnt = new incidenciasEntities();
            try
            {
                inciEnt.Database.Connection.Open();

            }catch(Exception ex)
            {
                conecta = false;
            }
            return conecta;
        }
    }
}
