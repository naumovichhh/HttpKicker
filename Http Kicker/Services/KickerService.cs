using Http_Kicker.Models;
using Http_Kicker.SignalR;
using Humanizer;
using Microsoft.AspNetCore.SignalR;

namespace Http_Kicker.Services
{
    public class KickerService : BackgroundService
    {
        private IHubContext<LogHub> _hubContext;

        public KickerService(IHubContext<LogHub> hubContext)
        {
            _hubContext = hubContext;
            try
            {
                if (File.Exists("settings"))
                {
                    using (var reader = new StreamReader("settings"))
                    {
                        URL = reader.ReadLine();
                        Interval = TimeSpan.Parse(reader.ReadLine()!);
                    }
                }
                else
                {
                    URL = null;
                    Interval = TimeSpan.FromMinutes(1);
                }
            }
            catch
            {
                URL = null;
                Interval = TimeSpan.FromMinutes(1);
            }
        }

        public string? URL { get; set; }
        public TimeSpan Interval { get; set; }

        private void GotHttpResponse(string message)
        {
            _hubContext.Clients?.All.SendAsync("Update", message);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var httpClient = new HttpClient();
            while (!stoppingToken.IsCancellationRequested)
            {
                if (URL != null)
                {
                    HttpResponseMessage response;
                    string message;
                    try
                    {
                        response = await httpClient.GetAsync(URL);
                        message = DateTime.Now.ToLongTimeString() + ": " + URL + ": " + (int)response.StatusCode + " " + response.ReasonPhrase;
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                    }

                    GotHttpResponse(message);
                }

                await Task.Delay(Interval);
            }
        }

        public void ApplySettings(Settings settings)
        {
            URL = settings.URL;
            Interval = settings.Interval;
            using (var writer = new StreamWriter("settings"))
            {
                writer.WriteLine(URL);
                writer.WriteLine(Interval);
            }
        }

        public Settings ReadSettings()
        {
            Settings result = new Settings();
            try
            {
                if (File.Exists("settings"))
                {
                    using (var reader = new StreamReader("settings"))
                    {
                        result.URL = reader.ReadLine();
                        result.Interval = TimeSpan.Parse(reader.ReadLine()!);
                    }
                }
            }
            catch
            {
                result.URL = null;
                result.Interval = TimeSpan.Zero;
            }

            return result;
        }
    }
}
