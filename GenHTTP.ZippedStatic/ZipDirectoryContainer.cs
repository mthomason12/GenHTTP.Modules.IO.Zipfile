using GenHTTP.Api.Content.IO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP.Modules.IO.Zipfile
{
    internal class ZipDirectoryContainer : IResourceContainer
    {
        protected ZipDirectoryTree Tree { get; }
        protected DirectoryInfo Directory { get; }
        protected bool IsRoot { get; }

        public DateTime? Modified
        {
            get { return null; }
        }

        protected ZipDirectoryContainer(DirectoryInfo directory, ZipDirectoryTree tree = null)
        {
            if (tree != null)
                Tree = tree;
            else
                Tree = (ZipDirectoryTree)this;
            Directory = directory;
        }


        public IAsyncEnumerable<IResourceNode> GetNodes() => GetNodesInternal().ToAsyncEnumerable();

        private IEnumerable<IResourceNode> GetNodesInternal()
        {
            foreach (string directory in Tree.DirectoryTree)
            {
                yield return new ZipDirectoryNode(new DirectoryInfo(Path.Combine(Directory.FullName,directory)), this);
            }
        }

        public IAsyncEnumerable<IResource> GetResources() => GetResourcesInternal().ToAsyncEnumerable();

        public IEnumerable<IResource> GetResourcesInternal()
        {
            /*foreach (var file in Directory.EnumerateFiles())
            {
                yield return Resource.FromFile(file).Build();
            }*/
        }

        public ValueTask<IResourceNode?> TryGetNodeAsync(string name)
        {
            var path = Path.Combine(Directory.FullName, name);

            var directory = new DirectoryInfo(path);

            if (Tree.DirectoryTree.Contains(directory.FullName))
            {
                return new(new ZipDirectoryNode(directory, this));
            }

            return new();
        }

        public ValueTask<IResource?> TryGetResourceAsync(string name)
        {
            /*var path = Path.Combine(Directory.FullName, name);

            var file = new FileInfo(path);

            if (file.Exists)
            {
                return new(Resource.FromFile(file).Build());
            }

            return new();*/
        }

    }
}
