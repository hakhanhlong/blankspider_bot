using BlankSpider.Api.Entities;
using BlankSpider.Api.Implements;
using BlankSpider.App.FORMS;
using BlankSpider.Spider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankSpider.App
{
    public partial class Main : Form
    {
        private WebSpider _formWebSpider;
        private ScreenConsole _formScreenConsole;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("ARE YOU SURE EXIT YES/NO ?", "INFORMATION", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Environment.Exit(0);
                    base.OnClosed(e);
                }
                catch { }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //ucListSource.InitLoadProject();
            
        }

        


        public void StartForm(string formName)
        {
            switch (formName)
            {
                case "WebSpider":
                    this.Invoke(new Action(() => {
                        if (_formWebSpider == null)
                        {
                            _formWebSpider = new WebSpider();
                            _formWebSpider.MdiParent = this;
                            _formWebSpider.Show();
                        }
                        else
                        {
                            if (_formWebSpider.IsDisposed) {
                                _formWebSpider = new WebSpider();
                                _formWebSpider.MdiParent = this;
                                _formWebSpider.Show();
                            }
                            else {
                                _formWebSpider.Activate();
                            }
                        }
                    }));
                    break;
                case "ScreenConsole":
                    this.Invoke(new Action(() => {
                        if (_formScreenConsole == null)
                        {
                            _formScreenConsole = new ScreenConsole();
                            _formScreenConsole.MdiParent = this;
                            _formScreenConsole.Show();
                        }
                        else
                        {
                            if (_formScreenConsole.IsDisposed)
                            {
                                _formScreenConsole = new ScreenConsole();
                                _formScreenConsole.MdiParent = this;
                                _formScreenConsole.Show();
                            }
                            else
                            {
                                _formScreenConsole.Activate();
                            }
                        }
                    }));
                    break;
            }
        }

        private void btnTabViewError_Click(object sender, EventArgs e)
        {
            StartForm("ScreenConsole");
        }

        private void btnTabWebSpider_Click(object sender, EventArgs e)
        {
            StartForm("WebSpider");
        }
    }
}
