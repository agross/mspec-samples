using System.Configuration;
using System.Diagnostics;
using System.IO;

using Machine.Specifications;

namespace LoginApp.Selenium.Specs
{
	public class ChromeDriver : IAssemblyContext
	{
		Process _chromeDriver;

		public void OnAssemblyStart()
		{
			var chromeDriver = Path.Combine(Path.GetFullPath(ConfigurationManager.AppSettings["PathToChromeDriver"]),
			                                "chromedriver.exe");

			_chromeDriver = new Process();
			var psi = new ProcessStartInfo
			          {
			          	FileName = chromeDriver
			          };

			_chromeDriver.StartInfo = psi;
			_chromeDriver.Start();
		}

		public void OnAssemblyComplete()
		{
			if (!_chromeDriver.HasExited)
			{
				_chromeDriver.Kill();
			}
		}
	}
}
