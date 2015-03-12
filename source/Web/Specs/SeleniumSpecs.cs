using System;

using Machine.Specifications;

using Specs.Infrastructure;

namespace Specs
{
  public abstract class SeleniumSpecs<TPage>
  {
    protected static TPage Page;

    Establish context =
      () => Page = (TPage) Activator.CreateInstance(typeof(TPage), Browser.Instance, IisExpressStarter.BaseUrl);
  }
  
  public abstract class SeleniumSpecs<TPage, TRedirectPage>
  {
    protected static TPage Page;
    protected static TRedirectPage Redirect;

    Establish context = () =>
      {
        Page = (TPage) Activator.CreateInstance(typeof(TPage), Browser.Instance, IisExpressStarter.BaseUrl);
        Redirect = (TRedirectPage) Activator.CreateInstance(typeof(TRedirectPage), Browser.Instance, IisExpressStarter.BaseUrl);
      };
  }
}
