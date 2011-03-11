using Machine.Specifications;

using WatiN.Core;

namespace LoginApp.Specs.Watin
{
	public abstract class WatinSpecs
	{
		internal static Browser Browser;

		protected static Browser CreateBrowser()
		{
			Browser = new IE();
			return Browser;
		}
	}

	[Subject("Home page")]
	public class When_on_home_page : WatinSpecs
	{
		Establish context =
			() => CreateBrowser();

		Because of = () =>
			{
				Browser.GoTo("http://localhost:1337/");
				Browser.WaitForComplete();
			};

		Cleanup after =
			() => Browser.Close();

		It should_show_add_details_link = 
			() => Browser.Link(Find.ByText("Log On")).Exists.ShouldBeTrue();
	}
}