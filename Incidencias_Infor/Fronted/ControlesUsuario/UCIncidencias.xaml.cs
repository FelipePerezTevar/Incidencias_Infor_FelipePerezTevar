using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Fronted.Dialogo;
using Incidencias_Infor.MVVM;
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

namespace Incidencias_Infor.Fronted.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCIncidencias.xaml
    /// </summary>
    public partial class UCIncidencias : UserControl
    {
        private incidenciasEntities inciEnt;
        private MVHardware mvHard;
        private MVSoftware mvSoft;
        private profesor profLogin;
        public UCIncidencias(incidenciasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;
            profLogin = prof;
            inicializa();

        }

        private void inicializa()
        {
            mvHard = new MVHardware(inciEnt);
            mvSoft = new MVSoftware(inciEnt);
            DataContext = mvHard;
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e)
        {
            
            if (dgIncidencia.SelectedItem != null){

                if(dgIncidencia.SelectedItem is hardware)
                {
                    DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin, mvHard.wareNuevo.incidencia1);
                    diag.Show();
                }
                else
                {
                    DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin, mvSoft.wareNuevo.incidencia1);
                    diag.Show();
                }

                
            }
            
            
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Esto borrará cosas wey",
                        "PRUEBA BORRADO", MessageBoxButton.OK, MessageBoxImage.Information);


        }

        private void checkTipoWare_Checked(object sender, RoutedEventArgs e)
        {
            
            hardSerie.Visibility = Visibility.Collapsed;
            hardTipo.Visibility = Visibility.Collapsed;
            softNombre.Visibility = Visibility.Visible;
            softVersion.Visibility = Visibility.Visible;
            DataContext = mvSoft;
            
        }

        private void checkTipoWare_Unchecked(object sender, RoutedEventArgs e)
        {
            
            hardSerie.Visibility = Visibility.Visible;
            hardTipo.Visibility = Visibility.Visible;
            softNombre.Visibility = Visibility.Collapsed;
            softVersion.Visibility = Visibility.Collapsed;
            DataContext = mvHard;
            
        }
    }
}
