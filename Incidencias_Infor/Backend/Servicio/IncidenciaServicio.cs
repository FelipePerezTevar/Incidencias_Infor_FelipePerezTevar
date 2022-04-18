using Incidencias_Infor.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.Backend.Servicio
{
    public class IncidenciaServicio: ServicioGenerico<incidencia>
    {
        private DbContext contexto;

        public IncidenciaServicio(DbContext context): base(context)
        {
            contexto = context;
        }

        public int getLastId()
        {
            incidencia inci = contexto.Set<incidencia>().OrderByDescending(i => i.codigo).FirstOrDefault();
            if(inci == null)
            {
                return 0;
            }
            else
            {
                return inci.codigo;
            }
            
        }

        public hardware getHardware()
        {
            hardware hard = contexto.Set<hardware>().Where(h => h.incidencia1.codigo == 1).FirstOrDefault();
            return hard;
        }

        public DateTime getFechaInicio()
        {
            incidencia inci = contexto.Set<incidencia>().OrderBy(i => i.fecha_introduccion).FirstOrDefault();
            return (DateTime)inci.fecha_introduccion;
        }

        public DateTime getFechaFinal()
        {
            incidencia inci = contexto.Set<incidencia>().OrderByDescending(i => i.fecha_introduccion).FirstOrDefault();
            return (DateTime)inci.fecha_introduccion;
        }
    }
}
