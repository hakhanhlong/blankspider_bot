namespace BlankSpider.App.CONTROLS
{
    partial class ListParsers
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
            this.lvParsers = new System.Windows.Forms.ListView();
            this.STT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SOURCE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TITLE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HREF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MESSAGE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvParsers
            // 
            this.lvParsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvParsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.STT,
            this.SOURCE,
            this.TITLE,
            this.HREF,
            this.MESSAGE});
            this.lvParsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvParsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lvParsers.FullRowSelect = true;
            this.lvParsers.GridLines = true;
            this.lvParsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvParsers.Location = new System.Drawing.Point(0, 0);
            this.lvParsers.Name = "lvParsers";
            this.lvParsers.Size = new System.Drawing.Size(702, 612);
            this.lvParsers.TabIndex = 0;
            this.lvParsers.UseCompatibleStateImageBehavior = false;
            this.lvParsers.View = System.Windows.Forms.View.Details;
            // 
            // STT
            // 
            this.STT.Text = "#";
            // 
            // SOURCE
            // 
            this.SOURCE.Text = "SOURCE";
            this.SOURCE.Width = 100;
            // 
            // TITLE
            // 
            this.TITLE.Text = "TITLE";
            this.TITLE.Width = 320;
            // 
            // HREF
            // 
            this.HREF.Text = "HREF";
            this.HREF.Width = 250;
            // 
            // MESSAGE
            // 
            this.MESSAGE.Text = "MESSAGE";
            this.MESSAGE.Width = 300;
            // 
            // ListParsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvParsers);
            this.Name = "ListParsers";
            this.Size = new System.Drawing.Size(702, 612);
            this.Load += new System.EventHandler(this.ListParsers_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvParsers;
        private System.Windows.Forms.ColumnHeader STT;
        private System.Windows.Forms.ColumnHeader SOURCE;
        private System.Windows.Forms.ColumnHeader TITLE;
        private System.Windows.Forms.ColumnHeader HREF;
        private System.Windows.Forms.ColumnHeader MESSAGE;
    }
}
