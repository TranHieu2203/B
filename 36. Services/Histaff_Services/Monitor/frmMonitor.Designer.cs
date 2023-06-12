namespace MainForm
{
    partial class frmMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnContinute = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblnumRow = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabOrder = new System.Windows.Forms.TabControl();
            this.tabserviceControl = new System.Windows.Forms.TabPage();
            this.ServiceList = new System.Windows.Forms.ListView();
            this.ServiceName = new System.Windows.Forms.ColumnHeader();
            this.Description = new System.Windows.Forms.ColumnHeader();
            this.STATUS = new System.Windows.Forms.ColumnHeader();
            this.tabNormalOrder = new System.Windows.Forms.TabPage();
            this.griNormalOrder = new System.Windows.Forms.DataGridView();
            this.tabPutOrder = new System.Windows.Forms.TabPage();
            this.griPutOrder = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabOrder.SuspendLayout();
            this.tabserviceControl.SuspendLayout();
            this.tabNormalOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.griNormalOrder)).BeginInit();
            this.tabPutOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.griPutOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.cboType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 62);
            this.panel1.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(413, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(66, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Order Status";
            // 
            // cboType
            // 
            this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboType.AutoCompleteCustomSource.AddRange(new string[] {
            "---------All---------",
            "Orders not sent",
            "Orders sent Success",
            "Orders error",
            "Orders Success "});
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "---------All---------",
            "Orders not sent",
            "Orders sent Success",
            "Orders error",
            "Orders Success "});
            this.cboType.Location = new System.Drawing.Point(486, 22);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(217, 21);
            this.cboType.TabIndex = 0;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.btnContinute);
            this.panel2.Controls.Add(this.btnPause);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnViewAll);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.lblnumRow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 446);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(715, 31);
            this.panel2.TabIndex = 1;
            // 
            // btnContinute
            // 
            this.btnContinute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinute.Location = new System.Drawing.Point(626, -1);
            this.btnContinute.Name = "btnContinute";
            this.btnContinute.Size = new System.Drawing.Size(87, 30);
            this.btnContinute.TabIndex = 3;
            this.btnContinute.Text = "&Continute";
            this.btnContinute.UseVisualStyleBackColor = true;
            this.btnContinute.Click += new System.EventHandler(this.btnContinute_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(534, -1);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(87, 30);
            this.btnPause.TabIndex = 3;
            this.btnPause.Text = "&Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(441, -1);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(87, 30);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "S&top";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(348, -1);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(87, 30);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnViewAll
            // 
            this.btnViewAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAll.Location = new System.Drawing.Point(255, -1);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(87, 30);
            this.btnViewAll.TabIndex = 3;
            this.btnViewAll.Text = "View &All";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(162, -1);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(87, 30);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblnumRow
            // 
            this.lblnumRow.AutoSize = true;
            this.lblnumRow.Location = new System.Drawing.Point(7, 9);
            this.lblnumRow.Name = "lblnumRow";
            this.lblnumRow.Size = new System.Drawing.Size(100, 13);
            this.lblnumRow.TabIndex = 2;
            this.lblnumRow.Text = "Number of Record :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabOrder);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(715, 384);
            this.panel3.TabIndex = 2;
            // 
            // tabOrder
            // 
            this.tabOrder.Controls.Add(this.tabserviceControl);
            this.tabOrder.Controls.Add(this.tabNormalOrder);
            this.tabOrder.Controls.Add(this.tabPutOrder);
            this.tabOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabOrder.Location = new System.Drawing.Point(0, 0);
            this.tabOrder.Name = "tabOrder";
            this.tabOrder.Padding = new System.Drawing.Point(15, 3);
            this.tabOrder.SelectedIndex = 0;
            this.tabOrder.Size = new System.Drawing.Size(715, 384);
            this.tabOrder.TabIndex = 0;
            this.tabOrder.SelectedIndexChanged += new System.EventHandler(this.tabOrder_SelectedIndexChanged);
            // 
            // tabserviceControl
            // 
            this.tabserviceControl.Controls.Add(this.ServiceList);
            this.tabserviceControl.Location = new System.Drawing.Point(4, 29);
            this.tabserviceControl.Name = "tabserviceControl";
            this.tabserviceControl.Size = new System.Drawing.Size(707, 351);
            this.tabserviceControl.TabIndex = 2;
            this.tabserviceControl.Text = "Service Control";
            this.tabserviceControl.UseVisualStyleBackColor = true;
            // 
            // ServiceList
            // 
            this.ServiceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ServiceName,
            this.Description,
            this.STATUS});
            this.ServiceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServiceList.FullRowSelect = true;
            this.ServiceList.GridLines = true;
            this.ServiceList.Location = new System.Drawing.Point(0, 0);
            this.ServiceList.MultiSelect = false;
            this.ServiceList.Name = "ServiceList";
            this.ServiceList.Size = new System.Drawing.Size(707, 351);
            this.ServiceList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ServiceList.TabIndex = 1;
            this.ServiceList.UseCompatibleStateImageBehavior = false;
            this.ServiceList.View = System.Windows.Forms.View.Details;
            this.ServiceList.SelectedIndexChanged += new System.EventHandler(this.ServiceList_SelectedIndexChanged);
            // 
            // ServiceName
            // 
            this.ServiceName.Text = "Service Name";
            this.ServiceName.Width = 222;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 387;
            // 
            // STATUS
            // 
            this.STATUS.Text = "Status";
            this.STATUS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.STATUS.Width = 72;
            // 
            // tabNormalOrder
            // 
            this.tabNormalOrder.Controls.Add(this.griNormalOrder);
            this.tabNormalOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabNormalOrder.Location = new System.Drawing.Point(4, 29);
            this.tabNormalOrder.Name = "tabNormalOrder";
            this.tabNormalOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabNormalOrder.Size = new System.Drawing.Size(707, 351);
            this.tabNormalOrder.TabIndex = 0;
            this.tabNormalOrder.Text = "Normal";
            this.tabNormalOrder.UseVisualStyleBackColor = true;
            // 
            // griNormalOrder
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.griNormalOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.griNormalOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.griNormalOrder.DefaultCellStyle = dataGridViewCellStyle32;
            this.griNormalOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.griNormalOrder.Location = new System.Drawing.Point(3, 3);
            this.griNormalOrder.Name = "griNormalOrder";
            this.griNormalOrder.ReadOnly = true;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.griNormalOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.griNormalOrder.Size = new System.Drawing.Size(701, 345);
            this.griNormalOrder.TabIndex = 0;
            // 
            // tabPutOrder
            // 
            this.tabPutOrder.Controls.Add(this.griPutOrder);
            this.tabPutOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPutOrder.Location = new System.Drawing.Point(4, 29);
            this.tabPutOrder.Name = "tabPutOrder";
            this.tabPutOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPutOrder.Size = new System.Drawing.Size(707, 351);
            this.tabPutOrder.TabIndex = 1;
            this.tabPutOrder.Text = "Putthrough";
            this.tabPutOrder.UseVisualStyleBackColor = true;
            // 
            // griPutOrder
            // 
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.griPutOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.griPutOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.griPutOrder.DefaultCellStyle = dataGridViewCellStyle35;
            this.griPutOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.griPutOrder.Location = new System.Drawing.Point(3, 3);
            this.griPutOrder.Name = "griPutOrder";
            this.griPutOrder.ReadOnly = true;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.griPutOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.griPutOrder.Size = new System.Drawing.Size(701, 345);
            this.griPutOrder.TabIndex = 1;
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(715, 477);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BOSC Transfer Monitor ";
            this.Load += new System.EventHandler(this.frmMonitor_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabOrder.ResumeLayout(false);
            this.tabserviceControl.ResumeLayout(false);
            this.tabNormalOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.griNormalOrder)).EndInit();
            this.tabPutOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.griPutOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabOrder;
        private System.Windows.Forms.TabPage tabNormalOrder;
        private System.Windows.Forms.TabPage tabPutOrder;
        private System.Windows.Forms.DataGridView griNormalOrder;
        private System.Windows.Forms.DataGridView griPutOrder;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblnumRow;
        private System.Windows.Forms.TabPage tabserviceControl;
        private System.Windows.Forms.ListView ServiceList;
        private System.Windows.Forms.ColumnHeader ServiceName;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader STATUS;
        private System.Windows.Forms.Button btnContinute;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button btnView;



    }
}

