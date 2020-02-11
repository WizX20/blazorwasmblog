using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;

namespace BlazorWasmBlog.Core.Infrastructure.Extensions
{
    public class InMemoryFileProvider : IFileProvider
    {
        private class InMemoryFile : IFileInfo
        {
            private readonly byte[] data;

            public InMemoryFile(string json) => this.data = Encoding.UTF8.GetBytes(json);

            public Stream CreateReadStream() => new MemoryStream(this.data);

            public bool Exists { get; } = true;
            public long Length => this.data.Length;
            public string PhysicalPath { get; } = string.Empty;
            public string Name { get; } = string.Empty;
            public DateTimeOffset LastModified { get; } = DateTimeOffset.UtcNow;
            public bool IsDirectory { get; } = false;
        }

        private readonly IFileInfo fileInfo;

        public InMemoryFileProvider(string json) => this.fileInfo = new InMemoryFile(json);

        public IFileInfo GetFileInfo(string _) => this.fileInfo;

        public IDirectoryContents GetDirectoryContents(string _) => null;

        public IChangeToken Watch(string _) => NullChangeToken.Singleton;
    }
}