using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Review: Entity
    {
        public Guid Id { get; init; }
        public Guid ReviwedEquipmentId { get; private set; }
        public Guid ReviewerId { get; init; }
        public string? Details { get; private set; }
        public DateOnly ReviewDate { get; private set; }
        public Condition Condition { get; init; }

        private Review()
        {

        }

        public Review(Guid reviewerId, Guid reviewedEquipmentId, string? details, DateOnly reviewDate, Condition condition)
        {
            if(reviewDate > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentException("Review from future");
            }
            if(!Enum.IsDefined(typeof(Review), reviewerId))
            {
                throw new ArgumentException("Condition not defined");
            }

            Id = Guid.NewGuid();
            ReviewerId = reviewerId;
            ReviwedEquipmentId = reviewedEquipmentId;
            Details = details;
            ReviewDate = reviewDate;
            Condition = condition;
        }

        public static Review Create(Guid reviwedEquipmentId, Guid reviewerId, string? details, DateOnly reviewDate, Condition condition)
        {
            return new Review ( reviwedEquipmentId, reviewerId, details, reviewDate, condition);
        }
    }
}
