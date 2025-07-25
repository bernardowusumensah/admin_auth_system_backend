using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.Entities;

public class Account
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("email")]
    public string Email { get; set; }

    [Column("hashed_password")]
    public string HashedPassword { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }
    public User User { get; set; }

}
