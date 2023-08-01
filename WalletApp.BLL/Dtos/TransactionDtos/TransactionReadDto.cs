﻿using Microsoft.AspNetCore.Http;
using WalletApp.BLL.Dtos.ImageDtos;
using WalletApp.BLL.Dtos.UserDtos;
using WalletApp.Common.Enums;

namespace WalletApp.BLL.Dtos.TransactionDtos;

public class TransactionReadDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }
    public string WasCreated { get; set; } = null!;
    public string IconName { get; set; } = null!;
    public string? UserName { get; set; }
}

public class TransactionAddDto
{
    public string Name { get; set; } = null!;
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }
    public IFormFile? Icon { get; set; } = null!;
    public Guid? UserId { get; set; }
}