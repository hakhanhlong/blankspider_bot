using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlankSpider.Api.Entities;
using BlankSpider.Api.Implements;
using BlankSpider.App.CustomEventArgs;
using BlankSpider.App.ENUMS;
using BlankSpider.Spider;
using System.IO;
using System.Collections;

namespace BlankSpider.App.CONTROLS
{
    public partial class ListSources : UserControl
    {

        private string currentDir = Directory.GetCurrentDirectory();
        private Hashtable _MapToSource = new Hashtable();
        private Hashtable _MapToListItem = new Hashtable();
        private Hashtable _MapToSpiderManagement = new Hashtable();


        //private static object _lock = new object();

     

        #region Custom Event
        public event EventHandler<SourceArgs> AddSource;
        public void OnAddSource(SourceArgs args)
        {
            if (AddSource != null)
            {
                AddSource(this, args);
            }
        }
        #endregion

        public ListSources()
        {
            InitializeComponent();
        }

        private void ListSources_Load(object sender, EventArgs e)
        {
            AddSource += ListSources_AddSource;
            SpiderSingletonEvent.Instance.SourceStatusChanged += Instance_SourceStatusChanged;
            SetDoubleBuffered(this.lvProject);
        }

        public object LockObj
        {
            get;set;
        }

        void Instance_SourceStatusChanged(object sender, Spider.Events.SpiderManagementArgs e)
        {
            lock (this)
            {
                if (e.SourceStatus == Spider.Enums.SOURCE_STATUS.RUNNING || e.SourceStatus == Spider.Enums.SOURCE_STATUS.PAUSED)
                {
                    if (lvProject.SelectedItems.Count > 0)
                    {
                        ListViewItem item = lvProject.SelectedItems[0];

                        Source s = (Source)_MapToSource[item];

                        s.status = e.SourceStatus.ToString();
                    }
                }
            }
            
        }

        void ListSources_AddSource(object sender, SourceArgs e)
        {
            this.Invoke(new Action(() => {

                ListViewItem item = new ListViewItem();
                Source s = e.Source;
                item.Text = e.Index.ToString();
                item.SubItems.Add(s.name);
                item.SubItems.Add(s.mode);
                item.SubItems.Add(s.status);

                item.SubItems.Add("0"); //sub libk
                item.SubItems.Add("0"); // detail link
                item.SubItems.Add("0"); // new record
                item.SubItems.Add("0"); // update record


                item.Group = e.ListViewGroup;

                _MapToListItem[s] = item;
                _MapToSource[item] = s;
                _MapToSpiderManagement[item] = e.SpiderManagement;
                

                lvProject.Items.Add(item);
            }));

            
        }

        public async void InitLoadProject()
        {

            await Task.Run(() =>
            {
                //ListProjects _listprojects = new ProjectImpl().GetAll<ListProjects>();
                string serverip = ConfigurationManager.AppSettings["SERVER_IP"].ToString();
                string _enviroment = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                ListProjects _listprojects = new ProjectImpl().GetByServerIP<ListProjects>(serverip);
                if (_listprojects != null)
                {

                    this.Invoke(new Action(() =>
                    {
                        int count = 0;
                        foreach (Project p in _listprojects.Projects)
                        {
                            ListViewGroup _group = new ListViewGroup(p.name, HorizontalAlignment.Left);
                            lvProject.Groups.Add(_group);


                            foreach (Source item in p.Sources)
                            {
                                //only source active 
                                if(item.is_active == true)
                                {
                                    count++;
                                    SourceArgs sArgs = new SourceArgs() { ListViewGroup = _group, Source = item, Index = count };
                                    BaseSpiderManagement spider = null;
                                    #region plugin - Default
                                    if (item.type_spider == TypeSpider.Default.ToString())
                                    {
                                        //load template
                                        if(_enviroment == "TEST")
                                        {
                                            spider = LoadPluginSpider_Test(string.Format("{0}Template", TypeSpider.Default.ToString()), "Default");
                                        }
                                        else
                                        {
                                            spider = LoadPluginSpider(string.Format("{0}Template", TypeSpider.Default.ToString()), "Default");
                                        }
                                        
                                        spider.Name = item.name;
                                        spider.SourceId = item.id;

                                        switch (item.mode)
                                        {
                                            case "GETNEW":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.GETNEW;
                                                break;
                                            case "UPDATE":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.UPDATE;
                                                break;
                                            case "TEST":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.TEST;
                                                break;
                                        }

                                        

                                        spider.LoadConfigGeneral();
                                        spider.LoadConfigLink();
                                        spider.LoadConfigField();
                                    }
                                    #endregion
                                    #region plugin - Taxonomy
                                    else if (item.type_spider == TypeSpider.Taxonomy.ToString())
                                    {

                                    }
                                    #endregion
                                    #region plugin - Archivied
                                    else if (item.type_spider == TypeSpider.Archivied.ToString())
                                    {
                                        //load template
                                        if(_enviroment == "TEST")
                                        {
                                            spider = LoadPluginSpider_Test(string.Format("{0}Template", TypeSpider.Archivied.ToString()), "Archivied");
                                        }
                                        else
                                        {
                                            spider = LoadPluginSpider(string.Format("{0}Template", TypeSpider.Archivied.ToString()), "Archivied");
                                        }

                                        
                                        spider.Name = item.name;
                                        spider.SourceId = item.id;
                                        switch (item.mode)
                                        {
                                            case "GETNEW":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.GETNEW;
                                                break;
                                            case "UPDATE":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.UPDATE;
                                                break;
                                            case "TEST":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.TEST;
                                                break;
                                        }

                                        spider.LoadConfigGeneral();
                                        spider.LoadConfigLink();
                                        spider.LoadConfigField();
                                        spider.LoadConfigVideo();
                                    }
                                    #endregion
                                    #region plugin - Customize
                                    else if (item.type_spider == TypeSpider.Customize.ToString())
                                    {

                                        //load template
                                        if (_enviroment == "TEST")
                                        {
                                            spider = LoadPluginCustomize_Test(item.name, item.name);
                                        }
                                        else
                                        {
                                            spider = LoadPluginCustomize(item.name, item.name);
                                        }

                                        
                                        spider.Name = item.name;
                                        spider.SourceId = item.id;
                                        switch (item.mode)
                                        {
                                            case "GETNEW":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.GETNEW;
                                                break;
                                            case "UPDATE":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.UPDATE;
                                                break;
                                            case "TEST":
                                                spider.Mode = BaseSpiderManagement.MODE_SPIDER.TEST;
                                                break;
                                        }

                                        spider.LoadConfigGeneral();
                                        spider.LoadConfigLink();
                                        spider.LoadConfigField();
                                    }
                                    #endregion
                                    sArgs.SpiderManagement = spider;
                                    OnAddSource(sArgs);
                                }
                                
                            }

                            timerUploadSource.Enabled = true;

                        }
                    }));



                }
            });
        }

        private BaseSpiderManagement LoadPluginSpider(string name, string type)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(currentDir + "\\Addon\\" + type + "Template\\" + name + ".dll");
            BaseSpiderManagement spiderManager = (BaseSpiderManagement)assembly.CreateInstance(name + "." + type + "Manager");
            return spiderManager;
        }



        string rootDir = string.Empty;
        private BaseSpiderManagement LoadPluginSpider_Test(string name, string type)
        {
            rootDir = getParentDirectory(currentDir, 3);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(rootDir + "\\" + type + "Template\\bin\\Debug\\" + name + ".dll");
            BaseSpiderManagement spiderManager = (BaseSpiderManagement)assembly.CreateInstance(name + "." + type + "Manager");
            return spiderManager;
        }


        private BaseSpiderManagement LoadPluginCustomize(string name, string type)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(currentDir + "\\Addon\\" + type + "\\" + name + ".dll");
            BaseSpiderManagement spiderManager = (BaseSpiderManagement)assembly.CreateInstance(name + "." + type + "Manager");
            return spiderManager;
        }        
        private BaseSpiderManagement LoadPluginCustomize_Test(string name, string type)
        {
            rootDir = getParentDirectory(currentDir, 3);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(rootDir + "\\" + type + "\\bin\\Debug\\" + name + ".dll");
            BaseSpiderManagement spiderManager = (BaseSpiderManagement)assembly.CreateInstance(name + "." + type + "Manager");
            return spiderManager;
        }

        private string getParentDirectory(string dir, int level)
        {
            string parentDir = dir;
            for (int i = 1; i <= level; i++)
            {
                try
                {
                    parentDir = Directory.GetParent(parentDir).FullName;
                }
                catch
                {
                    break;
                }
            }
            return parentDir;
        }


        private void contextMenuControlSource_Opening(object sender, CancelEventArgs e)
        {
            lock (this)
            {
                if (lvProject.SelectedItems.Count <= 0)
                {
                    e.Cancel = true;

                }
            }
            
        }

        private void lvProject_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            lock (this)
            {
                ListViewItem item = lvProject.Items[e.ItemIndex];
                Source s = (Source)_MapToSource[item];
                if (s.status == "RUNNING")
                {
                    btnProjectStart.Visible = false;
                    btnProjectStop.Visible = true;
                }
                else if (s.status == "PAUSED" || s.status == "NEED_CONFIG")
                {
                    btnProjectStart.Visible = true;
                    btnProjectStop.Visible = false;
                }
                else
                {
                    //btnProjectStart.Visible = true;
                    //btnProjectStop.Visible = true;
                }
            }

       

            
        }



        #region START & STOP

        delegate void ActionSpiderManagement(BaseSpiderManagement s, ListViewItem item);

        private void btnProjectStart_Click(object sender, EventArgs e)
        {
         

            SpiderManagementAction(delegate (BaseSpiderManagement s, ListViewItem item)
            {

                lock (this)
                {
                    if (lvProject.SelectedItems.Count > 0)
                    {
                        ((BaseSpiderManagement)_MapToSpiderManagement[item]).Start();
                        btnProjectStart.Visible = false;
                        btnProjectStop.Visible = true;
                    }
                }
            });
        }

        private void btnProjectStop_Click(object sender, EventArgs e)
        {
            SpiderManagementAction(delegate (BaseSpiderManagement s, ListViewItem item)
            {

                lock (this)
                {
                    if (lvProject.SelectedItems.Count > 0)
                    {
                        ((BaseSpiderManagement)_MapToSpiderManagement[item]).Stop();
                        btnProjectStart.Visible = true;
                        btnProjectStop.Visible = false;
                    }
                }
            });

          
        }

        private void SpiderManagementAction(ActionSpiderManagement action)
        {
            lock (this)
            {
                if (lvProject.SelectedItems.Count > 0)
                {
                    try
                    {
                        lvProject.BeginUpdate();


                        for (int i = lvProject.SelectedItems.Count - 1; i >= 0; i--)
                        {
                            ListViewItem item = lvProject.SelectedItems[i];

                            action((BaseSpiderManagement)_MapToSpiderManagement[item], item);


                        }


                    }
                    finally
                    {
                        lvProject.EndUpdate();
                    }
                }
            }


        }


        #endregion

        private void timerUploadSource_Tick(object sender, EventArgs e)
        {
            UpdateListSource();

        }

        private  void UpdateListSource()
        {

            lock (LockObj)
            {
                lvProject.BeginUpdate();
                foreach (ListViewItem item in lvProject.Items)
                {

                    this.Invoke(new Action(() =>
                    {

                        Source s = (Source)_MapToSource[item];
                        BaseSpiderManagement bs = ((BaseSpiderManagement)_MapToSpiderManagement[item]);
                        item.SubItems[3].Text = s.status;

                        //totallink
                        item.SubItems[4].Text = bs.frontierURL.Count.ToString();
                        //detaillink
                        item.SubItems[5].Text = bs.queueDetailURL.Count.ToString();

                        //total insert record
                        item.SubItems[6].Text = bs._BaseManagement.TotalInsertLink.ToString();

                        //total update record
                        item.SubItems[7].Text = bs._BaseManagement.TotalUpdateLink.ToString();

                    }));
                }
                lvProject.EndUpdate();
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
