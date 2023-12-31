﻿using WalletApp.Common.Enums;
using WalletApp.DAL.Entities.Common;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL.Entities;

public class Transaction : BaseEntity
{
    public override long Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public decimal Sum { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }

    public Guid? SenderId { get; set; }
    public AppUser? Sender { get; set; }
}
