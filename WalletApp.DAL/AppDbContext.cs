﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Entities;
using WalletApp.DAL.Entities.Identity;

namespace WalletApp.DAL;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<CardBalance> CardBalances => Set<CardBalance>();
    public DbSet<PaymentDue> PaymentDues => Set<PaymentDue>();
}

