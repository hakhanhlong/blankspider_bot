using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankSpider.Scheduler
{
    public abstract class JobScheduler
    {


        public void ExecuteJob() {
            if (IsSchedulable())
            {
                this.DoSchedulable();
                
            }
            else {
                DoJob();
            }
        }

        public abstract string GetName();
        public abstract void DoJob();
        public abstract void DoSchedulable();

        public abstract bool IsSchedulable();

        public abstract int GetScheduleIntervalTime();
    }
}
