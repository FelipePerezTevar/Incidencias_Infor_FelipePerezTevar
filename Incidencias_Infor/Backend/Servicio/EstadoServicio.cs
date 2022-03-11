using Incidencias_Infor.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.Backend.Servicio
{
    public class EstadoServicio: ServicioGenerico<estado>
    {
        private DbContext contexto;
        public EstadoServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        public estado getEnSolucion()
        {
           estado est = contexto.Set<estado>().Where(p => p.codigo == 1).FirstOrDefault();

            return est; 
        }
    }
}
