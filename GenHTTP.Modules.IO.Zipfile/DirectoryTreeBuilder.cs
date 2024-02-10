using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Modules.IO.FileSystem;
using System.IO;
using System.IO.Compression;

namespace GenHTTP.Modules.IO.Zipfile
{
    public class DirectoryTreeBuilder : IBuilder<IResourceTree>
    {
        ZipArchive _Zip;

        public DirectoryTreeBuilder(Stream stream) 
        {
            Open(stream);
        }

        public DirectoryTreeBuilder(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open);
            Open(stream);
        }

        private void Open(Stream stream)
        {
            _Zip = new ZipArchive(stream);
        }

        //return a ZipTree
        public IResourceTree Build()
        {
            return new ZipDirectoryTree(_Zip);
        }
    }
}
