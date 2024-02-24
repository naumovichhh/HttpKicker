using Http_Kicker.Services;
using Microsoft.AspNetCore.SignalR;

namespace Http_Kicker.SignalR
{
    public class LogHub : Hub
    {
        //private static IHubContext<LogHub> _contextInstance;

        //public LogHub(IHubContext<LogHub> hubContext)
        //{
        //    _contextInstance = hubContext;
        //}

        //public static void SubscribeToService(KickerService kickerService)
        //{
        //    kickerService.GotHttpResponse += Update;
        //}

        //private static void Update(string message)
        //{
        //    _contextInstance?.Clients?.All?.SendAsync("Update", message);
        //}
    }
}
