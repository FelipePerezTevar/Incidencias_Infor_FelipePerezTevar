using CrystalDecisions.CrystalReports.Engine;
using Incidencias_Infor.Backend.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Incidencias_Infor.Fronted.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCInformeDepartamento.xaml
    /// </summary>
    public partial class UCInformeDepartamento : UserControl
    {
        public UCInformeDepartamento()
        {
            InitializeComponent();
            cargarInforme();
        }

        /// <summary>
        /// Carga el gráfico de cuantas incidencias ha habido
        /// por deparatamento
        /// </summary>
        private void cargarInforme()
        {
            try
            {

                ReportDocument rd = new ReportDocument();
                rd.Load("../../Informes/CRDepartamento.rpt");

                ServicioSQL sqlServ = new ServicioSQL();
                rd.SetDataSource(sqlServ.getDatos("select count(*), d.nombre from incidencia i"+
                                                  " inner join lugar l on i.lugar = l.codigo"+
                                                  " inner join departamento d on l.departamento = d.codigo"+
                                                  " group by d.nombre; ", null));

                crvDpto.ViewerCore.ReportSource = rd;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.InnerException);
            }
        }
    }
}
