using Zer.Entities;
using Zer.Framework.Dependency;
using Zer.GytDataService;
using Zer.GytDto;
using Zer.GytDto.Extensions;

namespace Zer.Framework.Mvc.Logs
{
    public sealed class UserActionLogger : ILogger
    {
        private readonly ILogInfoDataService _logInfoDataService;

        private UserActionLogger(ILogInfoDataService logInfoDataService)
        {
            _logInfoDataService = logInfoDataService;
        }

        public static UserActionLogger Instance { get; private set; }

        static UserActionLogger()
        {
            Instance = new UserActionLogger(IocManager.Instance.Resolve<ILogInfoDataService>());
        }

        public void Info(LogInfoDto logInfoDto)
        {
            var entity = logInfoDto.Map<UserLogInfo>();
            if (entity != null)
            {
                _logInfoDataService.Insert(logInfoDto.Map<UserLogInfo>());
            }
            else
            {
                Log4NetLogger.Logger.Error(logInfoDto);
            }
        }
    }
}