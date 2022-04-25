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
    class MVHardware : MVBaseCRUD<hardware>
    {
        private incidenciasEntities inciEnt;
        private HardwareServicio hardServ;
        private TipoHWServicio tipoServ;
        private EstadoServicio estServ;
        private IncidenciaServicio inciServ;
        private hardware hard;

        private ListCollectionView lista;
        private DateTime fechaInicio;
        private DateTime fechaFinal;
        private tipohw tipo;
        private profesor prof;
       // private estado Resolucion;
        private List<Predicate<hardware>> criterios;
        private Predicate<hardware> criterioFecha;
        private Predicate<hardware> criterioTipo;
        private Predicate<hardware> criterioProf;
        private Predicate<hardware> criterioRespons;
       // private Predicate<hardware> criterioResol;

        public MVHardware(incidenciasEntities ent)
        {
            inciEnt = ent;
            inicializa();

            servicio = hardServ;
        }

        private void inicializa()
        {
            hardServ = new HardwareServicio(inciEnt);
            tipoServ = new TipoHWServicio(inciEnt);
            inciServ = new IncidenciaServicio(inciEnt);
            estServ = new EstadoServicio(inciEnt);
            fechaInicio = inciServ.getFechaInicio();
            fechaFinal = inciServ.getFechaFinal();
            //tipo = new tipohw();
            //prof = new profesor();
            //Resolucion = new estado();
            criterios = new List<Predicate<hardware>>();
            lista = new ListCollectionView(hardServ.getAll().ToList());
            criterioFecha = new Predicate<hardware>(m => m.incidencia1.fecha_introduccion >= inicioSeleccionado
            && m.incidencia1.fecha_introduccion <= finalSeleccionado);
            criterioTipo = new Predicate<hardware>(m => m.tipohw != null && m.tipohw.Equals(tipoSeleccionado));
            //criterioResol = new Predicate<hardware>(m => m.incidencia1.estado1.nombre != null && m.incidencia1.estado1.nombre.Equals(estadoSeleccionado));
            criterioProf = new Predicate<hardware>(m =>  m.incidencia1.profesor1 != null && m.incidencia1.profesor1.dni.Equals(profUsuario));
            criterioRespons = new Predicate<hardware>(m => m.incidencia1.profesor2 != null && m.incidencia1.profesor2.dni.Equals(profUsuario));
            hard = new hardware();
        }

        public List<tipohw> listTipo { get { return tipoServ.getAll().ToList(); } }
        public List<hardware> ListWare { get { return hardServ.getAll().ToList(); } }
        public List<estado> listEstado { get { return estServ.getAll().ToList(); } }
        public hardware wareNuevo { get { return hard; } set { hard = value;NotifyPropertyChanged(nameof(wareNuevo));} }

        public DateTime inicioSeleccionado { get { return fechaInicio; } set { fechaInicio = value; NotifyPropertyChanged(nameof(inicioSeleccionado)); } }
        public DateTime finalSeleccionado { get { return fechaFinal; } set { fechaFinal = value; NotifyPropertyChanged(nameof(finalSeleccionado)); } }
        public tipohw tipoSeleccionado { get { return tipo; } set { tipo = value; NotifyPropertyChanged(nameof(wareNuevo)); } }
       // public estado estadoSeleccionado { get { return Resolucion;} set { Resolucion = value; NotifyPropertyChanged(nameof(wareNuevo)); } }
       public profesor profUsuario { get { return prof; } set { prof = value;NotifyPropertyChanged(nameof(wareNuevo)); } }
       

        public ListCollectionView ListWare2 { get { return lista; } }

        public bool guarda { get { return add(wareNuevo); } }
        public bool edita { get { return update(wareNuevo); } }
        public bool borrar { get{ return delete(wareNuevo); } }

       

        public void addCriterios()
        {
            criterios.Clear();

            if(inicioSeleccionado != null && finalSeleccionado != null)
            {
                criterios.Add(criterioFecha);
            }

            if(tipoSeleccionado != null)
            {
                criterios.Add(criterioTipo);
            }

            if(profUsuario != null)
            {
                criterios.Add(criterioProf);
                criterios.Add(criterioRespons);
            }

           /* if (estadoSeleccionado != null)
            {
                criterios.Add(criterioResol);
            }*/
        }

        public bool filtroCombinadoCriterios(object item)
        {
            bool correcto = true;
            hardware hard = (hardware)item;

            if(criterios.Count != 0)
            {
                correcto = criterios.TrueForAll(x => x(hard));
            }

            return correcto;
        }
    }
}
