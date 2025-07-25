using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.ValueObjects;

public class UserRole
{
    [Column("role")]
    public string Role { get; set; }
}
