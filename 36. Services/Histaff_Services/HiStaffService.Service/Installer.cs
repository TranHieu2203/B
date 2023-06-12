using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;

namespace Histaff_Services
{
    [RunInstallerAttribute(true)]
    public class Histaff_Services_Installer: System.Configuration.Install.Installer
    {
        public Histaff_Services_Installer()
        {
            ServiceInstaller si = new ServiceInstaller();
            ServiceProcessInstaller spi = new ServiceProcessInstaller();
            si.ServiceName = "Histaff_Services_TMF";
            si.DisplayName = "Histaff_Services_TMF";
            si.Description = "Histaff_Services_TMF";
            si.StartType = ServiceStartMode.Automatic;
            this.Installers.Add(si);
            spi.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            spi.Username = null;
            spi.Password = null;
            this.Installers.Add(spi);
        }
    }
}
