using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO
{
    public class LogonDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Domain { get; set; }
        public DateTime LogonTime { get; set; }
        public string IpAddress { get; set; }
        public EmployeeBasicDto User { get; set; }
    }
}
