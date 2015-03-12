using Machine.Specifications;

using OpenQA.Selenium;

namespace Specs.Specs.Pages
{
  public class HomePage
  {
    readonly string _baseUrl;
    readonly IWebDriver _driver;

    public HomePage(IWebDriver driver, string baseUrl)
    {
      _driver = driver;
      _baseUrl = baseUrl;
    }

    public string Url
    {
      get
      {
        return _baseUrl + "/";
      }
    }

    public HomePage Open()
    {
      _driver.Navigate().GoToUrl(Url);

      return this;
    }

    public void AssertIsCurrentPage()
    {
      _driver.Url.ShouldEqual(Url);
    }
  }
}