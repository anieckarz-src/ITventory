using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries.SoftwareLicense
{
    public class GetSoftwareLicense: IQuery<ICollection<SoftwareLicenseDTO>>
    {
        public string? LicenseKey { get; set; }
        public DateOnly? MaxValidUntill { get; set; }
        public int? MaxUse { get; set; }
        public Guid? SoftwareVersionId { get; set; }
        
    }
}
