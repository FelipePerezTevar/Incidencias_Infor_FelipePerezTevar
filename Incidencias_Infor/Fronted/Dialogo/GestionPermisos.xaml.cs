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
        
        /// <summary>
        /// Inicializa todo para el correcto funcionamiento 
        /// de la ventana.
        /// </summary>
        /// <param name="ent">La conexión con la base de datos</param>
        /// <param name="prof">El profesor que está utilizando la aplicación</param>
        public GestionPermisos(incidencias_informaticasEntities ent, profesor prof)
        {
            InitializeComponent();
            inciEnt = ent;
            profe = prof;
            mvrol = new MVRol(inciEnt);
            mvrol.rolSel = profe.rol1;
            DataContext = mvrol;
            
            

        }

        
        /// <summary>
        /// Dependiendo del estado en que se encuentre el botón hará lo siguiente:
        /// Dejar el modo de visualización y pasar al modo de edición.
        /// Guardar los cambios realizados en un solo permiso y volver al modo de 
        /// visualización.
        /// </summary>
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

        /// <summary>
        /// Llena las listas de los listBox con los permisos correspondientes.
        /// </summary>

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var listaAuxiliar = new List<permiso>();
            mvrol.llenarListaPermiso();
            mvrol.ListRolPermiso = mvrol.rolSel.permiso.ToList();

            foreach (permiso permi in mvrol.ListPermiso)
            {
                if (mvrol.ListRolPermiso.Contains(permi))
                {
                    listaAuxiliar.Add(permi);
                }
            }

            foreach (permiso permi in listaAuxiliar)
            {
                mvrol.ListPermiso.Remove(permi);
            }
        }

        /// <summary>
        /// Pregunta al usuario si quiere deshacer los cambios y
        /// si la respuesta es afirmativa, los deshace
        /// </summary>
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

        /// <summary>
        /// Quita el permiso seleccionado de la lista del rol.
        /// </summary>
        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {

            if(mvrol.permiRol.codigo != 0)
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

                listaPermiso.Add(mvrol.permiRol);
                listaRolPermiso.Remove(mvrol.permiRol);

                mvrol.ListRolPermiso = listaRolPermiso;
                mvrol.ListPermiso = listaPermiso;
            }
            
        }

        /// <summary>
        /// Quita todos los permisos del rol.
        /// </summary>
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

        /// <summary>
        /// Añade un permiso a la lista del rol.
        /// </summary>
        private void btnAnyadir_Click(object sender, RoutedEventArgs e)
        {
            if (mvrol.permiOri.codigo != 0)
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
               
        }
        /// <summary>
        /// Añade todos los permisos al rol
        /// </summary>
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
