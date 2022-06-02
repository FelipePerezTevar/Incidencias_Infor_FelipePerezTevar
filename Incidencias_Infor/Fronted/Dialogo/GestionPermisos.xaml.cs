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
            inicializa();
            DataContext = mvrol;
            
            

        }

        private void inicializa()
        {
            mvrol.rolSel = profe.rol1;
            
        }

        private async void BtnEditarPermiso_Click(object sender, RoutedEventArgs e)
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
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Guardar",
                    NegativeButtonText = "Volver",
                    AnimateShow = true,
                    AnimateHide = false
                };

                MessageDialogResult result = await this.ShowMessageAsync("Lincidencias warning", "¿Quieres guardar los cambios realizados?",
                                MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {

                    mvrol.rolSel.permiso = mvrol.ListRolPermiso;
                    
                    if (mvrol.editar)
                    {
                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Se han guardado los cambios");
                        btnAnyadir.Visibility = Visibility.Collapsed;
                        btnAnyadirTodos.Visibility = Visibility.Collapsed;
                        btnQuitar.Visibility = Visibility.Collapsed;
                        btnQuitarTodos.Visibility = Visibility.Collapsed;
                        BtnDeshacer.Visibility = Visibility.Collapsed;
                        lbPermiso.Visibility = Visibility.Collapsed;
                        BtnEditarPermiso.Content = "Editar";
                    }
                    else
                    {
                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se ha podido guardar los cambios");
                    }
                    
                }
                

            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Comprobar si hay cambios para deshacer o no
            var pedro = new List<permiso>();
            mvrol.llenarListaPermiso();
            mvrol.ListRolPermiso = mvrol.rolSel.permiso.ToList();

            foreach (permiso permi in mvrol.ListPermiso)
            {
                if (mvrol.ListRolPermiso.Contains(permi))
                {
                    pedro.Add(permi);
                }
            }

            foreach (permiso permi in pedro)
            {
                mvrol.ListPermiso.Remove(permi);
            }
        }

        private async void BtnDeshacer_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Deshacer",
                NegativeButtonText = "Volver",
                AnimateShow = true,
                AnimateHide = false
            };

            MessageDialogResult result = await this.ShowMessageAsync("Lincidencias warning", "Si vuelves perderás los cambios, ¿estás seguro de volver?",
                            MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if(result == MessageDialogResult.Affirmative)
            {
                mvrol.rolSel = profe.rol1;
                mvrol.listPermiRol = mvrol.rolSel.permiso.ToList();
                btnAnyadir.Visibility = Visibility.Collapsed;
                btnAnyadirTodos.Visibility = Visibility.Collapsed;
                btnQuitar.Visibility = Visibility.Collapsed;
                btnQuitarTodos.Visibility = Visibility.Collapsed;
                BtnDeshacer.Visibility = Visibility.Collapsed;
                lbPermiso.Visibility = Visibility.Collapsed;
                BtnEditarPermiso.Content = "Editar";
            }
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            var listaPermiso = new List<permiso>();
            var listaRolPermiso = new List<permiso>();

            foreach(permiso permi in mvrol.ListRolPermiso)
            {
                listaRolPermiso.Add(permi);
            }

            foreach(permiso permi in mvrol.ListPermiso)
            {
                listaPermiso.Add(permi);
            }

            listaPermiso.Add(mvrol.permiRol);
            listaRolPermiso.Remove(mvrol.permiRol);

            mvrol.ListRolPermiso = listaRolPermiso;
            mvrol.ListPermiso = listaPermiso;
        }

        private void btnQuitarTodos_Click(object sender, RoutedEventArgs e)
        {
            var listaPermiso = new List<permiso>();
            var listaRolPermiso = new List<permiso>();

            foreach (permiso permi in mvrol.ListRolPermiso)
            {
                listaRolPermiso.Add(permi);
            }

            foreach (permiso permi in mvrol.ListPermiso)
            {
                listaPermiso.Add(permi);
            }

            foreach (permiso per in listaRolPermiso)
            {
                listaPermiso.Add(per);
            }

            listaRolPermiso = new List<permiso>();

            mvrol.ListRolPermiso = listaRolPermiso;
            mvrol.ListPermiso = listaPermiso;
        }

        private void btnAnyadir_Click(object sender, RoutedEventArgs e)
        {
            var listaPermiso = new List<permiso>();
            var listaRolPermiso = new List<permiso>();

            foreach (permiso permi in mvrol.ListRolPermiso)
            {
                listaRolPermiso.Add(permi);
            }

            foreach (permiso permi in mvrol.ListPermiso)
            {
                listaPermiso.Add(permi);
            }

            listaRolPermiso.Add(mvrol.permiOri);
            listaPermiso.Remove(mvrol.permiOri);

            mvrol.ListRolPermiso = listaRolPermiso;
            mvrol.ListPermiso = listaPermiso;
        }

        private void btnAnyadirTodos_Click(object sender, RoutedEventArgs e)
        {

            var listaPermiso = new List<permiso>();
            var listaRolPermiso = new List<permiso>();

            foreach (permiso permi in mvrol.ListRolPermiso)
            {
                listaRolPermiso.Add(permi);
            }

            foreach (permiso permi in mvrol.ListPermiso)
            {
                listaPermiso.Add(permi);
            }

            foreach (permiso per in listaPermiso)
            {
                listaRolPermiso.Add(per);
            }

            listaPermiso = new List<permiso>();

            mvrol.ListRolPermiso = listaRolPermiso;
            mvrol.ListPermiso = listaPermiso;
        }
    }
}
