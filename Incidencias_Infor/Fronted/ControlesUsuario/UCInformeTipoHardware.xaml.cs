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
    /// Lógica de interacción para UCInformeTipoHardware.xaml
    /// </summary>
    public partial class UCInformeTipoHardware : UserControl
    {
        public UCInformeTipoHardware()
        {
            InitializeComponent();
            cargarInforme();
        }
        private void cargarInforme()
        {
            try
            {

                ReportDocument rd = new ReportDocument();
                
                rd.Load("../../Informes/CRTipoHardware.rpt");

                ServicioSQL sqlServ = new ServicioSQL();
                rd.SetDataSource(sqlServ.getDatos("select count(*), t.nombre from incidencia i" +
                                                  " inner join hardware h on i.codigo = h.incidencia" +
                                                  " inner join tipohw t on h.tipo = t.codigo" +
                                                  " group by t.nombre; ", null));

                crvTipoHw.ViewerCore.ReportSource = rd;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.InnerException);
            }
        }
    }
}
