using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;

namespace ITventory.Domain.ValueObjects
{
    public record Lattitude: ValueObject
    {
        public double Value;

        public Lattitude(double value)
        {
            if(value < -90 || value > 90)
            {
                throw new ArgumentException("Lattitude must be between -90 and 90");

            }

            Value = value;
        }

        public static implicit operator double(Lattitude value) =>
             value.Value;

        public static implicit operator Lattitude(double value) =>
            new(value);
    }
}
