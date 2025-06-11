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
        public Guid CountryId {  get; init; }

        private Producent()
        {

        }

        public Producent(string name, Guid countryId)
        {
            this.Id = Guid.NewGuid();
            Name = name;
            CountryId = countryId;
        }
    }
}
