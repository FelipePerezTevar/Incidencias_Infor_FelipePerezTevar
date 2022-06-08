using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.MVVM
{
    class MVIncidencia : MVBaseCRUD<incidencia>
    {
        private incidencias_informaticasEntities inciEnt;
        private LugarServicio luServ;
        private EstadoServicio estServ;
        private IncidenciaServicio inciServ;
        private ProfesorServicio profServ;
        private HardwareServicio hardServ;
        private SoftwareServicio softServ;
        private PermisoServicio permServ;
        private TipoHWServicio tipoServ;

        private profesor profTIC;
        private hardware hard;
        private software soft;
        private estado estadoEnSolucion;
        private permiso limiteProfesor;
        private incidencia inci;
        private int numColumna;

        public MVIncidencia(incidencias_informaticasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = inciServ;
        }


        /// <summary>
        /// Inicializa con todo lo necesario para
        /// poder conectar correctamente el modelo
        /// con la vista
        /// </summary>
        private void inicializa()
        {
            luServ = new LugarServicio(inciEnt);
            estServ = new EstadoServicio(inciEnt);
            profServ = new ProfesorServicio(inciEnt);
            inciServ = new IncidenciaServicio(inciEnt);
            hardServ = new HardwareServicio(inciEnt);
            softServ = new SoftwareServicio(inciEnt);
            permServ = new PermisoServicio(inciEnt); 
            tipoServ = new TipoHWServicio(inciEnt);

            inci = new incidencia();
            hard = new hardware();
            soft = new software();
            estadoEnSolucion = estServ.getEnSolucion();
            limiteProfesor = permServ.bloqueProfesor();
            profTIC = profServ.getCoordTIC();
           
        }

        /// <summary>
        /// Lista con todos los tipos de hardware
        /// </summary>
        public List<tipohw> listTipo { get { return tipoServ.getAll().ToList(); } }

        /// <summary>
        /// Lista con todos los lugares
        /// </summary>
        public List<lugar> listLugar { get { return luServ.getAll().ToList(); } }

        /// <summary>
        /// Lista con todos los estado
        /// </summary>
        public List<estado> listEstado { get { return estServ.getAll().ToList(); } }

        /// <summary>
        /// Lista con todos los profesores
        /// </summary>
        public List<profesor> listProfesor { get { return profServ.getAll().ToList(); } }

        /// <summary>
        /// Coge el estado "En solución" para darselo a la incidencia
        /// que se va a crear.
        /// </summary>
        public estado estadoProf { get { return estadoEnSolucion; } }

        /// <summary>
        /// Coge el profesor que es coordinador TIC para darselo a la
        /// incidencia que se se va a crear
        /// </summary>
        public profesor coordTIC { get{ return profTIC; } }

        /// <summary>
        /// Lista con todas las incidencia 
        /// </summary>
        public List<incidencia> listIncidencia { get { return inciServ.getAll().ToList(); } }

        /// <summary>
        /// Lista con todos los objetos software
        /// </summary>
        public List<software> listSoftware { get { return softServ.getAll().ToList(); } }

        /// <summary>
        /// Lista con todos los objetos hardware
        /// </summary>
        public List<hardware> listHardware { get { return hardServ.getAll().ToList(); } }

        /// <summary>
        /// Es el objeto que se pasará para editar
        /// </summary>
        public incidencia inciNueva { get { return inci; } set { inci = value; NotifyPropertyChanged(nameof(inciNueva)); } }

        /// <summary>
        /// Permite que los botones de aceptar y cancelar del dialogo
        /// de incidencias se cambien de columna dependiendo de lo que
        /// se vaya a hacer
        /// </summary>
        public int num
        {
            get
            {
                return numColumna;
            }

            set
            {
                numColumna = value;
                NotifyPropertyChanged(nameof(num));
            }
        }

        /// <summary>
        /// Objeto software que recopila los datos del dialogo
        /// incidencias
        /// </summary>
       public software softNuevo { get { return soft; } set { soft = value; NotifyPropertyChanged(nameof(softNuevo)); } }

        /// <summary>
        /// Objeto hardware que recopila los datos del dialogo
        /// incidencias
        /// </summary>
        public hardware hardNuevo { get { return hard; } set { hard = value;NotifyPropertyChanged(nameof(hardNuevo)); } }

        /// <summary>
        /// Permite guardar una incidencia en la base de datos
        /// </summary>
        public bool guarda { get { return add(inciNueva); } }

        /// <summary>
        /// Permite editar una incidencia en la base de datos
        /// </summary>
        public bool edita { get { return update(inciNueva); } }

        /// <summary>
        /// Permite borrar una incidencia en la base de datos
        /// </summary>
        public bool borrar { get { return delete(inciNueva); } }
    } 
}
