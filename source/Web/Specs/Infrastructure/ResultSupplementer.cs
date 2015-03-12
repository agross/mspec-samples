using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

using Machine.Specifications;

using OpenQA.Selenium;

namespace Specs.Infrastructure
{
  public class WebDriverResultSupplementer : ISupplementSpecificationResults
  {
    static string TempPath;

    protected virtual string ImagesPath
    {
      get
      {
        return GetTempPath();
      }
    }

    protected virtual ITakesScreenshot Screenshotter
    {
      get
      {
        return Browser.Instance as ITakesScreenshot;
      }
    }

    protected virtual IWebDriver WebDriver
    {
      get
      {
        return Browser.Instance;
      }
    }

    public Result SupplementResult(Result result)
    {
      if (result.Status != Status.Failing)
      {
        return result;
      }

      var guid = Guid.NewGuid();
      var pageScreenshotPath = Path.Combine(ImagesPath, guid + "-full-page-screenshot.png");
      var htmlPath = Path.Combine(ImagesPath, guid + ".html");

      var supplement = new Dictionary<string, string>();

      CaptureScreenshot(supplement, pageScreenshotPath);
      CaptureHtmlSource(supplement, htmlPath);

      return Result.Supplement(result, "WebDriver", supplement);
    }

    void CaptureScreenshot(IDictionary<string, string> supplement, string screenshotPath)
    {
      try
      {
        Screenshotter.GetScreenshot().SaveAsFile(screenshotPath, ImageFormat.Png);
        supplement["img-full-page-screenshot"] = screenshotPath;
      }
      catch (Exception error)
      {
        Report(error, "img-full-page-screenshot", supplement);
      }
    }

    void CaptureHtmlSource(IDictionary<string, string> supplement, string htmlPath)
    {
      try
      {
        using (var writer = new StreamWriter(htmlPath))
        {
          writer.Write(WebDriver.PageSource);
        }
        supplement["html-source"] = htmlPath;
      }
      catch (Exception error)
      {
        Report(error, "html-source", supplement);
      }
    }

    static void Report(Exception error, string type, IDictionary<string, string> supplement)
    {
      supplement[type] = error.ToString();

      Console.Error.WriteLine();
      Console.Error.WriteLine("Problem capturing WebDriver {0}", type);
      Console.Error.WriteLine(error);
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