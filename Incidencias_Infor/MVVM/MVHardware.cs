using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.MVVM
{
    class MVHardware : MVBaseCRUD<hardware>
    {
        private incidenciasEntities inciEnt;
        private HardwareServicio hardServ;
        private hardware hard;

        public MVHardware(incidenciasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = hardServ;
        }

        private void inicializa()
        {
            hardServ = new HardwareServicio(inciEnt);
            hard = new hardware();
        }

        
        public List<hardware> ListWare { get { return hardServ.getAll().ToList(); } }
        public hardware wareNuevo { get { return hard; } set { hard = value;NotifyPropertyChanged(nameof(wareNuevo));} }

        public bool guarda { get { return add(wareNuevo); } }
        public bool edita { get { return update(wareNuevo); } }
        public bool borrar { get{ return delete(wareNuevo); } }
    }
}
