using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Incidencias_Infor.MVVM
{
    class MVHardware : MVBaseCRUD<hardware>
    {
        private incidencias_informaticasEntities inciEnt;
        private HardwareServicio hardServ;
        private TipoHWServicio tipoServ;
        private EstadoServicio estServ;
        private IncidenciaServicio inciServ;
        private hardware hard;
        private string _textoCheck;
        

        private ListCollectionView listaWare;
        private DateTime fechaInicio;
        private DateTime fechaFinal;
        private tipohw tipo;
        private profesor prof;
        
       
        private List<Predicate<hardware>> criterios;
        private Predicate<hardware> criterioFecha;
        private Predicate<hardware> criterioTipo;
        private Predicate<hardware> criterioProf;
        
      

        public MVHardware(incidencias_informaticasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = hardServ;
        }

        /// <summary>
        /// Inicializa todo lo necesario para
        /// poder relacionar correctamente
        /// el modelo con la vista.
        /// </summary>
        private void inicializa()
        {
            hardServ = new HardwareServicio(inciEnt);
            tipoServ = new TipoHWServicio(inciEnt);
            inciServ = new IncidenciaServicio(inciEnt);
            estServ = new EstadoServicio(inciEnt);
            fechaInicio = inciServ.getFechaInicio();
            fechaFinal = inciServ.getFechaFinal();
            
            criterios = new List<Predicate<hardware>>();
            listaWare = new ListCollectionView(hardServ.getAll().ToList());
            criterioFecha = new Predicate<hardware>(m => m.incidencia1.fecha_introduccion >= inicioSeleccionado
            && m.incidencia1.fecha_introduccion <= finalSeleccionado);
            criterioTipo = new Predicate<hardware>(m => m.tipohw != null && m.tipohw.Equals(tipoSeleccionado));
            criterioProf = new Predicate<hardware>(m => m.incidencia1.profesor1 != null &&  m.incidencia1.profesor2 != null && (m.incidencia1.profesor1.dni.Equals(profUsuario.dni) || m.incidencia1.profesor2.dni.Equals(profUsuario.dni)));
            
            hard = new hardware();
        }

        /// <summary>
        /// Cambia el valor de las propiedades por 
        /// su valor dado en la inicialización.
        /// </summary>
        public void refrescarFiltro()
        {
            inicioSeleccionado = inciServ.getFechaInicio();
            finalSeleccionado = inciServ.getFechaFinal();
            tipoSeleccionado = null;
        }

        /// <summary>
        /// Es la lista del combo para el filtrado por tipo de hardware
        /// </summary>
        public List<tipohw> listTipo { get { return tipoServ.getAll().ToList(); } }


        /// <summary>
        /// Es el objeto que se pasará para editar
        /// </summary>
        public hardware wareNuevo { get { return hard; } set { hard = value;NotifyPropertyChanged(nameof(wareNuevo));} }

        /// <summary>
        /// Le da valor al check de cambio de tipo de incidencia
        /// </summary>
        public string textoCheck { get { return _textoCheck; } set { _textoCheck = value; NotifyPropertyChanged(nameof(textoCheck)); } }

        /// <summary>
        /// Es el objeto de la fecha de inicio en el filtro de rango de fechas
        /// </summary>
        public DateTime inicioSeleccionado { get { return fechaInicio; } set { fechaInicio = value; NotifyPropertyChanged(nameof(inicioSeleccionado)); } }

        /// <summary>
        /// Es el objeto de la fecha final en el filtro de rango de fechas
        /// </summary>
        public DateTime finalSeleccionado { get { return fechaFinal; } set { fechaFinal = value; NotifyPropertyChanged(nameof(finalSeleccionado)); } }

        /// <summary>
        /// Esta propiedad representa el objeto seleccionado del filtro de
        /// tipo hardware
        /// </summary>
        public tipohw tipoSeleccionado { get { return tipo; } set { tipo = value; NotifyPropertyChanged(nameof(wareNuevo)); } }

        /// <summary>
        /// Es el usuario que está usando la aplicación
        /// </summary>
        public profesor profUsuario { get { return prof; } set { prof = value;NotifyPropertyChanged(nameof(wareNuevo)); } }


        /// <summary>
        /// Es la lista de incidencias de tipo hardware
        /// </summary>
        public ListCollectionView ListWare2 { get { return listaWare; } }

        /// <summary>
        /// Permite guardar un hardware en la base de datos
        /// </summary>
        public bool guarda { get { return add(wareNuevo); } }

        /// <summary>
        /// Permite edita un hardware en la base de datos
        /// </summary>
        public bool edita { get { return update(wareNuevo); } }

        /// <summary>
        /// Permite borrar un hardware en la base de datos
        /// </summary>
        public bool borrar { get{ return delete(wareNuevo); } }


        /// <summary>
        /// Llena la lista con criterios para después ser filtrados
        /// </summary>
        public void addCriterios()
        {
            criterios.Clear();

            if(inicioSeleccionado != null && finalSeleccionado != null)
            {
                criterios.Add(criterioFecha);
            }

            if(tipoSeleccionado != null)
            {
                criterios.Add(criterioTipo);
            }

            if(profUsuario != null)
            {
                criterios.Add(criterioProf);
                
            }
        }

        /// <summary>
        /// Comprueba para cada software si cumple con los filtros
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Devuelve true si el hardware cumple con los criterios de filtrado
        /// Devuelve false si no los cumple
        /// </returns>
        public bool filtroCombinadoCriterios(object item)
        {
            bool correcto = true;
            hardware hard = (hardware)item;

            if(criterios.Count != 0)
            {
                correcto = criterios.TrueForAll(x => x(hard));
            }

            return correcto;
        }

        
    }
}
