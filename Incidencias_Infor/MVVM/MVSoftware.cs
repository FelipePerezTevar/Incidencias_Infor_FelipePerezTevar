using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Incidencias_Infor.MVVM
{
    class MVSoftware : MVBaseCRUD<software>
    {
        
        private SoftwareServicio softServ;
        private IncidenciaServicio inciServ;
        private incidenciasEntities inciEnt;
        private EstadoServicio estServ;
        private software soft;
        private string _textoCheck;

        private ListCollectionView listaWare;
        private DateTime fechaInicio;
        private DateTime fechaFinal;
        //private estado Estado;
        private profesor prof;

        private List<Predicate<software>> criterios;
        private Predicate<software> criteriosFecha;
        private Predicate<software> criterioProf;
        private Predicate<software> criterioRespons;
        //private Predicate<software> criteriosEstado;

        public MVSoftware(incidenciasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = softServ;
        }

        private void inicializa()
        {
            softServ = new SoftwareServicio(inciEnt);
            inciServ = new IncidenciaServicio(inciEnt);
            estServ = new EstadoServicio(inciEnt);
            soft = new software();
            //Estado = new estado();
            fechaInicio = inciServ.getFechaInicio();
            fechaFinal = inciServ.getFechaFinal();
            criterios = new List<Predicate<software>>();
            listaWare = new ListCollectionView(softServ.getAll().ToList());
            criteriosFecha = new Predicate<software>(m => m.incidencia1.fecha_introduccion >= inicioSeleccionado
            && m.incidencia1.fecha_introduccion <= finalSeleccionado);
            // criteriosEstado = new Predicate<software>(m => m.incidencia1.estado1.nombre != null && m.incidencia1.estado1.nombre.Equals(estadoSeleccionado)); 
            criterioProf = new Predicate<software>(m => m.incidencia1.profesor1 != null && m.incidencia1.profesor1.dni.Equals(profUsuario));
            criterioRespons = new Predicate<software>(m => m.incidencia1.profesor2 != null && m.incidencia1.profesor2.dni.Equals(profUsuario));
        }

        public List<software> ListWare { get { return softServ.getAll().ToList(); } }

        public List<estado> listEstado { get { return estServ.getAll().ToList(); } }

        public string textoCheck { get {return _textoCheck; } set { _textoCheck = value; NotifyPropertyChanged(nameof(textoCheck) );  } }

        public software wareNuevo { get { return soft; } set { soft = value; NotifyPropertyChanged(nameof(wareNuevo)); } }

        public DateTime inicioSeleccionado { get { return fechaInicio; } set { fechaInicio = value; NotifyPropertyChanged(nameof(inicioSeleccionado));} }
        public DateTime finalSeleccionado { get { return fechaFinal; } set { fechaFinal = value; NotifyPropertyChanged(nameof(finalSeleccionado)); } }
        // public estado estadoSeleccionado { get { return Estado; } set { Estado = value; NotifyPropertyChanged(nameof(wareNuevo)); } }
        public profesor profUsuario { get { return prof; } set { prof = value; NotifyPropertyChanged(nameof(wareNuevo)); } }

        public ListCollectionView ListWare2 { get { return listaWare; } }

        public bool guarda { get { return add(wareNuevo); } }
        public bool edita { get { return update(wareNuevo); } }
        public bool borrar { get { return delete(wareNuevo); } }

       

        public void addCriterios()
        {
            criterios.Clear();

            if (inicioSeleccionado != null && finalSeleccionado != null)
            {
                criterios.Add(criteriosFecha);
            }

            if (profUsuario != null)
            {
                criterios.Add(criterioProf);
                criterios.Add(criterioRespons);
            }

            /*if(Estado != null)
            {
                criterios.Add(criteriosEstado);
            }*/
        }

        public bool filtroCombinadoCriterios(object item)
        {
            bool correcto = true;
            software soft = (software)item;

            if (criterios.Count != 0)
            {
                correcto = criterios.TrueForAll(x => x(soft));
            }

            return correcto;
        }

        
    }
}
