using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyLoader
{
    public class ShowType
    {
        public ShowType(Type type)
        {
            this.Type = type;
        }

        public bool AlreadyShown { get; set; }

        public Type Type { get; }
    }
}
