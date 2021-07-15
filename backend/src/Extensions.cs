using BenefitsApp.Business.Entities.Benefits;
using BenefitsApp.Business.Entities.Discounts;
using Microsoft.Extensions.DependencyInjection;

namespace BenefitsApp
{
  public static class Extensions
  {
    public static IServiceCollection AddBenefits(this IServiceCollection services)
    {
      services.AddScoped<IBenefit, GenericBenefit>();
      return services;
    }
    
    public static IServiceCollection AddDiscounts(this IServiceCollection services)
    {
      services.AddScoped<IDiscount, NameDiscount>();
      return services;
    }
  }
}