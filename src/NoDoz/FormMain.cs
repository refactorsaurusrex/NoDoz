using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NodaTime;

namespace NoDoz
{
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public partial class FormMain : Form
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(1000);
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private double _totalDuration;
        private readonly bool _startMinimized;
        private readonly Font _indefiniteFont = new Font("Open Sans", 12.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private readonly Font _timeoutFont = new Font("Open Sans", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);

        public FormMain(Options args)
        {
            InitializeComponent();

            _startMinimized = args.Minimize;
            _timer.Elapsed += _timer_Elapsed;
            _timer.SynchronizingObject = this;

            if (args.Timeout.HasValue)
            {
                SetCountDown(args.Timeout.Value);
            }
            else
            {
                SetIndefiniteMode();
            }
        }

        private async void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            labelDuration.Text = TimeRemaining().ToString(@"hh\:mm\:ss");
            if (_stopwatch.ElapsedMilliseconds >= _totalDuration)
            {
                _timer.Stop();
                Hide();
                ShowBalloon("Hey dude. I'm outta here...");
                await Task.Delay(5000);
                Close();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SystemSleep.Prevent();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                ShowBalloon();
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (_startMinimized)
            {
                Hide();
                ShowBalloon();
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
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
                case Keys.E:
                    ShowDurationEditDialog();
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
            else
            {
                ShowBalloon();
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

        private void labelDuration_DoubleClick(object sender, EventArgs e) => ShowDurationEditDialog();

        private void ShowDurationEditDialog()
        {
            using (var form = new FormDurationEdit())
            {
                var result = form.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                if (form.Timeout.HasValue)
                {
                    SetCountDown(form.Timeout.Value);
                }
                else
                {
                    SetIndefiniteMode();
                }
            }
        }

        private void SetCountDown(Duration duration)
        {
            _totalDuration = duration.TotalMilliseconds;
            labelDuration.Text = duration.ToString("HH:mm:ss", null);
            labelDuration.Font = _timeoutFont;
            _timer.Enabled = true;
            _stopwatch.Restart();
        }

        private void SetIndefiniteMode()
        {
            _totalDuration = 0;
            _timer.Enabled = false;
            _stopwatch.Reset();
            labelDuration.Font = _indefiniteFont;
            labelDuration.Text = "No timeout specified.";
        }
    }
}
