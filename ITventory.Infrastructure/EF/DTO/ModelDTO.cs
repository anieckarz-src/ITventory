using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO
{
    public class ModelDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProducentName { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Comments { get; set; }
    }
}
