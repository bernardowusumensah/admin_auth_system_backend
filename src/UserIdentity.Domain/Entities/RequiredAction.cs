using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.Entities
{
    public class RequiredAction
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("account_id")]
        public Guid AccountId { get; set; }
        
        [Column("required_action_type")]
        public RequiredActionType RequiredActionType { get; set; }
        
        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
        
        [Column("is_completed")]
        public bool IsCompleted { get; set; }
        
        // Navigation property
        public Account Account { get; set; } = null!;
    }

    public enum RequiredActionType
    {
        Skip = 0,
        ConfrimEmail = 1,
        EnableMfa = 2,
        CompleteUserInformation = 3
    }
}
