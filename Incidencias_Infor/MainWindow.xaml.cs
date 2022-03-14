using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Fronted.ControlesUsuario;
using Incidencias_Infor.Fronted.Dialogo;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Incidencias_Infor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private incidenciasEntities inciEnt;
        private profesor profLogin;
        public MainWindow(incidenciasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;
            profLogin = prof;
            btnAjustes.Content = profLogin.nombre;
        }

        private void menuNuevo_Click(object sender, RoutedEventArgs e)
        {
            DialogoIncidencias diag = new DialogoIncidencias(inciEnt, profLogin,null);
            diag.ShowDialog();
        }

        private void menuListar_Click(object sender, RoutedEventArgs e)
        {
            
            UCIncidencias uc = new UCIncidencias(inciEnt, profLogin);
            gridCentral.Children.Clear();
            gridCentral.Children.Add(uc);
        }
    }
}
