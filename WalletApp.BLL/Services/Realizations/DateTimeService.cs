using WalletApp.BLL.Services.Interfaces;

namespace WalletApp.BLL.Services.Realizations;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}
