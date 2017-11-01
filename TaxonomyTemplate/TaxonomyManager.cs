using BlankSpider.Scheduler;
using BlankSpider.Spider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxonomyTemplate
{
    public class TaxonomyManager : BaseSpiderManagement//JobScheduler
    {
        private string _Name = string.Empty;
        private Taxonomy _Taxonomy;
        public TaxonomyManager() { }

        //public TaxonomyManager(string Name) {
        //    _Name = Name;
        //    _Taxonomy = new Taxonomy(this.GetName());
        //}

        //public override string GetName()
        //{
        //    return _Name;
        //}


        //public override bool IsSchedulable()
        //{
        //    return true;
        //}

        //public override void DoJob()
        //{

        //    Console.WriteLine("Running Job by Scheduler");
        //}

        //public override void DoSchedulable()
        //{


            
        //    Console.WriteLine("Running Job by Scheduler Repeatition");



        //}

        //public override int GetScheduleIntervalTime()
        //{
        //    return 2000;
        //}

        public override void LoadConfigGeneral()
        {

        }

        public override void LoadConfigLink()
        {
           
        }

        public override void LoadConfigField()
        {
           
        }

        public override void LoadConfigVideo()
        {
         
        }


        public override BaseSpider CreateSpider()
        {
            return new Taxonomy();
        }

    }
}
