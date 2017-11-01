namespace BlankSpider.App.CONTROLS
{
    partial class ListSources
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvProject = new System.Windows.Forms.ListView();
            this.STT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SOURCE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MODE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.STATUS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LINK_SUB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LINK_DETAIL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NEW_RECORD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuControlSource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnProjectStart = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProjectStop = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUploadSource = new System.Windows.Forms.Timer(this.components);
            this.UPDATED_RECORD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuControlSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvProject
            // 
            this.lvProject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvProject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.STT,
            this.SOURCE,
            this.MODE,
            this.STATUS,
            this.LINK_SUB,
            this.LINK_DETAIL,
            this.NEW_RECORD,
            this.UPDATED_RECORD});
            this.lvProject.ContextMenuStrip = this.contextMenuControlSource;
            this.lvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lvProject.FullRowSelect = true;
            this.lvProject.GridLines = true;
            this.lvProject.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvProject.HideSelection = false;
            this.lvProject.Location = new System.Drawing.Point(0, 0);
            this.lvProject.Name = "lvProject";
            this.lvProject.Size = new System.Drawing.Size(634, 618);
            this.lvProject.TabIndex = 1;
            this.lvProject.UseCompatibleStateImageBehavior = false;
            this.lvProject.View = System.Windows.Forms.View.Details;
            this.lvProject.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvProject_ItemSelectionChanged);
            // 
            // STT
            // 
            this.STT.Text = "#";
            this.STT.Width = 40;
            // 
            // SOURCE
            // 
            this.SOURCE.Text = "SOURCE";
            this.SOURCE.Width = 125;
            // 
            // MODE
            // 
            this.MODE.Text = "MODE";
            this.MODE.Width = 80;
            // 
            // STATUS
            // 
            this.STATUS.Text = "STATUS";
            this.STATUS.Width = 100;
            // 
            // LINK_SUB
            // 
            this.LINK_SUB.Text = "SUB";
            this.LINK_SUB.Width = 80;
            // 
            // LINK_DETAIL
            // 
            this.LINK_DETAIL.Text = "DETAIL";
            this.LINK_DETAIL.Width = 80;
            // 
            // NEW_RECORD
            // 
            this.NEW_RECORD.Text = "NEW";
            // 
            // contextMenuControlSource
            // 
            this.contextMenuControlSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.contextMenuControlSource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProjectStart,
            this.btnProjectStop});
            this.contextMenuControlSource.Name = "contextMenuStrip1";
            this.contextMenuControlSource.Size = new System.Drawing.Size(113, 48);
            this.contextMenuControlSource.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuControlSource_Opening);
            // 
            // btnProjectStart
            // 
            this.btnProjectStart.Image = global::BlankSpider.App.Properties.Resources.start_32;
            this.btnProjectStart.Name = "btnProjectStart";
            this.btnProjectStart.Size = new System.Drawing.Size(112, 22);
            this.btnProjectStart.Text = "START";
            this.btnProjectStart.Click += new System.EventHandler(this.btnProjectStart_Click);
            // 
            // btnProjectStop
            // 
            this.btnProjectStop.Image = global::BlankSpider.App.Properties.Resources.stop_32;
            this.btnProjectStop.Name = "btnProjectStop";
            this.btnProjectStop.Size = new System.Drawing.Size(112, 22);
            this.btnProjectStop.Text = "STOP";
            this.btnProjectStop.Click += new System.EventHandler(this.btnProjectStop_Click);
            // 
            // timerUploadSource
            // 
            this.timerUploadSource.Interval = 1000;
            this.timerUploadSource.Tick += new System.EventHandler(this.timerUploadSource_Tick);
            // 
            // UPDATED_RECORD
            // 
            this.UPDATED_RECORD.Text = "UPDATE";
            // 
            // ListSources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvProject);
            this.Name = "ListSources";
            this.Size = new System.Drawing.Size(634, 618);
            this.Load += new System.EventHandler(this.ListSources_Load);
            this.contextMenuControlSource.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvProject;
        private System.Windows.Forms.ColumnHeader SOURCE;
        private System.Windows.Forms.ColumnHeader MODE;
        private System.Windows.Forms.ColumnHeader STATUS;
        private System.Windows.Forms.ColumnHeader STT;
        private System.Windows.Forms.ColumnHeader LINK_DETAIL;
        private System.Windows.Forms.ColumnHeader NEW_RECORD;
        private System.Windows.Forms.ContextMenuStrip contextMenuControlSource;
        private System.Windows.Forms.ToolStripMenuItem btnProjectStart;
        private System.Windows.Forms.ToolStripMenuItem btnProjectStop;
        private System.Windows.Forms.Timer timerUploadSource;
        private System.Windows.Forms.ColumnHeader LINK_SUB;
        private System.Windows.Forms.ColumnHeader UPDATED_RECORD;
    }
}
