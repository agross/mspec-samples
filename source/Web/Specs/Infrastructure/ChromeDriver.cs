using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;

using Specs.Infrastructure.FreePort;

namespace Specs.Infrastructure
{
  public class ChromeDriver : IStartable
  {
    internal static Uri BaseUrl;
    const int DefaultPort = 9515;
    static readonly TimeSpan StartTimeout = TimeSpan.FromSeconds(2);
    Launcher _chromeDriver;

    public void Start()
    {
      var chromeDriver = Path.Combine(Path.GetFullPath(ConfigurationManager.AppSettings["PathToChromeDriver"]),
                                      "chromedriver.exe");

      var settings = new Settings(chromeDriver, DefaultPort)
      {
        Arguments = x => String.Format("--port={0}", x.Port),
        CheckStarted = p =>
        {
          Thread.Sleep(StartTimeout);

          if (p.HasExited)
          {
            throw new Exception(String.Format("Chrome Driver died with exit code {0}", p.ExitCode));
          }
        }
      };

      _chromeDriver = Launcher.Launch(settings);

      BaseUrl = new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}", _chromeDriver.Port));
    }

    public void Stop()
    {
      _chromeDriver.Dispose();
    }
  }
}
