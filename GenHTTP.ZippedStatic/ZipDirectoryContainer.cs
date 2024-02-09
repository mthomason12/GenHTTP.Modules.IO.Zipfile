using GenHTTP.Api.Content.IO;
using System.IO.Compression;

namespace GenHTTP.Modules.IO.Zipfile
{
    internal class ZipDirectoryContainer : IResourceContainer
    {
        public ZipDirectoryTree Tree { get; }
        protected ZipDirectoryInfo Directory { get; }
        protected bool IsRoot { get; }

        public DateTime? Modified
        {
            get { return null; }
        }

        protected ZipDirectoryContainer(ZipDirectoryInfo directory, ZipDirectoryTree? tree = null)
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
                if (Directory.FullName == Path.GetDirectoryName(directory))
                {
                    yield return new ZipDirectoryNode(new ZipDirectoryInfo(Path.Combine(Directory.FullName, directory)), this);
                }
            }
        }

        public IAsyncEnumerable<IResource> GetResources() => GetResourcesInternal().ToAsyncEnumerable();

        public IEnumerable<IResource> GetResourcesInternal()
        {
            foreach(ZipArchiveEntry entry in Tree.Zip.Entries)
            {
                if (Directory.FullName == Path.GetDirectoryName(entry.FullName))
                {
                    yield return GetResource(entry);
                }
            }
        }

        public ValueTask<IResourceNode?> TryGetNodeAsync(string name)
        {
            var path = Path.Combine(Directory.FullName, name);

            var directory = new ZipDirectoryInfo(path);

            if (Tree.DirectoryTree.Contains(directory.FullName))
            {
                return new(new ZipDirectoryNode(directory, this));
            }

            return new();
        }

        public ValueTask<IResource?> TryGetResourceAsync(string name)
        {
            string? path = Path.Combine(Directory.FullName, name);

            ZipArchiveEntry? file = Tree.Zip.GetEntry(name);

            if (file is not null)
            {
                return new ValueTask<IResource?>(GetResource(file));
            }

            return new();
        }

        private IResource GetResource(ZipArchiveEntry entry)
        {
            return new ZipFileResourceBuilder().Entry(entry).Build();
        }

    }
}
