using System.IO;

using GenHTTP.Api.Content.IO;

namespace GenHTTP.Modules.IO.Zipfile
{

    internal class ZipDirectoryNode : ZipDirectoryContainer, IResourceNode
    {

        #region Get-/Setters

        public string Name => Directory.Name;

        public IResourceContainer Parent { get; }

        #endregion

        #region Initialization

        internal ZipDirectoryNode(DirectoryInfo directory, IResourceContainer parent) : base(directory)
        {
            Parent = parent;
        }

        #endregion

    }

}