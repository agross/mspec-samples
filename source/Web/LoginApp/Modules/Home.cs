using Nancy;

namespace LoginApp.Modules
{
  public class Home : NancyModule
  {
    public Home()
    {
      Get["/"] = x => View["home"];
    }
  }
}
