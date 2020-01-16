using System;
using System.Diagnostics;
using System.Windows.Forms;
using CommandLine;

namespace NoDoz
{
    internal static class Program
    {
        private const string Text = "Ouch! That didn't work right. Would you like to open a GitHub issue?";
        private const string BugReportUrl = @"https://github.com/refactorsaurusrex/NoDoz/issues/new?assignees=refactorsaurusrex&labels=bug&template=bug_report.md&title=";

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
                {
                    var form = new FormMain(options);
                    try
                    {
                        Application.Run(form);
                    }
                    catch (Exception)
                    {
                        var result = MessageBox.Show(form, Text, "NoDoz", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (result == DialogResult.Yes)
                            Process.Start(BugReportUrl);
                    }
                });
            }
            catch
            {
                var result = MessageBox.Show(Text, "NoDoz", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                    Process.Start(BugReportUrl);
            }
        }
    }
}
