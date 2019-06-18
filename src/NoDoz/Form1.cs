using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoDoz
{
    public partial class Form1 : Form
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(1000);
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly double _totalDuration;
        private readonly bool _startMinimized;

        public Form1(Options args)
        {
            InitializeComponent();
            _startMinimized = args.Minimize;

            if (args.Timeout.HasValue)
            {
                _totalDuration = args.Timeout.Value.TotalMilliseconds;
                label1.Text = args.Timeout.Value.ToString("HH:m:ss", null);

                _timer.Elapsed += _timer_Elapsed;
                _timer.SynchronizingObject = this;
                _timer.Start();

                _stopwatch.Start();
            }
            else
            {
                label1.Font = new Font(new FontFamily("Open Sans"), 12.5f);
                label1.Text = "No timeout specified.";
            }
        }

        private async void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            label1.Text = TimeRemaining().ToString(@"hh\:mm\:ss");
            if (_stopwatch.ElapsedMilliseconds >= _totalDuration)
            {
                _timer.Stop();
                Hide();
                ShowBalloon("Hey dude. I'm outta here...");
                await Task.Delay(5000);
                Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SystemSleep.Prevent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                ShowBalloon();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (_startMinimized)
            {
                Hide();
                ShowBalloon();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Hide();
                    ShowBalloon();
                    return;
                case Keys.X:
                    Close();
                    return;
                case Keys.H:
                    Process.Start(@"https://github.com/refactorsaurusrex/NoDoz/wiki");
                    return;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private TimeSpan TimeRemaining() => TimeSpan.FromMilliseconds(_totalDuration - _stopwatch.ElapsedMilliseconds);

        private void ShowBalloon(string message = null)
        {
            var r = TimeRemaining();
            var text = message ?? (_timer.Enabled ? $"{r.Hours} hours, {r.Minutes} minutes, {r.Seconds} seconds remaining..." : "NoDoz is running indefinitely...");
            notifyIcon1.ShowBalloonTip(3000, "NoDoz", text, ToolTipIcon.None);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://github.com/refactorsaurusrex/NoDoz/wiki");
        }
    }
}
