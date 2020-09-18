namespace webapi_m_sqlserver.Domain.Services.Background
{
    public interface IMQClient
    {
        void PushMessage(string topic, object message);
    }
}