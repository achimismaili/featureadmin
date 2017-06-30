using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.IO;

namespace FeatureAdmin.Services
{
    /// <summary>
    /// Logging is initialized in Program.cs
    /// </summary>
    public static class Logger
    {
        public static StringWriter LogMessages = new StringWriter();
    }
}
