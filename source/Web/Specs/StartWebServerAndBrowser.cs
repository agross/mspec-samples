using System;
using System.Linq;

using Machine.Specifications;

using Specs.Infrastructure;

namespace Specs
{
  public class StartWebServerAndBrowser : IAssemblyContext
  {
    readonly IStartable[] _startables;

    public StartWebServerAndBrowser()
    {
      _startables = new IStartable[]
      {
        new IisExpressStarter(),
        new ChromeDriver(),
        new Browser(),
        new VerifyRequestStatus()
      };
    }

    public void OnAssemblyStart()
    {
      Array.ForEach(_startables, x => x.Start());
    }

    public void OnAssemblyComplete()
    {
      var errors = _startables
        .Reverse()
        .Select(x =>
        {
          try
          {
            x.Stop();
            return null;
          }
          catch (Exception exception)
          {
            return exception;
          }
        })
        .Where(x => x != null)
        .ToList();

      if (errors.Any())
      {
        throw new AggregateException(errors);
      }
    }
  }
}
