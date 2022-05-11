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

    public static class InfoBox
    {
        public async static Task<MessageDialogResult> ShowMessageAsync(string title, string Message, MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        {
            return await ((MetroWindow)(Application.Current.MainWindow)).ShowMessageAsync(title, Message, style, settings);
        }
    }

    /// <summary>
    /// Lógica de interacción para CambioContrasenya.xaml
    /// </summary>
    public partial class CambioContrasenya : MetroWindow
    {
        public CambioContrasenya()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var pedro = await InfoBox.ShowMessageAsync("Pedro", "Pedro");

            if (pedro == MessageDialogResult.Negative)
            {
                await this.ShowMessageAsync("mamahuevo", "you are a wizard, Harry");
            }
            else
            {
                await this.ShowMessageAsync("chupa pija", "galactico");
            }
        }
    }
}
