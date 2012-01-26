using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

using Machine.Specifications;

namespace LoginApp.Watin.Specs
{
	public class IisExpress : IAssemblyContext
	{
		Process _iisExpress;

		public void OnAssemblyStart()
		{
			var iis = Path.Combine(Path.GetFullPath(ConfigurationManager.AppSettings["PathToIisExpress"]), "iisexpress.exe");
			var app = Path.GetFullPath(ConfigurationManager.AppSettings["PathToWebApp"]);

			_iisExpress = new Process();
			var psi = new ProcessStartInfo
			          {
			          	FileName = iis,
			          	Arguments = String.Format(@"/path:""{0}"" /port:1337", app)
			          };

			_iisExpress.StartInfo = psi;
			_iisExpress.Start();
		}

		public void OnAssemblyComplete()
		{
			if (!_iisExpress.HasExited)
			{
				_iisExpress.Kill();
			}
		}
	}
}