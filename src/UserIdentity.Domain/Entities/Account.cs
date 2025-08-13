using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.Entities;

public class Account
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("username")]
    public string? Username { get; set; }
    
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("hashed_password")]
    public string HashedPassword { get; set; } = string.Empty;
    
    [Column("email_confirmation")]
    public bool EmailConfirmation { get; set; }
    
    [Column("locked_out")]
    public DateTime? LockedOut { get; set; }
    
    [Column("created_on")]
    public DateTime CreatedOn { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public List<RequiredAction> RequiredActions { get; set; } = new();
    public List<Subscription> Subscriptions { get; set; } = new();
}
