using GenHTTP.Api.Content.IO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP.Modules.IO.Zipfile
{
    internal class ZipDirectoryTree: ZipDirectoryContainer, IResourceTree
    {
        List<string> allDirectories = new();
        public ZipArchive Zip { get; }
        public IEnumerable<string> DirectoryTree
        {
            get { return allDirectories; }
        }

        internal ZipDirectoryTree(ZipArchive zip) : base(new ZipDirectoryInfo(""))
        {
            Zip = zip;
            BuildTree(zip);
        }

        internal void BuildTree(ZipArchive zip)
        {
            //get distinct folders
            IEnumerable<string> directories = (from e in zip.Entries
                                              select Path.GetDirectoryName(e.FullName)).Distinct();

            //add to the list along with any missing links in the tree
            foreach (string directory in directories)
            {
                string thisDirectory = directory.Replace('\\', '/');
                allDirectories.Add(thisDirectory);
                string parentDirectory = Path.GetDirectoryName(thisDirectory);
                while (parentDirectory is not null)
                {
                    if (!allDirectories.Contains(thisDirectory))
                    {
                        allDirectories.Add(thisDirectory); 
                    }
                    parentDirectory = Path.GetDirectoryName(parentDirectory);
                }
            }
        }
    }
}
