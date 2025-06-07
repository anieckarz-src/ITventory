using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ITventory.Shared.Commands
{
    internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandDispatcher(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;


        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand_
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            //wymaga aby klasa implementująca dany interfejs była zarejestrowana w DI kontenerze - 'get required service'. Inaczej
            //wyrzuci wyjątek

            await handler.HandleAsync(command);
        }
    }
}
