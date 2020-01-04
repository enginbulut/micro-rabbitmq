using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using MicroRabbit.Transfer.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, MicroServiceType microServiceType)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp => 
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
            switch (microServiceType)
            {
                case MicroServiceType.Banking:
                    RegisterBankingSpesificServices(services);
                    break;
                case MicroServiceType.Transfer:
                    RegisterTransferSpesificServices(services);
                    break;
                default:
                    throw new NotImplementedException("Unsupported Application");
            }

        }

        public static void RegisterBankingSpesificServices(IServiceCollection services)
        {
            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDBContext>();
        }

        public static void RegisterTransferSpesificServices(IServiceCollection services)
        {
            //subscriptions
            services.AddTransient<TransferEventHandler>();
            //Domain Events
            services.AddTransient<IEventHandler<MicroRabbit.Transfer.Domain.Events.TransferCreatedEvent>, TransferEventHandler>();
            //Application Services
            services.AddTransient<ITransferService, TransferService>();
            //Data
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<TransferDBContext>();
        }
    }
}
