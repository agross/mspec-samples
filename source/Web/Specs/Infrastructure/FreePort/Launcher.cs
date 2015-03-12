using System;
using System.Diagnostics;

namespace Specs.Infrastructure.FreePort
{
  class Launcher : IDisposable
  {
    readonly Process _process;

    Launcher(Process process, int port)
    {
      _process = process;
      Port = port;
    }

    public int Port { get; private set; }

    public void Dispose()
    {
      if (_process.HasExited)
      {
        return;
      }

      Console.WriteLine("Killing [{0}] {1}", _process.Id, _process.MainModule.FileName);
      _process.Kill();
    }

    public static Launcher Launch(Settings settings)
    {
      var process = StartOnFirstFreePort(settings);

      return new Launcher(process.Item1, process.Item2);
    }

    static Tuple<Process, int> StartOnFirstFreePort(Settings settings)
    {
      do
      {
        try
        {
          Console.WriteLine("Trying to start {0} {1} on port {2}",
                            settings.Executable,
                            settings.Arguments(settings),
                            settings.Port);

          var process = StartProcess(settings);

          Console.WriteLine("Started [{0}] {1} on port {2}", process.Id, settings.Executable, settings.Port);
          settings.Started(process, settings);

          return new Tuple<Process, int>(process, settings.Port);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }

        settings = settings.TryNextPort();
      }
      while (true);
    }

    static Process StartProcess(Settings settings)
    {
      var process = Process.Start(new ProcessStartInfo
      {
        FileName = settings.Executable,
        Arguments = settings.Arguments(settings),
        UseShellExecute = false,
        RedirectStandardError = true,
        RedirectStandardOutput = true
      });

      settings.CheckStarted(process);

      return process;
    }
  }
}
