using WalletApp.BLL.Dtos.ImageDtos;

namespace WalletApp.WebApi.Tests.TestHelpers;

internal static class ImageTestHelper
{
    public static IEnumerable<ImageReadDto> GetImageReadDtos()
    {
        return new List<ImageReadDto>()
        {
            new()
            {
                Id = 1,
                Name = "Diana",

            },
            new()
            {
                Id = 2,
                Name = "Serhii",
            },
        };
    }

    public static ImageReadDto GetImageReadDto()
    {
        return GetImageReadDtos().First();
    }
}