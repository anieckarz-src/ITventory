using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.ReviewService.Add_review
{
    public record AddReview(Guid equipmentId, Guid reviewerId, string details, DateOnly reviewDate, Condition condition): ICommand_;
    
}
