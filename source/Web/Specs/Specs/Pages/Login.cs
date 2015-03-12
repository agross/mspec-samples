using Machine.Specifications;

using OpenQA.Selenium;

namespace Specs.Specs.Pages
{
  public class Login
  {
    readonly string _baseUrl;
    readonly IWebDriver _driver;

    public Login(IWebDriver driver, string baseUrl)
    {
      _driver = driver;
      _baseUrl = baseUrl;
    }

    public string Url
    {
      get
      {
        return _baseUrl + "/login";
      }
    }

    public Login UserName(string userName)
    {
      _driver.FindElement(By.Id("UserName")).SendKeys(userName);

      return this;
    }
    
    public Login Password(string password)
    {
      _driver.FindElement(By.Id("Password")).SendKeys(password);

      return this;
    }

    public void Submit()
    {
      _driver.FindElement(By.Id("Submit")).Click();
    }

    public Login Open()
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
