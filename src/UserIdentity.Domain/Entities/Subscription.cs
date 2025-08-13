using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.Entities
{
    public class Subscription
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("account_id")]
        public Guid AccountId { get; set; }
        
        [Column("subscription_type")]
        public SubscriptionType SubscriptionType { get; set; }
        
        [Column("status")]
        public SubscriptionStatus Status { get; set; }
        
        [Column("plan")]
        public SubscriptionPlan Plan { get; set; }
        
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        
        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
        
        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; }
        
        // Navigation property
        public Account Account { get; set; } = null!;
    }

    public enum SubscriptionType
    {
        Basic = 0,
        Premium = 1
    }

    public enum SubscriptionStatus
    {
        Pending = 0,
        Active = 1,
        Expired = 2,
        Canceled = 3,
        Trial = 4
    }

    public enum SubscriptionPlan
    {
        Monthly = 0,
        Yearly = 1,
        Lifetime = 2
    }
}
