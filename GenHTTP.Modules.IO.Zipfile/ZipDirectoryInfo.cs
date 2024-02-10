using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP.Modules.IO.Zipfile
{
    internal class ZipDirectoryInfo
    {
        public string Name { get; }
        public string FullName { get; }

        public ZipDirectoryInfo(string name) 
        {
            FullName = name;
            Name = Path.GetFileName(name);
        }
    }
}
