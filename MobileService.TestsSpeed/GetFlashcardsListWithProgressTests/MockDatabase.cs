using MobileService.Entities.Models;
using MobileService.TestsSpeed;
using System;
using System.Collections.Generic;

namespace MobileService.TestsSpeedGetFlashcardsListWithProgressTests
{
    public class MockDatabase
    {
        public static void Mock(int flashcardQuantity)
        {
            var collectionModel = new CollectionModel() { Id = Guid.Parse("a36d18ca-a912-49da-bfc2-2a41b1d87e7c"), Name = "collection name", UserId = "be723b31-56b0-4b6d-a7c3-b0d979ed4d00" };

            collectionModel.FlashcardModels = new List<FlashcardModel>();

            for (int i = 1; i <= flashcardQuantity; i++)
            {
                collectionModel.FlashcardModels.Add(new FlashcardModel()
                {
                    Foreign = $"Foreign {i}",
                    Native = $"Native {i}",
                    FlashcardProgressModels = new List<FlashcardProgressModel>()
                    {
                        new FlashcardProgressModel()
                        {
                            PracticeDate = DateTime.Now.Date.AddDays((new Random()).Next(-5, 100)),
                            CorrectInRow = (new Random()).Next(0, 10),
                            PracticeDirection = Entities.Enums.PracticeDirection.ForeignToNative
                        },
                        new FlashcardProgressModel()
                        {
                            PracticeDate = DateTime.Now.Date.AddDays((new Random()).Next(-5, 100)),
                            CorrectInRow = (new Random()).Next(0, 10),
                            PracticeDirection = Entities.Enums.PracticeDirection.NativeToForeign
                        }
                    }
                });
            }

            using (var db  = MockDatabaseFactory.Build())
            {
                db.Collections.RemoveRange(db.Collections);

                db.SaveChanges();

                db.Collections.Add(collectionModel);

                db.SaveChanges();
            }
        }
    }
}
