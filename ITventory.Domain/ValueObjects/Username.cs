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
        private Username()
        {

        }
        public Username(string value)
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOperationException("Username cannot be empty");
                }

                Value = value;
            }

            public string Value { get; private set; }


            public static implicit operator Username(string username)
                => new(username);
            public static implicit operator string(Username usn)
                => usn.Value;

        }
    }
