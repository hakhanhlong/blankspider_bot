using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlankSpider.Extension.Scheduler.Enum;

namespace BlankSpider.Extension.Scheduler
{
    public partial class TimeGrid : UserControl
    {
        Color UnselectedColor = Color.DarkGray;
        Color SelectedColor = Color.Green;

        object Selected = new object();
        object Unselected = new object();


        Label[] lbDays = new Label[DayHourMatrix.DAYS];
        Label[] lbHours = new Label[DayHourMatrix.HOURS];
        PictureBox[,] timePanels = new PictureBox[DayHourMatrix.DAYS, DayHourMatrix.HOURS];

        private Point _startPosition;
        PictureBox lastPanel;
        DayHourMatrix matrix = new DayHourMatrix();


        public TimeGrid()
        {
            InitializeComponent();

            for (int i = 0; i < DayHourMatrix.DAYS; i++)
            {
                for (int j = 0; j < DayHourMatrix.HOURS; j++)
                {
                    timePanels[i, j] = new PictureBox();
                    InitPanel(timePanels[i, j]);
                }
            }


            //Fill label Days in Week
            for (int i = 0; i < DayHourMatrix.DAYS; i++) {
                lbDays[i] = new Label();
                lbDays[i].AutoSize = false;
                lbDays[i].TextAlign = ContentAlignment.MiddleLeft;
                lbDays[i].Text = ((DayOfWeek)i).ToString().Substring(0, 3);
                lbDays[i].Visible = true;
                this.Controls.Add(lbDays[i]);
            }

            //Fill label hours in day

            for (int i = 0; i < DayHourMatrix.HOURS; i++)
            {
                lbHours[i] = new Label();
                lbHours[i].AutoSize = true;
                //timeLbs[i].TextAlign = ContentAlignment.MiddleCenter;
                lbHours[i].Text = i.ToString("0#");
                lbHours[i].Visible = true;
                this.Controls.Add(lbHours[i]);
            }

            this.ResumeLayout(true);

        }

        private void InitPanel(PictureBox p)
        {
            p.BackColor = UnselectedColor;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Visible = true;
            p.MouseMove += new MouseEventHandler(panel_MouseMove);
            p.Tag = Unselected;
            this.Controls.Add(p);
        }

        void panel_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);

            pt.Offset(-this.StartPosition.X, -this.StartPosition.Y);

            int hour = Math.Max(0, Math.Min(pt.X / timePanels[0, 0].Width, DayHourMatrix.HOURS - 1));
            int day = Math.Max(0, Math.Min(pt.Y / timePanels[0, 0].Height, DayHourMatrix.DAYS - 1));

            PictureBox p = timePanels[day, hour];

            if (p == lastPanel) return;

            if (e.Button == MouseButtons.Left)
            {
                SetPanel(p, Selected);
                matrix[(DayOfWeek)day, hour] = EnableMode.Active;

            }
            else if (e.Button == MouseButtons.Right)
            {
                SetPanel(p, Unselected);
                matrix[(DayOfWeek)day, hour] = EnableMode.Disabled;
            }
        }

        private void SetPanel(PictureBox p, object action)
        {
            if (action == null) action = p.Tag;

            if (action == Selected)
            {
                p.Tag = Selected;
                p.BackColor = SelectedColor;
            }
            else if (action == Unselected)
            {
                p.Tag = Unselected;
                p.BackColor = UnselectedColor;
            }
           

            lastPanel = p;
        }

        public DayHourMatrix SelectedTimes
        {
            get
            {
                return matrix;
            }
            set
            {
                matrix = value;

                // unselect all
                for (int i = 0; i < DayHourMatrix.DAYS; i++)
                {
                    for (int j = 0; j < DayHourMatrix.HOURS; j++)
                    {
                        object v = Unselected;

                        if (matrix != null)
                        {
                            EnableMode em = matrix[(DayOfWeek)i, j];
                            if (em == EnableMode.Active)
                            {
                                v = Selected;
                            }
                        }

                        SetPanel(timePanels[i, j], v);
                    }
                }
            }
        }







        private void layoutPanels() {

            int width = (this.Width - this.StartPosition.X) / DayHourMatrix.HOURS;
            int height = (this.Height - this.StartPosition.Y) / DayHourMatrix.DAYS;

            for (int i = 0; i < DayHourMatrix.DAYS; i++)
            {
                lbDays[i].Location = new Point(0, i * height + StartPosition.Y);
                lbDays[i].Size = new Size(StartPosition.X, height);

                for (int j = 0; j < DayHourMatrix.HOURS; j++)
                {
                    timePanels[i, j].Location = new Point(j * width + StartPosition.X, i * height + StartPosition.Y);
                    timePanels[i, j].Size = new Size(width, height);
                }
            }

            for (int j = 0; j < DayHourMatrix.HOURS; j++)
            {
                lbHours[j].Location = new Point(j * width + StartPosition.X, 0);
                lbHours[j].Size = new Size(width, StartPosition.Y);
            }

        }

        public Point StartPosition {
            get {
                return _startPosition;
            }
            set {
                _startPosition = value;
                this.layoutPanels();
            }



        }

        
    }
}
