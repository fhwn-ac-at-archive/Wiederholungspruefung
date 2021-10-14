using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLoader
{
    public class Command
    {
        public Command(string executionCommand, string args = null)
        {
            this.ExecutionCommand = executionCommand;
            this.Args = args;
        }

        public string ExecutionCommand { get; set; }
        public string Args { get; set; }
    }
}
