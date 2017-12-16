using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Messages
{
    public class LogMessage
    {
        public LogMessage(LogLevel level, string text)
        {
            Text = text;
            Level = level;
            Time = DateTime.Now;
        }
        public DateTime Time { get; }
        public LogLevel Level { get; }
        public string Text { get; }
    }
}
