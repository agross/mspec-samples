using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Specs.Infrastructure
{
  public class Browser : IStartable
  {
    internal static IWebDriver Instance;

    public void Start()
    {
      var options = new ChromeOptions();
      options.AddArgument("--test-type");

      Instance = new RemoteWebDriver(ChromeDriver.BaseUrl, options.ToCapabilities());
    }

    public void Stop()
    {
      Instance.Close();
    }
  }
}
