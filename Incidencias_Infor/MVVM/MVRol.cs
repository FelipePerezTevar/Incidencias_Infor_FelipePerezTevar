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
        public List<permiso> listPermi;
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
            listPermi = new List<permiso>();
            rol = new rol();
            listPermiRol = new List<permiso>();
            permisoRol = new permiso();
            permisoOri = new permiso();
        }

        public void llenarListaPermiso()
        {
            ListPermiso = permiServ.getAll().ToList();
        }

        public List<rol> listRoles { get { return rolServ.getAll().ToList(); } }
        public rol rolSel { get { return rol; } set { rol = value; NotifyPropertyChanged(nameof(rolSel)); } }
        public permiso permiRol { get { return permisoRol; } set { permisoRol = value; NotifyPropertyChanged(nameof(permiRol)); } }
        public permiso permiOri { get { return permisoOri; } set { permisoOri = value; NotifyPropertyChanged(nameof(permisoOri)); } }
        public List<permiso> ListRolPermiso { get { return listPermiRol; } set { listPermiRol = value; NotifyPropertyChanged(nameof(ListRolPermiso)); } }
        public List<permiso> ListPermiso
        {
            get { return listPermi; }
            set { listPermi = value; NotifyPropertyChanged(nameof(ListPermiso)); }
        }

        public bool guarda { get { return update(rolSel); } }
    }
}
