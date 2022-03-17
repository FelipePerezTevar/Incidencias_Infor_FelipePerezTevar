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
        //Declaracion de todas las variables necesarias
        private incidenciasEntities inciEnt;
        private MVIncidencia mvInci;
        private MVHardware mvHard;
        private MVSoftware mvSoft;
        private profesor profLogin;
        private incidencia inci;
        
        private bool tipoware = true;
        private bool guardado = false;
        private bool editar = false;
        private bool garantia;
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
            //Comprueba si hay que editar o no
            if(inci == null)
            {
                mvInci.inciNueva = new incidencia();
                
                
            }
            else
            {
                mvInci.inciNueva = inci;
                editar = true;
            }

            //Y dependiendo del resultado, mostrará los campos de adminstracion o no
            if (!editar)
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

            mvHard = new MVHardware(inciEnt);
            mvSoft = new MVSoftware(inciEnt);
            

        }
        //Este método comprueba y guarda las incidencias en la base de datos
        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //Primero, comprueba si es valido
            if (mvInci.IsValid(this))
            {
                //A continuación, comprueba si la fecha de inicio es mayor a la actual
                //IF -> continua correctamente
                //ELSE -> salta un mensaje de error diciendo que la fecha de inicio no debe ser superior
                if (DateIncio.SelectedDate < DateTime.Now)
                {
                    //Después comprueba que los campos de incidencia tengan información
                    //IF -> continua correctamente
                    //ELSE -> salta un mensaje de error diciendo que todos los campos son obligatorios
                    if (comboLugar.SelectedItem != null ||
                        DateIncio.SelectedDate != null || txtDescripcion.Text != null)
                    {
                        //Finalmente, comprueba si el usuario a elegido editar o borrar
                        if (editar)
                        {
                            //IF -> edita la incidencia correspondiente
                            bool editado = mvInci.edita;
                            //Dependiendo del resultado de la edición, nos dirá que ha salido bien o mal
                            if (editado)
                            {
                                if (tipoware == false)
                                {
                                    bool softEdita = mvSoft.edita;

                                    if (softEdita)
                                    {
                                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Se ha actualizado la incidencia");
                                    }
                                    else
                                    {
                                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se a podido actualizar la incidencia");
                                    }

                                }
                                else
                                {
                                    bool hardEdita = mvHard.edita;

                                    if (hardEdita)
                                    {
                                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Se ha actualizado la incidencia");
                                    }
                                    else
                                    {
                                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se a podido actualizar la incidencia");
                                    }
                                }
                                
                            }
                            else
                            {
                                await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se pudo actualizar la incidencia");
                            }
                        }
                        else
                        {
                            //ELSE -> guarda la nueva incidencia

                            //Primero asigna los campos que no debe introducir el usuario con los valores 
                            //que necesita la incidencia al principio
                            mvInci.inciNueva.fecha_introduccion = DateTime.Now;
                            mvInci.inciNueva.profesor1 = profLogin;
                            mvInci.inciNueva.profesor2 = mvInci.coordTIC;
                            mvInci.inciNueva.estado1 = mvInci.estadoProf;
                            mvInci.inciNueva.comunicado = 0;

                            //A continuación guarda la incidencia 
                            guardado = mvInci.guarda;

                            //Finalmente, dependiendo del resultado del guardado
                            //IF -> continua correctamente 
                            //ELSE -> salta un mensaje diciendo que no se ha podido guardar la incidencia
                            if (guardado)
                            {
                                //Comprueba si la variable es hardware(true) o software(false)
                                if (tipoware == false)
                                {
                                    //IF -> es un software y comprueba que sus campos no esten vacios para guardarlos
                                    //*IF -> asigna la información correspondiente al objeto software y lo guarda
                                    //*ELSE -> salta un mensaje diciendo que todos los campos son obligatorios
                                    if (txtSoftNombre.Text != null || txtSoftVersion.Text != null)
                                    {
                                        mvSoft.wareNuevo = mvInci.softNuevo;
                                        mvSoft.wareNuevo.incidencia1 = mvInci.inciNueva;
                                        bool softGuarda = mvSoft.guarda;

                                        if (softGuarda)
                                        {
                                            guardado = true;
                                        }
                                    }
                                    else
                                    {
                                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Todos los campos son obligatorios");
                                    }

                                }
                                else
                                {
                                    //ELSE -> es un hardware y comprueba que los campos no esten vacios para guardarlos
                                    //*IF -> asigna los valores al objeto hardware y lo guarda
                                    //*ELSE -> salta un mensaje diciendo que todos los campos son obligatorios
                                    if (txtNumSerie.Text != null || txtModelo.Text != null || comboTipoHW.SelectedItem != null)
                                    {
                                        
                                        mvHard.wareNuevo = mvInci.hardNuevo;
                                        mvInci.hardNuevo.incidencia1 = mvInci.inciNueva;
                                        if (garantia)
                                        {
                                            mvHard.wareNuevo.garantia = 1;
                                        }
                                        else
                                        {
                                            mvHard.wareNuevo.garantia = 0;
                                        }
                                        
                                        bool hardGuarda = mvHard.guarda;

                                        if (hardGuarda)
                                        {
                                            guardado = true;
                                        }
                                        else
                                        {
                                            await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "No se guardo el hardware");
                                        }
                                    }
                                    else
                                    {
                                        await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Todos los campos son obligatorios");
                                    }
                                }

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
        //Cierra el dialogo
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        //Si esta check, la incidencia pasa a estado de comunicado
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.comunicado = 1;
        }

        //Si esta uncheck, la incidencia pasa a estado de no comunicado
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.comunicado = 0;
        }

        //Si la incidencia se ha finalizado, la fecha de resolucion pasa a la actual y se calcula el tiempo
        private void checkFinalizado_Checked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.fecha_resolucion = DateTime.Now;
            mvInci.inciNueva.tiempo = mvInci.inciNueva.fecha_resolucion - mvInci.inciNueva.fecha_introduccion;
        }

        //Si la incidencia no se ha finalizado, la fecha de resolucion es nula y no se calcula el tiempo
        private void checkFinalizado_Unchecked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.fecha_resolucion = null;
            mvInci.inciNueva.tiempo = null;
        }

        //Si está check, los campos relacionados con el software pasan a visible, los de hardware a colapsados
        //y al bool encargado de gestionar el tipo pasa a ser software(false)
        private void checkCambioware_Checked(object sender, RoutedEventArgs e)
        {
            txtNumSerie.Visibility = Visibility.Collapsed;
            txtModelo.Visibility = Visibility.Collapsed;
            checkGarantia.Visibility = Visibility.Collapsed;
            comboTipoHW.Visibility = Visibility.Collapsed;
            txtHardware.Visibility = Visibility.Collapsed;

            txtSoftNombre.Visibility = Visibility.Visible;
            txtSoftVersion.Visibility = Visibility.Visible;
            txtSoftware.Visibility = Visibility.Visible;

            tipoware = false;
        }

        //Si está uncheck, los campos relacionados con hardware pasan a estar visibles, los campos de software
        //pasan a estar colapsados y la variable que controla el tipo de incidencia pasa hardware(true)
        private void checkCambioware_Unchecked(object sender, RoutedEventArgs e)
        {
            txtNumSerie.Visibility = Visibility.Visible;
            txtModelo.Visibility = Visibility.Visible;
            checkGarantia.Visibility = Visibility.Visible;
            comboTipoHW.Visibility = Visibility.Visible;
            txtHardware.Visibility = Visibility.Visible;

            txtSoftNombre.Visibility = Visibility.Collapsed;
            txtSoftVersion.Visibility = Visibility.Collapsed;
            txtSoftware.Visibility = Visibility.Collapsed;

            tipoware = true;
        }

        //Si esta check, el hardware tendrá garantia
        private void checkGarantia_Checked(object sender, RoutedEventArgs e)
        {
            garantia = true;
        }

        //Si esta uncheck, el hardware no tendrá garantia
        private void checkGarantia_Unchecked(object sender, RoutedEventArgs e)
        {
            garantia = false;
        }
    }
}
