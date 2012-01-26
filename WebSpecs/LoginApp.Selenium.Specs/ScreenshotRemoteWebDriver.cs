using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace LoginApp.Selenium.Specs
{
	public class ScreenshotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
	{
		public ScreenshotRemoteWebDriver(Uri address, ICapabilities capabilities)
			: base(address, capabilities)
		{
		}

		public Screenshot GetScreenshot()
		{
			var screenshotResponse = Execute(DriverCommand.Screenshot, null);
			var base64 = screenshotResponse.Value.ToString();

			return new Screenshot(base64);
		}
	}
}