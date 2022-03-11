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
    /// Lógica de interacción para DialogoIncidencias.xaml
    /// </summary>
    public partial class DialogoIncidencias : MetroWindow
    {
        private incidenciasEntities inciEnt;
        private MVIncidencia mvInci;
        private profesor profLogin;
        private incidencia inci;
        private bool guardado = false;
        private bool editar = false;
        public DialogoIncidencias(incidenciasEntities ent, profesor prof, incidencia inc)
        {
            InitializeComponent();
            inciEnt = ent;
            profLogin = prof;
            inci = inc;
            mvInci = new MVIncidencia(inciEnt);
            DataContext = mvInci;
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvInci.OnErrorEvent));
            mvInci.btnGuardar = btnAceptar;

            inicializa();
            
        }

        private void inicializa()
        {
            if(inci == null)
            {
                mvInci.inciNueva = new incidencia();
            }
            else
            {
                mvInci.inciNueva = inci;
                editar = true;
            }

            if (!profLogin.rol1.permiso.Contains(mvInci.bloqueProf))
            {
                txtSeparador.Visibility = Visibility.Collapsed;
                comboResponsable.Visibility = Visibility.Collapsed;
                comboEstado.Visibility = Visibility.Collapsed;
                comboEstado.SelectedItem = mvInci.estadoProf;
                checkComunicado.Visibility = Visibility.Collapsed;
                checkComunicado.IsChecked = false;
                checkFinalizado.Visibility = Visibility.Collapsed;
                txtObservacion.Visibility = Visibility.Collapsed;
            }
            
        }

        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (mvInci.IsValid(this))
            {
                if (DateIncio.SelectedDate < DateTime.Now)
                {
                    if (comboLugar.SelectedItem != null || comboArticulo.SelectedItem != null || 
                        DateIncio.SelectedDate != null || txtDescripcion.Text != null)
                    {

                        if (editar)
                        {
                            bool editado = mvInci.edita;

                            if (editado)
                            {
                                await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Se ha actualizado la incidencia");
                            }
                            else
                            {
                                await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se pudo actualizar la incidencia");
                            }
                        }
                        else
                        {
                            mvInci.inciNueva.fecha_introduccion = DateTime.Now;
                            mvInci.inciNueva.profesor1 = profLogin;
                            mvInci.inciNueva.profesor2 = mvInci.coordTIC;
                            mvInci.inciNueva.estado1 = mvInci.estadoProf;
                            mvInci.inciNueva.comunicado = 0;

                            guardado = mvInci.guarda;

                            if (guardado)
                            {
                                await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "La incidencia ya está subida en la base");
                            }
                            else
                            {
                                await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se pudo guardar la incidencia");
                            }
                        }
                    }
                    else
                    {
                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Todos los campos son obligatorios");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "La fecha de inicio no puede ser posterior a la actual");
                }

               
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.comunicado = 1;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.comunicado = 0;
        }

        private void checkFinalizado_Checked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.fecha_resolucion = DateTime.Now;
            mvInci.inciNueva.tiempo = mvInci.inciNueva.fecha_resolucion - mvInci.inciNueva.fecha_introduccion;
        }

        private void checkFinalizado_Unchecked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.fecha_resolucion = null;
            mvInci.inciNueva.tiempo = null;
        }
    }
}
