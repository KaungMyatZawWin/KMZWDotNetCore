using System;
using System.Collections.Generic;

namespace KMZWDotNetCore.MiniDigitalWalletDatabase.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public int Pin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
