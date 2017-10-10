using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace OracleCommunication_Demo
{
    public class TimerViewModel : BaseViewModel
    {

        private string currentTime;

        public TimerViewModel()
        {
            Timer = new DispatcherTimer();
            StopWatch = new Stopwatch();
            CurrentTime = "00:00";
        }

        public DispatcherTimer Timer { get; set; }

        public Stopwatch StopWatch { get; set; }

        public string CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                RaisePropertyChanged();
            }
        }

        public void InitTimer()
        {
            CurrentTime = "00:00";
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick -= Timer_Tick;
            Timer.Tick += Timer_Tick;
        }

        public void StartTimer()
        {
            Timer.Start();
            StopWatch.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            TimeSpan ts = StopWatch.Elapsed;
            CurrentTime = string.Format("{0:00}:{1:00}",
               ts.Minutes, ts.Seconds);
        }

        public void StopTimer()
        {
            Timer.Stop();
            StopWatch.Stop();
            StopWatch.Reset();
            CurrentTime = "00:00";
        }

        public void PauseTimer()
        {
            Timer.Stop();
            StopWatch.Stop();
        }
    }
}
