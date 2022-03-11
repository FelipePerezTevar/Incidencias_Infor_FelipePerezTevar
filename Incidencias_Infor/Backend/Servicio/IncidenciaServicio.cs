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

        public List<incidencia> getIncidenciasProf(profesor prof)
        {
            
            return null;
        }
    }
}
