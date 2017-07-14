namespace Zer.Framework.Logs
{
    public interface ILogger
    {
        void Error(object message);
        void Info(object message);
        void Debug(object message);
    }
}
