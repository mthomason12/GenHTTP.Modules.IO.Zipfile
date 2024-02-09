
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GenHTTP.Modules.IO.Zipfile


namespace GenHTTP.Modules.IO.Zipfile
{
    public static class ResourceTree
    {
        public static DirectoryTreeBuilder FromZip(FileStream zipfile)
        {
            return new DirectoryTreeBuilder(zipfile);
        }

        public static DirectoryTreeBuilder FromZip(String zipfile)
        {
            return new DirectoryTreeBuilder(zipfile);
        }
    }
}
