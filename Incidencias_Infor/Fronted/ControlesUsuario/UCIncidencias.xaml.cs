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
        private incidencias_informaticasEntities inciEnt;
        private MVHardware mvHard;
        private MVSoftware mvSoft;
        private profesor profLogin;
        private bool tipoWare;

        public UCIncidencias(incidencias_informaticasEntities ent, profesor prof)
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
            mvHard.textoCheck = "Cambiar a incidencias software";

            if (profLogin.rol1.nombre.Equals("Profesor"))
            {
                mvHard.profUsuario = profLogin;
                mvSoft.profUsuario = profLogin;

                if(mvHard.profUsuario != null)
                {
                    mvHard.addCriterios();
                    mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);

                    mvSoft.addCriterios();
                    mvSoft.ListWare2.Filter = new Predicate<object>(mvSoft.filtroCombinadoCriterios);
                }
            }
        }

        

        private void checkTipoWare_Checked(object sender, RoutedEventArgs e)
        {
            
            hardSerie.Visibility = Visibility.Collapsed;
            hardTipo.Visibility = Visibility.Collapsed;
            softNombre.Visibility = Visibility.Visible;
            softVersion.Visibility = Visibility.Visible;
            comboTipo.Visibility = Visibility.Collapsed;
            DataContext = mvSoft;
            tipoWare = true;
            mvSoft.textoCheck = "Cambiar a incidencias hardware";
            
        }

        private void checkTipoWare_Unchecked(object sender, RoutedEventArgs e)
        {
            
            hardSerie.Visibility = Visibility.Visible;
            hardTipo.Visibility = Visibility.Visible;
            softNombre.Visibility = Visibility.Collapsed;
            softVersion.Visibility = Visibility.Collapsed;
            comboTipo.Visibility = Visibility.Visible;
            DataContext = mvHard;
            tipoWare = false;
            mvHard.textoCheck = "Cambiar a incidencias software";
            
        }

        private void dgIncidencia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            if (dgIncidencia.SelectedItem != null)
            {

                if (dgIncidencia.SelectedItem is hardware)
                {
                    DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin, mvHard.wareNuevo.incidencia1, mvHard.wareNuevo, null);
                    diag.Show();
                }
                else
                {
                    DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin, mvSoft.wareNuevo.incidencia1, null, mvSoft.wareNuevo);
                    diag.Show();
                }


            }
        }

        private void dateInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(mvHard.inicioSeleccionado <= mvHard.finalSeleccionado)
            {
                mvHard.addCriterios();
                mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);
            }

            if (mvSoft.inicioSeleccionado <= mvSoft.finalSeleccionado)
            {
                mvSoft.addCriterios();
                mvSoft.ListWare2.Filter = new Predicate<object>(mvSoft.filtroCombinadoCriterios);
            }
        }

        private void dateFinal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mvHard.inicioSeleccionado <= mvHard.finalSeleccionado)
            {
                mvHard.addCriterios();
                mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);
            }

            if (mvSoft.inicioSeleccionado <= mvSoft.finalSeleccionado)
            {
                mvSoft.addCriterios();
                mvSoft.ListWare2.Filter = new Predicate<object>(mvSoft.filtroCombinadoCriterios);
            }
        }

        private void comboTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(mvHard.tipoSeleccionado != null)
            {
                mvHard.addCriterios();
                mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);
            }
            
        }

        

        private void comboEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mvHard.tipoSeleccionado != null)
            {
                mvHard.addCriterios();
                mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);
            }
        }

        private void btnPruebarRefresco_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            

            mvSoft = null;
            mvHard = null;

            mvSoft = new MVSoftware(inciEnt);
            mvHard = new MVHardware(inciEnt);

            if (tipoWare == true)
            {
                DataContext = mvSoft;
                mvSoft.textoCheck = "Cambiar a incidencias hardware";
            }
            else
            {
                DataContext = mvHard;
                mvHard.textoCheck = "Cambiar a incidencias software";
            }
        }
    }
}
