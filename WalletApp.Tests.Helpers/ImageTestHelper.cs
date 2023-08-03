using WalletApp.BLL.Dtos.ImageDtos;

namespace WalletApp.Tests.TestHelpers;

public static class ImageTestHelper
{
    public static IEnumerable<ImageReadDto> GetImageReadDtos()
    {
        return new List<ImageReadDto>()
        {
            new()
            {
                Id = 1,
                Name = "Ikea.png",

            },
            new()
            {
                Id = 2,
                Name = "apple.png",
            },
        };
    }

    public static ImageReadDto GetImageReadDto()
    {
        return GetImageReadDtos().First();
    }
}