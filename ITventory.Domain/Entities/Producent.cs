using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Producent: Entity
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public int CountryId {  get; init; }

        private Producent()
        {

        }

        public Producent(string name, int countryId)
        {
            this.Id = Guid.NewGuid();
            Name = name;
            CountryId = countryId;
        }
    }
}
