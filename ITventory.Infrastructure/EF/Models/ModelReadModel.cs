using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class ModelReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProducentId { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Comments { get; set; }

        // Inkorporacja relacji

        public virtual ProducentReadModel Producent { get; set; }
        public virtual CountryReadModel Country { get; set; }
        

    }
}
