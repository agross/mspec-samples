using System;

using Machine.Specifications;

namespace Behaviors
{
  [Subject("Date time parsing")]
  public class when_a_date_is_parsed_with_the_regular_expression_parser : DateTimeParsingSpecs
  {
    Establish context = () => { Parser = new RegexParser(); };

    Because of = () => { ParsedDate = Parser.Parse("2009/01/21"); };

    Behaves_like<DateTimeParsingBehavior> a_date_time_parser;

  	It should_succeed =
  		() => true.ShouldBeTrue();
  }

  [Subject("Date time parsing")]
  public class when_a_date_is_parsed_by_the_infrastructure : DateTimeParsingSpecs
  {
    Establish context = () => { Parser = new InfrastructureParser(); };

    Because of = () => { ParsedDate = Parser.Parse("2009/01/21"); };

    Behaves_like<DateTimeParsingBehavior> a_date_time_parser;
  }

  public abstract class DateTimeParsingSpecs
  {
    protected static DateTime ParsedDate;
    protected static IParser Parser;
  }

  [Behaviors]
  public class DateTimeParsingBehavior
  {
    protected static DateTime ParsedDate;

    It should_parse_the_date = 
		() => ParsedDate.ShouldEqual(new DateTime(2009, 1, 21));
  }
}
