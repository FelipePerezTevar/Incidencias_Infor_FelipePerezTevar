using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.MVVM
{
    class MVRol : MVBaseCRUD<rol>
    {
        private incidencias_informaticasEntities inciEnt;
        private RolServicio rolServ;
        private PermisoServicio permiServ;
        public List<permiso> listPermiRol;
        public rol rol;
        public permiso permisoRol;
        public permiso permisoOri;

        public MVRol(incidencias_informaticasEntities ent)
        {
            inciEnt = ent;
            inicializa();
            servicio = rolServ;
        }

        private void inicializa()
        {
            rolServ = new RolServicio(inciEnt);
            permiServ = new PermisoServicio(inciEnt);
            
            rol = new rol();
            listPermiRol = new List<permiso>();
            permisoRol = new permiso();
            permisoOri = new permiso();
        }

        public List<rol> listRoles { get { return rolServ.getAll().ToList(); } }
        public rol rolSel { get { return rol; } set { rol = value; NotifyPropertyChanged(nameof(rolSel)); } }
        public List<permiso> ListRolPermiso { get { return listPermiRol; } set { listPermiRol = value;  NotifyPropertyChanged(nameof(ListRolPermiso)); } }
        public List<permiso> listPermiso {  get { return permiServ.getAll().ToList(); } }
    }
}
