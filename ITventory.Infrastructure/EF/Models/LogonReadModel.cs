using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class LogonReadModel
    {
        public Guid Id { get; set; }
        public Guid HardwareId { get; set; }
        public Guid UserId { get; set; }
        public string Domain { get; set; }
        public DateTime LogonTime { get; set; }
        public string IpAddress { get; set; }

        public virtual HardwareReadModel Hardware { get; set; }
        public virtual EmployeeReadModel User { get; set; }
    }
}
