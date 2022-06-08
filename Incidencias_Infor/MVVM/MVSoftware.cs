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
    class MVSoftware : MVBaseCRUD<software>
    {
        
        private SoftwareServicio softServ;
        private IncidenciaServicio inciServ;
        private incidencias_informaticasEntities inciEnt;
        private EstadoServicio estServ;
        private software soft;
        private string _textoCheck;

        private ListCollectionView listaWare;
        private DateTime fechaInicio;
        private DateTime fechaFinal;
        
        private profesor prof;

        private List<Predicate<software>> criterios;
        private Predicate<software> criteriosFecha;
        private Predicate<software> criterioProf;
        
        /// <summary>
        /// Inicializa todo lo necesario para
        /// poder relacionar correctamente
        /// el modelo con la vista.
        /// </summary>
        /// <param name="ent">Conexión con la base de datos</param>
        public MVSoftware(incidencias_informaticasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = softServ;
        }

        private void inicializa()
        {
            softServ = new SoftwareServicio(inciEnt);
            inciServ = new IncidenciaServicio(inciEnt);
            estServ = new EstadoServicio(inciEnt);
            soft = new software();
            
            fechaInicio = inciServ.getFechaInicio();
            fechaFinal = inciServ.getFechaFinal();
            criterios = new List<Predicate<software>>();
            listaWare = new ListCollectionView(softServ.getAll().ToList());
            criteriosFecha = new Predicate<software>(m => m.incidencia1.fecha_introduccion >= inicioSeleccionado
            && m.incidencia1.fecha_introduccion <= finalSeleccionado);
            
            criterioProf = new Predicate<software>(m => m.incidencia1.profesor1 != null && m.incidencia1.profesor2 != null && (m.incidencia1.profesor1.dni.Equals(profUsuario.dni) || m.incidencia1.profesor2.dni.Equals(profUsuario.dni)));
            
        }

        /// <summary>
        /// Cambia el valor de las propiedades por 
        /// su valor dado en la inicialización.
        /// </summary>
        public void refrescarFiltro()
        {
            inicioSeleccionado = inciServ.getFechaInicio();
            finalSeleccionado = inciServ.getFechaFinal();
        }

        /// <summary>
        /// Le da valor al check de cambio de tipo de incidencia
        /// </summary>
        public string textoCheck { get {return _textoCheck; } set { _textoCheck = value; NotifyPropertyChanged(nameof(textoCheck) );  } }


        /// <summary>
        /// Es el objeto que se pasará para editar
        /// </summary>
        public software wareNuevo { get { return soft; } set { soft = value; NotifyPropertyChanged(nameof(wareNuevo)); } }


        /// <summary>
        /// Es el objeto de la fecha de inicio en el filtro de rango de fechas
        /// </summary>
        public DateTime inicioSeleccionado { get { return fechaInicio; } set { fechaInicio = value; NotifyPropertyChanged(nameof(inicioSeleccionado));} }

        /// <summary>
        /// Es el objeto de la fecha final en el filtro de rango de fechas
        /// </summary>
        public DateTime finalSeleccionado { get { return fechaFinal; } set { fechaFinal = value; NotifyPropertyChanged(nameof(finalSeleccionado)); } }
        
        /// <summary>
        /// Es el usuario que está usando la aplicación
        /// </summary>
        public profesor profUsuario { get { return prof; } set { prof = value; NotifyPropertyChanged(nameof(wareNuevo)); } }

        /// <summary>
        /// Esta propiedad representa el objeto seleccionado del filtro de
        /// tipo hardware, y se encuntra nulo porque no se va a utilizar
        /// </summary>
        public tipohw tipoSeleccionado { get { return null; }  set { } }
        /// <summary>
        /// Esta propiedad representa la lista del combo del filtro de
        /// tipo de hardware, y se encuentra nulo porque no se va a utilizar
        /// </summary>
        public List<tipohw> listTipo { get { return null; } }

        /// <summary>
        /// Es la lista de incidencias de tipo software
        /// </summary>
        public ListCollectionView ListWare2 { get { return listaWare; } }

        /// <summary>
        /// Permite guardar un software en la base de datos
        /// </summary>
        public bool guarda { get { return add(wareNuevo); } }
        /// <summary>
        /// Permite editar un software en la base de datos
        /// </summary>
        public bool edita { get { return update(wareNuevo); } }
        /// <summary>
        /// Permite borrar un software en la base de datos
        /// </summary>
        public bool borrar { get { return delete(wareNuevo); } }

        
        /// <summary>
        /// Llena la lista con criterios para después ser filtrados
        /// </summary>
        public void addCriterios()
        {
            criterios.Clear();

            if (inicioSeleccionado != null && finalSeleccionado != null)
            {
                criterios.Add(criteriosFecha);
            }

            if (profUsuario != null)
            {
                criterios.Add(criterioProf);
               
            }
        }

        /// <summary>
        /// Comprueba para cada software si cumple con los filtros
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Devuelve true si el software cumple con los criterios de filtrado
        /// Devuelve false si no los cumple
        /// </returns>
        public bool filtroCombinadoCriterios(object item)
        {
            bool correcto = true;
            software soft = (software)item;

            if (criterios.Count != 0)
            {
                correcto = criterios.TrueForAll(x => x(soft));
            }

            return correcto;
        }

        
    }
}
