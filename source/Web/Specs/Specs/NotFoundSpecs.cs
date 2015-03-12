using Machine.Specifications;

using Specs.Infrastructure;
using Specs.Specs.Pages;

namespace Specs.Specs
{
  [Subject("Failed requests")]
  class When_a_page_cannot_be_found : SeleniumSpecs<NotFound>
  {
    Establish context = 
      () => VerifyRequestStatus.DisableOnce();

    Because of =
      () => Page.Open();

    It should_display_error_message =
      () => Page.ShouldShowErrorMessage();
  }
}
