using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Incidencias_Infor.MVVM
{
    class MVProfesor : MVBaseCRUD<profesor>
    {

        private incidencias_informaticasEntities inciEnt;
        private ProfesorServicio profServ;
        private profesor prof;

        public MVProfesor(incidencias_informaticasEntities ent)
        {
            inciEnt = ent;
            inicializa();
        }

        /// <summary>
        /// Inicializa todo lo necesario para 
        /// la correcta conexión del modelo con la vista
        /// </summary>
        private void inicializa()
        {
            profServ = new ProfesorServicio(inciEnt);
            servicio = profServ;
            prof = new profesor();
        }

        /// <summary>
        /// Es el profesor registrado que recibirá la nueva contraseña
        /// </summary>
        public profesor profe { get { return prof; } set { prof = value; NotifyPropertyChanged(nameof(profe)); } }

        /// <summary>
        /// Permite editar a un profesor de la base de datos
        /// </summary>
        public bool edita { get { return update(profe); } }

        /// <summary>
        /// Coge el objeto profesor y le actualiza el campo contraseña
        /// </summary>
        /// <param name="pass">La nueva contraseña</param>
        public void setPassword(string pass)
        {
            profe.contrasenya = pass;

        }

        /// <summary>
        /// Recibe una contraseña y la cifra en sha
        /// </summary>
        /// <param name="pass">La nueva contraseña</param>
        /// <returns>Devuelve la nueva contraseña cifrada en SHA</returns>
        public string cifrarContraseña(string pass)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(pass));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }
}
