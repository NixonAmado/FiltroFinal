using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Refreshtoken
{
    public int Id { get; set; }

    public int IdUserFk { get; set; }

    public string Token { get; set; }

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Revoked { get; set; }

    public virtual User IdUserFkNavigation { get; set; }
}
