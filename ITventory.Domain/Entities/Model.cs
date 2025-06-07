using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Model: Entity
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public Guid ProducentId { get; init; }
        public int? ReleaseDate { get; private set; }
        public string? Comments { get; private set; }

        private Model() { }

        public Model(string name, Guid producentId, int releaseDate, string? comments)
        {
            this.Id = Guid.NewGuid();
            Name = name;
            ProducentId = producentId;
            ReleaseDate = releaseDate;
            Comments = comments;
        }
    }
}
