using System;
using System.Collections.Generic;

using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace Histaff_Services
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
  //      static void Main()
  //      {
            

  //          ServiceBase[] ServicesToRun;
  //          ServicesToRun = new ServiceBase[] 
  //          { 
  //              new Stockz_Service()
  //          };
  ////       System.Diagnostics.Debugger.Launch();
  //       ServiceBase.Run( ServicesToRun);
  //      }
        [STAThread]
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            if (args.Length == 1 && args[0] == "GUI")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DebugForm());
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new HiStaff_Service()
                };
                //System.Diagnostics.Debugger.Launch();
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
