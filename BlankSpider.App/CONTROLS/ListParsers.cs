using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlankSpider.Spider;

namespace BlankSpider.App.CONTROLS
{
    public partial class ListParsers : UserControl
    {

        private object _lock = new object();


        public ListParsers()
        {
            InitializeComponent();
        }

        private void ListParsers_Load(object sender, EventArgs e)
        {
            SpiderSingletonEvent.Instance.SpiderProcessing += Instance_SpiderProcessing;
            SetDoubleBuffered(this.lvParsers);
        }

        void Instance_SpiderProcessing(object sender, Spider.Events.SpiderArgs e)
        {
            lock (this)
            {
                
                this.Invoke(new Action(() =>
                {
                    lvParsers.BeginUpdate();

                    if(lvParsers.Items.Count > 1000)
                    {
                        lvParsers.Items.Clear();
                    }

                    ListViewItem item = new ListViewItem();
                    item.Text = e.Index.ToString();

                    item.SubItems.Add(e.SourceName);
                    item.SubItems.Add(e.Title);
                    item.SubItems.Add(e.Href);
                    item.SubItems.Add(e.Message);
                    lvParsers.Items.Add(item);
                    lvParsers.Items[lvParsers.Items.Count - 1].EnsureVisible();
                    lvParsers.EndUpdate();

                }));
                
                //throw new NotImplementedException();
            }
            
        }

        public object LockObj
        {
            get;set;
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
