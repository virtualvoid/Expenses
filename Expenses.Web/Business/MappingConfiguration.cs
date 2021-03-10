using AutoMapper;

namespace Expenses.Web.Business
{
  using Expenses.Web.Business.Data;
  using Expenses.Web.Business.Mediator.Account;
  using Expenses.Web.Business.Mediator.Categories;
  using Expenses.Web.Business.Mediator.StandingOrders;
  using Expenses.Web.Business.Mediator.Transactions;
  using Expenses.Web.Business.Mediator.User;
  using Expenses.Web.ViewModels;
  using System;

  public class MappingConfiguration : Profile
  {
    public MappingConfiguration()
    {
      CreateMap<RegistrationViewModel, RegistrationRequest>();
      CreateMap<LoginViewModel, LoginRequest>();

      CreateMap<CategoryViewModel, Category>().ReverseMap();
      CreateMap<CategoryViewModel, CategoryEditRequest>().ReverseMap();

      CreateMap<AccountViewModel, Account>().ReverseMap();
      CreateMap<AccountViewModel, AccountEditRequest>().ReverseMap();

      CreateMap<TransactionViewModel, Transaction>()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.Parse<TransactionType>(src.Type)))
        .ReverseMap()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.GetName(typeof(TransactionType), src.Type)));

      CreateMap<TransactionViewModel, TransactionEditRequest>()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.Parse<TransactionType>(src.Type)))
        .ReverseMap()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.GetName(typeof(TransactionType), src.Type)));

      CreateMap<StandingOrderViewModel, StandingOrder>()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.Parse<TransactionType>(src.Type)))
        .ReverseMap()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.GetName(typeof(TransactionType), src.Type)));

      CreateMap<StandingOrderViewModel, StandingOrderEditRequest>()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.Parse<TransactionType>(src.Type)))
        .ReverseMap()
        .ForMember(dst => dst.Type, it => it.MapFrom(src => Enum.GetName(typeof(TransactionType), src.Type)));
    }
  }
}
