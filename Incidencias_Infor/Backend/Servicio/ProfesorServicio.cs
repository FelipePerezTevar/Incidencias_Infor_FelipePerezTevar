using Incidencias_Infor.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.Backend.Servicio
{
    public class ProfesorServicio: ServicioGenerico<profesor>
    {
        private DbContext contexto;
        public profesor profLogin { get; set; }

        public ProfesorServicio (DbContext context): base(context)
        {
            contexto = context;
        }

        public Boolean login(string user, String pass)
        {
            Boolean correcto = true;
            try
            {

                profLogin = contexto.Set<profesor>().Where(p => p.email == user).FirstOrDefault();
                
            }catch(Exception e) {
                System.Console.WriteLine(e.StackTrace);
            }
                
            if(profLogin == null)
            {
                correcto = false;
            }else if(!profLogin.email.Equals(user) || !profLogin.contrasenya.Equals(pass))
            {
                correcto = false;
            }

            return correcto;
        }

        public profesor getCoordTIC()
        {
            profesor prof = contexto.Set<profesor>().Where(p => p.rol1.nombre == "Coordinador TIC").FirstOrDefault();
            return prof; 
        }
    }
}
