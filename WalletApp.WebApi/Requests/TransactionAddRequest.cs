using AutoMapper;
using FluentValidation;
using WalletApp.BLL.Dtos.TransactionDtos;
using WalletApp.Common.Enums;
using WalletApp.Common.Mapping;

namespace WalletApp.WebApi.Requests;

public class TransactionAddRequest : IMapTo<TransactionAddDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Sum { get; set; }
    public string Description { get; set; } = null!;
    public bool IsPending { get; set; }
    public TransactionType Type { get; set; }
    public IFormFile? Icon { get; set; }

    public class Validator : AbstractValidator<TransactionAddRequest>
    {
        public Validator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Sum)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
