using System.Configuration;
using System.IO;

namespace Specs.Infrastructure.IIsExpress
{
  class Settings
  {
    public Settings()
    {
      Executable = Path.Combine(Path.GetFullPath(ConfigurationManager.AppSettings["PathToIisExpress"]), "iisexpress.exe");
      PathToWebApp = Path.GetFullPath(ConfigurationManager.AppSettings["PathToWebApp"]);
      Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
    }

    public string Executable { get; private set; }
    public string PathToWebApp { get; private set; }
    public int Port { get; private set; }

    public string WebConfig
    {
      get
      {
        return Path.Combine(PathToWebApp, "web.config");
      }
    }
  }
}
