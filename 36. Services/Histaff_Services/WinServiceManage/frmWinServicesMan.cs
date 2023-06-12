
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data; 
using System.ServiceProcess; 

namespace WinServiceManage
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmWinServicesMan : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView ServiceList;
		private System.Windows.Forms.ColumnHeader Description;
		private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader ServiceName;
		private System.Windows.Forms.Button StopBtn;
		private System.Windows.Forms.Button StartBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmWinServicesMan()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.ServiceName = new System.Windows.Forms.ColumnHeader();
            this.ServiceList = new System.Windows.Forms.ListView();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 214;
            // 
            // ServiceName
            // 
            this.ServiceName.Text = "Service Name";
            this.ServiceName.Width = 126;
            // 
            // ServiceList
            // 
            this.ServiceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ServiceName,
            this.Description,
            this.Status});
            this.ServiceList.FullRowSelect = true;
            this.ServiceList.GridLines = true;
            this.ServiceList.Location = new System.Drawing.Point(8, 12);
            this.ServiceList.MultiSelect = false;
            this.ServiceList.Name = "ServiceList";
            this.ServiceList.Size = new System.Drawing.Size(416, 348);
            this.ServiceList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ServiceList.TabIndex = 0;
            this.ServiceList.UseCompatibleStateImageBehavior = false;
            this.ServiceList.View = System.Windows.Forms.View.Details;
            this.ServiceList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServiceList_KeyUp);
            this.ServiceList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServiceList_KeyDown);
            this.ServiceList.Click += new System.EventHandler(this.SetProperties);
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 71;
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(272, 400);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 4;
            this.StopBtn.Text = "Stop";
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(192, 400);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 5;
            this.StartBtn.Text = "Start";
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 8);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(352, 400);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View All";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // frmWinServicesMan
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(432, 429);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.ServiceList);
            this.MaximizeBox = false;
            this.Name = "frmWinServicesMan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Window Service Management Application";
            this.Load += new System.EventHandler(this.frmWinServicesMan_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmWinServicesMan());
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			try
			{
				ListViewItem datalist;														
				ServiceList.Items.Clear();  
				foreach(ServiceController service in services)
				{										
					datalist = new System.Windows.Forms.ListViewItem(service.ServiceName.ToString());  					  				    
					datalist.SubItems.Add(service.DisplayName);    
					datalist.SubItems.Add(service.Status.ToString());					
					ServiceList.Items.Add(datalist);								    
				}				

			}
			catch(Exception er)
			{
				MessageBox.Show(er.Message,"Error Exception",MessageBoxButtons.OK, MessageBoxIcon.Error); 				
			}

		}		

		public void ListChange()
		{
			if(ServiceList.SelectedItems.Count!=0)
			{
				ListViewItem item = ServiceList.FocusedItem;
				if(item!=null)
				{
					if(item.SubItems[2].Text=="Running")
					{
						StartBtn.Enabled = false;
						StopBtn.Enabled = true;
					}
					else
					{
						StartBtn.Enabled = true;
						StopBtn.Enabled = false;
					}
				}									
			}
		}

		private void ServiceList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			 ListChange();
		}

		private void ServiceList_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			 ListChange();
		}

		private void SetProperties(object sender, System.EventArgs e)
		{
			ListChange();
		}

		private void StopBtn_Click(object sender, System.EventArgs e)
		{

			try
			{
				if(ServiceList.SelectedItems.Count!=0)
				{
                    //int n = ServiceList.FocusedItem.Index;  
                    //if(services[n].Status ==ServiceControllerStatus.Running)
                    //{
                    //    services[n].Stop();
                    //    StartBtn.Enabled = true;
                    //    StopBtn.Enabled = false;
                    //    ServiceList.FocusedItem.SubItems[2].Text = "Stopped";  
                    //}
                    //else
                    //{
                    //    MessageBox.Show("This service couldn't be stopped","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //} 	
                    if (bosc2service.Status == ServiceControllerStatus.Running)
                    {
                        bosc2service.Stop();
                        StartBtn.Enabled = true;
                        StopBtn.Enabled = false;
                        ServiceList.FocusedItem.SubItems[2].Text = "Stopped";  
                    }
			       
				}
				else
				{
					MessageBox.Show("Please,choose a service that you want to stop","Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch(Exception er)
			{
				MessageBox.Show(er.Message,"Error Exception",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void StartBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ServiceList.SelectedItems.Count!=0)
				{
					int n = ServiceList.FocusedItem.Index;  					
					//services[n].Start();
                    bosc2service.Start();
					StartBtn.Enabled = false;
					StopBtn.Enabled = true;
					ServiceList.FocusedItem.SubItems[2].Text = "Running";  					 				
				}
				else
				{
					MessageBox.Show("Please,choose a service that you want to stop","Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch(Exception er)
			{
				MessageBox.Show(er.Message,"Error Exception",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public ServiceController[] services;
        public ServiceController bosc2service;

        private void frmWinServicesMan_Load(object sender, EventArgs e)
        {
            services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == "Histaff_Services")
                {
                    bosc2service = service;
                    ListViewItem datalist;
                    ServiceList.Items.Clear();
                    datalist = new System.Windows.Forms.ListViewItem(service.ServiceName.ToString());
                    datalist.SubItems.Add(service.DisplayName);
                    datalist.SubItems.Add(service.Status.ToString());
                    ServiceList.Items.Add(datalist);
                }
            }
            
        }
	}
}
