using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Histaff_Services
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!btnStart.Enabled) return;
            btnStart.Enabled = false;
            HiStaff_Service service = new HiStaff_Service();
            service.OnStartDebug();
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!btnStop.Enabled) return;
            btnStop.Enabled = false;
            HiStaff_Service service = new HiStaff_Service();
            service.OnStopDebug();
            btnStart.Enabled = true;
        }
    }
}
