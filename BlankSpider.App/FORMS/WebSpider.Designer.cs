namespace BlankSpider.App.FORMS
{
    partial class WebSpider
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebSpider));
            this.webSpiderToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnWebSpiderStartAll = new System.Windows.Forms.ToolStripButton();
            this.btnWebSpiderStopAll = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listSources = new BlankSpider.App.CONTROLS.ListSources();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listParsers1 = new BlankSpider.App.CONTROLS.ListParsers();
            this.lvLog = new System.Windows.Forms.ListView();
            this.colLogMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.webSpiderToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // webSpiderToolStrip
            // 
            this.webSpiderToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.webSpiderToolStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.webSpiderToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWebSpiderStartAll,
            this.btnWebSpiderStopAll});
            this.webSpiderToolStrip.Location = new System.Drawing.Point(0, 0);
            this.webSpiderToolStrip.Name = "webSpiderToolStrip";
            this.webSpiderToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.webSpiderToolStrip.Size = new System.Drawing.Size(1347, 25);
            this.webSpiderToolStrip.TabIndex = 0;
            this.webSpiderToolStrip.Text = "toolStrip1";
            // 
            // btnWebSpiderStartAll
            // 
            this.btnWebSpiderStartAll.Image = ((System.Drawing.Image)(resources.GetObject("btnWebSpiderStartAll.Image")));
            this.btnWebSpiderStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWebSpiderStartAll.Name = "btnWebSpiderStartAll";
            this.btnWebSpiderStartAll.Size = new System.Drawing.Size(89, 22);
            this.btnWebSpiderStartAll.Text = "START ALL";
            // 
            // btnWebSpiderStopAll
            // 
            this.btnWebSpiderStopAll.Image = ((System.Drawing.Image)(resources.GetObject("btnWebSpiderStopAll.Image")));
            this.btnWebSpiderStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWebSpiderStopAll.Name = "btnWebSpiderStopAll";
            this.btnWebSpiderStopAll.Size = new System.Drawing.Size(83, 22);
            this.btnWebSpiderStopAll.Text = "STOP ALL";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listSources);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1347, 535);
            this.splitContainer1.SplitterDistance = 548;
            this.splitContainer1.TabIndex = 1;
            // 
            // listSources
            // 
            this.listSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSources.Location = new System.Drawing.Point(0, 0);
            this.listSources.LockObj = null;
            this.listSources.Name = "listSources";
            this.listSources.Size = new System.Drawing.Size(548, 535);
            this.listSources.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listParsers1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvLog);
            this.splitContainer2.Size = new System.Drawing.Size(795, 535);
            this.splitContainer2.SplitterDistance = 413;
            this.splitContainer2.TabIndex = 0;
            // 
            // listParsers1
            // 
            this.listParsers1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listParsers1.Location = new System.Drawing.Point(0, 0);
            this.listParsers1.LockObj = null;
            this.listParsers1.Name = "listParsers1";
            this.listParsers1.Size = new System.Drawing.Size(795, 413);
            this.listParsers1.TabIndex = 0;
            // 
            // lvLog
            // 
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogMessage});
            this.lvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLog.FullRowSelect = true;
            this.lvLog.GridLines = true;
            this.lvLog.Location = new System.Drawing.Point(0, 0);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(795, 118);
            this.lvLog.TabIndex = 0;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // colLogMessage
            // 
            this.colLogMessage.Text = "MESSAGE";
            this.colLogMessage.Width = 696;
            // 
            // WebSpider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1347, 560);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.webSpiderToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebSpider";
            this.Text = "WEB SPIDER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WebSpider_Load);
            this.webSpiderToolStrip.ResumeLayout(false);
            this.webSpiderToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip webSpiderToolStrip;
        private System.Windows.Forms.ToolStripButton btnWebSpiderStartAll;
        private System.Windows.Forms.ToolStripButton btnWebSpiderStopAll;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private CONTROLS.ListSources listSources;
        private CONTROLS.ListParsers listParsers1;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader colLogMessage;
    }
}