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

        

        public hardware hardNuevo { get { return hard; } set { hard = value;NotifyPropertyChanged(nameof(hardNuevo));} }

        public bool guarda { get { return add(hardNuevo); } }
        public bool edita { get { return update(hardNuevo); } }
        public bool borrar { get{ return delete(hardNuevo); } }
    }
}
