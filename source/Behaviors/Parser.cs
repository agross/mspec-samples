using System;

namespace Behaviors
{
  public interface IParser
  {
    DateTime Parse(string date);
  }

  class RegexParser : IParser
  {
    public DateTime Parse(string date)
    {
      // Parse with a regular expression. Not that it's recommended, but that's why this example is contrived.
      return new DateTime(2009, 1, 21);
    }
  }

  class InfrastructureParser : IParser
  {
    public DateTime Parse(string date)
    {
      // Parse with DateTime.Parse.
      return new DateTime(2009, 1, 21);
    }
  }
}
