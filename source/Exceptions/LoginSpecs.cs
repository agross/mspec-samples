using System;
using System.Security;

using Machine.Specifications;

namespace Exceptions
{
  [Subject("Authentication")]
  public class When_authenticating_a_user_without_providing_a_password
  {
    static AuthenticationService Service;
    static Exception Exception;

    Establish context =
      () => Service = new AuthenticationService();

    Because of =
      () => Exception = Catch.Exception(() => Service.Authenticate("user", ""));

    It should_fail =
      () => Exception.ShouldBeOfExactType<SecurityException>();
  }

  class AuthenticationService
  {
    public void Authenticate(string user, string password)
    {
      if (String.IsNullOrWhiteSpace(password))
      {
        throw new SecurityException("You must provide a password");
      }

      throw new NotImplementedException();
    }
  }
}
