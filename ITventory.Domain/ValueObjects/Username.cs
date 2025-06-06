using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.ValueObjects
{
    public record Username
    {
        public string Value;

        public Username(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Empty value");
            }
            if(value.Length < 4)
            {
                throw new ArgumentException("Username must be at least 4 characters");
            }
            if (!value.Any(char.IsLetter))
            {
                throw new ArgumentException("Username must contain at least one character");
            }

        }
        public static implicit operator Username(string value) =>
            new Username(value);
        public static implicit operator string(Username username) =>
            username.Value;
    }
}
