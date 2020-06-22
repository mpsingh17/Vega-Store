using System;
using System.Collections.Generic;
using System.Text;

namespace VegaStore.Core.Entities
{
    public enum Colors
    {
        Red, Green, Black
    }

    public enum Condition
    {
        New, Old
    }

    public class Vehicle : EntityBase
    {
        public string Name { get; set; }

        public bool IsRegistered { get; set; }

        public Colors Color { get; set; }

        public Condition Condition { get; set; }

        public decimal Price { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

    }
}
