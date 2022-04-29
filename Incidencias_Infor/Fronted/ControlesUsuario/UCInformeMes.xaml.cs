using CrystalDecisions.CrystalReports.Engine;
using Incidencias_Infor.Backend.Modelo;
using Incidencias_Infor.Backend.Servicio;
using Incidencias_Infor.MVVM;
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
    /// Lógica de interacción para UCInformeMes.xaml
    /// </summary>
    public partial class UCInformeMes : UserControl
    {

       // private incidenciasEntities inciEnt;
       // private MVIncidencia mvInci;

        public UCInformeMes()
        {
            InitializeComponent();
            //inciEnt = ent;
            //mvInci = new MVIncidencia(inciEnt);
            //DataContext = mvInci;
            cargarInforme();
        }

        private void cargarInforme()
        {
            try
            {

                ReportDocument rd = new ReportDocument();
                rd.Load("../../Informes/CRMensual.rpt");

                ServicioSQL sqlServ = new ServicioSQL();
                rd.SetDataSource(sqlServ.getDatos("Select count(*) as NumInci, monthname(fecha_introduccion)" +
                    " from incidencia group by MONTH(fecha_introduccion)", null));

                crvMes.ViewerCore.ReportSource = rd;

            }catch(Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                System.Console.WriteLine(ex.InnerException);
            }
        }
    }
}
