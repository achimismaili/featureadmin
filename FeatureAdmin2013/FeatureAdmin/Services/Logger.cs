using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.IO;
using System.Windows.Forms;

namespace FeatureAdmin.Services
{
    /// <summary>
    /// Serilog Textwriter logging is initialized to a TextBox here
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/18726852/redirecting-console-writeline-to-textbox
    /// </remarks>
    public class Logger : TextWriter
    {
        private TextBox textBox;
        public override Encoding Encoding
        {
                get { return Encoding.ASCII; }
        }

        public Logger(TextBox textBox)
        {
            this.textBox = textBox;

            Log.Logger = new LoggerConfiguration()
              // .WriteTo.TextWriter(Services.Logger.LogMessages)
              .WriteTo.TextWriter(this, outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}")
              .CreateLogger();

            Log.Information("The global logger has been configured.");
        }

        public override void Write(char value)
        {
            textBox.Text += value;
        }

        public override void Write(string value)
        {
            textBox.Text += value;
        }

        public void ClearLog()
        {
            textBox.Clear();
        }
    }
}
