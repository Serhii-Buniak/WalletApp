using FluentValidation.Results;
using WalletApp.Common.Enums;
using WalletApp.WebApi.Requests;

namespace WalletApp.WebApi.Tests.Requests;

[TestFixture]
internal class TransactionAddRequestValidation
{
    private TransactionAddRequest.Validator _validator;
    private TransactionAddRequest _transactionAddRequest;

    [SetUp]
    public void Setup()
    {
        _validator = new TransactionAddRequest.Validator();

        _transactionAddRequest = new TransactionAddRequest()
        {
            Name = "name",
            Description = "description",
            Sum = 1,
            IsPending = false,
            Type = TransactionType.Payment
        };
    }

    [Test]
    public void Validate_IsValid_ReturnTrue()
    {
        ValidationResult result = _validator.Validate(_transactionAddRequest);

        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Validate_NameIsInvalid_ReturnFalse(string name)
    {
        _transactionAddRequest.Name = name;

        ValidationResult result = _validator.Validate(_transactionAddRequest);

        Assert.That(result.IsValid, Is.False);
    }   
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Validate_DescriptionIsInvalid_ReturnFalse(string description)
    {
        _transactionAddRequest.Description = description;

        ValidationResult result = _validator.Validate(_transactionAddRequest);

        Assert.That(result.IsValid, Is.False);
    }    

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    public void Validate_DescriptionIsInvalid_ReturnFalse(decimal sum)
    {
        _transactionAddRequest.Sum = sum;

        ValidationResult result = _validator.Validate(_transactionAddRequest);

        Assert.That(result.IsValid, Is.False);
    }    
}
