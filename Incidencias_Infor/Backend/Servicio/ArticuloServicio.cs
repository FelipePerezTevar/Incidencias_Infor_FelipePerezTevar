﻿using Incidencias_Infor.Backend.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias_Infor.Backend.Servicio
{
    public class ArticuloServicio: ServicioGenerico<articulo>
    {
        public ArticuloServicio(DbContext context): base(context)
        {

        }
    }
}
