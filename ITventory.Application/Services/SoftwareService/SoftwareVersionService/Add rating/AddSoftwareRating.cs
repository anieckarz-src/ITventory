using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_rating
{
    public record AddSoftwareRating (Guid reviwedSoftwareVersionId, Guid reviewedSoftwareId, int ratingMark, Guid ratedById): ICommand_;
    
}
