using System;
using System.Collections.Generic;

namespace Expenses.Web.Business.Mediator
{
  using Expenses.Web.Business.Mediator.Account;
  using Expenses.Web.Business.Mediator.Categories;
  using Expenses.Web.Business.Mediator.StandingOrders;
  using Expenses.Web.Business.Mediator.Transactions;
  using Expenses.Web.Business.Mediator.User;

  public static class RequestHandlers
  {
    public static IEnumerable<Type> Collection
    {
      get
      {
        yield return typeof(RegistrationHandler);
        yield return typeof(LoginHandler);

        yield return typeof(CategoryListRequestHandler);
        yield return typeof(CategoryRequestHandler);
        yield return typeof(CategoryEditRequestHandler);

        yield return typeof(AccountListRequestHandler);
        yield return typeof(AccountRequestHandler);
        yield return typeof(AccountEditRequestHandler);

        yield return typeof(TransactionRequestHandler);
        yield return typeof(TransactionEditRequestHandler);

        yield return typeof(StandingOrdersListRequestHandler);
        yield return typeof(StandingOrderRequestHandler);
        yield return typeof(StandingOrderEditRequestHandler);
        yield return typeof(StandingOrderProceedRequestHandler);
      }
    }
  }
}
