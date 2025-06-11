using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;

namespace ITventory.Domain.ValueObjects
{
    public record ZipCode: ValueObject
    {
        public string Value { get; private set; }

        public ZipCode(string value)
        {
            if (value.Length < 3){
                throw new ArgumentException("Invalid ZipCode");
            }

            Value = value;
        }

        public static implicit operator ZipCode(string value) =>
            new ZipCode(value);

        public static implicit operator string(ZipCode value) => 
            value.Value;
    }
}
