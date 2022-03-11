//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Incidencias_Infor.Backend.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class incidencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public incidencia()
        {
            this.hardware = new HashSet<hardware>();
            this.software = new HashSet<software>();
        }
    
        public int codigo { get; set; }
        public Nullable<System.DateTime> fecha_inicial { get; set; }
        public Nullable<System.DateTime> fecha_introduccion { get; set; }
        public Nullable<System.DateTime> fecha_resolucion { get; set; }
        public Nullable<short> comunicado { get; set; }
        public Nullable<System.TimeSpan> tiempo { get; set; }
        public string informacion { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public int lugar { get; set; }
        public int estado { get; set; }
        public int profesor { get; set; }
        public Nullable<int> responsable { get; set; }
    
        public virtual estado estado1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hardware> hardware { get; set; }
        public virtual lugar lugar1 { get; set; }
        public virtual profesor profesor1 { get; set; }
        public virtual profesor profesor2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<software> software { get; set; }
    }
}
