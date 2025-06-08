using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;
using ITventory.Domain.Enums;

namespace ITventory.Domain.Entities
{
    public class RatingSoftwareVersion: Rating
    {
        public Guid SoftwareVersionId { get; init; }

        private RatingSoftwareVersion(Guid reviewedProductId, int ratingMark, Guid ratedById) :
            base(reviewedProductId, ratingMark, ratedById)
        {

        }


        public static RatingSoftwareVersion Create(Guid reviewedSoftwareId, int ratingMark, Guid ratedById)
        {
            return new RatingSoftwareVersion(reviewedSoftwareId, ratingMark, ratedById);
        }
    }
}
