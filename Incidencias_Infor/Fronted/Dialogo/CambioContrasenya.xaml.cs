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

            if (comprobarContrasenya())
            {
                mvProf.setPassword(passNueva.Password );
                var pedro = mvProf.profe;
                bool result = mvProf.edita;

                if (result)
                {
                    await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "Se cambio la contraseña");
                }
                else
                {
                    await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "No se pudo cambiar, wey");
                }
            }
            else
            {
                await this.ShowMessageAsync("GESTIÓN DE CONTRASEÑA", "Eres debil contraseña, te faltan caracteres");
            }

            
        }

        private bool comprobarContrasenya()
        {
            bool correcto = false;
            string password = passNueva.Password;
            string password2 = passDoble.Password;
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(password2))
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

                            if(result > 48 && result < 57)
                            {
                                numero = true;
                            }

                            if(result > 65 && result < 90)
                            {
                                letraMas = true;
                            }

                            if(result > 97 && result < 122)
                            {
                                letraMen = true;
                            }
                            
                        }

                        if (numero && letraMas && letraMen )
                        {
                            correcto = true;
                        }
                    }
                
                }
                
            }
            return correcto;
        }
        
    }
}
