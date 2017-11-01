using BlankSpider.Extension.Scheduler;
using BlankSpider.Spider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaxonomyTemplate
{
    public class Taxonomy: BaseSpider
    {
        private string _Name = "Taxonomy";
        private Queue<int> _queueURL = new Queue<int>();
        private Thread []_threads;
        private SemaphoreSlim semaphore;
        private ManualResetEvent wait_handler;
        private bool _isStop = false;
        public DayHourMatrix _DayHourMatrix;

        public Taxonomy() { }
        public Taxonomy(string name) {
            _Name = name;
            //LoadConfigGeneral();
            //_CounterManager = new CounterManager(this._START_ID_, this._MAX_TRYING_COUNT_);
            //_threads = new Thread[this._NUMBER_THREADS_];
            //semaphore = new SemaphoreSlim(this._NUMBER_THREADS_CONCURRENT, this._NUMBER_THREADS_);
            wait_handler = new ManualResetEvent(true);
            _DayHourMatrix = new DayHourMatrix();
        }

        //public override string GetName()
        //{
        //    return _Name;
        //}

        //public override void LoadConfigGeneral()
        //{
        //    //_NUMBER_THREADS_ = 10;
        //    //_NUMBER_THREADS_CONCURRENT = 7;
        //}

        //public override BaseSpider CreateSpider()
        //{
        //    return new Taxonomy();
        //}

        //public override void LoadConfigurationParser()
        //{
            
        //}

        //public override void Process()
        //{

        //    while (!_isStop)
        //    {

                
        //        semaphore.Wait();
        //        int _number = _CounterManager.GetNumber();
        //        wait_handler.WaitOne();

        //        Console.WriteLine("Thread-" + Thread.CurrentThread.Name + " Number:" +  _number);


        //        Thread.Sleep(1000);
        //        semaphore.Release();
                
        //    }
        //}


        public override void LoadConfigParser()
        {

        }

        public override string GetName()
        {
            return "name";
        }
        protected override void ProcessFindLink()
        {

        }
        protected override void ProcessParseLink()
        {

        }


        public void Start() {

            //_isStop = false;
            //for (int i = 0; i < this._NUMBER_THREADS_; i++) {
            //    _threads[i] = new Thread(Process);
            //    _threads[i].Name = i.ToString();
            //    _threads[i].Start();
            //    //_threads[i].Join();
            //}
        }

        public void Pause() {
            wait_handler.Reset();
        }

        public void Resume() {
            wait_handler.Set();

        }

        public void Stop() {

            //_isStop = true;
            //for (int i = 0; i < this._NUMBER_THREADS_; i++)
            //{
            //    if (_threads[i] != null) {



            //        _threads[i].Abort();
            //        _threads[i].Join();
            //        _threads[i] = null;
            //        Thread.Sleep(1000);



            //    }
                
            //}

        }


    }
}
