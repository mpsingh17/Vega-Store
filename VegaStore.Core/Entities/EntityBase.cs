using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
