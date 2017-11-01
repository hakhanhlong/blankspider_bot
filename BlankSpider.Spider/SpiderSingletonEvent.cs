using BlankSpider.Spider.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Spider
{
    public class SpiderSingletonEvent
    {
        private static SpiderSingletonEvent _instance;
        public static SpiderSingletonEvent Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SpiderSingletonEvent();
                return _instance;
            }
        }
        public SpiderSingletonEvent() { }


        #region Spider
        public event EventHandler<SpiderArgs> SpiderStatusChanged;
        public event EventHandler<SpiderArgs> SpiderProcessing;
        public event EventHandler<SpiderArgs> SpiderInformation;
        public event EventHandler<SpiderArgs> SpiderScreenConsole;
        public event EventHandler<SpiderArgs> SpiderReloadForUpdate;

        public void OnSpiderStatusChanged(SpiderArgs args)
        {
            if (SpiderStatusChanged != null)
            {
                SpiderStatusChanged(this, args);
            }
        }

        public void OnSpiderReloadForUpdate(SpiderArgs args)
        {
            if (SpiderReloadForUpdate != null)
            {
                SpiderReloadForUpdate(this, args);
            }
        }

        public void OnSpiderProcessing(SpiderArgs args)
        {
            if (SpiderProcessing != null)
            {
                SpiderProcessing(this, args);
            }
        }

        public void OnSpiderInformation(SpiderArgs args)
        {
            if (SpiderInformation != null)
            {
                SpiderInformation(this, args);
            }
        }

        public void OnSpiderScreenConsole(SpiderArgs args)
        {
            if (SpiderScreenConsole != null)
            {
                SpiderScreenConsole(this, args);
            }
        }

        #endregion



        #region SpiderManagerment
        public event EventHandler<SpiderManagementArgs> SpiderCreated;
        public event EventHandler<SpiderManagementArgs> SourceStatusChanged;
        public event EventHandler<SpiderManagementArgs> SourceCountLinkChanged;

        public void OnSpiderCreated(SpiderManagementArgs args)
        {
            if (SpiderCreated != null)
            {
                SpiderCreated(this, args);
            }
        }

        public void OnSourceStatusChanged(SpiderManagementArgs args)
        {
            if (SourceStatusChanged != null)
            {
                SourceStatusChanged(this, args);
            }
        }

        public void OnSourceCountLinkChanged(SpiderManagementArgs args)
        {
            if (SourceCountLinkChanged != null)
            {
                SourceCountLinkChanged(this, args);
            }
        }

        #endregion


    }
}
