using Machine.Specifications;

using Selenium;

namespace LoginApp.Specs.Selenium
{
	public abstract class SeleniumSpecs
	{
		protected internal static ISelenium Selenium;

		Cleanup after =
			() => Selenium.Stop();

		Establish context = () =>
			{
				Selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://localhost:1337/");
				Selenium.Start();
			};
	}

	[Subject("Log in with valid credentials")]
	public class When_logging_in_valid_credentials : SeleniumSpecs
	{
		Establish context = () =>
			{
				Selenium.Open("/Account/LogOn");
				Selenium.Type("UserName", "admin");
				Selenium.Type("Password", "secret2");
				Selenium.Click("//input[@value='Log On']");
				Selenium.WaitForPageToLoad("30000");
			};

		It should_display_the_home_page =
			() => Selenium.GetAllWindowTitles().ShouldContain("Home Page");
	}

	[Subject("Log in with invalid credentials")]
	public class When_logging_in_with_invalid_credentials : SeleniumSpecs
	{
		Establish context = () =>
			{
				Selenium.Open("/Account/LogOn");
				Selenium.Type("UserName", "haxor");
				Selenium.Type("Password", "password");
				Selenium.Click("//input[@value='Log On']");
				Selenium.WaitForPageToLoad("30000");
			};

		It should_display_an_error_message =
			() => Selenium
			      	.GetText("//div[@class=\"validation-summary-errors\"]/span")
			      	.ShouldEqual("Login was unsuccessful. Please correct the errors and try again.");
	}
}