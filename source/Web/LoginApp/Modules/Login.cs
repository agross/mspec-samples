using Nancy;

namespace LoginApp.Modules
{
  public class Login : NancyModule
  {
    public Login()
    {
      Get["/login"] = x => View["login"];

      Post["login/send"] = x =>
      {
        var userName = Request.Form.UserName;
        var password = Request.Form.Password;

        if (userName.HasValue && password.HasValue)
        {
          if (userName == "admin" && password == "secret")
          {
            return Response.AsRedirect("/");
          }
        }

        return Response.AsRedirect("/login");
      };
    }
  }
}
