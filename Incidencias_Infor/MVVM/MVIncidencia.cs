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
        private ArticuloServicio artiServ;
        private ProfesorServicio profServ;
        private HardwareServicio hardServ;
        private SoftwareServicio softServ;
        private PermisoServicio permServ;
        private profesor profTIC;
        private estado estadoEnSolucion;
        private permiso limiteProfesor;
        private incidencia inci;

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
            artiServ = new ArticuloServicio(inciEnt);
            profServ = new ProfesorServicio(inciEnt);
            inciServ = new IncidenciaServicio(inciEnt);
            hardServ = new HardwareServicio(inciEnt);
            softServ = new SoftwareServicio(inciEnt);
            permServ = new PermisoServicio(inciEnt);
            inci = new incidencia();
            estadoEnSolucion = estServ.getEnSolucion();
            limiteProfesor = permServ.bloqueProfesor();
            profTIC = profServ.getCoordTIC();
            inci.codigo = inciServ.getLastId() + 1;
        }

        public List<lugar> listLugar { get { return luServ.getAll().ToList(); } }
        public List<estado> listEstado { get { return estServ.getAll().ToList(); } }
        public List<articulo> listArticulo { get { return artiServ.getAll().ToList(); } }
        public List<profesor> listProfesor { get { return profServ.getAll().ToList(); } }

        public estado estadoProf { get { return estadoEnSolucion; } }

        public permiso bloqueProf { get { return limiteProfesor; } }

        public profesor coordTIC { get{ return profTIC; } }

        public List<incidencia> listIncidencia { get { return inciServ.getAll().ToList(); } }

        public List<software> listSoftware { get { return softServ.getAll().ToList(); } }

        public List<hardware> listHardware { get { return hardServ.getAll().ToList(); } }

        public incidencia inciNueva { get { return inci; } set { inci = value; NotifyPropertyChanged(nameof(inciNueva)); } }

        public bool guarda { get { return add(inciNueva); } }
        public bool edita { get { return update(inciNueva); } }

        public bool borrar { get { return delete(inciNueva); } }
    } 
}
