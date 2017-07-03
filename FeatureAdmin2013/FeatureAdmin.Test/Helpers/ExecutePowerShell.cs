using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Test.Helpers
{
    public static class ExecutePowerShell
    {
        /// <summary>
        /// Runs a Powershell script taking it's path and parameters.
        /// </summary>
        /// <param name="scriptFullPath">The full file path for the .ps1 file.</param>
        /// <param name="parameters">The parameters for the script, can be null.</param>
        ///<remarks>
        ///see also https://stackoverflow.com/questions/25756739/how-to-display-psobject-in-c-sharp
        ///</remarks>
        public static void RunScript(string scriptFullPath, ICollection<CommandParameter> parameters = null)
        {
            var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            var pipeline = runspace.CreatePipeline();
            var cmd = new Command(scriptFullPath);
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }
            pipeline.Commands.Add(cmd);
            var results = pipeline.Invoke();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            pipeline.Dispose();
            runspace.Dispose();
            //return results;
        }
    }
}

  