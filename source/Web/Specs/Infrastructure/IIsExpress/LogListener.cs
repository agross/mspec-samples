using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Machine.Specifications;

namespace Specs.Infrastructure.IIsExpress
{
  class LogListener : IDisposable
  {
    static readonly List<Tuple<string, string>> RequestStatus = new List<Tuple<string, string>>();
    static readonly StringBuilder StandardOutput = new StringBuilder();
    static readonly Regex RequestEnded = new Regex("Request ended: \"(.*)\" with HTTP status (.*)");
    readonly Thread _listener;

    public LogListener(Process iisExpress)
    {
      _listener = new Thread(() => ListenToStandardOutput(iisExpress, StandardOutput, RequestStatus));
      _listener.Start();
    }

    public void Dispose()
    {
      if (_listener == null || !_listener.IsAlive)
      {
        return;
      }

      _listener.Join(TimeSpan.FromSeconds(5));

      if (_listener.IsAlive)
      {
        _listener.Abort();
      }
    }

    static void ListenToStandardOutput(Process iisExpress,
                                       StringBuilder standardOutput,
                                       ICollection<Tuple<string, string>> requestStatus)
    {
      while (true)
      {
        var line = iisExpress.StandardOutput.ReadLine();
        if (line == null)
        {
          return;
        }

        standardOutput.AppendLine(line);

        var match = RequestEnded.Match(line);
        if (!match.Success)
        {
          continue;
        }

        var url = match.Groups[1].Value;
        var status = match.Groups[2].Value;

        requestStatus.Add(new Tuple<string, string>(status, url));
      }
    }

    internal void AssertNoErrorsWereLogged()
    {
      var alltext = StandardOutput.ToString();
      alltext.ShouldNotBeEmpty();

      var requestStatus = RequestStatus.ToArray();

      requestStatus.Length.ShouldBeGreaterThan(0);

      var errorCodes4xxAnd5xx = from c in requestStatus
                                let code = c.Item1
                                let url = c.Item2
                                where code.StartsWith("4") || code.StartsWith("5")
                                select String.Format("{0}: {1}", code, url);

      errorCodes4xxAnd5xx.ShouldBeEmpty();
    }

    internal void ClearLog()
    {
      StandardOutput.Clear();
      RequestStatus.Clear();
    }
  }
}
