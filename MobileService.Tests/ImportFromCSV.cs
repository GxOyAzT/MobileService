using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.Flashcards;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests
{
    public class ImportFromCSV
    {
        //[Fact]
        //public async Task Import()
        //{
        //    var serviceProvider = new ServiceCollection()
        //        .AddTransient<ICollectionRepo, CollectionRepo>()
        //        .AddTransient<IFlashcardRepo, FlashcardRepo>()
        //        .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.ProductionConnectionString))
        //        .AddMediatR(typeof(MediatREntryPoint).Assembly)
        //        .BuildServiceProvider();

        //    List<Fla> flas = File.ReadAllLines(@"C:\Data\Uslugi.csv")
        //        .Skip(1)
        //        .Select(e => new Fla(e))
        //        .ToList();

        //    var service = serviceProvider.GetService<IMediator>();

        //    foreach(var fla in flas)
        //    {
        //        var inputModel = new FlashcardModelC()
        //        {
        //            CollectionId = Guid.Parse("88ed36ed-1ad8-4d98-ca8f-08d913aea372"),
        //            Foreign = fla.back,
        //            Native = fla.front
        //        };

        //        var insertFlashcardC = new InsertFlashcardC(inputModel, "01b00fcb-47ed-4194-a842-9210fac9e269");

        //        var response = await service.Send(insertFlashcardC, new System.Threading.CancellationToken());
        //    }
        //}

        //[Fact]
        //public async Task Manipulate()
        //{
        //    var serviceProvider = new ServiceCollection()
        //        .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.ProductionConnectionString))
        //        .BuildServiceProvider();

        //    var collections = new List<Guid>()
        //    { 
        //        Guid.Parse("88ed36ed-1ad8-4d98-ca8f-08d913aea372")
        //    };


        //    using (var db = serviceProvider.GetService<AppDbContext>())
        //    {
        //        var flashcards = db.Collections
        //            .Where(e => collections.Contains(e.Id))
        //            .Include(e => e.FlashcardModels)
        //            .ThenInclude(e => e.FlashcardProgressModels)
        //            .SelectMany(e => e.FlashcardModels)
        //            .SelectMany(e => e.FlashcardProgressModels)
        //            .ToList();

        //        foreach (var flashcard in flashcards)
        //        {
        //            flashcard.CorrectInRow = 4;
        //            flashcard.PracticeDate = DateTime.Now.Date.AddDays((new Random()).Next(0, 31));
        //        }

        //        db.FlashcardProgresses.UpdateRange(flashcards);
        //        db.SaveChanges();
        //    }
        //}
    }

    //public class Fla
    //{
    //    public string front;
    //    public string back;

    //    public Fla(string v)
    //    {
    //        string[] values = v.Split(';');
    //        front = values[0].Remove(0,1);
    //        front = front.Remove(front.Length - 1, 1);
    //        back = values[1].Remove(0, 1);
    //        back = back.Remove(back.Length - 1, 1);
    //    }
    //}
}
