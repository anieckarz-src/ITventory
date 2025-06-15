using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO
{
    public class HardwareDTO
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public string PrimaryUser {  get; set; }
        public string TopUser {  get; set; }
        public string DefaultDomain {  get; set; }
        public string HardwareType {  get; set; }
        public bool IsActive {  get; set; }
        public int LoginCount {  get; set; }
        public List<LogonDTO> LogonHistory { get; set; }

    }

    public class LogonDTO
    {
        public Guid Id { get; set; }
        public string LoggedUser {  get; set; }
        public string LoggedDomain {  get; set; }
        public string DateLogged {  get; set; }
        public string IpAddress {  get; set; }
    }
}
