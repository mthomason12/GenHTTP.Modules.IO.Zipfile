using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Api.Protocol;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenHTTP.Modules.IO.Zipfile
{
    public sealed class ZipFileResourceBuilder : IResourceBuilder<ZipFileResourceBuilder>
    {
        private ZipArchiveEntry _Entry;
        private string? _Name;
        private FlexibleContentType? _Type;


        public IResource Build()
        {
            var entry = _Entry ?? throw new BuilderMissingPropertyException("entry");

            return new ZipFileResource(entry, _Name, _Type);
        }

        public ZipFileResourceBuilder Modified(DateTime modified)
        {
            throw new NotImplementedException();
        }

        public ZipFileResourceBuilder Entry(ZipArchiveEntry entry)
        {
            _Entry = entry;
            return this;
        }

        public ZipFileResourceBuilder Name(string name)
        {
            _Name = name;
            return this;
        }

        public ZipFileResourceBuilder Type(FlexibleContentType contentType)
        {
            _Type = contentType;
            return this;
        }
    }
}
