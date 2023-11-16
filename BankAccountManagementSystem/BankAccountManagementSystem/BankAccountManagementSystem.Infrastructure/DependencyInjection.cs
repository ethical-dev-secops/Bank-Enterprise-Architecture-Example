using BankAccountManagementSystem.Application.Common.Interfaces;
using BankAccountManagementSystem.Domain.Common.Interfaces;
using BankAccountManagementSystem.Domain.Repositories;
using BankAccountManagementSystem.Infrastructure.Persistence;
using BankAccountManagementSystem.Infrastructure.Repositories;
using BankAccountManagementSystem.Infrastructure.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace BankAccountManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("DefaultConnection");
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountHolderRepository, AccountHolderRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IChequeAccountRepository, ChequeAccountRepository>();
            services.AddTransient<ISavingsAccountRepository, SavingsAccountRepository>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            return services;
        }
    }
}