using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.MVVM;
using MahApps.Metro.Controls;
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
    /// Lógica de interacción para GestionPermisos.xaml
    /// </summary>
    public partial class GestionPermisos : MetroWindow
    {

        private incidencias_informaticasEntities inciEnt;
        private MVRol mvrol;
        private profesor profe;
        

        public GestionPermisos(incidencias_informaticasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;
            profe = prof;
            mvrol = new MVRol(inciEnt);
            mvrol.rolSel = profe.rol1;
            mvrol.listPermiRol = mvrol.rolSel.permiso.ToList();
            DataContext = mvrol;

        }

        

        private void BtnEditarPermiso_Click(object sender, RoutedEventArgs e)
        {
            if (BtnEditarPermiso.Content.Equals("Editar"))
            {
                btnAnyadir.Visibility = Visibility.Visible;
                btnAnyadirTodos.Visibility = Visibility.Visible;
                btnQuitar.Visibility = Visibility.Visible;
                btnQuitarTodos.Visibility = Visibility.Visible;
                BtnDeshacer.Visibility = Visibility.Visible;
                lbPermiso.Visibility = Visibility.Visible;
                BtnEditarPermiso.Content = "Guardar";
            }
            else
            {
                //FUNCION DE GRUARDAR SIN EMPLEMENTAR POR AHORA
                btnAnyadir.Visibility = Visibility.Collapsed;
                btnAnyadirTodos.Visibility = Visibility.Collapsed;
                btnQuitar.Visibility = Visibility.Collapsed;
                btnQuitarTodos.Visibility = Visibility.Collapsed;
                BtnDeshacer.Visibility = Visibility.Collapsed;
                lbPermiso.Visibility = Visibility.Collapsed;
                BtnEditarPermiso.Content = "Editar";

            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mvrol.ListRolPermiso = mvrol.rolSel.permiso.ToList();
        }
    }
}
