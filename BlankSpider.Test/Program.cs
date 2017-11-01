using BlankSpider.Scheduler;
using DefaultTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankSpider.Test
{
    class Program
    {
        static void Main(string[] args)
        {



            DefaultManager _defaultManagerment = new DefaultManager("ICOOK", "56c015a5fed0800c905fff88");



            //JobScheduler _job = new TaxonomyTemplate.TaxonomyManager("Taxonomy");
            //_job.ExecuteJob();

            


            //TaxonomyTemplate.Taxonomy _taxonomy = new TaxonomyTemplate.Taxonomy("Taxonomy");
            //_taxonomy.Start();
            //Thread.Sleep(10000);


            //_taxonomy.Pause();


            //Thread.Sleep(10000);
            //_taxonomy.Resume();


            //Console.WriteLine("####################### Sleep 20s #######################################");
            //Thread.Sleep(20000);

            //Console.WriteLine("####################### Stop #######################################");
            //_taxonomy.Stop();


            //Console.WriteLine("####################### Start #######################################");
            //_taxonomy.Start();

            //Thread.Sleep(10000);
            //_taxonomy.Pause();
            //Thread.Sleep(10000);
            //_taxonomy.Resume();




        }
    }
}
