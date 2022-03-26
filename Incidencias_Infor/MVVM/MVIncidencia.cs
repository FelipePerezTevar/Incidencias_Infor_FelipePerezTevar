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
        private incidenciasEntities inciEnt;
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

        public MVIncidencia(incidenciasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = inciServ;
        }

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

        public List<tipohw> listTipo { get { return tipoServ.getAll().ToList(); } }
        public List<lugar> listLugar { get { return luServ.getAll().ToList(); } }
        public List<estado> listEstado { get { return estServ.getAll().ToList(); } }
        
        public List<profesor> listProfesor { get { return profServ.getAll().ToList(); } }

        public estado estadoProf { get { return estadoEnSolucion; } }

        public permiso bloqueProf { get { return limiteProfesor; } }

        public profesor coordTIC { get{ return profTIC; } }

        public List<incidencia> listIncidencia { get { return inciServ.getAll().ToList(); } }

        public List<software> listSoftware { get { return softServ.getAll().ToList(); } }

        public List<hardware> listHardware { get { return hardServ.getAll().ToList(); } }

        public incidencia inciNueva { get { return inci; } set { inci = value; NotifyPropertyChanged(nameof(inciNueva)); } }

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

       public software softNuevo { get { return soft; } set { soft = value; NotifyPropertyChanged(nameof(softNuevo)); } }

        public hardware hardNuevo { get { return hard; } set { hard = value;NotifyPropertyChanged(nameof(hardNuevo)); } }

        public bool guarda { get { return add(inciNueva); } }
        public bool edita { get { return update(inciNueva); } }

        public bool borrar { get { return delete(inciNueva); } }
    } 
}
