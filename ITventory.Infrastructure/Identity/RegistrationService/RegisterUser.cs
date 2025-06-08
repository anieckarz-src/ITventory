using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Infrastructure.Identity.RegistrationService
{
    public record RegisterUser(string username, string email, string password): ICommand_;
   
}
