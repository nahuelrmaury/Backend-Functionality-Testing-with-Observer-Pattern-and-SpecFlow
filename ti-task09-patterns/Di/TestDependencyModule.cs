using Autofac;
using BackendTests.Clients;
using BackendTests.Steps;
using BackendTests.Utils;

namespace BackendTests.Di
{
    public class TestDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UserServiceClient>()
                .AsSelf();

            builder
                .RegisterType<WalletServiceClient>()
                .AsSelf();

            builder
                .RegisterType<UserAssertSteps>()
                .AsSelf();

            builder
                .RegisterType<UserSteps>()
                .AsSelf();

            builder
                .RegisterType<WalletAssertSteps>()
                .AsSelf();

            builder
                .RegisterType<WalletSteps>()
                .AsSelf();

            builder
                .RegisterType<UserGenerator>()
                .AsSelf();

            builder
                .RegisterType<StringGenerator>()
                .AsSelf();

            builder
                .RegisterType<ChargeGenerator>()
                .AsSelf();

            builder
                .RegisterType<DataContext>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
