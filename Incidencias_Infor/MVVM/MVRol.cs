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

        /// <summary>
        /// Inicializa todo lo necesario para 
        /// conectar correctamente el modelo con la vista
        /// </summary>
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

        /// <summary>
        /// Llena la lista del listbox de edición con
        /// todos los permisos que hay
        /// </summary>
        public void llenarListaPermiso()
        {
            ListPermiso = permiServ.getAll().ToList();
        }

        /// <summary>
        /// Lista con todos los roles que hay
        /// </summary>
        public List<rol> listRoles { get { return rolServ.getAll().ToList(); } }

        /// <summary>
        /// Es el objeto seleccionado del combo con los roles
        /// </summary>
        public rol rolSel { get { return rol; } set { rol = value; NotifyPropertyChanged(nameof(rolSel)); } }

        /// <summary>
        /// Es el objeto seleccionado del listbox con los permisos del rol
        /// </summary>
        public permiso permiRol { get { return permisoRol; } set { permisoRol = value; NotifyPropertyChanged(nameof(permiRol)); } }

        /// <summary>
        /// Es el objeto seleccionado del listbox con los permisos que no tiene el rol
        /// </summary>
        public permiso permiOri { get { return permisoOri; } set { permisoOri = value; NotifyPropertyChanged(nameof(permisoOri)); } }

        /// <summary>
        /// Es la lista con los permisos que tiene el rol
        /// </summary>
        public List<permiso> ListRolPermiso { get { return listPermiRol; } set { listPermiRol = value; NotifyPropertyChanged(nameof(ListRolPermiso)); } }

        /// <summary>
        /// Es la lista con los permisos que no tiene el rol
        /// </summary>
        public List<permiso> ListPermiso
        {
            get { return listPermi; }
            set { listPermi = value; NotifyPropertyChanged(nameof(ListPermiso)); }
        }

        /// <summary>
        /// Permite la edicion del rol
        /// </summary>
        public bool editar { get { return update(rolSel); } }
    }
}
