using Autofac;

namespace WMS.Core.Infrastructure.DependencyManagment
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder);

        int Order { get; }
    }
}
