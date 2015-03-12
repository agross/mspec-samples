using Machine.Specifications;

using Specs.Specs.Pages;

namespace Specs.Specs
{
  [Subject("Log in")]
  public class When_logging_in_valid_credentials : SeleniumSpecs<Login, HomePage>
  {
    Establish context =
      () => Page.Open()
                .UserName("admin")
                .Password("secret");

    Because of =
      () => Page.Submit();

    It should_display_the_home_page =
      () => Redirect.AssertIsCurrentPage();
  }

  [Subject("Log in")]
  public class When_logging_in_with_invalid_credentials : SeleniumSpecs<Login>
  {
    Establish context =
      () => Page.Open()
                .UserName("haxor")
                .Password("secret");

    Because of =
      () => Page.Submit();

    It should_still_display_the_login_page =
      () => Page.AssertIsCurrentPage();
  }

  [Subject("Selenium support")]
  public class When_a_specification_fails : SeleniumSpecs<Login>
  {
    Establish context =
      () => Page.Open()
                .UserName("haxor")
                .Password("secret");

    It should_supply_screenshots_detailing_the_error =
      () => true.ShouldBeFalse();
  }
}
