using System;

using Machine.Specifications;

using Specs.Infrastructure.IIsExpress;

namespace Specs.Infrastructure
{
  public class VerifyRequestStatus : IStartable, ICleanupAfterEveryContextInAssembly
  {
    static bool ErrorLogCheckEnabled = true;
    static LogListener LogListener;

    public void Start()
    {
      LogListener = new LogListener(IisExpressStarter.Process);
    }

    public void Stop()
    {
      if (LogListener != null)
      {
        LogListener.Dispose();
      }
    }

    public void AfterContextCleanup()
    {
      EnsureNoErrorsWereLogged();
    }

    static void EnsureNoErrorsWereLogged()
    {
      if (!ErrorLogCheckEnabled)
      {
        Console.WriteLine("WARNING: IIS error log check disabled, reenabling now");
        ErrorLogCheckEnabled = true;
      }
      else
      {
        LogListener.AssertNoErrorsWereLogged();
      }

      LogListener.ClearLog();
    }

    internal static void DisableOnce()
    {
      Console.WriteLine("WARNING: Disabling IIS error log check");
      ErrorLogCheckEnabled = false;
    }
  }
}
