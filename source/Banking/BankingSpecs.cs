using System;

using Machine.Specifications;

namespace Banking
{
  [Subject(typeof(Account), "Funds transfer")]
  public class When_transferring_between_two_accounts
  {
    static Account ToAccount;
    static Account FromAccount;

    Establish context = () =>
    {
      FromAccount = new Account { Balance = 1m };
      ToAccount = new Account { Balance = 1m };
    };

    Because of =
      () => FromAccount.Transfer(1m, ToAccount);

    It should_debit_the_from_account_by_the_amount_transferred =
      () => FromAccount.Balance.ShouldEqual(0m);

    It should_credit_the_to_account_by_the_amount_transferred =
      () => ToAccount.Balance.ShouldEqual(2m);
  }

  [Subject(typeof(Account), "Funds transfer")]
  [Tags("failure")]
  public class When_transferring_an_amount_larger_than_the_balance_of_the_from_account
  {
    static Account FromAccount;
    static Account ToAccount;
    static Exception Exception;

    Establish context = () =>
    {
      FromAccount = new Account { Balance = 1m };
      ToAccount = new Account { Balance = 1m };
    };

    Because of =
      () => Exception = Catch.Exception(() => FromAccount.Transfer(2m, ToAccount));

    It should_fail =
      () => Exception.ShouldNotBeNull();
  }
}
