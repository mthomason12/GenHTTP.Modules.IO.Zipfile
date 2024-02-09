using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Protocol;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP.Modules.IO.Zipfile
{
    internal class ZipFileResource : IResource
    {
        public string? Name { get; }

        public DateTime? Modified { get; }

        public FlexibleContentType? ContentType { get; }

        public ulong? Length { get; }

        private ZipArchiveEntry entry;

        

        public ZipFileResource(ZipArchiveEntry entry, IResourceContainer container)
        {
            Name = entry.Name;
            Modified = null;
            Length = (ulong)entry.Length;
        }

        public ValueTask<ulong> CalculateChecksumAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Stream> GetContentAsync()
        {
            return entry.Open();
        }

        public ValueTask WriteAsync(Stream target, uint bufferSize)
        {
            throw new NotImplementedException();
        }
    }
}
