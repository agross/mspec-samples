using System;
using System.Diagnostics;

namespace Specs.Infrastructure.FreePort
{
  class Settings
  {
    public Settings(string executable, int initialPort, int addPort = 100)
      : this(executable,
             initialPort,
             addPort,
             initialPort,
             _ => String.Empty,
             _ => { },
             (_, __) => { })
    {
    }

    Settings(string executable,
             int initialPort,
             int addPort,
             int thisPort,
             Func<Settings, string> arguments,
             Action<Process> checkStarted,
             Action<Process, Settings> started)
    {
      Executable = executable;
      InitialPort = initialPort;
      Port = thisPort;
      AddPort = addPort;
      Arguments = arguments;
      CheckStarted = checkStarted;
      Started = started;

      EnsurePortRange();
    }

    public string Executable { get; private set; }
    public Func<Settings, string> Arguments { get; set; }
    public Action<Process> CheckStarted { get; set; }
    public Action<Process, Settings> Started { get; set; }
    int InitialPort { get; set; }
    int AddPort { get; set; }
    public int Port { get; private set; }

    void EnsurePortRange()
    {
      if (Port >= InitialPort && Port <= MaxPort(InitialPort, AddPort))
      {
        return;
      }

      throw new Exception(String.Format("Could not start {0} on a port in the range from {1} to {2}",
                                        Executable,
                                        InitialPort,
                                        MaxPort(InitialPort, AddPort)));
    }

    public Settings TryNextPort()
    {
      return new Settings(Executable, InitialPort, AddPort, Port + 1, Arguments, CheckStarted, Started);
    }

    static int MaxPort(int initialPort, int addPort)
    {
      var maxPort = initialPort + addPort;
      return maxPort > UInt16.MaxValue ? UInt16.MaxValue : maxPort;
    }
  }
}
