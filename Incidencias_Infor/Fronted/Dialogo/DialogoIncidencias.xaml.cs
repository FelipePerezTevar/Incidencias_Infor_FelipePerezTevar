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
        private incidencias_informaticasEntities inciEnt;
        private MVIncidencia mvInci;
        private MVHardware mvHard;
        private MVSoftware mvSoft;
        private profesor profLogin;
        private incidencia inci;
        private hardware hard;
        private software soft;
        
        private bool tipoware = true;
        private bool guardado = false;
        private bool editar = false;
        private bool garantia;
        public DialogoIncidencias(incidencias_informaticasEntities ent, profesor prof, incidencia inc, hardware h, software s)
        {
            InitializeComponent();
            inciEnt = ent;
            profLogin = prof;
            inci = inc;
            hard = h;
            soft = s;
            
            mvInci = new MVIncidencia(inciEnt);
            DataContext = mvInci;
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvInci.OnErrorEvent));
            mvInci.btnGuardar = btnAceptar;

            inicializa();
            
        }

        private void comprobarPermiso()
        {
            foreach(permiso permi in profLogin.rol1.permiso)
            {
                if (permi.codigo == 2){
                    btnAceptar.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;
                }
            }
        }

        private void inicializa()
        {
            
            //Comprueba si hay que editar o no
            if(inci == null)
            {
                mvInci.inciNueva = new incidencia();
                mvInci.num = 0;
                btnAceptar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;

            }
            else
            {

                comprobarPermiso();
                mvInci.inciNueva = inci;
                mvInci.num = 1;
                

                if(mvInci.inciNueva.comunicado == 0)
                {
                    checkComunicado.IsChecked = false;
                }
                else
                {
                    checkComunicado.IsChecked = true;
                }

                

                checkCambioware.IsEnabled = false;
                DateIncio.IsEnabled = false;
                comboLugar.IsEnabled = false;
                txtDescripcion.IsEnabled = false;

                if(hard != null)
                {
                    mvInci.hardNuevo = hard;
                    txtNumSerie.IsEnabled = false;
                    txtModelo.IsEnabled = false;
                    checkGarantia.IsEnabled = false;
                    comboTipoHW.IsEnabled = false;
                    checkCambioware.IsChecked = false;
                }
                else
                {
                    mvInci.softNuevo = soft;
                    txtSoftNombre.IsEnabled = false;
                    txtSoftVersion.IsEnabled = false;
                    checkCambioware.IsChecked = true;
                }

                editar = true;
            }

            //Y dependiendo del resultado, mostrará los campos de adminstracion o no
            if (!editar)
            {
                borderAdmin.Visibility = Visibility.Collapsed;
                checkComunicado.IsChecked = false;
            }
            else
            {
                btnAceptar.Content = "EDITAR";
                btnCancelar.Content = "BORRAR";
            }

            mvHard = new MVHardware(inciEnt);
            mvSoft = new MVSoftware(inciEnt);
            

        }
        //Este método comprueba y guarda las incidencias en la base de datos
        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(mvInci.inciNueva == null)
            {
                mvInci.inciNueva = new incidencia();
                mvInci.hardNuevo = new hardware();
                mvInci.softNuevo = new software();
            }
           
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
                    if (comboLugar.SelectedItem != null &&
                        DateIncio.SelectedDate != null && !string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        //Finalmente, comprueba si el usuario a elegido editar o borrar
                        if (editar)
                        {
                            //IF -> edita la incidencia correspondiente
                            bool editado = mvInci.edita;
                            //Dependiendo del resultado de la edición, nos dirá que ha salido bien o mal
                            if (editado)
                            {
                                await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Se  ha actualizado la incidencia");

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
                                    if (!string.IsNullOrEmpty(txtSoftNombre.Text) && !string.IsNullOrEmpty(txtSoftVersion.Text))
                                    {
                                        mvSoft.wareNuevo = mvInci.softNuevo;
                                        mvSoft.wareNuevo.incidencia1 = mvInci.inciNueva;
                                        bool softGuarda = mvSoft.guarda;
                                        mvInci.softNuevo = null;

                                        if (softGuarda)
                                        {
                                            guardado = true;
                                            mvInci.inciNueva = null;
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
                                    if (!string.IsNullOrEmpty(txtNumSerie.Text) && !string.IsNullOrEmpty(txtModelo.Text) && comboTipoHW.SelectedItem != null)
                                    {
                                        
                                        mvHard.wareNuevo = mvInci.hardNuevo;
                                        mvHard.wareNuevo.incidencia1 = mvInci.inciNueva;
                                        if (garantia)
                                        {
                                            mvHard.wareNuevo.garantia = 1;
                                        }
                                        else
                                        {
                                            mvHard.wareNuevo.garantia = 0;
                                        }
                                        
                                        bool hardGuarda = mvHard.guarda;
                                        mvInci.hardNuevo = null;

                                        if (hardGuarda)
                                        {
                                            guardado = true;
                                            mvInci.inciNueva = null;
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
        
        private async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            bool borrarWare;
            bool borrarInci;
            
            

            if (mvInci.IsValid(this))
            {
                if (editar)
                {

                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Borrar",
                        NegativeButtonText = "Volver",
                        AnimateShow = true,
                        AnimateHide = false
                    };

                    MessageDialogResult result = await this.ShowMessageAsync("Lincidencias warning", "¿Quieres borrar este registro?",
                                    MessageDialogStyle.AffirmativeAndNegative, mySettings);

                    if (tipoware == false)
                    {
                        mvSoft.wareNuevo = mvInci.softNuevo;

                         

                        if (result == MessageDialogResult.Affirmative)
                        {
                            borrarWare = mvSoft.borrar;
                        }
                        else
                        {
                            borrarWare = false;
                        }

                        
                    }
                    else
                    {

                        mvHard.wareNuevo = mvInci.hardNuevo;

                        if(result == MessageDialogResult.Affirmative)
                        {
                            borrarWare = mvHard.borrar;
                        }
                        else
                        {
                            borrarWare = false;
                        }
                        
                    }

                    if (borrarWare)
                    {

                        if (result == MessageDialogResult.Affirmative){
                            borrarInci = mvInci.borrar;
                        }
                        else
                        {
                            borrarInci = false;
                        }

                        

                        if (borrarInci)
                        {
                            await this.ShowMessageAsync("GESTIÓN DE INCIDENCIAS", "Se ha borrado la incidencia actual");
                            this.Close();
                        }
                        
                    }

                    
                    
                }
                else
                {
                    this.Close();
                    
                }
            }

            
            
        }

        //Si esta check, la incidencia pasa a estado de comunicado
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.comunicado = 1;
            comboEstado.IsEnabled = true;
            
        }

        //Si esta uncheck, la incidencia pasa a estado de no comunicado
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            mvInci.inciNueva.comunicado = 0;
            comboEstado.IsEnabled = false;
            
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

        private void comboEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mvInci.inciNueva.estado1.nombre.Equals("En solucion"))
            {
                mvInci.inciNueva.fecha_resolucion = null;
                mvInci.inciNueva.tiempo = null;
            }
            else
            {
                mvInci.inciNueva.fecha_resolucion = DateTime.Now;
                mvInci.inciNueva.tiempo = mvInci.inciNueva.fecha_resolucion - mvInci.inciNueva.fecha_introduccion;
            }
        }
    }
}
