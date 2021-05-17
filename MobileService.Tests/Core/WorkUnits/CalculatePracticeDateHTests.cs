using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.WorkUnits;
using MobileService.Entities.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.WorkUnits
{
    public class CalculatePracticeDateHTests
    {
        [Fact]
        public async Task TestA()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var calculatePracticeDateQ = new CalculatePracticeDateQ(2, FlashcardProgress.DontKnow);

            Assert.Equal(DateTime.Now.Date, await mediator.Send(calculatePracticeDateQ));
        }

        [Fact]
        public async Task TestB()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var calculatePracticeDateQ = new CalculatePracticeDateQ(2, FlashcardProgress.MediumKnow);

            Assert.Equal(DateTime.Now.Date.AddDays(1), await mediator.Send(calculatePracticeDateQ));
        }

        [Fact]
        public async Task TestC()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var calculatePracticeDateQ = new CalculatePracticeDateQ(2, FlashcardProgress.Know);

            Assert.Equal(DateTime.Now.Date.AddDays(3), await mediator.Send(calculatePracticeDateQ));
        }

        [Fact]
        public async Task TestD()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var calculatePracticeDateQ = new CalculatePracticeDateQ(-1, FlashcardProgress.Know);

            await Assert.ThrowsAsync<NotSupportedException>(async () => await mediator.Send(calculatePracticeDateQ));
        }
    }
}
