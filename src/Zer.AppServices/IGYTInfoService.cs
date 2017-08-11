using Zer.GytDto;

namespace Zer.AppServices
{
    public interface IGYTInfoService : IAppService<GYTInfoDto, int>
    {
        bool Exists(string bidTruckNo);
    }
}