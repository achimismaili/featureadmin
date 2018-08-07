using FeatureAdmin.Core.Models.Enums;
using System;

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

        public string FontWeight { get
            {
                switch (Level)
                {
                    //case LogLevel.Debug:
                    //    break;
                    //case LogLevel.Information:
                    //    break;
                    case LogLevel.Warning:
                        return "Normal";
                    case LogLevel.Error:
                        return "Bold";
                    default:
                        return "Thin";
                }
            }
        }

        public string LogLine { get
            {
                return string.Format("{0} - {1}: {2}",
                   ShortTime,
                   ShortLevel,
                   Text);
            }
        }

        public string ShortTime { get {
                return Time.ToString("T");
            } }

        public string ShortLevel
        {
            get
            {
                switch (Level)
                {
                    case LogLevel.Debug:
                        return "DBG";
                    case LogLevel.Information:
                        return "NFO";
                    case LogLevel.Warning:
                        return "WRN";
                    case LogLevel.Error:
                    default:
                        return "ERR";
                }
            }
        }
    }
}
