using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ITventory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                    );
            return services;

            // Extension method z katalogu shared może być traktowany jako "podstawowa" wersja, która potem jest rozszerzana
            // np w warstwie application. Jeżeli dodamy referencje do katalogu shared, możemy używać tej podstawowej wersji
            // na instancjach IServiceCollection gdziekolwiek indziej. Jeżeli dodamy "nową" klasę o tej samej
            // nazwie w innej warstwie, możemy dołozyć funkcjonalności, ale nadal możemy korzystać z tych
            // podstawowych funkcji definiowanych w Shared




            // This extension method registers command-related services into the Dependency Injection (DI) container.
            // It registers the ICommandDispatcher with a singleton lifetime, meaning there will be a single instance
            // of the InMemoryCommandDispatcher used throughout the application's lifetime.
            // Additionally, it uses Scrutor to automatically scan the current assembly for all classes that implement the
            // ICommandHandler<TCommand> interface. All of these classes are registered with a scoped lifetime, which means
            // an instance will be created for each request or operation (e.g., for each web request in a web application).
            // The purpose of this setup is to allow for automatic discovery and registration of command handlers
            // without the need to manually register each one, improving maintainability and reducing boilerplate code.
            // This makes it easy to add new command handlers by simply implementing the ICommandHandler<TCommand> interface
            // and having them automatically registered in the DI container.



        }
    }
}
