using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class ProducentReadModel
    {
       public Guid Id { get; set; }
       public string Name { get; set; }
       public Guid CountryId { get; set; }

        // załączenie całych obiektów w read modelu

       public virtual CountryReadModel Country { get; set; }
       public virtual ICollection<ModelReadModel> Models { get; set; }
       public virtual ICollection<SoftwareReadModel> Software { get; set; }
    }
}
