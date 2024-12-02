using System;
using System.Collections.Generic;

namespace KMZWDotNetCore.MiniDigitalWalletDatabase.Models;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public string Sender { get; set; } = null!;

    public string Receiver { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
