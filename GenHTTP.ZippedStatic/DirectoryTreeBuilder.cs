using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Modules.IO.FileSystem;
using System.IO;
using System.IO.Compression;

namespace GenHTTP.Modules.IO.Zipfile
{
    public class DirectoryTreeBuilder : IBuilder<IResourceTree>
    {
        ZipArchive zip;

        public DirectoryTreeBuilder(Stream stream) 
        {
            Open(stream);
        }

        public DirectoryTreeBuilder(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open);
            Open(stream);
        }

        public void Open(Stream stream)
        {
            zip = new ZipArchive(stream);
        }

        //return a ZipTree
        public IResourceTree Build()
        {
            return new ZipDirectoryTree(zip);
        }
    }
}
