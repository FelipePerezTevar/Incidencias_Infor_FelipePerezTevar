using Incidencias_Infor.Backend.Modelo;
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
    /// Lógica de interacción para CambioContrasenya.xaml
    /// </summary>
    public partial class CambioContrasenya : MetroWindow
    {

        private incidenciasEntities inciEnt;
        private MVProfesor mvProf;

        public CambioContrasenya(incidenciasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;

            mvProf = new MVProfesor(inciEnt);
            DataContext = mvProf;

            mvProf.profe = prof;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            mvProf.setPassword(passNueva.Password);
            bool result = mvProf.edita;

            if (result) {
                await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "Se cambio la contraseña");
            }
            else
            {
                await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "No se pudo cambiar, wey");
            }
        }
    }
}
