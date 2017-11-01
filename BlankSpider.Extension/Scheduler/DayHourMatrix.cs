using BlankSpider.Extension.Scheduler.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Extension.Scheduler
{
    public class DayHourMatrix
    {
        public const int DAYS = 7;
        public const int HOURS = 24;

        EnableMode[,] enableAt = new EnableMode[DAYS, HOURS];

        public DayHourMatrix() { }

        public DayHourMatrix(string data)
        {
            if (data == null || (data = data.Trim()).Length == 0)
            {
                return;
            }

            string[] days = data.Split('|');

            for (int i = 0; i < days.Length; i++)
            {
                string[] values = days[i].Split(','); //day,hour,status {0,1,2} -> index
                if (values.Length == 3)
                {
                    int day = int.Parse(values[0]);
                    int hour = int.Parse(values[1]);
                    EnableMode em = (EnableMode)int.Parse(values[2]);

                    this[(DayOfWeek)day, hour] = em;
                }
            }
        }

        public void LoadSchedule(string data) {
            if (data == null || (data = data.Trim()).Length == 0)
            {
                return;
            }

            string[] days = data.Split('|');

            for (int i = 0; i < days.Length; i++)
            {
                string[] values = days[i].Split(','); //day,hour,status {0,1,2} -> index
                if (values.Length == 3)
                {
                    int day = int.Parse(values[0]);
                    int hour = int.Parse(values[1]);
                    EnableMode em = (EnableMode)int.Parse(values[2]);

                    this[(DayOfWeek)day, hour] = em;
                }
            }
        }


        public EnableMode this[DayOfWeek day, int hour] {
            get {
                return enableAt[(int)day, hour];
            }
            set {
                enableAt[(int)day, hour] = value;
            }
        }

        public override string ToString()
        {
            string selected = string.Empty;
            for (int i = 0; i <DayHourMatrix.DAYS; i++) {

                for (int j = 0; j < DayHourMatrix.HOURS; j++) {

                    if (this[(DayOfWeek)i, j] != EnableMode.Disabled) { 

                        if (selected.Length > 0) {
                            selected += "|";
                        }
                        //day,hour,status|day,hour,status|day,hour,status e.x: 0,0,active | 1,23,disabled 
                        selected += string.Format("{0},{1},{2}", i, j, this[(DayOfWeek)i, j]);
                    }
                }
            }

            return selected;
        }
    }
}
