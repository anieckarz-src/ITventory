using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;
using ITventory.Domain.Enums;

namespace ITventory.Domain
{
    public class Equipment: Item
    {
        public Condition Condition { get; private set; }

        private double ConditionMultiplier()
        {
            return Condition switch
            {
                Condition.Ideal => 1.0,
                Condition.Good => 0.8,
                Condition.Average => 0.6,
                Condition.Poor => 0.4,
                Condition.Damaged => 0.0,
                _ => 0.5 // default fallback
            };
        }
        public double CurrentWorth => Worth * ConditionMultiplier();

        public DateOnly? LastReviewed => _historyOfReviews.Count == 0 ? null : _historyOfReviews.Max(r => r.ReviewDate);
        public int ReviewCount => _historyOfReviews.Count;


        private List<Review> _historyOfReviews = new();
        public IReadOnlyCollection<Review> HistoryOfReviews => _historyOfReviews.AsReadOnly();

        private Equipment()
        {

        }

        public Equipment(string description, double worth, Guid producentId, Guid modelId, int modelYear, string serialNumber,
                         DateOnly purchasedDate, Guid roomId, Guid departmentId, Condition condition)
    :   base(description, worth, producentId, modelId, modelYear, serialNumber, purchasedDate, roomId, departmentId)
        {
            this.Condition = condition;
        }

        public static Equipment Create(
            string description, double worth, Guid producentId, Guid modelId, int modelYear, string serialNumber,
            DateOnly purchasedDate, Guid roomId, Guid departmentId, Condition condition)
        {
            return new Equipment(description, worth, producentId, modelId, modelYear, serialNumber, purchasedDate, roomId, departmentId, condition);
        }

        public void AddReview(Review review)
        {
            if (review == null) throw new ArgumentNullException("Review is empty here");

            if(_historyOfReviews.Any(r => r.Id == review.Id))
            {
                throw new ArgumentException("Review already exists");
            }

            _historyOfReviews.Add(review);
            Condition = review.Condition;

        }

    }
}
