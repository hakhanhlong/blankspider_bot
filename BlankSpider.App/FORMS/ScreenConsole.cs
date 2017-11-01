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

namespace BlankSpider.App.FORMS
{
    public partial class ScreenConsole : Form
    {
        private readonly object _lock = new object();
        public ScreenConsole()
        {
            InitializeComponent();
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

        private void ScreenConsole_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Invalidate();

            SetDoubleBuffered(lvConsole);

            SpiderSingletonEvent.Instance.SpiderScreenConsole += Instance_SpiderScreebConsole;
            SpiderSingletonEvent.Instance.SpiderReloadForUpdate += Instance_SpiderReloadForUpdate;
        }

        private void Instance_SpiderReloadForUpdate(object sender, Spider.Events.SpiderArgs e)
        {
            lock (_lock)
            {
                this.Invoke(new Action(() =>
                {
                    lvConsole.BeginUpdate();
                    ListViewItem item = new ListViewItem();

                    item.Text = e.Message;

                    lvConsole.Items.Add(item);
                    lvConsole.Items[lvConsole.Items.Count - 1].EnsureVisible();
                    lvConsole.EndUpdate();

                }));
            }
        }

        void Instance_SpiderScreebConsole(object sender, Spider.Events.SpiderArgs e)
        {
            lock (_lock)
            {
                this.Invoke(new Action(() =>
                {
                    lvConsole.BeginUpdate();
                    ListViewItem item = new ListViewItem();

                    item.Text = e.Message;

                    lvConsole.Items.Add(item);
                    lvConsole.Items[lvConsole.Items.Count - 1].EnsureVisible();
                    lvConsole.EndUpdate();

                }));
            }


        }
    }
}
