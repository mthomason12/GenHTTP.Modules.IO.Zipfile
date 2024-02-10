using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Basics;
using GenHTTP.Modules.IO.Streaming;
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

        private ZipArchiveEntry _Entry;

        

        public ZipFileResource(ZipArchiveEntry entry, string? name, FlexibleContentType? contentType)
        {
            Name = name ?? entry.Name;
            Modified = null;
            Length = (ulong)entry.Length;
            ContentType = contentType ?? FlexibleContentType.Get(Name.GuessContentType() ?? Api.Protocol.ContentType.ApplicationForceDownload);
            _Entry = entry;
        }

        public ValueTask<ulong> CalculateChecksumAsync()
        {
            unchecked
            {
                ulong hash = 17;

                var length = Length;

                hash = hash * 23 + (ulong)Modified.GetHashCode();
                hash = hash * 23 + ((length != null) ? length.Value : 0);

                return new ValueTask<ulong>(hash);
            }
        }

        public ValueTask<Stream> GetContentAsync()
        {
            return new ValueTask<Stream>(_Entry.Open());
        }

        public async ValueTask WriteAsync(Stream target, uint bufferSize)
        {
            using var content = _Entry.Open();

            await content.CopyPooledAsync(target, bufferSize).ConfigureAwait(false);
        }
    }
}
