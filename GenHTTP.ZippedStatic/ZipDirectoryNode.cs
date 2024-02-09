using System.IO;

using GenHTTP.Api.Content.IO;

namespace GenHTTP.Modules.IO.Zipfile
{

    internal class ZipDirectoryNode : ZipDirectoryContainer, IResourceNode
    {

        public string Name => Directory.Name;

        public IResourceContainer Parent { get; }


        internal ZipDirectoryNode(ZipDirectoryInfo directory, ZipDirectoryContainer parent) : base(directory, parent.Tree)
        {
            Parent = parent;
        }


    }

}