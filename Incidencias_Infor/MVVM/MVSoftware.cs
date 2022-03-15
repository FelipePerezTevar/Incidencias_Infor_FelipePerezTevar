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

        public List<software> ListWare { get { return softServ.getAll().ToList(); } }

        public software wareNuevo { get { return soft; } set { soft = value; NotifyPropertyChanged(nameof(wareNuevo)); } }

        public bool guarda { get { return add(wareNuevo); } }
        public bool edita { get { return update(wareNuevo); } }
        public bool borrar { get { return delete(wareNuevo); } }
    }
}
