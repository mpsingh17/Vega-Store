using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.Entities
{
    public class Model : EntityBase
    {
        public string Name { get; set; }

        public int MakeId { get; set; }
        public Make Make { get; set; }
    }
}
