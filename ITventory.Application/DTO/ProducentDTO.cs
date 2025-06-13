using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Application.DTO
{
    public class ProducentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }

        public ICollection<ModelDTO> Models { get; set; }
    }
}
