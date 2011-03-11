using Machine.Specifications;

using WatiN.Core;

namespace LoginApp.Specs.Watin
{
	[Subject("Home page")]
	public class When_on_home_page
	{
		static IE Browser;

		Establish context =
			() => Browser = new IE();

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