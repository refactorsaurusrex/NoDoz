using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandLine;
using NodaTime;
using NodaTime.Text;

namespace NoDoz
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class Options
    {
        private readonly IPattern<Duration> _composite;

        public Options()
        {
            string[] formats =
            {
                "H'h'm'm's's'", "H'h'm'm'", "M'm's's'", "H'h'", "M'm'", "S's'"
            };
            var patterns = formats.Select(DurationPattern.CreateWithInvariantCulture);
            var builder = new CompositePatternBuilder<Duration>();
            foreach (var pattern in patterns)
                builder.Add(pattern, _ => true);
            _composite = builder.Build();
        }

        [Option('m')]
        public bool Minimize { get; set; }

        [Option('t')]
        public string TimeoutExpression { get; set; }

        [Option(Hidden = true)]
        internal Duration? Timeout
        {
            get
            {
                if (string.IsNullOrEmpty(TimeoutExpression))
                {
                    return null;
                }

                try
                {
                    return _composite.Parse(TimeoutExpression).Value;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}