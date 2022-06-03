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
        //Hola
        private incidencias_informaticasEntities inciEnt;
        private MVProfesor mvProf;

        public CambioContrasenya(incidencias_informaticasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;

            mvProf = new MVProfesor(inciEnt);
            DataContext = mvProf;

            mvProf.profe = prof;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Borrar",
                NegativeButtonText = "Volver",
                AnimateShow = true,
                AnimateHide = false
            };

            MessageDialogResult comprobacion = await this.ShowMessageAsync("Lincidencias warning", "¿Seguro de querer cambiar la contraseña?",
                            MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (comprobacion == MessageDialogResult.Affirmative)
            {
                if (comprobarContrasenya())
                {
                    mvProf.setPassword(mvProf.cifrarContraseña(passNueva.Password));
                    bool result = mvProf.edita;

                    if (result)
                    {
                        await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "Se cambio la contraseña");
                        this.Close();
                    }
                    else
                    {
                        await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "No se pudo cambiar");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "La contraseña no cumple con los requisitos para ser guardada");
                }
            }

        }

        private bool comprobarContrasenya()
        {
            bool correcto = false;
            string password = passNueva.Password;
            string password2 = passDoble.Password;
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(password2))
            {

                if (!mvProf.cifrarContraseña(password).Equals(mvProf.profe.contrasenya))
                {
                    if (password.Equals(password2))
                    {
                        char[] characters = password.ToCharArray();

                        int cant = password.Length;

                        if (cant >= 8)
                        {
                            bool numero = false;
                            bool letraMas = false;
                            bool letraMen = false;


                            for (int i = 0; i < cant; i++)
                            {
                                int result = Convert.ToInt32(characters.GetValue(i));

                                if (result > 48 && result < 57)
                                {
                                    numero = true;
                                }

                                if (result > 65 && result < 90)
                                {
                                    letraMas = true;
                                }

                                if (result > 97 && result < 122)
                                {
                                    letraMen = true;
                                }

                            }

                            if (numero && letraMas && letraMen)
                            {
                                correcto = true;
                            }
                        }

                    }
                }
                
            }
            return correcto;
        }

        private void passNueva_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passNueva.Password))
            {
                passDoble.IsEnabled = true;
            }
            else
            {
                passDoble.IsEnabled = false;
            }
            
        }

        private void passDoble_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(passNueva.Password) || !string.IsNullOrEmpty(passDoble.Password)){
                btnCambiar.IsEnabled = true;
            }
            else
            {
                btnCambiar.IsEnabled = false;
            }
        }
    }
}
