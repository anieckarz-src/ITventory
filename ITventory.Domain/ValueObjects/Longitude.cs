using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;

namespace ITventory.Domain.ValueObjects
{
    public record Longitude: ValueObject
    {
        public double Value { get; private set; }

        public Longitude(double value)
        {
            if (value < -180 || value > 180)
            {
                throw new ArgumentException("Longitude must be between -180 and 180");

            }

            Value = value;
        }
         
        public static implicit operator double (Longitude value) =>
            value.Value;

        public static implicit operator Longitude(double value) =>
            new(value);

    }
}
