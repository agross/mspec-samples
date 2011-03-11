using Machine.Specifications;

using WatiN.Core;

namespace LoginApp.Specs.Watin
{
	public abstract class WatinSpecs
	{
		internal static Browser Browser;

		Cleanup after =
			() => Browser.Close();

		protected static void CreateBrowser()
		{
			Browser = new IE();
		}
	}

	[Subject("Log in with valid credentials")]
	public class When_logging_in_valid_credentials : WatinSpecs
	{
		Establish context = CreateBrowser;

		Because of = () =>
			{
				Browser.GoTo("http://localhost:1337/Account/LogOn");
				Browser.TextField("UserName").TypeText("admin");
				Browser.TextField("Password").TypeText("secret");
				Browser.Button(Find.ByValue("Log On")).Click();
				Browser.WaitForComplete();
			};

		It should_display_the_home_page =
			() => Browser.Title.ShouldContain("Home Page");
	}

	[Subject("Log in with invalid credentials")]
	public class When_logging_in_with_invalid_credentials : WatinSpecs
	{
		Establish context = CreateBrowser;

		Because of = () =>
			{
				Browser.GoTo("http://localhost:1337/Account/LogOn");
				Browser.TextField("UserName").TypeText("haxor");
				Browser.TextField("Password").TypeText("password");
				Browser.Button(Find.ByValue("Log On")).Click();
				Browser.WaitForComplete();
			};

		It should_display_an_error_message =
			() => Browser.Div(Find.ByClass("validation-summary-errors"))
			      	.Spans.First()
			      	.Text.ShouldEqual("Login was unsuccessful. Please correct the errors and try again.");
	}

	[Subject("Watin support")]
	public class When_a_specification_fails : WatinSpecs
	{
		Establish context = CreateBrowser;

		Because of = () =>
			{
				Browser.GoTo("http://localhost:1337/Account/LogOn");
				Browser.TextField("UserName").TypeText("haxor");
				Browser.TextField("Password").TypeText("password");
				Browser.Button(Find.ByValue("Log On")).Click();
				Browser.WaitForComplete();
			};

		It should_supply_screenshots_detailing_the_error =
			() => true.ShouldBeFalse();
	}
}