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

        /// <summary>
        /// Inicializa todo lo necesario para el correcto
        /// funcionamiento del control de usuario.
        /// </summary>
        private void inicializa()
        {
            mvHard = new MVHardware(inciEnt);
            mvSoft = new MVSoftware(inciEnt);
            

            DataContext = mvHard;
            mvHard.textoCheck = "Cambiar a incidencias software";
            dgIncidencia.SelectedItem = null;
            comprobarProfesor();
            
        }

        /// <summary>
        /// Si el rol del profesor registrado es "Profesor",
        /// solamente podrá ver las incidencias creadas por él
        /// y en las que es responsable.
        /// </summary>
        private void comprobarProfesor()
        {
            if (profLogin.rol1.nombre.Equals("Profesor"))
            {
                mvHard.profUsuario = profLogin;
                mvSoft.profUsuario = profLogin;

                if (mvHard.profUsuario != null)
                {
                    mvHard.addCriterios();
                    mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);

                    mvSoft.addCriterios();
                    mvSoft.ListWare2.Filter = new Predicate<object>(mvSoft.filtroCombinadoCriterios);
                }
            }
        }

        /// <summary>
        /// Deja de mostrar las incidencias hardware para mostrar las 
        /// incidencias software.
        /// </summary>
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

        /// <summary>
        /// Deja de ver las incidencias software para ver las incidencias hardware.
        /// </summary>
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

        /// <summary>
        /// Este método abre el dialogo de edición de incidencias
        /// con la incidencia seleccionada.
        /// </summary>
        private void dgIncidencia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (dgIncidencia.SelectedItem != null)
            {

                if (dgIncidencia.SelectedItem is hardware)
                {
                    DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin, ((hardware)dgIncidencia.SelectedItem).incidencia1, (hardware)dgIncidencia.SelectedItem, null);
                    diag.Show();
                    dgIncidencia.SelectedItem = null;
                }
                else
                {
                    DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin, ((software)dgIncidencia.SelectedItem).incidencia1, null, (software)dgIncidencia.SelectedItem);
                    diag.Show();
                    dgIncidencia.SelectedItem = null;
                }


            }
        }

        /// <summary>
        /// Este método establece la fecha de inicio del
        /// filtrado por rango de fechas.
        /// </summary>

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

        /// <summary>
        /// Este método establece la fecha final del filtrado
        /// por el rango de fechas.
        /// </summary>

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

        /// <summary>
        /// Permite filtrar por el tipo de hardware en las 
        /// incidencias de tipo hardware
        /// </summary>

        private void comboTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(mvHard.tipoSeleccionado != null)
            {
                mvHard.addCriterios();
                mvHard.ListWare2.Filter = new Predicate<object>(mvHard.filtroCombinadoCriterios);
            }
            
        }


        /// <summary>
        /// Refresca la lista de incidencias
        /// </summary>
        private void btnPruebarRefresco_Click(object sender, RoutedEventArgs e)
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

            comprobarProfesor();
        }

       

        /// <summary>
        /// Resetea los filtros.
        /// </summary>
        private void btnBorrarFiltro_Click(object sender, RoutedEventArgs e)
        {
            if(tipoWare == true)
            {
                mvSoft.refrescarFiltro();
            }
            else
            {
                comboTipo.SelectedIndex = -1;
                mvHard.refrescarFiltro();
            }

           
            
        }
    }
}
