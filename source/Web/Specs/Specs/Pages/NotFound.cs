using Machine.Specifications;

using OpenQA.Selenium;

namespace Specs.Specs.Pages
{
  class NotFound
  {
    readonly string _baseUrl;
    readonly IWebDriver _driver;

    public NotFound(IWebDriver driver, string baseUrl)
    {
      _driver = driver;
      _baseUrl = baseUrl;
    }

    public string Url
    {
      get
      {
        return _baseUrl + "/404";
      }
    }

    public NotFound Open()
    {
      _driver.Navigate().GoToUrl(Url);
      return this;
    }

    public void ShouldShowErrorMessage()
    {
      _driver.FindElement(By.TagName("h3")).Text.ShouldEqual("HTTP Error 404.0 - Not Found");
    }
  }
}
