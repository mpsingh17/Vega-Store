using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.Entities
{
    public class FileOnFileSystem
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
