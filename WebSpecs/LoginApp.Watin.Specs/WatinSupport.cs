using System.IO;

using Machine.Specifications.WatinSupport;

using WatiN.Core;

namespace LoginApp.Specs.Watin
{
	public class WatinSupport : WatinResultSupplementer
	{
		static string TempPath;

		protected override string ImagesPath
		{
			get { return GetTempPath(); }
		}

		protected override int Quality
		{
			get { return 100; }
		}

		protected override int ScalePercentage
		{
			get { return 100; }
		}

		protected override bool ShowGuides
		{
			get { return true; }
		}

		protected override Browser Watin
		{
			get { return WatinSpecs.Browser; }
		}

		protected override bool WriteUrl
		{
			get { return true; }
		}

		static string GetTempPath()
		{
			if (string.IsNullOrEmpty(TempPath))
			{
				TempPath = Path.Combine(Path.GetTempPath(), "watin");

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