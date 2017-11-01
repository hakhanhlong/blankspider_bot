using BlankSpider.Spider;
using BlankSpider.Spider.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankSpider.App.FORMS
{
    public partial class WebSpider : Form
    {
        private readonly object _lockSource = new object();
        private readonly object _lockParser = new object();
        private readonly object _lock = new object();

        public WebSpider()
        {
            InitializeComponent();
        }

        private void WebSpider_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Invalidate();

            this.listSources.InitLoadProject();

            SpiderSingletonEvent.Instance.SpiderStatusChanged += Instance_SpiderStatusChanged;
            SpiderSingletonEvent.Instance.SourceStatusChanged += Instance_SourceStatusChanged;
            SpiderSingletonEvent.Instance.SpiderInformation += Instance_SpiderInformation;

            this.listSources.LockObj = _lockSource;
            this.listParsers1.LockObj = _lockParser;

            SetDoubleBuffered(lvLog);
        }

        private void Instance_SpiderInformation(object sender, Spider.Events.SpiderArgs e)
        {
            lock (_lock)
            {
                
                this.Invoke(new Action(() =>
                {
                    lvLog.BeginUpdate();
                    if (lvLog.Items.Count > 500)
                    {
                        lvLog.Items.Clear();
                    }

                    ListViewItem item = new ListViewItem();
                    item.Text = e.Message;
                    lvLog.Items.Add(item);
                    lvLog.Items[lvLog.Items.Count - 1].EnsureVisible();
                    lvLog.EndUpdate();

                }));
                
            }
       
        }

        void Instance_SourceStatusChanged(object sender, Spider.Events.SpiderManagementArgs e)
        {
            lock (this)
            {
                if (e.SourceStatus != SOURCE_STATUS.PAUSED || e.SourceStatus != SOURCE_STATUS.RUNNING)
                {

                }
                
                this.Invoke(new Action(() =>
                {
                   
                    lvLog.BeginUpdate();
                    ListViewItem item = new ListViewItem();

                    item.Text = e.Message;
                    lvLog.Items.Add(item);
                    lvLog.Items[lvLog.Items.Count - 1].EnsureVisible();
                    lvLog.EndUpdate();

                }));
                
            }

        }

        void Instance_SpiderStatusChanged(object sender, Spider.Events.SpiderArgs e)
        {
            lock (this)
            {
                this.Invoke(new Action(() =>
                {
                    lvLog.BeginUpdate();
                    ListViewItem item = new ListViewItem();

                    item.Text = e.Message;
                    lvLog.Items.Add(item);
                    lvLog.Items[lvLog.Items.Count - 1].EnsureVisible();
                    lvLog.EndUpdate();

                }));
            }
           
            
        }

        public void SetDoubleBuffered(System.Windows.Forms.Control c)
        {

            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }


    }
}
