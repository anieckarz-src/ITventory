using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class SoftwareReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PublisherId { get; set; }
        public string ApprovalType { get; set; }

        public virtual ProducentReadModel Publisher { get; set; }
        public virtual ICollection<SoftwareVersionReadModel> Versions { get; set; }
    }
}
