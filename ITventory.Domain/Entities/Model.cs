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
        public string Name { get; init; }
        public Guid ProducentId { get; init; }
        public DateOnly? ReleaseDate { get; private set; }
        public string? Comments { get; private set; }

        private Model() { }

        public Model(string name, Guid producentId, DateOnly releaseDate, string? comments)
        {
            this.Id = Guid.NewGuid();
            Name = name;
            ProducentId = producentId;
            ReleaseDate = releaseDate;
            Comments = comments;
        }

        public static Model Create(string name, Guid producentId, DateOnly releaseDate, string? comments)
        {
            return new Model(name, producentId, releaseDate, comments);
        }
    }
}
