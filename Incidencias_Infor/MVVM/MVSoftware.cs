using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.MVVM
{
    class MVSoftware : MVBaseCRUD<software>
    {
        
        private SoftwareServicio softServ;
        private incidenciasEntities inciEnt;
        private software soft;

        public MVSoftware(incidenciasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = softServ;
        }

        private void inicializa()
        {
            softServ = new SoftwareServicio(inciEnt);
            soft = new software();
        }

        public software softNuevo { get { return soft; } set { soft = value; NotifyPropertyChanged(nameof(softNuevo)); } }

        public bool guarda { get { return add(softNuevo); } }
        public bool edita { get { return update(softNuevo); } }
        public bool borrar { get { return delete(softNuevo); } }
    }
}
