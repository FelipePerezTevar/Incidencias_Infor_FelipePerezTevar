using Incidencias_Infor.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.Backend.Servicio
{
    public class HardwareServicio: ServicioGenerico<hardware>
    {
        DbContext contexto;

        public HardwareServicio(DbContext context): base(context)
        {
            contexto = context;
        }

        public int getLastId()
        {
            hardware hard = contexto.Set<hardware>().OrderByDescending(h => h.codigo).FirstOrDefault();
            if(hard == null)
            {
                return 0;
            }
            else
            {
                return hard.codigo;
            }
        }
    }
}
