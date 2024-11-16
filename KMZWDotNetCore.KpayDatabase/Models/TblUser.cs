using System;
using System.Collections.Generic;

namespace KMZWDotNetCore.KpayDatabase.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public string PinCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
