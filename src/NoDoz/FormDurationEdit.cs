using System;
using System.Windows.Forms;
using NodaTime;

namespace NoDoz
{
    public partial class FormDurationEdit : Form
    {
        public FormDurationEdit()
        {
            InitializeComponent();
        }

        public Duration? Timeout { get; private set; }

        private void buttonCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void buttonDone_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            if (!checkBoxIndefinite.Checked)
                Timeout = Duration.FromTimeSpan(new TimeSpan((int)numericUpDownHours.Value, (int)numericUpDownMinutes.Value, (int)numericUpDownSeconds.Value));
        }

        private void numericUpDownHours_Enter(object sender, EventArgs e) => numericUpDownHours.Select(0, 10);

        private void numericUpDownMinutes_Enter(object sender, EventArgs e) => numericUpDownMinutes.Select(0, 10);

        private void numericUpDownSeconds_Enter(object sender, EventArgs e) => numericUpDownSeconds.Select(0, 10);

        private void checkBoxIndefinite_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownHours.Enabled = !checkBoxIndefinite.Checked;
            numericUpDownMinutes.Enabled = !checkBoxIndefinite.Checked;
            numericUpDownSeconds.Enabled = !checkBoxIndefinite.Checked;
        }

        private void FormDurationEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
