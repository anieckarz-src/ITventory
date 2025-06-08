using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;

namespace ITventory.Domain.AbstractClasses
{
    public abstract class Rating
    {
        public Guid Id { get; init; }
        public Guid ReviewedProductId { get; private set; }
        public int RatingMark { get; private set; }
        public Guid RatedById { get; private set; }
        public DateOnly RateDate { get; private set; }

        public Rating(Guid reviewedProductId, int ratingMark, Guid ratedById)
        {
            if(ratingMark < 1 || ratingMark > 5)
            {
                throw new ArgumentException("Review mark must be between 1 and 5");
            }

            Id = Guid.NewGuid();
            ReviewedProductId = reviewedProductId;
            RatingMark = ratingMark;
            RatedById = ratedById;
            RateDate = DateOnly.FromDateTime(DateTime.UtcNow);
        }

     
    }
}
