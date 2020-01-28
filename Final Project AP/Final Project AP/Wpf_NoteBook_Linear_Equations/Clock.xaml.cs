using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;



namespace Wpf_NoteBook_Linear_Equations
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : INotifyPropertyChanged
    {
        public List<ClockPart> HourParts { get; set; }
        public List<ClockPart> SecondParts { get; set; }

        private DispatcherTimer Timer { get; set; } = new DispatcherTimer();
        private double angleHour;
        private double angleMin;
        private double angleSec;
        #region Constructor

       static bool ok = false;
       static int h = 0, m = 0, s = 0;

        public Clock()
        {
            InitializeComponent();
            HourParts = new List<ClockPart>();
            for (int x = 0; x < 12; x++)
            {
                HourParts.Add(new ClockPart()
                {
                    Number = (x + 1).ToString(),
                    Angle = (x + 1) * (360 / 12),
                });
            }

            SecondParts = new List<ClockPart>();
            for (int x = 0; x < 60; x++)
            {
                SecondParts.Add(new ClockPart()
                {
                    Number = (x + 1).ToString(),
                    Angle = (x + 1) * (360 / 60),
                });
            }
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();

        }
        #endregion

        #region Get-Set
        public double AngleHour
        {
            get { return angleHour; }
            set
            {
                angleHour = value;
                OnPropertyChanged("AngleHour");
            }
        }


        public double AngleMin
        {
            get { return angleMin; }
            set
            {
                angleMin = value;
                OnPropertyChanged("AngleMin");
            }
        }

        public double AngleSec
        {
            get { return angleSec; }
            set
            {
                angleSec = value;
                OnPropertyChanged("AngleSec");
            }
        }
        #endregion



        #region PropChange
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!ok)
            {
                DateTime time = DateTime.Now;
                AngleHour = (time.Hour) * (360 / 12);
                AngleMin = (time.Minute) * (360 / 60);
                AngleSec = (time.Second) * (360 / 60);
            }
            else
            {


                AngleHour = (h) * (360 / 12);
                AngleMin = (m) * (360 / 60);
                AngleSec = (s) * (360 / 60);
                if (s<=59)
                {
                    s++;
                }
                else if (s == 60)
                {
                    s = 0;
                    m++;
                }
                else if (m == 59)
                {
                    h++;
                    m = 0;
                }
                else if (h== 11)
                {
                    h = 1;
                }

            }
         
           
        }


     
        private void btnSet_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ok = true;
            if (txth.Text == "" && txtm.Text == "" && txtS.Text == "")
            {
                MessageBox.Show("اطلاعات زمان را وارد کنید");
            }
            else
            {
                 h = int.Parse(txth.Text);
                 m = int.Parse(txtm.Text);
                 s = int.Parse(txtS.Text);
            }
        }

        private void btnReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ok = false;
        }
    }

    public class ClockPart
    {
        public string Number { get; set; }
        public int Angle { get; set; }
    }
}
