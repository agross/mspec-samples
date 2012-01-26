using System.IO;

using Machine.Specifications.SeleniumSupport;

using Selenium;

namespace LoginApp.Selenium.Specs
{
	public class SeleniumSupport : SeleniumResultSupplementer
	{
		static string TempPath;

		protected override string ImagesPath
		{
			get { return GetTempPath(); }
		}

		protected override ISelenium Selenium
		{
			get { return SeleniumSpecs.Selenium; }
		}

		static string GetTempPath()
		{
			if (string.IsNullOrEmpty(TempPath))
			{
				TempPath = Path.Combine(Path.GetTempPath(), "selenium");

				if (Directory.Exists(TempPath))
				{
					Directory.Delete(TempPath, true);
				}

				Directory.CreateDirectory(TempPath);
			}

			return TempPath;
		}
	}
}