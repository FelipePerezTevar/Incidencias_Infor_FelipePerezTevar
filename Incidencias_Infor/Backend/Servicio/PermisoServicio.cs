using Incidencias_Infor.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.Backend.Servicio
{
    public class PermisoServicio: ServicioGenerico<permiso>
    {

        private DbContext contexto;

        public PermisoServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        public permiso bloqueProfesor()
        {
            permiso per = contexto.Set<permiso>().Where(p => p.codigo == 3).FirstOrDefault();

            return per;
        }
    }
}
