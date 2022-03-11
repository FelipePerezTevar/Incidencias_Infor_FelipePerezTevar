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
        private MVIncidencia mvInci;
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
            mvInci = new MVIncidencia(inciEnt);
            DataContext = mvInci;
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgIncidencia.SelectedItem != null){

                DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin,mvInci.inciNueva);
                diag.Show();
            }
            
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgIncidencia.SelectedItem != null)
            {
                bool borrado = mvInci.borrar;

                if (borrado)
                {
                    MessageBox.Show("Ha sido eliminado con exito",
                        "PRUEBA BORRADO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se borro, wey",
                        "PRUEBA BORRADO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
    }
}
