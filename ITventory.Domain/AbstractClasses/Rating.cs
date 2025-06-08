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
        public TypeOfRating TypeOfProduct { get; private set; }
        public int RatingMark { get; private set; }
        public Guid RatedById { get; private set; }
        public DateOnly RateDate { get; private set; }

        public Rating(Guid reviewedProductId, TypeOfRating typeOfRating, int ratingMark, Guid ratedById, DateOnly rateDate)
        {
            Id = Guid.NewGuid();
            ReviewedProductId = reviewedProductId;
            TypeOfProduct = typeOfRating;
            RatingMark = ratingMark;
            RatedById = ratedById;
            RateDate = rateDate;
        }

     
    }
}
