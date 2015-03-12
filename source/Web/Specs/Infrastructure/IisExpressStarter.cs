using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

using Specs.Infrastructure.FreePort;

using Settings = Specs.Infrastructure.IIsExpress.Settings;

namespace Specs.Infrastructure
{
  public class IisExpressStarter : IStartable
  {
    internal static string BaseUrl;
    internal static Process Process;
    Launcher _iisExpress;

    public void Start()
    {
      var iisSettings = new Settings();

      var settings = new FreePort.Settings(iisSettings.Executable, iisSettings.Port)
      {
        Arguments = x =>
        {
          var args = new[]
          {
            String.Format(CultureInfo.InvariantCulture, "/port:{0}", x.Port),
            String.Format(CultureInfo.InvariantCulture, @"/path:""{0}""", iisSettings.PathToWebApp),
            "/systray:false"
          };

          return String.Join(" ", args);
        },
        CheckStarted = p =>
        {
          var standardOutput = new StringBuilder();
          for (var line = 0; line < 4; line++)
          {
            standardOutput.AppendLine(p.StandardOutput.ReadLine());
          }

          if (p.HasExited ||
              standardOutput.ToString().IndexOf("fail", StringComparison.InvariantCultureIgnoreCase) != -1 ||
              standardOutput.ToString().IndexOf("invalid", StringComparison.InvariantCultureIgnoreCase) != -1)
          {
            throw new IOException("Could not start IIS Express:\n" + standardOutput);
          }
        },
        Started = (p, s) => Process = p
      };

      _iisExpress = Launcher.Launch(settings);

      BaseUrl = string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}", _iisExpress.Port);
    }

    public void Stop()
    {
      if (_iisExpress != null)
      {
        _iisExpress.Dispose();
      }
    }
  }
}
