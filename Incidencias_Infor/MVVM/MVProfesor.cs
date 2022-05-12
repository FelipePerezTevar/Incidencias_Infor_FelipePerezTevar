using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.MVVM
{
    class MVProfesor : MVBaseCRUD<profesor>
    {

        private incidenciasEntities inciEnt;
        private ProfesorServicio profServ;
        private profesor prof;

        public MVProfesor(incidenciasEntities ent)
        {
            inciEnt = ent;
            inicializa();
        }

        private void inicializa()
        {
            profServ = new ProfesorServicio(inciEnt);
            servicio = profServ;
            prof = new profesor();
        }

        public profesor profe { get { return prof; } set { prof = value; NotifyPropertyChanged(nameof(profe)); } }

        public bool edita { get { return update(profe); } }

        public void setPassword(string pass)
        {
            profe.contrasenya = pass;
        }

    }
}
